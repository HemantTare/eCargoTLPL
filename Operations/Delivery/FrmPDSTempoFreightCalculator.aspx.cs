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
using Raj.EC;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

public partial class Operations_Delivery_FrmPDSTempoFreightCalculator : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable DT = (DataTable)(Session["BindPDSGrid"]);
        if (Session["BindPDSGrid"] != null && DT.Rows.Count > 0)
        {
            int VehicleID;

            VehicleID = Convert.ToInt32(Request.QueryString["VehicleID"].ToString());

            DT.TableName = "pdsgcids";
            DataTable DT1 = DT.Copy();
            foreach (DataRow dr in DT1.Rows)
            {
                dr["Att"] = true;
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(DT1);

            string gcsXML = ds.GetXml().ToLower();

            DAL objDAL = new DAL();
            SqlParameter[] objSqlParam = {objDAL.MakeInParams("@gcsXML",SqlDbType.Xml,0,gcsXML),
            objDAL.MakeInParams("@PDSID",SqlDbType.Int ,0,null),
            objDAL.MakeInParams("@VehicleID",SqlDbType.Int ,0,VehicleID)};
            objDAL.RunProc("[dbo].[ec_opr_pds_tempo_freight_calculator]", objSqlParam, ref ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                dgGrid.DataSource = ds.Tables[0];
                dgGrid.DataBind();
                txtTotalFrt.Text = Convert.ToString( ds.Tables[0].Compute("SUM(TempoFrt)",""));
                txtTotalWt.Text = Convert.ToString(ds.Tables[0].Compute("SUM(ActualWt)", ""));
            }
        }
    }
}
