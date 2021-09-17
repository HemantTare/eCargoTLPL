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
using Raj.EC.FinanceView;
using Raj.EC.FinancePresenter;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC;
/// <summary>
/// Author        : Sunil Bhoyar
/// Created On    : 22/11/2008
/// Description   : This Is The Form For Leder CostCentre Details
/// </summary>

public partial class Finance_Accounting_Vouchers_VoucherCostCentre : System.Web.UI.UserControl
{
    #region ClassVariables
    Common objCommon = new Common();
    TextBox txt_Amount;
    DropDownList ddl_CostCentre;
    Label lbl_SrNo;
    #endregion
    #region ControlsValues

    public decimal Amount
    {
        set { txt_Amount.Text = value.ToString(); }
        get { return convertToDecimal(txt_Amount.Text); }
    }

    private int CostCentreId
    {
        get { return Convert.ToInt32(ddl_CostCentre.SelectedValue); }
        set { ddl_CostCentre.SelectedValue =value.ToString(); }
    }

    private int SrNo
    {
        get { return Util.String2Int(lbl_SrNo.Text); }
    }

    public decimal Debit
    {
        get { return convertToDecimal(Util.DecryptToDecimal(Request.QueryString["Debit"])); }
    }

    public decimal Credit
    {
        get { return convertToDecimal(Util.DecryptToDecimal(Request.QueryString["Credit"])); }
    }

    public string GetCostCentreXML
    {
        get
        {
               DataSet objDS = new DataSet();
               SessionCostCentre_New.TableName = "CostCentre";
               objDS.Tables.Add(SessionCostCentre_New.Copy());
               objDS.AcceptChanges();
               return objDS.GetXml();
        }
    }

    private object[] KeyArr
    {
        get 
        {
            object[] keyArr = new object[3];
            keyArr[0] = LedgerId;
            keyArr[1] = CostCentreId;
            keyArr[2] = SrNo;
            return keyArr;
        }
    }
    
    public string convertToDrCr(object value)
    {
        if (convertToDecimal(value)>0)
        { return "Dr"; }
        else{return "Cr";}
    }

    public string convertToAbs(object value)
    {
        return Convert.ToString(Math.Abs(Convert.ToDecimal(value)));
    }

    public decimal convertToDecimal(object value)
    {
        if (Convert.IsDBNull(value) || value.ToString().Trim() == string.Empty)
        { return 0;}
        else { return Convert.ToDecimal(value); }
    }

    public int convertToInt(object value)
    {
        if (value.ToString().Trim() == string.Empty)
        { return 0; }
        else { return Convert.ToInt32(value); }
    }

    public decimal TotalAmount
    {
        get { return convertToDecimal(SessionCostCentre_New.Compute("Sum(Amount)","Ledger_Id="+LedgerId.ToString()));}
    }

    #endregion

    #region ControlsBind



    public DataTable SessionCostCentreDT
    {
        set { StateManager.SaveState("VoucherCostCentre_DT", value); }
        get { return StateManager.GetState<DataTable>("VoucherCostCentre_DT"); }
    }


    public DataTable SessionCostCentre_New
    {
        set { StateManager.SaveState("VoucherCostCentreNew_DT", value); }
        get { return StateManager.GetState<DataTable>("VoucherCostCentreNew_DT"); }
    }

    public DataTable SessionDropDownCostCentre
    {
        set { StateManager.SaveState("DropDownCostCentre_DT", value); }
        get { return StateManager.GetState<DataTable>("DropDownCostCentre_DT"); }
    }

    public DataTable Bind_ddl_CostCentre
    {
        set
        {
            ddl_CostCentre.DataTextField = "Cost_Centre_Name";
            ddl_CostCentre.DataValueField = "Cost_Centre_ID";
            ddl_CostCentre.DataSource = value;
            ddl_CostCentre.DataBind();
        }
    }

    public DataTable Bind_dg_CostCentre
    {
        set
        {
            SessionCostCentre_New = value;

            dg_CostCentre.DataSource = value;
            dg_CostCentre.DataBind();

            //if (Debit > 0 && TotalAmount == Debit || Credit > 0 && TotalAmount == Credit)
            //{
            //    dg_CostCentre.ShowFooter = false;
            //}
            //else { dg_CostCentre.ShowFooter = true;}
        }
    }
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;

