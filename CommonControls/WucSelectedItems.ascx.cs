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
using System.Text;

using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC.ControlsView;
using Raj.EC;

public partial class CommonControls_WucSelectedItems : System.Web.UI.UserControl
{
    public event EventHandler GetSelectedItemsXMLButtonClick;
    //string SelectedItemsXML = "";
    Common objComm = new Common();
    DataTable dtdetails1 = new DataTable();


    public string SetFoundCaption
    {
        set { lbl_get_item.Text = value; }
    }
    public string SetNotFoundCaption
    {
        set { lbl_get_not_item.Text = value; }
    }
    public string EnterItem
    {
        get { return  txt_get_item.Text.Trim(); }
    }
    public string SetTDCaptionWidth
    {
        set 
        {
            td_get_item.Style.Add("width", value);
            td_get_not_item.Style.Add("width", value);
        }
    }

    public string SetTDDataWidth
    {
        set
        {
            td_get_item_data.Style.Add("width", value);
            td_get_Not_item_data.Style.Add("width", value);
        }
    }

    public void SetsmallTextboxWidth()
    {      
        txt_get_item.Style.Add("width", "600px");
        txt_get_not_item.Style.Add("width", "540px");        
    }

    public string SetNotFoundItemTextboxWidth
    {
        set
        {
            txt_get_not_item.Style.Add("width", value);
        }
    }
    public string Set_GCCaption
    {
        set
        {
            hdn_gcCaption.Value =  value;
        }
    }

    public string GetSelectedItemsXML
    {
        //get { return SelectedItemsXML; }
        get { return Get_Items_XML(); }
    }

    public DataTable dtdetails
    {
        get { return dtdetails1; }
        set { dtdetails1 = value; }
    }

    protected void btn_get_data_Click(object sender, EventArgs e)
    {
        Arrange();
        Get_Items_XML();
        GetSelectedItemsXMLButtonClick(sender, e);
    }

    private string Get_Items_XML()
    {
        StringBuilder EnteredItemsXml = new StringBuilder();

        string Comma_Seperated_Items = "";
        Comma_Seperated_Items = txt_get_item.Text.Trim();

        if (Comma_Seperated_Items == "")
        {
            EnteredItemsXml.Append("<parentroot><root><item>0</item></root></parentroot>");
        }
        else
        {
            string[] EnteredItems = Comma_Seperated_Items.Trim().Split(new char[] { ',' });

            EnteredItemsXml.Append("<parentroot>");
            for (int i = 0; i <= EnteredItems.Length - 1; i++)
            {
                EnteredItemsXml.Append("<root><item>");
                EnteredItemsXml.Append(EnteredItems[i].Trim());
                EnteredItemsXml.Append("</item></root>");
            }
            EnteredItemsXml.Append("</parentroot>");
        }
        //SelectedItemsXML = EnteredItemsXml.ToString();
        return EnteredItemsXml.ToString();
    }

    private void Arrange()
    {
        string CSV = "";
        string paddedvalue = "";
        string Comma_Seperated_Items = "";
        Comma_Seperated_Items = txt_get_item.Text.Trim();

        string[] EnteredItems = Comma_Seperated_Items.Trim().Split(new char[] { ',' });

        for (int i = 0; i <= EnteredItems.Length - 1; i++)
        {
            string[] newvalue = EnteredItems[i].Trim().Split(new char[] { '-' });

            if (newvalue.Length == 1)
            {
                paddedvalue = "";
                for (int j = 1; j <= Util.String2Int(ViewState["Actualgcmaxlength"].ToString()); j++)
                {
                    paddedvalue = paddedvalue + "0";
                }
                if (CSV == "")
                    //CSV = Util.String2Int(newvalue[0]).ToString(paddedvalue);
                    CSV = Util.String2Int(newvalue[0]).ToString();
                else
                    //CSV = CSV + "," + Util.String2Int(newvalue[0]).ToString(paddedvalue);
                    CSV = CSV + "," + Util.String2Int(newvalue[0]).ToString();
            }
            else
            {
                if (CSV == "")
                    CSV = EnteredItems[i].Trim();
                else
                    CSV = CSV + EnteredItems[i].Trim();
            }
        }

        txt_get_item.Text = CSV;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            ViewState["Actualgcmaxlength"] = objComm.Get_Values_Where("EC_Master_Company_GC_Parameter", "GC_No_Length", "", "GC_No_Length", false).Tables[0].Rows[0]["GC_No_Length"].ToString();

            if (CompanyManager.getCompanyParam().ClientCode.ToLower() == "nandwana")
            {
                hdn_gcMaxLength.Value = "15";
                txt_get_item.Attributes.Add("onblur", "Uppercase(" + txt_get_item.ClientID + ")");
            }
            else
            {
                hdn_gcMaxLength.Value = ViewState["Actualgcmaxlength"].ToString();

                txt_get_item.Attributes.Add("onkeyup", "validate_Input(" + txt_get_item.ClientID + ")");
                txt_get_item.Attributes.Add("onblur", "validate_Input(" + txt_get_item.ClientID + ");Uppercase(" + txt_get_item.ClientID + ")");
                txt_get_not_item.Attributes.Add("onkeyup", "validate_Input(" + txt_get_not_item.ClientID + ")");
                txt_get_not_item.Attributes.Add("onblur", "validate_Input(" + txt_get_not_item.ClientID + ")");
            }
        }
    }

    public void Get_Not_Selected_Items()
    {
        StringBuilder EnteredItemsXml = new StringBuilder();
        string NotSelectedItems = "";

        string Comma_Seperated_Items = "";
        Comma_Seperated_Items = txt_get_item.Text;

        DataRow dr = null;
        
        int i = 0;
        int j = 0;
        bool Found = false;
        int Dataset_count = 0;

        Dataset_count = dtdetails.Rows.Count;
        if (Dataset_count > 0)
        {
            string[] EnteredItems = Comma_Seperated_Items.Split(new char[] { ',' });

            for (i = 0; i <= EnteredItems.Length - 1; i++)
            {
                Found = false;
                for (j = 0; j <= Dataset_count - 1; j++)
                {
                    dr = dtdetails.Rows[j];
                    if (EnteredItems[i].ToString().Trim().ToLower() == dr["Item_No"].ToString().Trim().ToLower())
                    {
                        Found = true;
                        break;
                    }

                    if (j == Dataset_count - 1)
                    {
                        if (Found == false)
                        {
                            if (NotSelectedItems == string.Empty)
                            {
                                NotSelectedItems = EnteredItems[i];
                            }
                            else
                            {
                                NotSelectedItems = NotSelectedItems + "," + EnteredItems[i];
                            }
                        }
                    }
                }
            }

            txt_get_not_item.Text = NotSelectedItems;
        }
        else
        {
            txt_get_not_item.Text = EnterItem;
        }
    } 
}
