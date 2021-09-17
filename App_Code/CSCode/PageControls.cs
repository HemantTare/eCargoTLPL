using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
/// <summary>
/// Summary description for PageControls
/// </summary>
public class PageControls
{
    DAL objDAL = new DAL();
    Common objCommon = new Common();
    DataSet ds_control_attributes = new DataSet();
    TextBox oTextBox;
    Label oLabel;
    RequiredFieldValidator  oRFV;
    DropDownList oDropDown;
    CheckBox oCheckBox;
    LinkButton oLinkButton;
    Panel oPanel;
    ClassLibrary.UIControl.DDLSearch oDDLSearch;
    HtmlTableCell oHtmlTableCell;
    HtmlTableRow oHtmlTableRow;
    HtmlTable oHtmlTable;

	public PageControls()
	{

	}

    public void AddAttributes(ControlCollection cc)
    {
        SqlParameter[] objSqlParam ={ 
                objDAL.MakeInParams("@MenuItem_Id", SqlDbType.Int, 0, Common.GetMenuItemId())};

        //SqlParameter[] objSqlParam ={ 
        //        objDAL.MakeInParams("@MenuItem_Id", SqlDbType.Int, 0, 1)};

        objDAL.RunProc("COM_Adm_Controls_Attributes_GetValues", objSqlParam, ref ds_control_attributes);

        d(cc);
    }

    private void d(ControlCollection cc)
    {
        int count = cc.Count;

        for (int i = 0; i < count; i++)
        {
            Control c = cc[i];
            //char[] sep = { '_' };
            //string[] control_array = c.ClientID.Split(sep);
            //string control_id = control_array[control_array.Length - 1];

            DataView dv_controls_view = new DataView();

            if (c.ID == null)
                dv_controls_view = objCommon.Get_View_Table(ds_control_attributes.Tables[0], "Control_ID='u347643'");
            else
                dv_controls_view = objCommon.Get_View_Table(ds_control_attributes.Tables[0], "Control_ID='" + c.ID.ToString() + "'");

            if (dv_controls_view.Count > 0)
            {
                switch (c.GetType().ToString())
                {
                    case "ClassLibrary.UIControl.DDLSearch":
                        oDDLSearch = (ClassLibrary.UIControl.DDLSearch)c;
                        oTextBox = (TextBox)oDDLSearch.Controls[0];
                        oTextBox.Style.Add("display", dv_controls_view[0].Row["Visibility"].ToString());
                        oTextBox.Style.Add("Mandatory", dv_controls_view[0].Row["Mandatory"].ToString());
                        break;

                    case "System.Web.UI.WebControls.TextBox":
                        oTextBox = (TextBox)c;
                        oTextBox.Style.Add("display", dv_controls_view[0].Row["Visibility"].ToString());
                        oTextBox.Style.Add("Mandatory", dv_controls_view[0].Row["Mandatory"].ToString());
                        break;

                    case "System.Web.UI.WebControls.Label":
                        oLabel = (Label)c;
                        oLabel.Style.Add("display", dv_controls_view[0].Row["Visibility"].ToString());
                        oLabel.Style.Add("Mandatory", dv_controls_view[0].Row["Mandatory"].ToString());
                        break;

                    case "System.Web.UI.WebControls.Panel":
                        oPanel = (Panel)c;
                        oPanel.Style.Add("display", dv_controls_view[0].Row["Visibility"].ToString());
                        oPanel.Style.Add("Mandatory", dv_controls_view[0].Row["Mandatory"].ToString());
                        break;
                        
                    case "System.Web.UI.WebControls.RequiredFieldValidator":
                        oRFV = (RequiredFieldValidator)c;
                        oRFV.Style.Add("display", dv_controls_view[0].Row["Visibility"].ToString());
                        oRFV.Style.Add("Mandatory", dv_controls_view[0].Row["Mandatory"].ToString());
                        break;

                    case "System.Web.UI.WebControls.DropDownList":
                        oDropDown = (DropDownList)c;
                        oDropDown.Style.Add("display", dv_controls_view[0].Row["Visibility"].ToString());
                        oDropDown.Style.Add("Mandatory", dv_controls_view[0].Row["Mandatory"].ToString());
                        break;


                    case "System.Web.UI.WebControls.CheckBox":
                        oCheckBox = (CheckBox)c;
                        oCheckBox.Style.Add("display", dv_controls_view[0].Row["Visibility"].ToString());
                        oCheckBox.Style.Add("Mandatory", dv_controls_view[0].Row["Mandatory"].ToString());
                        break;


                    case "System.Web.UI.WebControls.LinkButton":
                        oLinkButton = (LinkButton)c;
                        oLinkButton.Style.Add("display", dv_controls_view[0].Row["Visibility"].ToString());
                        oLinkButton.Style.Add("Mandatory", dv_controls_view[0].Row["Mandatory"].ToString());
                        break;

                    case "System.Web.UI.HtmlControls.HtmlTableCell":
                        oHtmlTableCell = (HtmlTableCell)c;
                        oHtmlTableCell.Style.Add("display", dv_controls_view[0].Row["Visibility"].ToString());
                        oHtmlTableCell.Style.Add("Mandatory", dv_controls_view[0].Row["Mandatory"].ToString());
                        break;

                    case "System.Web.UI.HtmlControls.HtmlTableRow":
                        oHtmlTableRow = (HtmlTableRow)c;
                        oHtmlTableRow.Style.Add("display", dv_controls_view[0].Row["Visibility"].ToString());
                        oHtmlTableRow.Style.Add("Mandatory", dv_controls_view[0].Row["Mandatory"].ToString());
                        break;

                    case "System.Web.UI.HtmlControls.HtmlTable":
                        oHtmlTable = (HtmlTable)c;
                        oHtmlTable.Style.Add("display", dv_controls_view[0].Row["Visibility"].ToString());
                        oHtmlTable.Style.Add("Mandatory", dv_controls_view[0].Row["Mandatory"].ToString());
                        break;
                }
            }

            if (c.HasControls())
                d(c.Controls);
        }
    }

