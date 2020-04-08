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
        private static IPAddress GroupAddress = IPAddress.Parse("224.0.0.1");//인스턴스화
        private static int GroupPort = 1004;

        static void Main(string[] args)
        {
            try
            {
                UdpClient receive = new UdpClient(GroupPort);//그룹포트를 이용하여 인스턴스화
                receive.JoinMulticastGroup(GroupAddress);
                IPEndPoint groupEP = new IPEndPoint(GroupAddress, GroupPort);
                while(true)
                {
                    Thread.Sleep(2000);//대기시간
                    IPEndPoint endpoint = null;//엔트포인트 변수
                    byte[] data = receive.Receive(ref endpoint);//데이터 수신 바이트 저장
                    //ref는 파라미터 참조값을 전달함 
                    //ex)void FuncA(in int a)==void FuncA(const int &number) 
                    //ex)void FuncA(out int a)==void FuncA(int &number)//class는 참조타입이다
                    string strData = Encoding.UTF8.GetString(data);//데이터 바이트 문자열 변경후 화면에 출력
                    Console.WriteLine(strData);
                }
            }
            catch (Exception ex)
            {///에러내용출력
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
