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

public partial class TrackNTrace_WucTrackNTraceAUSDetails : System.Web.UI.UserControl
{
    private DAL objDAL = new DAL();
    private DataSet objDS;
    string Document_Type;
    int Document_No;
    public Label lbl_cancelled;

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

    public DataTable BindAUSGrid
    {
        set
        {
            DG_GC_AUS_Details.DataSource = value;
            DG_GC_AUS_Details.DataBind();
        }
    }

    public DataTable BindAUSMasterDetails
    {
        set
        {
            Rep_AUSDetails.DataSource = value;
            Rep_AUSDetails.DataBind();
        }
    }

    #endregion

    public void FillAUSDetails()
    {
        tr_Error.Visible = false;
        if (Doc_Type == "AUS")
        {
            fill_AUS_Grid();
        }
    }
    #region getValues    

    private void fill_AUS_Grid()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0, UserManager.getUserParam().YearCode) ,
        objDAL.MakeInParams("@AUS_Id", SqlDbType.Int, 0, Doc_No)};

        objDAL.RunProc("dbo.EC_Opr_TrackNTrace_AUS_Details", objSqlParam, ref objDS);

        DG_GC_AUS_Details.Columns[1].HeaderText = CompanyManager.getCompanyParam().GcCaption + " No";

        BindAUSMasterDetails = objDS.Tables[0];
        Cancelled = objDS.Tables[0].Rows.Count > 0 ? objDS.Tables[0].Rows[0]["CancelledText"].ToString() : "";
        
        if (objDS.Tables[1].Rows.Count > 0)
        {
            tr_Aus_Details.Visible = true;
            lbl_AUS_Heading.Text = objDS.Tables[1].Rows[0]["AUSCaption"].ToString() + " DETAILS";

            BindAUSGrid = objDS.Tables[1];
        }
        else
        {
            tr_Aus_Details.Visible = false;
            tr_Error.Visible = true;
        }
    }


    protected void DG_GC_AUS_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        LinkButton btn_Memo_No, btn_GC_No, btn_MEMO_UpdatedBy;
        HiddenField hdn_GC_Id, hdn_Memo_Id;

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                btn_Memo_No = (LinkButton)(e.Item.FindControl("btn_Memo_No"));
                btn_GC_No = (LinkButton)(e.Item.FindControl("btn_GC_No"));
                btn_MEMO_UpdatedBy = (LinkButton)(e.Item.FindControl("btn_MEMO_UpdatedBy"));
                hdn_GC_Id = (HiddenField)(e.Item.FindControl("hdn_GC_Id"));
                hdn_Memo_Id = (HiddenField)(e.Item.FindControl("hdn_Memo_Id"));

                btn_Memo_No.Attributes.Add("onclick", "return viewwindow('MEMO','" + hdn_Memo_Id.Value + "')");
                btn_GC_No.Attributes.Add("onclick", "return viewwindow('GC','" + hdn_GC_Id.Value + "')");
                btn_MEMO_UpdatedBy.Attributes.Add("onclick", "return view_userdetails('" + ClassLibraryMVP.Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Memo_UpdatedBy_id").ToString())) + "')");
            }
        }
    }

 #endregion    
    protected void Rep_AUSDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        LinkButton lbtn_Updatedby, lbtn_LHPO_No;
        Label lbl_Tot_GC, lbl_LHPO_No, lbl_LHPO_Date;
        HiddenField hdn_LHPO_Id;

        if (e.Item.ItemIndex != -1)
        {
            if (e.Item.ItemIndex == 0)
            {
                lbtn_LHPO_No = (LinkButton)(e.Item.FindControl("lbtn_LHPO_No"));
                lbtn_Updatedby = (LinkButton)(e.Item.FindControl("btn_AUS_updated_by"));
                lbl_Tot_GC = (Label)(e.Item.FindControl("lbl_Tot_GC"));
                lbl_LHPO_No = (Label)(e.Item.FindControl("lbl_LHPO_No"));
                lbl_LHPO_Date = (Label)(e.Item.FindControl("lbl_LHPO_Date"));
                hdn_LHPO_Id = (HiddenField)(e.Item.FindControl("hdn_LHPO_Id"));

                lbl_Tot_GC.Text ="Total Actual "+ CompanyManager.getCompanyParam().GcCaption;
                lbl_LHPO_No.Text = CompanyManager.getCompanyParam().LHPOCaption + " No";
                lbl_LHPO_Date.Text = CompanyManager.getCompanyParam().LHPOCaption + " Date";

                lbtn_LHPO_No.Attributes.Add("onclick", "return viewwindow('LHPO','" + hdn_LHPO_Id.Value + "')");
                lbtn_Updatedby.Attributes.Add("onclick", "return view_userdetails('" + ClassLibraryMVP.Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "AUS_UpdatedBy_id").ToString())) + "')");
            }
        }
    }
}
