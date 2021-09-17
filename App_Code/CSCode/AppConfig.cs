using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Author: Parikshit
/// Created On: 15-May-2008
/// Summary description for AppConfig
/// This Class returns the Value for Unique key Which is Defined in Web.config File
/// </summary>
public class AppConfig
{
    private static string _AUTO_SERIES_NAME = "AUTO SERIES";

    public static string AUTO_SERIES_NAME
    {
        get { return _AUTO_SERIES_NAME; }
    }

    public static string GetParameter(String KeyName)
    {
        return System.Configuration.ConfigurationManager.AppSettings.Get(KeyName);
    }


}
