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
/// Created On    : 07th October 2008
/// Description   : This is the Page For Master Transit Days State To State 
/// </summary>


public partial class Master_Branch_WucFreightCopy : System.Web.UI.UserControl,IFreightCopyView 
{
    #region ClassVariables
    FreightCopyPresenter objFreightCopyPresenter;
    #endregion

    #region ControlsValue


    public decimal Rate
    {
        set
        {
            txt_Rate.Text = Util.Decimal2String(value);
        }
        get
        {
            return Util.String2Decimal(txt_Rate.Text);
        }

    }
    public int StateID
    {
        set
        {
            ddl_State.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_State.SelectedValue);
        }

    }
    public int FromCityID
    {
        set
        {
            ddl_City.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_City.SelectedValue);
        }

    }
    public int CopyFromCityID
    {
        set
        {
            ddl_CopyFromCity.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_CopyFromCity.SelectedValue);
        }

    }
    #endregion

    #region ControlsBind
    public DataTable Bind_ddl_State
    {
        set
        {
            ddl_State.DataSource = value;
            ddl_State.DataTextField = "State_Name";
            ddl_State.DataValueField = "State_Id";
            ddl_State.DataBind();
            ddl_State.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable Bind_ddl_FromCityID
    {
        set
        {
            ddl_City.DataSource = value;
            ddl_City.DataTextField = "City_Name";
            ddl_City.DataValueField = "City_Id";
            ddl_City.DataBind();
            ddl_City.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable Bind_ddl_CopyFromCityID
    {
        set
        {
            ddl_CopyFromCity.DataSource = value;
            ddl_CopyFromCity.DataTextField = "City_Name";
            ddl_CopyFromCity.DataValueField = "City_Id";
            ddl_CopyFromCity.DataBind();
            ddl_CopyFromCity.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;
         if (Util.String2Int(ddl_City.SelectedValue) == 0)
        {
            lbl_Errors.Text = "Please Select Copy To City";
            _isValid = false;
        }
        else if (Util.String2Int(ddl_State.SelectedValue) == 0)
        {
            lbl_Errors.Text = "Please Select To State";
            _isValid = false;
        }
        else if (Util.String2Int(ddl_CopyFromCity.SelectedValue) == 0)
        {
            lbl_Errors.Text = "Please Select Copy From City";
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
        objFreightCopyPresenter = new FreightCopyPresenter(this, IsPostBack);
    }
    
    protected void btn_Update_Click(object sender, EventArgs e)
    {
        int MsgID = 0;

        string CloseScript = "";
        MsgID = objFreightCopyPresenter.Save();

        if (MsgID == 0)
        {
            CloseScript = "<script language='javascript'> self.close();</script>";
            Page.ClientScript.RegisterStartupScript(typeof(string), "CloseScript", CloseScript);
        }
    }
    #endregion
}
