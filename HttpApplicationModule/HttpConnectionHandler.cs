using Microsoft.AspNetCore.Connections;

using System;
using System.Threading.Tasks;

namespace Console_HubApplication
{

    internal class HttpConnectionHandler: ConnectionHandler
    {
        public override async Task OnConnectedAsync(ConnectionContext connection)
        {
            Console.WriteLine($"{connection.LocalEndPoint.AddressFamily.ToString()}");
            await Task.CompletedTask;
        }
    }

}