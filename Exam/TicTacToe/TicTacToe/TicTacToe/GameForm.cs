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
    public partial class GameForm : Form
    {
        public enum GameStatus
        {
            Playing, Finished, Waiting
        }

        private static String adresseIP;
        private int port;
        private String playerName;
        TcpClient client = new TcpClient();
        NetworkStream stream;
        Thread tRead;
        bool stopRead = false;
        byte[] sndBytesClick = new byte[2];

        private delegate void DelBtnClick(object sender);
        private delegate void DelInitialize();
        private delegate void DelDispose();
        private delegate void DelMessageBox();  //car sinon la MessageBox n'est pas modale

        /// Etat de la partie
        private GameStatus gameStatus;

        int count = 0;

        public GameForm(string playerName)
        {

            port = 8001;
            adresseIP = "127.0.0.1";
            this.playerName = playerName;
            InitializeComponent();
            labelMe.Text = playerName;
            IPAddress serverAddress = IPAddress.Parse(adresseIP);
            client.Connect(serverAddress, port);
            stream = client.GetStream();

            byte[] sndBytesName = GetBytes(playerName);
            stream.Write(sndBytesName, 0, sndBytesName.Length);

            byte[] rcvBytesName = new byte[100];
            stream.Read(rcvBytesName, 0, rcvBytesName.Length);
            labelOther.Text = GetString(rcvBytesName);

            //On lance la procédure qui initilialise un nouveau jeu
            Initialize();
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
            sndBytesClick[0] = 3;
            stream.Write(sndBytesClick, 0, sndBytesClick.Length);
            stream.Flush();
            Application.Exit();
        }

        public bool GameFinished()
        {
            bool gameFinished = false;

            //horizontal
            if ((A1.Text == A2.Text) && (A2.Text == A3.Text) && (!A1.Enabled))
                gameFinished = true;
            else if ((B1.Text == B2.Text) && (B2.Text == B3.Text) && (!B1.Enabled))
                gameFinished = true;
            else if ((C1.Text == C2.Text) && (C2.Text == C3.Text) && (!C1.Enabled))
                gameFinished = true;

            //vertical
            else if ((A1.Text == B1.Text) && (B1.Text == C1.Text) && (!A1.Enabled))
                gameFinished = true;
            else if ((A2.Text == B2.Text) && (B2.Text == C2.Text) && (!A2.Enabled))
                gameFinished = true;
            else if ((A3.Text == B3.Text) && (B3.Text == C3.Text) && (!A3.Enabled))
                gameFinished = true;

            //diagonal
            else if ((A1.Text == B2.Text) && (B2.Text == C3.Text) && (!A1.Enabled))
                gameFinished = true;
            else if ((A3.Text == B2.Text) && (B2.Text == C1.Text) && (!C1.Enabled))
                gameFinished = true;

            return gameFinished;
        }

        private void DisableButtons()
        {
            foreach (Control cont in this.Controls)
            {
                if (cont is Button)
                {
                    cont.Enabled = false;
                }
            }
        }

        private void NouveauJeuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sndBytesClick[0] = 2;
            stream.Write(sndBytesClick, 0, sndBytesClick.Length);
            stream.Flush();
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

        private void Initialize() /// Initialise une nouvelle partie
        {
            foreach (Control c in this.Controls)
            {
                if (c is Button)
                {
                    c.Enabled = true;
                    c.Text = "";
                    c.BackColor = Color.White;
                }
            }

            A1.Tag = 1;
            A2.Tag = 2;
            A3.Tag = 3;
            B1.Tag = 4;
            B2.Tag = 5;
            B3.Tag = 6;
            C1.Tag = 7;
            C2.Tag = 8;
            C3.Tag = 9;

            count = 0;
            sndBytesClick[0] = 0;

            //Tour de quel joueur ?
            byte[] rcvBytesTurn = new byte[1];
            stream.Read(rcvBytesTurn, 0, rcvBytesTurn.Length);
            switch (rcvBytesTurn[0])
            {
                case 1:
                    ChangeLabel(GameStatus.Playing, "A votre tour", Color.Green);
                    break;
                case 0:
                    ChangeLabel(GameStatus.Waiting, "En attente", Color.Black);
                    break;
            }
        }

        private void ChangeLabel(GameStatus status, string message, Color color)
        {
            gameStatus = status;
            labelEtatPartie.Text = message;
            labelEtatPartie.ForeColor = color;
            switch (gameStatus)
            {
                case GameStatus.Playing:
                    this.Cursor = Cursors.Default;
                    break;
                case GameStatus.Waiting:
                    this.Cursor = Cursors.WaitCursor;
                    break;
                case GameStatus.Finished:
                    labelEtatPartie.Text = "Partie terminée";
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
                            this.Invoke(new DelBtnClick(btn_Click_Player2), rcvClick);
                            break;
                        case 2:  //Nouvelle partie
                            this.Invoke(new DelInitialize(Initialize));
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
                this.Invoke(new DelMessageBox(erreurServeur));
                this.Invoke(new DelDispose(Dispose));
            }
        }

        void btn_Click(object sender, EventArgs e)
        {
            Button btn = ((Button)sender);

            if (gameStatus == GameStatus.Playing && btn.Text == "")
            {
                btn.Text = "X";
                btn.BackColor = Color.Blue;

                count++;

                btn.Enabled = false;

                ChangeLabel(GameStatus.Waiting, "En attente", Color.Black);
                sndBytesClick[1] = (byte)Convert.ToInt32(btn.Tag);

                stream.Write(sndBytesClick, 0, sndBytesClick.Length);
                stream.Flush();

                // FIN DE JEU
                if (GameFinished())
                {
                    victoire();
                    DisableButtons();
                    ChangeLabel(GameStatus.Finished, "Terminée", Color.Black);
                }
                else if (count == 9)
                {
                    egalite();
                    DisableButtons();
                    ChangeLabel(GameStatus.Finished, "Terminée", Color.Black);
                }


            }
        }

        private void btn_Click_Player2(object sender)
        {
            Button btn = new Button();
            foreach (Control item in this.Controls)
            {
                if (item is Button && item.Tag.ToString() == sender.ToString())
                {
                    btn = (Button)item;
                }
            }


            btn.Text = "O";
            btn.Enabled = false;

            btn.BackColor = Color.Red;
            ChangeLabel(GameStatus.Playing, "A votre tour", Color.Green);

            count++;
            // FIN DE JEU
            if (GameFinished())
            {
                defaite();
                DisableButtons();
                ChangeLabel(GameStatus.Finished, "Terminée", Color.Black);
            }
            else if (count == 9)
            {
                egalite();
                DisableButtons();
                ChangeLabel(GameStatus.Finished, "Terminée", Color.Black);
            }

        }

        private void victoire()
        {
            MessageBox.Show("Vous avez gagné !", "Fin");
            labelEtatPartie.Text = "Victoire !";
        }

        private void defaite()
        {
            MessageBox.Show("Vous avez perdu !", "Fin");
            labelEtatPartie.Text = "Defaite !";
        }

        private void egalite()
        {
            MessageBox.Show("Egalité !", "Fin");
            labelEtatPartie.Text = "Egalité !";
        }

        private void quitter()
        {
            MessageBox.Show("L'adversaire a quitté !", "Fin");
        }

        private void erreurServeur()
        {
            MessageBox.Show("Erreur serveur !", "Erreur");
        }






    }
}
