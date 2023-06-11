using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WinFormsTcpChatClient
{
    // Ŭ���̾�Ʈ�� txt_Chat(�ؽ�Ʈ�ڽ�)�� ���� ���� ���� ��������Ʈ
    // ���� ���� ���� ���� Form1 Ŭ������ �����尡 �ƴ� �ٸ� �������� ChatHandler�� ������ �̱⿡
    // (���� ��Ʈ���� ���� �����尡 �ƴ� �ٸ� �����忡�� �ؽ�Ʈ �ڽ��� ���� ���ٸ� ���� �߻�)
    // ChatHandler�� �����忡�� �� ��������Ʈ�� ȣ���Ͽ� �������� �Ѿ���� �޽����� ����.
    delegate void SetTextDelegate(string s);

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private TcpClient tcpClient = null;
        private NetworkStream ntwStream = null;

        // ������ ä���� ����
        ChatHandler chatHandler = new ChatHandler();


        // ���� ��ư Ŭ��
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text.Equals("����"))
            {
                try
                {
                    tcpClient = new TcpClient();
                    tcpClient.Connect(IPAddress.Parse("127.0.0.1"), 2023);
                    ntwStream = tcpClient.GetStream();

                    chatHandler.Setup(this, ntwStream, txtChatMsg);
                    Thread chatThread = new Thread(new ThreadStart(chatHandler.ChatProcess));
                    chatThread.Start();

                    Message_Snd("<" + txtName.Text + "> �Բ��� ���� �ϼ̽��ϴ�.", true);
                    btnConnect.Text = "������";
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Server �����߻� �Ǵ� Start ���� �ʾҰų�\n\n" + Ex.Message, "Client");
                }
            }
            else
            {
                Message_Snd("<" + txtName.Text + "> �Բ��� �������� �ϼ̽��ϴ�.", false);
                btnConnect.Text = "����";
                chatHandler.ChatClose();
                ntwStream.Close();
                tcpClient.Close();
            }
        }

        private void Message_Snd(string lstMessage, bool Msg)
        {
            try
            {
                // ���� �����͸� �о� Default ������ ����Ʈ ��Ʈ������ ��ȯ �ؼ� ����
                string dataToSend = lstMessage + "\r\n";
                byte[] data = Encoding.Default.GetBytes(dataToSend);
                ntwStream.Write(data, 0, data.Length);
            }
            catch (Exception Ex)
            {
                if (Msg == true)
                {
                    MessageBox.Show("������ Start ���� �ʾҰų� \n\n" + Ex.Message, "Client");
                    btnConnect.Text = "����";
                    chatHandler.ChatClose();
                    ntwStream.Close();
                    tcpClient.Close();
                }
            }
        }

        // �ٸ� �������� ChatHandler�� �����忡�� ȣ���ϴ� �Լ���
        // ��������Ʈ�� ���� ä�� ���ڿ��� �ؽ�Ʈ�ڽ��� ��
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
            // Enter Ű��
            if (e.KeyChar == 13)
            {
                if (btnConnect.Text.Equals("������"))
                {
                    Message_Snd("<" + txtName.Text + "> " + txtMsg.Text, true);
                }

                txtMsg.Text = "";
                e.Handled = true; // �̺�Ʈ ó�� ����, KeyUp or Click ��
            }
        }
    }
}