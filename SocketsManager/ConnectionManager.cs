using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.SocketsManager
{
    public class ConnectionManager
    {
        private ConcurrentDictionary<string, WebSocket> connections = new ConcurrentDictionary<string, WebSocket>();

        public WebSocket getSocketById(string id_)
        {
            return connections.FirstOrDefault(x => x.Key == id_).Value;
        }

        public ConcurrentDictionary<string,WebSocket> getAllConnections()
        {
            return connections;
        }

        public string getID(WebSocket webSocket_)
        {
            return connections.FirstOrDefault(x => x.Value == webSocket_).Key;
        }

        public async Task RemoveSocketAsync(string id)
        {
            connections.TryRemove(id, out var socket);
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Socket connection closed.", CancellationToken.None);
        }

        public void addSocket(WebSocket webSocket)
        {
            connections.TryAdd(getConnectionID(), webSocket);
        }

        public string getConnectionID()
        {
            return Guid.NewGuid().ToString("N");
        }

    }
}
