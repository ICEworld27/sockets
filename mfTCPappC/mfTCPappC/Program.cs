using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace mfTCPappC
{
    class Program
    {
        static public void TrueSend(Socket socket,byte[] data)
        {
            int totalSent = 0;
            while (totalSent < data.Length)
            {
                int actuallySent = socket.Send(
                data,
                totalSent,
                data.Length - totalSent,
                SocketFlags.None
                );
                totalSent += actuallySent;
            }
        }
        static public void TrueRecive(Socket socket, byte[] buffer)
        {
            int totalReceived = 0;
            while (totalReceived < buffer.Length)
            {
                int actuallyReceived = socket.Receive(
                    buffer,
                    totalReceived,
                    buffer.Length - totalReceived,
                    SocketFlags.None
                    );
                totalReceived += actuallyReceived;
            }
        }
        static public void Send(Socket soket, string msg)
        {
            string data = msg;
            byte[] dataToSend = Encoding.ASCII.GetBytes(data);
            byte[] lenOfsend = BitConverter.GetBytes(dataToSend.Length);
            TrueSend(soket, lenOfsend);
            TrueSend(soket, dataToSend);
        }
        static public string Recive(Socket sock)
        {
            byte[] bufferLen= new byte[4];
            TrueRecive(sock,bufferLen);
            byte[] buffer = new byte[BitConverter.ToInt32(bufferLen, 0)];
            TrueRecive(sock, buffer);
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
            sock.Close();
        }
    }
}



