using Chat.SocketsManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Handlers
{
    public class WebSocketMessageHandler : SocketHandler
    {
        public WebSocketMessageHandler(ConnectionManager connectionManager) : base(connectionManager)
        {
        }

        public override async Task onConnected(WebSocket webSocket)
        {
            await base.onConnected(webSocket);
            var socketId = Connections.getID(webSocket);
            await sendMessageToAll($"{socketId} just joined!");
        }

        public override async Task Receive(WebSocket webSocket, WebSocketReceiveResult result, byte[] buffer)
        {
            var socketId = Connections.getID(webSocket);
            var message = $"{socketId}: {Encoding.UTF8.GetString(buffer, 0, result.Count)}";
            await sendMessageToAll(message);
        }
    }
}
