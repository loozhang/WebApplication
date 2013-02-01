using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace Common
{

    // Attribute DefaultProperty指定组件的默认属性，ToolboxData指定当从IDE工具中的

    //工具箱中拖动自定义控件时为它生成的默认标记

    [DefaultProperty("Text"),

    ToolboxData("<{0}:MyControl runat=server></{0}:MyControl>")]

    //类MyControl派生自WebControl

    public class MyControl : System.Web.UI.WebControls.WebControl
    {

        private string text;

        //Attribute Bindable指定属性是否通常用于绑定

        //Category指定属性或事件将显示在可视化设计器中的类别

        //DefalutValue用于指定属性的默认值

        [Bindable(true),

        Category("Appearance"),

        DefaultValue("")]

        public string Text
        {

            get
            {

                return text;

            }

            set
            {

                text = value;

            }

        }

        //重写WebControl的Render方法，采用HtmlTextWriter类型的参数

        protected override void Render(HtmlTextWriter output)
        {

            //发送属性Text的值到浏览器

            output.Write("<span><b>" + "Text" + "</b></span>");//最后页面显示的是Text。


        }

    }

}

