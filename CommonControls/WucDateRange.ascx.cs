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


public partial class CommonControls_WucDateRange : System.Web.UI.UserControl
{
   public String Calendar_Img_Path;
    public string _setAttribute;
   

    protected void Page_Load(object sender, EventArgs e)
    {
        
        String Base_Url;   
        Base_Url = Util.GetBaseURL();
        Calendar_Img_Path = Base_Url + "/Images/btn_calendar.gif";
        Picker.Attributes.Add("Picker_OnSelectionChanged(picker)", "return SetDateToLable()");

        if (!IsPostBack)
        {
            
            if (Session["StartDate"] == null)
            {
                Picker.SelectedDate = Convert.ToDateTime(UserManager.getUserParam().StartDate);
            }
            else
            {
                Picker.SelectedDate = StateManager.GetState<DateTime>("StartDate");
            }

            if (Session["EndDate"] == null)
            {
                Picker1.SelectedDate = Convert.ToDateTime(UserManager.getUserParam().EndDate);
            }
            else
            {
                Picker1.SelectedDate = StateManager.GetState<DateTime>("EndDate");
            }
        }
        
    }

    //public DateTime Get_Selected_Date()
    //{
    //    return Picker.SelectedDate;
    //}

    //public DateTime Selected_Date
    //{
    //    get { return Picker.SelectedDate;}
    //    set { Picker.SelectedDate = value;}
    //}

    //public string SetAttribute
    //{
    //    set
    //    {
    //        _setAttribute = value+";";
    //    }
    //}
    protected void btn_Show_Click(object sender, EventArgs e)
    {
        Session["TB_DS"] = null;
        StateManager.SaveState("StartDate", Picker.SelectedDate);
        StateManager.SaveState("EndDate", Picker1.SelectedDate);

        string CloseScript = "<script language='javascript'> " + "window.opener.location.reload();window.close();" + "</script>";
        Page.RegisterStartupScript("CloseScript", CloseScript);


    }
}

