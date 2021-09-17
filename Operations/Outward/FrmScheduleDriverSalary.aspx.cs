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
using System.Data.SqlClient;
using ClassLibraryMVP.General;
using Raj.EC;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.Security;
using System.IO;


public partial class Operations_Outward_FrmScheduleDriverSalary : System.Web.UI.Page
{


    private string ErrorMessage
    {
        set { lblErrors.Text = value.ToString(); }
    }


    private string DetailsXML
    {
        get
        {
            string XML = "<newdataset>";

            for (int i = 0; i <= dgGrid.Items.Count - 1; i++)
            {
                CheckBox Chk_Att = (CheckBox)(dgGrid.Items[i].FindControl("Chk_Att"));
                Label lblNoOfDays = (Label)(dgGrid.Items[i].FindControl("lblNoOfDays"));
                HiddenField hdnVehicleID = (HiddenField)(dgGrid.Items[i].FindControl("hdnVehicleID"));
                Label lblVehicleNo = (Label)(dgGrid.Items[i].FindControl("lblVehicleNo"));
                Label lblDriverName = (Label)(dgGrid.Items[i].FindControl("lblDriverName"));
                TextBox txtNewDriver = (TextBox)(dgGrid.Items[i].FindControl("txtNewDriver"));
                ComponentArt.Web.UI.Calendar dtp_DriverFromDate = (ComponentArt.Web.UI.Calendar)(dgGrid.Items[i].FindControl("dtp_DriverFromDate"));
                ComponentArt.Web.UI.Calendar dtp_DriverToDate = (ComponentArt.Web.UI.Calendar)(dgGrid.Items[i].FindControl("dtp_DriverToDate"));

                TextBox txtRemark1 = (TextBox)(dgGrid.Items[i].FindControl("txtRemark1"));
                TextBox txtRemark2 = (TextBox)(dgGrid.Items[i].FindControl("txtRemark2"));
                TextBox txtRemark3 = (TextBox)(dgGrid.Items[i].FindControl("txtRemark3"));

                Label lblCleanerName = (Label)(dgGrid.Items[i].FindControl("lblCleanerName"));
                TextBox txtNewCleaner = (TextBox)(dgGrid.Items[i].FindControl("txtNewCleaner"));

                ComponentArt.Web.UI.Calendar dtp_CleanerFromDate = (ComponentArt.Web.UI.Calendar)(dgGrid.Items[i].FindControl("dtp_CleanerFromDate"));
                ComponentArt.Web.UI.Calendar dtp_CleanerToDate = (ComponentArt.Web.UI.Calendar)(dgGrid.Items[i].FindControl("dtp_CleanerToDate"));

                TextBox txtRemark4 = (TextBox)(dgGrid.Items[i].FindControl("txtRemark4"));




                if (Chk_Att.Checked == true)
                {
                    XML = XML + "<details>";
                    XML = XML + "<att>" + Chk_Att.Checked + "</att>";
                    XML = XML + "<noofdays>" + lblNoOfDays.Text + "</noofdays>";
                    XML = XML + "<vehicleid>" + hdnVehicleID.Value + "</vehicleid>";
                    XML = XML + "<vehicleno>" + lblVehicleNo.Text + "</vehicleno>";
                    XML = XML + "<drivername>" + lblDriverName.Text + "</drivername>";
                    XML = XML + "<newdriver>" + txtNewDriver.Text + "</newdriver>";

                    XML = XML + "<driverfromdate>" + String.Format("{0:dd MMM yyyy}", dtp_DriverFromDate.SelectedDate) + "</driverfromdate>";
                    XML = XML + "<drivertodate>" + String.Format("{0:dd MMM yyyy}", dtp_DriverToDate.SelectedDate) + "</drivertodate>";

                    XML = XML + "<remark1>" + txtRemark1.Text + "</remark1>";
                    XML = XML + "<remark2>" + txtRemark2.Text + "</remark2>";
                    XML = XML + "<remark3>" + txtRemark3.Text + "</remark3>";
                    XML = XML + "<cleanername>" + lblCleanerName.Text + "</cleanername>";
                    XML = XML + "<newcleaner>" + txtNewCleaner.Text + "</newcleaner>";

                    XML = XML + "<cleanerfromdate>" + String.Format("{0:dd MMM yyyy}", dtp_CleanerFromDate.SelectedDate) + "</cleanerfromdate>";
                    XML = XML + "<cleanertodate>" + String.Format("{0:dd MMM yyyy}", dtp_CleanerToDate.SelectedDate) + "</cleanertodate>";

                    XML = XML + "<remark4>" + txtRemark4.Text + "</remark4>";

                    XML = XML + "</details>";
                }

            }

            XML = XML + "</newdataset>";

            return XML;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(ExportToExcel);

        Wuc_Export_To_Excel1.FileName = "DriverScheduleForSalary";

        if (!IsPostBack)
        {
            FillGrid();
        }

    }


    private void FillGrid()
    {
        DAL objDAL = new DAL();
        DataSet ds = new DataSet();
        objDAL.RunProc("dbo.EC_Opr_Driver_Salary_Pending_Vehicles_For_Salary", ref ds);

        dgGrid.DataSource = ds;
        dgGrid.DataBind();

    }


    protected void dgGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {

    
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

            CheckBox Chk_Att;

            Chk_Att = (CheckBox)e.Item.FindControl("Chk_Att");


            if (Chk_Att.Checked  == true)
            {
                e.Item.BackColor = System.Drawing.Color.Orange; 
            }

        }
    }

