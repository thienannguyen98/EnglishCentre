using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace English2.Views.giaoVien
{
    public partial class gv_Chat : Form
    {
        private TcpClient client;
        public StreamReader STR;
        public StreamWriter STW; //Người gửi nó gửi cái gì lưu vô Writer vô -------> truyền qua ->>>>>>> Read ra
        public string Recieve;
        public string TextToSend;
        public string MyText;
        public gv_Chat()
        {
            InitializeComponent();
            label1.Text = fMain.username;

            IPAddress[] localIP = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress address in localIP)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    ServerIP.Text = address.ToString();

                }
            }
        }

        private void gv_Chat_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                TcpListener listener = new TcpListener(IPAddress.Any, int.Parse(ServerPort.Text));
                listener.Start();
                client = listener.AcceptTcpClient();
                STR = new StreamReader(client.GetStream());
                STW = new StreamWriter(client.GetStream());
                STW.AutoFlush = true;
                backgroundWorker1.RunWorkerAsync();
                backgroundWorker2.WorkerSupportsCancellation = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Start Server Failed: " + ex.Message.ToString());
            }

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            client = new TcpClient();
            IPEndPoint IpEnd = new IPEndPoint(IPAddress.Parse(ClientIP.Text), int.Parse(ClientPort.Text));
            client.Connect(IpEnd);
            try
            {
                ChatScreen.AppendText("Connect to Server");
                ChatScreen.AppendText(Environment.NewLine);
                STW = new StreamWriter(client.GetStream());
                STR = new StreamReader(client.GetStream());
                STW.AutoFlush = true;

                backgroundWorker1.RunWorkerAsync();
                backgroundWorker2.WorkerSupportsCancellation = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (client.Connected)
            {
                try
                {
                    Recieve = STR.ReadLine();
                    this.ChatScreen.Invoke(new MethodInvoker(delegate ()
                    {
                        ChatScreen.AppendText(Recieve);
                        ChatScreen.AppendText(Environment.NewLine);
                    }));
                    Recieve = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            if (client.Connected)
            {
                STW.WriteLine(TextToSend);
                this.ChatScreen.Invoke(new MethodInvoker(delegate ()
                {
                    ChatScreen.AppendText("Me: " + MyText);
                    ChatScreen.AppendText(Environment.NewLine);

                }));

            }
            else
            {
                MessageBox.Show("Sending Failed");

            }
            backgroundWorker2.CancelAsync();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (Message.Text != "")
            {
                TextToSend = label1.Text.ToString() + ": " + Message.Text;
                MyText = Message.Text;

                backgroundWorker2.RunWorkerAsync();
            }
            Message.Text = "";
        }
    }
}
