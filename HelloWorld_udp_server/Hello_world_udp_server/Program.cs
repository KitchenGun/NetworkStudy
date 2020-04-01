using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//udp관련
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Hello_world_udp_server
{
    class Program
    {
        private static IPAddress GroupAddress = IPAddress.Parse("224.0.0.1");
        private static int GroupPort = 1004;
        static void Main(string[] args)
        {
            try
            {
                string message = "hello";
                UdpClient sender = new UdpClient();
                sender.JoinMulticastGroup(GroupAddress);

                IPEndPoint groupEP = new IPEndPoint(GroupAddress, GroupPort);

                byte[] bytes = new byte[1024];

                int i = 0;
                while(i<100)
                {
                    Thread.Sleep(2000);
                    string msg = message + (++i);
                    Console.WriteLine(msg);

                    bytes = null;
                    bytes = Encoding.UTF8.GetBytes(msg);

                    sender.Send(bytes, bytes.Length, groupEP);
                    i++;

                }
                sender.Close();
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
        }
    }
}
