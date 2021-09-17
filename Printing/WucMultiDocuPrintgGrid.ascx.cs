using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.Security;
using Raj.EC;
using Raj.EC.Security;
using ClassLibraryMVP;

public partial class Printing_WucMultiDocuPrintgGrid : System.Web.UI.UserControl
{
    DataSet objDS = null;
    Common objCommon = new Common();
    private int _count;

    private int GetMenuHeadId
    {
        get{return StateManager.GetState<int>("MenuHeadId");}
    }
    private int MenuItemId
    {
        get { return Util.String2Int(ddl_DocumentType.SelectedValue); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!Page.IsPostBack)
        {
            Session["objDS"] = null; 
            
            Fill_DocumentType();
             
            Link.text = Rights.GetObject().GetLinkDetails(Common.GetMenuItemId()).Link.ToUpper();
 
        }
    }
 

    private void BindGrid()
    {
        objDS = StateManager.GetState<DataSet>("objDS");

        if (objDS != null)
        {
            if (objDS.Tables[0].Columns.Count > 1)
            {
                lbl_Errors.Text = "";   
                DataView objDV = new DataView(objDS.Tables[0]); 
            }
            else
            {
                lbl_Errors.Text = objDS.Tables[0].Rows[0][0].ToString();
            }
        }
    }

    private void Fill_DocumentType()
    {
       objDS = objCommon.Get_Values_Where("EC_Printing", "MenuItem_ID,Document_Name", "Is_Active = 1 and MenuItem_ID in (30,108) and Module_Type = " + GetMenuHeadId.ToString(), "Serial_No", true);

       ddl_DocumentType.DataSource = objDS;
       ddl_DocumentType.DataTextField = "Document_Name";
       ddl_DocumentType.DataValueField = "MenuItem_ID";
       ddl_DocumentType.DataBind();
       //ddl_DocumentType.Items.Insert(0, new ListItem("--- Select Document ---", "0"));
    }

    protected void ddl_DocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int menuitem = Util.String2Int(ddl_DocumentType.SelectedValue);
        Session["objDS"] = null; 
            ddl_DocumentType.Items.Remove(new ListItem("--- Select Document ---","0"));
    }


    protected void btnPrint_Click(object sender, EventArgs e)
    { 
       
        StringBuilder Path = new StringBuilder(Util.GetBaseURL());
        Path.Append("/");

        Path.Append("Reports/Direct_Printing/FrmMultiDocumentCommonRptViewer.aspx?Menu_Item_Id=" + Util.EncryptInteger(MenuItemId)
                    + "&MemoNoForPrint=" + Util.EncryptString(txtMemoNoForPrint.Text));


        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("Open_Print_Window('");
        sb.Append(Path);
        sb.Append("');");
        sb.Append("</script>");
                
        Page.RegisterStartupScript("script", sb.ToString());

    }
}
