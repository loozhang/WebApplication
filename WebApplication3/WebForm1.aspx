<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication3.WebForm1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <script type="text/javascript">
    
      function pageLoad() {
      }
      
      function btnInvoke_onclick() {
          var theName = $get("tbName").value;
          WebApplication3.SimpleWebService.SayHello(theName, onSayHelloSucceeded);
      }


      function onSayHelloSucceeded(result) {
          $get("result").innerHTML = result;
      }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
        <asp:ServiceReference Path="~/SimpleWebService.asmx" />
        </Services>
        </asp:ScriptManager>
    </div>
    <input id="tbName" type="text" />
<input id="btnInvoke" type="button" value="Say Hello" 
    onclick="return btnInvoke_onclick()" />
<div id="result"></div>

    </form>
</body>
</html>
