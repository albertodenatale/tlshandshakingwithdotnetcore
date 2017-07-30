using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace tcpserver
{
    class Program
    {
        static void Main(string[] args)
		{

			// Start the server  
			TcpHelper.StartServer(5678);
            var quit = TcpHelper.Listen().Result; // Start listening. 
		}
    }

	class TcpHelper
	{
		private static TcpListener listener { get; set; }
		private static bool accept { get; set; } = false;

		public static void StartServer(int port)
		{
			IPAddress address = IPAddress.Parse("127.0.0.1");
			listener = new TcpListener(address, port);

			listener.Start();
			accept = true;

			Console.WriteLine($"Server started. Listening to TCP clients at 127.0.0.1:{port}");
		}

		public async static Task<string> Listen()
		{
			if (listener != null && accept)
			{
				// Continue listening.  
				while (true)
				{
					Console.WriteLine("Waiting for client...");
					var clientTask = listener.AcceptTcpClientAsync(); // Get the client  

					if (clientTask.Result != null)
				    {
						Console.WriteLine("Client connected. Waiting for data.");
						var client = clientTask.Result;
						string message = "";
                        try
                        {

	                        var serverCertificate = new X509Certificate2(@"/Users/albertodenatale/.dotnet/corefx/cryptography/x509stores/my/cert.pfx", "alberto");

	                        using (var sslStream = new SslStream(client.GetStream(), false))
	                        {
	                            Task task = sslStream.AuthenticateAsServerAsync(serverCertificate, false, SslProtocols.Tls, false);

	                            await task;

	                            while (message != null && !message.StartsWith("quit"))
	                            {
	                                byte[] data = Encoding.ASCII.GetBytes("Send next data: [enter 'quit' to terminate] ");
	                                sslStream.Write(data, 0, data.Length);

	                                byte[] buffer = new byte[1024];
	                                sslStream.Read(buffer, 0, buffer.Length);

	                                message = Encoding.ASCII.GetString(buffer);
	                                Console.WriteLine(message);
	                            }

	                        }
	                    }
                        catch(Exception){
                            
                        }
                        try{
							Console.WriteLine("Closing connection.");
							client.GetStream().Dispose();
                            
                        }
                        catch(Exception){
                            
                        }
					}

				}
			}

            return "quit";
		}
	}
}
