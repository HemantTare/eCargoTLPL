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
/// Created On    : 21th October 2008
/// Description   : This is the Page For Operation Printing Stationery
/// </summary>

public partial class Operations_Document_Allocation_WucPrintingStationary : System.Web.UI.UserControl,IPrintingStationaryView 
{
    #region ClassVariables
    PrintingStationaryPresenter objPrintingStationaryPresenter;
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    Label lbl_SeriesGenerationID;
    
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

    public int SeriesGenerationID
    {
        set
        {
            hdn_SeriesGenerationID.Value = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(hdn_SeriesGenerationID.Value);
        }
    }
    public int MinStartNo
    {
        set
        {
            hdn_MinStartNo.Value = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(hdn_MinStartNo.Value);
        }
    }
    public int ParentStartNo
    {
        set
        {
            hdn_ParentStartNo.Value = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(hdn_ParentStartNo.Value);
        }
    }
    public int ParentEndNo
    {
        set
        {
            hdn_ParentEndNo.Value = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(hdn_ParentEndNo.Value);
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
    public int StartNo
    {
        set
        {
            txt_StartNo.Text = Util.Int2String(value);
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
    public int Balance
    {
        get
        {
            return (EndNo - StartNo) + 1;
        }

    }
    public DateTime DateofPrinting
    {
        set
        {
            WucDateofPrinting.SelectedDate = value;
        }
        get
        {
            return WucDateofPrinting.SelectedDate;
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
    public DataTable Bind_dg_GeneratedSeries
    {
        set
        {
            dg_GeneratedSeries.DataSource = value;
            dg_GeneratedSeries.DataBind();            
        }
    }

    public DataTable SessionGeneratedSeries
    {
        get { return StateManager.GetState<DataTable>("Freight"); }
        set { StateManager.SaveState("Freight", value); }
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
        else if (txt_StartNo.Text == string.Empty)
        {

            errorMessage = "Please Enter Start No";
            txt_StartNo.Focus();
        }
        else if (txt_EndNo.Text == string.Empty)
        {
            errorMessage = "Please Enter End No";
            txt_EndNo.Focus();
        }
        else if (check_Rbtn_Selected() == false && keyID <= 0)
        {
            errorMessage = "Please Select Any Generated Series";
        }
        else if (CheckStartNoEndNo() == false)
        {
            txt_StartNo.Focus();
            _isValid = false;
        }
        else if (objPrintingStationaryPresenter.CheckDuplicate() == true)
        {
            errorMessage = "Duplicate Series";
            txt_StartNo.Focus();
            return _isValid;
        }       
        else if (CheckMinMaxNo() == false)
        {
            _isValid = false;
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
    private bool CheckStartNoEndNo()
    {
        if (StartNo > EndNo)
        {
            errorMessage = "Start No Should be Less then End No";
            return false;
        }
        else if (StartNo < ParentStartNo || EndNo > ParentEndNo)
        {
            errorMessage = "Series Should be in between '" + ParentStartNo + "' To  '" + ParentEndNo + " '";
            return false;
        }
        return true;
    }
    private bool CheckMinMaxNo()
    {
        if (keyID > 0)
        {
            if (MinStartNo > 0 && MaxEndNo > 0 && ParentStartNo > 0 && ParentEndNo > 0)
            {
                if (StartNo > MinStartNo || EndNo < MaxEndNo || StartNo < ParentStartNo || EndNo > ParentEndNo)
                {
                    errorMessage = "Start No Should not be Less then or Equal to ' " + MinStartNo + "' And End No Should be Greater then '" + MaxEndNo + "' ";
                    return false;
                }
            }
        }

        return true;
    }
    #endregion
    #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        btn_Save.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save));

        objPrintingStationaryPresenter = new PrintingStationaryPresenter(this, IsPostBack);
        if (!IsPostBack)
        {
            if (keyID > 0)
            {
                ddl_DocumentType.Enabled = false;
                dg_GeneratedSeries.Columns[1].Visible = false;
            }
            else
            {
                txt_StartNo.MaxLength = Util.String2Int(ObjCommon.EC_Common_Pass_Query("select GC_No_Length from EC_Master_Company_GC_Parameter").Tables[0].Rows[0][0].ToString());
                txt_EndNo.MaxLength = txt_StartNo.MaxLength;
            }
        }
    }
    protected void ddl_DocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        objPrintingStationaryPresenter.FillGrid();
        txt_StartNo.Text = "";
        txt_EndNo.Text = "";
    }

    private bool check_Rbtn_Selected()
    {
        bool rbtnchecked = false;
        RadioButton rdo_Series;
        int i;

        for (i = 0; i < dg_GeneratedSeries.Items.Count; i++)
        {
            rdo_Series = (RadioButton)dg_GeneratedSeries.Items[i].FindControl("rdo_Series");
            if (rdo_Series.Checked == true)
            {
                rbtnchecked = true;
                break;
            }
        }
        return rbtnchecked;
    }
    protected void rdo_Series_CheckedChanged(object sender, EventArgs e)
    {
        Label lbl_StartNo, lbl_EndNo;
        RadioButton rdo_Series, rdo_SeriesUnChecked;
        int i;
        rdo_Series = (RadioButton)sender;
        DataGridItem _item = (DataGridItem)rdo_Series.Parent.Parent;
        lbl_StartNo = (Label)_item.FindControl("lbl_StartNo");
        lbl_EndNo = (Label)_item.FindControl("lbl_EndNo");
        lbl_SeriesGenerationID = (Label)_item.FindControl("lbl_SeriesGenerationID");

        SeriesGenerationID =Util.String2Int(lbl_SeriesGenerationID.Text);

        txt_StartNo.Text = lbl_StartNo.Text;
        //hdn_StartNo.Value = lbl_StartNo.Text;
        ParentStartNo = Util.String2Int(lbl_StartNo.Text);
        txt_EndNo.Text = lbl_EndNo.Text;
        //hdn_EndNo.Value = lbl_EndNo.Text;
        ParentEndNo = Util.String2Int(lbl_EndNo.Text);

        for (i = 0; i < dg_GeneratedSeries.Items.Count; i++)
        {
            rdo_SeriesUnChecked = (RadioButton)dg_GeneratedSeries.Items[i].FindControl("rdo_Series");
            rdo_SeriesUnChecked.Checked = false;
        }
        rdo_Series.Checked = true;
    }
   
    protected void btn_Save_Click(object sender, EventArgs e)
    {        
        objPrintingStationaryPresenter.Save();        
    }

    public void ClearVariables()
    {
        SessionGeneratedSeries = null;
    }
    //protected void dg_GeneratedSeries_ItemDataBound(object sender, DataGridItemEventArgs e)
    //{
    //    Label lbl_StartNo, lbl_EndNo;
        
    //    if (e.Item.ItemIndex != -1)
    //    {
    //        lbl_StartNo = (Label)e.Item.FindControl("lbl_StartNo");
    //        lbl_EndNo = (Label)e.Item.FindControl("lbl_EndNo");
    //        lbl_SeriesGenerationID = (Label)e.Item.FindControl("lbl_SeriesGenerationID");

    //        if (keyID > 0)
    //        {
    //            hdn_StartNo.Value = lbl_StartNo.Text;
    //            hdn_EndNo.Value = lbl_EndNo.Text;
    //        }
    //    }
    //}
    #endregion
}
