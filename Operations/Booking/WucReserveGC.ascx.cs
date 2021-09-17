using System;
using System.Data;
using System.Web.UI.WebControls;

using ClassLibraryMVP;
using Raj.EC.OperationPresenter;
using Raj.EC.OperationView;
using Raj.EC;

/// <summary>
/// Author        : Shiv kumar mishra
/// Created On    : 15/10/2008
/// Description   : This Page is For Reserve GC
/// </summary>
/// 

public partial class Operations_Booking_WucReserveGC : System.Web.UI.UserControl,IReserveGCView
{
    #region ClassVariables
    ReserveGCPresenter objReserveGCPresenter;
    Common ObjCommon = new Raj.EC.Common();
    #endregion

    #region ControlsValues


    public string BrnachName
    {
        set { lbl_Branch.Text = UserManager.getUserParam().MainName;}
    }

    public string ConsignorId
    {
        get { return DDLConsignor.SelectedValue; }
    }


    public string ConsignorName
    {
        get { return DDLConsignor.SelectedText; }
    }

    public int GCTypeId
    {
        get { return Util.String2Int(ddl_GcType.SelectedValue); }
    }

    public int VAId
    {
        get { return Util.String2Int(ddl_VA.SelectedValue); }
    }

    public int ReservedReasonId
    {
        get { return Util.String2Int(ddl_Reason.SelectedValue); }
    }
    public int GCNoFrom
    {
        //get { return lbl_GCNoFrom.Text; }
        //set {lbl_GCNoFrom.Text = value; }

        get { return Util.String2Int(txt_GcNoFrom.Text); }
        set { txt_GcNoFrom.Text = Util.Int2String(value);
              lbl_CurrentGCDisplay.Text = Util.Int2String(value);
             }
    }

    public int DocumentSeriesAllocationId
    {
        get { return Util.String2Int(hdn_Document_Allocation_Id.Value); }
        set { hdn_Document_Allocation_Id.Value = Util.Int2String(value); }
    }
    public int StartGCNo
    {
        get { return Util.String2Int(hdn_Start_GC_No.Value); }
        set { hdn_Start_GC_No.Value = Util.Int2String(value); }
    }
    public int EndGCNo
    {
        get { return Util.String2Int(hdn_End_GC_No.Value); }
        set { hdn_End_GC_No.Value = Util.Int2String(value); }
    }

    //Added:Anita Date:13/01/08
    //**********************************************
    
    public int GCNoTo
    {
        get { return Util.String2Int(txt_GCNoTo.Text); }
        set { txt_GCNoTo.Text = Util.Int2String(value); }
    }
    //**********************************************

    public int NoOfGC
    {
        get { return Util.String2Int(txt_NoOfGC.Text); }
        set { txt_NoOfGC.Text = Util.Int2String(value); }
    }
        
    public int GC_No_Length
    {
        set {hdn_GC_No_Length.Value = Util.Int2String(value);}
        get{return Util.String2Int(hdn_GC_No_Length.Value) <= 0 ? 0 : Util.String2Int(hdn_GC_No_Length.Value); 
        }
    }

    #endregion

    #region ControlsBind

    public DataTable BindGCType
    {
        set
        {
            ddl_GcType.DataTextField = "Document_Name";
            ddl_GcType.DataValueField = "Document_Id";
            ddl_GcType.DataSource = value;
            ddl_GcType.DataBind();
        }
    }

    public DataTable BindVA
    {
        set
        {
            ddl_VA.DataTextField = "VA_Name";
            ddl_VA.DataValueField = "VA_ID";
            ddl_VA.DataSource = value;
            ddl_VA.DataBind();
        }
    }

    public DataTable BindReason
    {
        set
        {
            ddl_Reason.DataTextField = "Reason";
            ddl_Reason.DataValueField = "Reason_ID";
            ddl_Reason.DataSource = value;
            ddl_Reason.DataBind();
            ddl_Reason.Items.Insert(0, new ListItem("Select One Reason", "0"));
        }
    }

    #endregion

