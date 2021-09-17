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
using Raj.EC.FinancePresenter;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EC.FinanceView;
using Raj.EC;


public partial class Finance_Accounting_Vouchers_FrmMRDelivery : ClassLibraryMVP.UI.Page
{
   
    MRDeliveryPresenter ObjMRDeliveryPresenter;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        tr_save.Visible = false;
    }

    public bool ValidateUI()
    {
        bool IsValid = true;

        if (WucMRDelivery1.validateUI() == false)
        {
            //MultiPage1.SelectedIndex = 0;
            //TabStrip1.SelectedTab = TabStrip1.Tabs[0];
            IsValid = false;
        }      
        else
        {
            IsValid = true;
        }

        return IsValid;
    }
     protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (ValidateUI() == true)
            ObjMRDeliveryPresenter.Save();
    }
    
}
