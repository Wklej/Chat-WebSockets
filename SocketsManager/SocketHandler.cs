using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.SocketsManager
{
    public abstract class SocketHandler
    {
        public ConnectionManager Connections { get; set; }

        public SocketHandler(ConnectionManager connectionManager)
        {
            Connections = connectionManager;
        }

        public virtual async Task onConnected(WebSocket webSocket)
        {
            await Task.Run(() => { Connections.addSocket(webSocket); });
        }

        public virtual async Task onDisconnected(WebSocket webSocket)
        {
            await Connections.RemoveSocketAsync(Connections.getID(webSocket));
        }

        public async Task sendMessage(WebSocket webSocket, string message)
        {
            if (webSocket.State != WebSocketState.Open)
                return;

            await webSocket.SendAsync(new System.ArraySegment<byte>(Encoding.ASCII.GetBytes(message), 0, message.Length)
                , WebSocketMessageType.Text, true, CancellationToken.None);

        }

    }
}
