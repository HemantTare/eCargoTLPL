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
using Raj.CRM.TransactionView;
using Raj.CRM.TransactionPresenter;
using System.Data.SqlClient;

public partial class CRM_Transaction_WucIncludeExcludeUser : System.Web.UI.UserControl,IIncludeExcludeView
{
    #region ClassVariables
    DataSet objDS;
    DataRow DR = null;
    Label UserId;
    Label UserName;
    Label EmployeeName;
    Label Profile_Name;  
    Label BranchName;   
    Label AreaName;   
    Label RegionName;
    CheckBox Chk;
    Label Is_HO;
   IncludeExcludePresenter objInclueExcludePresenter;   
    #endregion

    #region ControlValue

    public DataSet SessionUserGrid
    {
        get { return StateManager.GetState<DataSet>("UserGrid"); }
        set { StateManager.SaveState("UserGrid", value); }
    }

    public string ProfileName
    {
        set { lbl_ProfileName.Text = value; }
    }
    public string ComplaintNatureName
    {
        set { lbl_ComplaintNatureName.Text = value; }
    }

    public int ProfileId
    {
        get { return Util.DecryptToInt(Request.QueryString["Profile_Id"]); }
        //get { return 152; }
    }

    public int ComplaintNatureId
    {
      get { return Util.DecryptToInt(Request.QueryString["Complaint_Nature_Id"]); }
        //get { return 17; }
    }

    
    public DataSet SessionUserDetails
    {
        get { return StateManager.GetState<DataSet>("UserDetails"); }
        set { StateManager.SaveState("UserDetails", value); }
    }
   

   
    #endregion

    #region IView
    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public bool validateUI()
    {
        bool _isValid = true;
        //errorMessage = "";


         return _isValid;
    }

    #endregion


    #region ControlBind
    public DataSet BindGrid
    {
        set
        {
            
            SessionUserGrid=CheckSelectedUsers(value);

            dg_UserProfile.DataSource = SessionUserGrid;
            dg_UserProfile.DataBind();



            //for (int i = 0; i < SessionUserDetails.Tables[0].Rows.Count; i++)
            //{
            //    if (Convert.ToBoolean(SessionUserDetails.Tables[0].Rows[i]["User_Id"]))
            //    {
            //        Chk = (CheckBox)(dg_UserProfile.Items[0].FindControl("Chk_Attach"));
            //        Chk.Checked = true;
            //    }

            //}

          
        }
    }

    private DataSet CheckSelectedUsers(DataSet DS1)
    {

        DataSet DS = null;
        DS = DS1.Copy();
        DS.Tables[0].Columns.Add("Checked");
        
        foreach(DataRow DRowParent in DS.Tables[0].Rows)
        {
            DRowParent["Checked"] = 0;
            foreach (DataRow DRowChild in SessionUserDetails.Tables[0].Rows)
            {
                if(Util.String2Int(DRowChild["User_Id"].ToString()) == Util.String2Int(DRowParent["User_Id"].ToString()))
                {
                    DRowParent["Checked"] = 1;
                    //break;
                }
            }

        }

        return DS;

    }


    #endregion

    #region OtherMethods
    

    private DataSet MakeDS()
    {
        int cnt;

        DataSet objDS;
        objDS = SessionUserDetails;
        objDS.Tables[0].TableName = "IncludeExcludeUserDetails";
       // objDS.Clear();
               

            for (cnt = 0; cnt < dg_UserProfile.Items.Count; cnt++)
            {

                Chk = (CheckBox)(dg_UserProfile.Items[cnt].FindControl("Chk_Attach"));
               


                if (Chk.Checked == true)
                {
                    UserId = (Label)(dg_UserProfile.Items[cnt].FindControl("lbl_UserID"));
                    //UserName = (Label)(dg_UserProfile.Items[cnt].FindControl("lbl_UserName"));
                    //Profile_Name = (Label)(dg_UserProfile.Items[cnt].FindControl("lbl_ProfileName"));
                    //EmployeeName = (Label)(dg_UserProfile.Items[cnt].FindControl("lbl_EmployeeName"));
                    //Is_HO = (Label)(dg_UserProfile.Items[cnt].FindControl("lbl_IsHO"));
                    //BranchName = (Label)(dg_UserProfile.Items[cnt].FindControl("lbl_BranchName"));
                    //AreaName = (Label)(dg_UserProfile.Items[cnt].FindControl("lbl_AreaName"));
                    //RegionName = (Label)(dg_UserProfile.Items[cnt].FindControl("lbl_RegionName"));

                    DR = objDS.Tables[0].NewRow();
                    //DR["Checked"] = Chk.Checked;
                    DR["User_Id"] = UserId.Text;
                    DR["Profile_Id"] = ProfileId;
                    DR["Complaint_Nature_Id"] = ComplaintNatureId;
                    //DR["User_Name"] = UserName.Text;
                    //DR["Profile Name"] = Profile_Name.Text;
                    //DR["Employee_Name"] = EmployeeName.Text;
                    //DR["Is HO"] = Is_HO.Text;
                    //DR["Branch Name"] = BranchName.Text;
                    //DR["Area Name"] = AreaName.Text;
                    //DR["Region Name"] = RegionName.Text;


                    objDS.Tables["IncludeExcludeUserDetails"].Rows.Add(DR);
                }

            }
            SessionUserDetails = objDS;
            return objDS;
        }      
       
    #endregion

        #region OtherMethod
        //  public bool Valid()
        //{
        //    bool _isValid = false;

        //    DataSet ds = null;
        //    //ds = dg_UserProfile.Items;

        //    foreach (DataGridItem MyItem in dg_UserProfile.Items)
        //    {
        //       Chk = (CheckBox)(dg_UserProfile.Items[0].FindControl("Chk_Attach"));
        //       if (Chk.Checked == null)
        //       {
        //        lbl_Errors.Text = "Please Select One User";
        //        _isValid = false;
        //       }

        //    }
           
        //    return _isValid=true;
        //}
        #endregion

        #region ControlsEvent

        protected void Page_Load(object sender, EventArgs e)
    {
       
       objInclueExcludePresenter=new IncludeExcludePresenter(this,IsPostBack);
        
     }

    protected void btn_Save_Click(object sender, EventArgs e)
    {

     
            for (int i = SessionUserDetails.Tables[0].Rows.Count - 1; i >= 0; i += -1)
            {
                int Ds_ProfileId = Convert.ToInt32(SessionUserDetails.Tables[0].Rows[i]["profile_Id"]);
                int Ds_ComplaintNatureId = Convert.ToInt32(SessionUserDetails.Tables[0].Rows[i]["Complaint_Nature_Id"]);
                if (Ds_ProfileId == ProfileId && Ds_ComplaintNatureId == ComplaintNatureId)
                {
                    SessionUserDetails.Tables[0].Rows[i].Delete();
                    SessionUserDetails.AcceptChanges();
                }
            }

            MakeDS();
            Response.Redirect("~/Display/CloseForm.aspx");
       
    }

#endregion
    protected void dg_UserProfile_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            Chk = (CheckBox)(e.Item.FindControl("Chk_Attach"));
            Label lbl_Checked = (Label)(e.Item.FindControl("lbl_Checked"));
            if (lbl_Checked.Text.ToString() == "0")
            {
                Chk.Checked = false ;
            }
            else
            {
                Chk.Checked = true ;
            }
            
        }
        
    }
}