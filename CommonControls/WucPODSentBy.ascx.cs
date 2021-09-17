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
using Raj.EC.ControlsPresenter;
using Raj.EC.ControlsView;
using ClassLibraryMVP.General;
using Raj.EC;

public partial class CommonControls_WucPODSentBy : System.Web.UI.UserControl,IPODSentByView
{
    PODSentByPresenter objPODSentByPresenter;


    #region Control Values

    public string SetLabelSentBy
    {
        set{lbl_SentBy.Text = value;}
    }
    public string SetCallFrom
    {
        set { hdn_callfrom.Value = value; }
        get { return hdn_callfrom.Value; }
    }
    public string CourierName
    {
        set
        {
            if (SentByID != 1)
            {
                txt_CourierName.Text = "";
            }
            else
            {
                txt_CourierName.Text = value;
            }
        }
        get
        {
            if (SentByID != 1)
            {
                return "";
            }
            else
            {
                return txt_CourierName.Text.Trim();
            }
        }
    }
    public string CourierDocketNo
    {
        set
        {
            if (SentByID != 1)
            {
                txt_CourierDocketNo.Text = "";
            }
            else
            {
                txt_CourierDocketNo.Text = value;
            }
        }
        get
        {
            if (SentByID != 1)
            {
                return "";
            }
            else
            {
                return txt_CourierDocketNo.Text.Trim();
            }
        }
    }

    public int EmployeeID
    {
        get
        {
            if (SentByID != 2)
            {
                return 0;
            }
            else
            {
                return Util.String2Int(ddl_Employee.SelectedValue);
            }
        }
    }

    public int SentByID
    {
        get { return Util.String2Int(ddl_SentBy.SelectedValue); }
        set { ddl_SentBy.SelectedValue = value.ToString(); }
    }

    public DataTable Bind_ddl_SentBy
    {
        set
        {
            ddl_SentBy.DataSource = value;
            ddl_SentBy.DataTextField = "Cover_Sent_Type";
            ddl_SentBy.DataValueField = "Cover_Sent_Type_ID";
            ddl_SentBy.DataBind();

            Raj.EC.Common.InsertItem(ddl_SentBy);
        }
    }

    public void SetEmployeeId(string text, string value)
    {
        ddl_Employee.DataTextField = "Emp_Name";
        ddl_Employee.DataValueField = "Emp_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Employee);
    }

    public IVehicleSearchView VehicleSearchView 
    {
        get 
        { 
            return (IVehicleSearchView)WucVehicleSearch1;
        }
    }


    public int VehicleID
    {
        get
        {
            if (SentByID != 3)
            {
                return 0;
            }
            else
            {
                return WucVehicleSearch1.VehicleID;
            }
        }
        set
        {
            if (SentByID != 3)
            {
                WucVehicleSearch1.VehicleID = 0;
            }
            else
            {
                WucVehicleSearch1.VehicleID = value;
            }
        }
    }


    public bool IsDllSentByAlreadyBinded
    {
        get { return chk_is_ddl_sent_by_already_binded.Checked ; }
        set { chk_is_ddl_sent_by_already_binded.Checked = value; }
    }
   
    #endregion

    #region IView

    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }

    }

    public bool validateUI()
    {
        return true;

    }

    public string errorMessage
    {
        set { ;}

    }

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        objPODSentByPresenter = new PODSentByPresenter(this, IsPostBack);

        WucVehicleSearch1.DDLVehicleIndexChange += new EventHandler(VehicleIndexChange);

        String Script = "<script type='text/javascript'>Hide_Control(); </script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script", Script, false);
    }


    public bool validateWUCPODDetails(Label lbl_Error)
    {
        if (Util.String2Int(ddl_SentBy.SelectedValue) == 0)
        {
            if (SetCallFrom == "PODDD")
            {
                lbl_Error.Text = "Please Select Received By";
            }
            else
            {
                lbl_Error.Text = "Please Select Sent By";
            }
            ddl_SentBy.Focus();
            return false;
        }
        else if(Util.String2Int(ddl_SentBy.SelectedValue) == 1 && txt_CourierName.Text.Trim() == string.Empty)
        {
            lbl_Error.Text = "Please Enter Courier Name";
            txt_CourierName.Focus();
            return false;
        }
        else if (Util.String2Int(ddl_SentBy.SelectedValue) == 1 && txt_CourierDocketNo.Text.Trim() == string.Empty)
        {
            lbl_Error.Text = "Please Enter Courier Docket No";
            txt_CourierDocketNo.Focus();
            return false;
        }
        else if (Util.String2Int(ddl_SentBy.SelectedValue) == 2 && EmployeeID <= 0)
        {
            lbl_Error.Text = "Please Select Employee";
            return false;
        }
        else if (Util.String2Int(ddl_SentBy.SelectedValue) == 3 && VehicleSearchView.VehicleID <= 0)
        {
            lbl_Error.Text = "Please Select Vehicle";
            return false;
        }
        else
        {
            return true;
        }
    }

    private void VehicleIndexChange(object sender, EventArgs e)
    {
        //if (LHPOTypeID > 0)
        //{
        //    LHPOTypeID = 0;
        //    EnabledDisabledLHPONo();
        //    SetPageControls(sender, e);
        //    EnabledDisabledPageControl();
        //}
        //objLHPOHireDetailsPresenter.SetVehicleInfoOnVehicleChanged();
        //objLHPOHireDetailsPresenter.FillGrid();
    }

    public void Enable_Disable(bool value)
    {

        ddl_SentBy.Enabled = value;
        ddl_Employee.Enabled = value;
        txt_CourierName.Enabled = value;
        txt_CourierDocketNo.Enabled = value;
        WucVehicleSearch1.Enable_Disable(value);
        
    
    }

    public void SetPostBack(bool value)
    {
        WucVehicleSearch1.SetAutoPostBack = value;
    }

    

}
