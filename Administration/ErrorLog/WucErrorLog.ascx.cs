using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;
public partial class Administration_ErrorLog_WucErrorLog : System.Web.UI.UserControl
{
    public string physicalPath;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillGrid();
        }
    }
    protected void btn_Show_Click(object sender, EventArgs e)
    {       
        FillGrid();
    }
    protected void dg_ErrorLog_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        Image imgTab;        
        System.Web.UI.HtmlControls.HtmlTableCell td_Desc;

        if (e.Item.ItemIndex != -1)
        {
            imgTab = (Image)e.Item.FindControl("imgTab");            
            td_Desc = (System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("td_Desc");
            if (td_Desc.Align.Length > 20)
            {
                td_Desc.NoWrap = true;
            }
            imgTab.Attributes.Add("onclick", "javascript:Toggle('" + dg_ErrorLog.ClientID + "','" + imgTab.ClientID + "','" + td_Desc.ClientID + "')");
            td_Desc.Style.Add("display", "none");

        }
    }
    private void FillGrid()
    {
        DataSet ds=new DataSet();
        lbl_Error.Text = "";
        string _FileName;
        if (rdbl_SelectErrors.SelectedValue == "0")
        {_FileName = "Log";}
        else { _FileName = "DBLog";}
        string _DateName = "";
        physicalPath=HttpContext.Current.Request.PhysicalApplicationPath;
        _DateName=Wuc_Date.SelectedDate.ToString("yyyy-M-d");
        _FileName = physicalPath + "/Error_Log/" + _FileName + _DateName + ".xml";

        FileInfo _file = new FileInfo(_FileName);
        if (_file.Exists)
        {
            ds.ReadXml(_FileName);
        }
        else
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("date");
            dt.Columns.Add("url");
            dt.Columns.Add("time");
            dt.Columns.Add("message");
            ds.Tables.Add(dt);
            if (IsPostBack)
            {
                lbl_Error.Text = "Error Log File Not Exists";
            }
        }
        dg_ErrorLog.DataSource = ds;
        dg_ErrorLog.DataBind();
    }

    
    
}
