using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace HelloWorld_tcp_client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TcpClient tcpc = new TcpClient();
                byte[] read = new byte[32];
                //자기자신으로 지정
                tcpc.Connect("127.0.0.1", 1004);

                Stream s;
                s = tcpc.GetStream();
                int bytes = s.Read(read, 0, read.Length);

                string strReceive = Encoding.UTF8.GetString(read);
                Console.WriteLine("{0}바이트를 받음", bytes);
                Console.WriteLine(strReceive);

                tcpc.Close();

                Console.WriteLine("끝내려면 return키를 누르세여");
                Console.Read();

            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message);
                Console.ReadLine();
                return;
            }
        }
    }
}
