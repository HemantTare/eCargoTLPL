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
/// author Nilesh Kumar Jha
/// created on 21rd may 08
/// this is user control  for TemporaryPerniTaxDetail 
/// </summary>
public partial class Master_Vehicle_WucTemporaryPermitTaxDetails : System.Web.UI.UserControl,IView 
{
    private DataSet objDs;
    #region ClassVariables
    //private TemporaryPermitTaxDetailsPresenter objTemporaryPermitTaxDetailsPresenter;
    private int MainSrNo;
    private DateTime PermitValidFrom, PermitValidUpTo;
    #endregion
    
#region ControlValue
    private int StateID 
     {
         get { return Util.String2Int(hdn_State_ID.Value); }
        set { hdn_State_ID.Value = Util.Int2String(value); }
     }

    private string StateName
    {
        get { return lbl_StateName.Text; }
        set { lbl_StateName.Text = value; }
    }

    private string PermitNumber
    {
        get { return lbl_Permit_Number.Text; }
        set { lbl_Permit_Number.Text = value; }
    }

    private string ReceiptNo
     {
       get { return txt_ReceiptNo.Text; }
       set { txt_ReceiptNo.Text = value; }
     }
    private decimal TaxAmount 
     {
         get { return Util.String2Decimal(txt_TaxAmount.Text); }
         set {txt_TaxAmount.Text=Util.Decimal2String(value);}
     }
    private DateTime ValidFrom 
    {
        get { return (Dtp_ValidFrom.SelectedDate); }
        set { Dtp_ValidFrom.SelectedDate = value; }
    }
    private DateTime ValidUpto 
    {
        get { return Dtp_ValidUpTo.SelectedDate; }
        set { Dtp_ValidUpTo.SelectedDate = value; }
    }
    public DataTable SessionStateDropDown
    {
        get { return StateManager.GetState<DataTable>("SessionStateDropDown"); }
        set { StateManager.SaveState("SessionStateDropDown", value); }
    }
    private DataSet SessionRegistrationGrid
    {
        get { return StateManager.GetState<DataSet>("RegistrationGrid"); }
        set { StateManager.SaveState("RegistrationGrid", value); }
    }

 

    
    #endregion

#region ControlBind
     // public  DataTable Fill_DDL_State 
     //  {
     //    set
     //       {
     //        ddl_State.DataSource = value;
     //        ddl_State.DataTextField = "State_Name";
     //        ddl_State.DataValueField = "State_ID";
     //        ddl_State.DataBind();
     //        ddl_State.Items.Insert(0, new ListItem("Select One", "0"));
     //       }
     //}
    #endregion

#region IView
public  string errorMessage
{
    set{lbl_Errors.Text = value;}
}
public  int keyID 
{
    get { return -1; }
}

    public bool validateUI()
    {
        return true;
    }
#endregion

#region ControlsEvent
    protected void Page_Load(object sender, EventArgs e)
    {
        //objTemporaryPermitTaxDetailsPresenter = new TemporaryPermitTaxDetailsPresenter(this, IsPostBack);
        MainSrNo =Util.String2Int(Request.QueryString["SrNo"].ToString());
        StateID = Util.String2Int(Request.QueryString["StateID"].ToString());
        StateName = Request.QueryString["StateName"].ToString();
        PermitNumber = Request.QueryString["PermitNumber"].ToString();
        PermitValidFrom = Convert.ToDateTime(Request.QueryString["PermitValidFrom"].ToString());
        PermitValidUpTo = Convert.ToDateTime(Request.QueryString["PermitValidUpTo"].ToString());

         if (!IsPostBack)
         {
             SetValues();
         }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
      {
          if (Allow_To_Add_Update() == true)
          {
              InsertUpdateDataset();
          }
      }
    #endregion
      
#region OtherFunction
      private void SetValues()
    {
        hdn_Editable.Value = "False";
        objDs = SessionRegistrationGrid;

        DataRow[] DR = objDs.Tables[0].Select("MainSrNo = " + MainSrNo.ToString());

        if (DR.Length > 0)
        {
            StateID = Util.String2Int(DR[0]["PermitStateId"].ToString());
            StateName = DR[0]["PermitStateName"].ToString();
            TaxAmount = Util.String2Decimal(DR[0]["TaxAmount"].ToString());
            ReceiptNo = DR[0]["ReceiptNo"].ToString();
            ValidFrom = Convert.ToDateTime(DR[0]["ValidFrom"].ToString());
            ValidUpto = Convert.ToDateTime(DR[0]["ValidUpto"].ToString());
            hdn_Editable.Value = "True";
        }
    }

    public bool Allow_To_Add_Update()
    {
        bool _isValid = false;
        if (StateID == 0 || StateID == -1)
        {
            errorMessage = "Plase Select State";
        }
        else if (TaxAmount <= 0)
        {
            errorMessage = "Please Enter Tax Amount";
            txt_TaxAmount.Focus();
        }
        else if (ReceiptNo == string.Empty)
        {
            errorMessage = "Please Enter ReceiptNo";
            txt_ReceiptNo.Focus();
        }
        else if (Dtp_ValidFrom.SelectedDate > Dtp_ValidUpTo.SelectedDate)
        {
            errorMessage = "Valid From cannot be greater then Valid UpTo Date";
            Dtp_ValidFrom.Focus();
        }
        else if (Dtp_ValidFrom.SelectedDate < PermitValidFrom || Dtp_ValidUpTo.SelectedDate > PermitValidUpTo)
        {
            errorMessage = "Valid From and Valid UpTo Date are not in proper range.";
            Dtp_ValidFrom.Focus();
        }
        else
        {
            _isValid = true;
        }
        return _isValid;
    }

    private void InsertUpdateDataset()
    {
        objDs = SessionRegistrationGrid;

        try
        {
            if (hdn_Editable.Value.ToLower() == "false")
            {
                DataRow DR = null;
                DR = objDs.Tables[0].NewRow();
                DR["MainSrNo"] = MainSrNo;
                DR["PermitStateId"] = StateID;
                DR["PermitStateName"] = lbl_StateName.Text;
                DR["TaxAmount"] = TaxAmount;
                DR["ReceiptNo"] = ReceiptNo;
                DR["ValidFrom"] = ValidFrom;
                DR["ValidUpto"] = ValidUpto;
                objDs.Tables[0].Rows.Add(DR);
            }
            else
            {
                DataRow[] DR = objDs.Tables[0].Select("MainSrNo = " + MainSrNo.ToString());
                DR[0]["PermitStateId"] = StateID;
                DR[0]["PermitStateName"] = lbl_StateName.Text;
                DR[0]["TaxAmount"] = TaxAmount;
                DR[0]["ReceiptNo"] = ReceiptNo;
                DR[0]["ValidFrom"] = ValidFrom;
                DR[0]["ValidUpto"] = ValidUpto;
            }

            SessionRegistrationGrid = objDs;
            Response.Redirect("~/Display/CloseForm.aspx");
        }
        catch (ConstraintException)
        {
            errorMessage = "Duplicate Permit State Name";
        }
    }

   
    #endregion
}

