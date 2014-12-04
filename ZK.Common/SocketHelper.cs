
using System;
using System.Threading;								// Sleeping
using System.Net;									// Used to local machine info
using System.Net.Sockets;							// Socket namespace
using System.Collections;
using System.Text;							// Access to the Array list

namespace P2PGrid
{
    /// <summary>
    /// Main class from which all objects are created
    /// </summary>
    class P2PApp 
    {

        #region Fields
        //private IList clientList = new ArrayList();	// List of Client Connections
        private byte[] msgBuff = new byte[50];		// Receive data buffer
        private Socket clientSock;                  // Client Socket
   

        #endregion
        /// <summary>
        /// Close Client Socket
        /// </summary>
        public void CloseClient()
        {
            if (clientSock != null)
            {
                clientSock.Shutdown(SocketShutdown.Both);
            }          
        }

        public void CloseAll()
        {
            if (clientSock != null)
            {
                clientSock.Close();
            }  
        }

        #region P2P Server Operation

        /// <summary>
        /// Callback used when a client requests a connection. 
        /// Accpet the connection, adding it to our list and setup to 
        /// accept more connections.
        /// </summary>
        /// <param name="ar"></param>
        public void OnConnectRequest(IAsyncResult ar)
        {
            Socket listener = (Socket)ar.AsyncState;
            NewConnection(listener.EndAccept(ar));
            listener.BeginAccept(new AsyncCallback(OnConnectRequest), listener);
        }

        /// <summary>
        /// Add the given connection to our list of clients
        /// Note we have a new friend send a welcome to the new client
        /// Setup a callback to recieve data
        /// </summary>
        /// <param name="sockClient">Connection to keep</param>
        public void NewConnection(Socket clientSock)
        {
            // Program blocks on Accept() until a client connects.
            // SocketChatClient client = new SocketChatClient( listener.AcceptSocket() );
            // SocketChatClient client = new SocketChatClient(sockClient);
            // m_aryClients.Add(client);
            Console.WriteLine("Client {0}, joined", clientSock.RemoteEndPoint);

            String connectedMsg = "Connected to " + clientSock.LocalEndPoint + "success \n\r";
            // Convert to byte array and send.
            Byte[] byteMsg = System.Text.Encoding.ASCII.GetBytes(connectedMsg.ToCharArray());
            clientSock.Send(byteMsg, byteMsg.Length, 0);

            SetupRecieveCallback(clientSock);
        }
        #endregion

        #region P2P Client Operation
        /// <summary>
        /// Callback used when a server accept a connection. 
        /// Setup to receive message
        /// </summary>
        /// <param name="ar"></param>
        public void OnConnect(IAsyncResult ar)
        {
            // Socket was the passed in object
            Socket sock = (Socket)ar.AsyncState;

            // Check if we were sucessfull
            try
            {
                //sock.EndConnect( ar );
                if (sock.Connected)
                    SetupRecieveCallback(sock);
                else
                    Console.WriteLine("Unable to connect to remote machine", "Connect Failed!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Unusual error during Connect!");
            }
        }

        /// <summary>
        /// Connect to the server, setup a callback to connect
        /// </summary>
        /// <param name="serverAdd">server ip address</param>
        /// <param name="port">port</param>
        public void Connect(string serverAdd, int port)
        {
            try
            {
                // Create the socket object
                clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Define the Server address and port
                IPEndPoint epServer = new IPEndPoint(IPAddress.Parse(serverAdd), port);

                // Connect to server non-Blocking method
                clientSock.Blocking = false;
                
                // Setup a callback to be notified of connection success 
                clientSock.BeginConnect(epServer, new AsyncCallback(OnConnect), clientSock);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Server Connect failed!");
                Console.WriteLine(ex.Message);
            }
        }


        #endregion


        #region P2P Server&Client Operation

        /// <summary>
        /// Callback used when receive data., both for server or client
        /// Note: If not data was recieved the connection has probably died.
        /// </summary>
        /// <param name="ar"></param>
        public void OnRecievedData(IAsyncResult ar)
        {
            Socket sock = (Socket)ar.AsyncState;
            // Check if we got any data
            try
            {
                int nBytesRec = sock.EndReceive(ar);
                if (nBytesRec > 0)
                {
                    // Get the received message 
                    string sRecieved = Encoding.ASCII.GetString(msgBuff, 0, nBytesRec);
                  
                    SetupRecieveCallback(sock);
                }
                else
                {
                    // If no data was recieved then the connection is probably dead
                    Console.WriteLine("disconnect from server {0}", sock.RemoteEndPoint);
                    sock.Shutdown(SocketShutdown.Both);
                    sock.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Unusual error druing Recieve!");
            }
        }
        /// <summary>
        /// Send message to client scoket
        /// </summary>
        /// <param name="msg">message</param>
        public void Send(string msg)
        {
            Byte[] byteMsg = System.Text.Encoding.ASCII.GetBytes(msg.ToCharArray());
            clientSock.Send(byteMsg);
        }
        /// <summary>
        /// Setup the callback for recieved data and loss of conneciton
        /// </summary>
        /// <param name="app">socket used to receive</param>
        public void SetupRecieveCallback(Socket sock)
        {
            try
            {
                AsyncCallback recieveData = new AsyncCallback(OnRecievedData);
                sock.BeginReceive(msgBuff, 0, msgBuff.Length, SocketFlags.None, recieveData, sock);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Recieve callback setup failed! {0}", ex.Message);
            }
        }  
 
        #endregion
    }

}