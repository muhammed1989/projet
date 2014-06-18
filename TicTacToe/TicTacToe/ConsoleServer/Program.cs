using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleServer
{
    class Program
    {
        

        private static void threadReadStm(TcpClient cl1, TcpClient cl2)
        {
            try
            {
                NetworkStream stm1 = cl1.GetStream();
                NetworkStream stm2 = cl2.GetStream();

                byte[] rcvBytesClick = new byte[2];

                while (stm1.Read(rcvBytesClick, 0, rcvBytesClick.Length) != 0)
                {
                    Console.WriteLine("{0} a cliqué --> {1}", cl1.Client.RemoteEndPoint, Thread.CurrentThread.Name);
                    stm2.Write(rcvBytesClick, 0, rcvBytesClick.Length);
                    stm2.Flush();

                    if (rcvBytesClick[0] == 2)  //si l'un des clients a clické sur Rejouer
                    {
                        stm1.Write(rcvBytesClick, 0, rcvBytesClick.Length);  //pour qu'il initialise a travers son threadRead
                        byte[] tabBytes = new byte[2]; // car 8*32 = 256 bits = 16*16
                       
                        byte[] sndBytesTour = new byte[] { 255 };
                        stm1.Write(sndBytesTour, 0, sndBytesTour.Length);
                        stm1.Flush();

                        byte[] sndBytesAtt = new byte[] { 0 };
                        stm2.Write(sndBytesAtt, 0, sndBytesAtt.Length);
                        stm2.Flush();
                        Console.WriteLine("Nouvelle partie");
                    }
                }
                stm1.Close();
                cl1.Close();
                Console.WriteLine("{0} terminé", Thread.CurrentThread.Name);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
                Console.ReadLine();
            }

        }


        static void Main(string[] args)
        {
            try
            {
                //IPAddress ipAd = IPAddress.Parse("127.0.0.1"); //use local m/c IP address, and use the same in the client
                ///* Initializes the Listener */
                TcpListener myList = new TcpListener(IPAddress.Any, 8003);
                //TcpListener myList = new TcpListener(ipAd, 8003);
                /* Start Listeneting at the specified port */
                myList.Start();
                Console.WriteLine("The server is running at port 8003...");
                Console.WriteLine("The local End point is :" + myList.LocalEndpoint);
                Console.WriteLine("Waiting for a connection.....");
                //Socket s = myList.AcceptSocket();
                while (true)
                {
                    try
                    {
                        TcpClient cl1 = myList.AcceptTcpClient();
                        Console.WriteLine("First client connected.....");
                        NetworkStream stm1 = cl1.GetStream();
                        byte[] rcvBytesPseudoJ1 = new byte[100];
                        stm1.Read(rcvBytesPseudoJ1, 0, rcvBytesPseudoJ1.Length);

                        TcpClient cl2 = myList.AcceptTcpClient();
                        Console.WriteLine("Second client connected.....");
                        NetworkStream stm2 = cl2.GetStream();
                        byte[] rcvBytesPseudoJ2 = new byte[100];
                        stm2.Read(rcvBytesPseudoJ2, 0, rcvBytesPseudoJ2.Length);

                        stm2.Write(rcvBytesPseudoJ1, 0, rcvBytesPseudoJ1.Length);
                        stm1.Write(rcvBytesPseudoJ2, 0, rcvBytesPseudoJ2.Length);


                        byte[] sndBytesTour = new byte[] { 255 };
                        stm1.Write(sndBytesTour, 0, sndBytesTour.Length);
                        Console.WriteLine("C'est au tour du 1e client");
                        stm1.Flush();


                        byte[] sndBytesAtt = new byte[] { 0 };
                        stm2.Write(sndBytesAtt, 0, sndBytesAtt.Length);
                        Console.WriteLine("Le 2e client est en attente");
                        stm2.Flush();
                        /*
                         * un thread ne peut avoir qu'un seul paramètre au maximum
                         * pour plus de paramètres, on peut utiliser une méthode anonyme
                         * une méthode anonyme permet de passer un bloc de code en paramètre d'un délégué
                         * il n'est plus nécessaire d'instancier le délégué avec sa méthode nommée
                         */
                        Thread t1 = new Thread(delegate() { threadReadStm(cl1, cl2); });
                        t1.Name = "J1 to J2";
                        t1.Start();
                        Thread t2 = new Thread(delegate() { threadReadStm(cl2, cl1); });
                        t2.Name = "J2 to J1";
                        t2.Start();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error..... " + e.StackTrace);
                    }
                    //myList.Stop();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
                Console.ReadLine();
            }
        }
    }
}
