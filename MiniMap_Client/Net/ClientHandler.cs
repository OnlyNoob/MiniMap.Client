using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;

using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using DotNetty.Buffers;
using DotNetty.Handlers.Tls;
using Newtonsoft.Json;
using MahApps.Metro.Controls;

namespace MiniMap_Client.Net
{
    public class ClientHandler : ChannelHandlerAdapter
    {
        readonly IByteBuffer initialMessage;
        readonly byte[] buffer;

        public ClientHandler()
        {
            ClientNetLoginObj LoginMessage = new ClientNetLoginObj();
            LoginMessage.SteamID = "SteamID Soon";
            LoginMessage.Name = "ClientName Soon";

            ClientNetRootObj SendMessage = new ClientNetRootObj();
            SendMessage.Action = "login";
            SendMessage.Message = JsonConvert.SerializeObject(LoginMessage);

            this.buffer = new byte[4096];
            this.initialMessage = Unpooled.Buffer(4096);
            byte[] messageBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(SendMessage) + "\0");
            this.initialMessage.WriteBytes(messageBytes);
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            //Это при подключении) Раскоментить когда будет готов логин)
            //
            Globals.MainWindowDispatcher.BeginInvoke(new Action(() =>
            {
                //Do this in GUI THREAD
                Globals.ClientNetStatusGlobal.Content = "Подключён";
            }));
            context.WriteAndFlushAsync(this.initialMessage);
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var byteBuffer = message as IByteBuffer;
            if (byteBuffer != null)
            {
                Debug.WriteLine("Received from server: " + byteBuffer.ToString(Encoding.UTF8));
                foreach(string msg in byteBuffer.ToString(Encoding.UTF8).Split(new Char[] { '\0' }))
                {
                    if(msg.Length > 0)
                        ReceiveMessage(msg);
                }
            }
            //context.WriteAsync(message);
            //context.WriteAndFlushAsync(message);
        }

        public override Task WriteAsync(IChannelHandlerContext context, object message)
        {
            if (message is IByteBuffer)
                return context.WriteAndFlushAsync(message);

            IByteBuffer buffer = context.Allocator.Buffer().WriteBytes(message as byte[]);

            return context.WriteAndFlushAsync(buffer);
        }

        public override void ChannelReadComplete(IChannelHandlerContext context)
        {
            context.Flush();
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            context.CloseAsync();
            MainWindow.ShowMessage("Error", exception.Message);
        }
        public override void ChannelRegistered(IChannelHandlerContext context)
        {
            //Globals.MainWindowDispatcher.BeginInvoke(new Action(() =>
            //{
            //    //Do this in GUI THREAD
            //    Globals.ClientNetStatusGlobal.Content = "Подключён";
            //}));
        }
        public override void ChannelUnregistered(IChannelHandlerContext context)
        {
            Globals.MainWindowDispatcher.BeginInvoke(new Action(() =>
            {
                //Do this in GUI THREAD
                Globals.ClientNetStatusGlobal.Content = "Не подключён";
            }));
        }
        public static void ReceiveMessage(string message)
        {
            ClientNetRootObj recvmessage = JsonConvert.DeserializeObject<ClientNetRootObj>(message);
            switch (recvmessage.Action)
            {
                case "chat":
                    ClientNetChatObj chatMsg = JsonConvert.DeserializeObject<ClientNetChatObj>(recvmessage.Message);
                    Globals.MainWindowDispatcher.BeginInvoke(new Action(() =>
                    {
                        Globals.ChatGlobal.AppendText(string.Format("{0}: {1}\r", chatMsg.Name, chatMsg.Message));
                        Globals.ChatGlobal.ScrollToEnd();
                    }));
                    break;
                case "init":
                    ClientNetInitObj initMsg = JsonConvert.DeserializeObject<ClientNetInitObj>(recvmessage.Message);
                    Globals.MainWindowDispatcher.BeginInvoke(new Action(() =>
                    {
                        if(Globals.TrackDrawPlateGlobal.Children.Count > 0)
                        {
                            Globals.TrackDrawPlateGlobal.Children.Clear();
                        }
                        Track.Init(initMsg.Map);
                    }));
                    break;
                case "occupied":
                    ClientNetOccupiedObj occupiedmsg = JsonConvert.DeserializeObject<ClientNetOccupiedObj>(recvmessage.Message);
                    string[] Signals = occupiedmsg.Signals.Split(new char[] { '-' });
                    for (int LineID = 0; LineID < TrackData.track_gm_mus_orange_metro_h.TrackDataLines.GetLength(0); LineID++)
                    {
                        if ((string)TrackData.track_gm_mus_orange_metro_h.TrackDataLines[LineID, 4] == string.Format("{0}-{1}",Signals[0],Signals[1]) ||
                            (string)TrackData.track_gm_mus_orange_metro_h.TrackDataLines[LineID, 4] == string.Format("{0}-{1}", Signals[1], Signals[0]))
                        {
                            Line TrackLine = (Line)TrackData.track_gm_mus_orange_metro_h.TrackDataLines[LineID, 3];
                            if (occupiedmsg.Occupied)
                            {
                                Globals.MainWindowDispatcher.BeginInvoke(new Action(() =>
                                {
                                    TrackLine.Stroke = Brushes.Red;
                                }));
                            } else
                            {
                                Brush DefaultColor = (Brush)TrackData.track_gm_mus_orange_metro_h.TrackDataLines[LineID, 5] ?? Brushes.Green;
                                Globals.MainWindowDispatcher.BeginInvoke(new Action(() =>
                                {
                                    TrackLine.Stroke = DefaultColor;
                                }));
                            }
                        }
                    }
                    break;
            }
        }
    }
}