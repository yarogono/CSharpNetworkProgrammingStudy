using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WinFormsTcpChatClient
{
    // 클라이언트의 txt_Chat(텍스트박스)에 글을 쓰기 위한 델리게이트
    // 실제 글을 쓰는 것은 Form1 클래스의 스레드가 아닌 다른 스레드인 ChatHandler의 스레드 이기에
    // (만약 컨트롤을 만든 스레드가 아닌 다른 스레드에서 텍스트 박스에 글을 쓴다면 에러 발생)
    // ChatHandler의 스레드에서 이 델리게이트를 호출하여 서버에서 넘어오는 메시지를 쓴다.
    delegate void SetTextDelegate(string s);

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private TcpClient tcpClient = null;
        private NetworkStream ntwStream = null;

        // 서버와 채팅을 실행
        ChatHandler chatHandler = new ChatHandler();


        // 입장 버튼 클릭
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text.Equals("입장"))
            {
                try
                {
                    tcpClient = new TcpClient();
                    tcpClient.Connect(IPAddress.Parse("127.0.0.1"), 2023);
                    ntwStream = tcpClient.GetStream();

                    chatHandler.Setup(this, ntwStream, txtChatMsg);
                    Thread chatThread = new Thread(new ThreadStart(chatHandler.ChatProcess));
                    chatThread.Start();

                    Message_Snd("<" + txtName.Text + "> 님께서 접속 하셨습니다.", true);
                    btnConnect.Text = "나가기";
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Server 오류발생 또는 Start 되지 않았거나\n\n" + Ex.Message, "Client");
                }
            }
            else
            {
                Message_Snd("<" + txtName.Text + "> 님께서 접속해제 하셨습니다.", false);
                btnConnect.Text = "입장";
                chatHandler.ChatClose();
                ntwStream.Close();
                tcpClient.Close();
            }
        }

        private void Message_Snd(string lstMessage, bool Msg)
        {
            try
            {
                // 보낼 데이터를 읽어 Default 형식의 바이트 스트림으로 변환 해서 전송
                string dataToSend = lstMessage + "\r\n";
                byte[] data = Encoding.Default.GetBytes(dataToSend);
                ntwStream.Write(data, 0, data.Length);
            }
            catch (Exception Ex)
            {
                if (Msg == true)
                {
                    MessageBox.Show("서버가 Start 되지 않았거나 \n\n" + Ex.Message, "Client");
                    btnConnect.Text = "입장";
                    chatHandler.ChatClose();
                    ntwStream.Close();
                    tcpClient.Close();
                }
            }
        }

        // 다른 스레드인 ChatHandler의 쓰레드에서 호출하는 함수로
        // 델리게이트를 통해 채팅 문자열을 텍스트박스에 씀
        public void SetText(string text)
        {
            if (this.txtChatMsg.InvokeRequired)
            {
                SetTextDelegate d = new SetTextDelegate(SetText);
                this.Invoke(d, new Object[] { text });
            }
            else
            {
                this.txtChatMsg.AppendText(text);
            }
        }

        private void txtMsg_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Enter 키면
            if (e.KeyChar == 13)
            {
                if (btnConnect.Text.Equals("나가기"))
                {
                    Message_Snd("<" + txtName.Text + "> " + txtMsg.Text, true);
                }

                txtMsg.Text = "";
                e.Handled = true; // 이벤트 처리 중지, KeyUp or Click 등
            }
        }
    }
}