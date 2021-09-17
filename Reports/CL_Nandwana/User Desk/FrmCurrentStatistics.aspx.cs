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
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Reports_CL_Nandwana_User_Desk_FrmCurrentStatistics : System.Web.UI.Page
{
    #region ClassVariables
    private DataSet ds;
    private DAL objDAL = new DAL();  
    private int RecordCount;
    Common objcommon = new Common();
    int year_code;

    #endregion

    #region ControlsValue
    
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {         
            objcommon.SetStandardCaptionForGrid(dg_GridCurrentStatistics);  
            BindGrid("form_and_pageload", e); 
            
        }
    } 

   

    //private void calculate_totals()
    //{
        
    //    if (ds.Tables.Count > 0)
    //    {
            
             
    //    }
    //}

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        string HierarchyCode = UserManager.getUserParam().HierarchyCode;
        int MainId = UserManager.getUserParam().MainId;
        DateTime StartDate = UserManager.getUserParam().StartDate;  
        DateTime EndDate = UserManager.getUserParam().EndDate;  

        int Branch_ID, Area_ID, Region_ID;
        Branch_ID = 0;
        Area_ID = 0;
        Region_ID = 0;

        if (HierarchyCode == "AD" || HierarchyCode == "HO")
        {
            Branch_ID = 0;
            Area_ID = 0;
            Region_ID = 0;
        }
        else if (HierarchyCode == "RO")
        {
            Branch_ID = 0;
            Area_ID = 0;
            Region_ID = MainId;
        }
        else if (HierarchyCode == "AO")
        {
            Branch_ID = 0;
            Area_ID = MainId;
            Region_ID = 0;
        }
        else if (HierarchyCode == "BO")
        {
            Branch_ID = MainId;
            Area_ID = 0;
            Region_ID = 0;
        }
        

        SqlParameter[] objSqlParam = {objDAL.MakeInParams("@Branch_ID",SqlDbType.Int,0,Branch_ID),
            objDAL.MakeInParams("@Area_ID",SqlDbType.Int,0,Area_ID),
            objDAL.MakeInParams("@Region_ID",SqlDbType.Int,0,Region_ID),
            objDAL.MakeInParams("@Fromdate", SqlDbType.DateTime,0,StartDate),
            objDAL.MakeInParams("@Todate", SqlDbType.DateTime,0,EndDate)

        };

        objDAL.RunProc("COM_UserDesk_Get_CurrentStatistics", objSqlParam, ref ds);

         
 
        //calculate_totals();
        //dg_GridCurrentStatistics.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());
        objcommon.ValidateReportForm(dg_GridCurrentStatistics, ds.Tables[0], CallFrom, lbl_Error);
         
    }
 
    public void ClearVariables()
    {
        ds = null;
    }
    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

    protected void dg_GridCurrentStatistics_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.Cells[0].Text) == "Booking")
        {
            e.Item.BackColor = System.Drawing.Color.PowderBlue;
            e.Item.ForeColor = System.Drawing.Color.Blue;
            e.Item.Font.Bold = true;
            e.Item.Cells[2].Text = "";
            e.Item.Cells[3].Text = "";
            e.Item.Cells[4].Text = "";
        }
        if ((e.Item.Cells[0].Text) == "Delivery")
        {
            e.Item.BackColor = System.Drawing.Color.PowderBlue;
            e.Item.ForeColor = System.Drawing.Color.Blue;
            e.Item.Font.Bold = true;
            e.Item.Cells[2].Text = "";
            e.Item.Cells[3].Text = "";
            e.Item.Cells[4].Text = "";
        }
    }
}



