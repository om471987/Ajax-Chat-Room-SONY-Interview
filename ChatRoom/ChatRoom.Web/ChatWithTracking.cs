using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;
using System.Linq;
using Microsoft.AspNet.SignalR;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using ChatRoom.Web.Models;

namespace ChatRoom.Web
{
    public class ChatWithTracking : Hub
    {
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();

        public void Send(string message, string sender)
        {
            string name = Context.QueryString["name"];
            
            if (message.StartsWith("@") && message.Contains(":"))
            {
                var user = string.Empty;
                user = message.Split(':')[0];
                user = user.Remove(0, 1);

                if (_connections._connections.ContainsKey(user))
                {
                    Clients.Client(_connections.GetConnections(user).FirstOrDefault()).send(name + ": " + message);
                    if (user != sender)
                    {
                        Clients.Client(_connections.GetConnections(sender).FirstOrDefault()).send(name + ": " + message);
                    }
                    return;
                }
            }
            Clients.All.send(name + ": " + message);
        }

        public void GetUserList()
        {
            var oSerializer = new JavaScriptSerializer();
            var keyList = new List<string>(_connections._connections.Keys);
            var json = oSerializer.Serialize(keyList);
            Clients.All.userList(json);
        }

        public override Task OnConnected()
        {
            string name = Context.QueryString["name"];

            _connections.Add(name, Context.ConnectionId);

            GetUserList();

            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            string name = Context.QueryString["name"];

            _connections.Remove(name, Context.ConnectionId);

            return base.OnDisconnected();
        }        
    }
}