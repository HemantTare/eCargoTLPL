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
using System.Text;

public partial class Operations_Outward_FrmShortTripVehicles : System.Web.UI.Page
{
    Common objCommon = new Common();


    private string ErrorMessage
    {
        set { lblErrors.Text = value.ToString(); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        if (!IsPostBack)
        {
            FillGrid();

            TextBox txtVehicleNo = (TextBox)(dgGrid.Items[0].FindControl("txtVehicleNo"));
            txtVehicleNo.Focus();
        }

    }

    private void FillGrid()
    {
        DAL objDAL = new DAL();
        DataSet ds = new DataSet();
        objDAL.RunProc("dbo.EC_Opr_Short_Trip_Vehicles_Fill_Blank_Grid", ref ds);

        dgGrid.DataSource = ds;
        dgGrid.DataBind();
    }


    private string VehicleNoXML
    {
        get
        {
            string XML = "<newdataset>";

            for (int i = 0; i <= dgGrid.Items.Count - 1; i++)
            {
                TextBox txtVehicleNo = (TextBox)(dgGrid.Items[i].FindControl("txtVehicleNo"));

                if (txtVehicleNo.Text.Trim() != "")
                {
                    XML = XML + "<details>";
                    XML = XML + "<vehicleno>" + txtVehicleNo.Text + "</vehicleno>";
                    XML = XML + "</details>";
                }
            }

            XML = XML + "</newdataset>";

            return XML;
        }
    }


    private void ShowDetails()
    {

        DataSet objDS = new DataSet();
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@VehiclesXML", SqlDbType.Xml, 0, VehicleNoXML) };

        objDAL.RunProc("dbo.EC_Opr_Short_Trip_Vehicles_Details", objSqlParam, ref objDS);

        dg_Trips.DataSource = objDS.Tables[0];
        dg_Trips.DataBind();

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        ShowDetails();
    }


    protected void dg_Trips_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
       
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            StringBuilder PathF4 = new StringBuilder(Util.GetBaseURL());
            int Vehicle_Id;
            LinkButton lbtn_Vehicle_No;


            Vehicle_Id  = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Vehicle_Id").ToString());

            lbtn_Vehicle_No = (LinkButton)e.Item.FindControl("lbtn_Vehicle_No");

            PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4.Append("/Operations/Outward/FrmShortTripVehiclesMemoDetails.aspx?Vehicle_ID=" + Vehicle_Id + "&Vehicle_No=" + lbtn_Vehicle_No.Text);
            lbtn_Vehicle_No.Attributes.Add("onclick", "return OpenShorTripDetails('" + PathF4 + "')");
        }
    }

}
