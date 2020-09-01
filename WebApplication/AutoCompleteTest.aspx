<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutoCompleteTest.aspx.cs" Inherits="WebApplication3.AutoCompleteTest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" >
    <title>Ajax AutoComplete</title>
    <style type="text/css">
    .autocomplete_completionListElement 
{ 
visibility : hidden;
margin : 0px!important;
background-color : inherit;
color : windowtext;
border : buttonshadow;
border-width : 1px;
border-style : solid;
cursor : 'default';
overflow : auto;
height : 200px;
    text-align : left; 
    list-style-type : none;
}

/* AutoComplete highlighted item */

.autocomplete_highlightedListItem
{
background-color: #ffff99;
color: black;
padding: 1px;
}

/* AutoComplete item */

.autocomplete_listItem 
{
background-color : window;
color : windowtext;
padding : 1px;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
    </div>
    <asp:TextBox ID="TextBox1" runat="server" Width="250px" autocomplete="true"></asp:TextBox>
    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1"
                runat="server"
                BehaviorID="AutoCompleteEx"
                TargetControlID="TextBox1"   
                ServicePath="Services/AutoComplete.asmx" 
                ServiceMethod="GetCompletionList"
                MinimumPrefixLength="1"  
                EnableCaching="true"    
                CompletionSetCount="10"       
                CompletionListCssClass="autocomplete_completionListElement" 
                CompletionListItemCssClass="autocomplete_listItem" 
                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                DelimiterCharacters=";, :">      
                <Animations>
                    <OnShow>
                        <Sequence>
                            <%-- Make the completion list transparent and then show it --%>
                            <OpacityAction Opacity="0" />
                            <HideAction Visible="true" />
                            
                            <%--Cache the original size of the completion list the first time
                                the animation is played and then set it to zero --%>
                            <ScriptAction Script="
                                // Cache the size and setup the initial size
                                var behavior = $find('AutoCompleteEx');
                                if (!behavior._height) {
                                    var target = behavior.get_completionList();
                                    behavior._height = target.offsetHeight - 2;
                                    target.style.height = '0px';
                                }" />
                            
                            <%-- Expand from 0px to the appropriate size while fading in --%>
                            <Parallel Duration=".4">
                                <FadeIn />
                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx')._height" />
                            </Parallel>
                        </Sequence>
                    </OnShow>
                    <OnHide>
                        <%-- Collapse down to 0px and fade out --%>
                        <Parallel Duration=".4">
                            <FadeOut />
                            <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx')._height" EndValue="0" />
                        </Parallel>
                    </OnHide>
                </Animations>
            </ajaxToolkit:AutoCompleteExtender>
    </form>
</body>
</html>


