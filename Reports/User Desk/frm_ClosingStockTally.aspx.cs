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
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP;
using Raj.EC;
using ClassLibraryMVP.Security;

public partial class Reports_User_Desk_frm_ClosingStockTally : System.Web.UI.Page
{
    string _GC_No_XML;
    private DAL objDAL = new DAL();
    private DataSet objDS = new DataSet();
    
    public String GetGCXML
    {
        get
        {
            if (_GC_No_XML != null)
            {
                return _GC_No_XML.ToString().ToLower();
            }
            else
            {
                return "<NewDataSet/>";
            }
        }
        set { _GC_No_XML = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        WucSelectedItems1.GetSelectedItemsXMLButtonClick += new EventHandler(OnGetGCXML);
        SetStandardCaption();
    }
    private void SetStandardCaption()
    {
        WucSelectedItems1.SetFoundCaption = "Enter  " + CompanyManager.getCompanyParam().GcCaption + "  Nos.:";
        WucSelectedItems1.SetNotFoundCaption = CompanyManager.getCompanyParam().GcCaption + "  Nos.Not Found :";
        WucSelectedItems1.Set_GCCaption = CompanyManager.getCompanyParam().GcCaption;
    }
    private void OnGetGCXML(object sender, EventArgs e)
    {
        DataSet objDS = new DataSet();
        _GC_No_XML = WucSelectedItems1.GetSelectedItemsXML;

        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@GCXML", SqlDbType.Xml, 0, GetGCXML) };
        objDAL.RunProc("dbo.EC_Opr_Closing_Stock_Tally_Details", objSqlParam, ref objDS);

        if (objDS.Tables[0].Rows.Count > 0)
        {
            dg_Crossingstocktally.DataSource = objDS.Tables[0];
            dg_Crossingstocktally.DataBind();

            WucSelectedItems1.dtdetails = objDS.Tables[0];
            pnl_DlyBranch.Visible = true;
        }
        else
            pnl_DlyBranch.Visible = false;

        WucSelectedItems1.Get_Not_Selected_Items();
    }
    protected void dg_Crossingstocktally_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        LinkButton btn_LR_No, btn_Consignee, btn_CurrentStatus;
        HiddenField hdn_GC_Id, hdn_Consignee_Client_ID, hdn_Is_Consignee_Regular_Client, hdn_Status_ID;
        int Memo_Type_Id;
        string Delivery_Type_Id;

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                btn_LR_No = (LinkButton)(e.Item.FindControl("btn_LR_No"));
                btn_Consignee = (LinkButton)(e.Item.FindControl("btn_Consignee"));
                btn_CurrentStatus = (LinkButton)(e.Item.FindControl("btn_CurrentStatus"));

                hdn_GC_Id = (HiddenField)(e.Item.FindControl("hdn_GC_Id"));
                hdn_Consignee_Client_ID = (HiddenField)(e.Item.FindControl("hdn_Consignee_Client_ID"));
                hdn_Is_Consignee_Regular_Client = (HiddenField)(e.Item.FindControl("hdn_Is_Consignee_Regular_Client"));
                hdn_Status_ID = (HiddenField)(e.Item.FindControl("hdn_Status_ID"));

                int gc_id = Util.String2Int(hdn_GC_Id.Value);
                string path = Util.GetBaseURL() + "/" + Rights.GetObject().GetLinkDetails(30).ViewUrl + "&Id=" + ClassLibraryMVP.Util.EncryptInteger(gc_id);
                btn_LR_No.Attributes.Add("onclick", "return viewwindow_ForGC('" + path + "')");

                btn_CurrentStatus.Attributes.Add("onclick", "return viewwindow_TANDT('GC','" + hdn_GC_Id.Value + "')");

                int client_id = Util.String2Int(hdn_Consignee_Client_ID.Value);

                if (Util.String2Bool(hdn_Is_Consignee_Regular_Client.Value))
                    btn_Consignee.Attributes.Add("onclick", "return viewwindow_ClientWalkIn('" + ClassLibraryMVP.Util.EncryptInteger(client_id) + "')");
                else
                    btn_Consignee.Attributes.Add("onclick", "return viewwindow_ClientRegular('" + ClassLibraryMVP.Util.EncryptInteger(client_id) + "')");

            }
        }
    }
}
