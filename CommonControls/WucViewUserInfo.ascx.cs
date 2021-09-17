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
using System.IO;

using ClassLibraryMVP;
using System.Text.RegularExpressions;
using Raj.EC;
using ClassLibraryMVP.DataAccess;


public partial class CommonControls_WucViewUserInfo : System.Web.UI.UserControl
{
    #region ClassVariables      
     DataSet ObjDS =new DataSet();
     Common ObjCommon = new Common();    
    #endregion 

     #region OtherMethod
     public void EnableDisableControls()
    {
      int Mode=Convert.ToInt32(Util.DecryptToInt(Request.QueryString["Mode"]));
      if (Mode == ClassLibraryMVP.General.Mode.VIEW)
      {
          ViewUserInfo.Visible = true;
         
      }
      else
      {
          ViewUserInfo.Visible = false;
         
      }
    }
     #endregion




     protected void Page_Load(object sender, EventArgs e)
    {
       
        int IdValue = Convert.ToInt32(Util.DecryptToInt(Request.QueryString["Id"]));
           if (!IsPostBack)
            {
                int Mode = Convert.ToInt32(Util.DecryptToInt(Request.QueryString["Mode"]));
                if (Mode == ClassLibraryMVP.General.Mode.VIEW)
                {
                    ViewUserInfo.Visible = true;
                    ObjDS = ObjCommon.GetUserInfo(IdValue);
                    if (ObjDS.Tables[0].Rows.Count > 0)
                    {
                        lbl_CreatedBy.Text = ObjDS.Tables[0].Rows[0]["User_Name"].ToString();
                        lbl_CreatedOn.Text = String.Format("{0:f}", ObjDS.Tables[0].Rows[0]["CreatedOn"]);
                        lbl_UpdatedBy.Text = ObjDS.Tables[0].Rows[0]["UpdatedUserName"].ToString();
                        lbl_UpdatedOn.Text = String.Format("{0:f}", ObjDS.Tables[0].Rows[0]["UpdatedOn"]);
                    }

                }
                else
                {
                    ViewUserInfo.Visible = false;

                }
                
              }

    }
       

        
     


 }
