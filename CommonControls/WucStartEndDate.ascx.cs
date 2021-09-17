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
using ClassLibraryMVP.Security;
using Raj.EC;
using Raj.EC.ControlsView;

public partial class CommonControls_WucStartEndDate : System.Web.UI.UserControl
{
    public String Calendar_Img_Path;
    public string _setAttribute;
    public EventHandler OnDateChange;

    public DateTime Start_Date
    {
        get {return Picker.SelectedDate;}
        set { Picker.SelectedDate = value;}
    }

    public DateTime End_Date
    {
        get { return Picker1.SelectedDate; }
        set { Picker1.SelectedDate = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        String Base_Url;
        Base_Url = Util.GetBaseURL();
        Calendar_Img_Path = Base_Url + "/Images/btn_calendar.gif";
        Picker.Attributes.Add("Picker_OnSelectionChanged(picker)", "return SetDateToLable()");
    
    }

    protected void btn_Change_Click(object sender, EventArgs e)
    {
        if (OnDateChange != null)
        {
            OnDateChange(sender,e);
        }
    }
}
