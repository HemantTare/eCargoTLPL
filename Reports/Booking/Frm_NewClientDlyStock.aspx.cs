using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
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

//Author : Harshal Sapre
//Desc   : Delivery Stock List Report
//Date   : 14-01-09

public partial class Reports_Booking_Frm_NewClientDlyStock : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    public DataSet SessionBindNewClientDlyStock
    {
        get { return StateManager.GetState<DataSet>("BindNewClientDlyStock"); }
        set
        {
            StateManager.SaveState("BindNewClientDlyStock", value);
            
        }
    }
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        //Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        //Wuc_Export_To_Excel1.FileName = "NewClientDlyStockList";

        if (IsPostBack == false)
        { 
            
            BindGrid("form_and_pageload", e);
            WucFilter1.setddldatasource(ds);
        }
        
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;

        //StringBuilder Path = new StringBuilder(Util.GetBaseURL());
        //Path.Append("/");
        //Path.Append("Reports/Booking/Frm_NewClientDlyStockViewer.aspx?Menu_Item_Id=" + Util.EncryptInteger(Raj.EC.Common.GetMenuItemId())
        //     + "&calledfrom=" + Util.EncryptString("form") + "&colid=" + Util.EncryptInteger(WucFilter1.colid)
        //     + "&criteria_id=" + Util.EncryptInteger(WucFilter1.criteriaid) + "&Filtered_Text=" + Util.EncryptString(WucFilter1.Filtered_Text)
        //     + "&Filtered_Date=" + Util.EncryptString(Convert.ToString(WucFilter1.Filtered_Date)) + "&Filtered_Bit=" + Util.EncryptBool(WucFilter1.Filtered_bit));

        //btn_view.Attributes.Add("onclick", "return Open_NewClientDlyStock_Window('" + Path + "')");

    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        if (Wuc_Region_Area_Branch1.BranchID > 0)
        {
            DateCommon objDateCommon = new DateCommon();

            lbl_Error.Text = "";

            BindGrid("form", e);

            string calledfrom = Util.EncryptString("form");
            string BranchID = Util.EncryptInteger(Wuc_Region_Area_Branch1.BranchID);
            string BranchText = Util.EncryptString(Wuc_Region_Area_Branch1.SelectedBranchText);
            string AreaText = Util.EncryptString(Wuc_Region_Area_Branch1.SelectedAreaText);
            string RegionText = Util.EncryptString(Wuc_Region_Area_Branch1.SelectedRegionText);

            string colid = Util.EncryptInteger(WucFilter1.colid);
            string criteria_id = Util.EncryptInteger(WucFilter1.criteriaid);
            string Filtered_Text = Util.EncryptString(WucFilter1.Filtered_Text);
            string Filtered_Date = Util.EncryptString(Convert.ToString(WucFilter1.Filtered_Date));
            string Filtered_Bit = Util.EncryptString(Convert.ToString(WucFilter1.Filtered_bit));

            //Path.Append("Reports/Booking/Frm_NewClientDlyStockViewer.aspx?Menu_Item_Id=" + Util.EncryptInteger(Raj.EC.Common.GetMenuItemId())
            //     + "&calledfrom=" + Util.EncryptString("form") + "&colid=" + Util.EncryptInteger(WucFilter1.colid)
            //     + "&criteria_id=" + Util.EncryptInteger(WucFilter1.criteriaid) + "&Filtered_Text=" + Util.EncryptString(WucFilter1.Filtered_Text)
            //     + "&Filtered_Date=" + Util.EncryptString(Convert.ToString(WucFilter1.Filtered_Date)) + "&Filtered_Bit=" + Util.EncryptBool(WucFilter1.Filtered_bit));



            ClientScript.RegisterStartupScript(this.GetType(), "NewClientDlyStock", "viewwindow_general('"
                + BranchID
                + "','" + BranchText
                + "','" + AreaText
                + "','" + RegionText
                + "','" + calledfrom
                + "','" + colid
                + "','" + criteria_id
                + "','" + Filtered_Text
                + "','" + Filtered_Date
                + "','" + Filtered_Bit
                + "');", true);

        }
        else
        {
            lbl_Error.Text = "Select One Branch";      
        }
    }
 
    #endregion

    #region Other Function
     

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender); 
        DateTime start_date = (DateTime)UserManager.getUserParam().StartDate;

        int Branch_Id = Wuc_Region_Area_Branch1.BranchID; 

        SqlParameter[] objSqlParam ={  
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int, 0,Branch_Id), 
            objDAL.MakeInParams("@calledfrom",SqlDbType.VarChar,20,CallFrom),
            objDAL.MakeInParams("@colid",SqlDbType.Int,0,WucFilter1.colid),
            objDAL.MakeInParams("@datatype_id",SqlDbType.Int,0,WucFilter1.Datatype_ID),
            objDAL.MakeInParams("@criteria_id",SqlDbType.Int,0,WucFilter1.criteriaid),
            objDAL.MakeInParams("@Filtered_Text",SqlDbType.VarChar,50,WucFilter1.Filtered_Text),
            objDAL.MakeInParams("@Filtered_Date",SqlDbType.DateTime,0,WucFilter1.Filtered_Date),
            objDAL.MakeInParams("@Filtered_Bit",SqlDbType.Bit,0,WucFilter1.Filtered_bit) 
        };

        objDAL.RunProc("EC_RPT_New_Client_Delivery_Stock_List", objSqlParam, ref ds);
        
        if (CallFrom == "form")
        {
            SessionBindNewClientDlyStock = ds;
        }

        

        if (CallFrom == "form_and_pageload") return;
         
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
    #endregion

     

     
}
