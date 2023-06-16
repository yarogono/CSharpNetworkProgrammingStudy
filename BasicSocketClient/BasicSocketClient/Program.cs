// 참고한 깃허브 링크 : https://github.com/jacking75/edu_com2us_CSharpNetworkProgramming/blob/master/BasicSocketClient/BasicSocketClient/Program.cs
// 참고한 웹 사이트 : http://www.csharpstudy.com/net/article/9-Socket-%ed%81%b4%eb%9d%bc%ec%9d%b4%ec%96%b8%ed%8a%b8

using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BasicSocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Baisc TCP Client");
            StartClient();
        }

        private static void StartClient()
        {
            // (1) 소켓 객체 생성 (TCP 소켓)
            Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                // (2) 서버에 연결
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7000);
                sender.Connect(remoteEP);

                string cmd = string.Empty;
                byte[] receiverBuff = new byte[8192];

                Console.WriteLine("Connected... Enter Quit to exit");

                // Q를 누를 때까지 계속 Echo 실행
                while (true)
                {
                    cmd = Console.ReadLine();
                    byte[] buff = Encoding.UTF8.GetBytes(cmd);

                    // (3) 서버에 데이터 전송
                    int bytesSent = sender.Send(buff);

                    // (4) 서버에서 데이터 수신
                    int bytesRec = sender.Receive(receiverBuff);

                    if (cmd.Equals("Quit"))
                    {
                        Console.WriteLine("Client Disconnected");
                        break;
                    }

                    Console.WriteLine("Echoed test = {0}", Encoding.UTF8.GetString(receiverBuff, 0, bytesRec));
                }

                // (5) 소켓 닫기
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected exception : {0}", ex.ToString());
            }
        }
    }
}