    private void ExportToExcel(object sender, EventArgs e)
    {

        string CallFrom = (string)(sender);

        if (AllowToSave())
        {
            Save("ExportToExcel");
        }

        if (CallFrom == "exporttoexcelusercontrol")
        {

            string DetailsXML2;

            if (DetailsXML == "<newdataset></newdataset>")
            {

                DetailsXML2 = "<newdataset><details>";
                DetailsXML2 = DetailsXML2 + "<att></att>";
                DetailsXML2 = DetailsXML2 + "<noofdays></noofdays>";
                DetailsXML2 = DetailsXML2 + "<vehicleid></vehicleid>";
                DetailsXML2 = DetailsXML2 + "<vehicleno></vehicleno>";
                DetailsXML2 = DetailsXML2 + "<drivername></drivername>";
                DetailsXML2 = DetailsXML2 + "<newdriver></newdriver>";

                DetailsXML2 = DetailsXML2 + "<driverfromdate></driverfromdate>";
                DetailsXML2 = DetailsXML2 + "<drivertodate></drivertodate>";

                DetailsXML2 = DetailsXML2 + "<remark1></remark1>";
                DetailsXML2 = DetailsXML2 + "<remark2></remark2>";
                DetailsXML2 = DetailsXML2 + "<remark3></remark3>";
                DetailsXML2 = DetailsXML2 + "<cleanername></cleanername>";
                DetailsXML2 = DetailsXML2 + "<newcleaner></newcleaner>";

                DetailsXML2 = DetailsXML2 + "<cleanerfromdate></cleanerfromdate>";
                DetailsXML2 = DetailsXML2 + "<cleanertodate></cleanertodate>";

                DetailsXML2 = DetailsXML2 + "<remark4></remark4>";

                DetailsXML2 = DetailsXML2 + "</details></newdataset>";

            }
            else
            {
                DetailsXML2 = DetailsXML;
            }

            StringReader dr = new StringReader(DetailsXML2);
            DataSet ds = new DataSet();
            ds.ReadXml(dr);

            ds.Tables[0].Columns.Remove("att");
            ds.Tables[0].Columns.Remove("vehicleid");

            Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (AllowToSave())
        {
            Save("SaveBtn");
        }
    }

    private bool AllowToSave()
    {
        bool ATS = false;

        CheckBox chk;
        int i;

        for (i = 0; i <= dgGrid.Items.Count - 1; i++)
        {
            chk = (CheckBox)dgGrid.Items[i].FindControl("Chk_Att");

            if (chk.Checked == true)
            {
                ATS = true;
                break;
            }
        }

        if (ATS == false)
        {
            ErrorMessage = "Select Atleast One Record";
        }

        return ATS;
    }


    private Message Save(string CallFrom)
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = { 
            objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@DetailsXML", SqlDbType.Xml, 0,DetailsXML), 
            objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0, UserManager.getUserParam().UserId)};

        objDAL.RunProc("[dbo].[EC_Opr_Driver_Salary_Scheduled_Vehicles_For_Salary_Save]", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);

        if (objMessage.messageID == 0)
        {

            ErrorMessage = "Saved SuccessFully";
            string _Msg = "Saved SuccessFully";

            if (CallFrom == "SaveBtn")
            {
                ClearVariables();
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            }
        }
        else
        {
            ErrorMessage = objMessage.message;
        }
        return objMessage;
    }

    public void ClearVariables()
    {
        dgGrid.DataSource = null;
        dgGrid.DataBind();

    }



}
