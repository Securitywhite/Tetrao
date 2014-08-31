using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetrao___Client
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        public string Email;
        public string Password;
        public string Hotel;
        public static bool ServerClosed = false;
        HSession HS;
        static TcpClient client = new TcpClient();
        static IPEndPoint point = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 102);
        private void Main_Load(object sender, EventArgs e)
        {
            Login();
        }

        #region Connecting
        private void Connect()
        {
            try
            {
                client.Connect(point);
                Send(string.Format("CONNECTED|{0}|{1}|", HS.Email, HS.PlayerName));
                client.GetStream().BeginRead(new byte[] { 0 }, 0, 0, Read, null);
            }
            catch
            {
                Thread.Sleep(1000);
                Cleanup();
                Connect();
                MessageBox.Show("Server is not online!");
            }
        }
        #region DataParsing
        void Read(IAsyncResult ar)
        {
            try
            {
                StreamReader reader = new StreamReader(client.GetStream());
                Parse(reader.ReadLine());
                client.GetStream().BeginRead(new byte[] {0} , 0, 0, Read, null);
            }
            catch
            {
                Thread.Sleep(3000);
                Cleanup();
                Connect();
                MessageBox.Show("Failed to read!");
            }
        }
        void Parse(string msg)
        {
            string[] cut = msg.Split('|');
            switch (cut[0])
            {
                case "MSGBOX":
                    MessageBox.Show(cut[1]);
                    break;
                case "DISCONNECT":
                    ServerClosed = true;
                    client.Close();
                    Process.GetCurrentProcess().Kill();
                    break;
                case "CLICK":
                    int x;
                    int y;
                    int.TryParse(cut[1], out x);
                    int.TryParse(cut[2], out y);
                    Methods.Click(x, y, mainBrowser);
                    break;
                case "ROOMRELOAD":
                    int roomid = 0;
                    int.TryParse(cut[1], out roomid);
                    HS.NavigateRoom(roomid);
                    break;
                case "ATyper":
                    string message;
                    if (cut[1].ToLower() == "true")
                    {
                        message = ":shout " + cut[2];
                    }
                    message = cut[2];
                    mainBrowser.Focus();
                    Methods.SpeechMethod(message, mainBrowser);
                    Methods.EnterMethod(mainBrowser.Handle);
                    break;
                case "RAID":
                    break;
                case "RESIZE":
                    if (this.WindowState != FormWindowState.Maximized)
                        this.WindowState = FormWindowState.Maximized;
                    else
                        this.WindowState = FormWindowState.Normal;
                    break;
                case "RELOCATE":
                    this.Location = new Point(int.Parse(cut[1]), int.Parse(cut[2]));
                    break;
                case "FOCUS":
                    this.BringToFront();
                    break;
            }
        }
        #endregion

        static void Cleanup()
        {
            try
            {
                client.Close();
            }
            catch
            {
            }
        }
        static void Send(string msg)
        {
            try
            {
                StreamWriter writer = new StreamWriter(client.GetStream());
                writer.WriteLine(msg);
                writer.Flush();
            }
            catch
            {
            }
        }
        static void SendStatus(string msg)
        {
            try
            {
                StreamWriter writer = new StreamWriter(client.GetStream());
                writer.WriteLine("STATUS|" + msg);
                writer.Flush();
            }
            catch
            {
            }
        }
        private void Login()
        {
            try
            {
                SKore.ClearCache();
                HS = new HSession(Email, Password, Hotel.ToHotel());
                HS.Login();
                if (!HS.isLoggedIn)
                {
                    MessageBox.Show("Failed to Login!");
                    Process.GetCurrentProcess().Kill();
                }
                mainBrowser.DocumentText = HS[HPages.Client];
                Connect();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ":" + ex.InnerException);
            }
        }
        #endregion

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Send("DISCONNECT|");
        }

    }
}
