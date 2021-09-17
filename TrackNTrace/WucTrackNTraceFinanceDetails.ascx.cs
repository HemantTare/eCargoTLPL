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

public partial class TrackNTrace_WucTrackNTraceFinanceDetails : System.Web.UI.UserControl
{
    private DAL objDAL = new DAL();
    private DataSet objDS;
    string Document_Type;
    int Document_No, DocumentId;
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

    public DataTable BindFinanceGrid
    {
        set
        {
            DG_GC_Finance_Details.DataSource = value;
            DG_GC_Finance_Details.DataBind();
        }
    }
    public DataTable BindReceiptVoucherGrid
    {
        set
        {
            DG_GC_ReceiptVoucher_Details.DataSource = value;
            DG_GC_ReceiptVoucher_Details.DataBind();
        }
    }
    public DataTable BindAthBthVoucherGrid
    {
        set
        {
            DG_GC_AthBthVoucher_Details.DataSource = value;
            DG_GC_AthBthVoucher_Details.DataBind();
        }
    }
    public bool setVisibleOnPageLoad
    {
        set
        {
            tbl_Finance.Visible = value;
            tbl_ReceiptVoucher.Visible = value;
            tbl_ATH_BTH_Voucher.Visible = value;
        }
    }
    #endregion

    public void FillFinanceDetails()
    {
        tr_Error.Visible = false;
        tbl_ReceiptVoucher.Visible = false;
        fill_Finance_Grid();
    }

    private int getDocumentId()
    {
        if (Doc_Type == "GC")
            DocumentId = 2;
        else if (Doc_Type == "LHPO")
            DocumentId = 5;
        else if (Doc_Type == "BILL")
            DocumentId = 7;
        else if (Doc_Type == "CMEMO")
            DocumentId = 8;
        else if (Doc_Type == "BKMR" || Doc_Type == "DLMR")
            DocumentId = 11;
        else if (Doc_Type == "AUS")
            DocumentId = 16;

        return DocumentId;
    }
    private bool IsReceiptVoucherRequired()
    {
        if (getDocumentId() == 7 || getDocumentId() == 8)
            return true;
        else
            return false;
    }
    private bool IsATHBTHVoucherRequired()
    {
        if (getDocumentId() == 5)
            return true;
        else
            return false;
    }
    #region getValues

    private void fill_Finance_Grid()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0, UserManager.getUserParam().YearCode), 
            objDAL.MakeInParams("@Transaction_Id", SqlDbType.Int, 0, Doc_No),
            objDAL.MakeInParams("@Document_Id", SqlDbType.Int, 0, getDocumentId())};

        objDAL.RunProc("dbo.EC_Opr_TrackNTrace_Finance_Details", objSqlParam, ref objDS);

        if (objDS.Tables[0].Rows.Count > 0)
        {
            tbl_Finance.Visible = true;
            BindFinanceGrid = objDS.Tables[0];
        }
        else
        {
            tbl_Finance.Visible = false;

            if (IsPostBack)
                tr_Error.Visible = true;
        }

        if (IsReceiptVoucherRequired())
        {
            if (objDS.Tables[1].Rows.Count > 0)
            {
                tbl_ReceiptVoucher.Visible = true;
                BindReceiptVoucherGrid = objDS.Tables[1];
            }
            else
            {
                tbl_ReceiptVoucher.Visible = false;
            }
        }
        if (IsATHBTHVoucherRequired())
        {
            if (objDS.Tables[1].Rows.Count > 0)
            {
                tbl_ATH_BTH_Voucher.Visible = true;
                BindAthBthVoucherGrid = objDS.Tables[1];
            }
            else
            {
                tbl_ATH_BTH_Voucher.Visible = false;
            }
        }
    }

    protected void DG_GC_Finance_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        LinkButton btn_Voucher_No, btn_UpdatedBy;

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                btn_Voucher_No = (LinkButton)(e.Item.FindControl("btn_Voucher_No"));
                btn_UpdatedBy = (LinkButton)(e.Item.FindControl("btn_UpdatedBy"));

                btn_UpdatedBy.Attributes.Add("onclick", "return view_userdetails('" + Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "UpdatedBy_Id").ToString())) + "')");
                btn_Voucher_No.Attributes.Add("onclick", "return openVoucherWindow('" + Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Voucher_Id").ToString())) + "')");
            }
        }
    }
    protected void DG_GC_ReceiptVoucher_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        LinkButton btn_Voucher_No, btn_UpdatedBy;

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                btn_Voucher_No = (LinkButton)(e.Item.FindControl("btn_Voucher_No"));
                btn_UpdatedBy = (LinkButton)(e.Item.FindControl("btn_UpdatedBy"));

                btn_UpdatedBy.Attributes.Add("onclick", "return view_userdetails('" + Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "UpdatedBy_Id").ToString())) + "')");
                btn_Voucher_No.Attributes.Add("onclick", "return openVoucherWindow('" + Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Voucher_Id").ToString())) + "')");
            }
        }
    }
    protected void DG_GC_AthBthVoucher_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        LinkButton btn_Voucher_No, btn_UpdatedBy;

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                btn_Voucher_No = (LinkButton)(e.Item.FindControl("btn_Voucher_No"));
                btn_UpdatedBy = (LinkButton)(e.Item.FindControl("btn_UpdatedBy"));

                btn_UpdatedBy.Attributes.Add("onclick", "return view_userdetails('" + Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "UpdatedBy_Id").ToString())) + "')");
                btn_Voucher_No.Attributes.Add("onclick", "return openVoucherWindow('" + Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Voucher_Id").ToString())) + "')");
            }
        }
    }
    #endregion

}
