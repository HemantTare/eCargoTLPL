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

public partial class TrackNTrace_WucTrackNTraceDeliveryDetails : System.Web.UI.UserControl
{
    private DAL objDAL = new DAL();
    private DataSet objDS;
    public Label lbl_cancelled;

    string Document_Type;
    int Delivery_Type_ID, Document_No;

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

    public int DeliveryType_ID
    {
        get { return Delivery_Type_ID; }
        set { Delivery_Type_ID = value; }
    }

    public DataTable BindDeliveryGrid
    {
        set
        {
            DG_GC_Delivery_Details.DataSource = value;
            DG_GC_Delivery_Details.DataBind();
        }
    }

    public DataTable BindDeliveryMRGrid
    {
        set
        {
            DG_GC_MR_Details.DataSource = value;
            DG_GC_MR_Details.DataBind();
        }
    }

    public DataTable BindCreditMemoGrid
    {
        set
        {
            DG_Credit_Memo_Details.DataSource = value;
            DG_Credit_Memo_Details.DataBind();
        }
    }

    public DataTable BindDeliveryMasterDetails
    {
        set
        {
            Rep_DeliveryDetails.DataSource = value;
            Rep_DeliveryDetails.DataBind();
        }
    }

    public bool setVisibleOnPageLoad
    {
        set
        {
            tr_DELIVERY_MR.Visible = value;
            tr_DELIVERY_Details.Visible = value;
            tr_Credit_Memo.Visible = value;
        }
    }
    #endregion

    public void FillDeliveryDetails()
    {
        tr_Error.Visible = false;
        DG_GC_Delivery_Details.Columns[3].HeaderText = CompanyManager.getCompanyParam().LHPOCaption + " No";

        if (Doc_Type == "GC")
        {
            DG_GC_Delivery_Details.Columns[11].HeaderText = "Updated By";
            fill_GCDelivery_Grid();
        }
        else if (Doc_Type == "DDC")
        {
            DG_GC_Delivery_Details.Columns[1].HeaderText = CompanyManager.getCompanyParam().GcCaption + " No";
            DG_GC_Delivery_Details.Columns[11].HeaderText = CompanyManager.getCompanyParam().GcCaption + " Updated By";

            fill_Delivery_Grid();
        }
    }
    #region getValues

