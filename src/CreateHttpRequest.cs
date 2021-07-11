using System;
using System.Net.Sockets;
using System.Text;
using Mono.Unix;

namespace FDK
{
    public class CreateHttpRequest
    {
        public static void HttpRequestCreation()
        {
            var socket = new Socket(AddressFamily.Unix,SocketType.Stream, ProtocolType.IP);
            var unixEndPoint = new UnixEndPoint(Server._realSock);
            socket.Connect(unixEndPoint);
            var request = $"GET /getMyData?id=testIdValue "
            + "HTTP/1.1\r\n"
            + "Host: localhost\r\n"
            + "Content-Length: 0\r\n"
            + "Fn-Intent: http\r\n"
            + "\r\n";
            // convert the request into byte data
            byte[] requestBytes = Encoding.ASCII.GetBytes(request);
            socket.Send(requestBytes);
            byte[] bytesReceived = new byte[1024];
            int numBytes = socket.Receive(bytesReceived);
            string responseReceived = Encoding.ASCII.GetString(bytesReceived);
            ParseResponseBody(responseReceived);
            socket.Disconnect(false);
            socket.Close();
        }

        public static void ParseResponseBody(string str)
        {
            string[] strArray = str.Split("\n");
            int header_count = 0;
            foreach(string s in strArray)
            {
                if(String.IsNullOrWhiteSpace(s)==true || s=="\n")
                continue;
                header_count++;
                Console.WriteLine(header_count+")" + s);
            }
        }
    }
}