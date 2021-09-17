
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
using ClassLibraryMVP.General;
using Raj.EC.MasterView;
using Raj.EC.MasterPresenter; 
using ClassLibraryMVP;
using Raj.EC; 

public partial class EC_Master_wucCommodity : System.Web.UI.UserControl, ICommodityView
{
    private CommodityPresenter _CommodityPresenter;
    bool isValid = false;

    #region IView Members

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public int keyID
    {
        get
        {
               return Util.DecryptToInt(Request.QueryString["Id"]);
          //  return 34;
        }
    }

    #endregion

    #region InitInterface
     

    public string CommodityName
    {
        set
        {
            txt_CommodityName.Text = value;
        }
        get
        {
            return txt_CommodityName.Text;
        }
    }

    public int Commodity_Type_Id
    {
        set
        {
             ddl_CommodityType.SelectedValue = Convert.ToString( value);
        }
        get
        {
            return Convert.ToInt32( ddl_CommodityType.SelectedValue  );
        }
    }


    public int CommodityId
    {
        set
        {
            hdn_CommodityId.Value  = Util.Int2String (value);
        }
        get
        {
            return Util.String2Int (hdn_CommodityId.Value );
        }
    }

    public Boolean Is_Restricted
    {
        set
        {
            chk_IsRestricted.Checked = Convert.ToBoolean(value);
        }
        get
        {
            return Convert.ToBoolean(chk_IsRestricted.Checked);
        }
    }

    public Boolean Is_Perishable
    {
        set
        {
            chk_IsPerishable.Checked = Convert.ToBoolean(value);
        }
        get
        {
            return Convert.ToBoolean(chk_IsPerishable.Checked);
        }
    }

    public Boolean Is_Service_Tax_Applicable
    {
        set
        {
            chk_Is_ServiceTaxApplicable.Checked = Convert.ToBoolean(value);
        }
        get
        {
            return Convert.ToBoolean(chk_Is_ServiceTaxApplicable.Checked);
        }
    }


    public Boolean Is_CST_Applicable
    {
        set
        {
            chk_IsCSTApplicable.Checked = Convert.ToBoolean(value);
        }
        get
        {
            return Convert.ToBoolean(chk_IsCSTApplicable.Checked);
        }
    }
 

    #endregion

    #region ControlsBind

    public DataTable BindCommodityType
    {
        set
        {
            ddl_CommodityType.DataSource = value;
            ddl_CommodityType.DataTextField = "Commodity_Type";
            ddl_CommodityType.DataValueField = "Commodity_Type_Id";
            ddl_CommodityType.DataBind();

            Raj.EC.Common.InsertItem(ddl_CommodityType);
        }
    }

    #endregion
    
     
      
    #region Function


    public bool validateUI()
    {

        isValid = false;

        errorMessage = "";

        if (txt_CommodityName.Text.Trim() == String.Empty)
        {
            errorMessage = "Please Enter Commodity Name.";
            SM_COMMODITY.SetFocus(txt_CommodityName);
        }
        else if (ddl_CommodityType.Items.Count <= 0)
        {
            errorMessage = "Please Select Commodity Type.";
            SM_COMMODITY.SetFocus(ddl_CommodityType);
        }
        else if (Util.String2Int(ddl_CommodityType.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Commodity Type.";
            SM_COMMODITY.SetFocus(ddl_CommodityType);
        }
        else
        {
            isValid = true;
        }

        return isValid;        
         
    }
   

     


   
    #endregion

 

     

    protected void Page_Load(object sender, EventArgs e)
    {       
        _CommodityPresenter = new CommodityPresenter(this, IsPostBack);
        if (!IsPostBack)
        {
            if (keyID <= 0)
            {
                Is_CST_Applicable = true;
                Is_Service_Tax_Applicable = true; //added :Ankit
            }
        }
    }
 
    protected void btn_Save_Click(object sender, EventArgs e)
    {

        if (validateUI())
        {

            _CommodityPresenter.save();


            string _Msg;
            _Msg = "Saved SuccessFully";

            String Call_From;

            Call_From = Request.QueryString["Call_From"];

            String popupScript = "";


            if (Call_From == "GC")
            {
                popupScript = "<script language='javascript'>alert('" + _Msg + "');self.close();Update_Commodity_Details();</script>";
            }
            else
            {
                //popupScript = "<script language='javascript'>alert('" + _Msg + "');self.close();</script>";
                string LinkUrl = ClassLibraryMVP.Security.Rights.GetObject().GetLinkDetails(Common.GetMenuItemId()).LinkUrl;
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + LinkUrl + "&DecryptUrl='No'");
            }

            System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel2, typeof(String), "PopupScript1", popupScript.ToString(), false);

        }
    }
}
