using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO;
using System.Diagnostics;

using MahApps.Metro.Controls;
using System.Windows.Controls.Primitives;
using Newtonsoft.Json;
using DotNetty.Transport.Channels;

namespace MiniMap_Client
{
    class Globals
    {
        //Interface
        public static Canvas TrackDrawPlateGlobal;
        public static StatusBarItem ClientNetStatusGlobal;
        public static RichTextBox ChatGlobal;
        //MainWindow Dispatcher
        public static Dispatcher MainWindowDispatcher;
        //Settings
        public static Config Settings;
        //Network
        public static IChannel ClientChannel;
        public static IEventLoopGroup MainClientWorkers;
        //public static IEventLoopGroup ChildClientWorkers;
    }
}
