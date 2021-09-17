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
using ClassLibraryMVP.General;
using Raj.EC.FinanceView;
using Raj.EC.FinancePresenter;
using Raj.EC;

public partial class Finance_Accounting_Vouchers_WucOtherChargeDetails : System.Web.UI.UserControl,IOtherChargeDetailsView
{
    OtherChargeDetailsPresenter objOtherChargeDetailsPresenter;
    public EventHandler OtherDetailsGrid;
    ClassLibrary.UIControl.DDLSearch ddl_Ledger;
    DropDownList ddl_AddLess;
    ScriptManager scm;
    TextBox txt_Amount;
    DataTable objDT;
    DataRow DR = null;
    bool isValid = false;

    #region controls Values
   
    public decimal TotalAmount 
    {
        get {  return hdn_TotalAmount.Value == "" ? 0 : Util.String2Decimal(hdn_TotalAmount.Value);}
        set { 
                hdn_TotalAmount.Value = value.ToString();
                lbl_TotalAmount.Text = value.ToString();
            }
    }

    public String OtherDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            DataTable dt = new DataTable();
            dt = Session_OtherDetailsGrid;
            _objDs.Tables.Add(dt.Copy());

            _objDs.Tables[0].TableName = "otherdetails";
            return _objDs.GetXml().ToLower();
        }
    }
    
    public DataTable Bind_OtherDetailsGrid
    {
        set
        {            
            dg_OtherChargeDetails.DataSource = value;
            dg_OtherChargeDetails.DataBind();
         }
    }
    
    public DataTable Session_OtherDetailsGrid
    {
        get { return StateManager.GetState<DataTable>("OtherDetails"); }
        set { StateManager.SaveState("OtherDetails", value); }
    }
    #endregion

    #region IView

    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }

    public string errorMessage
    {
        set{ lbl_Errors.Text = value;}
    }

    public bool validateUI()
    {
        bool _isValid = true;
        return _isValid;
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        objOtherChargeDetailsPresenter = new OtherChargeDetailsPresenter(this, IsPostBack);
    }
    protected void dg_OtherChargeDetails_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_OtherChargeDetails.EditItemIndex = -1;
        dg_OtherChargeDetails.ShowFooter = true;
        Bind_OtherDetailsGrid = Session_OtherDetailsGrid;
    }
    protected void dg_OtherChargeDetails_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            Calculate_Amount(source, e);
            objDT = Session_OtherDetailsGrid;
            objDT.Rows.RemoveAt(e.Item.ItemIndex);
            objDT.AcceptChanges();
            Session_OtherDetailsGrid = objDT;
            dg_OtherChargeDetails.EditItemIndex = -1;
            dg_OtherChargeDetails.ShowFooter = true;
            Bind_OtherDetailsGrid = Session_OtherDetailsGrid;
        }
    }
    protected void dg_OtherChargeDetails_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_OtherChargeDetails.EditItemIndex = e.Item.ItemIndex;
        dg_OtherChargeDetails.ShowFooter = false;
        Bind_OtherDetailsGrid = Session_OtherDetailsGrid;
    }
    protected void dg_OtherChargeDetails_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            Insert_Update_Dataset(source, e);
            if (isValid == true)
            {
                Bind_OtherDetailsGrid = Session_OtherDetailsGrid;
                dg_OtherChargeDetails.EditItemIndex = -1;
                dg_OtherChargeDetails.ShowFooter = true;
            }
        }
    }
    protected void dg_OtherChargeDetails_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        Insert_Update_Dataset(source, e);
        if (isValid == true)
        {
            dg_OtherChargeDetails.EditItemIndex = -1;
            dg_OtherChargeDetails.ShowFooter = true;

            Bind_OtherDetailsGrid = Session_OtherDetailsGrid;
        }
    }
    protected void dg_OtherChargeDetails_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                txt_Amount = (TextBox)(e.Item.FindControl("txt_Amount"));
                ddl_AddLess = (DropDownList)(e.Item.FindControl("ddl_AddLess"));
                ddl_Ledger = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_Ledger"));
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                objDT = Session_OtherDetailsGrid;

                DR = objDT.Rows[e.Item.ItemIndex];

                txt_Amount.Text = DR["Amount"].ToString();
               
                SetLedgerID(DR["Ledger_Name"].ToString(), DR["Ledger_ID"].ToString()); 

                if (Convert.ToBoolean(DR["Is_Add"]) == true)
                {
                    ddl_AddLess.SelectedValue = "1";
                }
                else
                {
                    ddl_AddLess.SelectedValue = "2";
                }                
            }
        }
    }

    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        txt_Amount = (TextBox)(e.Item.FindControl("txt_Amount"));
        ddl_AddLess = (DropDownList)(e.Item.FindControl("ddl_AddLess"));
        ddl_Ledger = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_Ledger"));        

        objDT = Session_OtherDetailsGrid;

        if (Allow_To_Add_Update() == true)
        {

        if (e.CommandName == "Add")
        {
            objDT.Columns["Ledger_Id"].AllowDBNull = true;
            DR = objDT.NewRow();
            objDT.Rows.Add(DR);                       
        }
        else if (e.CommandName == "Update")
        {
            DR = objDT.Rows[e.Item.ItemIndex];
        }

        
            try
            {
                DR["Ledger_ID"] = Util.String2Int(ddl_Ledger.SelectedValue);
            }
            catch
            {
                if (e.CommandName == "Add")
                {
                    objDT.Rows.Remove(DR);
                }
                errorMessage = "Duplicate Record";
                isValid = false;
                return;
            }
            Calculate_Amount(source, e);

            DR["Amount"] = Util.String2Decimal(txt_Amount.Text);            
            DR["Ledger_Name"] = ddl_Ledger.SelectedText;
            if (Util.String2Int(ddl_AddLess.SelectedValue) == 1)
            {
                DR["Is_Add"] = 1;
            }
            else
            {
                DR["Is_Add"] = 0;
            }
            
            DR["Is_AddLess"] = ddl_AddLess.SelectedItem.Text;
            
            Session_OtherDetailsGrid = objDT;            
        }
    }

    private bool Allow_To_Add_Update()
    {

        lbl_Errors.Text = "";

        if (ddl_AddLess.SelectedValue == "0")
        {
            errorMessage = "Please Select IsAdd/Less";
            //scm.SetFocus(ddl_AddLess);
        }
        else if (Util.String2Int(ddl_Ledger.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Ledger";
        }
        else if (txt_Amount.Text == string.Empty)
        {
            errorMessage = "Please Enter Amount";
        }
        else
        {
            isValid = true;
        }

        return isValid;
    }

    public void Calculate_Amount(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        decimal Total_Amount = TotalAmount;

        if (e.CommandName == "Add" || e.CommandName == "Update" || e.CommandName == "Delete")
        {
            //if (e.CommandName == "Update")
            //{
            //    if (Convert.ToBoolean(Session_OtherDetailsGrid.Rows[e.Item.ItemIndex]["Is_Add"]) == true)
            //    {
            //        Total_Amount = Total_Amount - Convert.ToDecimal(Session_OtherDetailsGrid.Rows[e.Item.ItemIndex]["Amount"]);
            //    }
            //    else
            //    {
            //        Total_Amount = Total_Amount + Convert.ToDecimal(Session_OtherDetailsGrid.Rows[e.Item.ItemIndex]["Amount"]);
            //    }
            //}

            if (e.CommandName == "Delete" || e.CommandName == "Update")
            {
                if (Convert.ToBoolean(Session_OtherDetailsGrid.Rows[e.Item.ItemIndex]["Is_Add"]) == true)
                {
                    Total_Amount = Total_Amount - Convert.ToDecimal(Session_OtherDetailsGrid.Rows[e.Item.ItemIndex]["Amount"]);
                }
                else
                {
                    Total_Amount = Total_Amount + Convert.ToDecimal(Session_OtherDetailsGrid.Rows[e.Item.ItemIndex]["Amount"]);
                }
            }

            if (e.CommandName != "Delete")
            {
                if (Util.String2Decimal(txt_Amount.Text) > 0)
                {
                    if (ddl_AddLess.SelectedValue == "1")
                    {
                        Total_Amount = Total_Amount + Util.String2Decimal(txt_Amount.Text);
                    }
                    else
                    {
                        Total_Amount = Total_Amount - Util.String2Decimal(txt_Amount.Text);
                    }
                }
            }
        }

       TotalAmount = Total_Amount;

        if (e.CommandName == "Add" || e.CommandName == "Update" || e.CommandName == "Delete")
        {
            if (OtherDetailsGrid != null)
            {
                OtherDetailsGrid(this, e);
            }        
        }        
    }

    protected void dg_OtherChargeDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    public void SetLedgerID(string Ledger_Name, string Ledger_ID)
    {
        ddl_Ledger.DataTextField = "Ledger_Name";
        ddl_Ledger.DataValueField = "Ledger_Id";
        Raj.EC.Common.SetValueToDDLSearch(Ledger_Name, Ledger_ID,ddl_Ledger);
    }
}
