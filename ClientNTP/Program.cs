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
            UInt32 serveurRetour = 0;
            byte[] trame = new byte[48];
            trame[0] = 0x23; // héxadécimal
            Socket sock = null;

            try
            {
                // Création de la socket
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                // Connexion au serveur (PAS de connect)
                IPEndPoint serveur = new IPEndPoint(Dns.GetHostEntry("ntp.u-picardie.fr").AddressList[0], 123);
                EndPoint serveurRemote = (EndPoint)serveur;

                Console.WriteLine("Connecté à : " + serveur.Address);
                //Envoie des données
                sock.SendTo(trame, serveur);
                sock.ReceiveFrom(trame, ref serveurRemote);
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

            serveurRetour = BitConverter.ToUInt32(trame, 40);

            serveurRetour = ReverseBytes(serveurRetour);

            DateTime start = new DateTime(1900, 1, 1);
            start = start.AddSeconds(serveurRetour);
            start = start.ToLocalTime();

            Console.WriteLine("Il est : " + start.ToString());
            Console.ReadKey();
        }
    }
}
