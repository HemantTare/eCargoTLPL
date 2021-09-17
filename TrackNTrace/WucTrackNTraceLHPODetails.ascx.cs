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

public partial class TrackNTrace_WucTrackNTraceLHPODetails : System.Web.UI.UserControl
{
    private DAL objDAL = new DAL();
    private DataSet objDS;
    public Label lbl_cancelled;
    string  Document_Type;
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
    public DataTable BindLHPOGrid
    {
        set
        {
            DG_GC_LHPO_Details.DataSource = value;
            DG_GC_LHPO_Details.DataBind();
        }
    }

    public DataTable BindATHGrid
    {
        set
        {
            DG_GC_ATH_Details.DataSource = value;
            DG_GC_ATH_Details.DataBind();
        }
    }

    public DataTable BindLHPOMasterDetails
    {
        set
        {
            Rep_LHPODetails.DataSource = value;
            Rep_LHPODetails.DataBind();
        }
    }

    #endregion

    public void FillLHPODetails()
    {
        tr_Error.Visible = false;
        if (Doc_Type == "LHPO")
        {
            fill_LHPO_Grid();
            lbl_LHPO_Heading.Text = CompanyManager.getCompanyParam().LHPOCaption + " DETAILS";
            lbl_ATH_Heading.Text = "ATH DETAILS";
        }
    }

    #region getValues

    private void fill_LHPO_Grid()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0, UserManager.getUserParam().YearCode), 
            objDAL.MakeInParams("@LHPO_Id", SqlDbType.Int,0, Doc_No) };

        objDAL.RunProc("dbo.EC_Opr_TrackNTrace_LHPO_Details", objSqlParam, ref objDS);

        BindLHPOMasterDetails = objDS.Tables[0];
        Cancelled = objDS.Tables[0].Rows.Count > 0 ? objDS.Tables[0].Rows[0]["CancelledText"].ToString() : "";       
        
        if (objDS.Tables[1].Rows.Count > 0)
        {
            tr_LHPO_Details.Visible = true;
            DG_GC_LHPO_Details.Columns[4].HeaderText = objDS.Tables[1].Rows[0]["AUSCaption"].ToString() + "/DDC No";
            DG_GC_LHPO_Details.Columns[6].HeaderText = objDS.Tables[1].Rows[0]["AUSCaption"].ToString() + "/DDC Date";

            BindLHPOGrid = objDS.Tables[1];
        }
        else
        {
            tr_LHPO_Details.Visible = false;
            tr_Error.Visible = true;
        }

        if (objDS.Tables[2].Rows.Count > 0)
        {
            tr_ATH_Details.Visible = true;
            BindATHGrid = objDS.Tables[2];
        }
        else
        {
            tr_ATH_Details.Visible = false;
            tr_Error.Visible = true;
        }
    }

    protected void DG_GC_LHPO_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        LinkButton btn_Memo_No, btn_AUS_DDC_No, btn_Memo_UpdatedBy;
        HiddenField hdn_Memo_Id, hdn_AUSDDC_Id;
        int Memo_Type_Id;
        string Delivery_Type_Id;

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                btn_Memo_No = (LinkButton)(e.Item.FindControl("btn_Memo_No"));
                btn_AUS_DDC_No = (LinkButton)(e.Item.FindControl("btn_AUS_DDC_No"));
                btn_Memo_UpdatedBy = (LinkButton)(e.Item.FindControl("btn_Memo_UpdatedBy"));
                hdn_Memo_Id = (HiddenField)(e.Item.FindControl("hdn_Memo_Id"));
                hdn_AUSDDC_Id = (HiddenField)(e.Item.FindControl("hdn_AUSDDC_Id"));

                Memo_Type_Id = Util.String2Int(((HiddenField)(e.Item.FindControl("hdn_Memo_Type_Id"))).Value);
                Delivery_Type_Id = ((HiddenField)(e.Item.FindControl("hdn_DelType_Id"))).Value;

                btn_Memo_No.Attributes.Add("onclick", "return viewwindow('MEMO','" + hdn_Memo_Id.Value + "')");
                btn_Memo_UpdatedBy.Attributes.Add("onclick", "return view_userdetails('" + ClassLibraryMVP.Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "memo_UpdatedBy_id").ToString())) + "')");

                if (Memo_Type_Id == 1)
                {
                    btn_AUS_DDC_No.Attributes.Add("onclick", "return viewwindow('AUS','" + hdn_AUSDDC_Id.Value + "')");
                }
                else if (Memo_Type_Id == 2)
                {
                    btn_AUS_DDC_No.Attributes.Add("onclick", "return viewwindow_ddc('" + Delivery_Type_Id + "','" + hdn_AUSDDC_Id.Value + "')");
                }
            }
        }
    }

  #endregion
    protected void Rep_LHPODetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        LinkButton lbtn_Updatedby;
        Label lbl_LHPO_No, lbl_LHPO_Date, lbl_LHPO_Type, lbl_LHPO_Branch;
        Label lbl_LHPO_From, lbl_LHPO_To, lbl_Total_GC, lbl_LHPO_UpdatedBy;

        if (e.Item.ItemIndex != -1)
        {
            if (e.Item.ItemIndex == 0)
            {
                lbtn_Updatedby = (LinkButton)(e.Item.FindControl("btn_LHPO_Updated_by"));
                lbl_LHPO_No = (Label)(e.Item.FindControl("lbl_LHPO_No"));
                lbl_LHPO_Date = (Label)(e.Item.FindControl("lbl_LHPO_Date"));
                lbl_LHPO_Type = (Label)(e.Item.FindControl("lbl_LHPO_Type"));
                lbl_LHPO_Branch = (Label)(e.Item.FindControl("lbl_LHPO_Branch"));
                lbl_LHPO_From = (Label)(e.Item.FindControl("lbl_LHPO_From"));
                lbl_LHPO_To = (Label)(e.Item.FindControl("lbl_LHPO_To"));
                lbl_Total_GC = (Label)(e.Item.FindControl("lbl_Total_GC"));
                lbl_LHPO_UpdatedBy = (Label)(e.Item.FindControl("lbl_LHPO_UpdatedBy"));

                lbl_LHPO_No.Text = CompanyManager.getCompanyParam().LHPOCaption + " No";
                lbl_LHPO_Date.Text = CompanyManager.getCompanyParam().LHPOCaption + " Date";
                lbl_LHPO_Type.Text = CompanyManager.getCompanyParam().LHPOCaption + " Type";
                lbl_LHPO_Branch.Text = CompanyManager.getCompanyParam().LHPOCaption + " Branch";
                lbl_LHPO_From.Text = CompanyManager.getCompanyParam().LHPOCaption + " From";
                lbl_LHPO_To.Text = CompanyManager.getCompanyParam().LHPOCaption + " To";
                lbl_Total_GC.Text = "Total No Of " + CompanyManager.getCompanyParam().GcCaption;
                lbl_LHPO_UpdatedBy.Text = CompanyManager.getCompanyParam().LHPOCaption + " Updated By";

                lbtn_Updatedby.Attributes.Add("onclick", "return view_userdetails('" + ClassLibraryMVP.Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "LHPO_UpdatedBy_Id").ToString())) + "')");
            }
        }
    }
    protected void DG_GC_ATH_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        LinkButton ATH_UpdatedBy_id;
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ATH_UpdatedBy_id = (LinkButton)(e.Item.FindControl("ATH_UpdatedBy_id"));
            ATH_UpdatedBy_id.Attributes.Add("onclick", "return view_userdetails('" + ClassLibraryMVP.Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "ATH_UpdatedBy_id").ToString())) + "')");

        }
    }
}
