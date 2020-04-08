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
                string message = "hello";//출력할 메세지
                UdpClient sender = new UdpClient();
                sender.JoinMulticastGroup(GroupAddress);

                IPEndPoint groupEP = new IPEndPoint(GroupAddress, GroupPort);

                byte[] bytes = new byte[1024];//버퍼

                int i = 0;
                while(i<100)//메세지 전송횟수
                {
                    Thread.Sleep(2000);//대기 시간
                    string msg = message + (++i);//카운트 증가
                    Console.WriteLine(msg);//출력

                    bytes = null;//바이트로 변환
                    bytes = Encoding.UTF8.GetBytes(msg);

                    sender.Send(bytes, bytes.Length, groupEP);//전송

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
