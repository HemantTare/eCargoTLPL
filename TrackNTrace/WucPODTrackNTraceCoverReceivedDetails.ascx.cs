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
using ClassLibraryMVP;
using Raj.EC;
using ClassLibraryMVP.DataAccess;
using System.Data.SqlClient;

public partial class TrackNTrace_WucPODTrackNTraceCoverReceivedDetails : System.Web.UI.UserControl
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

    public string Cancelled
    {
        set { lbl_cancelled.Text = value; }
    }

    public string Doc_Type
    {
        get { return Document_Type; }
        set { Document_Type = value; }
    }

    public DataTable BindGCDetailsGrid
    {
        set
        {
            dg_GC_Details.DataSource = value;
            dg_GC_Details.DataBind();
        }
    }

    public DataTable BindCRMasterDetails
    {
        set
        {
            Rep_PODCRMasterDetails.DataSource = value;
            Rep_PODCRMasterDetails.DataBind();
        }
    }

    #endregion

    public void FillPODCoverReceivedDetails()
    {
        tr_Error.Visible = false;
        if (Doc_Type == "PODCR")
        {
            Fill_PODCoverRecvd_Grid();
        }
    }
    #region getValues

    private void Fill_PODCoverRecvd_Grid()
    {
        SqlParameter[] objSqlParam ={ 
                        objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0, UserManager.getUserParam().YearCode), 
                        objDAL.MakeInParams("@Cover_Id", SqlDbType.Int, 0, Doc_No) };

        objDAL.RunProc("dbo.EC_Opr_TrackNTrace_PODCoverReceived_Details", objSqlParam, ref objDS);

        dg_GC_Details.Columns[0].HeaderText = CompanyManager.getCompanyParam().GcCaption + " No";
        dg_GC_Details.Columns[1].HeaderText = CompanyManager.getCompanyParam().GcCaption + " Date";

        if (objDS.Tables[0].Rows.Count > 0)
        {
            tr_att_PODCR_Details.Visible = true;
            BindCRMasterDetails = objDS.Tables[0];
        }
        else
        {
            tr_att_PODCR_Details.Visible = false;
            tr_Error.Visible = true;
        }

        Cancelled = objDS.Tables[0].Rows.Count > 0 ? objDS.Tables[0].Rows[0]["CancelledText"].ToString() : "";

        if (objDS.Tables[1].Rows.Count > 0)
        {
            tr_GC_Details.Visible = true;
            BindGCDetailsGrid = objDS.Tables[1];
        }
        else
        {
            tr_GC_Details.Visible = false;
        }
    }
    #endregion

    protected void Rep_PODCRMasterDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        LinkButton lnk_CG_Updated_By, lnk_CR_Updated_By, lnk_CG_No;
        HiddenField hdn_Cover_Id;

        if (e.Item.ItemIndex != -1)
        {
            if (e.Item.ItemIndex == 0)
            {
                lnk_CG_Updated_By = (LinkButton)(e.Item.FindControl("lnk_CG_Updated_By"));
                lnk_CR_Updated_By = (LinkButton)(e.Item.FindControl("lnk_CR_Updated_By"));
                lnk_CG_No = (LinkButton)(e.Item.FindControl("lnk_CG_No"));
                hdn_Cover_Id = (HiddenField)(e.Item.FindControl("hdn_Cover_Id"));

                lnk_CG_Updated_By.Attributes.Add("onclick", "return view_userdetails('" + Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "CG_Updated_By_ID").ToString())) + "')");
                lnk_CR_Updated_By.Attributes.Add("onclick", "return view_userdetails('" + Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "CR_Updated_By_ID").ToString())) + "')");
                lnk_CG_No.Attributes.Add("onclick", "return viewwindow('PODCG','" + hdn_Cover_Id.Value + "')");
            }
        }
    }
    protected void dg_GC_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        LinkButton lnk_GC_No;
        HiddenField hdn_GC_Id;

        if (e.Item.ItemIndex != -1)
        {
            if (e.Item.ItemIndex == 0)
            {
                lnk_GC_No = (LinkButton)(e.Item.FindControl("lnk_GC_No"));
                hdn_GC_Id = (HiddenField)(e.Item.FindControl("hdn_GC_Id"));

                lnk_GC_No.Attributes.Add("onclick", "return viewwindow('GC','" + hdn_GC_Id.Value + "')");
            }
        }
    }   
}
