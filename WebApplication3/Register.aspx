<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebApplication3.Register" %>

<%@ Register Assembly="Utility" Namespace="Utility" TagPrefix="cc2" %>

<%@ Register Assembly="Common" Namespace="Common" TagPrefix="cc1" %>
    <script src="JS/jquery-1.3.2.min.js" type="text/javascript"></script>

    <script src="JS/jquery.select.js" type="text/javascript"></script>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <title></title>
           <script type="text/javascript">
               function isValidDay(obj_year, obj_month, obj_day) {
                   var m = parseInt(obj_month.options[obj_month.selectedIndex].text, 10) - 1;
                   var d = parseInt(obj_day.options[obj_day.selectedIndex].text, 10);

                   var end = new Array(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);

                   if ((obj_year.options[obj_year.selectedIndex].text % 4 == 0
		&& obj_year.options[obj_year.selectedIndex].text % 100 != 0)
		|| obj_year.options[obj_year.selectedIndex].text % 400 == 0) {
                       end[1] = 29;
                   }

                   return (d >= 1 && d <= end[m]);
               }
               function SetDayList(obj_year, obj_month, obj_day) {
                   var m = parseInt(obj_month.options[obj_month.selectedIndex].text, 10) - 1;
                   var end = new Array(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);

                   if (obj_month.selectedIndex == 0) {
                       obj_day.length = 2;
                       return;
                   }

                   if ((obj_year.options[obj_year.selectedIndex].text % 4 == 0
		&& obj_year.options[obj_year.selectedIndex].text % 100 != 0)
		|| obj_year.options[obj_year.selectedIndex].text % 400 == 0) {
                       end[1] = 29;
                   }

                   obj_day.length = end[m];
                   for (i = 0; i < end[m]; i++) {
                       obj_day.options[i + 2] = new Option(i + 1);
                   }
               }
               function SetBirthday(obj_year, obj_month, obj_day, obj_birth) {
                   if (isValidDay(obj_year, obj_month, obj_day)) {
                       obj_birth.value = obj_year.options[obj_year.selectedIndex].text + '/' + obj_month.options[obj_month.selectedIndex].text + '/' + obj_day.options[obj_day.selectedIndex].text;
                   }
               }
               function ChangeValidateImg(img) {
                   img.src = "ValidateCode/ValidateCode.aspx?r=" + Math.random();
               }
               function AlertMessage(msg) {
                   parent.alert(msg);
               }
    </script>
</head>
<body>

<form runat="server">

        <div> 
        <asp:ScriptManager ID="ScriptManager2" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="upLogin" runat="server" UpdateMode="Conditional">
                <ContentTemplate>              
                        
        <asp:Label ID="lblUserName" runat="server" Text="用户名"></asp:Label>
        <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                                                   </ContentTemplate>
            </asp:UpdatePanel>
        <br />
        <asp:Label ID="lblPassword" runat="server" Text="密码"></asp:Label>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" Display="Dynamic"
            ErrorMessage="请输入密码"></asp:RequiredFieldValidator> 
        <asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="txtPassword" Display="Dynamic"></asp:RegularExpressionValidator>
        <br />
        <asp:Label ID="lblPasswordConfirm" runat="server" Text="确认密码"></asp:Label>
        <asp:TextBox ID="txtPasswordConfirm" runat="server" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvPasswordConfirm" runat="server" ControlToValidate="txtPasswordConfirm" Display="Dynamic" 
            ErrorMessage="确认密码不能为空"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="cvPasswordConfirm" runat="server" ControlToValidate="txtPasswordConfirm" ControlToCompare="txtPassword" 
                    Display="Dynamic" ErrorMessage="密码必须匹配" />
        <br />
        <asp:Label ID="lblNickName" runat="server" Text="昵称"></asp:Label>
        <asp:TextBox ID="txtNickName" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvNickName" runat="server" ControlToValidate="txtNickName" Display="Dynamic"
            ErrorMessage="请输入昵称"></asp:RequiredFieldValidator> 
        <br />
        <asp:Label ID="lblMobile" runat="server" Text="电话"></asp:Label>
        <asp:TextBox ID="txtMobile" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvMobile" runat="server"  ControlToValidate="txtMobile"  Display="Dynamic" ErrorMessage="请输入手机号码" />
                                            <span
                                                style="color: #ff0000" __designer:mapid="29b">
                                                <asp:RegularExpressionValidator ID="revMobile" 
                    runat="server" ControlToValidate="txtMobile"
                                                    Display="Dynamic" ErrorMessage="请输入移动手机号" 
                                                
                    ValidationExpression="^1(3[4-9]|5[012789]|8[78])\d{8}$"></asp:RegularExpressionValidator>
                                            </span>
        <br />
        <asp:Label ID="lblName" runat="server" Text="姓名"></asp:Label>
        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblRadioButtonList" runat="server" Text="性别"></asp:Label>
        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Value="1">男</asp:ListItem>
            <asp:ListItem Value="0">女</asp:ListItem>
        </asp:RadioButtonList>
    
        <asp:Label ID="lblBirth" runat="server" Text="出生日期"></asp:Label>
        <asp:HiddenField ID="birth" runat="server" />
        <asp:DropDownList ID="ddlYear" runat="server" AppendDataBoundItems="True" TabIndex="8">
        </asp:DropDownList>
        年
        <asp:DropDownList ID="ddlMonth" runat="server" AppendDataBoundItems="True" TabIndex="9">
        </asp:DropDownList>
        月
        <asp:DropDownList ID="ddlDay" runat="server" AppendDataBoundItems="True" TabIndex="10">
        </asp:DropDownList>
        日
    
       <br />
        <asp:Label ID="lblArea" runat="server" Text="所在地"></asp:Label>
<%--            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>--%>
      <cc1:SelectArea ID="SelectArea1" runat="server" IsAddSpace="false"/>
    
        <br />
        <asp:Label ID="lblEmail" runat="server" Text="电子邮箱"></asp:Label>
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvEmail" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="请输入电子邮箱" runat="server" ></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="revEMail" runat="server" ControlToValidate="txtEmail" ErrorMessage="格式错误" Display="Dynamic"></asp:RegularExpressionValidator>
        
    
        <br />
        <asp:Label ID="lblBlog" runat="server" Text="博客"></asp:Label>
        <asp:TextBox ID="txtBlog" runat="server"></asp:TextBox>
    
        <br />

                    <asp:Label ID="lblValidate" runat="server" Text="验证码"></asp:Label>
                    <asp:TextBox ID="txtValidate" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvValidate" runat="server" ControlToValidate="txtValidate" Display="Dynamic" ErrorMessage="请输入验证码"></asp:RequiredFieldValidator> 
                    <img onclick="ChangeValidateImg(this);" runat="server" id="imgValidateImg" style="vertical-align: middle;" alt="验证码看不清？点击刷新验证码！" />
        <br />
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" ForeColor="#FF6000" Font-Size="12px"
                        Font-Names="微软雅黑,宋体" Visible="false" />
        <asp:Button ID="Button1" runat="server" Text="确定" onclick="Button1_Click" /> 
       
           </div> 
    </form>
</body>
</html>