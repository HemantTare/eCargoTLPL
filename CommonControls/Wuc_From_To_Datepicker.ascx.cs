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
using System.ComponentModel;
using Raj.EC;

public partial class CommonControls_Wuc_From_To_Datepicker : System.Web.UI.UserControl
{
    public String Calendar_Img_Path;

    protected void Page_Load(object sender, EventArgs e)
    {
        String Base_Url;
        Base_Url = ClassLibraryMVP.Util.GetBaseURL();
        Calendar_Img_Path = Base_Url + "/Images/btn_calendar.gif";

        if (!IsPostBack)
        {
            SelectedFromDate = DateTime.Now;
            SelectedToDate = DateTime.Now;
        }
    }

    public DateTime SelectedFromDate
    {
        get { return Picker_From_Date.SelectedDate; }
        set { Picker_From_Date.SelectedDate = value; }
    }

    public DateTime SelectedToDate
    {
        get { return Picker_To_Date.SelectedDate; }
        set { Picker_To_Date.SelectedDate = value; }
    }

    public string Set_TD_Caption_Width
    {
        set
        {
            td_from_date_caption.Style.Add("width", value);
            td_to_date_caption.Style.Add("width", value);
            td_blank_caption.Style.Add("width", value);
        }
    }

    public string Set_TD_Data_Width
    {
        set
        {
            td_from_date_data.Style.Add("width", value);
            td_to_date_data.Style.Add("width", value);
            td_blank_data_caption.Style.Add("width", value);
        }
    }
    public string Set_To_Date_Visible
    {
        set
        { 
            td_to_date_data.Style.Add("visibility", value);
            td_blank_data_caption.Style.Add("visibility", value);
            lbl_to_date_Caption.Style.Add("visibility", value);
        }
    }

    public string Set_FromDate_Caption
    {
        set{lbl_from_date_Caption.Text = value;}
    }

    public string Set_ToDate_Caption
    {
        set { lbl_to_date_Caption.Text = value; }
    }
}