    public bool Control_Is_Mandatory(object Control_ID)
    {
        //if (typeof(HtmlContainerControl) == Control_ID.GetType().BaseType)
        //{
        //    HtmlControl objec;
        //    objec = (HtmlControl)(Control_ID);
        //}
        //else
        //{
        //    WebControl objec;
        //    objec = (WebControl)(Control_ID);
        //}

        bool rtnvalue;
        if (Control_ID.GetType().FullName.Contains("System.Web.UI.HtmlControls.HtmlTable"))
        {
            HtmlControl objec;
            objec = (HtmlControl)(Control_ID);

            if (objec.Style["Mandatory"] == "y")
                rtnvalue = true;
            else if (objec.Style["Mandatory"] == "n")
                rtnvalue = false;
            else
                rtnvalue = true;
        }
        else
        {
            WebControl objec;
            objec = (WebControl)(Control_ID);

            if (objec.Style["Mandatory"] == "y")
                rtnvalue = true;
            else if (objec.Style["Mandatory"] == "n")
                rtnvalue = false;
            else
                rtnvalue = true;
        }
        return rtnvalue;
    }

    public void Txtbox_Add_Attributes(ControlCollection cc)
    {
        TextBox oTextBox;

        int count = cc.Count;

        for (int i = 0; i < count; i++)
        {
            Control c = cc[i];

            switch (c.GetType().ToString())
            {
                case "System.Web.UI.WebControls.TextBox":
                    oTextBox = (TextBox)c;
                    string ExistingonfocusAttributes = oTextBox.Attributes["onfocus"];
                    string ExistingonblurAttributes = oTextBox.Attributes["onblur"];

                    if (ExistingonfocusAttributes == null || ExistingonfocusAttributes == "")
                    {
                        ExistingonfocusAttributes = "this.style.backgroundColor = 'yellow';this.select();";
                    }

                    else
                        ExistingonfocusAttributes = "this.style.backgroundColor = 'yellow';this.select();" + ExistingonfocusAttributes;

                    if (ExistingonblurAttributes == null || ExistingonblurAttributes == "")
                        ExistingonblurAttributes = "this.style.backgroundColor = 'white'";
                    else
                        ExistingonblurAttributes = "this.style.backgroundColor = 'white';" + ExistingonblurAttributes;

                    oTextBox.Attributes.Add("onfocus", ExistingonfocusAttributes);
                    oTextBox.Attributes.Add("onblur", ExistingonblurAttributes);
                    break;
            }

            if (c.HasControls())
                Txtbox_Add_Attributes(c.Controls);
        }
    }

}
