using ChatServerWinForms;
using System.Collections;
using System.Net;
using System.Net.Sockets;

namespace WinFormsTcpChatServer
{

    //������ txtChatMsg �ؽ�Ʈ�ڽ��� ���� �������� ��������Ʈ
    //���� ���� ���°��� Form1Ŭ������ UI�����尡 �ƴ� �ٸ� �������� ClientHandler�� ������ �̱⿡        
    //ClientHandler�� �����忡�� �� ��������Ʈ�� ȣ���Ͽ� �ؽ�Ʈ �ڽ��� ���� ����.
    //(���� ��Ʈ���� ���� ������ UI�����尡 �ƴ� �ٸ� �����忡�� �ؽ�Ʈ�ڽ��� ���� ���ٸ� �����߻�)
    delegate void SetTextDelegate(string s);

    public partial class Form1 : Form
    {
        public static ArrayList clientSocketArray = new ArrayList();
        private TcpListener chatServer = new TcpListener(IPAddress.Parse("127.0.0.1"), 2023);

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblMsg.Tag.ToString() == "Stop")
                {
                    chatServer.Start();

                    //��� �� �����鼭 Ŭ���̾�Ʈ�� ������ ��ٸ��� ������ ����
                    //�� �����尡 �����ϴ� �޼ҵ忡�� Ŭ���̾�Ʈ ������ �ް�
                    //������ Ŭ���̾�Ʈ ������ clientSocketArray�� ��� ���ο� �����带 �����
                    //���ӵ� Ŭ���̾�Ʈ �������� ä���� �Ѵ�.
                    Thread waitThread = new Thread(new ThreadStart(AcceptClient));
                    waitThread.Start();

                    lblMsg.Text = "Server ���۵�";
                    lblMsg.Tag = "Start";
                    btnStart.Text = "���� ����";
                }
                else
                {
                    chatServer.Stop();
                    foreach (Socket socket in clientSocketArray)
                    {
                        socket.Close();
                    }
                    clientSocketArray.Clear();
                    lblMsg.Text = "Server ������";
                    lblMsg.Tag = "Stop";
                    btnStart.Text = "���� ����";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("���� ���� ���� :" + ex.Message);
            }
        }

        private void AcceptClient()
        {
            Socket socketClient = null;
            while (true)
            {
                try
                {
                    //������ ��ٸ��ٰ� Ŭ���̾�Ʈ�� �����ϸ� AcceptSocket �޼��尡 ����Ǿ�
                    //Ŭ���̾�Ʈ�� ����� ������ ���� �޴´�.
                    socketClient = chatServer.AcceptSocket();

                    //Chatting�� �����ϴ� ClientHandler �ν��Ͻ�ȭ��Ű��
                    //������ Ŭ���̾�Ʈ ���� ������ �Ҵ�
                    ClientHandler clientHandler = new ClientHandler();
                    clientHandler.Setup(this, socketClient, this.txtChatMsg);

                    //Ŭ���̾�Ʈ�� ����ϸ鼭 ä���� �����ϴ� ������ ���� �� ����
                    Thread thd_ChatProcess = new Thread(new ThreadStart(clientHandler.Chat_Process));
                    thd_ChatProcess.Start();

                }
                catch (SocketException ex)
                {
                    Form1.clientSocketArray.Remove(socketClient);
                    break;
                }
            }
        }


        public void SetText(string text)
        {
            // t.InvokeRequired�� True�� ��ȯ�ϸ�
            // Invoke �޼ҵ� ȣ���� �ʿ�� �ϴ� ���°� �� ���� �����尡 UI �����尡 �ƴ�
            // �� �� Invoke�� ��Ű�� UI�����尡 ��������Ʈ�� ������ �޼ҵ带 �������ش�.
            // False�� ��ȣ���ϸ� UI�� �����尡 �����ϴ� ���� ��Ʈ�ѿ� ���� �����ص� ������ ���� ���´�.
            if (this.txtChatMsg.InvokeRequired)
            {
                SetTextDelegate d = new SetTextDelegate(SetText);   // ��������Ʈ ����
                this.Invoke(d, new object[] { text });  // ��������Ʈ�� ���� ���� ����
            }
            else
            {
                this.txtChatMsg.AppendText(text); // �ؽ�Ʈ �ڽ��� ���� ��
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            chatServer.Stop();
        }
    }
}