using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace Tetrao
{
    class Connection
    {
        private TcpClient client; //This is the Client
        private string ip; //This is the Ip for the client
        //===========================Events=============================
        public delegate void Disconnected(Connection client);
        public event Disconnected DisconnectedEvent;
        public delegate void Received(Connection client, string Message);
        public event Received ReceivedEvent;
        //===============================================================
        public Connection(TcpClient client)
        {
            this.client = client; //Sets the client with the new TcpClient.
            ip = client.Client.RemoteEndPoint.ToString().Remove(client.Client.RemoteEndPoint.ToString().LastIndexOf(':'));
            client.GetStream().BeginRead(new byte[] { 0 }, 0, 0, Read, null); //Begins reading!
        }
        void Read(IAsyncResult ar)
        {
            try
            {
                StreamReader reader = new StreamReader(client.GetStream()); //Creates StreamReader
                string msg = reader.ReadLine(); //Reads a line and holds it in msg.
                if (msg == "") //If the message sent is nothing, meaning that IF it is not connected
                {
                    DisconnectedEvent(this); //Raises the disconnected event.
                    return; //if you know decent coding, this exits the function
                }
                ReceivedEvent(this, msg);
                client.GetStream().BeginRead(new byte[] { 0 }, 0, 0, Read, null); //begin to read
            }
            catch
            {
                DisconnectedEvent(this);
            }
        }
        public void Send(string Message)
        {//I hope you know what this does lol
            try
            {
                StreamWriter writer = new StreamWriter(client.GetStream());
                writer.WriteLine(Message);
                writer.Flush();
                //k if you really dont know, this starts new stream writer, gets the stream, writes out whatever messeage you make, then sends it (Flush)
            }
            catch
            {
                //just some exception handling
            }
        }
        public string IPAddress
        {
            get
            {
                return ip; //Returns the ip.
            }
        }
    }
}
