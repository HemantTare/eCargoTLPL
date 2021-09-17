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

public partial class TrackNTrace_WucTrackNTraceMemoDetails : System.Web.UI.UserControl
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

    public DataTable BindMemoGrid
    {
        set
        {
            DG_GC_Memo_Details.DataSource = value;
            DG_GC_Memo_Details.DataBind();
        }
    }

    public DataTable BindMemoMasterDetails
    {
        set
        {
            Rep_MemoDetails.DataSource = value;
            Rep_MemoDetails.DataBind();
        }
    }
   
    #endregion
    
    public void FillMemoDetails()
    {
        tr_Error.Visible = false;
        if (Doc_Type == "MEMO")
        {
            fill_Memo_Grid();
        }
    }

    #region getValues

    private void fill_Memo_Grid()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0, UserManager.getUserParam().YearCode), 
            objDAL.MakeInParams("@Memo_Id", SqlDbType.Int, 0, Doc_No) };

        objDAL.RunProc("dbo.EC_Opr_TrackNTrace_Memo_Details", objSqlParam, ref objDS);

        DG_GC_Memo_Details.Columns[0].HeaderText = CompanyManager.getCompanyParam().GcCaption + " No";
        DG_GC_Memo_Details.Columns[7].HeaderText = CompanyManager.getCompanyParam().GcCaption + " Updated By";

        BindMemoMasterDetails = objDS.Tables[0];
        Cancelled = objDS.Tables[0].Rows.Count > 0 ? objDS.Tables[0].Rows[0]["CancelledText"].ToString() : "";

        if (objDS.Tables[1].Rows.Count > 0)
        {
            tr_Memo_Details.Visible = true;
            DG_GC_Memo_Details.Columns[3].HeaderText = objDS.Tables[1].Rows[0]["AUSCaption"].ToString() + "/DDC No";
            DG_GC_Memo_Details.Columns[5].HeaderText = objDS.Tables[1].Rows[0]["AUSCaption"].ToString() + "/DDC Date";

            BindMemoGrid = objDS.Tables[1];

            btn_Print_Memo.Visible = true;
            SetLinks_For_MemoPrint();
        }
        else
        {
            tr_Memo_Details.Visible = false;
            tr_Error.Visible = true;
            btn_Print_Memo.Visible = false;
        }
    }

    private void SetLinks_For_MemoPrint()
    {
        string path = "";

        String PrintPath = Util.GetBaseURL() + "/Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(51) + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(Doc_No);
        btn_Print_Memo.Attributes.Add("onclick", "return Open_Print_Window('" + PrintPath + "');");
    }

    protected void DG_GC_Memo_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        LinkButton btn_DDC_No, btn_GC_No, btn_GC_UpdatedBy;
        HiddenField hdn_GC_Id, hdn_AUSDDC_Id;
        int Memo_Type_Id;
        string Delivery_Type_Id;

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                btn_DDC_No = (LinkButton)(e.Item.FindControl("btn_AUS_DDC_No"));
                btn_GC_No = (LinkButton)(e.Item.FindControl("btn_GC_No"));
                btn_GC_UpdatedBy = (LinkButton)(e.Item.FindControl("btn_GC_UpdatedBy"));
                hdn_GC_Id = (HiddenField)(e.Item.FindControl("hdn_GC_ID"));
                hdn_AUSDDC_Id = (HiddenField)(e.Item.FindControl("hdn_AUS_DDC_ID"));

                Memo_Type_Id = Util.String2Int(((HiddenField)(e.Item.FindControl("hdn_Memo_Type_Id"))).Value);
                Delivery_Type_Id = ((HiddenField)(e.Item.FindControl("hdn_DelType_Id"))).Value;

                btn_GC_No.Attributes.Add("onclick", "return viewwindow('GC','" + hdn_GC_Id.Value + "')");
                btn_GC_UpdatedBy.Attributes.Add("onclick", "return view_userdetails('" + ClassLibraryMVP.Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "GC_UpdatedBy_id").ToString())) + "')");

                if (Memo_Type_Id == 1)
                {
                    btn_DDC_No.Attributes.Add("onclick", "return viewwindow('AUS','" + hdn_AUSDDC_Id.Value + "')");
                }
                else if (Memo_Type_Id == 2)
                {
                    btn_DDC_No.Attributes.Add("onclick", "return viewwindow_ddc('" + Delivery_Type_Id + "','" + hdn_AUSDDC_Id.Value + "')");
                }
            }
        }
    }

    protected void Rep_MemoDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        LinkButton lbtn_Updatedby, lbtn_LHPO_No;
        HiddenField hdn_LHPO_Id;
        Label lbl_Total_GC, lbl_LHPO_No, lbl_LHPO_Date;

        if (e.Item.ItemIndex != -1)
        {
            if (e.Item.ItemIndex == 0)
            {
                lbtn_LHPO_No = (LinkButton)(e.Item.FindControl("lbtn_LHPO_No"));
                lbtn_Updatedby = (LinkButton)(e.Item.FindControl("btn_Memo_updated_by"));
                lbl_Total_GC = (Label)(e.Item.FindControl("lbl_Total_GC"));
                lbl_LHPO_No = (Label)(e.Item.FindControl("lbl_LHPO_No"));
                lbl_LHPO_Date = (Label)(e.Item.FindControl("lbl_LHPO_Date"));
                hdn_LHPO_Id = (HiddenField)(e.Item.FindControl("hdn_LHPOId"));

                lbl_Total_GC.Text = "Total No Of " + CompanyManager.getCompanyParam().GcCaption;
                lbl_LHPO_No.Text = CompanyManager.getCompanyParam().LHPOCaption + " No";
                lbl_LHPO_Date.Text = CompanyManager.getCompanyParam().LHPOCaption + " Date";

                lbtn_LHPO_No.Attributes.Add("onclick", "return viewwindow('LHPO','" + hdn_LHPO_Id.Value + "')");

                lbtn_Updatedby.Attributes.Add("onclick", "return view_userdetails('" + ClassLibraryMVP.Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Memo_UpdatedBy_Id").ToString())) + "')");
            }
        }
    }

  #endregion   
}
