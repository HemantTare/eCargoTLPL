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
using System.Drawing;

public partial class Reports_User_Desk_frmPendingMemo : System.Web.UI.Page
{
    Raj.EC.Common objComm = new Raj.EC.Common();
    private DataSet objDS;  

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            objDS = objComm.GetPendingMemo();
            dg_Grid.DataSource = objDS;
            dg_Grid.DataBind();
        }
    }

    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        TextBox txt_invoiceno, txt_tripno;
        if (e.Item.ItemIndex != -1)
        {
            txt_invoiceno = (TextBox)e.Item.FindControl("txtInvoiceNo");
            txt_tripno = (TextBox)e.Item.FindControl("txtTripNo");

            if (txt_invoiceno.Text.Trim().ToLower() == "not prepaired")
            {
                txt_invoiceno.ForeColor = Color.Red;
            }

            if (txt_tripno.Text.Trim().ToLower() == "not prepaired")
            {
                txt_tripno.ForeColor = Color.Red;
            }
        }
    }
}
