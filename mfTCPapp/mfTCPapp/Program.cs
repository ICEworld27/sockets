using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace mfTCPapp
{
    class Program
    {
        static public void Send(Socket soket, string msg)
        {
            int totalSent = 0;
            string data =msg;
            byte[] dataToSend = Encoding.ASCII.GetBytes(data);
            while (totalSent < dataToSend.Length)
            {
                int actuallySent = soket.Send(
                dataToSend,
                totalSent,
                dataToSend.Length - totalSent,
                SocketFlags.None
                );
                totalSent += actuallySent;
            }
        }
        static public string Recive(Socket sock)
        {
            byte[] buffer = new byte[128];
            sock.Receive(buffer);
            return Encoding.ASCII.GetString(buffer, 0, buffer.Length); 
        }
        static string Change(string data)
        {
            
            string newData = "";
            for (int i = data.Length - 1; i > -1; i--)
            {
                newData += data[i];
            }
            return newData;
        } 
        static void TaskHandler(Socket socket) // 3-я задача
        {
            string data = Recive(socket);
            while (data == null)
            {
                data = Recive(socket);
            }

            Console.WriteLine(Change(data));

            Send(socket, Change(data));
        }
    static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Socket server = new Socket(
                AddressFamily.InterNetwork, 
                SocketType.Stream, 
                ProtocolType.Tcp 
                );
            ThreadPool<Socket> pool = new ThreadPool<Socket>(5, TaskHandler);// 3-я задача
            pool.Start();
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint addr = new IPEndPoint(ip, 1337);
            server.Bind(addr);
            server.Listen();
            while (1 == 1)  //2-aя задача
            {
                
                Socket client = server.Accept();
                pool.AddTask(client); // 3-я задача
                /*
                IPEndPoint addrc = (IPEndPoint)client.RemoteEndPoint;
                Console.WriteLine(addr.Address.ToString());
                string data = Recive(client);
                //Console.WriteLine(data);
                while (data == null)
                {
                    data = Recive(client);
                    Console.WriteLine(data);
                }
                
                Console.WriteLine(Change(data));

                Send(client, Change(data));
                */
            }

        }
    }
}
