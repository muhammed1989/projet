using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        private static String adresseIP;
        private int port;
        private String pseudo;
        TcpClient client = new TcpClient();
        NetworkStream stream;
        Thread tRead;
        bool stopRead = false;
        byte[] sndBytesClick = new byte[2];
        int lastClick;
        private delegate void DelBtnClick(object sender);
        private delegate void DelInitialise();
        private delegate void DelDispose();
        private delegate void DelMessageBox();  //car sinon la MessageBox n'est pas modale

        /// Etat de la partie (cf enumération 'etatPartie')
        private EtatPartie etatCourant;


        bool turn = true; // true = X turn; false = Y turn
        int turn_count = 0;

        public Form1(Joueur j)
        {

            port = j.getPort();
            adresseIP = j.getAdreseIP();
            pseudo = j.getPseudo();
            InitializeComponent();
            label1.Text = pseudo;
            IPAddress serverAddress = IPAddress.Parse(adresseIP);
            client.Connect(serverAddress, port);
            stream = client.GetStream();

            byte[] sndBytesPseudo = GetBytes(pseudo);
            stream.Write(sndBytesPseudo, 0, sndBytesPseudo.Length);

            byte[] rcvBytesPseudo = new byte[100];
            stream.Read(rcvBytesPseudo, 0, rcvBytesPseudo.Length);
            label2.Text = GetString(rcvBytesPseudo);

            //On lance la procédure qui initilialise un nouveau jeu
            initialise();
            tRead = new Thread(threadRead);
            tRead.IsBackground = true;
            tRead.Start();
           
        }

        private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("By Yilmaz", "TicTacToe");
        }

        private void QuitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            b.Text = pseudo;
            

            turn = !turn;       // X et 0 à la fois
            b.Enabled = false; // bloquer modification case
            turn_count++;
            Check_Gagnant();
        }

        private void Check_Gagnant()
        {
            bool jeu_fini = false;

            //horizontal
            if ((A1.Text == A2.Text) && (A2.Text == A3.Text) && (!A1.Enabled))
                jeu_fini = true;
            else if ((B1.Text == B2.Text) && (B2.Text == B3.Text) && (!B1.Enabled))
                jeu_fini = true;
            else if ((C1.Text == C2.Text) && (C2.Text == C3.Text) && (!C1.Enabled))
                jeu_fini = true;

            //vertical
            else if ((A1.Text == B1.Text) && (B1.Text == C1.Text) && (!A1.Enabled))
                jeu_fini = true;
            else if ((A2.Text == B2.Text) && (B2.Text == C2.Text) && (!A2.Enabled))
                jeu_fini = true;
            else if ((A3.Text == B3.Text) && (B3.Text == C3.Text) && (!A3.Enabled))
                jeu_fini = true;

            //diagonal
            else if ((A1.Text == B2.Text) && (B2.Text == C3.Text) && (!A1.Enabled))
                jeu_fini = true;
            else if ((A3.Text == B2.Text) && (B2.Text == C1.Text) && (!C1.Enabled))
                jeu_fini = true;



            if (jeu_fini)
            {
                disableButtons();

                string gagnant = "";
                if (turn)
                    gagnant = "0";
                else
                    gagnant = "X";

                MessageBox.Show(gagnant + " a gagné", "Félicitation");

            }//end if
            else
            {
                if (turn_count == 9)
                    MessageBox.Show("égalité", "Déception!");
            }

        }


        private void disableButtons()
        {
            

                foreach (Control cont in this.Controls)
                {
                    if (cont is Button)
                    {
                        cont.Enabled = false;
                    }

                }//end foreach
        }

        private void NouveauJeuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            turn = true;
            turn_count = 0;

            try
            {

                foreach (Control c in Controls)
                {
                    Button b = (Button)c;
                    b.Enabled = true;       // activation bouton
                    b.Text = "";            //initialisation bouton
                }//end foreach
            }//end try
            catch { }
        }


       private void button_enter(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Enabled)
            {
                if (turn)
                    b.Text = "X";
                else
                    b.Text = "O";
            }//end if
        }

        private void button_leave(object sender, EventArgs e)
        {
            Button b =(Button)sender;
            if(b.Enabled)
            {
                b.Text= "";
            }
        }


        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }


        private void initialise() /// Initialise une nouvelle partie
        {
            //On positionne les différents paramètres
            sndBytesClick[0] = 0;
            lastClick = 0;

            //A qui est le tour
            byte[] rcvBytesTour = new byte[1];
            stream.Read(rcvBytesTour, 0, rcvBytesTour.Length);
            switch (rcvBytesTour[0])
            {
                case 255:
                    ChangeStatusStrip(EtatPartie.EnCours, "A votre tour", Color.Green);
                    break;
                case 0:
                    ChangeStatusStrip(EtatPartie.EnAttente, "En attente", Color.DarkOrange);
                    break;
            }
        }

        private void ChangeStatusStrip(EtatPartie etat, string str, Color color)
        {
            etatCourant = etat;
            label3.Text = str;
            label3.ForeColor = color;
            switch (etat)
            {
                case EtatPartie.EnCours:
                    this.Cursor = Cursors.Default;
                    break;
                case EtatPartie.EnAttente:
                    this.Cursor = Cursors.WaitCursor;
                    break;
                case EtatPartie.Fini:
                    label3.Text = "Rejouer";
                    this.Cursor = Cursors.Default;
                    break;
            }
        }

        private void threadRead()
        {
            try
            {
                while (!stopRead)
                {
                    byte[] rcvBytesClick = new byte[2];
                    stream.Read(rcvBytesClick, 0, rcvBytesClick.Length);

                    switch (rcvBytesClick[0])
                    {
                        case 0:
                            int rcvClick = rcvBytesClick[1];
                            this.Invoke(new DelBtnClick(btn_Click_J2), rcvClick);
                            break;
                        case 1:  //l'autre a abandonné
                            this.Invoke(new DelMessageBox(victoire));
                            break;
                        case 2:  //rejouer
                            this.Invoke(new DelInitialise(initialise));
                            break;
                        case 3:  //quitter
                            stopRead = true;
                            this.Invoke(new DelMessageBox(quitter));
                            this.Invoke(new DelDispose(Dispose));
                            break;
                    }
                }
            }
            catch
            {
                this.Invoke(new DelMessageBox(serveurHS));
                this.Invoke(new DelDispose(Dispose));
            }
        }


        void btn_Click(object sender, EventArgs e)
        {
            Button btn = ((Button)sender);

            if (etatCourant == EtatPartie.EnCours && btn.Text == "")
            {
                lastClick = Convert.ToInt32(btn.Tag);

                btn.Text = pseudo;
                    //btn.ForeColor = Color.White;
                    //btn.BackColor = Color.Blue;
                    //btn.Click -= new EventHandler(btn_Click);


                    ChangeStatusStrip(EtatPartie.EnAttente, "En attente", Color.DarkOrange);
                    sndBytesClick[1] = (byte) Convert.ToInt32(btn.Tag);

                    stream.Write(sndBytesClick, 0, sndBytesClick.Length);
                    stream.Flush();

                    //// VERIFIER FIN DE JEU
                    //if (nombreDeMinesTrouvees + nombreDeMinesTrouveesParJ2 == nombreDeMines)
                    //{
                    //    if (nombreDeMinesTrouvees > nombreDeMinesTrouveesParJ2) victoire();
                    //    if (nombreDeMinesTrouvees < nombreDeMinesTrouveesParJ2) defaite();
                    //    if (nombreDeMinesTrouvees == nombreDeMinesTrouveesParJ2) exaequo();
                    //}
                
                
            }
        }

        private void btn_Click_J2(object sender)
        {
            Button btn = new Button();
            foreach (Control item in this.Controls)
            {
                if (item is Button && item.Tag.ToString()== sender.ToString())
                {
                    btn = (Button)item;
                }
            }
            lastClick = Convert.ToInt32(btn.Tag);


            btn.Text = "--" + pseudo;

                //btn.ForeColor = Color.White;
                //btn.BackColor = Color.Red;
                ChangeStatusStrip(EtatPartie.EnCours, "A votre tour", Color.Green);

            //// VERIFIER FIN DE JEU
                //if (nombreDeMinesTrouvees + nombreDeMinesTrouveesParJ2 == nombreDeMines)
                //{
                //    if (nombreDeMinesTrouvees > nombreDeMinesTrouveesParJ2) victoire();
                //    if (nombreDeMinesTrouvees < nombreDeMinesTrouveesParJ2) defaite();
                //    if (nombreDeMinesTrouvees == nombreDeMinesTrouveesParJ2) exaequo();
                //}
            
        }


        private void victoire()
        {
            MessageBox.Show("Bravo, Vous avez gagné :)", "Fin");
        }

        private void defaite()
        {
            MessageBox.Show("Vous avez perdu :(", "Fin");
        }

        private void exaequo()
        {
            MessageBox.Show("Ex aequo :o", "Fin");
        }

        private void quitter()
        {
            MessageBox.Show("L'autre joueur a quitté le jeu :o", "Fin");
        }

        private void serveurHS()
        {
            MessageBox.Show("Désolé, Le serveur est HS.", "Erreur");
        }





    }
}
