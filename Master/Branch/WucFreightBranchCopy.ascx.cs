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
/// Created On    : 11th October 2008
/// Description   : This is the Page For Master Freight Branch Copy 
/// </summary>
/// 

public partial class Master_Branch_WucFreightBranchCopy : System.Web.UI.UserControl, IFreightBranchCopyView
{
    #region ClassVariables
    FreightBranchCopyPresenter objFreightBranchCopyPresenter;
    #endregion

    #region ControlsValue


    public decimal FreightRate
    {
        set
        {
            txt_FreightRate.Text = Util.Decimal2String(value);
        }
        get
        {
            return Util.String2Decimal(txt_FreightRate.Text);
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

    public int CommodityID
    {
        set
        {
            ddl_Commodity.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_Commodity.SelectedValue);
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

    public DataTable Bind_ddl_Commodity
    {
        set
        {
            ddl_Commodity.DataSource = value;
            ddl_Commodity.DataTextField = "Commodity_Name";
            ddl_Commodity.DataValueField = "Commodity_ID";
            ddl_Commodity.DataBind();
            ddl_Commodity.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    
    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;
        if (Util.String2Int(ddl_Branch.SelectedValue) == 0)
        {
            lbl_Errors.Text = "Please Select Copy To Branch";
            _isValid = false;
        }
        else if (Util.String2Int(ddl_Area.SelectedValue) == 0)
        {
            lbl_Errors.Text = "Please Select To Area";
            _isValid = false;
        }
        else if (Util.String2Int(ddl_CopyFromBranch.SelectedValue) == 0)
        {
            lbl_Errors.Text = "Please Select Copy From Branch";
            _isValid = false;
        }
        else if (txt_FreightRate.Text.Trim() == "")
        {
            lbl_Errors.Text = "Please Enter Rate to add or subract";
            _isValid = false;
        }
        else if (hdn_Mode.Value == "2" && (Util.String2Int(ddl_Commodity.SelectedValue) == 0))
        {
            lbl_Errors.Text = "Please Select Commodity";
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
        objFreightBranchCopyPresenter = new FreightBranchCopyPresenter(this, IsPostBack);

        if (IsPostBack == false)
        {
            hdn_Mode.Value = Request.QueryString["mode"];

            if (hdn_Mode.Value == "1")
            {
                lbl_Commodity.Visible = false;
                ddl_Commodity.Visible = false;
                lbl_CommodityManadotory.Visible = false;
            }
        }
    }
    protected void btn_Update_Click(object sender, EventArgs e)
    {
        int MsgID = 0;

        string CloseScript = "";
        objFreightBranchCopyPresenter.Save();

        if (MsgID == 0)
        {
            CloseScript = "<script language='javascript'> self.close();</script>";
            Page.ClientScript.RegisterStartupScript(typeof(string), "CloseScript", CloseScript);
        }
    }
    #endregion

}
