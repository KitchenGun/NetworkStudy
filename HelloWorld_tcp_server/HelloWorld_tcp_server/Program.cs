using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//네트워크 관련
using System.Net;
using System.Net.Sockets;

namespace HelloWorld_tcp_server
{
    class Program
    {
        static void Main(string[] args)
        {
            string strSend;
            Encoding ASCII = Encoding.UTF8;

            try
            {
                TcpListener tcpl = new TcpListener(1004);
                tcpl.Start();

                Console.WriteLine("연결할 클라이언트를 대기중입니다.");

                while (true)
                {//접속했으면 해당 코드 실행
                    Socket s = tcpl.AcceptSocket();
                    strSend = "Hello";
                    byte[] byteHelloLine = ASCII.GetBytes(strSend);

                    s.Send(byteHelloLine, byteHelloLine.Length, 0);
                    s.Close();
                    Console.WriteLine("{0}보냄", strSend);
                }
            }
            catch (SocketException socketError)
            {//접속이 안되면 해당코드 실행
                if (socketError.ErrorCode == 10048)
                {
                    Console.WriteLine("이포트는 다른 프로그램에서 s사용중입니다.");
                }
            }
        }
    }
}
