using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.Net.Sockets;

namespace ControllerGamer.Libraries.NetListener
{
    public static class NetListener
    {
        public static bool Running = false;
        private static int _port = 0;
        private static Socket siminputserver = null;
        private static Queue<string> instructions = new Queue<string>();

        public static void Start(int port)
        {
            Running = true;
            _port = port;

            Thread thread = new Thread(new ThreadStart(Bind));
            thread.IsBackground = true;
            thread.Start();

            InstructionConsumer.Start(instructions);
        }

        public static void Stop()
        {
            Running = false;
            InstructionConsumer.Stop();
            if (siminputserver != null) siminputserver.Close();
            siminputserver = null;
        }

        private static void Bind()
        {
            try
            {
                IPEndPoint ipep = new IPEndPoint(IPAddress.Any, _port);
                siminputserver = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                siminputserver.Bind(ipep);
                siminputserver.Listen(10);
                Logger.Log("Listening on 0.0.0.0:" + _port.ToString());
                while (Running)
                {
                    Socket client = siminputserver.Accept(); 
                    IPEndPoint clientep = (IPEndPoint)client.RemoteEndPoint;
                    Logger.Log("Listening from "+clientep.Address.ToString());
                    ParameterizedThreadStart pts = new ParameterizedThreadStart(ReceiveFrom);
                    Thread thread = new Thread(pts);
                    thread.IsBackground = true;
                    thread.Start(client);
                }
            }
            catch (Exception e)
            {
                Running = false;
                Logger.Log(e);
                Logger.Log(e.StackTrace);
            }
        }

        private static void ReceiveFrom(object _client)
        {
            Socket client = (Socket)_client;
            string fullstr = "";
            string brokenstr = "";
            int idx = 0;
            while (true)
            {
                try
                {
                    byte[] arrServerRecMsg = new byte[1024 * 1024];
                    int length = client.Receive(arrServerRecMsg);
                    fullstr += brokenstr;
                    fullstr += Encoding.UTF8.GetString(arrServerRecMsg, 0, length);
                    string tobedealt = fullstr.Substring(idx);
                    int splitpoint = tobedealt.LastIndexOf("\n");
                    if (splitpoint >= 0)
                    {
                        string parseable = tobedealt.Substring(0, splitpoint + 1);
                        if (splitpoint < tobedealt.Length - 1)
                            brokenstr = tobedealt.Substring(splitpoint + 1);
                        else
                            brokenstr = "";
                        foreach (string ins in parseable.Split(new string[2] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries))
                            instructions.Enqueue(ins);
                        idx += parseable.Length;
                    }

                }
                catch (Exception e)
                {
                    Logger.Log(e);
                    Logger.Log(e.StackTrace);
                    client.Close();
                    break;
                }
            }
        }
        
    }
}
