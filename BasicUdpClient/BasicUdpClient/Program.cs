using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BasicUdpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // (1) UdpClient 객체 성성
            UdpClient udpClient = new UdpClient();
            string msg = "안녕하세요.";
            byte[] datagram = Encoding.UTF8.GetBytes(msg);

            // (2) 데이터 송신
            udpClient.Send(datagram, datagram.Length, "127.0.0.1", 9999);
            Console.WriteLine("[Send] 127.0.0.1:7777 로 {0} 바이트 전송", datagram.Length);

            // (3) 데이타 수신
            IPEndPoint epRemote = new IPEndPoint(IPAddress.Any, 0);
            byte[] bytes = udpClient.Receive(ref epRemote);
            Console.WriteLine("[Receive] {0} 로부터 {1} 바이트 수신", epRemote.ToString(), bytes.Length);

            // (4) UdpClient 객체 닫기
            udpClient.Close();
        }
    }
}