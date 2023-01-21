using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;

using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using DotNetty.Buffers;
using DotNetty.Handlers.Tls;
using MahApps.Metro.Controls;

namespace MiniMap_Client.Net
{
    public class Client
    {
        public static async Task ConnectToServer(string host, int port)
        {
            try
            {
                Globals.MainClientWorkers = new MultithreadEventLoopGroup();
                //Globals.ChildClientWorkers = new MultithreadEventLoopGroup();

                var bootstrap = new Bootstrap();
                bootstrap
                    .Group(Globals.MainClientWorkers)
                    .Channel<TcpSocketChannel>()
                    .Option(ChannelOption.TcpNodelay, true)
                    .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
                    {
                        IChannelPipeline pipeline = channel.Pipeline;
                        pipeline.AddLast(new ClientHandler());
                    }));

                Globals.ClientChannel = await bootstrap.ConnectAsync(new IPEndPoint(IPAddress.Parse(host), port));
            }
            catch (Exception ex)
            {
                //AppendStatusText(ex.Message);
                //AppendStatusText(ex.StackTrace);
                //AppendStatusText(ex.Source);
                MainWindow.ShowMessage("Error", ex.Message);
            }
        }
        public static async void Connect(string host, int port) => await ConnectToServer(host, port);

        public static async void Close()
        {
            await Globals.ClientChannel.CloseAsync();

            await Globals.MainClientWorkers.ShutdownGracefullyAsync();

            //await Globals.ChildClientWorkers.ShutdownGracefullyAsync();
        }
        public static void Send(string message)
        {
            message += "\0";
            IByteBuffer sendBytes = Globals.ClientChannel.Allocator.Buffer().WriteBytes(Encoding.UTF8.GetBytes(message));
            Globals.ClientChannel.WriteAsync(sendBytes);
        }
        /*
        private static void ReceivedDataCallback(NetworkData incomingData, IConnection responseChannel)
        {
            Debug.WriteLine("-------------");
            Debug.WriteLine(Encoding.UTF8.GetString(incomingData.Buffer).Trim());
            Globals.MainWindowDispatcher.BeginInvoke(new Action(() =>
            {
                //Do this in GUI THREAD
            }));
        }
        private static void ConnectionEstablishedCallback(INode remoteAddress, IConnection responseChannel)
        {
            Globals.MainWindowDispatcher.BeginInvoke(new Action(() =>
            {
                //Do this in GUI THREAD
                Globals.ClientNetStatusGlobal.Content = "Подключён";
            }));
            Debug.WriteLine("---Connected---");
        }
        private static void ConnectionTerminatedCallback(HeliosConnectionException reason, IConnection closedChannel)
        {
            Globals.MainWindowDispatcher.BeginInvoke(new Action(() =>
            {
                //Do this in GUI THREAD
                Globals.ClientNetStatusGlobal.Content = "Не подключён";
            }));
            MainWindow.ShowMessage("Disconnected", string.Format("Disconnected from {0}", closedChannel.RemoteHost));
        }
        private static void ConnectionErrorCallback(Exception exception, IConnection connection)
        {
            Globals.MainWindowDispatcher.BeginInvoke(new Action(() =>
            {
                //Do this in GUI THREAD
                Globals.ClientNetStatusGlobal.Content = "Не подключён";
            }));
            MainWindow.ShowMessage("Error", exception.Message);
        }
        */
    }
}
