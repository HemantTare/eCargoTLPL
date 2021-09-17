using System;
using System.Data;
using System.Web.UI.WebControls;

using Raj.EC;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.FinancePresenter;
using Raj.EC.FinanceView;

//added : Ankit champaneriya
//desc  : voucher view 

public partial class Finance_IBT_WucVoucherView : System.Web.UI.UserControl,IVoucherDetailsView
{

    #region class variable
    private DataSet ds = new DataSet();
    private VoucherViewPresenter  objVoucherViewPresenter;

    String Crypt;
    string crypt_id;
  
    DateTime Voucher_Date;
 
    Decimal Total_Amt;

    #endregion

    #region otherProperties

    //public int crypt_id
    //{
    //    get
    //    {
    //        //return Util.String2Int(hdn_CryptId.Value);
    //        return 4390;
    //    }
    //    set
    //    {
    //        hdn_CryptId.Value = Util.Int2String(value);
    //    }
    //}
    //public String crypt_Type
    //{
    //    get { return "Approved Vouchers"; }
    //}


    public DataSet Session_DetailsVoucher
    {
        get { return StateManager.GetState<DataSet>("Session_Voucher"); }
        set { StateManager.SaveState("Session_Voucher", value); }
    }

    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;
        _isValid = true;
        return _isValid;
    }

    public string errorMessage
    {
        set
        {
            lbl_Error.Text = "";// value;
        }
    }

    public int keyID
    {
        get
        {
            //return Util.DecryptToInt(Request.QueryString["Id"]);
            return 1;
        }
    }

    #endregion

    #region Other Method

    private void LoadDataset()
    {
        DataSet dts = new DataSet();
        DataTable dtab = new DataTable("Voucher_BillWiseDetails");
        dtab.Columns.Add(new DataColumn("Sr_No"));
        dtab.Columns.Add(new DataColumn("Ledger_Id"));
        dtab.Columns.Add(new DataColumn("Type_of_Ref"));
        dtab.Columns.Add(new DataColumn("Bill_Date"));
        dtab.Columns.Add(new DataColumn("Credit_Days"));
        dtab.Columns.Add(new DataColumn("Amount"));
        dtab.Columns.Add(new DataColumn("calcAmount"));
        dtab.Columns.Add(new DataColumn("Cr_Dr"));
        dtab.Columns.Add(new DataColumn("CrDr_Text"));
        dtab.Columns.Add(new DataColumn("IBT_Type"));
        dtab.Columns.Add(new DataColumn("Name"));
        dtab.Columns.Add(new DataColumn("unique_key"));
        dts.Tables.Add(dtab);


        DataTable dtble = new DataTable("Cost_Centre_Details");//'...holds CostCentre records

        dtble.Columns.Add(new DataColumn("Sr_No"));
        dtble.Columns.Add(new DataColumn("Cost_Centre_Id"));
        dtble.Columns.Add(new DataColumn("Amount"));
        dtble.Columns.Add(new DataColumn("Ledger_Id"));
        dtble.Columns.Add(new DataColumn("Cost_Centre_Name"));
        dtble.Columns.Add(new DataColumn("calcAmount"));
        dtble.Columns.Add(new DataColumn("Cost_Centre"));
        dts.Tables.Add(dtble);

        dts.Tables.Add(objVoucherViewPresenter.getData("AO", "201011", 21).Tables["Cost_Centre"].Copy());

        Session_DetailsVoucher = dts;
    }

    //private Boolean Check_Validity_Before_Save()
    //{

    //    if (ddl_Ledger_Name.SelectedValue.Trim() == String.Empty)
    //    {
    //        lbl_Error.Text = "Please Select Ladger Name";
    //        btn_CostCentre.Visible = false;
    //        btn_BillwiseDetails.Visible = false;
    //        return false;
    //    }

    //    int Ledger_Id = Util.String2Int(ddl_Ledger_Name.SelectedValue);

    //    int BillApp = Util.String2Int(ddl_Ledger_Name.GetValueAt(1));
    //    string Ledger_Name = ddl_Ledger_Name.SelectedItem;


    //    //Dim TempDS As DataSet = CType(Session("Voucher_Data"), DataSet)
    //    if (Convert.ToBoolean(BillApp))
    //    {
    //        DataView TempDV = new DataView();
    //        //    Dim TempDV As DataView = New DataView(TempDS.Tables("Voucher_BillWiseDetails"), "Ledger_Id=" & Ledger_Id.ToString(), "", DataViewRowState.CurrentRows)
    //        if (TempDV.Count == 0)
    //        {
    //            lbl_Error.Text = "Please Enter Bill Wise Details ";
    //            return false;
    //        }
    //    }

    //    if (Convert.ToBoolean(objVoucherViewPresenter.Is_Cost_Centre(Ledger_Id) == true))
    //    {
    //        DataView TempDV = new DataView();
    //        //DataView TempDV = new DataView(TempDS.Tables("Cost_Centre_Details"), "Ledger_Id=" + Ledger_Id.ToString(), "", DataViewRowState.CurrentRows);
    //        if (TempDV.Count == 0)
    //        {
    //            lbl_Error.Text = "Please Enter Cost Centre Details ";
    //            return false;
    //        }
    //    }


    //    lbl_Error.Text = "";
    //    return true;
    //}

    #endregion

    #region event click
    protected void Page_Load(object sender, EventArgs e)
    {
        Crypt = Request.QueryString["VoucherId"];
        crypt_id = ClassLibraryMVP.Util.DecryptToString(Crypt);

        Boolean isApproveVoucher = Util.String2Bool(Request.QueryString["IsApproveVoucher"]);

        objVoucherViewPresenter = new VoucherViewPresenter(this, IsPostBack);

        if (!IsPostBack)
        {
            if (isApproveVoucher)
            {
                dv_UnApprovedVoucher.DataSource = objVoucherViewPresenter.Bind_dv_UnApprovedVoucher();
                dv_UnApprovedVoucher.DataBind();
                
                dg_Details.DataSource = objVoucherViewPresenter.Bind_dg_Details();
                dg_Details.DataBind();

                lgd1.InnerHtml = "Approved Voucher";
                lgd2.InnerHtml = "Approved Voucher Details";
            }
            else
            {
                dv_UnApprovedVoucher.DataSource = objVoucherViewPresenter.Bind_dv_UnApprovedVoucher();
                dv_UnApprovedVoucher.DataBind();

                dg_Details.DataSource = objVoucherViewPresenter.Bind_dg_Details();
                dg_Details.DataBind();

                lgd1.InnerHtml = "Un-Approved Voucher";
                lgd2.InnerHtml = "Un-Approved Voucher Details";
            }
            LoadDataset();
        }
    }

    protected void dv_UnApprovedVoucher_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i <= dv_UnApprovedVoucher.Rows.Count - 1; i++)
        {
            dv_UnApprovedVoucher.Rows[i].Attributes.Add("onmouseover", "this.style.backgroundColor='#E5E5E5'");
            dv_UnApprovedVoucher.Rows[i].Attributes.Add("onmouseout", "this.style.backgroundColor='white'");
        }
        dv_UnApprovedVoucher.RowStyle.Height = 17;
    }
    
    protected void btn_Reverse_Click(object sender, EventArgs e)
    {
        //if (txt_Reason.Text.Trim() == "")
        //    lbl_Error.Visible = true;
        //else
        //    if (Check_Validity_Before_Save())
        //    {
        //        lbl_Error.Visible = false;
        //        objUnAppVoucherCancellationPresenter.save();
        //    }
        //    else
        //        lbl_Error.Visible = true;
    }
    #endregion

}
