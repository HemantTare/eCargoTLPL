using System;
using System.Data;
using System.Data.SqlClient;
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

public partial class Operations_Booking_wucNewRequiredForm : System.Web.UI.UserControl
{
    private DAL objDAL = new DAL();
    private DataSet objDS;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        int DeliveryBranchId = Util.String2Int(Request.QueryString["DeliveryBaranchId"].ToString());
        fillRequiredForms(DeliveryBranchId);
    }
    private void fillRequiredForms(int DeliveryBranchId)
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, DeliveryBranchId) };

        objDAL.RunProc("EC_Opr_NewGC_Get_Required_Forms", objSqlParam, ref objDS);

        lbl_Delivery_Branch_Name_Value.Text = objDS.Tables[1].Rows[0]["Branch_Name"].ToString();

        if (objDS.Tables[0].Rows.Count > 0)
        {
            lbl_Errors.Visible = false;

            lst_RequireForms.DataSource = objDS.Tables[0];
            lst_RequireForms.DataTextField = "Form_Name";
            lst_RequireForms.DataValueField = "Form_Id";
            lst_RequireForms.DataBind();
        }
        else
        {
            lbl_RequireForms.Visible = false;
            lst_RequireForms.Visible = false;

            lbl_Errors.Visible = true;
            lbl_Errors.Text = " No Forms Required.";
        }
    }
}
