using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMap_Client.Net
{
    class Objects
    {
    }
    public class ClientNetRootObj
    {
        public string Action { get; set; }
        public string Message { get; set; }
    }
    public class ClientNetChatObj
    {
        public string Name { get; set; }
        public string Message { get; set; }
    }
    public class ClientNetLoginObj
    {
        public string SteamID { get; set; }
        public string Name { get; set; }
    }
    public class ClientNetInitObj
    {
        public string Map { get; set; }
    }
    public class ClientNetOccupiedObj
    {
        public string Signals { get; set; }
        public bool Occupied { get; set; }
    }
}
