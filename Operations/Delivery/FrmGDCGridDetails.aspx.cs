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
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.Security;

public partial class Operations_Delivery_FrmGDCGridDetails : System.Web.UI.Page
{
    DataTable objGridSessionDT = new DataTable();
    int ds_Index,_menuItemId;
    string Crypt,Menuitem;
    DataRow dr;
    string Mode = "0";

  #region ControlsBind

    public void BindDeliveryMode()
    {
        ddl_DeliveryMode.DataValueField = "Delivery_Mode_ID";
        ddl_DeliveryMode.DataTextField = "Delivery_Mode";
        ddl_DeliveryMode.DataSource = StateManager.GetState<DataTable>("DDLDeliveryMode");
        ddl_DeliveryMode.DataBind();
        ddl_DeliveryMode.Items.Insert(0, new ListItem("Select One", "0"));
    }
  #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Mode = Request.QueryString["Mode"].ToString();

        Crypt = Request.QueryString["ds_index"];
        Menuitem = Request.QueryString["_menuItemid"];
        ds_Index = Util.DecryptToInt(Crypt);
        _menuItemId = Util.DecryptToInt(Menuitem);

        if (_menuItemId == 80) //Godown Delivery
        {       
            objGridSessionDT = StateManager.GetState<DataTable>("BindGDCGrid");
        }
        else if (_menuItemId == 82) //Door Delivery
        {
            objGridSessionDT = StateManager.GetState<DataTable>("BindDDSGrid");
        }

        if (!IsPostBack)
        {
            WucDeliveryOtherDetails1.SetDeliveryAgainstCaption = "Against:";

            if (_menuItemId == 80 || _menuItemId == 82) //Godown Delivery,Door Delivery
            {
                BindDeliveryMode();
            }
            //else
            //{
            //    Disable_control();
            //}
            if (Util.String2Int(objGridSessionDT.Rows[ds_Index]["is_updated"].ToString()) == 0)
            {
                get_delivery_Det();
            }
            else
            {
                get_delivery_Det_From_Dataset();
            }

            if (Mode == "4")
            {
                btn_Save.Visible = false;
                ddl_DeliveryMode.Enabled = false;
                txt_Description.Enabled = false;
                txt_DeliveryTakenBy.Enabled = false;
                txt_ContactNo.Enabled = false;
                WucDeliveryOtherDetails1.SetEnabled = false;
            }
        }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (AlowToSave() == true)
        {
            Add_In_Dataset();
            Response.Write("<script language='javascript'>{self.close()}</script>");
        }
    }
    private bool AlowToSave()
    {
        bool ats = false;

        if (_menuItemId == 80 && Util.String2Bool(objGridSessionDT.Rows[ds_Index]["IsDelDetailsReq"].ToString()) == true && Util.String2Int(ddl_DeliveryMode.SelectedValue) == 0)
        {
            lbl_errors.Text = "Please Select Delivery Mode";
            ddl_DeliveryMode.Focus();
        }
        else if (Util.String2Bool(objGridSessionDT.Rows[ds_Index]["IsDelDetailsReq"].ToString()) == true && WucDeliveryOtherDetails1.DeliveryToID == 0)
        {
            lbl_errors.Text = "Please Select Delivery To";
        }
        else if (txt_DeliveryTakenBy.Text.Trim() == string.Empty)
        {
            lbl_errors.Text = "Please Enter Delivery Taken By";
        }      
        else
        {
            ats = true;
        }
        return ats;
    }
    //private void Disable_control()
    //{
    //    tr_Del_Mod.Visible=false;
    //    tr_Del_Des.Visible = false;
    //}
    private void Add_In_Dataset()
    {
        if (objGridSessionDT.Rows.Count > 0)
        {
            dr = objGridSessionDT.Rows[ds_Index];

            dr["is_updated"] = 1;
            if (_menuItemId == 80 || _menuItemId ==  82) //Godown Delivery
            {
                dr["Delivery_Mode_ID"] = Util.String2Int(ddl_DeliveryMode.SelectedValue);
                dr["Delivery_Mode_Description"] = txt_Description.Text;
                dr["Delivery_Taken_By"] = txt_DeliveryTakenBy.Text;
                dr["Contact_No"] = txt_ContactNo.Text;
            }
            dr["Cne_Copy_Status_ID"] = WucDeliveryOtherDetails1.ConsigneeCopyID;
            dr["Delivery_Against_ID"] = WucDeliveryOtherDetails1.DeliveryAgainstID;
            dr["Delivery_To_ID"] = WucDeliveryOtherDetails1.DeliveryToID;

            if (_menuItemId == 80) //Godown Delivery
            {
                StateManager.SaveState("BindGDCGrid", objGridSessionDT);
            }
            else if (_menuItemId == 82) //Door Delivery
            {
                StateManager.SaveState("BindDDSGrid", objGridSessionDT);
            }
            
        }
    }
    private void get_delivery_Det()
     {
         dr = objGridSessionDT.Rows[ds_Index];
         if (_menuItemId == 80 || _menuItemId == 82) //Godown Delivery
         {
             ddl_DeliveryMode.SelectedValue = dr["Delivery_Mode_ID"].ToString();
             txt_Description.Text = dr["Delivery_Mode_Description"].ToString();
             txt_DeliveryTakenBy.Text = dr["Delivery_Taken_By"].ToString();
             txt_ContactNo.Text = dr["Contact_No"].ToString();
         }
         WucDeliveryOtherDetails1.ConsigneeCopyID = Util.String2Int(dr["Cne_Copy_Status_ID"].ToString());
         WucDeliveryOtherDetails1.DeliveryAgainstID = Util.String2Int(dr["Delivery_Against_ID"].ToString());
         WucDeliveryOtherDetails1.DeliveryToID = Util.String2Int(dr["Delivery_To_ID"].ToString());
    }

    private void get_delivery_Det_From_Dataset()
     {

         dr = objGridSessionDT.Rows[ds_Index];
         if (_menuItemId == 80 || _menuItemId == 82) //Godown Delivery
         {
             ddl_DeliveryMode.SelectedValue = dr["Delivery_Mode_ID"].ToString();
             txt_Description.Text = dr["Delivery_Mode_Description"].ToString();
             txt_DeliveryTakenBy.Text = dr["Delivery_Taken_By"].ToString();
             txt_ContactNo.Text = dr["Contact_No"].ToString();
         }
         WucDeliveryOtherDetails1.ConsigneeCopyID = Util.String2Int(dr["Cne_Copy_Status_ID"].ToString());
         WucDeliveryOtherDetails1.DeliveryAgainstID = Util.String2Int(dr["Delivery_Against_ID"].ToString());
         WucDeliveryOtherDetails1.DeliveryToID = Util.String2Int(dr["Delivery_To_ID"].ToString());
        }
    protected void btn_Exit_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
}
