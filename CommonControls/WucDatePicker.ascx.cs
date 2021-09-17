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
public partial class CommonControls_WucDatePicker : System.Web.UI.UserControl
{
     
    
    public String Calendar_Img_Path;

    public event EventHandler DateSelectionChanged;
    public string _injectJSfunction;

    protected void Page_Load(object sender, EventArgs e)
    {
        //hdnMenuItemID.Value = Common.GetMenuItemId().ToString();
    }

    override protected void OnInit(EventArgs e)
    {
        base.OnInit(e);
        String Base_Url;
        Base_Url = ClassLibraryMVP.Util.GetBaseURL();
        Calendar_Img_Path = Base_Url + "/Images/btn_calendar.gif";
        if (!IsPostBack)
        {
            this.SelectedDate = DateTime.Now;
        }
    }

    public bool IsAutoPostBack
    {     
      set
         {
             Picker.AutoPostBackOnSelectionChanged=value;
             Calendar.AutoPostBackOnSelectionChanged = value;
         }
        get { return Picker.AutoPostBackOnSelectionChanged; }
    }

    
    public DateTime SelectedDate
    {
        get { return Picker.SelectedDate;}
        set { Picker.SelectedDate = value;}
    }

    public string InjectJSfunction
    {
        set { _injectJSfunction = value + ";"; }
    }

    public string PickerClientID
    {
        get { return Picker.ClientID; }
    }

    public bool disableForView
    {
        set
        {
            Picker.Enabled = value;
            TD_Calender.Visible = value;
        }
    }

    public bool Disable
    {
        set
        {
            if (value)
            {
                Calendar.AllowDaySelection = false;
                Calendar.AllowMonthSelection = false;
                Calendar.AllowMultipleSelection = false;
                Calendar.AllowWeekSelection = false;
               _injectJSfunction = _injectJSfunction + "DisablePicker(" + PickerClientID + ");";
             }
         }
     }
    protected void Calendar_SelectionChanged(object sender, EventArgs e)
    {
        if (DateSelectionChanged != null)
        {
            DateSelectionChanged(sender, e);
        }
    }

    protected void Picker_SelectionChanged(object sender, EventArgs e)
    {
        if (DateSelectionChanged != null)
        {
            DateSelectionChanged(sender, e);
        }
    }
}
