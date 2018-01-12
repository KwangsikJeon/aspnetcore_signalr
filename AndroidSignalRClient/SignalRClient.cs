using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace AndroidSignalRClient
{
    public class SignalRClient
    {
        private HubConnection _connection = null;

        public async Task<bool> ConnectAsync()
        {
            var url = $"http://192.168.100.107:63502/chat";

            _connection = new HubConnectionBuilder()
                .WithUrl(url)
                .WithMessagePackProtocol()
                .WithConsoleLogger(LogLevel.Error)
                .WithTransport(Microsoft.AspNetCore.Sockets.TransportType.WebSockets)
                .Build();

            await _connection.StartAsync();
            _connection.Connected += _connection_Connected;
            _connection.Closed += _connection_Closed;
            return true;
        }

        private Task _connection_Closed(Exception arg)
        {
            _connection.Closed -= _connection_Closed;
            _connection.Connected -= _connection_Connected;
            _connection = null;
            return Task.FromResult<object>(null);
        }

        private Task _connection_Connected()
        {
            Log.Info("SignalRClient", "Connected");
            return Task.FromResult<object>(null);
        }
    }
}