
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


public partial class EC_Master_wucItem : System.Web.UI.UserControl, IItemView
{
    private ItemPresenter _ItemPresenter;
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

    

     

    public string ItemName
    {
        set
        {
            txt_ItemName.Text = value;
        }
        get
        {
            return txt_ItemName.Text;
        }
    }



    public string Description
    {
        set
        {
            txt_Description.Text = value;
        }
        get
        {
            return txt_Description.Text;
        }
    }

    public int Commodity_Id
    {
        set
        {
            ddl_Commodity.SelectedValue = Convert.ToString(value);
        }
        get
        {
            return Convert.ToInt32(ddl_Commodity.SelectedValue);
        }
    } 

    public int ItemId
    {
        set
        {
            hdn_ItemId.Value = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(hdn_ItemId.Value);
        }
    }

    public decimal ItemRatePerKg
    {
        set { txt_ItemRatePerKg.Text = Util.Decimal2String(value); }
        get { return txt_ItemRatePerKg.Text == string.Empty ? 0 : Util.String2Decimal(txt_ItemRatePerKg.Text); }
    }

 

    #endregion

    #region ControlsBind

    public DataTable BindCommodity 
    {
        set
        {
            ddl_Commodity.DataSource = value;
            ddl_Commodity.DataTextField = "Commodity_Name";
            ddl_Commodity.DataValueField = "Commodity_Id";
            ddl_Commodity.DataBind();

            Raj.EC.Common.InsertItem(ddl_Commodity);
        }
    }

    #endregion
    
     
      
    #region Function


    public bool validateUI()
    {

        isValid = false;

        errorMessage = "";

        if (txt_ItemName.Text.Trim() == String.Empty)
        {
            errorMessage = "Please Enter Item Name.";
            txt_ItemName.Focus();
        }
        else if (ddl_Commodity.Items.Count <= 0)
        {
            errorMessage = "Please Select Commodity Name.";
        }
        else if (Util.String2Int(ddl_Commodity.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Commodity Name.";
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
        _ItemPresenter = new ItemPresenter(this, IsPostBack);        
    }
 
    protected void btn_Save_Click(object sender, EventArgs e)
    {

        if (validateUI())
        {
            _ItemPresenter.save();

            string _Msg;
            _Msg = "Saved SuccessFully";

            String Call_From;

            Call_From = Request.QueryString["Call_From"];

            String popupScript = "";

            if (Call_From == "GC")
            {
                popupScript = "<script language='javascript'>alert('" + _Msg + "');self.close();Update_Item_Details();</script>";
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
