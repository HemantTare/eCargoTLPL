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

public partial class TrackNTrace_WucTrackNTraceDeliveryMR : System.Web.UI.UserControl
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
    public DataTable BindDeliveryMRGrid
    {
        set
        {
            Rep_DeliveryMR.DataSource = value;
            Rep_DeliveryMR.DataBind();
        }
    }

    public DataTable BindChequeDetails
    {
        set
        {
            DG_Cheque_Details.DataSource = value;
            DG_Cheque_Details.DataBind();
        }
    }

    #endregion

    public void FillDeliveryMRDetails()
    {
        tr_Error.Visible = false;
        if (Doc_Type == "DLMR")
        {
            fill_MR_Grid();
        }
    }
    #region getValues 

    private void fill_MR_Grid()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0, UserManager.getUserParam().YearCode), 
            objDAL.MakeInParams("@MR_Id", SqlDbType.Int, 0, Doc_No) };

        objDAL.RunProc("dbo.EC_Opr_TrackNTrace_DeliveryMR_Details", objSqlParam, ref objDS);

        BindDeliveryMRGrid = objDS.Tables[0];
        Cancelled = objDS.Tables[0].Rows.Count > 0 ? objDS.Tables[0].Rows[0]["CancelledText"].ToString() : "";

        if (objDS.Tables[0].Rows.Count > 0)
        {
            tr_Error.Visible = false;
        }
        else
        {
            tr_Error.Visible = true;
        }
        if (objDS.Tables[1].Rows.Count > 0)
        {
            tr_cheque_details.Visible = true;
            BindChequeDetails = objDS.Tables[1];
        }
        else
        {
            tr_cheque_details.Visible = false;
            //tr_Error.Visible = true;
        }
    }

    protected void Rep_DeliveryMR_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        LinkButton btn_GC_No, btn_Updated_by;
        Label lbl_GC_No, lbl_GC_Date, lbl_GC_Tot_Amt;
        HiddenField hdn_GC_Id;

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                btn_GC_No = (LinkButton)(e.Item.FindControl("lbtn_GC_No"));
                btn_Updated_by = (LinkButton)(e.Item.FindControl("btn_MR_updated_by"));
                lbl_GC_No = (Label)(e.Item.FindControl("lbl_GC_No"));
                lbl_GC_Date = (Label)(e.Item.FindControl("lbl_GC_Date"));
                lbl_GC_Tot_Amt = (Label)(e.Item.FindControl("lbl_GC_Tot_Amt"));
                hdn_GC_Id = (HiddenField)(e.Item.FindControl("hdn_GC_Id"));

                lbl_GC_No.Text = CompanyManager.getCompanyParam().GcCaption + " No";
                lbl_GC_Date.Text = CompanyManager.getCompanyParam().GcCaption + " Date";
                lbl_GC_Tot_Amt.Text = " Total " + CompanyManager.getCompanyParam().GcCaption + " Amount";

                btn_GC_No.Attributes.Add("onclick", "return viewwindow('GC','" + hdn_GC_Id.Value + "')");
                btn_Updated_by.Attributes.Add("onclick", "return view_userdetails('" + ClassLibraryMVP.Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "MR_UpdatedBy_id").ToString())) + "')");
            }
        }
    }

    #endregion    
}
