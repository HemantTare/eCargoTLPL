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
using Raj.EC.Security;
using ClassLibraryMVP.Security;
using System.Data.SqlClient;
using ClassLibraryMVP.DataAccess;
using Raj.EC;


public partial class Display_WucCommaSeparatedGc : System.Web.UI.UserControl
{
    #region ClassVariables
    private int _ID = 0;
    private int _Menu_Item_ID = 0;
    private DAL objDAL = new DAL();
    #endregion
    
    #region  ControlsValue
    public string CommaSeparatedGc
    {
        set { txt_CommaSeparatedGc.Text = value; }
    }

    public string SetGcCaptionWidth
    {
        set { td_GCCaption.Style.Add("width", value); }
    }
    public string SetGcCaption
    {
        set { lbl_GC_No.Text = value; }
    }
    public string SetCommaSeparatedGcDataWidth
    {
        set { td_CommaSeparatedGCData.Style.Add("Width", value); }
    }
    public bool SetControlEnable
    {
        set { txt_CommaSeparatedGc.Enabled = value; }
    }
    #endregion

    #region PageEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        _ID = Util.DecryptToInt(Request.QueryString["Id"]);

        if (!IsPostBack)
        {
            SqlParameter[] param ={                                
                                  objDAL.MakeInParams("@menuitem_id",SqlDbType.Int,0,Raj.EC.Common.GetMenuItemId()),
                                  objDAL.MakeInParams("@transaction_id",SqlDbType.Int,0,_ID),
                                  objDAL.MakeOutParams("@comma_seperated_gc",SqlDbType.VarChar,8000),
                                  objDAL.MakeOutParams("@txt_box_is_visible",SqlDbType.Bit,0)
                               };

            objDAL.RunProc("[dbo].[EC_Get_Comma_Seperated_GC]", param);
            CommaSeparatedGc = Convert.ToString(param[2].Value);
            Boolean IsTextBoxVisible;
            IsTextBoxVisible = Convert.ToBoolean(param[3].Value);

            if (IsTextBoxVisible == true)
            {
                tr_CommaSeparatedGC.Visible = true;
            }
            else
            {
                tr_CommaSeparatedGC.Visible = false;
            }

        }
    }
    #endregion
}
