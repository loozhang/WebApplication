<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessageInfo.aspx.cs" Inherits="WebApplication3.MessageInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <script src="JS/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="JS/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            var hideDelay = 500;
            var currentID;
            var hideTimer = null;

            // One instance that's reused to show info for the current person   
            var container = $('<div id="personPopupContainer">'
      + '<table width="" border="0" cellspacing="0" cellpadding="0" align="center" class="personPopupPopup">'
      + '<tr>'
      + '   <td class="corner topLeft"></td>'
      + '   <td class="top"></td>'
      + '   <td class="corner topRight"></td>'
      + '</tr>'
      + '<tr>'
      + '   <td class="left">&nbsp;</td>'
      + '   <td><div id="personPopupContent"></div></td>'
      + '   <td class="right">&nbsp;</td>'
      + '</tr>'
      + '<tr>'
      + '   <td class="corner bottomLeft">&nbsp;</td>'
      + '   <td class="bottom">&nbsp;</td>'
      + '   <td class="corner bottomRight"></td>'
      + '</tr>'
      + '</table>'
      + '</div>');

            $('body').append(container);

            $('.personPopupTrigger').live('mouseover', function (event) {
                // format of 'rel' tag: pageid,personguid   
                var settings = $(this).attr('rel').split(',');
                var pageID = settings[0];
                currentID = settings[1];

                // If no guid in url rel tag, don't popup blank   
                if (currentID == '')
                    return;

                if (hideTimer)
                    clearTimeout(hideTimer);

                var pos = $(this).offset();
                var width = $(this).width();
                var offset = $(event.target).offset();
                container.css({ top: offset.top + $(event.target).height() + "px", left: offset.left - 150,"position": "relative",
    "z-index":2 });

                $('#personPopupContent').html('&nbsp;<asp:HyperLink runat="server" NavigateUrl="~/About.aspx" >HyperLink</asp:HyperLink>');

                $.ajax({
                    type: 'GET',
                    url: 'personajax.aspx',
                    data: 'page=' + pageID + '&guid=' + currentID,
                    success: function (data) {
                        // Verify that we're pointed to a page that returned the expected results.   
                        if (data.indexOf('personPopupResult') < 0) {
                            $('#personPopupContent').html('<span >Page ' + pageID + ' did not return a valid result for person ' + currentID + '.Please have your administrator check the error log.</span>');
                        }

                        // Verify requested person is this person since we could have multiple ajax   
                        // requests out if the server is taking a while.   
                        if (data.indexOf(currentID) > 0) {
                            var text = $(data).find('.personPopupResult').html();
                            $('#personPopupContent').html(text);
                        }
                    }
                });

                container.css('display', 'block');
            });

            $('.personPopupTrigger').live('mouseout', function () {
                if (hideTimer)
                    clearTimeout(hideTimer);
                hideTimer = setTimeout(function () {
                    container.css('display', 'none');
                }, hideDelay);
            });

            // Allow mouse over of details without hiding details   
            $('#personPopupContainer').mouseover(function () {
                if (hideTimer)
                    clearTimeout(hideTimer);
            });

            // Hide after mouseout   
            $('#personPopupContainer').mouseout(function () {
                if (hideTimer)
                    clearTimeout(hideTimer);
                hideTimer = setTimeout(function () {
                    container.css('display', 'none');
                }, hideDelay);
            });
        });
</script>
    <style type="text/css">
        #TextArea1
        {
            height: 113px;
            width: 324px;
        }
    </style>
    <style type="text/css">
{       
    position:relative;       
    left:0;       
    top:0;       
       
    z-index: 20000;   }    
  
.div
{
    position:absolute;
top:0%; 
left:25%; 
   z-index:1;
   
    }
.personPopupPopup   
{   
 
    
}  
 
#personPopupContent   
{   
    background-color: #FFF;   
    min-width: 175px;   
    min-height: 50px;   
}   
  
.personPopupPopup .personPopupImage   
{   
    margin: 5px;   
    margin-right: 15px;   
}   
  
.personPopupPopup .corner    
{   
    width: 19px;   
    height: 15px;   
}   
       
.personPopupPopup .topLeft    
{   
    background: url(../images/personpopup/balloon_topLeft.png) no-repeat;   
}   
       
.personPopupPopup .bottomLeft    
{   
    background: url(../images/personpopup/balloon_bottomLeft.png) no-repeat;   
}   
       
.personPopupPopup .left    
{   
    background: url(../images/personpopup/balloon_left.png) repeat-y;   
}   
       
.personPopupPopup .right    
{   
    background: url(../images/personpopup/balloon_right.png) repeat-y;   
}   
       
.personPopupPopup .topRight    
{   
    background: url(../images/personpopup/balloon_topRight.png) no-repeat;   
}   
       
.personPopupPopup .bottomRight    
{   
    background: url(../images/personpopup/balloon_bottomRight.png) no-repeat;   
}   
       
.personPopupPopup .top    
{   
    background: url(../images/personpopup/balloon_top.png) repeat-x;   
}   
       
.personPopupPopup .bottom    
{   
    background: url(../images/personpopup/balloon_bottom.png) repeat-x;   
    text-align: center;   
}  
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div class=div>
    
        <textarea id="TextArea1" name="S1" runat="server"></textarea>
        <br />
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    <a class="personPopupTrigger" href="<link to person>" rel="4218,a17bee64-8593-436e-a2f8-599a626370df">House, Devon</a>   
    <a class="personPopupTrigger" href="<link to person>" rel="4218,f6434101-15bf-4c06-bbb2-fbe8c111b948">House, Gregory</a>
    <asp:HyperLink  ID="HyperLink1" runat="server" NavigateUrl="~/About.aspx">HyperLink</asp:HyperLink>
        <asp:GridView ID="GridView1" SkinID="defaultView" Width="200px" runat="server"
                                    AutoGenerateColumns="False" AllowPaging="True" AllowSorting="False" ShowFooter="true"
                                     PageSize="20" OnPageIndexChanging="GridView1_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="NickName" HeaderText="博主" />
                                        <asp:BoundField DataField="content" HeaderText="内容" />
                                        <%--<asp:BoundField DataField="ContactMobile" HeaderText="联系人手机号" />
                                        <asp:BoundField DataField="ProductName" HeaderText="产品名称" />
                                        <asp:BoundField DataField="CreateTime" HeaderText="创建时间" DataFormatString="{0:yyyy-MM-dd}" />
                                        <asp:BoundField DataField="EffectTime" HeaderText="生效时间" DataFormatString="{0:yyyy-MM-dd}" />
                                        <asp:BoundField DataField="ExpireTime" HeaderText="失效时间" DataFormatString="{0:yyyy-MM-dd}" />--%>
                                        <asp:TemplateField HeaderText="操作">
                                            <ItemTemplate>
                                                <a href="javascript:void(0)" onclick="Retweet(<%#"'" +  Eval("MsgID") + "'"%>)">
                                                    转发</a>                                                
                                                <a href="javascript:void(0)">
                                                    评论</a>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                    </Columns>
                                </asp:GridView>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
    
    </div>
    <input type="hidden" id="hdDeleteID" runat="server" />    
    <input id="btnRetweet" type="button" causesvalidation="false" value="button" style="display: none;"  runat="server" onserverclick="btnRetweet_ServerClick" />

    </form>
</body>
 <script type="text/javascript">

        function Retweet(id) {
             document.getElementById('<%=hdDeleteID.ClientID %>').value = id;
             window.document.all.<%=btnRetweet.ClientID %>.click();
        }

    </script>
</html>
