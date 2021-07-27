using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace mfTCPappC
{
    class Program
    {

            static public void Send(Socket soket, string msg)
            {
                int totalSent = 0;
                string data = msg;
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
        static string Read()
        {
            Console.WriteLine("Text:");
            string r = Console.ReadLine();
            return r;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello!");
            Socket sock = new Socket(
                AddressFamily.InterNetwork, 
                SocketType.Stream, 
                ProtocolType.Tcp 
                );
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint addr = new IPEndPoint(ip, 1337);
            sock.Connect(addr);

            Send(sock, Read());
            Console.WriteLine(Recive(sock));
        }
    }
}



