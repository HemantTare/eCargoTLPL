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
using Raj.EC.MasterPresenter;
using Raj.EC.MasterView;

/// <summary>
/// Author        : Aashish Lad
/// Created On    : 10th October 2008
/// Description   : This is the Page For Master Standard Crossing Rate Copy 
/// </summary>


public partial class Master_Branch_WucStandardCrossingRateCopy : System.Web.UI.UserControl,IStandardCrossingRateCopyView 
{
    #region ClassVariables
    StandardCrossingRateCopyPresenter objStandardCrossingRateCopyPresenter;
    #endregion

    #region ControlsValue


    public decimal HamaliRate
    {
        set
        {
            txt_Hamali.Text = Util.Decimal2String(value);
        }
        get
        {
            return Util.String2Decimal(txt_Hamali.Text);
        }

    }

    public decimal HireRate 
    {
        set
        {
            txt_HireRate.Text = Util.Decimal2String(value);
        }
        get
        {
            return Util.String2Decimal(txt_HireRate.Text);
        }
    }
    public int AreaID
    {
        set
        {
            ddl_Area.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_Area.SelectedValue);
        }

    }
    public int FromBranchID
    {
        set
        {
            ddl_Branch.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_Branch.SelectedValue);
        }

    }
    public int CopyFromBranchID
    {
        set
        {
            ddl_CopyFromBranch.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_CopyFromBranch.SelectedValue);
        }

    }
    #endregion

    #region ControlsBind
    public DataTable Bind_ddl_Area
    {
        set
        {
            ddl_Area.DataSource = value;
            ddl_Area.DataTextField = "Area_Name";
            ddl_Area.DataValueField = "Area_Id";
            ddl_Area.DataBind();
            ddl_Area.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable Bind_ddl_FromBranchID
    {
        set
        {
            ddl_Branch.DataSource = value;
            ddl_Branch.DataTextField = "Branch_Name";
            ddl_Branch.DataValueField = "Branch_Id";
            ddl_Branch.DataBind();
            ddl_Branch.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable Bind_ddl_CopyFromBranchID
    {
        set
        {
            ddl_CopyFromBranch.DataSource = value;
            ddl_CopyFromBranch.DataTextField = "Branch_Name";
            ddl_CopyFromBranch.DataValueField = "Branch_Id";
            ddl_CopyFromBranch.DataBind();
            ddl_CopyFromBranch.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;
        if (Util.String2Int(ddl_Branch.SelectedValue) == 0)
        {
            lbl_Errors.Text = "Please select Copy To Branch";
            _isValid = false;
        }
        else if (Util.String2Int(ddl_Area.SelectedValue) == 0)
        {
            lbl_Errors.Text = "Please select To Area";
            _isValid = false;
        }
        else if (Util.String2Int(ddl_CopyFromBranch.SelectedValue) == 0)
        {
            lbl_Errors.Text = "Please Select Copy From Branch";
            _isValid = false;
        }
        else if (Util.String2Int(txt_Hamali.Text) < 0)
        {
            lbl_Errors.Text = "Please Enter Hamali Rate";
            _isValid = false;
        }
        else if (Util.String2Int(txt_HireRate.Text) < 0)
        {
            lbl_Errors.Text = "Please Enter Hire Rate";
            _isValid = false;
        }

        else
        {
            _isValid = true;
        }

        return _isValid;
    }


    public string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;
        }
    }


    public int keyID
    {
        get
        {
            //return Util.DecryptToInt(Request.QueryString["Id"]);
            return -1;
        }
    }

    #endregion


    #region OtherProperties

    #endregion


    #region OtherMethods

    #endregion
    #region ControlsEvent


    protected void Page_Load(object sender, EventArgs e)
    {
        objStandardCrossingRateCopyPresenter = new StandardCrossingRateCopyPresenter(this, IsPostBack);
    }
    protected void btn_Update_Click(object sender, EventArgs e)
    {
        int MsgID = 0;

        string CloseScript = "";
        objStandardCrossingRateCopyPresenter.Save();

        if (MsgID == 0)
        {
            CloseScript = "<script language='javascript'> self.close();</script>";
            Page.ClientScript.RegisterStartupScript(typeof(string), "CloseScript", CloseScript);
        }
    }
    #endregion

}