    #region IView
    public bool validateUI()
    {
        errorMessage = "";
        bool _isValid = false;

        if (GCTypeId <= 0)
        {
            errorMessage = GetLocalResourceObject("Msg_ddl_GcType").ToString();
            ddl_GcType.Focus();
        }
        else if (ConsignorId == string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_ddl_Consignor").ToString();
        }       
        else if (txt_GcNoFrom.Text.Trim()==string.Empty)
        {
            errorMessage = "Please enter " + CompanyManager.getCompanyParam().GcCaption + " From Value";
            txt_GcNoFrom.Focus();
        }
        //Added: Anita Date: 13/01/08
        //*****************************************
        else if(Util.String2Int(txt_GcNoFrom.Text) > Util.String2Int(txt_GCNoTo.Text) && CompanyManager.getCompanyParam().IsGcNumberEditable == true)
        {
            errorMessage = CompanyManager.getCompanyParam().GcCaption + " From Value Should be Less than or equal with " + CompanyManager.getCompanyParam().GcCaption + "To Value";
            txt_GcNoFrom.Focus();
        }
        else if (Util.String2Int(txt_NoOfGC.Text) <= 0 && CompanyManager.getCompanyParam().IsGcNumberEditable == false)
        {
            errorMessage="No Of " + CompanyManager.getCompanyParam().GcCaption + " should be greater than zero";
        }
        else if (Util.String2Int(txt_GcNoFrom.Text) < StartGCNo || Util.String2Int(txt_GcNoFrom.Text) > EndGCNo)
        {
            errorMessage = CompanyManager.getCompanyParam().GcCaption + " From Value does not fall in Valid Range(i.e." + StartGCNo+ "-"+ EndGCNo +")";
            txt_GCNoTo.Focus();
        }
        else if (Util.String2Int(txt_GCNoTo.Text) < StartGCNo || Util.String2Int(txt_GCNoTo.Text) > EndGCNo)
        {
            errorMessage = CompanyManager.getCompanyParam().GcCaption + " To Value does not fall in Valid Range(i.e." + StartGCNo + "-" + EndGCNo + ")";
            txt_GcNoFrom.Focus();
        }
        else
        {
            _isValid = true;
        }
        return _isValid;
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
            //return 12;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        DDLConsignor.DataTextField = "Client_Name";
        DDLConsignor.DataValueField = "Client_ID";

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        objReserveGCPresenter = new ReserveGCPresenter(this, IsPostBack);
        ObjCommon.Disable_save_button_on_click(Page, btn_Save, "Allow_To_Save()");

        txt_GcNoFrom.MaxLength = GC_No_Length;
        txt_GCNoTo.MaxLength = GC_No_Length;

        DDLConsignor.OtherColumns = UserManager.getUserParam().MainId.ToString();

        if (!IsPostBack)
        {
            lbl_Branch.Text = UserManager.getUserParam().MainName;
            GetGcNumber();
            
            hdf_ResourecString.Value = ObjCommon.GetResourceString("Operations/Booking/App_LocalResources/WucReserveGC.ascx.resx");
            ObjCommon.CheckFormRights(btn_Save);

            // Added : Anita On: 13 Feb 09
            if(UserManager.getUserParam().HierarchyCode != "BO")
                lbl_Errors.Text="You are not authorized to Save Reserved " + CompanyManager.getCompanyParam().GcCaption + ".";                
         }
         if (CompanyManager.getCompanyParam().IsGcNumberEditable == true)
         {
             tr_ReserveGCFromandTo.Visible = false;
             tr_txtGCFromandTo.Visible = true;
         }
         else
         {
             tr_ReserveGCFromandTo.Visible = true;
             tr_txtGCFromandTo.Visible = false;
         }

         SetStandardCaption();


    }
    private void SetStandardCaption()
    {
        lbl_GcType.Text = CompanyManager.getCompanyParam().GcCaption + "  Type:";
        lbl_GCNoFrom.Text = CompanyManager.getCompanyParam().GcCaption + "  No From:";
        lbl_GCNoTo.Text = CompanyManager.getCompanyParam().GcCaption + "  No To:";
        lbl_CurrentGC.Text = " Current " + CompanyManager.getCompanyParam().GcCaption + ":";
        lbl_NoOfGC.Text = "No Of " + CompanyManager.getCompanyParam().GcCaption + ":";
    }

    private void GetGcNumber()
    {
        int Document_Series_Allocation_ID=0, Start_No=0, End_No=0, Next_No=0;

        ObjCommon.Get_Document_Allocation_Details(ref Document_Series_Allocation_ID, ref Start_No,
            ref End_No, ref Next_No,VAId, 0, GCTypeId);

        GCNoFrom = Next_No;
        DocumentSeriesAllocationId = Document_Series_Allocation_ID;
        StartGCNo = Start_No;
        EndGCNo = End_No;
        GCNoTo = Next_No;

        if (DocumentSeriesAllocationId == 0)
        {
            Common.DisplayErrors(1013);
        }
    }

    private void SetGcNoTo()
    {
        GCNoTo = (GCNoFrom + NoOfGC) - 1;
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {        
        if (validateUI() == true)
        {
            if (CompanyManager.getCompanyParam().IsGcNumberEditable == false)
            {
                SetGcNoTo();
            }
            objReserveGCPresenter.Save();
        }
    }

    protected void ddl_GcType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetGcNumber();
    }
}
