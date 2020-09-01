<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication3._Default" %>

<%@ Register src="WebUserControl1.ascx" tagname="WebUserControl1" tagprefix="uc1" %>
<%@ Register src="Message.ascx" tagname="Message" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>

    <script language="javascript">
        function btnInvoke_onclick() {
            var theName = $get("tbName").value;
            WebApplication3.SimpleWebService.SayHello(theName, onSayHelloSucceeded);
        }


        function onSayHelloSucceeded(result) {
            $get("result").innerHTML += result;
        }


    </script>
</head>
<body bgcolor=<%=Session["color"]%>>
    <form id="form1" runat="server" >
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
        <asp:ServiceReference Path="Services/SimpleWebService.asmx" />
        <asp:ServiceReference Path="Services/AutoComplete.asmx" />
        </Services>
        </asp:ScriptManager>
    <div >
    <h3>Default Page</h3>
    <div id="wrapper">
    <a href="Display.aspx" title="Sign In">Sign In</a>
    
    </div>


    <input id="tbName" type="text" />
<input id="btnInvoke" type="button" value="Say Hello（ajax实现）" 
    onclick="return btnInvoke_onclick()" />
        <asp:Button ID="Button1" runat="server" Text="后台调用" onclick="Button1_Click" />
<div id="result"></div>

<asp:Label ID="Label1" runat="server"></asp:Label>
    </div>
   
    <uc1:WebUserControl1 ID="WebUserControl11" runat="server" />
    </form>
</body>

</html>
