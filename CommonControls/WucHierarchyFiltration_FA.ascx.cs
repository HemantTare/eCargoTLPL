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
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using System.Data.SqlClient;

public partial class CommonControls_WucHierarchyFiltration : System.Web.UI.UserControl
{

    DAL objDal = new DAL();
    private string _HierarchyCode = UserManager.getUserParam().HierarchyCode;
    private int _Main_Id = UserManager.getUserParam().MainId;
    

    public DataSet Bind_Hierarchy
    {
        set 
        {
            ddl_hierarchy.DataSource = value;
            ddl_hierarchy.DataTextField = "Hierarchy_Name";
            ddl_hierarchy.DataValueField = "Hierarchy_Code";
            ddl_hierarchy.DataBind();
            //ddl_hierarchy.Items.Insert(0, new ListItem("--All Hierarchy--", "0"));
            //if ( _HierarchyCode == "AD")//_HierarchyCode == "HO" || -- comment By Harshal on 06-08-09
            //{
            //    ddl_hierarchy.Items.Insert(0, new ListItem("Select One","0"));
            //}
            if (_HierarchyCode == "HO" || _HierarchyCode == "AD")
            {
                ddl_hierarchy.Items.Insert(0, new ListItem("--All Hierarchy--", "0"));
                HierarchyCode = "HO";
            }

            if (HierarchyCode == "BO")
            {
                Set_Visible_Consolidate = false;
            }
            else
            {
                Set_Visible_Consolidate = true;
            }
        }
    }

    public DataSet Bind_Location
    {
        set
        {
            if (HierarchyCode == "HO" || HierarchyCode == "0")
            {
                Set_Control_Visible = false;
            }
            else
            {
                Set_Control_Visible = true;
            }

            if (value.Tables.Count > 0)
            {
                ddl_location.DataSource = value;
                ddl_location.DataTextField = "Location_Name";
                ddl_location.DataValueField = "Location_Id";
                ddl_location.DataBind();

                //if (_HierarchyCode != "BO" && _HierarchyCode != "AO" && _HierarchyCode != "RO")
                //{
                //    ddl_location.Items.Insert(0, new ListItem("All", "0"));
                //}               

                string _location_Caption="";
                if (HierarchyCode == "AO")
                {
                    _location_Caption = "Area";
                }
                else if (HierarchyCode == "RO")
                {
                    _location_Caption = "Region";
                }
                else if (HierarchyCode == "BO")
                {
                    _location_Caption = "Branch";
                }

                Location_Caption=_location_Caption;

                //ddl_location.Items.Insert(0, new ListItem("--All "+_location_Caption+"--", "0"));
            }
        }
    }

    public DataSet Bind_Division
    {
        set
        {
            ddl_Division.DataSource = value;
            ddl_Division.DataTextField = "Division_Name";
            ddl_Division.DataValueField = "Division_ID";
            ddl_Division.DataBind();
            ddl_Division.Items.Insert(0,new ListItem("--All Division--","0"));
        }
    }

    public int DivisionID
    {
        get { return Convert.ToInt32(ddl_Division.SelectedValue); }
        set { ddl_Division.SelectedValue = value.ToString(); }
    }
    
    public string HierarchyCode
    {
        get {return ddl_hierarchy.SelectedValue;}
        set {ddl_hierarchy.SelectedValue = value;}
    }

    public int Main_Id
    {
        get {
                if (HierarchyCode == "HO" || HierarchyCode == "0")
                {
                    return 0;
                }
                else
                {
                    return Util.String2Int(ddl_location.SelectedValue);
                }
            }
        set {ddl_location.SelectedValue = value.ToString();}
    }

    public string Location_Caption
    {
        set {lbl_location_caption.Text = value+" :";}
    }

    public bool Set_Control_Visible
    {
        set 
            {
                lbl_location_caption.Visible = value;
                ddl_location.Visible = value;
                td_MDddl.Visible = value;
            }
    }

    public bool Set_Visible_Consolidate
    {
        set 
            {
                if (HierarchyCode == "BO" || HierarchyCode == "0" || Main_Id == 0 || HierarchyCode == "HO")
                { chk_ViewConsolidated.Visible = false; }
                else { chk_ViewConsolidated.Visible = true;}
            }
    }
    public bool Set_View_Consolidate
    {
        set
        {
            td_lbl_View_Consolidated.Visible = value;
            td_chk_View_Consolidated.Visible = value;
        }
    }
 

    public bool Is_Consol
    {
        get 
        {
            //if (HierarchyCode == "BO" || HierarchyCode == "0" || Main_Id == 0 || HierarchyCode == "HO")
            if (HierarchyCode == "BO" || HierarchyCode == "HO")
            {
                return false;
            }
            else if ( HierarchyCode == "0")
            {
                return true;
            }
            else
            {
                return chk_ViewConsolidated.Checked;
            }
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
         
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Set_Default_Values();
            DivisionID = UserManager.getUserParam().DivisionId;
        }

        if (ddl_Division.SelectedValue != null)
        {
            if (UserManager.getUserParam().IsDivisionReq == true)
            {
                System.Web.HttpContext.Current.Session["GetSelectedDivisionId"] = ddl_Division.SelectedValue.ToString();
            }
            else
            {
                ddl_Division.Visible = false;
                lbl_Division.Visible = false;
                System.Web.HttpContext.Current.Session["GetSelectedDivisionId"] = UserManager.getUserParam().DivisionId.ToString();
            }
        }

        //if (!IsPostBack)
        //{
        //    if (HierarchyCode == "0")
        //    {
        //        chk_ViewConsolidated.Visible = false;
        //    }
        //}
    }

    private void Fill_Division()
    {
        DataSet ds = new DataSet();
        objDal.RunProc("FA_RPT_Fill_Division", ref ds);
        Bind_Division = ds;
    }

    private void Fill_Hierarchy()
    {
       DataSet ds = new DataSet();

       SqlParameter[] sqlpara = 
                                {
                                objDal.MakeInParams("@HierarchyCode",SqlDbType.VarChar , 2 ,_HierarchyCode)
                                };
       objDal.RunProc("FA_RPT_Fill_Hierarchy", sqlpara, ref ds);

       Bind_Hierarchy = ds;
    }

    private void Fill_Location()
    {
        DataSet ds = new DataSet();

        SqlParameter[] sqlpara = 
                        {
                        objDal.MakeInParams("@LoginHierarchy",SqlDbType.VarChar,2,_HierarchyCode),
                        objDal.MakeInParams("@Main_Id",SqlDbType.Int,0,_Main_Id),
                        objDal.MakeInParams("@HierarchyCode",SqlDbType.VarChar,2,HierarchyCode)
                        };

            objDal.RunProc("FA_RPT_Fill_Location", sqlpara, ref ds);

            Bind_Location = ds;
        
    }

    private void Set_Default_Values()
    {
        Fill_Division();
        Fill_Hierarchy();
        Fill_Location();
    }
    
    
    protected void ddl_hierarchy_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (HierarchyCode == "BO")
        {
            Set_Visible_Consolidate = false;
        }
        else
        {
            Set_Visible_Consolidate = true;
        }

        Fill_Location();
    }


    public bool validateHierarchyFilteration(Label lbl_Errors)
    {
        bool _isvalid = true;

        //if (Is_Consol == false && HierarchyCode != "0")
        //{
        //    lbl_Errors.Text = "Please Select Hierarchy or View Consolidated";
        //}
        //else
        //{
        //    _isvalid = true;
        //}

        return _isvalid;
    }
    protected void ddl_Division_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
