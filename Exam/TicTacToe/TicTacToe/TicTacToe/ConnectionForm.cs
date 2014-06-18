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
    public partial class ConnectionForm : Form
    {
        private static String adresseIP;
        private int port;
        TcpClient client = new TcpClient();
        NetworkStream stream;
        Thread tWaitingPlayer2;
        Thread t1;
        bool stopWaitingPlayer2 = true;
        public bool waitingPlayer2 = false;
        Object lockObject = new Object();
        delegate void DelHide();
        delegate void DelDispose();
        delegate void DelSetStatusStrip(String message);
        delegate void DelMessageBox();

        public ConnectionForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxPlayer.Text != "")
            {
                button1.Enabled = false;
                textBoxPlayer.Enabled = false;
                tWaitingPlayer2 = new Thread(threadWaitingPlayer2);
                t1 = new Thread(threadMessage);
                tWaitingPlayer2.IsBackground = true;
                t1.IsBackground = true;
                tWaitingPlayer2.Start();
                stopWaitingPlayer2 = false;
                t1.Start();
                Cursor = Cursors.WaitCursor;
            }
            else
            {
                MessageBox.Show("Entrez votre nom!", "Info manquant");
            }
        }


        private void clientConnect(string playerName)
        {
            port = 8001;
            adresseIP = "127.0.0.1";
            IPAddress serverAddress = IPAddress.Parse(adresseIP);
            client.Connect(serverAddress, port);
        }

        private void threadWaitingPlayer2()
        {
            try
            {
                GameForm game = new GameForm(textBoxPlayer.Text);
                stopWaitingPlayer2 = true;
                this.Invoke(new DelHide(Hide));
                game.ShowDialog();  //Le démarrage d'une deuxième boucle de messages sur un seul thread n'est pas une opération valide. Utilisez Form.ShowDialog à la place.
                game.Dispose();  //car sinon n'a pas l'occasion de faire stream.Write
                this.Invoke(new DelDispose(Dispose));
            }
            catch
            {
                this.Invoke(new DelMessageBox(ErrorServer));
                stopWaitingPlayer2 = true;
                this.Invoke(new DelDispose(Dispose));
            } //car si on ouvre un client sans serveur, erreur à client.Connect
        }

        private void ErrorServer()
        {
            MessageBox.Show("Une erreur est survenue.", "Erreur");
        }

        private void SetLabelMessage(string message)
        {
            labelMessage.Text = message;
        }

        private void threadMessage()
        {
            while (!stopWaitingPlayer2)
            {
                lock (lockObject)
                {
                    try
                    {
                        this.Invoke(new DelSetStatusStrip(SetLabelMessage), "En attente d'un adversaire.");
                    }
                    catch { }
                    Thread.Sleep(500);
                }
            }
        }

    }
}
