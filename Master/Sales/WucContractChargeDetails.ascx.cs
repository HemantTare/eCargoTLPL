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
using Raj.EC;


public partial class Master_Sales_WucContractChargeDetails : System.Web.UI.UserControl
{
    DataSet ds = new DataSet();
    string SrNo,Mode;
    DataRow[] dr;
    int MenuItemId;
   

    protected void Page_Load(object sender, EventArgs e)
    {
        SrNo = ClassLibraryMVP.Util.DecryptToString(Request.QueryString["SrNo"]);
        Mode = ClassLibraryMVP.Util.DecryptToString(Request.QueryString["Mode"]);
        MenuItemId = ClassLibraryMVP.Util.DecryptToInt(Request.QueryString["Menu_Item_Id"]);

        if (!IsPostBack)
        {
            PageControls pc = new PageControls();
            pc.AddAttributes(this.Controls);
            SetOtherCharges();
            if (Mode == "4")
            {
                DisableFields();
            }
        }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        SaveData();
        Response.Write("<script language='javascript'> { self.close() }</script>");
    }
    protected void btn_Exit_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'> { self.close() }</script>");
    }
    private void SetOtherCharges()
    {
        ds = StateManager.GetState<DataSet>("OtherChargesFreightRate");
        dr = ds.Tables[0].Select("SrNo= '" + SrNo + " '");        
        
        if (dr.Length > 0)
        {
            DataRow NewRow;    
            int IndexSrNo=0;
            NewRow = dr[0];
            IndexSrNo = ds.Tables[0].Rows.IndexOf(NewRow);
                    
            txt_BiltyCharges.Text = ds.Tables[0].Rows[IndexSrNo]["Bilty_Charges"].ToString();
            txt_FOVPer.Text = ds.Tables[0].Rows[IndexSrNo]["FOV_Percent"].ToString();
            txt_ToPayCharges.Text = ds.Tables[0].Rows[IndexSrNo]["To_Pay_Charges"].ToString();
            txt_DACCCharges.Text = ds.Tables[0].Rows[IndexSrNo]["DACC_Charges"].ToString();
            txt_LocalCharges.Text = ds.Tables[0].Rows[IndexSrNo]["Local_Charges"].ToString();
            txt_Hamali.Text = ds.Tables[0].Rows[IndexSrNo]["Hamali_Per_Kg"].ToString();
            txt_DoorDeliveryCharges.Text = ds.Tables[0].Rows[IndexSrNo]["Door_Delivery_Charges"].ToString();
            txt_OctroiFormCharges.Text = ds.Tables[0].Rows[IndexSrNo]["Octroi_Form_Charges"].ToString();
            txt_OctroiServiceCharges.Text = ds.Tables[0].Rows[IndexSrNo]["Octroi_Service_Charges"].ToString();
            txt_DemurrageDays.Text = ds.Tables[0].Rows[IndexSrNo]["Demurrage_Days"].ToString();
            txt_DemurrageRate.Text = ds.Tables[0].Rows[IndexSrNo]["Demurrage_Percent"].ToString();
            txt_GICharges.Text = ds.Tables[0].Rows[IndexSrNo]["GI_Charges"].ToString();
            txt_FOVRate.Text = ds.Tables[0].Rows[IndexSrNo]["FOV_Rate"].ToString();
            txt_InvoiceRate.Text = ds.Tables[0].Rows[IndexSrNo]["Invoice_Rate"].ToString();
            txt_InvoicePerRs.Text = ds.Tables[0].Rows[IndexSrNo]["Invoice_Per_How_Many_Rs"].ToString();
            txt_HamaliArticle.Text = ds.Tables[0].Rows[IndexSrNo]["Hamali_per_Article"].ToString();

        }       
    }

    private void DisableFields()
    {
        btn_Save.Visible = false;
        txt_BiltyCharges.Enabled = false;
        txt_FOVPer.Enabled = false;
        txt_ToPayCharges.Enabled = false;
        txt_DACCCharges.Enabled = false;
        txt_LocalCharges.Enabled = false;
        txt_Hamali.Enabled = false;
        txt_DoorDeliveryCharges.Enabled = false;
        txt_OctroiFormCharges.Enabled = false;
        txt_OctroiServiceCharges.Enabled = false;
        txt_DemurrageDays.Enabled = false;
        txt_DemurrageRate.Enabled = false;
        txt_GICharges.Enabled = false;
    }

    private void SaveData()
    {
        DataRow NewRow,RowIndex;
        ds = StateManager.GetState<DataSet>("OtherChargesFreightRate");
        dr = ds.Tables[0].Select("SrNo= '" + SrNo + " '");
        int IndexSrNo=0;
       // IndexSrNo = ds.Tables[0].Rows.IndexOf(dr);
        if (dr.Length > 0)
        {
            RowIndex = dr[0];
            IndexSrNo = ds.Tables[0].Rows.IndexOf(RowIndex);

            ds.Tables[0].Rows[IndexSrNo]["Bilty_Charges"] = Util.String2Decimal(txt_BiltyCharges.Text);
            ds.Tables[0].Rows[IndexSrNo]["FOV_Percent"] = Util.String2Decimal(txt_FOVPer.Text);
            ds.Tables[0].Rows[IndexSrNo]["To_Pay_Charges"] = Util.String2Decimal(txt_ToPayCharges.Text);
            ds.Tables[0].Rows[IndexSrNo]["DACC_Charges"] = Util.String2Decimal(txt_DACCCharges.Text);
            ds.Tables[0].Rows[IndexSrNo]["Local_Charges"] = Util.String2Decimal(txt_LocalCharges.Text);
            ds.Tables[0].Rows[IndexSrNo]["Hamali_Per_Kg"] = Util.String2Decimal(txt_Hamali.Text);
            ds.Tables[0].Rows[IndexSrNo]["Door_Delivery_Charges"] = Util.String2Decimal(txt_DoorDeliveryCharges.Text);
            ds.Tables[0].Rows[IndexSrNo]["Octroi_Form_Charges"] = Util.String2Decimal(txt_OctroiFormCharges.Text);
            ds.Tables[0].Rows[IndexSrNo]["Octroi_Service_Charges"] = Util.String2Decimal(txt_OctroiServiceCharges.Text);
            ds.Tables[0].Rows[IndexSrNo]["Demurrage_Days"] = Util.String2Int(txt_DemurrageDays.Text);
            ds.Tables[0].Rows[IndexSrNo]["Demurrage_Percent"] = Util.String2Decimal(txt_DemurrageRate.Text);
            ds.Tables[0].Rows[IndexSrNo]["GI_Charges"] = Util.String2Decimal(txt_GICharges.Text);
            ds.Tables[0].Rows[IndexSrNo]["FOV_Rate"] = Util.String2Decimal(txt_FOVRate.Text);
            ds.Tables[0].Rows[IndexSrNo]["Invoice_Rate"] = Util.String2Decimal(txt_InvoiceRate.Text);
            ds.Tables[0].Rows[IndexSrNo]["Invoice_Per_How_Many_Rs"] = Util.String2Decimal(txt_InvoicePerRs.Text);
            ds.Tables[0].Rows[IndexSrNo]["Hamali_Per_Article"] = Util.String2Decimal(txt_HamaliArticle.Text);
        }
        else
        {
            NewRow = ds.Tables[0].NewRow();
            NewRow["Contract_ID"] = 0;
            NewRow["SrNo"] =Util.String2Int(SrNo);
            NewRow["Bilty_Charges"] = Util.String2Decimal(txt_BiltyCharges.Text);
            NewRow["FOV_Percent"] = Util.String2Decimal(txt_FOVPer.Text);
            NewRow["To_Pay_Charges"] = Util.String2Decimal(txt_ToPayCharges.Text);
            NewRow["DACC_Charges"] = Util.String2Decimal(txt_DACCCharges.Text);
            NewRow["Local_Charges"] = Util.String2Decimal(txt_LocalCharges.Text);
            NewRow["Hamali_Per_Kg"] = Util.String2Decimal(txt_Hamali.Text);
            NewRow["Door_Delivery_Charges"] = Util.String2Decimal(txt_DoorDeliveryCharges.Text);
            NewRow["Octroi_Form_Charges"] = Util.String2Decimal(txt_OctroiFormCharges.Text);
            NewRow["Octroi_Service_Charges"] = Util.String2Decimal(txt_OctroiServiceCharges.Text);
            NewRow["Demurrage_Days"] = Util.String2Int(txt_DemurrageDays.Text);
            NewRow["Demurrage_Percent"] = Util.String2Decimal(txt_DemurrageRate.Text);
            NewRow["GI_Charges"] = Util.String2Decimal(txt_GICharges.Text);
            NewRow["FOV_Rate"] = Util.String2Decimal(txt_FOVRate.Text);
            NewRow["Invoice_Rate"] = Util.String2Decimal(txt_InvoiceRate.Text);
            NewRow["Invoice_Per_How_Many_Rs"] = Util.String2Decimal(txt_InvoicePerRs.Text);
            NewRow["Hamali_Per_Article"] = Util.String2Decimal(txt_HamaliArticle.Text);

            ds.Tables[0].Rows.Add(NewRow);
        }
        Session["OtherChargesFreightRate"]=ds;
    }
}
