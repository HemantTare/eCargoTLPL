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
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Operations_Booking_NewGC_FrmNewGCInvoice : System.Web.UI.Page
{
    private DAL objDAL = new DAL();
    private DataSet objDS;
    TextBox txt_InvoiceNo, txt_Chalan_No, txt_BE_BLNo, txt_InvoiceAmount;
    DataRow dr;
    bool Allow_To_Save;

    public bool Is_Invoice_Amount_Required
    {
        set { chk_Is_Invoice_Amount_Required.Checked = value; }
        get { return chk_Is_Invoice_Amount_Required.Checked; }
    }
    public int Session_Mode
    {
        get { return StateManager.GetState<int>("SessionMode"); }
        set { StateManager.SaveState("SessionMode", value); }
    }
    public DataTable Session_InvoiceGrid
    {
        get { return StateManager.GetState<DataTable>("InvoiceGrid"); }
        set { StateManager.SaveState("InvoiceGrid", value); }
    }
    public void Bind_dg_Invoice()
    {
        dg_Invoice.DataSource = Session_InvoiceGrid;
        dg_Invoice.DataBind();
        Set_LabelText();
        if (IsPostBack)
            updateparentdataset();
    }
    private void Set_LabelText()
    {
        hdn_TotalInvoiceAmount.Value = "0";
        lbl_TotalInvoiceAmount.Text = "0";

        hdn_TotalInvoiceAmount.Value = Session_InvoiceGrid.Compute("SUM(Invoice_Amount)", "").ToString();
        lbl_TotalInvoiceAmount.Text = hdn_TotalInvoiceAmount.Value;
    }   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getInvoiceAmtParameters();
            Bind_dg_Invoice();
            if (Session_Mode == 4)
            {
                dg_Invoice.Columns[4].Visible = false;
                dg_Invoice.Columns[5].Visible = false;
                dg_Invoice.ShowFooter = false;
            }
        }
    }
    private void getInvoiceAmtParameters()
    {
        SqlParameter[] objSqlParam ={objDAL.MakeOutParams("@Is_Invoice_Amount_Required", SqlDbType.Bit, 0)};

        objDAL.RunProc("EC_Opr_NewGC_Invoice_FillValues", objSqlParam, ref objDS);
        Is_Invoice_Amount_Required = Util.String2Bool(objSqlParam[0].Value.ToString());
    }
    private void updateparentdataset()
    {
        string popupScript = "<script language='javascript'>updateparentdataset(" + hdn_TotalInvoiceAmount.Value + ");</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(string), "PopupScript", popupScript.ToString(), false);
    }
    protected void dg_Invoice_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            Insert_Update_Invoice_Dataset(source, e);
            if (Allow_To_Save == true)
            {
                Bind_dg_Invoice();
                dg_Invoice.EditItemIndex = -1;
                dg_Invoice.ShowFooter = true;
                scm_Invoice.SetFocus(txt_InvoiceNo);
            }
        }
    }
    protected void dg_Invoice_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
            {
                txt_InvoiceNo = (TextBox)(e.Item.FindControl("txt_Invoice_No"));
                txt_Chalan_No = (TextBox)(e.Item.FindControl("txt_Chalan_No"));
                txt_BE_BLNo = (TextBox)(e.Item.FindControl("txt_BE_BL_No"));
                txt_InvoiceAmount = (TextBox)(e.Item.FindControl("txt_Invoice_Amount"));
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                dr = Session_InvoiceGrid.Rows[e.Item.ItemIndex];

                txt_InvoiceNo.Text = dr["Invoice_No"].ToString();
                txt_Chalan_No.Text = dr["Chalan_No"].ToString();
                txt_BE_BLNo.Text = dr["BE_BL_No"].ToString();
                txt_InvoiceAmount.Text = dr["Invoice_Amount"].ToString();
                scm_Invoice.SetFocus(txt_InvoiceNo);
            }
        }
    }
    protected void dg_Invoice_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        Insert_Update_Invoice_Dataset(source, e);
        if (Allow_To_Save == true)
        {
            dg_Invoice.EditItemIndex = -1;
            dg_Invoice.ShowFooter = true;
            Bind_dg_Invoice();
        }
        scm_Invoice.SetFocus(txt_InvoiceNo);
    }
    protected void dg_Invoice_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Invoice.EditItemIndex = -1;
        dg_Invoice.ShowFooter = true;
        Bind_dg_Invoice();
        lbl_invoiceErrorMsg.Text = "";
        scm_Invoice.SetFocus(txt_InvoiceNo);
    }
    protected void dg_Invoice_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Invoice.EditItemIndex = e.Item.ItemIndex;
        dg_Invoice.ShowFooter = false;
        Bind_dg_Invoice();
        lbl_invoiceErrorMsg.Text = "";
    }
    protected void dg_Invoice_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            dr = Session_InvoiceGrid.Rows[e.Item.ItemIndex];
            dr.Delete();
            Session_InvoiceGrid.AcceptChanges();
            dg_Invoice.EditItemIndex = -1;
            dg_Invoice.ShowFooter = true;
            Bind_dg_Invoice();
        }
        scm_Invoice.SetFocus(txt_InvoiceNo);
    }
    public Boolean Allow_To_Add_Update_Invoice()
    {
        Allow_To_Save = false;
        lbl_invoiceErrorMsg.Text = "";
        decimal InvoiceAmt = txt_InvoiceAmount.Text.Trim() == string.Empty ? 0 : Util.String2Decimal(txt_InvoiceAmount.Text.Trim());

        if (txt_InvoiceNo.Text.Trim() == string.Empty && txt_Chalan_No.Text.Trim() == string.Empty)
        {
            lbl_invoiceErrorMsg.Text = "Please Enter Invoice No. or Chalan No.";
            scm_Invoice.SetFocus(txt_InvoiceNo);
        }
        else if (InvoiceAmt <= 0 && Is_Invoice_Amount_Required)
        {
            lbl_invoiceErrorMsg.Text = "Please Enter Invoice Amount";
            scm_Invoice.SetFocus(txt_InvoiceAmount);
        }
        else
        {
            Allow_To_Save = true;
        }
        return Allow_To_Save;
    }

    private void Insert_Update_Invoice_Dataset(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        txt_InvoiceNo = (TextBox)(e.Item.FindControl("txt_Invoice_No"));
        txt_Chalan_No = (TextBox)(e.Item.FindControl("txt_Chalan_No"));
        txt_BE_BLNo = (TextBox)(e.Item.FindControl("txt_BE_BL_No"));
        txt_InvoiceAmount = (TextBox)(e.Item.FindControl("txt_Invoice_Amount"));

        if (Allow_To_Add_Update_Invoice())
        {
            if (e.CommandName == "Add")
                dr = Session_InvoiceGrid.NewRow();
            else if (e.CommandName == "Update")
                dr = Session_InvoiceGrid.Rows[e.Item.ItemIndex];

            dr["Invoice_No"] = txt_InvoiceNo.Text.Trim();
            dr["Chalan_No"] = txt_Chalan_No.Text.Trim();
            dr["BE_BL_No"] = txt_BE_BLNo.Text;
            dr["Invoice_Amount"] = txt_InvoiceAmount.Text.Trim() == string.Empty ? "0" : txt_InvoiceAmount.Text.Trim();

            if (e.CommandName == "Add")
                Session_InvoiceGrid.Rows.Add(dr);
        }
    }
}
