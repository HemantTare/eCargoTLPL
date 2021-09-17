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
/// Author :<Nilesh Kumar Jha>
/// Created On:<30/04/2008>
/// Description :This page is for Registration Permit
/// 
/// 
///  Author :<shiv Kumar mishra>
/// Created On:<08/05/2008>
/// Description :Modification in only Grid Control event
/// </summary>
public partial class Master_Vehicle_WucRegistrationPermit : System.Web.UI.UserControl, IRegistrationPermitView 
{
  
    #region ClassVariables
    RegistrationPermitPresenter objRegistrationPermitPresenter;
    ComponentArt.Web.UI.Calendar dtp_PermitValidFrom;
    ComponentArt.Web.UI.Calendar dtp_PermitValidUpto;
    DropDownList ddl_PermitState;
    TextBox txt_PermitNo;
    TextBox txt_PermitTaxAmount;
    TextBox txt_TemproryDocumentNo;
    LinkButton lbtnTaxDetails;
    TextBox txt_PermitReceiptNo;
    Raj.EC.Common objCommon = new Raj.EC.Common();
    ScriptManager scm_VehicleRegistrationPermit;
    DataSet objDS;
   
    private int Srno, DgRowCount;
    bool isValid = false;
    #endregion

    #region Controls Variable

    public int MainSrNo
    {
        set { hdn_Main_SrNo.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_Main_SrNo.Value); }
    }

    public int MainSrNoEdit
    {
        set { hdn_Main_SrNo_Edit.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_Main_SrNo_Edit.Value); }
    } 

    public int PermitTypeId
    {
        set{ddl_PermitType.SelectedValue = Util.Int2String(value);}
        get{return Util.String2Int(ddl_PermitType.SelectedValue);}
    }
    public string PermitNo
    {
        set{txt_RegPermitNo.Text =value ; }
        get { return txt_RegPermitNo.Text; }
    }
    public string PermitDocumentNo
    {
        set{txt_DocumentNo.Text = value;}
        get { return txt_DocumentNo.Text; }
    }
    public DateTime PermitValidFrom
    {
        set { dtp_RegPermitValidFrom.SelectedDate = value; }
        get { return dtp_RegPermitValidFrom.SelectedDate; }
    }
    public DateTime PermitValidUpTo
    {
        set { dtp_RegPermitValidUpTo.SelectedDate = value; }
        get { return dtp_RegPermitValidUpTo.SelectedDate; }
    }


    public string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;
        }
    }

    public string GriderrorMessage
    {
        set
        {
            lbl_Grid_Errors.Text = value;
        }
    }
    public int keyID
    {
        get{return Util.DecryptToInt(Request.QueryString["Id"]);}
    }

    public DataTable Bind_ddl_Permit_Type
    {
        set
        {
            ddl_PermitType.DataSource = value;
            ddl_PermitType.DataTextField = "Permit_Type";
            ddl_PermitType.DataValueField = "Permit_Type_ID";
            ddl_PermitType.DataBind();
            //ddl_PermitType.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable Bind_DDLPermitState
    {
        set
        {
            ddl_PermitState.DataSource = value;
            ddl_PermitState.DataTextField = "State_Name";
            ddl_PermitState.DataValueField = "State_ID";
            ddl_PermitState.DataBind();
            ddl_PermitState.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable SessionStateDropDown
    {
        get { return StateManager.GetState<DataTable>("SessionStateDropDown"); }
        set { StateManager.SaveState("SessionStateDropDown", value); }
    }

    public DataSet SessionPermitTaxDetails
    {
        get { return StateManager.GetState<DataSet>("RegistrationGrid"); }
        set { StateManager.SaveState("RegistrationGrid", value); }
    }
    public DataSet SessionTemparayRegistrationPermitGrid
    {
        get { return StateManager.GetState<DataSet>("TemparayRegistrationGrid"); }
        set { StateManager.SaveState("TemparayRegistrationGrid", value); }

    }
    #endregion

    #region OtherProperties
    public ScriptManager SetScriptManager
    {
        set { scm_VehicleRegistrationPermit = value; }
    }

    public String VehiclePermitTaxDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs = SessionPermitTaxDetails.Copy();
            _objDs.Tables[0].TableName = "Vehicle_Permit_Tax_Details";
            return _objDs.GetXml().ToLower();
        }
    }

    public String VehicleTemporaryPermitDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs = SessionTemparayRegistrationPermitGrid.Copy();
            _objDs.Tables[0].TableName = "Temporary_Permit_Details";
            return _objDs.GetXml().ToLower();
        }
    }

    private  int SessionMainSrNo
    {
        get { return StateManager.GetState<int>("Session_SrNo"); }
        set { StateManager.SaveState("Session_SrNo", value); }
    }
    #endregion

    #region OtherFunction
       private void BindDgRegistrationPermitGrid()
         {
           
            DataSet ds;
            ds = SessionPermitTaxDetails;
            if (ds.Tables[0].Rows.Count > 0)
            {
                string FE = "MainSrNo = 0";
                Dg_RegistrationPermit.DataSource = objCommon.Get_View_Table(SessionPermitTaxDetails.Tables[0], FE);
                Dg_RegistrationPermit.DataBind();
                DgRowCount = SessionPermitTaxDetails.Tables[0].Rows.Count;
            }
            else
            {
                Dg_RegistrationPermit.DataSource = SessionPermitTaxDetails;
                Dg_RegistrationPermit.DataBind();
            }
        }

    private void BindTemparayRegistrationPermitGrid()
    {
        Dg_TemparayRegistrationPermit.DataSource = SessionTemparayRegistrationPermitGrid;
        Dg_TemparayRegistrationPermit.DataBind();
    }
    #endregion

    #region IView

    public bool validateUI()
    {
        bool _IsValid = false;
        DataView dv = new DataView();
        lbl_Errors.Text = "";

        string FE = "MainSrNo = " + 0;
        dv = objCommon.Get_View_Table(SessionPermitTaxDetails.Tables[0], FE);

        if (PermitTypeId == 0 || PermitTypeId == -1)
        {
            lbl_Errors.Text = "Please Select Permit Type";// GetLocalResourceObject("MsgPermitType").ToString();
            scm_VehicleRegistrationPermit.SetFocus(ddl_PermitType);
        }
        else if (PermitNo == string.Empty)
        {
            lbl_Errors.Text = "Please Enter Permit No";// GetLocalResourceObject("MsgPermitNo").ToString();
            scm_VehicleRegistrationPermit.SetFocus(txt_RegPermitNo);
        }
        
        else if (PermitTypeId == 1 && dv.Count <= 2)
        {
            lbl_Errors.Text = "Please Enter At Least Three Records For National Permit";// GetLocalResourceObject("MsgNationalPermit").ToString(); 
        }
        else if (PermitTypeId == 2 && dv.Count <= 0)
        {
            lbl_Errors.Text = "Please Enter At Least One Record For Counter Permit";// GetLocalResourceObject("MsgCounterPermit").ToString();
        }
        else if (PermitValidUpTo < PermitValidFrom)
        {
            lbl_Errors.Text = "Valid UpTo  Should Be Greater Than Or Equal To Valid From";// GetLocalResourceObject("MsgValidUpto").ToString();
        }
        else if (ValidateProperDateRange(PermitValidFrom, PermitValidUpTo, SessionPermitTaxDetails, 0) == false) { }
        else if (ValidateDuplicatePermitNo(PermitNo, 1) == false) { }
        else if (ValidateDuplicatePermitNo(PermitNo, 3) == false) { }
        else
        {
            _IsValid = true;
        }

        return _IsValid;
    }

    #endregion

    #region RegistrationPermitGridEvent
    protected void Dg_RegistrationPermit_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        ddl_PermitState = (DropDownList)(e.Item.FindControl("ddl_PermitState"));
        dtp_PermitValidFrom = (ComponentArt.Web.UI.Calendar)(e.Item.FindControl("dtp_PermitValidFrom"));
        dtp_PermitValidUpto = (ComponentArt.Web.UI.Calendar)(e.Item.FindControl("dtp_PermitValidUpto"));
        txt_PermitReceiptNo = (TextBox)(e.Item.FindControl("txt_PermitReceiptNo"));
        txt_PermitTaxAmount = (TextBox)(e.Item.FindControl("txt_PermitTaxAmount"));

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                dtp_PermitValidFrom.SelectedDate = System.DateTime.Now;
                dtp_PermitValidUpto.SelectedDate = System.DateTime.Now;
            }
           
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                Bind_DDLPermitState = SessionStateDropDown;
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                objDS = SessionPermitTaxDetails;
                DataRow DR = objDS.Tables[0].Rows[e.Item.ItemIndex];

                LinkButton lbtn_Delete = (LinkButton)(e.Item.FindControl("lbtn_Delete"));
                lbtn_Delete.Enabled = false;

                ddl_PermitState.SelectedValue = DR["PermitStateId"].ToString();
                txt_PermitReceiptNo.Text = DR["ReceiptNo"].ToString();
                txt_PermitTaxAmount.Text = DR["TaxAmount"].ToString();
                dtp_PermitValidFrom.SelectedDate = Convert.ToDateTime(DR["ValidFrom"].ToString());
                dtp_PermitValidUpto.SelectedDate = Convert.ToDateTime(DR["ValidUpto"].ToString());
            }
        }
    }

    protected void Dg_RegistrationPermit_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        { 
            try
            {
                objDS = SessionPermitTaxDetails;
                DataColumn[] _dtColumnPrimaryKey;
                _dtColumnPrimaryKey = new DataColumn[1];
                _dtColumnPrimaryKey[0] = objDS.Tables[0].Columns["PermitStateID"];
                objDS.Tables[0].PrimaryKey = _dtColumnPrimaryKey;

             
                Insert_Update_Dataset_For_Registration_Permit(source, e);
                if (isValid == true)
                {
                    BindDgRegistrationPermitGrid();
                    Dg_RegistrationPermit.EditItemIndex = -1;
                    Dg_RegistrationPermit.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                GriderrorMessage = "Duplicate Permit State Name";
               /// scm_VehicleRegistrationPermit.SetFocus(ddl_PermitState);
            }
        }
    }

    private void Insert_Update_Dataset_For_Registration_Permit(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        ddl_PermitState = (DropDownList)(e.Item.FindControl("ddl_PermitState"));
        dtp_PermitValidFrom = (ComponentArt.Web.UI.Calendar)(e.Item.FindControl("dtp_PermitValidFrom"));
        dtp_PermitValidUpto = (ComponentArt.Web.UI.Calendar)(e.Item.FindControl("dtp_PermitValidUpto"));
        txt_PermitReceiptNo = (TextBox)(e.Item.FindControl("txt_PermitReceiptNo"));
        txt_PermitTaxAmount = (TextBox)(e.Item.FindControl("txt_PermitTaxAmount"));

        objDS = SessionPermitTaxDetails;
        DataRow DR = null;
        if (e.CommandName == "Add")
        {
            DR = objDS.Tables[0].NewRow();
        }
        else if (e.CommandName == "Update")
        {
            DR = objDS.Tables[0].Rows[e.Item.ItemIndex];
        }

        if (Allow_To_Add_Update_For_Registration_Permit() == true)
        {
            DR["MainSrNo"] = 0;
            DR["PermitStateId"] = ddl_PermitState.SelectedValue;
            DR["TaxAmount"] = txt_PermitTaxAmount.Text;
            DR["PermitStateName"] = ddl_PermitState.SelectedItem.Text;
            DR["ReceiptNo"] = txt_PermitReceiptNo.Text;
            DR["ValidFrom"] = dtp_PermitValidFrom.SelectedDate.ToString("dd MMMM yyyy");
            DR["ValidUpto"] = dtp_PermitValidUpto.SelectedDate;

            if (e.CommandName == "Add") {objDS.Tables[0].Rows.Add(DR);}
            SessionPermitTaxDetails = objDS;
        }
    }

    private bool Allow_To_Add_Update_For_Registration_Permit()
    {
        GriderrorMessage  = "";
        if (ddl_PermitState.SelectedValue == "0" || Util.String2Int(ddl_PermitState.SelectedValue) == -1)
        {
            GriderrorMessage  = "Please Select Permit State";
            scm_VehicleRegistrationPermit.SetFocus(ddl_PermitState);
        }
        else if (txt_PermitReceiptNo.Text == string.Empty)
        {
            GriderrorMessage = "Plase Enter Permit Receipt No";
            scm_VehicleRegistrationPermit.SetFocus(txt_PermitReceiptNo);

        }
        else if (txt_PermitTaxAmount.Text == string.Empty)
        {
            GriderrorMessage = "Please Enter Permit No";
            scm_VehicleRegistrationPermit.SetFocus(txt_PermitTaxAmount);
        }
        else if (dtp_PermitValidFrom.SelectedDate > dtp_PermitValidUpto.SelectedDate)
        {
            GriderrorMessage = "Valid UpTo  Should Be Greater Than Or Equal To Valid From";
        }
        else if (dtp_PermitValidFrom.SelectedDate < dtp_RegPermitValidFrom.SelectedDate || dtp_PermitValidUpto.SelectedDate > dtp_RegPermitValidUpTo.SelectedDate)
        {
            GriderrorMessage = "Please select Valid From and Valid UpTo in proper range.";
        }
        else if (ValidateDuplicateState(SessionTemparayRegistrationPermitGrid, Util.String2Int(ddl_PermitState.SelectedValue),0) == false) { }
        else
        {
            isValid = true;
        }

        return isValid;
    }

    protected void Dg_RegistrationPermit_EditCommand(object source, DataGridCommandEventArgs e)
    {

        Dg_RegistrationPermit.EditItemIndex = e.Item.ItemIndex;
        Dg_RegistrationPermit.ShowFooter = false;
        BindDgRegistrationPermitGrid();
    }

    protected void Dg_RegistrationPermit_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            objDS = SessionPermitTaxDetails;

            DataColumn[] _dtColumnPrimaryKey;
            _dtColumnPrimaryKey = new DataColumn[1];
            _dtColumnPrimaryKey[0] = objDS.Tables[0].Columns["PermitStateID"];
            objDS.Tables[0].PrimaryKey = _dtColumnPrimaryKey;

        Insert_Update_Dataset_For_Registration_Permit(source, e);

        if (isValid == true)
        {
            Dg_RegistrationPermit.EditItemIndex = -1;
            Dg_RegistrationPermit.ShowFooter = true;

            BindDgRegistrationPermitGrid();
        }
    }
    catch (ConstraintException)
    {
        GriderrorMessage = "Duplicate Permit State Name";
        scm_VehicleRegistrationPermit.SetFocus(ddl_PermitState);
    }
    }

    protected void Dg_RegistrationPermit_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        Dg_RegistrationPermit.EditItemIndex = -1;
        Dg_RegistrationPermit.ShowFooter = true;
        BindDgRegistrationPermitGrid();
    }

    protected void Dg_RegistrationPermit_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            objDS = SessionPermitTaxDetails;
            objDS.Tables[0].Rows.RemoveAt(e.Item.ItemIndex);
            objDS.Tables[0].AcceptChanges();
            SessionPermitTaxDetails = objDS;
            Dg_RegistrationPermit.EditItemIndex = -1;
            Dg_RegistrationPermit.ShowFooter = true;
            BindDgRegistrationPermitGrid();
        }
    }

    
