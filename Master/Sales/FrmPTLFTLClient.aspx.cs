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
using System.Data.SqlClient;
using ClassLibraryMVP.General;
using Raj.EC;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.Security;

public partial class Master_Sales_FrmPTLFTLClient : ClassLibraryMVP.UI.Page
{
    Raj.EC.Common ObjCommon = new Raj.EC.Common();

    private DAL objDAL = new DAL();
    private DataSet objDS;
    DropDownList ddl_VehicleManufacturer, ddl_VehicleModel;
    TextBox txt_LoadWeight;
    DataRow dr;
    bool Allow_To_Save;
    DataTable objDT;
    LinkButton lbtn_Add_Commodity;

    public int KeyId
    {
        set { hdnKeyID.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdnKeyID.Value); }
    }


    public DataTable Session_ManufactureDdl
    {
        get { return StateManager.GetState<DataTable>("ManufactureDdl"); }
        set { StateManager.SaveState("ManufactureDdl", value); }
    }

    public DataTable Session_VehicleModelDdl
    {
        get { return StateManager.GetState<DataTable>("VehicleModelDdl"); }
        set { StateManager.SaveState("VehicleModelDdl", value); }
    }

   

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        if (!IsPostBack)
        {
            KeyId = Util.DecryptToInt(Request.QueryString["Id"]);

            fillVehicleManufacturer();

            if (KeyId > 0)
            {
                ReadValues();
            }
            fillGridDetails();
        }
        lst_City.Style.Add("visibility", "hidden");
    }

    private void fillVehicleManufacturer()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@KeyId", SqlDbType.Int, 0, 0) };
        objDAL.RunProc("[rstil42].[EF_Mst_Vehicle_Model_FillManufacturer]",objSqlParam, ref objDS);
        Session_ManufactureDdl = objDS.Tables[0];


        SqlParameter[] objSqlParam1 ={ objDAL.MakeInParams("@Manufacturer_ID", SqlDbType.Int, 0, objDS.Tables[0].Rows[0][0].ToString()) };
        objDAL.RunProc("[rstil42].[EF_Mst_Inspection_Template_FillVehicleModelOnManufacturerChanged]", objSqlParam1, ref objDS);

        Session_VehicleModelDdl = objDS.Tables[0];
        
    }


    protected void ddl_VehicleManufacturer_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_VehicleManufacturer = (DropDownList)sender;
        DataGridItem _item = (DataGridItem)ddl_VehicleManufacturer.Parent.Parent;

        ddl_VehicleManufacturer = (DropDownList)_item.FindControl("ddl_VehicleManufacturer");

        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Manufacturer_ID", SqlDbType.Int, 0, Convert.ToInt32(ddl_VehicleManufacturer.SelectedValue)) };
        objDAL.RunProc("[rstil42].[EF_Mst_Inspection_Template_FillVehicleModelOnManufacturerChanged]", objSqlParam, ref objDS);

        Session_VehicleModelDdl = objDS.Tables[0];

        ddl_VehicleModel = (DropDownList)sender;
        DataGridItem _item1 = (DataGridItem)ddl_VehicleModel.Parent.Parent;

        ddl_VehicleModel = (DropDownList)_item1.FindControl("ddl_VehicleModel");

        BindVehicleModel = Session_VehicleModelDdl;

    }

    private void fillGridDetails()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Client_ID", SqlDbType.Int, 0, KeyId) };

        objDAL.RunProc("EC_Mst_PTLFTL_Client_Details", objSqlParam, ref objDS);

        Session_Grid = objDS.Tables[0];

        Bind_dg_Commodity();
    }


    private void Set_Common_DDL(DropDownList DDl, string TextField, string ValueField, DataTable DT, bool Is_ZeroInex)
    {
        DDl.DataSource = DT;
        DDl.DataTextField = TextField;
        DDl.DataValueField = ValueField;
        DDl.DataBind();
        if (Is_ZeroInex)
            DDl.Items.Insert(0, new ListItem("Select One", "0"));
    }

    public DataTable BindManufacturer
    {
        set { Set_Common_DDL(ddl_VehicleManufacturer, "Manufacturer", "Manufacturer_ID", value, true); }
    }

    public DataTable BindVehicleModel
    {
        set
        {

            ddl_VehicleModel.DataTextField = "Vehicle_Model";
            ddl_VehicleModel.DataValueField = "Vehicle_Model_ID";
            ddl_VehicleModel.DataSource = value;
            ddl_VehicleModel.DataBind();
            ddl_VehicleModel.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable Session_Grid
    {
        get { return StateManager.GetState<DataTable>("Grid"); }
        set { StateManager.SaveState("Grid", value); }
    } 

    private void ReadValues()
    {
        DAL objDAL = new DAL();
        DataSet ds = new DataSet();
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Client_ID", SqlDbType.Int, 0, KeyId) };
        objDAL.RunProc("dbo.EC_Mst_PTLFTL_Client_ReadValues", objSqlParam, ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = ds.Tables[0].Rows[0];

            txt_ClientName.Text = objDR["Client_Name"].ToString();

            txt_ContactPerson1.Text = objDR["Contact_Person_1"].ToString();
            txt_Contact1Mobile1.Text = objDR["Contact_1_Mobile_1"].ToString();
            txt_Contact1Mobile2.Text = objDR["Contact_1_Mobile_2"].ToString();

            txt_ContactPerson2.Text = objDR["Contact_Person_2"].ToString();
            txt_Contact2Mobile1.Text = objDR["Contact_2_Mobile_1"].ToString();
            txt_Contact2Mobile2.Text = objDR["Contact_2_Mobile_2"].ToString();

            txt_Address1.Text = objDR["Address_Line_1"].ToString();
            txt_Address2.Text = objDR["Address_Line_2"].ToString();

            txt_City.Text = objDR["City_Name"].ToString();
            hdn_City.Value = objDR["City_Id"].ToString();
        }
    }

    private void Bind_dg_Commodity()
    {
        dg_Commodity.DataSource = Session_Grid;
        dg_Commodity.DataBind();

    } 

    private bool ValidateUI()
    {
        bool ATS = false;

        if (txt_ClientName.Text.Trim() == string.Empty)
        {
            lblErrors.Text = "Please Enter Client Name";
            txt_ClientName.Focus();
        }
        else if (txt_ContactPerson1.Text.Trim() == "")
        {
            lblErrors.Text = "Please Enter Contact Person 1";
            txt_ContactPerson1.Focus();
        }
        else if (txt_Contact1Mobile1.Text.Trim() == "")
        {
            lblErrors.Text = "Please Enter Contact 1 Mobile 1";
            txt_Contact1Mobile1.Focus();
        }
        else if (Util.String2Decimal(hdn_City.Value) <= 0)
        {
            lblErrors.Text = "Please Select City";
            txt_City.Focus();
        }
        else
        {
            ATS = true;
        }

        return ATS;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateUI())
        {
            Save();
        }
    }

    public Message Save()
    {

        DataTable DT1 = Session_Grid.Copy();
        DT1.TableName = "Grid_Details";
        DataSet ds = new DataSet();
        ds.Tables.Add(DT1);

        string DetailsXML = ds.GetXml().ToLower();


        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
                objDAL.MakeOutParams("@Client_ID", SqlDbType.Int,0), 
                objDAL.MakeInParams("@KeyID",SqlDbType.Int,0,KeyId),
                objDAL.MakeInParams("@ClientName",SqlDbType.VarChar,100,txt_ClientName.Text.Trim()),
                objDAL.MakeInParams("@ContactPerson1",SqlDbType.VarChar,100,txt_ContactPerson1.Text.Trim()),
                objDAL.MakeInParams("@ContactPerson1Mobile1",SqlDbType.VarChar,15,txt_Contact1Mobile1.Text.Trim()),
                objDAL.MakeInParams("@ContactPerson1Mobile2",SqlDbType.VarChar,15,txt_Contact1Mobile2.Text.Trim()),
                objDAL.MakeInParams("@ContactPerson2",SqlDbType.VarChar,100,txt_ContactPerson2.Text.Trim()),
                objDAL.MakeInParams("@ContactPerson2Mobile1",SqlDbType.VarChar,15,txt_Contact2Mobile1.Text.Trim()),
                objDAL.MakeInParams("@ContactPerson2Mobile2",SqlDbType.VarChar,15,txt_Contact2Mobile2.Text.Trim()),
                objDAL.MakeInParams("@AddressLine1",SqlDbType.VarChar  ,100,txt_Address1.Text),
                objDAL.MakeInParams("@AddressLine2",SqlDbType.VarChar  ,100,txt_Address2.Text),
                objDAL.MakeInParams("@CityId",SqlDbType.Int,0,Util.String2Int(hdn_City.Value)),
                objDAL.MakeInParams("@DetailsXML",SqlDbType.Xml,0,DetailsXML),
                objDAL.MakeInParams("@UserId",SqlDbType.Int,0,UserManager.getUserParam().UserId)
        };

        objDAL.RunProc("dbo.EC_Mst_PTL_FTL_Client_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);

        if (objMessage.messageID == 0)
        {
            hdnKeyID.Value = Convert.ToString(objSqlParam[2].Value);
            string _Msg;
            _Msg = "Saved SuccessFully";
            lblErrors.Text = _Msg;
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
        }
        else
        {
            lblErrors.Text = objMessage.message;
        }
        return objMessage;
    }


    protected void dg_Commodity_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
            {
                ddl_VehicleManufacturer = (DropDownList)(e.Item.FindControl("ddl_VehicleManufacturer"));
                ddl_VehicleModel = (DropDownList)(e.Item.FindControl("ddl_VehicleModel"));
                txt_LoadWeight = (TextBox)(e.Item.FindControl("txt_LoadWeight"));

                lbtn_Add_Commodity = (LinkButton)(e.Item.FindControl("lbtn_Add_Commodity"));

                BindManufacturer  = Session_ManufactureDdl;
                BindVehicleModel = Session_VehicleModelDdl; 

            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                
                DataRow DR = null;
                DataTable dt = Session_Grid;//for grid topic
                DR = dt.Rows[e.Item.ItemIndex];
                BindManufacturer = Session_ManufactureDdl;
                ddl_VehicleManufacturer.SelectedValue = DR["Manufacturer_ID"].ToString();
                ddl_VehicleManufacturer_SelectedIndexChanged(ddl_VehicleManufacturer, e);

                BindVehicleModel = Session_VehicleModelDdl; 

                ddl_VehicleModel.SelectedValue = DR["Vehicle_Model_ID"].ToString();
                txt_LoadWeight.Text = DR["LoadWeight"].ToString();
            }
        }
    }
    protected void dg_Commodity_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Commodity.EditItemIndex = e.Item.ItemIndex;
        dg_Commodity.ShowFooter = false;
        Bind_dg_Commodity();
        lblErrors.Text = "";

    }
    protected void dg_Commodity_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Commodity.EditItemIndex = -1;
        dg_Commodity.ShowFooter = true;


        Bind_dg_Commodity();
        lblErrors.Text = "";

    }
    protected void dg_Commodity_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            dr = Session_Grid.Rows[e.Item.ItemIndex];
            dr.Delete();
            Session_Grid.AcceptChanges();
            dg_Commodity.EditItemIndex = -1;
            dg_Commodity.ShowFooter = true;
            Bind_dg_Commodity();
        }

    }
    protected void dg_Commodity_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            Insert_Update_Commodity_Dataset(source, e);
            if (Allow_To_Save == true)
            {
                dg_Commodity.EditItemIndex = -1;
                dg_Commodity.ShowFooter = true;

                Bind_dg_Commodity();
            }
        }
        catch (ConstraintException)
        {
            lblErrors.Text  = "Duplicate Vehicle Manufacturer,Vehicle Model";
        }
    }
    protected void dg_Commodity_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add" || e.CommandName == "Update")
        {
            try
            {
                objDT = Session_Grid;

                DataColumn[] _dtColumnPrimaryKey;
                _dtColumnPrimaryKey = new DataColumn[2];
                _dtColumnPrimaryKey[0] = objDT.Columns["Manufacturer_ID"];
                _dtColumnPrimaryKey[1] = objDT.Columns["Vehicle_Model_ID"];
                objDT.PrimaryKey = _dtColumnPrimaryKey;

                Insert_Update_Commodity_Dataset(source, e);
                if (Allow_To_Save == true)
                {
                    Bind_dg_Commodity();
                    dg_Commodity.EditItemIndex = -1;
                    dg_Commodity.ShowFooter = true;

                }
                //TotalRate = TotalRate;
            }
            catch (ConstraintException)
            {
                lblErrors.Text  = "Duplicate Vehicle Manufacturer,Vehicle Model";
            }
        }
    }


    public bool Allow_To_Add_Update_Commodity()
    {
        Allow_To_Save = false;
        lblErrors.Text  = "";
        

        decimal LoadWeight = txt_LoadWeight.Text.Trim() == string.Empty ? 0 : Util.String2Decimal(txt_LoadWeight.Text.Trim());

        if (Util.String2Int(ddl_VehicleManufacturer.SelectedValue) <= 0)
        {
            lblErrors.Text  = "Please Select Vehicle Manufacturer.";
            scm_Comm.SetFocus(ddl_VehicleManufacturer);
        }
        else if (Util.String2Int(ddl_VehicleModel.SelectedValue) <= 0)
        {
            lblErrors.Text = "Please Select Vehicle Model.";
            scm_Comm.SetFocus(ddl_VehicleModel);
        }
        else if (LoadWeight <= 0)
        {
            lblErrors.Text = "Please Enter Load Weight";
            scm_Comm.SetFocus(txt_LoadWeight);
        }
        else
        {
            Allow_To_Save = true;
        }

        return Allow_To_Save;
    }

    private void Insert_Update_Commodity_Dataset(object source, DataGridCommandEventArgs e)
    {
        ddl_VehicleManufacturer = (DropDownList)(e.Item.FindControl("ddl_VehicleManufacturer"));
        ddl_VehicleModel = (DropDownList)(e.Item.FindControl("ddl_VehicleModel"));
        txt_LoadWeight = (TextBox)(e.Item.FindControl("txt_LoadWeight"));

        if (Allow_To_Add_Update_Commodity())
        {
            if (e.CommandName == "Add")
            {
                dr = Session_Grid.NewRow();
            }
            else if (e.CommandName == "Update")
            {
                dr = Session_Grid.Rows[e.Item.ItemIndex];
            }

            dr["Manufacturer_ID"] = ddl_VehicleManufacturer.SelectedValue;
            dr["Manufacturer"] = Util.String2Int(ddl_VehicleManufacturer.SelectedValue) == 0 ? "" : ddl_VehicleManufacturer.SelectedItem.Text;
            dr["Vehicle_Model_ID"] = ddl_VehicleModel.SelectedValue;
            dr["Vehicle_Model"] = Util.String2Int(ddl_VehicleModel.SelectedValue) == 0 ? "" : ddl_VehicleModel.SelectedItem.Text;
            dr["LoadWeight"] = txt_LoadWeight.Text.Trim() == string.Empty ? "0" : txt_LoadWeight.Text.Trim();

            if (e.CommandName == "Add")
            {
                Session_Grid.Rows.Add(dr);
            }
        }
    }

    
}
