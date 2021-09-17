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
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.Security;
using Raj.EC.Security;
using ClassLibraryMVP;
using Raj.EC;

public partial class Printing_FrmPrinterNameSetting : System.Web.UI.Page
{
    #region ClassVariables
    DataSet objDS = null;
    DataTable objDT;
    Common objCommon = new Common();
    private DAL objDAL = new DAL();
    // private int MainID = UserManager.getUserParam().MainId;
    //public int MainID = 1;
    #endregion

    #region ControlsValue
    public string HierarchyID
    {
        get { return ddl_HierarchyCode.SelectedValue; }
        set { ddl_HierarchyCode.SelectedValue = value; }
    }

    public int DocumentTypeId
    {
        get { return Util.String2Int(ddl_DocumentType.SelectedValue); }
        set { ddl_DocumentType.SelectedValue = Util.Int2String(value); }
    }
    public string PrinterNameToCopy
    {
        get { return txt_PrinterNameForCopy.Text; }
        set { txt_PrinterNameForCopy.Text = value; }
    }
    public DataTable SessionPrinterNameGrid
    {
        get { return StateManager.GetState<DataTable>("SessionPrinterNameGridBind"); }
        set { StateManager.SaveState("SessionPrinterNameGridBind", value); }
    }
    public String PrinterNameDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();

            _objDs.Tables.Add(SessionPrinterNameGrid.Copy());
            _objDs.Tables[0].TableName = "PrinterNameDetails";
            return _objDs.GetXml().ToLower();
        }
    }
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        //if (HierarchyID == "0")
        //{
        //    errorMessage = "Please Select Hierarchy Name.";
        //    ddl_HierarchyCode.Focus();
        //}
         if (DocumentTypeId <= 0)
        {
            errorMessage = "Please Select Document Type.";
            ddl_DocumentType.Focus();
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
        get { return Util.DecryptToInt(Request.QueryString["Id"]); ; }
    }

    #endregion


    #region ControlBind

    public DataTable BindHierarchyCode
    {
        set
        {
            ddl_HierarchyCode.DataTextField = "Hierarchy_Name";
            ddl_HierarchyCode.DataValueField = "Hierarchy_Code";
            ddl_HierarchyCode.DataSource = value;
            ddl_HierarchyCode.DataBind();
            //ddl_HierarchyCode.Items.Insert(0, new ListItem("Select One", "0"));

        }
    }

    public DataTable BindDocumentType
    {
        set
        {
            ddl_DocumentType.DataTextField = "Document_Name";
            ddl_DocumentType.DataValueField = "MenuItem_ID";
            ddl_DocumentType.DataSource = value;
            ddl_DocumentType.DataBind();
            ddl_DocumentType.Items.Insert(0, new ListItem("Select One", "0"));

        }
    }
    #endregion

    #region OtherMethods
   

    private void BindPrinterNamegrid()
    {
        dg_PrinterName.DataSource = SessionPrinterNameGrid;
        dg_PrinterName.DataBind();
        
    }
    public DataTable FillValues()
    {
        int MainID = UserManager.getUserParam().MainId;
        SqlParameter[] param ={ objDAL.MakeInParams("@flag", SqlDbType.Int, 0, 0),
                                objDAL.MakeInParams("@Menuitem_id",SqlDbType.Int,0, DocumentTypeId),
                                objDAL.MakeInParams("@Hirarchy_code",SqlDbType.VarChar, 5,HierarchyID ), 
                                objDAL.MakeInParams("@Main_Id",SqlDbType.Int,0,MainID)
                               
          };

        objDAL.RunProc("[dbo].[EC_PrinterName_Settings_Fill]", param, ref objDS);
        return objDS.Tables[0];

      
    }
    private void MakeDT()
    {
        DataTable objDT;

        int i = 0;

        if (dg_PrinterName.Items.Count > 0)
        {
            for (i = 0; i <= dg_PrinterName.Items.Count - 1; i++)
            {
              TextBox txt_PrinterName = (TextBox)dg_PrinterName.Items[i].FindControl("txt_PrinterName");
              Label lbl_MainId = (Label)dg_PrinterName.Items[i].FindControl("lbl_MainId");
              SessionPrinterNameGrid.Rows[i]["Printer_Name"] = txt_PrinterName.Text;
              SessionPrinterNameGrid.Rows[i]["Main_ID"] = lbl_MainId.Text;
            }
            objDT = SessionPrinterNameGrid;
        }
    }
    private void SetHeaderName()
    {
        const int Hierarchy_Name =0;
        if (HierarchyID == "BO")
        {
            dg_PrinterName.Columns[Hierarchy_Name].HeaderText="Branch Name";
        }
        else if (HierarchyID == "AO")
        {
             dg_PrinterName.Columns[Hierarchy_Name].HeaderText=" Area Name";
        }
        else if (HierarchyID == "RO")
        {
           dg_PrinterName.Columns[Hierarchy_Name].HeaderText=" Region Name";
       }
       else if (HierarchyID == "HO")
       {
           dg_PrinterName.Columns[Hierarchy_Name].HeaderText = "Head Office";
       }
    }
    #endregion

    #region ControlEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillValues();           
            BindHierarchyCode = objDS.Tables[0];
            BindDocumentType = objDS.Tables[1];
            const int Hierarchy_Name = 0;
            dg_PrinterName.Columns[Hierarchy_Name].HeaderText = "Head Office";
            SessionPrinterNameGrid = objDS.Tables[2];
            BindPrinterNamegrid();           
            
            
        }
        SetHeaderName();
        

    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (validateUI() == true)
        {
            MakeDT();
            SqlParameter[] param ={                                
                                objDAL.MakeInParams("@Menuitem_id",SqlDbType.Int,0, DocumentTypeId),
                                objDAL.MakeInParams("@Hirarchy_code",SqlDbType.VarChar, 5,HierarchyID),                                
                                objDAL.MakeInParams("@PrinterNameDetailsXML",SqlDbType.Xml,0,PrinterNameDetailsXML)
                               
          };

            objDAL.RunProc("[dbo].[EC_PrinterName_Settings_Save]", param);
            errorMessage = "Saved SuccessFully";
        }
        
        
       
    }
    protected void ddl_HierarchyCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        errorMessage = "";
        FillValues();
        SessionPrinterNameGrid = objDS.Tables[2];
        BindPrinterNamegrid();
        SetHeaderName();


    }
    protected void ddl_DocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        errorMessage = "";
        FillValues();
        SessionPrinterNameGrid = objDS.Tables[2];
        BindPrinterNamegrid();
    }
    protected void btn_CopyPrinterName_Click(object sender, EventArgs e)
    {
       
        if (dg_PrinterName.Items.Count > 0)
        {
            for (int i = 0; i <= dg_PrinterName.Items.Count - 1; i++)
            {

                if (SessionPrinterNameGrid.Rows[i]["Printer_Name"] == string.Empty)
                {
                    SessionPrinterNameGrid.Rows[i]["Printer_Name"] = txt_PrinterNameForCopy.Text;
                }
            }
            objDT = SessionPrinterNameGrid;
        }
        SessionPrinterNameGrid = objDT;
        BindPrinterNamegrid();
    }
    #endregion

    
}

