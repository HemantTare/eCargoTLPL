  
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls; 
using System.Text.RegularExpressions;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP;
using ClassLibraryMVP.Security;
using Raj.EC;
using Raj.EC.ControlsView;
using Raj.EC.OperationView;
using Raj.EC.OperationPresenter;

//Author : Ankit champaneriya
//Desc   : Booking register Report
//Date   : 03-01-09

public partial class Finance_Reports_FrmBranchWiseVehicleAdvance : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    

    
    #endregion

    #region ControlsValues

    public DateTime From_Date
    {
        set
        {
            dtp_From_Date.SelectedDate = value;
            hdn_From_Date.Value = dtp_From_Date.SelectedDate.ToString();
        }
        get { return dtp_From_Date.SelectedDate; }
    }

    public DateTime To_Date
    {
        set
        {
            dtp_To_Date.SelectedDate = value;
            hdn_To_Date.Value = dtp_To_Date.SelectedDate.ToString();
        }
        get { return dtp_To_Date.SelectedDate; }
    }
    
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    { 
        if (IsPostBack == false)
        { 

         
           From_Date = DateTime.Now;
           To_Date = DateTime.Now;
        }

        int Region_Id = Wuc_Region_Area_Branch1.RegionID;
        int Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;

        StringBuilder Path = new StringBuilder(Util.GetBaseURL());
        Path.Append("/");
        Path.Append("Reports/Direct_Printing/FrmBranchWiseVehicleAdvanceViewer.aspx?Menu_Item_Id=" + Util.EncryptInteger(Raj.EC.Common.GetMenuItemId()) 
             + "&Region_Id=" + Util.EncryptInteger(Region_Id) + "&Area_id=" + Util.EncryptInteger(Area_id) 
             + "&Branch_id=" + Util.EncryptInteger(Branch_id));
             //+ "&From_Date=" + From_Date + "&To_Date=" + To_Date
        btn_view.Attributes.Add("onclick", "return Open_Details_Window('" + Path + "'," + hdn_From_Date.ClientID + "," + hdn_To_Date.ClientID + ")");

    }
    
    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        if ((objDateCommon.Vaildate_Date(dtp_From_Date.SelectedDate , dtp_To_Date.SelectedDate, ref msg)) == true)
        {
            lbl_Error.Text = "";   
        }
        else
        {
            lbl_Error.Text = msg; 
        }
    }
    
    #endregion
 
    public void ClearVariables()
    {
        ds = null;
    }
    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

    protected void dtp_From_Date_SelectionChanged(object sender, EventArgs e)
    {
        hdn_From_Date.Value = dtp_From_Date.SelectedDate.ToString(); 
    }
    protected void dtp_To_Date_SelectionChanged(object sender, EventArgs e)
    {
        hdn_To_Date.Value = dtp_To_Date.SelectedDate.ToString(); 

    }
}
