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

using Raj.EF.MasterPresenter;
using Raj.EF.MasterView;
using Raj.EF;



/// <summary>
/// Author        : Shiv kumar mishra
/// Created On    : 03/05/2008
/// Description   : This Page is For Master Vehicle Chasis Tyre
/// </summary>
/// 

public partial class Master_Vehicle_WucVehicleChasisTyres : System.Web.UI.UserControl,IVehicleChasisTyresView 
{

    #region ClassVariables
    VehicleChasisTyresPresenter objVehicleChasisTyresPresenter;
    private ScriptManager scm_ChasisTyres;
    DropDownList  ddl_Dual;
    Image imgLO, imgLI, imgRI, imgRO, imgStick;
    DataTable objDT;
    DataRow dr = null;
    bool isValid = false;
    #endregion

    #region iview implementaion
    public int FrontWheelSizeID
    {
        set { ddl_FrontWheelSize.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_FrontWheelSize.SelectedValue); }
    }
    public int FrontTyreSizeID
    {
        set { ddl_FrontTyresize.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_FrontTyresize.SelectedValue); }
    }
    public int RearWheelSizeID
    {
        set { ddl_RearWheelSize.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_RearWheelSize.SelectedValue); }
    }
    public int RearTyreSizeID
    {
        set { ddl_RearTyresize.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_RearTyresize.SelectedValue); }
    }
    public int FrontPSI
    {
        set { txt_FrontPSI.Text = Util.Int2String(value); }
        get { return Util.String2Int(txt_FrontPSI.Text); }
    }
    public int RearPSI
    {
        set { txt_RearPSI .Text  = Util.Int2String(value); }
        get { return Util.String2Int(txt_RearPSI.Text); }
    }

    public int NoOfStephaney
    {
        set { txt_no_of_stephaney.Text = Util.Int2String(value); }
        get { return Util.String2Int(txt_no_of_stephaney.Text); }
    }

    public int OldNoOfStephaney
    {
        set { hdn_old_no_of_stephaney.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_old_no_of_stephaney.Value); }
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }
    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]); 
            //return 128;
        }
    }

    public DataTable BindFrontWheelSize
    {
        set
        {
            ddl_FrontWheelSize.DataSource = value;
            ddl_FrontWheelSize.DataTextField = "Wheel_Size";
            ddl_FrontWheelSize.DataValueField = "Wheel_Size_ID";
            ddl_FrontWheelSize.DataBind();
            ddl_FrontWheelSize.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable BindFrontTyreSize
    {
        set
        {
            ddl_FrontTyresize.DataSource = value;
            ddl_FrontTyresize.DataTextField = "Tyre_Size";
            ddl_FrontTyresize.DataValueField = "Tyre_Size_ID";
            ddl_FrontTyresize.DataBind();
            ddl_FrontTyresize.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable BindRearWheelSize
    {
        set
        {
            ddl_RearWheelSize.DataSource = value;
            ddl_RearWheelSize.DataTextField = "Wheel_Size";
            ddl_RearWheelSize.DataValueField = "Wheel_Size_ID";
            ddl_RearWheelSize.DataBind();
            ddl_RearWheelSize.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable BindRearTyreSize
    {
        set
        {
            ddl_RearTyresize.DataSource = value;
            ddl_RearTyresize.DataTextField = "Tyre_Size";
            ddl_RearTyresize.DataValueField = "Tyre_Size_ID";
            ddl_RearTyresize.DataBind();
            ddl_RearTyresize.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable BindDualType
    {
        set
        {
            ddl_Dual.DataSource = value;
            ddl_Dual.DataTextField = "Axle_Configuration";
            ddl_Dual.DataValueField = "Axle_Configuration_ID";
            ddl_Dual.DataBind();
            ddl_Dual.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable SessionDualType
    {
        get { return StateManager.GetState<DataTable>("SessionDualType"); }
        set { StateManager.SaveState("SessionDualType", value); }
    }

    public DataTable BindChasisTyresGrid
    {
        set
        {
            SessionChasisTyresGrid = value;
            Set_Sr_No();
            dg_ChasisTyres.DataSource = value;
            dg_ChasisTyres.DataBind();
        }
    }

    public DataTable SessionChasisTyresGrid
    {
        get { return StateManager.GetState<DataTable>("ChasisTyresGrid"); }
        set { StateManager.SaveState("ChasisTyresGrid", value); }
    }
    #endregion

    #region OtherProperties
    public ScriptManager SetScriptManager
    {
        set { scm_ChasisTyres = value; }
    }

    public String VehicleTyreConfigurationDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionChasisTyresGrid.Copy());
            _objDs.Tables[0].TableName = "Vehicle_Configuration_Details";
            return _objDs.GetXml().ToLower();
        }
    }
    #endregion


    #region validation
    public bool validateUI()
    {
        bool _isValid = false;

        if (NoOfStephaney > 5)
        {
            errorMessage = "No. Of Stephaney(s) Must Less Than Or Equal To 5";
            scm_ChasisTyres.SetFocus(txt_no_of_stephaney);
        }
        else if (NoOfStephaney < OldNoOfStephaney)
        {
            errorMessage = "Please Enter No. of Stephaney(s) greater than or equal to " + OldNoOfStephaney.ToString();
            scm_ChasisTyres.SetFocus(txt_no_of_stephaney);
        }
        else
            _isValid = true;

        return _isValid;
    }
    #endregion


    #region Other Methods
    private void Set_Sr_No()
    {
        int Sr_No;
        objDT = SessionChasisTyresGrid;
        //DataRow DR = null;
        for (Sr_No = 0; Sr_No <= objDT.Rows.Count - 1; Sr_No++)
        {
            dr = objDT.Rows[Sr_No];
            dr["Sr_No"] = Sr_No + 1;
        }
        SessionChasisTyresGrid = objDT;
    }
    #endregion

    #region ControlsEvents
    protected void Page_Load(object sender, EventArgs e)
    {
      
        objVehicleChasisTyresPresenter = new VehicleChasisTyresPresenter(this, IsPostBack);
    }

    protected void dg_ChasisTyres_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        ddl_Dual = (DropDownList)(e.Item.FindControl("ddl_dual"));
        imgLO = (Image)(e.Item.FindControl("imgLO"));
        imgLI = (Image)(e.Item.FindControl("imgLI"));
        imgRI = (Image)(e.Item.FindControl("imgRI"));
        imgRO = (Image)(e.Item.FindControl("imgRO"));
        imgStick = (Image)(e.Item.FindControl("imgStick"));
                
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                imgLO.Visible = false;
                imgLI.Visible = false;
                imgRI.Visible = false;
                imgRO.Visible = false;
                imgStick.Visible = false;
            }
            else if (e.Item.ItemType == ListItemType.EditItem)
            {
                imgLO.Visible = false;
                imgLI.Visible = false;
                imgRI.Visible = false;
                imgRO.Visible = false;
                imgStick.Visible = false;

                objDT = SessionChasisTyresGrid;

                LinkButton lbtn_Delete = (LinkButton)(e.Item.FindControl("lbtn_Delete"));
                lbtn_Delete.Enabled = false;

                ddl_Dual.SelectedValue = objDT.Rows[e.Item.ItemIndex]["Axle_Configuration_ID"].ToString();

              if (Convert.ToInt32(objDT.Rows[e.Item.ItemIndex]["Axle_Configuration_ID"]) == 0)
                {
                    imgLO.Visible = false;
                    imgLI.Visible = false;
                    imgRI.Visible = false;
                    imgRO.Visible = false;
                    imgStick.Visible = false;
                }
                else if (Convert.ToInt32(objDT.Rows[e.Item.ItemIndex]["Axle_Configuration_ID"]) == 1)
                {
                    imgLO.Visible = true;
                    imgLI.Visible = false;
                    imgRI.Visible = false;
                    imgRO.Visible = true;
                    imgStick.Visible = true;
                    imgLO.ImageUrl = "~\\Images\\Tyre.GIF";
                    imgRO.ImageUrl = "~\\Images\\Tyre.GIF";
                    imgStick.ImageUrl = "~\\Images\\TyreStick.GIF";
                }
                else if (Convert.ToInt32(objDT.Rows[e.Item.ItemIndex]["Axle_Configuration_ID"]) == 2)
                {
                    imgLO.Visible = true;
                    imgLI.Visible = true;
                    imgRI.Visible = true;
                    imgRO.Visible = true;
                    imgStick.Visible = true;
                    imgLO.ImageUrl = "~\\Images\\Tyre.GIF";
                    imgLI.ImageUrl = "~\\Images\\Tyre.GIF";
                    imgRI.ImageUrl = "~\\Images\\Tyre.GIF";
                    imgRO.ImageUrl = "~\\Images\\Tyre.GIF";
                    imgStick.ImageUrl = "~\\Images\\TyreStick.GIF";
                }
            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                e.Item.Cells[7].Enabled = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Can_Update_Delete"));
                e.Item.Cells[8].Enabled = Convert.ToBoolean( DataBinder.Eval(e.Item.DataItem, "Can_Update_Delete"));
            }

            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                BindDualType = SessionDualType;
            }

            if (e.Item.ItemIndex != -1)
            {
                objDT = SessionChasisTyresGrid;

                imgLO.Visible = false;
                imgLI.Visible = false;
                imgRI.Visible = false;
                imgRO.Visible = false;
                imgStick.Visible = false;

                if (Convert.ToInt32(objDT.Rows[e.Item.ItemIndex]["Axle_Configuration_ID"]) == 0)
                {
                    imgLO.Visible = false;
                    imgLI.Visible = false;
                    imgRI.Visible = false;
                    imgRO.Visible = false;
                    imgStick.Visible = false;
                }
                else if (Convert.ToInt32(objDT.Rows[e.Item.ItemIndex]["Axle_Configuration_ID"]) == 1)
                {
                    imgLO.Visible = true;
                    imgLI.Visible = false;
                    imgRI.Visible = false;
                    imgRO.Visible = true;
                    imgStick.Visible = true;
                    imgLO.ImageUrl = "~\\Images\\Tyre.GIF";
                    imgRO.ImageUrl = "~\\Images\\Tyre.GIF";
                    imgStick.ImageUrl = "~\\Images\\TyreStick.GIF";
                }
                else if (Convert.ToInt32(objDT.Rows[e.Item.ItemIndex]["Axle_Configuration_ID"]) == 2)
                {
                    imgLO.Visible = true;
                    imgLI.Visible = true;
                    imgRI.Visible = true;
                    imgRO.Visible = true;
                    imgStick.Visible = true;
                    imgLO.ImageUrl = "~\\Images\\Tyre.GIF";
                    imgLI.ImageUrl = "~\\Images\\Tyre.GIF";
                    imgRI.ImageUrl = "~\\Images\\Tyre.GIF";
                    imgRO.ImageUrl = "~\\Images\\Tyre.GIF";
                    imgStick.ImageUrl = "~\\Images\\TyreStick.GIF";
                }
            }
        }
    }
    protected void dg_ChasisTyres_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
                objDT = SessionChasisTyresGrid;
                Insert_Update_Dataset(source, e);
                if (isValid == true)
                {
                    BindChasisTyresGrid = SessionChasisTyresGrid;
                    dg_ChasisTyres.EditItemIndex = -1;
                    dg_ChasisTyres.ShowFooter = true;
                }
        }
    }

    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        ddl_Dual = (DropDownList)(e.Item.FindControl("ddl_Dual"));

        objDT = SessionChasisTyresGrid;

        if (e.CommandName == "Add")
        {
            dr = objDT.NewRow();
        }
        else if (e.CommandName == "Update")
        {
            dr = objDT.Rows[e.Item.ItemIndex];
        }

        if (Allow_To_Add_Update() == true)
        {
            dr["Tyre_Configuration_Id"] = 0;
            dr["Axle_Configuration_ID"] = ddl_Dual.SelectedValue;
            dr["Axle_Configuration"] = ddl_Dual.SelectedItem.Text;
            dr["Can_Update_Delete"] = 1;

            if (e.CommandName == "Add") { objDT.Rows.Add(dr); }
            SessionChasisTyresGrid = objDT;
        }
    }

    private bool Allow_To_Add_Update()
    {
        lbl_Errors.Text = "";
        if (Util.String2Int(ddl_Dual.SelectedValue) == 0)
        {
            lbl_Errors.Text = "Please Select Dual Type";
            scm_ChasisTyres.SetFocus(ddl_Dual);
        }
        else
        {
            isValid = true;
        }
        return isValid;
    }

    protected void dg_ChasisTyres_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            objDT = SessionChasisTyresGrid;
            objDT.Rows.RemoveAt(e.Item.ItemIndex);
            objDT.AcceptChanges();
            SessionChasisTyresGrid = objDT;
            dg_ChasisTyres.EditItemIndex = -1;
            dg_ChasisTyres.ShowFooter = true;
            BindChasisTyresGrid = SessionChasisTyresGrid;
        }
    }

    protected void dg_ChasisTyres_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
            Insert_Update_Dataset(source, e);
            if (isValid == true)
            {
                dg_ChasisTyres.EditItemIndex = -1;
                dg_ChasisTyres.ShowFooter = true;
                BindChasisTyresGrid = SessionChasisTyresGrid;
            }
    }

    protected void dg_ChasisTyres_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_ChasisTyres.EditItemIndex = -1;
        BindChasisTyresGrid = SessionChasisTyresGrid;
        dg_ChasisTyres.ShowFooter = true;
    }

    protected void dg_ChasisTyres_EditCommand(object source, DataGridCommandEventArgs e)
    {
            dg_ChasisTyres.EditItemIndex = e.Item.ItemIndex;
            dg_ChasisTyres.ShowFooter = false;
            BindChasisTyresGrid = SessionChasisTyresGrid;
    }

    protected void ddl_dual_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Dual = (DropDownList)sender;
        DataGridItem _dgItem = (DataGridItem)ddl_Dual.Parent.Parent;
              objDT = SessionChasisTyresGrid;

                imgLO = (Image)(_dgItem.FindControl("imgLO"));
                imgLI = (Image)(_dgItem.FindControl("imgLI"));
                imgRI = (Image)(_dgItem.FindControl("imgRI"));
                imgRO = (Image)(_dgItem.FindControl("imgRO"));
                imgStick = (Image)(_dgItem.FindControl("imgStick"));
                imgLO.Visible = false;
                imgLI.Visible = false;
                imgRI.Visible = false;
                imgRO.Visible = false;
                imgStick.Visible = false;

                if (Convert.ToInt32(ddl_Dual.SelectedValue) == 0)
                {
                    imgLO.Visible = false;
                    imgLI.Visible = false;
                    imgRI.Visible = false;
                    imgRO.Visible = false;
                    imgStick.Visible = false;
                }
                else if (Convert.ToInt32(ddl_Dual.SelectedValue) == 1)
                {
                    imgLO.Visible = true;
                    imgLI.Visible = false;
                    imgRI.Visible = false;
                    imgRO.Visible = true;
                    imgStick.Visible = true;
                    imgLO.ImageUrl = "~\\Images\\Tyre.GIF";
                    imgRO.ImageUrl = "~\\Images\\Tyre.GIF";
                    imgStick.ImageUrl = "~\\Images\\TyreStick.GIF";
                }
                else if (Convert.ToInt32(ddl_Dual.SelectedValue) == 2)
                {
                    imgLO.Visible = true;
                    imgLI.Visible = true;
                    imgRI.Visible = true;
                    imgRO.Visible = true;
                    imgStick.Visible = true;
                    imgLO.ImageUrl = "~\\Images\\Tyre.GIF";
                    imgLI.ImageUrl = "~\\Images\\Tyre.GIF";
                    imgRI.ImageUrl = "~\\Images\\Tyre.GIF";
                    imgRO.ImageUrl = "~\\Images\\Tyre.GIF";
                    imgStick.ImageUrl = "~\\Images\\TyreStick.GIF";
                }
            }
    #endregion
        }
