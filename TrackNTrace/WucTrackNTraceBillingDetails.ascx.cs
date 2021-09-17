using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;
using Raj.EC;
using ClassLibraryMVP.DataAccess;

public partial class TrackNTrace_WucTrackNTraceBillingDetails : System.Web.UI.UserControl
{
    private DAL objDAL = new DAL();
    private DataSet objDS;
    public Label lbl_cancelled;

    string Document_Type;
    int Document_No;

    #region ControlsValues

    public int Doc_No
    {
        get { return Document_No; }
        set { Document_No = value; }
    }

    public string Doc_Type
    {
        get { return Document_Type; }
        set { Document_Type = value; }
    }

    public string Cancelled
    {
        set { lbl_cancelled.Text = value; }
    }

    public DataTable BindBillingGrid
    {
        set
        {
            Rep_BillDetails.DataSource = value;
            Rep_BillDetails.DataBind();
        }
    }

    public DataTable BindGCDetails
    {
        set
        {
            DG_GC_Details.DataSource = value;
            DG_GC_Details.DataBind();
        }
    }

    #endregion

    public void FillBillingDetails()
    {
        tr_Error.Visible = false;
        if (Doc_Type == "BILL")
        {
            fill_Bill_Grid();
        }
    }

    #region getValues

    private void fill_Bill_Grid()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0, UserManager.getUserParam().YearCode), 
            objDAL.MakeInParams("@Bill_Id", SqlDbType.Int,0, Doc_No) };

        objDAL.RunProc("dbo.EC_Opr_TrackNTrace_Billing_Details", objSqlParam, ref objDS);

        BindBillingGrid = objDS.Tables[0];
        Cancelled = objDS.Tables[0].Rows.Count > 0 ? objDS.Tables[0].Rows[0]["CancelledText"].ToString() : "";

        DG_GC_Details.Columns[0].HeaderText = CompanyManager.getCompanyParam().GcCaption + " No";
        lbl_GC_Details.Text = CompanyManager.getCompanyParam().GcCaption.ToUpper() + " DETAILS";

        if (objDS.Tables[0].Rows.Count > 0)
        {
            if (Util.String2Int(objDS.Tables[0].Rows[0]["bill_type_id"].ToString()) == 1)
            {
                DG_GC_Details.Columns[6].Visible = false;
                DG_GC_Details.Columns[7].Visible = false;
                DG_GC_Details.Columns[9].Visible = false;
                DG_GC_Details.Columns[4].Visible = true;
                DG_GC_Details.Columns[5].Visible = true;
            }
            else if (Util.String2Int(objDS.Tables[0].Rows[0]["bill_type_id"].ToString()) == 2)
            {
                DG_GC_Details.Columns[4].Visible = false;
                DG_GC_Details.Columns[5].Visible = false;
                DG_GC_Details.Columns[6].Visible = true;
                DG_GC_Details.Columns[7].Visible = true;
                DG_GC_Details.Columns[9].Visible = true;
            }
            else
            {
                DG_GC_Details.Columns[4].Visible = true;
                DG_GC_Details.Columns[5].Visible = true;
                DG_GC_Details.Columns[6].Visible = true;
                DG_GC_Details.Columns[7].Visible = true;
                DG_GC_Details.Columns[9].Visible = true;
            }
        }
        if (objDS.Tables[1].Rows.Count > 0)
        {
            tr_Gc_Details.Visible = true;
            BindGCDetails = objDS.Tables[1];
        }
        else
        {
            tr_Gc_Details.Visible = false;
            tr_Error.Visible = true;
        }
    }

    protected void Rep_BillDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        LinkButton btn_Updated_by;
        Label lbl_tot_Gc;
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                btn_Updated_by = (LinkButton)(e.Item.FindControl("btn_Bill_updated_by"));
                lbl_tot_Gc = (Label)(e.Item.FindControl("lbl_GC_Tot"));

                lbl_tot_Gc.Text = "Total " + CompanyManager.getCompanyParam().GcCaption;

                btn_Updated_by.Attributes.Add("onclick", "return view_userdetails('" + ClassLibraryMVP.Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Bill_UpdatedBy_id").ToString())) + "')");
            }
        }
    }

    protected void DG_GC_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        LinkButton btn_GC_No, btn_OtherCharge;
        string OChargeUrl;

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                btn_GC_No = (LinkButton)(e.Item.FindControl("btn_GC_No"));
                btn_OtherCharge = (LinkButton)(e.Item.FindControl("btn_OtherCharges"));

                OChargeUrl = ClassLibraryMVP.Util.GetBaseURL() +
                         "/TrackNTrace/frmTNTBillingOtherChargeDetails.aspx?Bill_Id=" +
                         ClassLibraryMVP.Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Bill_Id").ToString())) +
                         "&GC_ID=" + ClassLibraryMVP.Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "GC_Id").ToString()));

                btn_GC_No.Attributes.Add("onclick", "return viewwindow('GC','" + DataBinder.Eval(e.Item.DataItem, "GC_ID").ToString() + "')");

                if (Util.String2Decimal(btn_OtherCharge.Text) > 0)
                    btn_OtherCharge.Attributes.Add("onclick", "return view_otherchargedetails('" + OChargeUrl + "')");
                else
                    btn_OtherCharge.Enabled = false;
            }
        }
    }

    #endregion
   
}
