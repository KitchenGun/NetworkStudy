using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//udp관련
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Hello_world_udp_client
{
    class Program
    {
        private static IPAddress GroupAddress = IPAddress.Parse("224.0.0.1");
        private static int GroupPort = 1004;

        static void Main(string[] args)
        {
            try
            {
                UdpClient receive = new UdpClient(GroupPort);
                receive.JoinMulticastGroup(GroupAddress);
                IPEndPoint groupEP = new IPEndPoint(GroupAddress, GroupPort);
                while(true)
                {
                    Thread.Sleep(2000);
                    IPEndPoint endpoint = null;
                    byte[] data = receive.Receive(ref endpoint);

                    string strData = Encoding.UTF8.GetString(data);
                    Console.WriteLine(strData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
