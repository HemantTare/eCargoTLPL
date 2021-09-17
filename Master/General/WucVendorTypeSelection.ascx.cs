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
using Raj.EF.MasterPresenter;
using Raj.EF.MasterView;
using System.Data.SqlClient;

using Raj.EC.ControlsView;
using Raj.EC;

public partial class Master_General_WucVendorTypeSelection : System.Web.UI.UserControl, IVendorTypeSelectionView
{
   
    #region ClassVariables
    VendorTypeSelectionPresenter objVendorTypeSelectionPresenter;
    DataRow DR = null;


    #endregion

    #region ControlsValues

    public DataSet SessionChkListVendorType
    {
        get { return StateManager.GetState<DataSet>("ChkListVendorType"); }
        set { StateManager.SaveState("ChkListVendorType", value); }
    }

    public int KeyNameId
    {
        set { ddl_KeyName.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_KeyName.SelectedValue); }

    }
    public int ChkVendorType
    {
        set
        {
          ChkList_VendorType.SelectedValue=Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ChkList_VendorType.SelectedValue);
        }
    }

    public string ChkListVendorTypeDetails
    {
        get
        {
            return SessionChkListVendorType.GetXml();
        }
    }
    
#endregion

    # region OtherMethod
   private DataSet MakeDS()
    {
        int cnt;
        //DataSet Ds_VendorTypeDetail;
        DataSet objDS;
        objDS= SessionChkListVendorType;
        objDS.Tables[0].TableName = "ChkListVendorTypeDetails";
        objDS.Clear();
       
      
        for (cnt = 0; cnt < ChkList_VendorType.Items.Count ; cnt++)
        {
            
            if (ChkList_VendorType.Items[cnt].Selected == true)
            {
                DR = objDS.Tables[0].NewRow();
                DR["Vendor_Type_ID"] = ChkList_VendorType.Items[cnt].Value;
                DR["Vendor_Type"] = ChkList_VendorType.Items[cnt].Text;
                objDS.Tables["ChkListVendorTypeDetails"].Rows.Add(DR);              
            }       
                   
            
        }

        //Ds_VendorTypeDetail.GetXml();        
        SessionChkListVendorType = objDS;
        return objDS;

    }
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;

        if (ddl_KeyName.SelectedValue=="0")
        {
            errorMessage = "Please Select Key Name";// GetLocalResourceObject("Msg_ddl_KeyName").ToString();
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
            return Util.DecryptToInt(Request.QueryString["Id"]);

        }
    }

    #endregion

    #region ControlsBind
    public DataSet BindKeyName
    {
        set
        {
            ddl_KeyName.DataTextField = "Key_Name";
            ddl_KeyName.DataValueField = "Key_Id";
            ddl_KeyName.DataSource = value;
            ddl_KeyName.DataBind();
            ddl_KeyName.Items.Insert(0, new ListItem("Select One", "0"));
            

        }

    }
    public DataSet BindChkListVendorType
    {
        set
        {
            ChkList_VendorType.DataTextField = "Vendor_Type";
            ChkList_VendorType.DataValueField = "Vendor_Type_ID";       
            ChkList_VendorType.DataSource = value;
            SessionChkListVendorType = value;          
            ChkList_VendorType.DataBind();

            int i;
            if (KeyNameId > 0)
            {
                for (i = 0; i < SessionChkListVendorType.Tables[0].Rows.Count; i++)
                {
                    if (Convert.ToBoolean(SessionChkListVendorType.Tables[0].Rows[i]["Checked"]))
                    {
                        ChkList_VendorType.Items[i].Selected = true;
                    }
                }
            }
           
        }
    }
    
    #endregion

    #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    Common ObjCommon = new Common();
        //    hdf_ResourecString.Value = ObjCommon.GetResourceString("Fleet/Master/General/App_LocalResources/WucVendorTypeSelection.ascx.resx");
        //}
        objVendorTypeSelectionPresenter = new VendorTypeSelectionPresenter(this, IsPostBack);
       
     }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        MakeDS();
        objVendorTypeSelectionPresenter.Save();
            

    }
    protected void ddl_KeyName_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        objVendorTypeSelectionPresenter.FillOnKeyNameChanged();
        
           
    }
    #endregion


}
