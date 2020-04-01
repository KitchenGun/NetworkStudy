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
        //static void Main(string[] args)
        //{
        //    int res = MakeException(30, 3);
        //    Console.WriteLine(res);
        //    Console.ReadLine();
        //}
        public static int MakeException(int v1, int v2)
        {
            try
            {
                return v1 / v2;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return -1;
            }   
        }
}
}
