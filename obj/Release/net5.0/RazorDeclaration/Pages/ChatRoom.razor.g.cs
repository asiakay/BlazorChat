// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace BlazorChat.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "/Users/asialakaygradyloves/Projects/BlazorChat/BlazorChat/_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/asialakaygradyloves/Projects/BlazorChat/BlazorChat/_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/Users/asialakaygradyloves/Projects/BlazorChat/BlazorChat/_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "/Users/asialakaygradyloves/Projects/BlazorChat/BlazorChat/_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "/Users/asialakaygradyloves/Projects/BlazorChat/BlazorChat/_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "/Users/asialakaygradyloves/Projects/BlazorChat/BlazorChat/_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "/Users/asialakaygradyloves/Projects/BlazorChat/BlazorChat/_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "/Users/asialakaygradyloves/Projects/BlazorChat/BlazorChat/_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "/Users/asialakaygradyloves/Projects/BlazorChat/BlazorChat/_Imports.razor"
using BlazorChat;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "/Users/asialakaygradyloves/Projects/BlazorChat/BlazorChat/_Imports.razor"
using BlazorChat.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/Users/asialakaygradyloves/Projects/BlazorChat/BlazorChat/Pages/ChatRoom.razor"
using Microsoft.AspNetCore.SignalR.Client;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/chatroom")]
    public partial class ChatRoom : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 55 "/Users/asialakaygradyloves/Projects/BlazorChat/BlazorChat/Pages/ChatRoom.razor"
       
    // flag to indicate chat status
    private bool _isChatting = false;

    // name of the user who will be chatting
    private string _username;

    // on-screen message
    private string _message;

    // new message input
    private string _newMessage;

    // list of messages in chat
    private List<Message>
    _messages = new List<Message>
        ();

        private string _hubUrl;
        private HubConnection _hubConnection;

        public async Task Chat()
        {
        // check username is valid
        if (string.IsNullOrWhiteSpace(_username))
        {
        _message = "Please enter a name";
        return;
        };

        try
        {
        // Start chatting and force refresh UI.
        _isChatting = true;
        await Task.Delay(1);

        // remove old messages if any
        _messages.Clear();

        // Create the chat client
        string baseUrl = navigationManager.BaseUri;

        _hubUrl = baseUrl.TrimEnd('/') + BlazorChatSampleHub.HubUrl;

        _hubConnection = new HubConnectionBuilder()
        .WithUrl(_hubUrl)
        .Build();

        _hubConnection.On<string, string>
            ("Broadcast", BroadcastMessage);

            await _hubConnection.StartAsync();

            await SendAsync($"[Notice] {_username} joined chat room.");
            }
            catch (Exception e)
            {
            _message = $"ERROR: Failed to start chat client: {e.Message}";
            _isChatting = false;
            }
            }

            private void BroadcastMessage(string name, string message)
            {
            bool isMine = name.Equals(_username, StringComparison.OrdinalIgnoreCase);

            _messages.Add(new Message(name, message, isMine));

            // Inform blazor the UI needs updating
            StateHasChanged();
            }

            private async Task DisconnectAsync()
            {
            if (_isChatting)
            {
            await SendAsync($"[Notice] {_username} left chat room.");

            await _hubConnection.StopAsync();
            await _hubConnection.DisposeAsync();

            _hubConnection = null;
            _isChatting = false;
            }
            }

            private async Task SendAsync(string message)
            {
            if (_isChatting && !string.IsNullOrWhiteSpace(message))
            {
            await _hubConnection.SendAsync("Broadcast", _username, message);

            _newMessage = string.Empty;
            }
            }

            private class Message
            {
            public Message(string username, string body, bool mine)
            {
            Username = username;
            Body = body;
            Mine = mine;
            }

            public string Username { get; set; }
            public string Body { get; set; }
            public bool Mine { get; set; }

            public bool IsNotice => Body.StartsWith("[Notice]");

            public string CSS => Mine ? "sent" : "received";
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager navigationManager { get; set; }
    }
}
#pragma warning restore 1591
