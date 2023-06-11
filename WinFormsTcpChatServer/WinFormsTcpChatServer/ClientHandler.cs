using System.Net.Sockets;
using System.Text;
using WinFormsTcpChatServer;

namespace ChatServerWinForms
{
    public class ClientHandler
    {
        private TextBox txtChatMsg;
        private Socket socketClient;
        private NetworkStream netStream;
        private StreamReader streamReader;
        private Form1 form1;

        public void Setup(Form1 form1, Socket socketClient, TextBox txtChatMsg)
        {
            this.txtChatMsg = txtChatMsg;   // 채팅 메시지 출력을 위한 TextBox;
            this.socketClient = socketClient;   // 클라이언트 접속 소켓, 이를 통해 스트림을 만들어 채팅한다.
            this.netStream = new NetworkStream(socketClient);
            Form1.clientSocketArray.Add(socketClient);  // 클라이언트 접속 소켓을 List에 담음
            this.streamReader = new StreamReader(netStream);
            this.form1 = form1;
        }

        public void Chat_Process()
        {
            while (true) 
            {
                try
                {
                    // 문자열을 받음
                    string lstMessage = streamReader.ReadLine();
                    if (lstMessage != null && lstMessage != "")
                    {
                        form1.SetText(lstMessage + "\r\n");
                        byte[] byteSand_Data = Encoding.Default.GetBytes(lstMessage + "\r\n");
                        lock (Form1.clientSocketArray)
                        {
                            foreach (Socket socket in Form1.clientSocketArray)
                            {
                                NetworkStream stream = new NetworkStream(socket);
                                stream.Write(byteSand_Data, 0, byteSand_Data.Length); 
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Form1.clientSocketArray.Remove(socketClient);
                    break;
                }
            }
        }
    }
}