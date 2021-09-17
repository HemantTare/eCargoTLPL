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
using Raj.EC;
using ClassLibraryMVP.DataAccess;

public partial class TrackNTrace_WucTrackNTraceUserInformation : System.Web.UI.UserControl
{
    private DataSet objDS;
    private DAL objDAL = new DAL();

    int User_ID;
    string Crypt;

    protected void Page_Load(object sender, EventArgs e)
    {
        Crypt = Request.QueryString["U_Id"].ToString();
        User_ID = ClassLibraryMVP.Util.DecryptToInt(Crypt);
        fill_User_Details();
    }

    private void fill_User_Details()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@User_ID", SqlDbType.Int, 20, User_ID) };

        objDAL.RunProc("dbo.EC_Opr_TrackNTrace_User_Details", objSqlParam, ref objDS);

        DetailsView1.DataSource = objDS;
        DetailsView1.DataBind();
    }

    protected void DetailsView1_DataBound(object sender, EventArgs e)
    {
        int i ;
        for(i = 0;i < DetailsView1.Rows.Count - 1;i++)
        {
            DetailsView1.Rows[i].Attributes.Add("onmouseover", "this.style.backgroundColor='#E5E5E5'");
            DetailsView1.Rows[i].Attributes.Add("onmouseout", "this.style.backgroundColor='white'");
        }
        DetailsView1.RowStyle.Height = 17;
    }
}
