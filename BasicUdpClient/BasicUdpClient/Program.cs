using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BasicUdpClient
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("UDP 서버로 전송할 문자를 입력하세요.");
                Console.WriteLine("종료를 하기 위해서는 exit 을 입력해주세요.");
                string input = Console.ReadLine();

                if (input.Equals("exit")) return;

                UdpSendToServer(input);
            }



        }

        private static void UdpSendToServer(string input)
        {
            // (1) UdpClient 객체 성성
            UdpClient udpClient = new UdpClient();
            byte[] datagram = Encoding.UTF8.GetBytes(input);

            // (2) 데이터 송신
            udpClient.Send(datagram, datagram.Length, "127.0.0.1", 9999);
            Console.WriteLine("[Send] 127.0.0.1:9999 로 {0} 바이트 전송", datagram.Length);

            // (3) 데이타 수신
            IPEndPoint epRemote = new IPEndPoint(IPAddress.Any, 0);
            byte[] bytes = udpClient.Receive(ref epRemote);
            Console.WriteLine("[Receive] {0} 로부터 {1} 바이트 수신", epRemote.ToString(), bytes.Length);

            // (4) UdpClient 객체 닫기
            udpClient.Close();
        }
    }
}