/*
 *  Author: Avinash
 *  Date: August 30th, 2014 
 *  Desc: Code for 'Control Panel' for bots
 *  Version: 1.0
 */
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
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace Tetrao
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        #region Variables + Function Pointers
        [DllImport("User32", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern IntPtr SetParent(IntPtr oldchildhandle, IntPtr newhandle);

        int ActiveBots = 0;
        public TcpListener listener;
        public Thread thread;
        bool ConsoleEnabled = false;
        bool AllAccounts = true;
        #endregion

        #region Form Events
        private void Main_Load(object sender, EventArgs e)
        {
            listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 102);
            thread = new Thread(Listen); //Thread that server will run on
            thread.Start();
            Coordinates.Start(); //Timer to constantly grab coordinates of mouse
        }
        private void btnLoginAccount_Click(object sender, EventArgs e)
        {
            NewClient();
        }
        private void btnClick_Click(object sender, EventArgs e)
        {
            if (AllAccounts)
            {
                foreach (ListViewItem i in CloneList.Items)
                {
                    Connection client = (Connection)i.Tag;
                    client.Send("CLICK|" + txtX.Text + "|" + txtY.Text);
                }
            }
            else
            {
                try
                {
                    foreach (ListViewItem item in CloneList.SelectedItems)
                    {
                        Connection client = (Connection)item.Tag;
                        client.Send("CLICK|" + txtX.Text + "|" + txtY.Text);
                    }
                }
                catch
                { MessageBox.Show(" Error Occured: Make sure to select a bot, or that 'All Accounts' is checked"); }
            }
        }
        private void btnLoadRoom_Click(object sender, EventArgs e)
        {
        }
        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            bool Shout = false;
            if (chkShout.Checked)
                Shout = true;
            if (AllAccounts)
            {
                foreach (ListViewItem i in CloneList.Items)
                {
                    Connection client = (Connection)i.Tag;
                    client.Send("ATyper|" + Shout.ToString() + "|" + txtMessage.Text);
                }
            }
            else
            {
                try
                {
                    foreach (ListViewItem item in CloneList.SelectedItems)
                    {
                        Connection client = (Connection)item.Tag;
                        client.Send("ATyper|" + Shout.ToString() + "|" + txtMessage.Text);
                    }
                }
                catch
                { MessageBox.Show("Select a bot, or check off 'All Accounts'"); }
            }
        }

        private void CloneList_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in CloneList.SelectedItems)
            {
                Connection account = (Connection)item.Tag;
                account.Send("FOCUS|");
            }
        }

        private void Coordinates_Tick(object sender, EventArgs e)
        {
            toolStripCoords.Text = string.Format("X: {0} Y:{1}", MDI_client.PointToClient(Cursor.Position).X, MDI_client.PointToClient(Cursor.Position).Y);
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("You sure broh?", "~ Closing Tetrao ~", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }
            foreach (ListViewItem i in CloneList.Items)
            {
                Connection client = (Connection)i.Tag;
                client.Send("DISCONNECT|");
            }
            Process.GetCurrentProcess().Kill(); //Kills processes so that thread that server is running on exits, Application.Exit() will not work
        }
        private void chkEnableConsole_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnableConsole.Checked) { ConsoleEnabled = true; }
            else { ConsoleEnabled = false; }
        }

        private void chkAllAccounts_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllAccounts.Checked) { AllAccounts = true; }
            else { AllAccounts = false; }
        }

        private void chkProxyConnect_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProxyConnect.Checked) { EnableProxy(); }
            else { DisableProxy(); }
        }
        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.G)       // Ctrl-G
            {
                txtX.Text = MDI_client.PointToClient(Cursor.Position).X.ToString();
                txtY.Text = MDI_client.PointToClient(Cursor.Position).Y.ToString();
            }
        }
        #endregion

        #region Misc. Functions
        //TODO: Implement Proxy Functions
        private void EnableProxy() { }
        private void DisableProxy() { }
        private void AddToConsole(RichTextBox rtb,string text, Color color, Font font, bool AddNewLine = true)
        {
            if (AddNewLine)
            {
                text += Environment.NewLine;
            }

            rtb.SelectionStart = rtb.TextLength;
            rtb.SelectionLength = 0;

            rtb.SelectionColor = color;
            rtb.SelectionFont = font;
            rtb.AppendText(text);
            rtb.SelectionColor = rtb.ForeColor;
        }
        #endregion

        #region Create Bot Process
        private void NewClient()
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "Tetrao - Client";
                process.StartInfo.Arguments = string.Format("{0} {1} {2}", txtUser.Text, txtPass.Text, Hotel.Text);
                process.Start();
                WaitForHandle(process);
            }
            catch
            {
                MessageBox.Show("Could not find Tetrao - Client");
            }
        }
        private void WaitForHandle(Process client)
        {
            while (client.MainWindowHandle == (IntPtr)0)
            {
                Thread.Sleep(1000);
            }
            Thread.Sleep(100);
            SetParent(client.MainWindowHandle, MDI_client.Handle);
            return;
        }
        #endregion

        #region SocketWorks
        public void Listen()
        {
            listener.Start();
            while (true)
            {
                Connection clientConnection = new Connection(listener.AcceptTcpClient());
                clientConnection.DisconnectedEvent += new Connection.Disconnected(clientConnection_DisconnectedEvent);
                clientConnection.ReceivedEvent += new Connection.Received(clientConnection_ReceivedEvent);
            }
        }
        private void clientConnection_ReceivedEvent(Connection client, String Message)
        {
            string[] cut = Message.Split('|');
            switch (cut[0])
            {
                case "CONNECTED":
                    Invoke(new _AddClient(AddClient), client, cut[1], cut[2]);
                    break;
                case "STATUS":
                    MessageBox.Show("Status of client : " + cut[1]);
                    break;
                case "DISCONNECT":
                    Invoke(new _RemoveClient(RemoveClient), client, cut[1]);
                    break;
                case "COORDS":
                    break;
            }
        }
        void clientConnection_DisconnectedEvent(Connection client)
        {
        }
        delegate void _AddClient(Connection client, string email, string username);
        void AddClient(Connection client, string email, string username)
        {
            client.Send("RELOCATE|" + MDI_client.Location.X.ToString() + "|" + MDI_client.Location.Y.ToString() + "|");
            ListViewItem item = new ListViewItem();
            item.Text = email;
            item.SubItems.Add(username);
            item.Tag = client;
            CloneList.Items.Add(item);
            ActiveBots++;
            tlStripActiveBots.Text = "Active Bots: " + ActiveBots.ToString() + "|";
            client.Send("RESIZE|");
        }
        delegate void _RemoveClient(Connection client, string username);
        void RemoveClient(Connection client, string username)
        {
            foreach (ListViewItem i in CloneList.Items)
                if ((Connection)i.Tag == client)
                {
                    i.Remove();
                    ActiveBots--;
                    tlStripActiveBots.Text = "ActiveBots : " + ActiveBots;
                    break;
                }
        }
        #endregion


    }
}
