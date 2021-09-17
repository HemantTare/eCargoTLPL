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
using Raj.EC;
public partial class Operations_Inward_FrmAUSNewExcessDetails : System.Web.UI.Page
{

    #region ClassVariables
    Common objCommon = new Common();
    PageControls pc = new PageControls();

    DataTable objDT;
    DataRow DR = null;
    bool isValid = false;

    DataTable SessionExcessDetailsDT = null;

    DropDownList ddl_PackingType,ddl_Commodity, ddl_Item;
    TextBox txt_GCNo, txt_ExcessArticles, txt_Marking, txt_Remarks;

    int RowNo;
#endregion


    #region ControlsBind

    public DataSet SessionDDLPackingType
    {
        set { StateManager.SaveState("SessionDDLPackingType", value); }
        get { return StateManager.GetState<DataSet>("SessionDDLPackingType"); }
    }
    
    public DataSet SessionDDLItem
    {
        set { StateManager.SaveState("SessionDDLItem", value); }
        get { return StateManager.GetState<DataSet>("SessionDDLItem"); }
    }

    public DataSet SessionDDLCommodity
    {
        set { StateManager.SaveState("SessionDDLCommodity", value); }
        get { return StateManager.GetState<DataSet>("SessionDDLCommodity"); }
    }

    public int TotalExcessArticles
    {
        set
        {
            lbl_TotalExcessArticlesValue.Text = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(lbl_TotalExcessArticlesValue.Text);
        }
    }

#endregion

    #region IView

