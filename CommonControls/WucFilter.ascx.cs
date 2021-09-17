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

public partial class CommonControls_WucFilter : System.Web.UI.UserControl
{
    private string Datatype
    { 
        get 
        {
            string returnval = "";

            if (ddl_column.Items.Count<=0)
                returnval = "";
            else
            {
                string[] ddlvalue = ddl_column.SelectedValue.Split(new char[] { ',' });
                returnval =  ddlvalue[0];
            }

            return returnval;
        }
    }

    public int colid
    {
        get
        {
            int returnval = 0;
            if (ddl_column.Items.Count <= 0)
                returnval = 0;
            else
            {
                string[] ddlvalue = ddl_column.SelectedValue.Split(new char[] { ',' });
                returnval = Convert.ToInt16(ddlvalue[1].ToString());
            }
            return returnval;
        }
    }

    public int Datatype_ID
    {
        get
        {
            int returnval = 0;

            if (Datatype == "s")
                returnval = 1;
            else if (Datatype == "n")
                returnval = 2;
            else if (Datatype == "b")
                returnval = 3;
            else if (Datatype == "d")
                returnval = 4;

            return returnval;
        }
    }

    public int criteriaid
    {
        get
        {
            int returnval = 0;
            if (ddl_criteria.Items.Count <= 0)
                returnval = 0;
            else
            {
                returnval = Convert.ToInt16(ddl_criteria.SelectedValue.ToString());
            }
            return returnval;
        }
    }

    public string Filtered_Text
    {
        get
        {
            return txt_search.Text.Trim();
        }
    }


    public DateTime Filtered_Date
    {
        get
        {
            return WucDatePicker1.SelectedDate;
        }
    }

    public bool Filtered_bit
    {
        get
        {
            bool returnval = false;
            if (ddl_truefalse.SelectedValue == "1")
                returnval = true;
            else
                returnval = false;

            return returnval;
        }
    }

    public string Set_TD_Data_Width
    {
        set
        {
            td_column_data.Width = value;
            td_criteria_data.Width = value;
            td_searchfor_data.Width = value;
        }
    }

    public string Set_TD_caption_Width
    {
        set
        {
            td_column_caption.Width = value;
            td_criteria_caption.Width = value;
            td_searchfor_Caption.Width = value;
        }
    }

    public void setddldatasource(DataSet ds)
    {
        ddl_column.DataTextField = "Datatext_Field";
        ddl_column.DataValueField = "Datavalue_Field";
        ddl_column.DataSource = ds;
        ddl_column.DataBind();

        if (ddl_column.Items.Count <= 0)
            tbl_filter.Visible = false;
        else
        {
            //ResetSearchControls();
            resetddlcriteria();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        js();
    }  

    private void ResetSearchControls()
    {
        txt_search.Text = "";
        WucDatePicker1.SelectedDate = DateTime.Now.Date;
        ddl_truefalse.SelectedValue = "1";

        if (Datatype == "s" || Datatype == "n")
        {
            txt_search.Visible = true;
            WucDatePicker1.Visible = false;
            ddl_truefalse.Visible = false;
        }
        else if (Datatype == "b")
        {
            txt_search.Visible = false;
            WucDatePicker1.Visible = false;
            ddl_truefalse.Visible = true;
        }
        else if (Datatype == "d")
        {
            txt_search.Visible = false;
            WucDatePicker1.Visible = true;
            ddl_truefalse.Visible = false ;
        }
    }

    protected void ddl_column_SelectedIndexChanged(object sender, EventArgs e)
    {
        resetddlcriteria();
        //ResetSearchControls();
    }

    private void resetddlcriteria()
    {
        ddl_criteria.Items.Clear();
        ddl_criteria.Items.Add(new ListItem("Select", "0"));
        if (Datatype == "s")
        {
            ddl_criteria.Items.Add(new ListItem("=", "1"));
            ddl_criteria.Items.Add(new ListItem("Starts With", "6"));
            ddl_criteria.Items.Add(new ListItem("Ends With", "7"));
            ddl_criteria.Items.Add(new ListItem("Contains", "8"));
            //txt_search.Attributes.Add("onkeyPress", "");
        }
        else if (Datatype == "n")
        {
            ddl_criteria.Items.Add(new ListItem("=", "1"));
            ddl_criteria.Items.Add(new ListItem(">=", "2"));
            ddl_criteria.Items.Add(new ListItem("<=", "3"));
            ddl_criteria.Items.Add(new ListItem(">", "4"));
            ddl_criteria.Items.Add(new ListItem("<", "5"));
            //txt_search.Attributes.Add("onkeyPress", "return Only_Numbers(this,event);");
        }
        else if (Datatype == "b")
        {
            ddl_criteria.Items.Add(new ListItem("=", "1"));
        }
        else if (Datatype == "d")
        {
            ddl_criteria.Items.Add(new ListItem("=", "1"));
            ddl_criteria.Items.Add(new ListItem(">=", "2"));
            ddl_criteria.Items.Add(new ListItem("<=", "3"));
            ddl_criteria.Items.Add(new ListItem(">", "4"));
            ddl_criteria.Items.Add(new ListItem("<", "5"));
        }
    }

    private void js()
    {
        string popupScript = "<script language='javascript'>ddl_column_change();</script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(String), "PopupScript", popupScript.ToString(), false);
    }
}
