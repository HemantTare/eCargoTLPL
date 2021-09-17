using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using System.Text;
using Raj.EC;
using System.Web.UI.WebControls;

public partial class Reports_Booking_Inward_Frm_Branch_Vasuli_Hisab : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    int _Menu_Item_Id;
    #endregion

    #region ControlsValues

    public DateTime From_Date
    {
        set
        {
            Wuc_From_To_Datepicker1.SelectedFromDate = value;
        }
        get
        {
            return Wuc_From_To_Datepicker1.SelectedFromDate;
        }
    }

    public DateTime To_Date
    {
        set
        {
            Wuc_From_To_Datepicker1.SelectedToDate = value;

        }
        get { return Wuc_From_To_Datepicker1.SelectedToDate; }
    }

    public int Menu_Item_Id
    {
        set { _Menu_Item_Id = value; }
        get { return _Menu_Item_Id; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
       Menu_Item_Id = Raj.EC.Common.GetMenuItemId();
      
        if (IsPostBack == false)
        {
            CommonSetting(); 
        }
 

    }
    
    private void CommonSetting()
    {
        if (Menu_Item_Id == 5239)
        {
            this.Title = "Branch Vasuli Hisab";
            lbl_Heading.Text = "Branch Vasuli Hisab";
        } 
    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();


        int Branch_id2 = Wuc_Region_Area_Branch1.BranchID;
        if (Branch_id2 <= 0)
        {
            lbl_Error.Text = "Please Select One Branch";
        }
        else
        {

            lbl_Error.Text = "";
            int Region_Id = Wuc_Region_Area_Branch1.RegionID;
            string Region_Name = Wuc_Region_Area_Branch1.SelectedRegionText;

            int Area_id = Wuc_Region_Area_Branch1.AreaID;
            string Area_Name = Wuc_Region_Area_Branch1.SelectedAreaText;

            int Branch_id = Wuc_Region_Area_Branch1.BranchID;
            string Branch_Name = Wuc_Region_Area_Branch1.SelectedBranchText;

            StringBuilder Path = new StringBuilder(Util.GetBaseURL());
            Path.Append("/");

            Path.Append("Reports/Inward/Branch_Vasuli_Hisab_ReportsViewer.aspx?Menu_Item_Id=" + Util.EncryptInteger(Raj.EC.Common.GetMenuItemId())
                     + "&Region_Id=" + Util.EncryptInteger(Region_Id) + "&Region_Name=" + Util.EncryptString(Region_Name)
                     + "&Area_id=" + Util.EncryptInteger(Area_id) + "&Area_Name=" + Util.EncryptString(Area_Name)
                     + "&Branch_id=" + Util.EncryptInteger(Branch_id) + "&Branch_Name=" + Util.EncryptString(Branch_Name)
                     + "&Fromdate=" + From_Date + "&Todate=" + To_Date);


            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("Open_Details_Window('");
            sb.Append(Path);
            sb.Append("');");
            sb.Append("</script>");

            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
        } 
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
  
}