    private string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;
        }
    }

    #endregion

    #region FillDropDown
    private void FillDropDown()
    {
        SessionDDLPackingType = objCommon.Get_Values_Where("EC_Master_Packing", "Packing_ID,Packing_Type", "Is_Active =1", "Packing_Type", true);
        SessionDDLItem  = objCommon.Get_Values_Where("EC_Master_Item", "Item_ID,Item_Name", "Is_Active =1", "Item_Name",true );
        SessionDDLCommodity = objCommon.Get_Values_Where("EC_Master_Commodity", "Commodity_ID,Commodity_Name", "Is_Active =1", "Commodity_Name", true);
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        SessionExcessDetailsDT = StateManager.GetState<DataTable>("SessionExcessDetailsGrid");

        if (!IsPostBack)
        {
            FillDropDown();
            Bind_Grid();
        }
    }

    public void CalculateTotal(DataTable dt)
    {
        int i = dt.Rows.Count;
        int Total = 0;
        for (i = 0; i <= dt.Rows.Count - 1; i++)
        {
            Total = Total + Util.String2Int(dt.Rows[i]["Excess_Articles"].ToString());
        }
        TotalExcessArticles = Total;
    }

    private void Bind_Grid()
    {
        string Condition = "";

        dg_ExcessDetails.DataSource = objCommon.Get_View_Table(SessionExcessDetailsDT, Condition);
        dg_ExcessDetails.DataBind();

        if (IsPostBack)
        {
            //update_Session();
            //updateparentdataset();
        }

        CalculateTotal(SessionExcessDetailsDT);
    }

    protected void dg_ExcessDetails_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_ExcessDetails.EditItemIndex = -1;
        dg_ExcessDetails.ShowFooter = true;
        Bind_Grid();
    }

    protected void dg_ExcessDetails_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_ExcessDetails.EditItemIndex = e.Item.ItemIndex;
        dg_ExcessDetails.ShowFooter = false;
        Bind_Grid();
    }

    protected void dg_ExcessDetails_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            try
            {
                objDT = SessionExcessDetailsDT;

                Insert_Update_Dataset(source, e);
                if (isValid == true)
                {
                    Bind_Grid();
                    dg_ExcessDetails.EditItemIndex = -1;
                    dg_ExcessDetails.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                errorMessage = "Error";
                ScriptManager.SetFocus(ddl_PackingType);
            }
        }
    }


    protected void dg_ExcessDetails_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            ddl_PackingType = (DropDownList)(e.Item.FindControl("ddl_PackingType"));
            ddl_Commodity = (DropDownList)(e.Item.FindControl("ddl_Commodity"));
            txt_GCNo = (TextBox)(e.Item.FindControl("txt_GCNo"));
            txt_ExcessArticles = (TextBox)(e.Item.FindControl("txt_ExcessArticles"));
            txt_Marking = (TextBox)(e.Item.FindControl("txt_Marking"));
            ddl_Item = (DropDownList)(e.Item.FindControl("ddl_Item"));
            txt_Remarks = (TextBox)(e.Item.FindControl("txt_Remarks"));

            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                //ddl_PackingType = (DropDownList)(e.Item.FindControl("ddl_PackingType"));
                //ddl_Item = (DropDownList)(e.Item.FindControl("ddl_Item"));
                
                ddl_PackingType.DataTextField = "Packing_Type";
                ddl_PackingType.DataValueField = "Packing_ID";
                ddl_PackingType.DataSource = SessionDDLPackingType;
                ddl_PackingType.DataBind();

                ddl_Item.DataTextField = "Item_Name";
                ddl_Item.DataValueField = "Item_Id";
                ddl_Item.DataSource = SessionDDLItem;
                ddl_Item.DataBind();

                ddl_Commodity.DataTextField = "Commodity_Name";
                ddl_Commodity.DataValueField = "Commodity_ID";
                ddl_Commodity.DataSource = SessionDDLCommodity;
                ddl_Commodity.DataBind();
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                objDT = SessionExcessDetailsDT;

                DR = objDT.Rows[e.Item.ItemIndex];

                txt_GCNo.Text = DR["GC_No"].ToString();
                txt_ExcessArticles.Text = DR["Excess_Articles"].ToString();
                txt_Marking.Text = DR["Marking_On_Package"].ToString();
                ddl_PackingType.SelectedValue = DR["Packing_Type_ID"].ToString();
                ddl_Item.SelectedValue = DR["Item_ID"].ToString();
                ddl_Commodity.SelectedValue = DR["Commodity_ID"].ToString();
                txt_Remarks.Text = DR["Remarks"].ToString();

            }
        }
    }

    protected void dg_ExcessDetails_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            objDT = SessionExcessDetailsDT;

            objDT.Rows.RemoveAt(e.Item.ItemIndex);
            objDT.AcceptChanges();
            SessionExcessDetailsDT = objDT;
            dg_ExcessDetails.EditItemIndex = -1;
            dg_ExcessDetails.ShowFooter = true;
            Bind_Grid();

            string popupScript = "<script language='javascript'>updateparent();</script>";
            System.Web.UI.ScriptManager.RegisterStartupScript(lbl_Errors, typeof(String), "PopupScript", popupScript.ToString(), false);
        }
    }

    protected void dg_ExcessDetails_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            objDT = SessionExcessDetailsDT;

            Insert_Update_Dataset(source, e);

            if (isValid == true)
            {
                dg_ExcessDetails.EditItemIndex = -1;
                dg_ExcessDetails.ShowFooter = true;
                Bind_Grid();
            }
        }
        catch (ConstraintException)
        {
            errorMessage = "Error";
            ScriptManager.SetFocus(ddl_PackingType);
        }
    }
    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        ddl_PackingType = (DropDownList)(e.Item.FindControl("ddl_PackingType"));
        ddl_Commodity = (DropDownList)(e.Item.FindControl("ddl_Commodity"));
        txt_GCNo = (TextBox)(e.Item.FindControl("txt_GCNo"));
        txt_ExcessArticles = (TextBox)(e.Item.FindControl("txt_ExcessArticles"));
        txt_Marking = (TextBox)(e.Item.FindControl("txt_Marking"));
        ddl_Item = (DropDownList)(e.Item.FindControl("ddl_Item"));
        txt_Remarks = (TextBox)(e.Item.FindControl("txt_Remarks"));
        
        objDT = SessionExcessDetailsDT;

        if (e.CommandName == "Add")
        {
            DR = objDT.NewRow();
        }
        else if (e.CommandName == "Update")
        {
            DR = objDT.Rows[e.Item.ItemIndex];
        }

        if (Allow_To_Add_Update() == true)
        {
            DR["GC_No"] = txt_GCNo.Text;
            DR["Excess_Articles"] = txt_ExcessArticles.Text;
            DR["Marking_on_Package"] = txt_Marking.Text;

            DR["Packing_Type_Id"] = ddl_PackingType.SelectedValue;
            DR["Packing_Type"] = ddl_PackingType.SelectedItem;

            DR["Item_Id"] = ddl_Item.SelectedValue;
            DR["Item_Name"] = ddl_Item.SelectedItem;

            DR["Commodity_ID"] = ddl_Commodity.SelectedValue;
            DR["Commodity_Name"] = ddl_Commodity.SelectedItem;
            
            DR["Remarks"] = txt_Remarks.Text;

            if (e.CommandName == "Add") 
            { 
                objDT.Rows.Add(DR); 
            }
            SessionExcessDetailsDT = objDT;

            string popupScript = "<script language='javascript'>updateparent();</script>";
            System.Web.UI.ScriptManager.RegisterStartupScript(lbl_Errors, typeof(String), "PopupScript", popupScript.ToString(), false);
        }
    }

    private bool Allow_To_Add_Update()
    {
        lbl_Errors.Text = "";

        if (Util.String2Int(ddl_PackingType.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Packing";
            ScriptManager.SetFocus(ddl_PackingType);
        }
        else if (Util.String2Int(ddl_Item.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Item";
            ScriptManager.SetFocus(ddl_Item);
        }
        else if (Util.String2Int(ddl_Commodity.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Commodity";
            ScriptManager.SetFocus(ddl_Commodity);
        }
        else
        {
            isValid = true;
        }

        return isValid;
    }

    protected void btn_Exit_Click(object sender, EventArgs e)
    {

        Response.Write("<script language='javascript'>{self.close()}</script>");

    }
}