        if(Debit>0 && TotalAmount!=Debit||Credit>0 && TotalAmount!=Credit)
        {
            errorMessage = "Total Amount Not Match";
        }
        else
        {
            _isValid = true;
        }
        return _isValid;  
    }


    public string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;
        }
    }


    public int LedgerId
    {
        get
        {
           return Util.DecryptToInt(Request.QueryString["Ledger_Id"]);
           //return -1;
        }
    }

    public int VoucherId
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Voucher_Id"]);
            //return -1;
        }
    }

    public string LedgerName
    {
        get
        {
            return Util.DecryptToString(Request.QueryString["Ledger_Name"]);
           //return -1;
        }
    }
    
    #endregion


    #region OtherProperties

    
    

    #endregion


    #region OtherMethods


   
    private void InsertUpdateCostCentre(DataGridCommandEventArgs e)
    {
        findControls(e.Item);
        if (ValidateCostCentreValues() == false)
        { return; }

       // int costCentreIdCount=objCommon.Get_View_Table(SessionCostCentre_New,"Cost_Centre_ID="+CostCentreId.ToString()+"And Not Cost_Centre_ID="+SessionCostCentre_New.Rows.Find(SrNo)["Sr_No"].ToString());

        //if(e.CommandName=="Add" &&  costCentreIdCount>0 && )

        DataTable objDT = SessionCostCentre_New;
        DataRow objDR = null;

        if (e.CommandName == "Add")
        {
            objDR = objDT.NewRow();
        }
        else
        {
            objDR = objDT.Rows[e.Item.ItemIndex];
        }

        try
        {
            objDR["Cost_Centre_Name"] = ddl_CostCentre.SelectedItem.Text;
            objDR["Cost_Centre_ID"] = CostCentreId;
            objDR["Amount"] = Amount;

            if (e.CommandName == "Add")
            {
                //objDR["Details_Id"] = 0;
                objDR["Ledger_Id"] = LedgerId;
                objDT.Rows.Add(objDR);
            }

            if (e.CommandName == "Update")
            {
                dg_CostCentre.EditItemIndex = -1;
                dg_CostCentre.ShowFooter = true;
            }

            Bind_dg_CostCentre = objDT;
        }
        catch (ConstraintException)
        {

            if (e.CommandName == "Edit")
            {
                objDR.RejectChanges();
            }
            errorMessage = "Duplicate Cost Centre";
        }
    }

    private bool ValidateCostCentreValues()
    {
        bool _isValid = false;
        if (CostCentreId <=0)
        {
            errorMessage = "Please Select Cost Centre";
        }
        else if (Amount <= 0)
        {
            errorMessage = "Please Enter Amount";
        }
        else
        {
            _isValid = true;
        }
        return _isValid;
    }


    #endregion


    #region  ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {

        btn_Save.Attributes.Add("onclick", objCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save));

        if (!IsPostBack)
        {
            lbl_LedgerName.Text = LedgerName;
            lbl_Upto.Text = Debit > 0 ? Debit.ToString() + " Dr" : Credit.ToString() + " Cr";

            SessionCostCentre_New = objCommon.Get_View_Table(SessionCostCentreDT, "Ledger_Id=" + LedgerId).ToTable();
            Common.SetPrimaryKeys(new string[] {"Ledger_Id", "Cost_Centre_ID" }, SessionCostCentre_New);
            Bind_dg_CostCentre = SessionCostCentre_New;
        }
    }

   

    #endregion


    #region CostCentre_GridEvents
    protected void dg_CostCentre_ItemCommand(object source, DataGridCommandEventArgs e)
    {
     
        if (e.CommandName == "Add" || e.CommandName == "Update")
        {
            InsertUpdateCostCentre(e);
        }

        else if (e.CommandName == "Edit")
        {
            dg_CostCentre.EditItemIndex = e.Item.ItemIndex;
            Bind_dg_CostCentre = SessionCostCentre_New;
            dg_CostCentre.ShowFooter = false;
        }

        else if (e.CommandName == "Cancel")
        {
            dg_CostCentre.EditItemIndex = -1;
            Bind_dg_CostCentre = SessionCostCentre_New;
            dg_CostCentre.ShowFooter = true;
        }

        else if (e.CommandName == "Delete")
        {
            SessionCostCentre_New.Rows[e.Item.ItemIndex].Delete();
            SessionCostCentre_New.AcceptChanges();
            Bind_dg_CostCentre = SessionCostCentre_New;
        }
    }

    protected void dg_CostCentre_ItemDataBound(object source, DataGridItemEventArgs e)
    {
         if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
         {
             findControls(e.Item);
             Bind_ddl_CostCentre = SessionDropDownCostCentre;

             if (e.Item.ItemType == ListItemType.EditItem)
             {
                 LinkButton lnk_Delete = (LinkButton)e.Item.FindControl("lnk_Delete");
                 lnk_Delete.Visible = false;

                 DataRow Dr = SessionCostCentre_New.Rows[e.Item.ItemIndex];
                 CostCentreId =Util.String2Int(Dr["Cost_Centre_ID"].ToString());
             }
         }
    }

    #endregion



    private void findControls(DataGridItem item)
    {
        lbl_SrNo = (Label)item.FindControl("lbl_SrNo");
        ddl_CostCentre = (DropDownList)item.FindControl("ddl_CostCentre");
        txt_Amount = (TextBox)item.FindControl("txt_Amount");
    }


    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            foreach (DataRow Dr in SessionCostCentreDT.Rows)
            {
                if (Util.String2Int(Dr["Ledger_Id"].ToString()) == LedgerId)
                {
                    Dr.Delete();
                }
            }

            SessionCostCentreDT.AcceptChanges();
            SessionCostCentreDT.Merge(SessionCostCentre_New);
            SessionCostCentreDT.AcceptChanges();

            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx");
        }
    }
 
}














 


   
 

