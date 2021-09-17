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

public partial class TrackNTrace_WucTrackNTraceCrossingDetails : System.Web.UI.UserControl
{
    private DAL objDAL = new DAL();
    private DataSet objDS;
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

    public DataTable BindCrossingGrid
    {
        set
        {
            DG_GC_Crossing_Details.DataSource = value;
            DG_GC_Crossing_Details.DataBind();
        }
    }
    public DataTable BindStockTransferGrid
    {
        set
        {
            dg_stock_transfer.DataSource = value;
            dg_stock_transfer.DataBind();
        }
    }
    public DataTable BindDeliveryBranchGrid
    {
        set
        {
            dg_deliveryBranchUpdate.DataSource = value;
            dg_deliveryBranchUpdate.DataBind();
        }
    }
    public bool setVisibleOnPageLoad
    {
        set
        {
            tbl_Crosssing.Visible = value;           
        }
    }
    #endregion

    public void FillCrossingDetails()
    {
        tr_Error.Visible = false;
        DG_GC_Crossing_Details.Columns[8].HeaderText = CompanyManager.getCompanyParam().LHPOCaption + " No";

        if (Doc_Type == "GC")
        {
            fill_Crossing_Grid();
        }
    }

    #region getValues
       
    private void fill_Crossing_Grid()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0, UserManager.getUserParam().YearCode), 
            objDAL.MakeInParams("@GC_Id", SqlDbType.Int, 0, Doc_No) };

        objDAL.RunProc("dbo.EC_Opr_TrackNTrace_Crossing_Details", objSqlParam, ref objDS);

        if (objDS.Tables[0].Rows.Count > 0)
        {
            tbl_Crosssing.Visible = true;
            DG_GC_Crossing_Details.Columns[9].HeaderText = objDS.Tables[0].Rows[0]["AUSCaption"].ToString() + "/DDC No";
            DG_GC_Crossing_Details.Columns[11].HeaderText = objDS.Tables[0].Rows[0]["AUSCaption"].ToString() + "/DDC Date";
            lbl_OutwardText.Text = objDS.Tables[0].Rows[0]["OutwardCaption"].ToString();
            lbl_InwardText.Text = objDS.Tables[0].Rows[0]["InwardCaption"].ToString();

            BindCrossingGrid = objDS.Tables[0];
        }
        else
        {
            tbl_Crosssing.Visible = false;

            if (IsPostBack)
            tr_Error.Visible = true;
        }

        if (objDS.Tables[1].Rows.Count > 0)
        {
            tr_stock_transfer.Visible = true;
            BindStockTransferGrid = objDS.Tables[1];
        }
        else
        {
            tr_stock_transfer.Visible = false;
        }

        if (objDS.Tables[2].Rows.Count > 0)
        {
            tr_del_branch_update.Visible = true;
            BindDeliveryBranchGrid = objDS.Tables[2];
        }
        else
        {
            tr_del_branch_update.Visible = false;
        }
    }

    protected void DG_GC_Crossing_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        LinkButton btn_Memo_No, btn_LHPO_No, btn_AUS_No, btn_MemoUpdated_By, btn_AUS_DDCUpdated_By;
        HiddenField hdn_Memo_Id, hdn_LHPO_Id, hdn_AUS_DDC_Id;
        int Memo_Type_Id;
        string Delivery_Type_Id;

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                btn_Memo_No = (LinkButton)(e.Item.FindControl("btn_Memo_No"));
                btn_LHPO_No = (LinkButton)(e.Item.FindControl("btn_LHPO_No"));
                btn_AUS_No = (LinkButton)(e.Item.FindControl("btn_AUS_No"));
                btn_MemoUpdated_By = (LinkButton)(e.Item.FindControl("btn_MemoUpdated"));
                btn_AUS_DDCUpdated_By = (LinkButton)(e.Item.FindControl("btn_AusUpdated"));

                hdn_Memo_Id = (HiddenField)(e.Item.FindControl("hdn_Memo_Id"));
                hdn_LHPO_Id = (HiddenField)(e.Item.FindControl("hdn_LHPO_Id"));
                hdn_AUS_DDC_Id = (HiddenField)(e.Item.FindControl("hdn_AUS_DDC_Id"));

                Memo_Type_Id =Util.String2Int(((HiddenField)(e.Item.FindControl("hdn_Memo_Type_Id"))).Value);
                Delivery_Type_Id = ((HiddenField)(e.Item.FindControl("hdn_DelType_Id"))).Value;

                btn_Memo_No.Attributes.Add("onclick", "return viewwindow('MEMO','" + hdn_Memo_Id.Value + "')");
                btn_LHPO_No.Attributes.Add("onclick", "return viewwindow('LHPO','" + hdn_LHPO_Id.Value + "')");
                btn_MemoUpdated_By.Attributes.Add("onclick", "return view_userdetails('" + Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Memo_UpdatedBy_id").ToString())) + "')");
                btn_AUS_DDCUpdated_By.Attributes.Add("onclick", "return view_userdetails('" + Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "AUS_DDC_UpdatedBy_id").ToString())) + "')");

                if (Memo_Type_Id == 1)
                {
                    btn_AUS_No.Attributes.Add("onclick", "return viewwindow('AUS','" + hdn_AUS_DDC_Id.Value + "')");
                }
                else if(Memo_Type_Id == 2)
                {
                    btn_AUS_No.Attributes.Add("onclick", "return viewwindow_ddc('" + Delivery_Type_Id + "','" + hdn_AUS_DDC_Id.Value + "')");
                }
            }
        }
    }
  #endregion
    protected void dg_stock_transfer_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        LinkButton btn_UpdatedBy;

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                btn_UpdatedBy = (LinkButton)(e.Item.FindControl("btn_UpdatedBy"));
                btn_UpdatedBy.Attributes.Add("onclick", "return view_userdetails('" + Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Stk_UpdatedBy_Id").ToString())) + "')");
            }
        }
    }
    protected void dg_deliveryBranchUpdate_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        LinkButton btn_UpdatedBy;
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                btn_UpdatedBy = (LinkButton)(e.Item.FindControl("btn_UpdatedBy"));
                btn_UpdatedBy.Attributes.Add("onclick", "return view_userdetails('" + Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "DBU_UpdatedBy_Id").ToString())) + "')");
            }
        }
    }
}
