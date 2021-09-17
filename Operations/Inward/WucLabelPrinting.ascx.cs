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
using System.Drawing;
using System.Text;
using ClassLibraryMVP;
using ClassLibraryMVP.Security;
using Raj.EC.OperationPresenter;
using Raj.EC.OperationView;
/// <summary>
/// Author        : Shiv kumar mishra
/// Created On    : 22/10/2008
/// Description   : This Page is For LabelPrinting Operation
/// </summary>
/// 
public partial class Operations_Outward_WucLabelPrinting : System.Web.UI.UserControl,ILabelPrintingView
{
    #region ClassVariables
    LabelPrintingPresenter objLabelPrintingPresenter;
    Raj.EC.Common objComm = new Raj.EC.Common();
    DataTable objDT = new DataTable();
    DataSet objDS = new DataSet();
    string _flag = "";
    string Mode = "0";
    bool _IsFrom_Edit = true; 
    string _GC_No_XML;
    PageControls pc = new PageControls();
    #endregion

    #region ControlsValues
   
    public int Total_No_Of_GC
    {
        set { hdn_Total_GC.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_Total_GC.Value); }
    }
    
    public string Flag
    {
        get { return _flag; }
    }
    public bool IsFrom_Edit
    {
        get { return _IsFrom_Edit; }
    } 
    
  
    
    #endregion

    #region ControlsBind

    public void BindLabelPrintingGrid()
    {
        dg_LabelPrinting.DataSource = SessionBindLabelPrintingGrid;
        dg_LabelPrinting.DataBind();

        if (SessionBindLabelPrintingGrid.Rows.Count > 0)
        {
            Total_No_Of_GC = SessionBindLabelPrintingGrid.Rows.Count;
        }
    }

    public DataTable SessionBindLabelPrintingGrid
    {
        get { return StateManager.GetState<DataTable>("BindLabelPrintingGrid"); }
        set
        { 
            StateManager.SaveState("BindLabelPrintingGrid", value);

            if (StateManager.Exist("BindLabelPrintingGrid"))
                BindLabelPrintingGrid();
        }
    }

    public String LabelPrintingDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();

            //DataView view = objComm.Get_View_Table(SessionBindLabelPrintingGrid, "Att = true");
            DataView view = objComm.Get_View_Table(SessionBindLabelPrintingGrid, "");
            _objDs.Tables.Add(view.ToTable().Copy());

            _objDs.Tables[0].TableName = "LabelPrintingGrid_Details";
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

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        

        errorMessage = "";
        lbl_Error_Client.Text = "";
      
        if ( SessionBindLabelPrintingGrid.Rows.Count  <= 0)
        {
            errorMessage = "Please Enter Atleast One " + CompanyManager.getCompanyParam().GcCaption;
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
        }
    }

    //Added : Anita On : 19 Feb 09
    public void ClearVariables()
    {
        SessionBindLabelPrintingGrid = null;
    }
    #endregion      

    #region grid validation

    ////private bool grid_gc_validation()
    ////{
    ////    bool ATS = true;

    ////    objDS = objLabelPrintingPresenter.gc_grid_validation();

    ////    if (Util.String2Bool(objDS.Tables[0].Rows[0]["IsDuplicate"].ToString()) == true)
    ////    {
    ////        errorMessage = "LabelPrinting Already Prepare For Same Vehicle No,Same LabelPrinting To Branch and Same LabelPrinting Type For Gc No : " + objDS.Tables[0].Rows[0]["GCNo"].ToString();
    ////        ATS = false;
    ////    }
    ////    else
    ////    {
    ////        ATS = true;
    ////    }
    ////    return ATS;
    ////}

    ////private bool grid_validation()
    ////{
       
    ////    CheckBox chk;
    ////    int i;
    ////    bool ATS = true;

    ////    if(Total_No_Of_GC > 0)
    ////    {
    ////        objDT = SessionBindLabelPrintingGrid;

    ////        for (i = 0; i <= dg_LabelPrinting.Items.Count - 1; i++)
    ////        {
    ////            chk = (CheckBox)dg_LabelPrinting.Items[i].FindControl("Chk_Attach");
               

    ////            if (grid_gc_validation() == false)
    ////            {
    ////                ATS = false;
    ////                break;
    ////            } 
    ////            else
    ////            {
    ////                ATS = true;
    ////            }
    ////        }
    ////    }
    ////    return ATS;
    ////}
 

    #endregion

    #region grid validation
   
    private void Assign_Hidden_Values_To_TextBox()
    { 
        lbl_Total_GC.Text = hdn_Total_GC.Value;
    }

    private void Assign_Hidden_Values_For_Reset()
    {
        hdn_Total_GC.Value = "0";
        lbl_Total_GC.Text = "0";
    }

     

    private void OnGetGCXML(object sender, EventArgs e)
    {
            _IsFrom_Edit = false;
            Assign_Hidden_Values_For_Reset();
            _GC_No_XML = WucSelectedItems1.GetSelectedItemsXML;
            objLabelPrintingPresenter.fillgrid();
            WucSelectedItems1.dtdetails = SessionBindLabelPrintingGrid;
            Assign_Hidden_Values_To_TextBox();
            WucSelectedItems1.Get_Not_Selected_Items();
    }

    
     #endregion
       
    #region control event

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "4")
        {
            btn_Close.Visible = true;
            btn_Close.Enabled = true; 
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        pc.AddAttributes(this.Controls);
        SetPostBackValues(); 

        if (!IsPostBack)
        {
          
            Assign_Hidden_Values_For_Reset();
            hdn_LoginBranch_Id.Value = Util.Int2String(UserManager.getUserParam().MainId); 
 
         }

        objLabelPrintingPresenter = new LabelPrintingPresenter(this, IsPostBack);
         

        if (keyID > 0)
        {
            td_gccontrol.Style.Add("display", "none");
        }  

        Assign_Hidden_Values_To_TextBox();

  
    }

    private void SetPostBackValues()
    { 
        WucSelectedItems1.GetSelectedItemsXMLButtonClick += new EventHandler(OnGetGCXML);
        WucSelectedItems1.Set_GCCaption = CompanyManager.getCompanyParam().GcCaption;

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        ////btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save,btn_Save_Exit,btn_Save_Print,btn_Close));
        ////btn_Save_Exit.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page,btn_Save_Exit,btn_Save,btn_Save_Print,btn_Close));
        ////btn_Save_Print.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page,btn_Save_Print,btn_Save,btn_Save_Exit,btn_Close));
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        Page.MaintainScrollPositionOnPostBack = true;
        SetStandardCaption();
    }
    
    #endregion

    #region save control event

    private void SetStandardCaption()
    {
        const int GCNoCaption = 0;
        hdn_GCCaption.Value = CompanyManager.getCompanyParam().GcCaption;
        WucSelectedItems1.SetFoundCaption = "Enter  " + hdn_GCCaption.Value + "  Nos.:";
        WucSelectedItems1.SetNotFoundCaption = hdn_GCCaption.Value + "  Nos.Not Found :";
        Label1.Text = "Total  " + hdn_GCCaption.Value + ":";
        dg_LabelPrinting.Columns[GCNoCaption].HeaderText = hdn_GCCaption.Value + "  No";
    }
    
    protected void btn_Save_Print_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndPrint";
        
        objLabelPrintingPresenter.Save();
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
    protected void dg_LabelPrinting_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string calculate_grid ="";
        string calculate_grid1 = "";
        string calculate_grid2 = "";
        string calculate_grid3 = "";
        CheckBox chk_Attach;
        //TextBox Txt_Loded_Art, Txt_Loded_Wt;

        if (e.Item.ItemIndex != -1)
        {
            chk_Attach = (CheckBox)e.Item.FindControl("Chk_Attach");
            //Txt_Loded_Art = (TextBox)e.Item.FindControl("txt_Loaded_Art");
            //Txt_Loded_Wt = (TextBox)e.Item.FindControl("txt_Loaded_Wt");

            if (CompanyManager.getCompanyParam().IsALSRequired == false)
            {
                //Txt_Loded_Art.Enabled = true;
                //Txt_Loded_Wt.Enabled = true;

                //calculate_grid = "Check_Single(" + chk_Attach.ClientID + ",'j','2')";
                //calculate_grid1 = "Check_Single(" + chk_Attach.ClientID + ",'j','3')";
                //calculate_grid2 = "Check_Single(" + chk_Attach.ClientID + ",'j','4')";
                //calculate_grid3 = "Check_Single(" + chk_Attach.ClientID + ",'j','5')";

                //Txt_Loded_Art.Attributes.Add("onblur", calculate_grid);
                //Txt_Loded_Wt.Attributes.Add("onblur", calculate_grid1);

                //Txt_Loded_Art.Attributes.Add("onfocus", calculate_grid2);
                //Txt_Loded_Wt.Attributes.Add("onfocus", calculate_grid3);           
            }
            //else
            //{
            //    Txt_Loded_Art.Enabled = false;
            //    Txt_Loded_Wt.Enabled = false;
            //}

            //if (CompanyManager.getCompanyParam().IsALSRequired == false && CompanyManager.getCompanyParam().IsPartLoadingRequired == true)
            //{
            //    Txt_Loded_Art.Enabled = true;
            //    Txt_Loded_Wt.Enabled = true;
            //}
            //else
            //{
            //    disable_Textbox(Txt_Loded_Art, Txt_Loded_Wt);
            //}

        }
    }

    private void disable_Textbox(TextBox txtbox1, TextBox txtbox2)
    {
        txtbox1.BackColor = Color.Transparent;
        txtbox1.BorderColor = Color.Transparent;
        txtbox1.ReadOnly = true;

        txtbox2.BackColor = Color.Transparent;
        txtbox2.BorderColor = Color.Transparent;
        txtbox2.ReadOnly = true;
    }

   #endregion

    ////protected void btn_Print_Click(object sender, EventArgs e)
    ////{
       
    ////    //StringBuilder Path = new StringBuilder(Util.GetBaseURL());
    ////    //Path.Append("/Operations/Inward/Frm_LabelPrintingViewer.aspx?Menu_Item_Id=" + Util.EncryptInteger(Raj.EC.Common.GetMenuItemId()) + "&LabelPrintingDetailsXML=" + LabelPrintingDetailsXML);               + "&LabelPrintingDetailsXML='" + LabelPrintingDetailsXML + "';"

    ////    string Path = "/Operations/Inward/Frm_LabelPrintingViewer.aspx?Menu_Item_Id=" + Util.EncryptInteger(Raj.EC.Common.GetMenuItemId()) + ";";
    ////    string popupScript = "<script type='text/javascript' language='javascript'>function Open_Details_Window(){ var w = screen.availWidth-10;var h = screen.availHeight-10;var popW =w-2, popH = h-25;var leftPos = (w-popW)/2, topPos = ((h-20)-popH)/2;window.open('" + Path + "', 'CustomPopUp', 'width=' + popW + ', height=' + popH + ',top=' + topPos + ',left=' + leftPos + ', menubar=no, resizable=yes,scrollbars=yes')return false;}</script>";
    ////    Page.ClientScript.RegisterStartupScript(typeof(string), "PopupScript", popupScript);


    ////}
}
