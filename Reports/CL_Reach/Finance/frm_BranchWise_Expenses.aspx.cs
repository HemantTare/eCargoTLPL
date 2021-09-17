using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using System.IO;
using System.Text;

public partial class Reports_Finance_frm_BranchWise_Expenses : System.Web.UI.Page
{
    private DataSet ds;
    private DAL objDAL = new DAL();
    int Menu_Item_Id;
    DateTime From_Date;
    DateTime To_date;
    StringBuilder Path;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();

        int Region_Id = Wuc_Region_Area_Branch1.RegionID;
        int Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;

        From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        To_date = Wuc_From_To_Datepicker1.SelectedToDate;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),
              
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),
            objDAL.MakeInParams("@IS_HO ", SqlDbType.Bit,0,Wuc_Region_Area_Branch1.IsHo )
        };

        objDAL.RunProc("[FC_RPT_BranchWise_Expenses]", objSqlParam, ref ds);
    }

    protected void Btn_Preview_Click(object sender, EventArgs e)
    {
        Menu_Item_Id = Common.GetMenuItemId();
        BindGrid("", e);
        Session["FIN_DS"]= ds;
        Path = new StringBuilder(Util.GetBaseURL());
        Path.Append("/Reports/Direct_Printing/Frm_FinancePrintingViewer.aspx?Menu_Item_Id=" + Menu_Item_Id + "&Start_Date=" + Util.EncryptString(From_Date.ToString()) + "&End_Date=" + Util.EncryptString(To_date.ToString()));       
        String Script = "<script type='text/javascript'>Open_Show_Window('" + Path + "');</script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script", Script, false);
        //ds = null;
    }
}
