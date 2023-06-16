// 참고한 깃허브 링크 : https://github.com/jacking75/edu_com2us_CSharpNetworkProgramming/blob/master/BasicSocketServer/BasicSocketServer/Program.cs
// 참고한 웹 사이트 : http://www.csharpstudy.com/net/article/10-Socket-%ec%84%9c%eb%b2%84

using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BasicSocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Basic TCP Server");
            StartListening();
        }

        public static void StartListening()
        {
            // (1) 소켓 객체 생성 (TCP 소켓)
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                // (2) 포트에 바인드
                IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7000);
                listener.Bind(localEndPoint);

                // (3) 포트 Listening 시작
                listener.Listen(10);

                Console.WriteLine("Waiting for a connection...");

                // (4) 연결을 받아들여 새 소켓 생성
                Socket handler = listener.Accept();

                // Data buffer for incoming data.
                byte[] bytes = new byte[8192];

                Console.WriteLine("Client Connected...");

                while (true)   // 키 누르면 종료
                {

                    // (5) 소켓 수신
                    int bytesRec = handler.Receive(bytes);

                    string data = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    Console.WriteLine("Text received : {0}", data);

                    if (data.Equals("Quit"))
                    {
                        Console.WriteLine("Client Disconnected");
                        break;
                    }

                    byte[] msg = Encoding.ASCII.GetBytes(data);

                    // (6) 소켓 송신
                    handler.Send(bytes, 0, bytesRec, SocketFlags.None);  // echo
                }

                // (7) 소켓 닫기
                handler.Close();
                listener.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();
        }
    }
}