using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC.OperationPresenter;
using Raj.EC.OperationView;
using Raj.EC;

/// <summary>
/// Author : Ankit champaneriya
/// Date   : 08-01-09
/// </summary>
/// 

public partial class Operations_Inward_Updates_FrmGCConsigneeUpdate : System.Web.UI.Page, IGCConsigneeListView
{
    #region Declaration
    private GCConsigneeListPresenter objGCConsigneeListPresenter;
    private DataTable objDT;
    Common objComm = new Common();

    #endregion

    #region Properties

    public int GC_ID
    {
        get { return Util.String2Int(Request.QueryString["GC_ID"]); }
    }

    public int BranchId
    {
        get { return Util.String2Int(Request.QueryString["BranchID"]); } 
    }

    public int NewClientId
    {
        set { hdn_ConsigneeId.Value = value.ToString(); }
        get { return Util.String2Int(hdn_ConsigneeId.Value); }
    }
    public int isRegularClient
    {
        set { hdn_IsRegularConsignee.Value = value.ToString(); }
        get { return Util.String2Int(hdn_IsRegularConsignee.Value); }
    }

    public int ToLocationId
    {
        set { hdn_ToLocationId.Value = value.ToString(); }
        get { return Util.String2Int(hdn_ToLocationId.Value); }
    }

    public DataTable SessionGConsigneeList
    {
        get { return objDT; }
        set { objDT = value; }
    }

    public String GetBranchXML
    {
        get { return ""; }
        set { string _GC_No_XML = value; }
    }

    ////public string ClientId
    ////{
    ////    get { return ddl_Client.SelectedValue; }
    ////}

    ////public string ClientName
    ////{
    ////    get { return ddl_Client.SelectedText; }
    ////}
    public string ClientName
    {
        get { return txt_ConsigneeName.Text; }
    }

    public string ContactPerson
    {
        set { txt_ContactPerson.Text = value; }
    }

    public string Add1
    {
        get { return txt_Address1.Text; }
        set { txt_Address1.Text = value; }
    }

    public string Add2
    {
        get { return txt_Address2.Text; }
        set { txt_Address2.Text = value; }
    }
    public string pincode
    {
        get { return txt_Pincode.Text; }
        set { txt_Pincode.Text = value; }
    }
    public string stdcode
    {
        get { return txt_StdCode.Text; }
        set { txt_StdCode.Text = value; }
    }
    public string phone
    {
        get { return txt_Phone.Text; }
        set { txt_Phone.Text = value; }
    }
    public string mobile
    {
        get { return txt_Mobile.Text; }
        set { txt_Mobile.Text = value; }
    }
    public string csttinno
    {
        get { return txt_CSTTinNo.Text; }
        set { txt_CSTTinNo.Text = value; }
    }
    public string servicetaxNo
    {
        get { return txt_ServiceTaxNo.Text; }
        set { txt_ServiceTaxNo.Text = value; }
    }
    public string Reason
    {
        get { return txt_Reason.Text; }
        set { txt_Reason.Text = value; }
    }

    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;
        //////TextBox txt_Client;
        //////txt_Client = (TextBox)ddl_Client.FindControl("txtBoxddl_Client");

        //if (ClientId == "") 
        if (hdn_ConsigneeId.Value == "")
        {
            errorMessage = "Please Select Consignee";
            scm_GCConsigneeUpdate.SetFocus(txt_ConsigneeName);
            ////scm_GCConsigneeUpdate.SetFocus(txt_Client);
        }
        else if (Reason == String.Empty)
        {
            errorMessage = "Please Enter Reason";
             scm_GCConsigneeUpdate.SetFocus(txt_Reason);
        }
        else
            _isValid = true;

        return _isValid;
    }

    public string errorMessage
    {
        set { lbl_Error.Text = value; }
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }
    #endregion

    #region Event Code

    protected void Page_Load(object sender, EventArgs e)
    {
        tr_csttinno.Visible = false;
        tr_serviceTaxNo.Visible = false;
        tr_ContactPerson.Visible = false;
        lbl_GCno_Display.Text = Request.QueryString["GC_No"].ToString();
        lbl_ConsigneeNameDisplay.Text = Request.QueryString["Consignee_Name"].ToString();
        
        //////ddl_Client.DataTextField = "Client_Name";
        //////ddl_Client.DataValueField = "Client_ID";

        btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save));

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EC.OperationModel.NewGCSearch));
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        objGCConsigneeListPresenter = new GCConsigneeListPresenter(this, IsPostBack);
        SetStandardCaption();

        //////ddl_Client.OtherColumns = Util.Int2String(BranchId);
        ToLocationId = BranchId;

    }
    private void SetStandardCaption()
    {
        lbl_GC_No.Text = CompanyManager.getCompanyParam().GcCaption + "  No :";
    }

    private void updateconsigneegrid()
    {
        string popupScript = "<script language='javascript'>updateconsigneegrid();</script>";

        System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel2, typeof(String), "PopupScript", popupScript.ToString(), false);
    }

    //////protected void ddl_Client_TxtChange(object sender, EventArgs e)
    //////{
    //////    string[] splitted = ddl_Client.SelectedValue.Split(new char[] { '*' });
    //////    int Client_Id = Util.String2Int(splitted[0]);

    //////    if (Client_Id >= 0)
    //////    {
    //////        objGCConsigneeListPresenter.fillClientDetails();
    //////    }
    //////    else
    //////    {
    //////        ClearControls();
    //////    }

    //////    Update_updatePanel();
    //////}
    public void ClearControls()
    {
        Add1 = "";
        Add2 = "";
        pincode = "";
        stdcode = "";
        mobile = "";
        csttinno = "";
        ContactPerson = "";
        servicetaxNo = "";
        phone = "";
    }
    private void Update_updatePanel()
    {
        UpdatePanel2.Update();
        UpdatePanel3.Update();
        UpdatePanel4.Update();
        UpdatePanel10.Update();
        UpdatePanel5.Update();
        UpdatePanel6.Update();
        UpdatePanel9.Update();

    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            objGCConsigneeListPresenter.Save();
            //////updateconsigneegrid();

            string popupScript2 = "<script language='javascript'>{updateconsigneegrid();self.close()}</script>";
            System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(String), "PopupScript2", popupScript2.ToString(), false);
        }
    }

    #endregion

    public void ClearVariables() // added Ankit
    {
        //SessionGConsigneeList = null;
    }
}