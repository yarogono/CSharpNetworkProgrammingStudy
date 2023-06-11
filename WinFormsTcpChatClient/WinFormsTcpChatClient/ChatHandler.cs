using System.Net.Sockets;

namespace WinFormsTcpChatClient
{
    public class ChatHandler
    {
        private TextBox txtChatMsg;
        private NetworkStream ntwStream;
        private StreamReader streamReader;
        private Form1 form1;

        public void Setup(Form1 form1, NetworkStream ntwStream, TextBox txtChatMsg)
        {
            this.txtChatMsg = txtChatMsg;
            this.ntwStream = ntwStream;
            this.form1 = form1;
            this.streamReader = new StreamReader(ntwStream);
        }

        public void ChatClose()
        {
            ntwStream.Close();
            streamReader.Close();
        }

        public void ChatProcess()
        {
            while (true)
            {
                try
                {
                    string lstMessage = streamReader.ReadLine();

                    if (lstMessage != null && lstMessage != "")
                    {
                        //SetText 메서드에서 델리게이트를 이용하여 서버에서 넘어오는 메시지를 쓴다.
                        form1.SetText(lstMessage + "\r\n");
                    }
                }
                catch (Exception Ex)
                {
                    break;
                }
            }
        }
    }
}