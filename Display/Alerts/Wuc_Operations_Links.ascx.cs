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
using System.Text;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.Security;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Display_Alerts_Wuc_Operations_Links : System.Web.UI.UserControl
{
    public Common CommonObj = new Common();
    public int MenuItemId = 0;
    public int UserId;
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            UserId = UserManager.getUserParam().UserId;
            if (CompanyManager.getCompanyParam().ClientCode.ToLower() == "nandwana")
            {
                if (UserManager.getUserParam().HierarchyCode.ToLower() == "bo")
                {
                    CreateShortCutLinks();
                }
                else
                {
                    //this.Visible = false; 
                    ImgF4.Visible = false;
                    lnk_btnF4.Visible = false;

                    ImgF5.Visible = false;
                    lnk_btnF5.Visible = false;

                    ImgF6.Visible = false;
                    lnk_btnF6.Visible = false;

                    ImgF7.Visible = false;
                    lnk_btnF7.Visible = false;

                    ImgF8.Visible = false;
                    lnk_btnF8.Visible = false;

                    ImgF9.Visible = false;
                    lnk_btnF9.Visible = false;
                }

                CreateShortCutLinksStockTally();
                CreateEFleetUrl();
            }
            else
            { this.Visible = false; }
        }

    }

    private void CreateShortCutLinks()
    {

        StringBuilder PathF4 = new StringBuilder(Util.GetBaseURL());
        StringBuilder PathF5 = new StringBuilder(Util.GetBaseURL());
        StringBuilder PathF6 = new StringBuilder(Util.GetBaseURL());
        StringBuilder PathF8 = new StringBuilder(Util.GetBaseURL());
        StringBuilder PathF9 = new StringBuilder(Util.GetBaseURL());


        if ((Rights.GetObject().getForm_Rights(30).canAdd()) == true)
        {
            StateManager.SaveState("QueryString", Rights.GetObject().GetLinkDetails(30).QueryString);
            PathF4.Append("/");
            PathF4.Append(Rights.GetObject().GetLinkDetails(30).AddUrl);
            lnk_btnF4.Attributes.Add("onclick", "return OpenF4Menu('" + PathF4 + "','" + Convert.ToString(30) + "')");
            hdn_F4.Value = "true";
            hdn_F4Path.Value = PathF4.ToString();
        }
        else
        {
            lnk_btnF4.Visible = false;
            ImgF4.Visible = false;
            hdn_F4.Value = "false";
            hdn_F4Path.Value = "";
        }
        Boolean AllowBooking = UserManager.getUserParam().AllowBooking;

        if (AllowBooking == false)
        {
            lnk_btnF4.Visible = false;
            ImgF4.Visible = false;
            hdn_F4.Value = "false";
            hdn_F4Path.Value = "";
        }

        if ((Rights.GetObject().getForm_Rights(51).canAdd()) == true)
        {
            PathF5.Append("/");
            PathF5.Append(Rights.GetObject().GetLinkDetails(51).AddUrl);
            lnk_btnF5.Attributes.Add("onclick", "return OpenF5Menu('" + PathF5 + "','" + Convert.ToString(51) + "')");
            hdn_F5.Value = "true";
            hdn_F5Path.Value = PathF5.ToString();
        }
        else
        {
            lnk_btnF5.Visible = false;
            ImgF5.Visible = false;
            hdn_F5.Value = "false";
            hdn_F5Path.Value = "";
        }

        if ((Rights.GetObject().getForm_Rights(73).canAdd()) == true)
        {
            //StateManager.SaveState("QueryString", Rights.GetObject().GetLinkDetails(73).QueryString);
            PathF6.Append("/");
            PathF6.Append(Rights.GetObject().GetLinkDetails(73).AddUrl);
            lnk_btnF6.Attributes.Add("onclick", "return OpenF6Menu('" + PathF6 + "','" + Convert.ToString(73) + "')");
            hdn_F6.Value = "true";
            hdn_F6Path.Value = PathF6.ToString();
        }
        else
        {
            lnk_btnF6.Visible = false;
            ImgF6.Visible = false;
            hdn_F6.Value = "false";
            hdn_F6Path.Value = "";
        }

        String url;
        url = CommonObj.getBaseURL() + "/TrackNTrace/FrmMainTrackNTrace.aspx";
        lnk_btnF7.Attributes.Add("onclick", "return newwindow1('" + url + "')");
        hdn_F7Path.Value = url;

        if ((Rights.GetObject().getForm_Rights(5221).canRead()) == true)
        {
            PathF8.Append("/");
            PathF8.Append(Rights.GetObject().GetLinkDetails(5221).ReportUrl);
            lnk_btnF8.Attributes.Add("onclick", "return LoadPopUp('" + PathF8 + "','" + Convert.ToString(5221) + "')");
            hdn_F8.Value = "true";
            hdn_F8Path.Value = PathF8.ToString();
        }
        else
        {
            lnk_btnF8.Visible = false;
            ImgF8.Visible = false;
            hdn_F8.Value = "false";
            hdn_F8Path.Value = "";
        }

        if ((Rights.GetObject().getForm_Rights(5159).canRead()) == true)
        {
            PathF9.Append("/");
            PathF9.Append(Rights.GetObject().GetLinkDetails(5159).ReportUrl);
            lnk_btnF9.Attributes.Add("onclick", "return LoadPopUp('" + PathF9 + "','" + Convert.ToString(5159) + "')");
            hdn_F9.Value = "true";
            hdn_F9Path.Value = PathF9.ToString();
        }
        else
        {
            lnk_btnF9.Visible = false;
            ImgF9.Visible = false;
            hdn_F9.Value = "false";
            hdn_F9Path.Value = "";
        }
    }

    private void CreateShortCutLinksStockTally()
    {
        StringBuilder PathF10 = new StringBuilder(Util.GetBaseURL());

        if ((Rights.GetObject().getForm_Rights(5253).canRead()) == true)
        {
            PathF10.Append("/");
            PathF10.Append(Rights.GetObject().GetLinkDetails(5253).ReportUrl);
            lnk_btnF10.Attributes.Add("onclick", "return LoadPopUp('" + PathF10 + "','" + Convert.ToString(5253) + "')");
            hdn_F10.Value = "true";
            hdn_F10Path.Value = PathF10.ToString();
        }
        else
        {
            lnk_btnF10.Visible = false;
            ImgF10.Visible = false;
            hdn_F10.Value = "false";
            hdn_F10Path.Value = "";
        }
    }

    private void CreateEFleetUrl()
    {
        string totalcount = "0";
        string totalcountPM = "0";

        CommonObj.RunTaskScheduler();

        ds = CommonObj.GetPMTaskAlertCount("UserDesk");
        
        if (ds.Tables[0].Rows.Count > 0)
            totalcount = ds.Tables[0].Compute("SUM([TaskCount])", "").ToString();

        if (ds.Tables[1].Rows.Count > 0)
            totalcountPM = ds.Tables[1].Compute("SUM([TaskCount])", "").ToString();


        if ((Rights.GetObject().getForm_Rights(5254).canRead()) == true && Util.String2Int(totalcount) > 0)
        {
            lnk_efleet.Text = "Vehicle Renewals(" + totalcount + ")";
            lnk_efleet.Attributes.Add("onclick", "return FleetLinksDetails('Renewals');");
        }
        else
        {
            lnk_efleet.Visible = false;
            Image11.Visible = false;
        }


        if ((Rights.GetObject().getForm_Rights(5255).canRead()) == true && Util.String2Int(totalcountPM) > 0)
        {
            lnk_efleetPM.Text = "Vehicle PM(" + totalcountPM + ")";
            lnk_efleetPM.Attributes.Add("onclick", "return FleetLinksDetails('PM');");
        }
        else
        {
            lnk_efleetPM.Visible = false;
            Image12.Visible = false;
        }

    }
}
