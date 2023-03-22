using System.Net;
using System.Net.Sockets;
using System.Text;


using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
try
{
    await socket.ConnectAsync("127.0.0.1", 8888);
    Console.WriteLine($"Подключение к {socket.RemoteEndPoint} установлено");
}
catch (SocketException)
{
    Console.WriteLine($"Не удалось установить подключение с {socket.RemoteEndPoint}");
}
IPEndPoint point = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 80);
socket.Connect(point);
byte[] buffer = new byte[1024];
int c;
do
{
    c = socket.Receive(buffer);
    Console.WriteLine(Encoding.UTF8.GetString(buffer));
}
while (c > 0);

using var udpClient = new UdpClient(8001);
var brodcastAddress = IPAddress.Parse("235.5.5.11");

udpClient.JoinMulticastGroup(brodcastAddress);
Console.WriteLine("Начало прослушивания сообщений");
while (true)
{
    var result = await udpClient.ReceiveAsync();
    string message = Encoding.UTF8.GetString(result.Buffer);
    if (message == "END")
    { 

    Thread.Sleep(7000);
        break; }
    Console.WriteLine(message);
}

udpClient.DropMulticastGroup(brodcastAddress);
Console.WriteLine("Udp-клиент завершил свою работу");
