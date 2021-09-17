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
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class TrackNTrace_FrmGCLiveTracking : System.Web.UI.Page
{
    #region Declaration
    private DAL objDAL = new DAL();
    DataSet objDS = new DataSet();
    public String GC_No,Url;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.AppendHeader("Access-Control-Allow-Headers", "X-Requested-With");
        Response.AppendHeader("Access-Control-Allow-Origin", "*");
        Response.AppendHeader("x-frame-options", "ALLOWALL");
        
        if (!IsPostBack)
        {
            //lnk_btnGoogleMap.Text = "Google Maps";
            //lnk_btnGoogleMap.Attributes.Add("onclick", "return GoogleMaps()");

            GC_No = Request.QueryString["GC_No"];
            CurrentStatus();
            lnk_btnGoogleMap_Click(sender, e);
        }
    }

    protected void lnk_btnGoogleMap_Click(object sender, EventArgs e)
    {
        string script = "window.onload = function() { GoogleMaps('" + Url + "');};";
        ClientScript.RegisterStartupScript(this.GetType(), "GoogleMaps", script, true);
    }

    private void CurrentStatus()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@GC_No", SqlDbType.Int, 0, GC_No) };
        objDAL.RunProc("dbo.EC_Opr_TrackNTrace_Current_Status_Tracking", objSqlParam, ref objDS);
        if (objDS.Tables[0].Rows.Count > 0)
            Url = objDS.Tables[0].Rows[0]["Url"].ToString();
        else
            Url = "http://110.227.250.188/ecargolive/TrackNTrace/FrmGCStatusSMS.aspx?GC_No=" + GC_No + "&Msg=UwBNAFMATgBvAA==";
    }
}