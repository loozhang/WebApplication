<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebApplication3.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv="Pragma" content="no-cache">
    <meta http-equiv="Cache-Control" content="no-cache">
    <meta http-equiv="Expires" content="0">
    <title></title>
    <style type="text/css">
        #TextArea1
        {
            height: 88px;
            width: 365px;
        }
    </style>
 </head>
<body>
    <form id="form1" runat="server">
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
        <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">LinkButton</asp:LinkButton>
        <br />
        <asp:LinkButton
            ID="LinkButton2" runat="server" onclick="LinkButton2_Click" >LinkButton</asp:LinkButton>
    <br />
    <asp:LinkButton ID="LinkButton3" runat="server" onclick="LinkButton3_Click">LinkButton</asp:LinkButton>
    <br />
    <div >
        <iframe src="MessageInfo.aspx" style="height: 600px; width: 600px">

  </iframe>
    </div style="position:relative">
  
        <asp:Button ID="Button1" runat="server" Text="Log out" OnClick="btnLogout_Click"/>
  

    </form>
</body>
</html>
