using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientNTP
{
    class Program
    {
        public static uint ReverseBytes(uint value)
        {
            return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 | (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;
        }

        static void Main(string[] args)
        {
            // Adresse du serveur à joindre
            IPAddress ip_addressServer = IPAddress.Parse("192.168.211.54");

            byte[] trame = new byte[48];
            byte[] 
            Socket sock = null;

            try
            {
                // Création de la socket
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                // Connexion au serveur (PAS de connect)
                IPEndPoint serveur = new IPEndPoint(ip_addressServer, 6000);
                EndPoint serveurRemote = (EndPoint)serveur;

                //Envoie des données
                sock.SendTo(b_userString, serveur);
                sock.ReceiveFrom(b_serverStringResponse, ref serveurRemote);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return;
            }
            finally
            {
                // Fermeture de la connexion
                if (sock != null)
                {
                    sock.Shutdown(SocketShutdown.Both);
                    sock.Close();
                }
            }

            serveurRetour = Encoding.UTF8.GetString(b_serverStringResponse);

            Console.WriteLine(serveurRetour);
            Console.ReadKey();
        }
    }
}
