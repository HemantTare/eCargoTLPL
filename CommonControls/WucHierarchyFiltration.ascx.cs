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

            if (_HierarchyCode == "HO" || _HierarchyCode == "AD")
            {
                ddl_hierarchy.Items.Insert(0, new ListItem("Select One","0"));
            }

            if (_HierarchyCode == "HO")
            {
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

                if (HierarchyCode == "AO")
                {
                    Location_Caption = "Area :";
                }
                else if (HierarchyCode == "RO")
                {
                    Location_Caption = "Region :";
                }
                else if (HierarchyCode == "BO")
                {
                    Location_Caption = "Branch :";
                }
            }
        }
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
        set {lbl_location_caption.Text = value;}
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
                chk_ViewConsolidated.Visible = value;
                lbl_ViewConsolidated.Visible = value;
            }
    }

    public bool Is_Consol
    {
        get 
        {
            if (HierarchyCode == "BO")
            {
                return false;
            }
            else
            {
                return chk_ViewConsolidated.Checked;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Set_Default_Values();
        }
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
        bool _isvalid = false;

        if (Is_Consol == false && HierarchyCode == "0")
        {
            lbl_Errors.Text = "Please Select Hierarchy or View Consolidated";
        }
        else
        {
            _isvalid = true;
        }

        return _isvalid;
    }
}
