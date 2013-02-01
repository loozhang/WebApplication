//Add By Zhang LiJia

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel;
using BLL;
using System.Web.Script.Serialization;

namespace Utility
{
    /// <summary>
    /// 多下拉框选项(需要引入jquery-1.3.2.min.js，jquery.select.js, 并用到微软的Ajax库)
    /// </summary>
    // [DesignerAttribute(typeof(ShowAreaDesigner))]
    public class SelectArea : WebControl, INamingContainer
    {

        private string _SpaceText = "==请选择==";
        ///<summary>
        ///</summary>
        public string SpaceText { get { return this._SpaceText; } set { this._SpaceText = value; } }

        private bool _IsAddSpace = true;
        ///<summary>
        ///</summary>
        public bool IsAddSpace { get { return this._IsAddSpace; } set { this._IsAddSpace = value; } }

        /// <summary>
        /// 根区域ID
        /// </summary>
        //private int _RootAreaID = AreaIDSHelper.AreaID_CH;
        public int RootAreaID
        {
            get
            {
                if (this.ViewState["RootAreaID"] == null)
                {
                    this.ViewState["RootAreaID"] = AreaIDSHelper.AreaID_CH;
                }
                return (int)this.ViewState["RootAreaID"];
            }
            set
            {
                var t = AreaBLL.GetInfo(value);
                if (t == null)
                {
                    this.ViewState["RootAreaID"] = AreaIDSHelper.AreaID_CH;
                }
                else
                {
                    this.ViewState["RootAreaID"] = value;
                }
            }
        }

        /// <summary>
        /// 显示几层显示几级下拉框
        /// </summary>
        private int _Level = 10;
        ///<summary>
        ///</summary>
        public int Level
        {
            get { return this._Level; }
            set { this._Level = value; }
        }

        private bool isFirst = true;
        private int? _AreaID;
        /// <summary>
        /// 当前区域ID
        /// </summary>
        public int? AreaID
        {
            get
            {
                if (isFirst)
                {
                    string val = Context.Request.Form[hidId];
                    if (!string.IsNullOrEmpty(val))
                    {
                        _AreaID = int.Parse(val);
                    }
                    else if (val == "-1")
                    {
                        _AreaID = null;
                    }
                    else if (!IsAddSpace)
                    {
                        if (AllArea.Sons.Count > 0)
                        {
                            _AreaID = AllArea.Sons[0].AreaID;
                        }
                    }
                    isFirst = false;
                }
                return _AreaID;
            }
            set
            {
                if (value <= 0)
                {
                    _AreaID = null;
                }
                else
                {
                    if (value == null)
                    {
                        _AreaID = null;
                        return;
                    }

                    var t = AreaBLL.GetInfo(value.Value);
                    if (t == null)
                    {
                        _AreaID = null;
                        return;
                    }

                    if (!AreaBLL.IsFatherOrUp(RootAreaID, value.Value))
                    {
                        _AreaID = null;
                        return;
                    }

                    _AreaID = value;

                }
            }
        }

        /// <summary>
        /// 当前区域ID
        /// </summary>
        public int AreaUseID
        {
            get
            {
                return AreaID == null ? -1 : AreaID.Value;
            }
            set
            {
                do
                {
                    this.AreaID = value;
                    if (value == -1)
                        return;

                } while (this.AreaID != value);

            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            Type type = typeof(SelectArea);
            string url = Page.ClientScript.GetWebResourceUrl(type, "Utility.SelectArea.js");
            if (!Page.ClientScript.IsClientScriptIncludeRegistered(type, type.FullName))
            {
                Page.ClientScript.RegisterClientScriptInclude(type, type.FullName, url);
            }
            base.OnPreRender(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"<input type='hidden' id='{0}' name='{0}' value='{1}' />", this.hidId, this.AreaUseID);
            sb.Append("<style type='text/css'> select {  display:inline; } </style>");
            sb.Append("<script language='javascript'>");
            sb.Append("$(document).ready(function() {");
            string datasName = this.ID + "datas";
            sb.Append("var " + datasName + " = " + this.GetJavaScriptSerializer(this.AllArea) + ";");
            sb.AppendFormat(@"SelectAreaObjs['{0}'] = new SelectAreaObj('{1}', {2}, '{3}', {4}, {5});", this.ClientID, this.ClientID, this.IsAddSpace.ToString().ToLower(), this.SpaceText, this.RootAreaID, datasName);
            sb.Append("});");

            sb.Append("</script>");

            writer.Write(sb.ToString());
        }

        private string hidId
        {
            get
            {
                return String.Format(@"_hidden{0}", this.ClientID);
            }
        }


        #region AreaSmall
        ///<summary>
        ///</summary>
        [Serializable]
        public class AreaSmall
        {
            ///<summary>
            ///</summary>
            public AreaSmall()
            {
                this.Sons = new List<AreaSmall>();
            }
            ///<summary>
            ///</summary>
            public int AreaID { get; set; }
            ///<summary>
            ///</summary>
            public string AreaName { get; set; }
            ///<summary>
            ///</summary>
            public int ParentID { get; set; }
            ///<summary>
            ///</summary>
            public List<AreaSmall> Sons { get; set; }
        }

        private AreaSmall AllArea
        {
            get
            {
                if (this.ViewState["AllArea"] == null)
                {
                    var allArea = AreaBLL.TreeByAreaID(this.RootAreaID);
                    var entity = new AreaSmall();
                    entity.AreaID = this.RootAreaID;
                    entity.AreaName = AreaBLL.GetAreaName(this.RootAreaID, true);
                    entity.ParentID = AreaBLL.GetInfo(this.RootAreaID, true).ParentAreaID;
                    SmallAreaLoad(allArea, entity, 0);
                    this.ViewState["AllArea"] = entity;
                    return entity;
                }
                return (AreaSmall)this.ViewState["AllArea"];
            }
        }

        private void SmallAreaLoad(IList<AreaInfo> areas, AreaSmall father, int level)
        {
            if (level < this.Level)
            {
                var areaSons = areas.Where(f => f.ParentAreaID == father.AreaID).ToList();

                foreach (AreaInfo areaInfo in areaSons)
                {
                    var entity = new AreaSmall();
                    entity.AreaID = areaInfo.AreaID;
                    entity.AreaName = areaInfo.AreaName;
                    entity.ParentID = areaInfo.ParentAreaID;
                    father.Sons.Add(entity);
                    SmallAreaLoad(areas, entity, level + 1);
                }
            }
        }

        private string GetJavaScriptSerializer(object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }
        #endregion

        //public class ShowAreaDesigner : ControlDesigner
        //{
        //    public ShowAreaDesigner()
        //        : base()
        //    {
        //    }

        //    protected override string GetEmptyDesignTimeHtml()
        //    {
        //        return "<select><option>区域</option></select>";

        //    }


        //}



    }


}
