using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using ClassLibraryMVP;
using System.Web.Security;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using System.Text;

public partial class Reports_Operation_Frm_Export_To_Excel_ATC : System.Web.UI.Page
{
    private DataSet ds, objDS;
    private DAL objDAL = new DAL();
    Common objCommon = new Common();
    int _Menu_Item_Id;
 

    private int GetMenuHeadId
    {
        get { return StateManager.GetState<int>("MenuHeadId"); }
    }
    public int Menu_Item_Id
    {
        set { _Menu_Item_Id = value; }
        get { return _Menu_Item_Id; }
    } 
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Menu_Item_Id = Raj.EC.Common.GetMenuItemId();
 
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(Bindview);

        if (ddl_DocumentType.SelectedIndex > 0)
        {
            td_ExpToExl.Visible = true;
        }
        else
        {
            td_ExpToExl.Visible = false;
        }

        if (IsPostBack == false)
        {
            Fill_DocumentType();
            Wuc_From_To_Datepicker1.SelectedFromDate = DateTime.Now;
            Wuc_From_To_Datepicker1.SelectedToDate = DateTime.Now;
            Wuc_Export_To_Excel1.FileName = ddl_DocumentType.SelectedItem.Text;
        }
    }

    private void Fill_DocumentType()
    {
        ds = objCommon.EC_Common_Pass_Query("select Document_Id,Document_Name from ec_Master_Document where Document_Id in (2,4,5)");

        ddl_DocumentType.DataSource = ds;
        ddl_DocumentType.DataTextField = "Document_Name";
        ddl_DocumentType.DataValueField = "Document_Id";
        ddl_DocumentType.DataBind();
        ddl_DocumentType.Items.Insert(0, new ListItem("--- Select Document ---", "0"));
    }
        
    protected void ddl_DocumentType_SelectedIndexChanged(object sender, EventArgs e)
    { 
        if (ddl_DocumentType.SelectedIndex > 0)
        {
            td_ExpToExl.Visible = true;
        }
        else
        {
            td_ExpToExl.Visible = false;
        }
    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
        {
            lbl_Error.Text = "";

            Bindview("form", e);
        }
        else
        {
            lbl_Error.Text = msg;
            
        }
    } 

    private void Bindview(object sender, EventArgs e)
    {
        DAL objDAL = new DAL(); 
        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_Date = Wuc_From_To_Datepicker1.SelectedToDate;

        int _mainId = UserManager.getUserParam().MainId;
        string _hierarchyCode = UserManager.getUserParam().HierarchyCode;

        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        if (ddl_DocumentType.SelectedIndex > 0)
        {
            if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
            {
                SqlParameter[] objSqlParam ={  
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_Date", SqlDbType.DateTime,0,To_Date),
            objDAL.MakeInParams("@Document_ID", SqlDbType.Int,0,ddl_DocumentType.SelectedValue), 
            objDAL.MakeInParams("@Main_Id", SqlDbType.Int,0,_mainId), 
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar,5,_hierarchyCode) 
            };

                objDAL.RunProc("EC_Report_Opr_Export_To_Excel_ATC", objSqlParam, ref objDS);

                PrepareDTForExportToExcel();
            }
            td_ExpToExl.Visible = true;
        }
        else
        {
            td_ExpToExl.Visible = false;
        }
    }

    private void PrepareDTForExportToExcel()
    {
        if (objDS.Tables[0].Rows.Count > 0)
        {
            Wuc_Export_To_Excel1.has_last_row_as_total = false;
            Wuc_Export_To_Excel1.SessionExporttoExcel = objDS.Tables[0];
            Wuc_Export_To_Excel1.FileName = ddl_DocumentType.SelectedItem.Text;
        }
    }
    
}
