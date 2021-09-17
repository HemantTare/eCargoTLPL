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
using System.Text;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.Security;
using Raj.EC.OperationPresenter;
using Raj.EC.OperationView;

public partial class Operations_Octroi_Update_WucOctroiRecoveryForm : System.Web.UI.UserControl, IOctroiRecoveryFormView
{
    #region ClassVariables
    OctroiRecoveryFormPresenter objOctroiRecoveryFormPresenter;
    Raj.EC.Common objComm = new Raj.EC.Common();
    DataTable objDT = new DataTable();
    DataSet objDS;
    Label lbl_GCNo;
    Label lbl_BookingDate;
    Label lbl_BookingBranch;
    Label lbl_DeliveryBranch;
    Label lbl_OctroiFormType;
    Label lbl_OctroiReceiptNo;
    Label lbl_OctroiAmount;
    DropDownList ddl_OctroiRecoveryForm;
    string _GC_No_XML;
    #endregion

    #region ControlsValues
    public DataTable SessionBindOctroiRecoveryFormGrid
    {
        get { return StateManager.GetState<DataTable>("OctroiRecoveryFormGrid"); }
        set
        {
            StateManager.SaveState("OctroiRecoveryFormGrid", value);
        }
    }
    public String OctroiRecoveryFormDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();

            _objDs.Tables.Add(SessionBindOctroiRecoveryFormGrid.Copy());

            _objDs.Tables[0].TableName = "OctroiRecoveryFormDetails";
            return _objDs.GetXml().ToLower();
        }
    }
    public String GetGCNoXML
    {
        get
        {
            if (_GC_No_XML != null)
            {
                return _GC_No_XML.ToString().ToLower();
            }
            else
            {
                return "<NewDataSet/>";
            }
        }
        set { _GC_No_XML = value; }
    }
    #endregion

    #region ControlsBind

    public DataTable BindOctroiRecoveryFormGrid
    {
        set
        {
            dg_OctroiUpdateRecoveryForm.DataSource = SessionBindOctroiRecoveryFormGrid;
            dg_OctroiUpdateRecoveryForm.DataBind();
        }
    }
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = true;
        return _isValid;
    }

    public string errorMessage
    {
        set { ; }
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }
    #endregion

    #region OtherMethod
    private void MakeDT()
    {
        DataTable objDT;

        int i = 0;

        if (dg_OctroiUpdateRecoveryForm.Items.Count > 0)
        {
            for (i = 0; i <= dg_OctroiUpdateRecoveryForm.Items.Count - 1; i++)
            {

                ddl_OctroiRecoveryForm = (DropDownList)dg_OctroiUpdateRecoveryForm.Items[i].FindControl("ddl_OctroiRecoveryForm");
                Label lbl_OctroiRecoveryFormId = (Label)dg_OctroiUpdateRecoveryForm.Items[i].FindControl("lbl_OctroiRecoveryFormId");

                SessionBindOctroiRecoveryFormGrid.Rows[i]["Octroi_Recovery_Form_Id"] = ddl_OctroiRecoveryForm.SelectedValue;
                SessionBindOctroiRecoveryFormGrid.Rows[i]["Octroi_Recovery_Form"] = ddl_OctroiRecoveryForm.SelectedItem.Text;
            }
            objDT = SessionBindOctroiRecoveryFormGrid;
        }
    }

    private void OnGetGCXML(object sender, EventArgs e)
    {
        if (WucSelectedItems1.EnterItem != string.Empty)
        {
            _GC_No_XML = WucSelectedItems1.GetSelectedItemsXML;
            objOctroiRecoveryFormPresenter.fillgrid();
            WucSelectedItems1.dtdetails = SessionBindOctroiRecoveryFormGrid;
            WucSelectedItems1.Get_Not_Selected_Items();
        }

    }
    private void SetStandardCaption()
    {
        const int GCNoCaption = 1;
        WucSelectedItems1.SetFoundCaption = "Enter  " + CompanyManager.getCompanyParam().GcCaption + "   Nos.:";
        WucSelectedItems1.SetNotFoundCaption = CompanyManager.getCompanyParam().GcCaption + "  Nos.Not Found :";
        WucSelectedItems1.Set_GCCaption = CompanyManager.getCompanyParam().GcCaption;
    }

    #endregion

    #region ControlEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        WucSelectedItems1.GetSelectedItemsXMLButtonClick += new EventHandler(OnGetGCXML);
        objOctroiRecoveryFormPresenter = new OctroiRecoveryFormPresenter(this, IsPostBack);
        btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save));

        if (!IsPostBack)
        {
            WucSelectedItems1.SetFoundCaption = "Please Select " + CompanyManager.getCompanyParam().GcCaption + "  No";
            WucSelectedItems1.SetNotFoundCaption = CompanyManager.getCompanyParam().GcCaption + "  No. Not Found";
            WucSelectedItems1.Set_GCCaption = CompanyManager.getCompanyParam().GcCaption;
        }
        SetStandardCaption();
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        MakeDT();
        objOctroiRecoveryFormPresenter.save();
    }

    #endregion

    #region GridEvents
    protected void dg_OctroiUpdateRecoveryForm_ItemDataBound(object sender, DataGridItemEventArgs e)
    {


        if (e.Item.ItemIndex != -1)
        {

            lbl_GCNo = (Label)(e.Item.FindControl("lbl_GCNo"));
            lbl_BookingDate = (Label)(e.Item.FindControl("lbl_BookingDate"));
            lbl_BookingBranch = (Label)(e.Item.FindControl("lbl_BookingBranch"));
            lbl_DeliveryBranch = (Label)(e.Item.FindControl("lbl_DeliveryBranch"));
            lbl_OctroiFormType = (Label)(e.Item.FindControl("lbl_OctroiFormType"));

            lbl_OctroiReceiptNo = (Label)(e.Item.FindControl("lbl_OctroiReceiptNo"));
            lbl_OctroiAmount = (Label)(e.Item.FindControl("lbl_OctroiAmount"));
            ddl_OctroiRecoveryForm = (DropDownList)(e.Item.FindControl("ddl_OctroiRecoveryForm"));

            if (Util.String2Bool(SessionBindOctroiRecoveryFormGrid.Rows[e.Item.ItemIndex]["Is_Oct_Recovered_From_Consignee"].ToString()) == true)
            {
                ddl_OctroiRecoveryForm.SelectedValue = "1";
            }
            else
            {
                ddl_OctroiRecoveryForm.SelectedValue = "0";
            }

        }
    }
    #endregion

    public void ClearVariables() // added Ankit
    {
        SessionBindOctroiRecoveryFormGrid = null;
    }
}
