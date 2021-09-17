using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using Raj.EC;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.Security;

public partial class Display_FrmDisplay : System.Web.UI.Page
{

    private int _userID;
    private int _systemID;
    public DataSet ObjDS;
    
    Raj.EC.Common CommonObj = new Raj.EC.Common();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            _userID = UserManager.getUserParam().UserId;
            _systemID = UserManager.getUserParam().SystemId;

           
            Rights objRights = new Rights();
            objRights.SetRights(_userID, _systemID, 0, 0);
            lbl_Name.Text = UserManager.getUserParam().FirstName + " " + UserManager.getUserParam().LastName;
            lbl_Date.Text = DateTime.Now.ToString("dddd, dd MMMM, yyyy");
            Add_Attributes();
            Set_Desktop_Information_Value();

            //imgbtn_GSTDetails.Attributes.Add("onclick", "return GSTINDetails()");
            //imgbtn_BranchAddres.Attributes.Add("onclick", "return BranchAddressDetails()");
            imgbtn_SMSLinks.Attributes.Add("onclick", "return SMSLinksDetails()");
        }
    }

    private void Set_Desktop_Information_Value()
    {
        DataSet objds = new DataSet();

        if (Application["DesktopInfo"] == null)
        {

            objds = CommonObj.Get_Values_Where("EC_Master_VTrans_Desktop_Information", "*", "", "ServiceInfoLine1", false);
             
            Application["DesktopInfo"] = objds;


            if ((objds.Tables[0].Rows.Count > 0))
            {
                Application["ServiceInfoLine1"] = objds.Tables[0].Rows[0]["ServiceInfoLine1"].ToString();
                Application["ServiceInfoLine2"] = objds.Tables[0].Rows[0]["ServiceInfoLine2"].ToString();
                Application["ServiceInfoLine3"] = objds.Tables[0].Rows[0]["ServiceInfoLine3"].ToString();

                Application["GeneralInfoLine1"] = objds.Tables[0].Rows[0]["GeneralInfoLine1"].ToString();
                Application["GeneralInfoLine2"] = objds.Tables[0].Rows[0]["GeneralInfoLine2"].ToString();
                Application["GeneralInfoLine3"] = objds.Tables[0].Rows[0]["GeneralInfoLine3"].ToString();
            }
        }

        ServiceInfoLine1.InnerText = Application["ServiceInfoLine1"].ToString();
        ServiceInfoLine2.InnerText = Application["ServiceInfoLine2"].ToString();
        ServiceInfoLine3.InnerText = Application["ServiceInfoLine3"].ToString();

        GeneralInfoLine1.InnerText = Application["GeneralInfoLine1"].ToString();
        GeneralInfoLine2.InnerText = Application["GeneralInfoLine2"].ToString();
        GeneralInfoLine3.InnerText = Application["GeneralInfoLine3"].ToString();

        

    }


    private void Add_Attributes()
    {
        // Change PassWord
        string 
        Url = "../Login/FrmChangePassword.aspx";
        lnk_ChangePassword.Attributes.Add("onclick", "return ChangePassword('" + Url + "')");
        btn_null_sessions.Style.Add("display", "none");

        
    }

    public void ClearVariables()
    {
        System.Web.HttpContext.Current.Session.RemoveAll();
        System.Web.HttpContext.Current.Session.Abandon();
    }
    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
    }
  
}
