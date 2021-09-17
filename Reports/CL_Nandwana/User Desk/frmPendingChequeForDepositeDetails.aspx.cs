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

public partial class Reports_CL_Nandwana_User_Desk_frmPendingChequeForDepositeDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        lbl_ChqSentToHOOn.Text = "  " + Request.QueryString["ChqSentToHOOn"].ToString();
        lbl_SentTime.Text = " :  " + Request.QueryString["SentTime"].ToString();
        lbl_ChqSentToHOBy.Text = "  " + Request.QueryString["ChqSentToHOBy"].ToString();
        lbl_ChqRecdAtHOOn.Text = "  " + Request.QueryString["ChqRecdAtHOOn"].ToString();
        lbl_RecdTime.Text = " :  " + Request.QueryString["RecdTime"].ToString();
        lbl_ChqRecdAtHOBy.Text = "  " + Request.QueryString["ChqRecdAtHOBy"].ToString();


        if (lbl_ChqSentToHOOn.Text.Trim() == "")
        {
            lbl_ChqSentToHOOnH.Text = "";
            lbl_ChqSentToHOByH.Text = "";
            lbl_SentTime.Text = "";
        }

        if (lbl_ChqRecdAtHOOn.Text.Trim() == "")
        {
            lbl_ChqRecdAtHOOnH.Text = "";
            lbl_ChqRecdAtHOByH.Text = "";
            lbl_RecdTime.Text = "";
 
        }
    }
}
