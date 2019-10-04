<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="SimpleWS2.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    
   

</head>
<body>
    <form id="form1" runat="server">
        <div>
            
            <span id="webSocketStatusSpan"></span>
            <br />
            <span id="webSocketReceiveDataSpan"></span>
            <br />
            <span>Please enter a string</span>
            <br />
            <input id="nameTextBox" type="text" value="" />
            <input type="button" value="Send data" onclick="SendData();" />
            <input type="button" value="Close WebSocket" onclick="CloseWebSocket();" />
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="json test" />
&nbsp;<asp:TextBox ID="TextBox1" runat="server" Width="315px"></asp:TextBox>
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="deserial" />


        </div>
    </form>
     <script type="text/javascript">

        var webSocketStatusSpan = document.getElementById("webSocketStatusSpan");
        var webSocketReceiveDataSpan = document.getElementById("webSocketReceiveDataSpan");
        var nameTextBox = document.getElementById("nameTextBox");

        var webSocket;

        //The address of our HTTP-handler
        var handlerUrl = "ws://localhost:54672/WebSocketHandler.ashx";
        //http://localhost:54672/'
        function SendData() {

            //Initialize WebSocket.
            InitWebSocket();

            //Send data if WebSocket is opened.
            if (webSocket.OPEN && webSocket.readyState == 1)
                webSocket.send(nameTextBox.value);

            //If WebSocket is closed, show message.
            if (webSocket.readyState == 2 || webSocket.readyState == 3)
                webSocketStatusSpan.innerText = "WebSocket closed, the data can't be sent.";
        }

        function CloseWebSocket() {
            webSocket.close();
        }

        function InitWebSocket() {

            //If the WebSocket object isn't initialized, we initialize it.
            if (webSocket == undefined) {
                webSocket = new WebSocket(handlerUrl);

                //Open connection  handler.
                webSocket.onopen = function () {
                    webSocketStatusSpan.innerText = "WebSocket opened.";
                    webSocket.send(nameTextBox.value);
                };

                //Message data handler.
                webSocket.onmessage = function (e) {
                    webSocketReceiveDataSpan.innerText = e.data;
                };

                //Close event handler.
                webSocket.onclose = function () {
                    webSocketStatusSpan.innerText = "WebSocket closed.";
                };

                //Error event handler.
                webSocket.onerror = function (e) {
                    webSocketStatusSpan.innerText = e.message;
                }
            }
        }
    </script>

</body>
</html>
