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
using Raj.EC.OperationPresenter;
using Raj.EC.OperationView;

/// <summary>
/// Author        : Aashish Lad
/// Created On    : 20th October 2008
/// Description   : This is the Page For Operation Series Generation
/// </summary>

public partial class Document_Allocation_WucSeriesGeneration : System.Web.UI.UserControl,ISeriesGenerationView 
{
    #region ClassVariables
    SeriesGenerationPresenter objSeriesGenerationPresenter;
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    #endregion

    #region ControlsValue
    
    public int DocumentTypeID
    {
        set
        {
            ddl_DocumentType.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_DocumentType.SelectedValue);
        }
    }
    public int StartNo
    {
        set
        {
            txt_StartNo.Text  = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(txt_StartNo.Text);
        }
    }
    public int EndNo
    {
        set
        {
            txt_EndNo.Text = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(txt_EndNo.Text);
        }
    }
    public int MinStartNo
    {
        set
        {
            hdn_MinStartNo.Value  = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(hdn_MinStartNo.Value);
        }
    }
    public int MaxEndNo
    {
        set
        {
            hdn_MaxEndNo.Value = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(hdn_MaxEndNo.Value);
        }
    }
    public int Balance
    {        
        get
        {
            return (EndNo-StartNo)+ 1;
        }
    }
    public DateTime GeneratedDate
    {
        set
        {
            WucSeriesGeneratedDate.SelectedDate = value;
        }
        get
        {
            return WucSeriesGeneratedDate.SelectedDate;
        }
    }
    
    #endregion

    #region ControlsBind
    public DataTable Bind_ddl_DocumentType
    {
        set
        {
            ddl_DocumentType.DataSource = value;
            ddl_DocumentType.DataTextField = "Document_Name";
            ddl_DocumentType.DataValueField = "Document_Id";
            ddl_DocumentType.DataBind();
            ddl_DocumentType.Items.Insert(0, new ListItem("----Select One----", "0"));
        }
    }   
    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;
        if (DocumentTypeID == 0)
        {
            errorMessage = "Please Select Document Type";
            ddl_DocumentType.Focus();
        }
        else if (txt_StartNo.Text==string.Empty)
        {
            errorMessage = "Please Enter Start No";
             txt_StartNo.Focus();
        }
        else if (txt_EndNo.Text == string.Empty)
        {
            errorMessage = "Please Enter End No";
             txt_EndNo.Focus();
        }
        else if(StartNo > EndNo)
        {
            errorMessage = "Start No Should be Less then End No";
            txt_StartNo.Focus();
        }
        else if (CheckMinMaxNo()==false)
        {            
            txt_StartNo.Focus();
            return _isValid;            
        }
        else if (objSeriesGenerationPresenter.CheckDuplicate() == true)
        {
            errorMessage = "Duplicate Series";
            txt_StartNo.Focus();
            return _isValid;
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

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }

    #endregion

    #region OtherMethods
    private bool CheckMinMaxNo()
    {
        if (keyID > 0)
        {
            if ((StartNo > MinStartNo || EndNo < MaxEndNo) && (MinStartNo > 0 && MaxEndNo > 0))
            {
                errorMessage = "Start No Should not be Less than or Equal to ' " +MinStartNo  +"' And End No Should be Greater then '" + MaxEndNo+"' ";
                return false;
            }
        }
        return true;
    }
    #endregion

    #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        btn_Save.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save));

        objSeriesGenerationPresenter = new SeriesGenerationPresenter(this, IsPostBack);
        if(!IsPostBack)
        {
            if (keyID > 0)
            {
                ddl_DocumentType.Enabled = false;
            }
            else
            {
                txt_StartNo.MaxLength = Util.String2Int(ObjCommon.EC_Common_Pass_Query("select GC_No_Length from EC_Master_Company_GC_Parameter").Tables[0].Rows[0][0].ToString());
                txt_EndNo.MaxLength = txt_StartNo.MaxLength;
            }
        }
    }
    
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objSeriesGenerationPresenter.Save();
    }
    #endregion
}
