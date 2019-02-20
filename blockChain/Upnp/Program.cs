using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Upnp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==============upnp测试============");

            testUpnp();
            Thread.Sleep(3000);
            
            Task.Run(()=> {
                startServer(internalip, port);
            });
            Task.Run(() =>
            {
                startClient(internalip, port);//内网----》内网
                startClient(extralip, port);//通过网关ip连接内网的服务器
            });

            Console.ReadKey();
        }
        static IPAddress extralip;
        static IPAddress internalip;
        static int port = 34343;


        private async static void testUpnp()
        {
            bool findGate = await Tool.UPnP.DiscoverAsync();//查找网关
            Console.WriteLine("find Gate:" + findGate);

            IPAddress externalIP = await Tool.UPnP.GetExternalIPAsync();//获取外网IP
            Console.WriteLine("external Ip:" + externalIP);

            var interIp = (await Dns.GetHostAddressesAsync(Dns.GetHostName())).First(p => p.AddressFamily == AddressFamily.InterNetwork);
            Console.WriteLine("internal Ip:" + interIp);

            await Tool.UPnP.ForwardPortAsync(port, System.Net.Sockets.ProtocolType.Tcp, "allpet upnp");//端口映射
            Console.WriteLine("端口映射 网关端口--》内网端口");

            extralip = externalIP;
            internalip = interIp;
        }

        static void startServer(IPAddress internalip, int port)
        {

            Console.WriteLine("====建立服务器端====");
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(internalip, port));
            socket.Listen(1000);
            while(true)
            {
                var client = socket.Accept();
                client.Send(Encoding.Unicode.GetBytes("欢迎来到内网的服务器！===" + Environment.NewLine));
            }
            //Console.ReadKey(false);
        }

        static void startClient(IPAddress extralip, int port)
        {
            try
            {
                var id = Guid.NewGuid().GetHashCode();

                Console.WriteLine("====建立客户端（"+id+"）====");
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(new IPEndPoint(extralip, port));

                using (var ns = new NetworkStream(socket))
                using (var sr = new StreamReader(ns, Encoding.Unicode))
                {
                    Console.WriteLine("client:"+id+" -------《  收到来自服务器的消息：");
                    Console.WriteLine(sr.ReadLine());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
