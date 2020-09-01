<% @ Control Language="C#" ClassName="UserControl1" %>
<script runat="server">
    protected int currentColorIndex;
    protected String[] colors = {"Red", "Blue", "Green", "Yellow"};
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            currentColorIndex = 
                Int16.Parse(Session["currentColorIndex"].ToString());
        }
        else
        {
            currentColorIndex = 0;//赋给默认值。
            DisplayColor();
        }
    }
    
    protected void DisplayColor()
    {
        textColor.Text = colors[currentColorIndex];
        Session["currentColorIndex"] = currentColorIndex.ToString();
        Session["color"] = colors[currentColorIndex];//直接将枚举出来的颜色名赋给session，以备调用。
    }
    
    protected void buttonUp_Click(object sender, EventArgs e)
    {
        if(currentColorIndex == 0)
        {
            currentColorIndex = colors.Length - 1;
        }
        else
        {
            currentColorIndex -= 1;
        }
        DisplayColor();
    }

    protected void buttonDown_Click(object sender, EventArgs e)
    {
        if(currentColorIndex == (colors.Length - 1))
        {
            currentColorIndex = 0;
        }    
        else
        {
            currentColorIndex += 1;
        }
        DisplayColor();
    }
</script>
<asp:TextBox ID="textColor" runat="server" 
    ReadOnly="True" />
<asp:Button Font-Bold="True" ID="buttonUp" runat="server" 
    Text="^" OnClick="buttonUp_Click" />
<asp:Button Font-Bold="True" ID="buttonDown" runat="server" 
    Text="v" OnClick="buttonDown_Click" />

