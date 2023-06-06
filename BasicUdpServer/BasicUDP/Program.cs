using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BasicUdpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Basic UDP Server!");
            UdpServer("127.0.0.1", 9999);
        }

        static void UdpServer(string server, int port)
        { 
            UdpClient udpServer = new UdpClient(port);

            try
            {
                while (true)
                {
                    Console.WriteLine("수신 대기 . . .");

                    IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    Byte[] receiveBytes = udpServer.Receive(ref remoteIpEndPoint);

                    string returnData = Encoding.UTF8.GetString(receiveBytes);

                    // 수신 데이터와 보낸 곳 정보 표시
                    Console.WriteLine("수신: {0}Bytes {1}", receiveBytes.Length, returnData);
                    Console.WriteLine("보낸 곳 IP=" 
                                            + remoteIpEndPoint.Address.ToString
                                            + " 포트 번호= "
                                            + remoteIpEndPoint.Port.ToString());

                    // 수신 데이터를 대문자로 변환하여 보낸다.
                    returnData = returnData.ToUpper();

                    byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(returnData);


                    // 보낸 곳에 답변
                    Console.WriteLine("답변 데이터: {0}Bytes {1}", sendBytes.Length, returnData);

                    udpServer.Send(sendBytes, sendBytes.Length, remoteIpEndPoint);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            udpServer.Close();

            Console.Read();
        }
    }
}