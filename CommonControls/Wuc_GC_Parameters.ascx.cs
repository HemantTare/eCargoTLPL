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
using Raj.EC;

public partial class CommonControls_Wuc_GC_Parameters : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            Fill_Dropdowns();
        }
    }

    public int Booking_Type_ID
    {
        get { return  Util.String2Int(ddl_Bkg_type.SelectedValue); }
        set { ddl_Bkg_type.SelectedValue = Util.Int2String(value); }
    }

    public string Booking_Type
    {
        get { return ddl_Bkg_type.SelectedItem.Text; }
    }

    public int Delivery_Type_ID
    {
        get { return Util.String2Int(ddl_dly_type.SelectedValue); }
        set { ddl_dly_type.SelectedValue = Util.Int2String(value); }
    }

    public string Delivery_Type
    {
        get { return ddl_dly_type.SelectedItem.Text; }
    }

    public int Payment_Type_ID
    {
        get { return Util.String2Int(ddl_payment_type.SelectedValue); }
        set { ddl_payment_type.SelectedValue = Util.Int2String(value); }
    }

    public string Payment_Type
    {
        get { return ddl_payment_type.SelectedItem.Text; }
    }

    public string Set_TD_Caption_Width
    {
        set
        {
            td_Bkg_type_caption.Style.Add("width", value);
            td_dly_type_caption.Style.Add("width", value);
            td_payment_type_caption.Style.Add("width", value);
        }
    }

    public string Set_TD_Data_Width
    {
        set
        {
            td_Bkg_type_data.Style.Add("width", value);
            td_dly_type_data.Style.Add("width", value);
            td_payment_type_data.Style.Add("width", value);
        }
    }

    public bool Set_ddl_bkg_type_visibility
    {
        set
        {
            td_Bkg_type_caption.Visible = value;
            td_Bkg_type_data.Visible = value;
        }
    }

    public bool Set_ddl_dly_type_visibility
    {
        set
        {
            td_dly_type_caption.Visible = value;
            td_dly_type_data.Visible = value;
        }
    }

    public bool Set_ddl_payment_type_visibility
    {
        set
        {
            td_payment_type_caption.Visible = value;
            td_payment_type_data.Visible = value;
        }
    }

    private void Fill_Dropdowns()
    {
        Common objcommon = new Common();
        DataSet ds = new DataSet();
        string Query = "";

        Query = "select 0 as Booking_Type_Id,'All' as Booking_Type Union select * from ec_master_booking_type order by Booking_Type";
        ds = objcommon.EC_Common_Pass_Query(Query);
        ddl_Bkg_type.DataSource = ds;
        ddl_Bkg_type.DataTextField = "Booking_Type";
        ddl_Bkg_type.DataValueField = "Booking_Type_Id";
        ddl_Bkg_type.DataBind();

        Query = "select 0 as Delivery_Type_Id,'All' as Delivery_Type Union select * from ec_master_delivery_type order by Delivery_Type";
        ds = objcommon.EC_Common_Pass_Query(Query);
        ddl_dly_type.DataSource = ds;
        ddl_dly_type.DataTextField = "Delivery_Type";
        ddl_dly_type.DataValueField = "Delivery_Type_Id";
        ddl_dly_type.DataBind();

        Query = "select 0 as Payment_Type_Id,'All' as Payment_Type Union select * from ec_master_payment_type order by Payment_Type";
        ds = objcommon.EC_Common_Pass_Query(Query);
        ddl_payment_type.DataSource = ds;
        ddl_payment_type.DataTextField = "Payment_Type";
        ddl_payment_type.DataValueField = "Payment_Type_Id";
        ddl_payment_type.DataBind();
    }
}