    private void fill_GCDelivery_Grid()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0, UserManager.getUserParam().YearCode), 
            objDAL.MakeInParams("@GC_Id", SqlDbType.Int, 0, Doc_No) };

        objDAL.RunProc("dbo.EC_Opr_TrackNTrace_GC_Delivery_Details", objSqlParam, ref objDS);

        tr_RepDelDetals.Visible = false;
        if (objDS.Tables[0].Rows.Count > 0)
        {
            tr_DELIVERY_Details.Visible = true;
            DG_GC_Delivery_Details.Columns[0].Visible = true;
            DG_GC_Delivery_Details.Columns[1].Visible = false;
            DG_GC_Delivery_Details.Columns[4].HeaderText = objDS.Tables[0].Rows[0]["AUSCaption"].ToString() + " No";

            BindDeliveryGrid = objDS.Tables[0];
        }
        else
        {
            tr_DELIVERY_Details.Visible = false;

            if (IsPostBack)
            tr_Error.Visible = true;
        }
        if (objDS.Tables[1].Rows.Count > 0)
        {
            tr_DELIVERY_MR.Visible = true;
            BindDeliveryMRGrid = objDS.Tables[1];
        }
        else
        {
            tr_DELIVERY_MR.Visible = false;
        }

        if (objDS.Tables[2].Rows.Count > 0)
        {
            tr_Credit_Memo.Visible = true;
            BindCreditMemoGrid = objDS.Tables[2];
        }
        else
        {
            tr_Credit_Memo.Visible = false;
        }
    }

    private void fill_Delivery_Grid()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0, UserManager.getUserParam().YearCode) ,
                                        objDAL.MakeInParams("@DDC_Id", SqlDbType.Int, 0, Doc_No),
                                        objDAL.MakeInParams("@DDC_Type_Id", SqlDbType.Int, 0, DeliveryType_ID)};

        objDAL.RunProc("dbo.EC_Opr_TrackNTrace_Delivery_Details", objSqlParam, ref objDS);

        tr_RepDelDetals.Visible = true;
        Cancelled = objDS.Tables[0].Rows.Count > 0 ? objDS.Tables[0].Rows[0]["CancelledText"].ToString() : "";

        BindDeliveryMasterDetails = objDS.Tables[0];
        if (objDS.Tables[1].Rows.Count > 0)
        {
            tr_DELIVERY_Details.Visible = true;

            DG_GC_Delivery_Details.Columns[0].Visible = false;
            DG_GC_Delivery_Details.Columns[1].Visible = true;
            DG_GC_Delivery_Details.Columns[4].HeaderText = objDS.Tables[1].Rows[0]["AUSCaption"].ToString() + " No";

            BindDeliveryGrid = objDS.Tables[1];
        }
        else
        {
            tr_DELIVERY_Details.Visible = false;
        }
        if (objDS.Tables[0].Rows.Count == 0)
            tr_Error.Visible = true;
    }

    protected void DG_GC_Delivery_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        LinkButton btn_DDC_No, btn_GC_No, btn_UpdatedBy, btn_Memo_No, btn_LHPO_No, btn_AUS_No;
        HiddenField hdn_GC_Id,hdn_Memo_Id, hdn_LHPO_Id, hdn_AUS_Id, hdn_DDC_Id;
        string Delivery_Type_Id;

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                btn_DDC_No = (LinkButton)(e.Item.FindControl("btn_DDC_No"));
                btn_GC_No = (LinkButton)(e.Item.FindControl("btn_GC_No"));
                btn_Memo_No = (LinkButton)(e.Item.FindControl("btn_Memo_No"));
                btn_LHPO_No = (LinkButton)(e.Item.FindControl("btn_LHPO_No"));
                btn_AUS_No = (LinkButton)(e.Item.FindControl("btn_AUS_No"));
                btn_UpdatedBy = (LinkButton)(e.Item.FindControl("btn_UpdatedBy"));

                hdn_GC_Id = (HiddenField)(e.Item.FindControl("hdn_GC_Id"));
                hdn_Memo_Id = (HiddenField)(e.Item.FindControl("hdn_Memo_Id"));
                hdn_LHPO_Id = (HiddenField)(e.Item.FindControl("hdn_LHPO_Id"));
                hdn_AUS_Id = (HiddenField)(e.Item.FindControl("hdn_AUS_Id"));
                hdn_DDC_Id = (HiddenField)(e.Item.FindControl("hdn_DDC_Id"));

                Delivery_Type_Id = ((HiddenField)(e.Item.FindControl("hdn_DelType_Id"))).Value;

                btn_DDC_No.Attributes.Add("onclick", "return viewwindow_ddc('" + Delivery_Type_Id + "','" + hdn_DDC_Id.Value + "')");
                btn_GC_No.Attributes.Add("onclick", "return viewwindow('GC','" + hdn_GC_Id.Value + "')");
                btn_Memo_No.Attributes.Add("onclick", "return viewwindow('MEMO','" + hdn_Memo_Id.Value + "')");
                btn_LHPO_No.Attributes.Add("onclick", "return viewwindow('LHPO','" + hdn_LHPO_Id.Value + "')");
                btn_AUS_No.Attributes.Add("onclick", "return viewwindow('AUS','" + hdn_AUS_Id.Value + "')");

                btn_UpdatedBy.Attributes.Add("onclick", "return view_userdetails('" + ClassLibraryMVP.Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "UpdatedBy_id").ToString())) + "')");
            }
        }
    }

    protected void DG_GC_MR_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        LinkButton btn_MR_No, btn_MR_UpdatedBy;
        HiddenField hdn_MR_Id;

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                btn_MR_No = (LinkButton)(e.Item.FindControl("btn_MR_No"));
                btn_MR_UpdatedBy = (LinkButton)(e.Item.FindControl("btn_MR_UpdatedBy"));
                hdn_MR_Id = (HiddenField)(e.Item.FindControl("hdn_MR_Id"));

                btn_MR_No.Attributes.Add("onclick", "return viewwindow('DLMR','" + hdn_MR_Id.Value + "')");
                btn_MR_UpdatedBy.Attributes.Add("onclick", "return view_userdetails('" + ClassLibraryMVP.Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "MR_Updated_By_ID").ToString())) + "')");
            }
        }
    }

    protected void DG_Credit_Memo_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        LinkButton btn_Credit_Memo_No, btn_UpdatedBy;

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                btn_Credit_Memo_No = (LinkButton)(e.Item.FindControl("btn_Credit_Memo_No"));
                btn_UpdatedBy = (LinkButton)(e.Item.FindControl("btn_UpdatedBy"));

                //e.Item.Cells[0].Attributes.Add("onclick", "return viewwindow('DLMR','" + DataBinder.Eval(e.Item.DataItem, "Credit_Memo_Id").ToString() + "')");
                //e.Item.Cells[0].Attributes.Add("onmouseover", "this.style.cursor='hand'");
                //e.Item.Cells[0].Attributes.Add("color", "ActiveCaption");

                //e.Item.Cells[4].Attributes.Add("onclick", "return view_userdetails('" + ClassLibraryMVP.Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "UpdatedBy_Id").ToString())) + "')");
                //e.Item.Cells[4].Attributes.Add("onmouseover", "this.style.cursor='hand'");
                //e.Item.Cells[4].Attributes.Add("Underline", "true");
                //e.Item.Cells[4].Attributes.Add("color", "#8066ef");

                btn_Credit_Memo_No.Attributes.Add("onclick", "return viewwindow('CMEMO','" + DataBinder.Eval(e.Item.DataItem, "Credit_Memo_Id").ToString() + "')");
                btn_UpdatedBy.Attributes.Add("onclick", "return view_userdetails('" + ClassLibraryMVP.Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "UpdatedBy_Id").ToString())) + "')");
            }
        }
    }

    protected void Rep_DeliveryDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        LinkButton lbtn_Updatedby;
        Label lbl_Tot_GC;

        if (e.Item.ItemIndex != -1)
        {
            if (e.Item.ItemIndex == 0)
            {
                lbtn_Updatedby = (LinkButton)(e.Item.FindControl("btn_DDC_UpdatedBy"));
                lbl_Tot_GC = (Label)(e.Item.FindControl("lbl_Tot_GC"));

                lbl_Tot_GC.Text = "Total No Of " + CompanyManager.getCompanyParam().GcCaption;

                lbtn_Updatedby.Attributes.Add("onclick", "return view_userdetails('" + ClassLibraryMVP.Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Del_UpdatedBy_id").ToString())) + "')");
            }
        }
    }

    #endregion 
}
