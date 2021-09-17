using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using System.Text;


public partial class Finance_Reports_FrmDaliyChequeReceived : System.Web.UI.Page
{
    #region Declaration
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

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();

        lbl_Error.Text = "";
        int Region_Id = Wuc_Region_Area_Branch1.RegionID;
        string Region_Name = Wuc_Region_Area_Branch1.SelectedRegionText;

        int Area_id = Wuc_Region_Area_Branch1.AreaID;
        string Area_Name = Wuc_Region_Area_Branch1.SelectedAreaText;

        int Branch_id = Wuc_Region_Area_Branch1.BranchID;
        string Branch_Name = Wuc_Region_Area_Branch1.SelectedBranchText;

        int ReportType;

        if (rdl_ReportType.SelectedValue == "0")
        {
            ReportType = 0;
        }
        else
        {
            ReportType = 1;
        }

        StringBuilder Path = new StringBuilder(Util.GetBaseURL());
        Path.Append("/");

        Path.Append("Finance/Reports/FrmDaliyChequeReceivedViewer.aspx?Menu_Item_Id=" + Util.EncryptInteger(Raj.EC.Common.GetMenuItemId())
                 + "&Region_Id=" + Util.EncryptInteger(Region_Id) + "&Region_Name=" + Util.EncryptString(Region_Name)
                 + "&Area_id=" + Util.EncryptInteger(Area_id) + "&Area_Name=" + Util.EncryptString(Area_Name)
                 + "&Branch_id=" + Util.EncryptInteger(Branch_id) + "&Branch_Name=" + Util.EncryptString(Branch_Name)
                 + "&Fromdate=" + From_Date + "&Todate=" + To_Date + "&ReportType=" + ReportType);


        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("Open_Details_Window('");
        sb.Append(Path);
        sb.Append("');");
        sb.Append("</script>");

        ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());

    }



    #endregion



}