#endregion

    #region TemproryRegistrationPermit
    protected void Dg_TemparayRegistrationPermit_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        ddl_PermitState = (DropDownList)(e.Item.FindControl("ddl_PermitState"));
        dtp_PermitValidFrom = (ComponentArt.Web.UI.Calendar)(e.Item.FindControl("dtp_PermitValidFrom"));
        dtp_PermitValidUpto = (ComponentArt.Web.UI.Calendar)(e.Item.FindControl("dtp_PermitValidUpto"));
        txt_PermitNo = (TextBox)(e.Item.FindControl("txt_PermitNo"));
        txt_PermitTaxAmount = (TextBox)(e.Item.FindControl("txt_PermitTaxAmount"));
        txt_TemproryDocumentNo = (TextBox)(e.Item.FindControl("txt_TemproryDocumentNo"));
        lbtnTaxDetails = (LinkButton)(e.Item.FindControl("lbtnTaxDetails"));

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            
            DataSet DS = SessionTemparayRegistrationPermitGrid;
            DataRow DR = null;
            DR = DS.Tables[0].Rows[e.Item.ItemIndex];

            Srno =Util.String2Int(DR["MainSrNo"].ToString());
            int StateID = Util.String2Int(DR["PermitStateId"].ToString());
            string StateName = DR["PermitStateName"].ToString();
            string PermitNumber = DR["PermitNo"].ToString();
            string PermitValidFrom = DR["ValidFrom"].ToString();
            string PermitValidUpTo = DR["ValidUpTo"].ToString();

            lbtnTaxDetails.Attributes.Add("onclick", "return newwindow('" + Srno + "','" + StateID + "','" + StateName + "','" + PermitNumber + "','" + PermitValidFrom + "','" + PermitValidUpTo + "','0')"); 
        }

        if (e.Item.ItemType == ListItemType.Footer)
        {
            dtp_PermitValidFrom.SelectedDate = System.DateTime.Now;
            dtp_PermitValidUpto.SelectedDate = System.DateTime.Now;
            lbtnTaxDetails.Attributes.Add("onclick", "return newwindow('0','" + ddl_PermitState.ClientID + "','pankaj','" + txt_PermitNo.ClientID + "'," + dtp_PermitValidFrom.ClientID  + "," + dtp_PermitValidUpto.ClientID + ",'1')"); 
        }

        if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
        {
            Bind_DDLPermitState = SessionStateDropDown;
        }

        if (e.Item.ItemType == ListItemType.EditItem)
        {
            objDS = SessionTemparayRegistrationPermitGrid;
            DataRow DR = objDS.Tables[0].Rows[e.Item.ItemIndex];
            LinkButton lbtn_Delete = (LinkButton)(e.Item.FindControl("lbtn_Delete"));

            lbtn_Delete.Enabled = false;
            lbtnTaxDetails.Enabled = false;

            ddl_PermitState.SelectedValue = DR["PermitStateId"].ToString();
            txt_PermitNo.Text = DR["PermitNo"].ToString();
            txt_TemproryDocumentNo.Text = DR["DocumentNo"].ToString();
            dtp_PermitValidFrom.SelectedDate = Convert.ToDateTime(DR["ValidFrom"].ToString());
            dtp_PermitValidUpto.SelectedDate = Convert.ToDateTime(DR["ValidUpto"].ToString());
        }
    }

    protected void Dg_TemparayRegistrationPermit_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            objDS = SessionTemparayRegistrationPermitGrid;
            try
            {
                DataColumn[] _dtColumnPrimaryKey;
                _dtColumnPrimaryKey = new DataColumn[1];
                _dtColumnPrimaryKey[0] = objDS.Tables[0].Columns["PermitStateID"];
                objDS.Tables[0].PrimaryKey = _dtColumnPrimaryKey;

                objDS.Tables[0].Columns["PermitNo"].Unique = true;
                

                Insert_Update_Dataset_For_TemparayRegistration_Permit(source, e);

                if (isValid == true)
                {
                    Srno = MainSrNo;
                    Srno = Srno + 1;
                    MainSrNo = Srno;

                    BindTemparayRegistrationPermitGrid();
                    Dg_TemparayRegistrationPermit.EditItemIndex = -1;
                    Dg_TemparayRegistrationPermit.ShowFooter = true;
                }
            }
            catch (ConstraintException ex)
            {
                if (ex.Message.Contains("PermitStateId") == true)
                {
                    GriderrorMessage = "Dulpicate Permit State Name";
                    scm_VehicleRegistrationPermit.SetFocus(ddl_PermitState);
                }
                else if (ex.Message.Contains("PermitNo") == true)
                {
                    GriderrorMessage = "Dulpicate Permit No";
                    scm_VehicleRegistrationPermit.SetFocus(txt_PermitNo);
                }
                //scm_VehicleRegistrationPermit.SetFocus(ddl_PermitState);
                return;
            }
        }
    }

    private void Insert_Update_Dataset_For_TemparayRegistration_Permit(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        ddl_PermitState = (DropDownList)(e.Item.FindControl("ddl_PermitState"));
        dtp_PermitValidFrom = (ComponentArt.Web.UI.Calendar)(e.Item.FindControl("dtp_PermitValidFrom"));
        dtp_PermitValidUpto = (ComponentArt.Web.UI.Calendar)(e.Item.FindControl("dtp_PermitValidUpto"));
        txt_PermitNo = (TextBox)(e.Item.FindControl("txt_PermitNo"));
        txt_TemproryDocumentNo = (TextBox)(e.Item.FindControl("txt_TemproryDocumentNo"));
        objDS = SessionTemparayRegistrationPermitGrid;

        int SrNoToCheck = 0;

        DataRow DR = null;
        if (e.CommandName == "Add")
        {
            DR = objDS.Tables[0].NewRow();
            DR["MainSrNo"] = MainSrNo;
            SrNoToCheck = MainSrNo;
        }
        else if (e.CommandName == "Update")
        {
            DR = objDS.Tables[0].Rows[e.Item.ItemIndex];
            MainSrNoEdit = Util.String2Int(DR["MainSrNo"].ToString());
            SrNoToCheck = MainSrNoEdit;
        }

        if (Allow_To_Add_Update_For_TemparayRegistration_Permit(SrNoToCheck) == true)
        {
           
            DR["PermitStateId"] = ddl_PermitState.SelectedValue;
            DR["PermitStateName"] = ddl_PermitState.SelectedItem.Text;
            DR["PermitNo"] = txt_PermitNo.Text;
            DR["DocumentNo"] = txt_TemproryDocumentNo.Text;
            DR["ValidFrom"]=dtp_PermitValidFrom.SelectedDate;
            DR["ValidUpto"] = dtp_PermitValidUpto.SelectedDate;
               
           if (e.CommandName == "Add") 
            {
                objDS.Tables[0].Rows.Add(DR);
            }
            SessionTemparayRegistrationPermitGrid = objDS;
        }
    }

    private bool Allow_To_Add_Update_For_TemparayRegistration_Permit(int SrNoToCheck)
    {
        isValid = false;
        GriderrorMessage = "";
        if (ddl_PermitState.SelectedValue == "0" || Util.String2Int(ddl_PermitState.SelectedValue) == -1)
        {
            GriderrorMessage = "Please Select Permit State";
            scm_VehicleRegistrationPermit.SetFocus(ddl_PermitState);
        }
        else if (txt_PermitNo.Text == string.Empty)
        {
            GriderrorMessage = "Please Enter Permit No";
            scm_VehicleRegistrationPermit.SetFocus(txt_PermitNo);
        }
        else if (txt_TemproryDocumentNo.Text == string.Empty)
        {

            GriderrorMessage = "Please Enter Document No";
            scm_VehicleRegistrationPermit.SetFocus(txt_TemproryDocumentNo);
        }
        else if (dtp_PermitValidFrom.SelectedDate > dtp_PermitValidUpto.SelectedDate)
        {
            GriderrorMessage = "Valid UpTo  Should Be Greater Than Or Equal To Valid From";
        }
        else if (ValidateProperDateRange(dtp_PermitValidFrom.SelectedDate, dtp_PermitValidUpto.SelectedDate, SessionPermitTaxDetails, SrNoToCheck) == false) { }

        else if (ValidateDuplicateState(SessionPermitTaxDetails, Util.String2Int(ddl_PermitState.SelectedValue), SrNoToCheck) == false) { }
     
        else if(ValidateDuplicatePermitNo(txt_PermitNo.Text.Trim(),2) == false){}
        else
        {
            isValid = true;
        }

        return isValid;
    }


    private bool ValidateDuplicateState(DataSet ds,int StateID,int MainSrNo)
    {
        bool isValid = true;
        DataView dv;
        string FQ  ="PermitStateId=" + StateID.ToString() + " And MainSrNo <>" + MainSrNo;
        dv = objCommon.Get_View_Table(ds.Tables[0], FQ);
        if (dv.Count > 0)
        {
            GriderrorMessage = "State is already selected";
            isValid = false;
        }

        return isValid;
    }

    private bool ValidateProperDateRange(DateTime ValidFrom, DateTime ValidUpTo, DataSet ds, int MainSrNo)
    {
        bool isValid = true;
        string FQ = string.Empty;
        DataView dv;
        dv = null;
        FQ = "MainSrNo=" + MainSrNo + " And (ValidFrom < '" + ValidFrom + "' Or ValidUpTo > '" + ValidUpTo + "')";
        dv = objCommon.Get_View_Table(ds.Tables[0], FQ);
        if (dv.Count > 0)
        {
            GriderrorMessage = "Valid From and Valid Upto are not in proper range";
            isValid = false;
        }
        return isValid;
    }

    private bool ValidateDuplicatePermitNo(string  GridPermitNo, int CheckType)
    {
        bool isValid = true;
        string FQ = string.Empty;
        DataView dv;
        dv = null;

        switch (CheckType)
        {
            case 1:
                {
                    FQ = "PermitNo='" + PermitNo + "'";
                    dv = objCommon.Get_View_Table(SessionTemparayRegistrationPermitGrid.Tables[0], FQ);
                    if (dv.Count > 0)
                        isValid = false;
                    break;
                }
            case 2:
                {
                    if (PermitNo.ToLower() == GridPermitNo.ToLower())
                        isValid = false;
                    break;
                }
            case 3:
                {
                    isValid = !(objRegistrationPermitPresenter.ValidateDuplicatePermitNo());
                    break;
                }
        }

        if (isValid == false)
            GriderrorMessage = "Duplicate Permit No.";
      
        return isValid;
    }

    protected void Dg_TemparayRegistrationPermit_EditCommand(object source, DataGridCommandEventArgs e)
    {
        //objDS = SessionTemparayRegistrationPermitGrid;
        //DataRow DR = null;
        //DR = objDS.Tables[0].Rows[e.Item.ItemIndex];
        //MainSrNoEdit = DR["MainSrNo"];

        Dg_TemparayRegistrationPermit.EditItemIndex = e.Item.ItemIndex;
        Dg_TemparayRegistrationPermit.ShowFooter = false;
        BindTemparayRegistrationPermitGrid();
    }

    protected void Dg_TemparayRegistrationPermit_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            objDS = SessionTemparayRegistrationPermitGrid;

            DataColumn[] _dtColumnPrimaryKey;
            _dtColumnPrimaryKey = new DataColumn[1];
            _dtColumnPrimaryKey[0] = objDS.Tables[0].Columns["PermitStateID"];
            objDS.Tables[0].PrimaryKey = _dtColumnPrimaryKey;

            Insert_Update_Dataset_For_TemparayRegistration_Permit(source, e);

            if (isValid == true)
            {
                Dg_TemparayRegistrationPermit.EditItemIndex = -1;
                Dg_TemparayRegistrationPermit.ShowFooter = true;

                BindTemparayRegistrationPermitGrid();
            }
        }
        catch (ConstraintException)
        {
            GriderrorMessage = "Dulpicate Permit State Name";
            ddl_PermitState.Focus();
            //scm_VehicleRegistrationPermit.SetFocus(ddl_PermitState);
            return;
        }
    }

    protected void Dg_TemparayRegistrationPermit_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        Dg_TemparayRegistrationPermit.EditItemIndex = -1;
        Dg_TemparayRegistrationPermit.ShowFooter = true;
        BindTemparayRegistrationPermitGrid();
    }

    protected void Dg_TemparayRegistrationPermit_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            objDS = SessionTemparayRegistrationPermitGrid;
            objDS.Tables[0].Rows.RemoveAt(e.Item.ItemIndex);
            objDS.Tables[0].AcceptChanges();
            SessionTemparayRegistrationPermitGrid = objDS;
            Dg_TemparayRegistrationPermit.EditItemIndex = -1;
            Dg_TemparayRegistrationPermit.ShowFooter = true;
            BindTemparayRegistrationPermitGrid();
        }
    }
    #endregion
   
    #region "ContrilEvents"
    protected void Page_Load(object sender, EventArgs e)
    {
        objRegistrationPermitPresenter = new RegistrationPermitPresenter(this, IsPostBack);
        if (!IsPostBack)
        {
            BindDgRegistrationPermitGrid();
            BindTemparayRegistrationPermitGrid();
        }
    }
}
    #endregion

