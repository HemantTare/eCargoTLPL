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
using Raj.EC;
using Raj.EC.Security;
using ClassLibraryMVP;

using System.Data.SqlClient;

public partial class Operations_Delivery_wucDeliveryOtherDetails : System.Web.UI.UserControl
{
    #region ClassVariables
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    #endregion
    #region ControlsValue
    public string SetTDCaptionWidth
    {
        set 
        { 
            td_lbl_DeliveryTo.Style.Add("width",value);
            td_lbl_ConsigneeCopy.Style.Add("width",value);
            td_lbl_DeliveryAgainst.Style.Add("width", value);
        }
        
    }
    public string SetDeliveryToCaption
    {
        set
        {
            lbl_DeliveryTo.Text = value;            
        }
    }
    public string SetConsigneeCopyCaption
    {
        set
        {
            lbl_ConsigneeCopy.Text = value;
        }
    }
    public string SetDeliveryAgainstCaption
    {
        set
        {
            lbl_DeliveryAgainst.Text = value;            
        }
    }
    public string SetTDControlWidth
    {
        set
        {
            td_ddl_DeliveryTo.Style.Add("width", value);
            td_ddl_ConsigneeCopy.Style.Add("width", value);
            td_ddl_DeliveryAgainst.Style.Add("width", value);
        }

    }
    public bool SetEnabled
    {
        set
        {
            ddl_DeliveryTo.Enabled=value;
            ddl_ConsigneeCopy.Enabled = value;
            ddl_DeliveryAgainst.Enabled = value;           
            lbl_DeliveryTo.Enabled = value;           
            lbl_DeliveryAgainst.Enabled = value;
            lbl_ConsigneeCopy.Enabled = value;           
        }

    }
    public string SetVisibility
    {
        set
        {
            tr_DeliveryTo.Style.Add("display",value);
            tr_DeliveryAgainst.Style.Add("display", value);
        }

    }
    public string SetDeliveryAgainstVisibility
    {
        set
        {
            td_ddl_DeliveryAgainst.Style.Add("display", value);
            td_lbl_DeliveryAgainst.Style.Add("display", value);
        }

    }
    public int DeliveryToID
    {
        set { ddl_DeliveryTo.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_DeliveryTo.SelectedValue); }
    }
    public int ConsigneeCopyID
    {
        set 
        { 
            ddl_ConsigneeCopy.SelectedValue = Util.Int2String(value);
            if (value == 2)
            {
                lbl_DeliveryAgainst.Visible = true;
                ddl_DeliveryAgainst.Visible = true;
            }
            else
            {
                lbl_DeliveryAgainst.Visible = false;
                ddl_DeliveryAgainst.Visible = false;
            }
        }
        get { return Util.String2Int(ddl_ConsigneeCopy.SelectedValue); }
    }
    public int DeliveryAgainstID
    {
        set { ddl_DeliveryAgainst.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_DeliveryAgainst.SelectedValue); }
    }
    #endregion

    #region OtherMethods
    private void FillDeliveryTo()
    {
      DataSet ds=new DataSet();
      string Query="";
      Query = "select Delivery_To_ID,Delivery_To  From EC_Master_Delivery_To Where Is_Active=1";
      ds=ObjCommon.EC_Common_Pass_Query(Query);
      ddl_DeliveryTo.DataSource=ds;
      ddl_DeliveryTo.DataTextField = "Delivery_To";
      ddl_DeliveryTo.DataValueField = "Delivery_To_ID";
      ddl_DeliveryTo.DataBind();
      ddl_DeliveryTo.Items.Insert(0, new ListItem("Select One", "0")); 
    }
    private void FillConsigneeCopy()
    {
        DataSet ds = new DataSet();
        string Query = "";
        Query = "select Cne_Copy_Status_ID,Cne_Copy_Status From EC_Master_Cne_Copy_Status";
        ds = ObjCommon.EC_Common_Pass_Query(Query);
        ddl_ConsigneeCopy.DataSource = ds;
        ddl_ConsigneeCopy.DataTextField = "Cne_Copy_Status";
        ddl_ConsigneeCopy.DataValueField = "Cne_Copy_Status_ID";
        ddl_ConsigneeCopy.DataBind();
        //ddl_ConsigneeCopy.Items.Insert(0, new ListItem("Select One", "0"));
    }
    private void FillDeliveryAgainst()
    {
        DataSet ds = new DataSet();
        string Query = "";
        Query = "select Delivery_Against_ID,Delivery_Against From EC_Master_Delivery_Against Where Is_Active=1";
        ds = ObjCommon.EC_Common_Pass_Query(Query);
        ddl_DeliveryAgainst.DataSource = ds;
        ddl_DeliveryAgainst.DataTextField = "Delivery_Against";
        ddl_DeliveryAgainst.DataValueField = "Delivery_Against_ID";
        ddl_DeliveryAgainst.DataBind();
        //ddl_DeliveryAgainst.Items.Insert(0, new ListItem("Select One", "0"));
    }
    #endregion

    #region ControlsEvent
    override protected void OnInit(EventArgs e)
    {
        //if (!Page.IsPostBack)
        //{
            base.OnInit(e); 
            FillDeliveryTo();
            FillConsigneeCopy();
            FillDeliveryAgainst();
            //td_ddl_DeliveryAgainst.Style.Add("display", "none");
            //td_lbl_DeliveryAgainst.Style.Add("display", "none");
            
        //}
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddl_ConsigneeCopy_SelectedIndexChanged(sender,e);            
        }
    }

    protected void ddl_ConsigneeCopy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Util.String2Int(ddl_ConsigneeCopy.SelectedValue) == 2)
        {
            lbl_DeliveryAgainst.Visible = true;
            ddl_DeliveryAgainst.Visible = true;
        }
        else
        {
            lbl_DeliveryAgainst.Visible = false;
            ddl_DeliveryAgainst.Visible = false;
        }
    }
}
