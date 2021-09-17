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
using Raj.EF.MasterPresenter;
using Raj.EF.MasterView;


/// <summary>
/// Author        : Ashish Lad
/// Created On    : 26/04/2008
/// Description   : This is the Form  For Master Vehicle Loan Details
/// </summary>

public partial class Master_Vehicle_WucVehicleLoanDetails : System.Web.UI.UserControl,IVehicleLoanDetailsView 
{

    #region ClassVariables
     public int SrNumber=1;
     private ScriptManager scm_VehicleLoanDetails;
     VehicleLoanDetailsPresenter objVehicleLoanDetailsPresenter;
     ComponentArt.Web.UI.Calendar dtp_DgDueDate;
     TextBox txt_DgChequeNo;
     DropDownList ddl_DgBankName;
     TextBox txt_DgPrincipleAmount;
     TextBox txt_DgInterest;
     TextBox txt_DgEMIAmount;
    #endregion


    #region Iview Implementation
    public int BankID
    {
        set{ddl_Bank_Name.SelectedValue = Util.Int2String(value);}
        get{return Util.String2Int(ddl_Bank_Name.SelectedValue);}
    }
    public string LoanAcctNo
    {
        set{txt_Loan_Acct_No.Text = value;}
        get{return txt_Loan_Acct_No.Text;}
    }
    public Decimal LoanAmount
    {
        set{txt_Loan_Amount.Text =Util.Decimal2String(value);}
        get{return Util.String2Decimal(txt_Loan_Amount.Text);}
    }
    public int TermsInMonths
    {
        set{txt_Terms_In_Months.Text =Util.Int2String(value);}
        get{return Util.String2Int(txt_Terms_In_Months.Text);}
    }
    public Decimal RateOfInterest
    {
        set{txt_Rate_Of_Interest.Text = Util.Decimal2String(value);}
        get{return Util.String2Decimal(txt_Rate_Of_Interest.Text);}
    }
    public Decimal EMIAmount
    {
        set{txt_EMI_Amount.Text = Util.Decimal2String(value);}
        get{return Util.String2Decimal(txt_EMI_Amount.Text);}
    }
    public int InterestTypeID
    {
        set{ddl_Interest_Type.SelectedValue = Util.Int2String(value);}
        get{return Util.String2Int(ddl_Interest_Type.SelectedValue);}
    }
    public int PaymentModeID
    {
        set{
              ddl_Payment_Mode.SelectedValue = Util.Int2String(value);
              if (value == 2)
              { 
                  txt_Start_Cheque_No.Style.Add("visibility","visible");
                  lbl_StartChequeNo.Style.Add("visibility", "visible");
              }
              else
              {
                  txt_Start_Cheque_No.Style.Add("visibility", "hidden");
                  lbl_StartChequeNo.Style.Add("visibility", "hidden");
              }
           }
        get{return Util.String2Int(ddl_Payment_Mode.SelectedValue);}
    }
    public DateTime FirstPaymentDue
    {
        set{dtp_FirstPaymentDue.SelectedDate = value;}
        get{return dtp_FirstPaymentDue.SelectedDate;}
    }
    public DateTime LastPaymentDue
    {
        set{lbl_Last_Payment_Due.Text = value.ToString();}
        get{return  dtp_FirstPaymentDue.SelectedDate.AddMonths(TermsInMonths-1);}
    }
    public string Comments
    {
        set{txt_Comments.Text = value;}
        get{return txt_Comments.Text;}
    }
    public int PaymentBankID
    {
        set{ddl_Payment_Bank.SelectedValue = Util.Int2String(value);}
        get{return Util.String2Int(ddl_Payment_Bank.SelectedValue);}
    }
    public int StartChequeNo
    {
        set{txt_Start_Cheque_No.Text = Util.Int2String(value);}
        get{return Util.String2Int(txt_Start_Cheque_No.Text);}
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }
    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]); 
           // return 128;
        }
    }

    public DataTable Bind_ddl_Bank_Name
    {
        set
        {
            ddl_Bank_Name.DataTextField = "Bank_Name";
            ddl_Bank_Name.DataValueField = "Bank_Id";
            ddl_Bank_Name.DataSource = value;
            ddl_Bank_Name.DataBind();
            //ddl_Bank_Name.Items.Insert(0, new ListItem("Select One", "0"));
            Raj.EC.Common.InsertItem(ddl_Bank_Name);
        }
    }
    public DataTable Bind_ddl_InterestType
    {
        set
        {
            ddl_Interest_Type.DataTextField = "Interest_Type";
            ddl_Interest_Type.DataValueField = "Interest_Type_ID";
            ddl_Interest_Type.DataSource = value;
            ddl_Interest_Type.DataBind();
            //ddl_Interest_Type.Items.Insert(0, new ListItem("Select One", "0"));
            Raj.EC.Common.InsertItem(ddl_Interest_Type);
        }
    }
    public DataTable Bind_ddl_PaymentMode
    {
        set
        {
            ddl_Payment_Mode.DataTextField = "EMI_Payment_Mode";
            ddl_Payment_Mode.DataValueField = "EMI_Payment_Mode_ID";
            ddl_Payment_Mode.DataSource = value;
            ddl_Payment_Mode.DataBind();
            //ddl_Payment_Mode.Items.Insert(0, new ListItem("Select One", "0"));
            Raj.EC.Common.InsertItem(ddl_Payment_Mode );
        }
    }
    public DataTable Bind_ddl_PaymentBank_Name
    {
        set
        {
            ddl_Payment_Bank.DataTextField = "Bank_Name";
            ddl_Payment_Bank.DataValueField = "Bank_Id";
            ddl_Payment_Bank.DataSource = value;
            ddl_Payment_Bank.DataBind();

            Raj.EC.Common.InsertItem(ddl_Payment_Bank);
            //ddl_Payment_Bank.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable SesssionPaymentDetailsDT
    {
        set { StateManager.SaveState("PaymentDetailsDS", value); }
        get { return StateManager.GetState<DataTable>("PaymentDetailsDS"); }
    }
    public DataTable SesssionBankNameDT
    {
        set { StateManager.SaveState("BankNameDT", value); }
        get { return StateManager.GetState<DataTable>("BankNameDT"); }
    }
    private DataTable Bind_ddl_DgBankName
    {
        set
        {
            ddl_DgBankName.DataTextField = "Bank_Name";
            ddl_DgBankName.DataValueField = "Bank_Id";
            ddl_DgBankName.DataSource = value;
            ddl_DgBankName.DataBind();
        }
    }
    public DataTable Bind_dg_Payment_Details
    {
        set
        {
            dg_Payment_Details.DataSource = value;

            if (PaymentModeID == 2)
                dg_Payment_Details.Columns[2].Visible = true;
            else
                dg_Payment_Details.Columns[2].Visible = false;

            dg_Payment_Details.DataBind();
            SesssionPaymentDetailsDT = value;

          
        }
    }
    #endregion


    #region other properties
    public String VehicleLoanDetailsXML
    {
        get
        {
            string returnvalue = "";

            DataSet _objDs = new DataSet();

            if (StateManager.Exist("PaymentDetailsDS"))
            {
                _objDs.Tables.Add(SesssionPaymentDetailsDT.Copy());
                _objDs.Tables[0].TableName = "Loan_Payment_Details";
                returnvalue = _objDs.GetXml().ToLower();
            }
            else
                returnvalue = "<newdataset />";

            return returnvalue;
        }
    }
    public ScriptManager SetScriptManager
    {
        set { scm_VehicleLoanDetails = value; }
    }
    #endregion


    #region Validation
    public bool validateUI()
    {
        bool _isValid = false;
        if (BankID !=0 && (ddl_Bank_Name.SelectedValue == "0" || Util.String2Int(ddl_Bank_Name.SelectedValue) == -1))
        {
            errorMessage = "Please Select Bank";// GetLocalResourceObject("MsgBank").ToString();
            scm_VehicleLoanDetails.SetFocus(ddl_Bank_Name);
        }
        else if (BankID !=0 && txt_Loan_Acct_No.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Loan Account No.";// GetLocalResourceObject("MsgLoanAccountNo").ToString();
            scm_VehicleLoanDetails.SetFocus(txt_Loan_Acct_No);
        }
        else if (BankID !=0 && txt_Loan_Amount.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Loan Amount.";// GetLocalResourceObject("MsgLoanAccount").ToString();
            scm_VehicleLoanDetails.SetFocus(txt_Loan_Amount);
        }
        else if (BankID !=0 && txt_Terms_In_Months.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Terms In Months";// GetLocalResourceObject("MsgTermsInMonth").ToString();
            scm_VehicleLoanDetails.SetFocus(txt_Terms_In_Months);
        }
        else if (BankID !=0 && txt_Rate_Of_Interest.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Rate Of Interest";// GetLocalResourceObject("MsgRateOfInterest").ToString();
            scm_VehicleLoanDetails.SetFocus(txt_Rate_Of_Interest);
        }
        else if (BankID != 0 && RateOfInterest > 100)
        {
            errorMessage = "Rate Of Interest Can not be Greater Than 100%";
            scm_VehicleLoanDetails.SetFocus(txt_Rate_Of_Interest);
        }
        else if (BankID !=0 &&  txt_EMI_Amount.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter EMI Amount";// GetLocalResourceObject("MsgEMIAmount").ToString();
            scm_VehicleLoanDetails.SetFocus(txt_EMI_Amount);
        }
        else if (BankID != 0 && (ddl_Interest_Type.SelectedValue == "0" || Util.String2Int(ddl_Interest_Type.SelectedValue) == -1))
        {
            errorMessage = "Please Select Interest Type";// GetLocalResourceObject("MsgInterestType").ToString();
            scm_VehicleLoanDetails.SetFocus(ddl_Interest_Type);
        }
        else if(BankID !=0 && (ddl_Payment_Mode.SelectedValue == "0" || Util.String2Int(ddl_Payment_Mode.SelectedValue) == -1))
        {
            errorMessage = "Please Select Payment Mode";// GetLocalResourceObject("MsgPaymentMode").ToString();
            scm_VehicleLoanDetails.SetFocus(ddl_Payment_Mode);
        }
        else if(BankID !=0 && (ddl_Payment_Mode.SelectedValue == "0" || Util.String2Int(ddl_Payment_Mode.SelectedValue) == -1))
        {
            errorMessage = "Please Select Payment Bank";// GetLocalResourceObject("MsgPaymentBank").ToString();
            scm_VehicleLoanDetails.SetFocus(ddl_Payment_Mode);
        }
        else if (BankID !=0 && dg_Payment_Details.Items.Count == 0)
        {
            errorMessage = "Please Generate Payment Details";// GetLocalResourceObject("MsgPaymentDetails").ToString();
        }
        else if (PaymentModeID == 2 && SesssionPaymentDetailsDT.Rows[0]["Cheque_No"].ToString() == "0")
        {
            errorMessage = "Please Generate Payment Details.";
        }
        else if (PaymentModeID  > 0 && PaymentModeID != 2 && Util.String2Int(SesssionPaymentDetailsDT.Rows[0]["Cheque_No"].ToString()) > 0)
        {
            errorMessage = "Please Generate Payment Details.";
        }
        else
        {
            _isValid = true;
        }

        return _isValid;
    }
    #endregion


    #region OtherMethods
    private DataTable makeDT()
    {
        DataTable objDT;

        objDT = SesssionPaymentDetailsDT;
        objDT.Rows.Clear();
        for (int i = 0; i < TermsInMonths; i++)
        {

            
            string Temp_str= dtp_FirstPaymentDue.SelectedDate.AddMonths(i).ToString("dd MMMM yyyy");

            DataRow objDR = objDT.NewRow();
            objDR["Sr_No"] = i + 1;
            objDR["Due_Date"] = Temp_str;
            if (PaymentModeID == 2)
            {
                objDR["Cheque_No"] = Util.String2Int(txt_Start_Cheque_No.Text) + i;
            }
            else
            {
                objDR["Cheque_No"] = "0";
            }
            objDR["Bank_ID"] = ddl_Payment_Bank.SelectedValue;
            objDR["Bank_Name"]=ddl_Payment_Bank.SelectedItem.Text;
            objDR["Principle_Amount"] = "0.00";
            objDR["Interest"]= "0.00";
            objDR["EMI_Amount"] = txt_EMI_Amount.Text;

            objDT.Rows.Add(objDR);
         }

        
         return objDT;
    }

    private void insertUpdateDataset(DataGridCommandEventArgs e)
    {
        DataTable objDT = SesssionPaymentDetailsDT;
        DataRow objDR=null;
        dtp_DgDueDate = (ComponentArt.Web.UI.Calendar)(e.Item.FindControl("dtp_DueDate"));
        txt_DgChequeNo = (TextBox)(e.Item.FindControl("txt_ChequeNo"));
        ddl_DgBankName = (DropDownList)(e.Item.FindControl("ddl_BankName"));
        txt_DgPrincipleAmount = (TextBox)(e.Item.FindControl("txt_PrincipleAmount"));
        txt_DgInterest = (TextBox)(e.Item.FindControl("txt_Interest"));
        txt_DgEMIAmount = (TextBox)(e.Item.FindControl("txt_EMIAmount"));
        

        if (e.CommandName == "Add")
        {
            objDR = objDT.NewRow();
        }
        else if (e.CommandName == "Update")
        {
            objDR = objDT.Rows[e.Item.ItemIndex];
        }


        if (validateGrid() == true)
        {
            objDR["Sr_No"] = e.Item.ItemIndex+1;
            objDR["Due_Date"] = dtp_DgDueDate.SelectedDate.ToString("dd MMMM yyyy");
            objDR["Cheque_No"] = txt_DgChequeNo.Text;
            objDR["Bank_Name"] = ddl_DgBankName.SelectedItem.Text;
            objDR["Bank_ID"] = ddl_DgBankName.SelectedValue;
            objDR["Principle_Amount"] = txt_DgPrincipleAmount.Text;
            objDR["Interest"] = 0.00;
            objDR["EMI_Amount"] = txt_DgEMIAmount.Text;

 
            if (e.CommandName == "Add") 
            {
                objDT.Rows.Add(objDR); 
            }
            else if (e.CommandName == "Update")
            {
                dg_Payment_Details.EditItemIndex = -1;
                dg_Payment_Details.ShowFooter = true;
            }

            Bind_dg_Payment_Details = objDT;
        }
    }

    private bool validateGrid()
    {
        bool _isValid=false;
        if (PaymentModeID==2 && txt_DgChequeNo.Text == string.Empty)
        {
            errorMessage = "Please Enter Cheque No";
            scm_VehicleLoanDetails.SetFocus(txt_DgChequeNo);
        }
        else if (ddl_DgBankName.SelectedValue == "0")
        {
            errorMessage = "Please Select Bank Name";
            scm_VehicleLoanDetails.SetFocus(ddl_DgBankName);
        }
        else if (txt_DgPrincipleAmount.Text == string.Empty)
        {
            errorMessage = "Please Enter Principle Amount";
            scm_VehicleLoanDetails.SetFocus(txt_DgPrincipleAmount);
        }
        else if (txt_DgInterest.Text == string.Empty)
        {
            errorMessage = "Please Enter Interest";
            scm_VehicleLoanDetails.SetFocus(txt_DgInterest);
        }
        else if (txt_DgEMIAmount.Text == string.Empty)
        {
            errorMessage = "Please Enter EMI Amount";
            scm_VehicleLoanDetails.SetFocus(txt_DgEMIAmount);
        }
        else
            _isValid = true;

        return _isValid;

     }
    #endregion

    #region  ControlsEvent
    protected void Page_Load(object sender, EventArgs e)
    {
        objVehicleLoanDetailsPresenter = new VehicleLoanDetailsPresenter(this, IsPostBack);
        ddl_Bank_Name.Attributes.Add("onclick", "Enable_LoanDetails()");
    }

    protected void btn_Generate_Click(object sender, EventArgs e)
    {       
         Bind_dg_Payment_Details = makeDT();        
    }
   

    #region GridControlsEvents
    protected void dg_Payment_Details_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Payment_Details.EditItemIndex = -1;
        Bind_dg_Payment_Details= SesssionPaymentDetailsDT;
    }

    protected void dg_Payment_Details_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Payment_Details.EditItemIndex = e.Item.ItemIndex;
        Bind_dg_Payment_Details = SesssionPaymentDetailsDT;
    }

    protected void dg_Payment_Details_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add" || e.CommandName == "Update")
         insertUpdateDataset(e);
    }

    protected void dg_Payment_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
          if (e.Item.ItemType == ListItemType.EditItem)
          {
              ddl_DgBankName = (DropDownList)(e.Item.FindControl("ddl_BankName"));
              Bind_ddl_DgBankName = SesssionBankNameDT;
              ddl_DgBankName.SelectedValue = SesssionPaymentDetailsDT.Rows[e.Item.ItemIndex]["Bank_ID"].ToString();
          }
    }

    protected void dg_Payment_Details_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        DataTable objDT = SesssionPaymentDetailsDT;
        objDT.Rows[e.Item.ItemIndex].Delete();
        objDT.AcceptChanges();
        Bind_dg_Payment_Details = objDT;
    }
    #endregion
    #endregion
}
