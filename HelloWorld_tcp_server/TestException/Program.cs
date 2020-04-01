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
    class TestException
    {

        static void Main(string[] args)
        {
            int res = MakeException(30, 0);

            Console.WriteLine("결과는 {0}",res);
            Console.ReadLine();
        }
        public static int MakeException(int a, int b)
        {
            try
            {
                return a / b;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                Console.WriteLine(ex);
                Console.WriteLine(ex.StackTrace);//스택 경로를 추적
                return -1;
            }
        }
    }
}