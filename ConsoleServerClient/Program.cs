using System.Net;
using System.Net.Sockets;
using System.Text;

IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, 8888);
using Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
socket.Bind(ipPoint);
socket.Listen();
Console.WriteLine("Сервер запущен. Ожидание подключений...");
// получаем входящее подключение
using Socket client = await socket.AcceptAsync();
// получаем адрес клиента
Console.WriteLine($"Адрес подключенного клиента: {client.RemoteEndPoint}");

var messages = new string[] { "Hello World!", "Hello METANIT.COM", "Hello work", "ENDA" };
var brodcastAddress = IPAddress.Parse("235.5.5.11");
using var udpSender = new UdpClient();
Console.WriteLine("Начало отправки сообщений...");
// отправляем сообщения
foreach (var message in messages)
{
    Console.WriteLine($"Отправляется сообщение: {message}");
    byte[] data = Encoding.UTF8.GetBytes(message);
    await udpSender.SendAsync(data, new IPEndPoint(brodcastAddress, 8001));
    await Task.Delay(1000);
}
