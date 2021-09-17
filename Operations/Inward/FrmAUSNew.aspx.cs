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
using System.Text;
using System.Net;
using System.IO;


public partial class Operations_Inward_FrmAusNew : ClassLibraryMVP.UI.Page
{
    Raj.EC.Common objComm = new Raj.EC.Common();
    int EmployeeID = 0;
    string Mode = "0";
    string _flag = "";
    private DataSet objDS;
    DataTable objDT = new DataTable();
    private DAL objDAL = new DAL();

    public int keyID
    {
        get{return Util.DecryptToInt(Request.QueryString["Id"]);}
    }

    public String TURNo
    {
        set { lbl_TURNoValue.Text = value; }
        get { return lbl_TURNoValue.Text; }
    }

    public int VehicleID
    {
        get { return WucVehicleSearch1.VehicleID; }
        set
        {
            WucVehicleSearch1.VehicleID = value;
            hdn_Vehicle_Id.Value = Util.Int2String(value);
        }
    }
    public DateTime AUS_Date
    {
        set { wuc_AUSDate.SelectedDate = value; }
        get { return wuc_AUSDate.SelectedDate; }
    }
    public String AUS_Time
    {
        set { wuc_TASTime.setTime(value); }
        get { return wuc_TASTime.getTime(); }
    }
    public int LHPO_Id
    {
        get { return Util.String2Int(ddl_LHPO.SelectedValue); }
    }

    public int Vehicle_Category_Id
    {
        set { hdn_Vehicle_Category_Id.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_Vehicle_Category_Id.Value.Trim() == string.Empty ? "0" : hdn_Vehicle_Category_Id.Value.Trim()); }
    }

    public string LHPODate
    {
        set { lbl_LHPO_Date.Text = value; }
    }
    public decimal BTHAmount
    {
        set { lbl_BTHAmountValue.Text = Util.Decimal2String(value); }
    }
    public string LHPOFromLocation
    {
        set { lbl_LHPOFromLocation.Text = value; }
    }
    public string LHPOToLocation
    {
        set { lbl_LHPOToLocation.Text = value; }
    }
    public int VendorID
    {
        get { return WucVehicleSearch1.VehicleVendorID; }
        set { WucVehicleSearch1.VehicleVendorID = value; }
    }
    public int TotalLRs
    {
        get { return Util.String2Int(hdn_Total_Loaded_LR.Value); }
        set
        {
            lbl_Total_Loaded_LR.Text = "     " + Util.Int2String(value);
            hdn_Total_Loaded_LR.Value = Util.Int2String(value);
        }
    }
    public int TotalLoadedArticles
    {
        get { return Util.String2Int(hdn_Total_Loaded_Articles.Value); }
        set
        {
            lbl_Total_Loaded_Articles.Text = "     " + Util.Int2String(value);
            hdn_Total_Loaded_Articles.Value = Util.Int2String(value);
        }
    }
    public decimal TotalLoadedWeight
    {
        get { return Util.String2Decimal(hdn_Total_Loaded_Weight.Value); }
        set
        {
            lbl_Total_Loaded_Weight.Text = "     " + Util.Decimal2String(value);
            hdn_Total_Loaded_Weight.Value = Util.Decimal2String(value);
        }
    }
    public int TotalShort
    {
        get { return Util.String2Int(hdn_Total_Short.Value); }
        set
        {
            lbl_Total_Short.Text = Util.Int2String(value);
            hdn_Total_Short.Value = Util.Int2String(value);
            if (value > 0)
                rbl_Short.SelectedValue = "1";
        }
    }
    public int TotalDamaged
    {
        get { return Util.String2Int(hdn_Total_Damaged.Value); }
        set
        {
            lbl_Total_Damaged.Text = Util.Int2String(value);
            hdn_Total_Damaged.Value = Util.Int2String(value);
            if (value > 0)
                rbl_Damage.SelectedValue = "1";
        }
    }
    public decimal TotalDamagedValue
    {
        get { return Util.String2Int(hdn_Total_Damaged_Value.Value); }
        set { hdn_Total_Damaged_Value.Value = Util.Decimal2String(value); }
    }
    public int TotalExcess
    {
        get { return Util.String2Int(hdn_Total_Excess.Value); }
        set
        {
            lbl_Total_Excess.Text = Util.Int2String(value);
            hdn_Total_Excess.Value = Util.Int2String(value);
            if (value > 0)
                rbl_Excess.SelectedValue = "1";
        }
    }
    public int TotalReceivedArticles
    {
        get { return Util.String2Int(hdn_Total_Received_Articles.Value); }
        set { hdn_Total_Received_Articles.Value = Util.Int2String(value); }
    }
    public decimal TotalReceivedWt
    {
        get { return Util.String2Decimal(hdn_Total_Received_Weight.Value); }
        set { hdn_Total_Received_Weight.Value = Util.Decimal2String(value); }
    }
    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "4")
        {
            WucVehicleSearch1.Can_Add_Vehicle = false;
            WucVehicleSearch1.Can_View_Vehicle = true;
        }
    }
    public void SetLHPO(string text, string value)
    {
        ddl_LHPO.DataTextField = "LHPO_No_For_Print";
        ddl_LHPO.DataValueField = "LHPO_ID";
        ddl_LHPO.Items.Insert(0, new ListItem(text, value));
    }

    public DataTable SessionExcessDetailsGrid
    {
        get { return StateManager.GetState<DataTable>("SessionExcessDetailsGrid"); }
        set { StateManager.SaveState("SessionExcessDetailsGrid", value); }
    }

    public DataTable SessionShortDetailsGrid
    {
        get { return StateManager.GetState<DataTable>("SessionShortDetailsGrid"); }
        set { StateManager.SaveState("SessionShortDetailsGrid", value); }
    }

    public DataTable SessionDamageDetailsGrid
    {
        get { return StateManager.GetState<DataTable>("SessionDamageDetailsGrid"); }
        set { StateManager.SaveState("SessionDamageDetailsGrid", value); }
    }

    public DataTable SessionUnloadingDetailsGrid
    {
        get { return StateManager.GetState<DataTable>("UnloadingDetailsGrid"); }
        set { StateManager.SaveState("UnloadingDetailsGrid", value); }
    }

    public String Unloading_Details_Xml
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionUnloadingDetailsGrid.Copy());
            _objDs.Tables[0].TableName = "unloading_details";
            return _objDs.GetXml().ToLower();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());
        errorMessage = "";
        if (!IsPostBack)
        {
            Set_Default_Value();
            if (keyID <= 0)
            {
                wuc_AUSDate.SelectedDate = DateTime.Now.Date;
                Next_Actual_Unloading_Number();
            }
            else
            {
                ReadValues();
                ddl_LHPO.Enabled = false;
                WucVehicleSearch1.SetEnabled = false;
            }
            ShortReadValues();
            ExcessReadValues();
            DamageReadValues();

            if (Mode == "4")
            {
                wuc_AUSDate.Enabled = false;
                TD_Calender.Visible = false;
            }
        }

        DataSet ds = objComm.EC_Common_Pass_Query("select Employee_Id from dbo.COM_Adm_User where User_Id = " + UserManager.getUserParam().UserId);
        EmployeeID = Util.String2Int(ds.Tables[0].Rows[0][0].ToString());

        string Crypt = "", _LHPO_Id = "";
        int _VehicleId;

        Crypt = System.Web.HttpContext.Current.Request.QueryString["VehicleId"];
        _VehicleId = ClassLibraryMVP.Util.DecryptToInt(Crypt);

        if (_VehicleId > 0)
        {
            Crypt = System.Web.HttpContext.Current.Request.QueryString["LHPOId"];
            _LHPO_Id = ClassLibraryMVP.Util.DecryptToString(Crypt);

            WucVehicleSearch1.VehicleID = _VehicleId;
            ddl_LHPO.SelectedValue = _LHPO_Id;
            GetDetails(this, e);
            ddl_LHPO.Enabled = false;
            WucVehicleSearch1.SetEnabled = false;
        }        

        WucVehicleSearch1.DDLVehicleIndexChange += new EventHandler(GetDetails);
    }

    private void Next_Actual_Unloading_Number()
    {
        TURNo = objComm.Get_Next_Number();
    }
    public void Set_Default_Value()
    {
        if (!IsPostBack)
        {
            rbl_Short.SelectedValue = "0";
            rbl_Damage.SelectedValue = "0";
            rbl_Excess.SelectedValue = "0";

            string set_time = DateTime.Now.ToString("HH:mm");
            wuc_TASTime.setFormat("24");
            wuc_TASTime.setTime(set_time);
        }
    }
    public void Get_VehicleDetails()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam ={  
                                    objDAL.MakeOutParams("@Vehicle_Category_ID", SqlDbType.VarChar , 20 ),
                                    objDAL.MakeOutParams("@Vehicle_Category", SqlDbType.VarChar , 20 ),    
                                    objDAL.MakeOutParams("@NoofMinuteDifferenceForLate", SqlDbType.Int , 0 ),    
                                    objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int,0, WucVehicleSearch1.VehicleID)};

        objDAL.RunProc("EC_Opr_AUS_Get_Vehicle_Details", objSqlParam, ref objDS);

        hdn_Vehicle_Category_Id.Value = objSqlParam[0].Value.ToString();
        lbl_Vehicle_Category.Text = objSqlParam[1].Value.ToString();
    }
    public void GetDetails(object sender, EventArgs e)
    {
        Get_VehicleDetails();
        Get_LHPO();
        Get_LHPODetails();
    }

    public void Get_LHPO()
    {
        DAL objDAL = new DAL();
        SqlParameter[] objSqlParam ={
                                    objDAL.MakeInParams("@AUS_Branch_ID", SqlDbType.Int  , 0, UserManager.getUserParam().MainId  ),                                    
                                    objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int, 0, WucVehicleSearch1.VehicleID),
                                    objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0, 1),
                                    objDAL.MakeInParams("@AUS_Date", SqlDbType.DateTime  , 0, wuc_AUSDate.SelectedDate)                                 
            };

        objDAL.RunProc("EC_opr_AUS_Get_LHPO", objSqlParam, ref objDS);

        ddl_LHPO.DataTextField = "LHPO_No_For_Print";
        ddl_LHPO.DataValueField = "Main_LHPO_ID";

        ddl_LHPO.DataSource = objDS.Tables[0];
        ddl_LHPO.DataBind();
    }
    public void Get_LHPODetails()
    {
        DAL objDAL = new DAL();
        SqlParameter[] objSqlParam ={   objDAL.MakeOutParams("@LHPO_Date", SqlDbType.VarChar  , 20 ) ,
                                            objDAL.MakeOutParams("@Schedule_Arrival_Delivery_Date", SqlDbType.VarChar  , 20 ) ,
                                            objDAL.MakeOutParams("@Schedule_Arrival_Delivery_Time", SqlDbType.VarChar  , 20 ) ,
                                            objDAL.MakeOutParams("@Total_Booking_Articles", SqlDbType.Int   , 0 ) ,
                                            objDAL.MakeOutParams("@Total_Booking_Articles_Wt", SqlDbType.Int , 0 ) ,
                                            objDAL.MakeOutParams("@Total_Loaded_Articles", SqlDbType.Int , 0 ) ,
                                            objDAL.MakeOutParams("@Total_Loaded_Articles_Wt", SqlDbType.Int , 0 ) ,
                                            objDAL.MakeOutParams("@Total_Received_Articles", SqlDbType.Int , 0 ) ,
                                            objDAL.MakeOutParams("@Total_Received_Articles_Wt", SqlDbType.Int , 0 ) ,
                                            objDAL.MakeOutParams("@Total_Damage_Leakage_Articles", SqlDbType.Int , 20 ) ,
                                            objDAL.MakeOutParams("@Total_Damage_Leakage_Value", SqlDbType.Int , 0 ) ,
                                            objDAL.MakeOutParams("@Delivery_Commision", SqlDbType.Decimal  , 0), 
                                            objDAL.MakeOutParams("@To_Pay_Collection", SqlDbType.Decimal  , 0),
                                            objDAL.MakeOutParams("@Delivery_Receivable",SqlDbType.Decimal,0),
                                            objDAL.MakeOutParams("@Service_Charges_Payable",SqlDbType.Decimal,0),
                                            objDAL.MakeInParams("@AUS_Branch_ID", SqlDbType.Int  , 0, UserManager.getUserParam().MainId  ),                                    
                                            objDAL.MakeInParams("@LHPO_Id", SqlDbType.Int, 0, Util.String2Int(ddl_LHPO.SelectedValue)),
                                            objDAL.MakeInParams("@TAS_Id", SqlDbType.Int, 0,0),
                                            objDAL.MakeInParams("@menuitem_id",SqlDbType.Int, 0, 72),
                                            objDAL.MakeOutParams("@LHPOFromLocation",SqlDbType.VarChar,20),
                                            objDAL.MakeOutParams("@LHPOToLocation",SqlDbType.VarChar,20),
                                            objDAL.MakeOutParams("@BTHAmount",SqlDbType.Decimal,0),
                                            objDAL.MakeOutParams("@UpCountry_Receivable",SqlDbType.Decimal,0),
                                            objDAL.MakeOutParams("@UpCountry_Crssing_Cost_Payable",SqlDbType.Decimal,0)
                                            };

        objDAL.RunProc("EC_opr_AUS_Get_Memo_GC_Details", objSqlParam, ref objDS);

        SessionUnloadingDetailsGrid = objDS.Tables[0];

        lbl_LHPO_Date.Text = objSqlParam[0].Value.ToString();
        lbl_BTHAmountValue.Text = objSqlParam[21].Value.ToString();
        lbl_LHPOFromLocation.Text = objSqlParam[19].Value.ToString();
        lbl_LHPOToLocation.Text = objSqlParam[20].Value.ToString();

        TotalLRs = Util.String2Int(objDS.Tables[0].Rows.Count.ToString());
        TotalLoadedArticles = Util.String2Int(objSqlParam[5].Value.ToString());
        TotalLoadedWeight = Util.String2Decimal(objSqlParam[6].Value.ToString());

        TotalReceivedArticles = Util.String2Int(objSqlParam[7].Value.ToString());
        TotalReceivedWt = Util.String2Decimal(objSqlParam[8].Value.ToString());

        Session["AUSNew_LHPO_Id"] = ddl_LHPO.SelectedValue.ToString();
    }
    protected void ddl_LHPO_SelectedIndexChanged(object sender, EventArgs e)
    {
        Get_LHPODetails();
    }
    protected void wuc_AUSDate_SelectionChanged(object sender, EventArgs e)
    {
        if (keyID <= 0)
        {
            Get_LHPO();
            Get_LHPODetails();
        }
    }
    public void ExcessReadValues()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Actual_Unloading_Sheet_ID", SqlDbType.Int, 0, keyID) };
        objDAL.RunProc("dbo.EC_Operation_Excess_ReadValues", objSqlParam, ref objDS);

        SessionExcessDetailsGrid = objDS.Tables[0];
    }
    public void ShortReadValues()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Actual_Unloading_Sheet_ID", SqlDbType.Int, 0, keyID) };
        objDAL.RunProc("dbo.EC_Operation_AUS_Short_ReadValues", objSqlParam, ref objDS);

        Common.SetPrimaryKeys(new string[] { "GC_ID" }, objDS.Tables[0]);

        SessionShortDetailsGrid = objDS.Tables[0];
    }
    public void DamageReadValues()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Actual_Unloading_Sheet_ID", SqlDbType.Int, 0, keyID) };
        objDAL.RunProc("dbo.EC_Operation_AUS_Damage_ReadValues", objSqlParam, ref objDS);
        Common.SetPrimaryKeys(new string[] { "GC_ID" }, objDS.Tables[0]);
        SessionDamageDetailsGrid = objDS.Tables[0];
    }
    public String AUSExcessGridXML
    {
        get
        {
            DataSet _objDs1 = new DataSet();
            _objDs1.Tables.Add(SessionExcessDetailsGrid.Copy());

            _objDs1.Tables[0].TableName = "excess_details";
            return _objDs1.GetXml().ToLower();
        }
    }
    public void ClearVariables()
    {
        SessionExcessDetailsGrid = null;
        SessionShortDetailsGrid = null;
        SessionDamageDetailsGrid = null;
        SessionUnloadingDetailsGrid = null;
        Session["AUSNew_LHPO_Id"] = null;
    }

    protected void rbl_Excess_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbl_Excess.SelectedItem.Text == "Yes")
            CalculateExcessTotal();
        //else
        //    TotalExcess = 0;
    }

    protected void btn_ExcessUpdate_Click(object sender, EventArgs e)
    {
        CalculateExcessTotal();
    }
    protected void btn_ShortUpdate_Click(object sender, EventArgs e)
    {
        CalculateShortTotal();
    }
    protected void btn_DamageUpdate_Click(object sender, EventArgs e)
    {
        CalculateDamageTotal();
    }
    private void CalculateExcessTotal()
    {
        int Total_Excess, i, j;
        Total_Excess = 0;
        objDT = SessionExcessDetailsGrid;

        if (objDT.Rows.Count > 0)
        {
            for (i = 0; i <= objDT.Rows.Count - 1; i++)
                Total_Excess = Total_Excess + Convert.ToInt32(objDT.Rows[i]["Excess_Articles"].ToString());
        }

        TotalExcess = Total_Excess;
    }
    private void CalculateDamageTotal()
    {
        int Total_Damaged, i, l;
        decimal Total_Damaged_Value = 0;
        Total_Damaged = 0;
        objDT = SessionDamageDetailsGrid;

        if (objDT.Rows.Count > 0)
        {
            for (i = 0; i <= objDT.Rows.Count - 1; i++)
            {
                int damageGCID = Convert.ToInt32(objDT.Rows[i]["GC_ID"].ToString());
                int Received_Condition_ID = Convert.ToInt32(objDT.Rows[i]["Received_Condition_ID"].ToString());
                int Damaged_Articles = Convert.ToInt32(objDT.Rows[i]["Damaged_Articles"].ToString());
                decimal Damaged_Value = Util.String2Decimal(objDT.Rows[i]["Damaged_Value"].ToString());

                Total_Damaged = Total_Damaged + Damaged_Articles;
                Total_Damaged_Value = Total_Damaged_Value + Damaged_Value;

                for (l = 0; l < SessionUnloadingDetailsGrid.Rows.Count; l++)
                {
                    int GCID = Convert.ToInt32(SessionUnloadingDetailsGrid.Rows[l]["GC_ID"].ToString());

                    if (GCID == damageGCID)
                    {
                        SessionUnloadingDetailsGrid.Rows[l]["damaged_articles"] = Damaged_Articles;
                        SessionUnloadingDetailsGrid.Rows[l]["Damaged_Value"] = Damaged_Value;
                        SessionUnloadingDetailsGrid.Rows[l]["Received_Condition_ID"] = Received_Condition_ID;

                        break;
                    }
                }
            }
        }

        TotalDamaged = Total_Damaged;
        TotalDamagedValue = Total_Damaged_Value;
    }
    private void CalculateShortTotal()
    {
        int Total_Short, i, l;
        Total_Short = 0;
        DataTable objDTShort = SessionShortDetailsGrid;
        DataTable objDTMemo = SessionUnloadingDetailsGrid;

        if (objDTShort.Rows.Count > 0)
        {
            for (i = 0; i <= objDTShort.Rows.Count - 1; i++)
            {
                int shortGCID = Convert.ToInt32(objDTShort.Rows[i]["GC_ID"].ToString());
                int ShortArticles = Convert.ToInt32(objDTShort.Rows[i]["Short_Articles"].ToString());
                int ReceivedArticles = Convert.ToInt32(objDTShort.Rows[i]["Received_Articles"].ToString());
                Total_Short = Total_Short + Convert.ToInt32(ShortArticles);

                //DataView dv = new DataView(objDTMemo);
                //dv.RowFilter = "GC_ID = " + shortGCID;
                //dv.RowStateFilter = DataViewRowState.CurrentRows;
                //DataTable dd = dv.ToTable();

                //if (dd.Rows.Count > 0)
                //{
                //    txt_lol_Tmp_frgt.Text = dd.Rows[0]["TempoFrt"].ToString();
                //    hdn_Local_Tempo_Freight.Value = dd.Rows[0]["TempoFrt"].ToString();
                //}

                for (l = 0; l < objDTMemo.Rows.Count; l++)
                {
                    int GCID = Convert.ToInt32(objDTMemo.Rows[l]["GC_ID"].ToString());

                    if (GCID == shortGCID)
                    {
                        int LoadedArticles = Util.String2Int(objDTMemo.Rows[l]["Loaded_Articles"].ToString());
                        decimal LoadedWeight = Util.String2Decimal(objDTMemo.Rows[l]["Loaded_Actual_Wt"].ToString());
                        decimal AvgWeight = (LoadedWeight / LoadedArticles) * ReceivedArticles;

                        SessionUnloadingDetailsGrid.Rows[l]["Received_articles"] = ReceivedArticles;
                        SessionUnloadingDetailsGrid.Rows[l]["Received_Wt"] = AvgWeight;

                        break;
                    }
                }
            }
        }

        TotalShort = Total_Short;
        TotalReceivedArticles = Util.String2Int(SessionUnloadingDetailsGrid.Compute("SUM(Received_articles)", String.Empty).ToString());
        TotalReceivedWt = Util.String2Decimal(SessionUnloadingDetailsGrid.Compute("SUM(Received_Wt)", String.Empty).ToString());
    }

    public bool validateUI()
    {
        bool _isValid = false;

        errorMessage = "";

        if (WucVehicleSearch1.VehicleID <= 0)
        {
            errorMessage = "Please Select Vehicle.";
        }
        else if (LHPO_Id <= 0)
        {
            errorMessage = "Please Select LHPO No.";
        }
        else if (Datemanager.IsValidProcessDate("OPR_AUS", AUS_Date) == false)
        {
            errorMessage = "TUR Date Should be Less than or equal to System/Current Date";
        }

        else if (rbl_Short.Items[0].Selected == false && rbl_Short.Items[1].Selected == false)
        {
            errorMessage = "Select Short Articles Remarks as Yes/No.";
        }

        else if (rbl_Short.Items[1].Selected == true && Convert.ToInt32(lbl_Total_Short.Text) == 0)
        {
            errorMessage = "Enter Short Articles Deails";
        }

        else if (rbl_Short.Items[1].Selected == false && Convert.ToInt32(lbl_Total_Short.Text) > 0)
        {
            errorMessage = "Short Articles Entered And You Select As No Short";
        }

        else if (rbl_Damage.Items[0].Selected == false && rbl_Damage.Items[1].Selected == false)
        {
            errorMessage = "Select Damaged Articles Remarks as Yes/No.";
        }

        else if (rbl_Damage.Items[1].Selected == true && Convert.ToInt32(lbl_Total_Damaged.Text) == 0)
        {
            errorMessage = "Enter Damaged Articles Deails";
        }

        else if (rbl_Damage.Items[1].Selected == false && Convert.ToInt32(lbl_Total_Damaged.Text) > 0)
        {
            errorMessage = "Damaged Articles Entered And You Select As No Damaged";
        }

        else if (rbl_Excess.Items[0].Selected == false && rbl_Excess.Items[1].Selected == false)
        {
            errorMessage = "Select Excess Articles Remarks as Yes/No.";
        }

        else if (rbl_Excess.Items[1].Selected == true && Convert.ToInt32(lbl_Total_Excess.Text) == 0)
        {
            errorMessage = "Enter Excess Articles Deails";
        }

        else if (rbl_Excess.Items[1].Selected == false && Convert.ToInt32(lbl_Total_Excess.Text) > 0)
        {
            errorMessage = "Excess Articles Entered And You Select As No Excess";
        }

        else if (SessionUnloadingDetailsGrid.Rows.Count <= 0)
        {
            errorMessage = " Unloading Details Are Not Available.";
        }
        else if (TotalReceivedArticles <= 0)
        {
            errorMessage = "Total Received Articles Must be Greater Than Zero.";
        }
        else if (AUS_Date < Convert.ToDateTime(lbl_LHPO_Date.Text))
        {
            errorMessage = " AUS Date Can Not be Less than LHPO Date";
        }
        else if (grid_validation() == false)
        {
            _isValid = false;
        }
        else
        {
            _isValid = true;
        }

        return _isValid;
    }
    private bool grid_validation()
    {
        int Lod_Art, Rec_Art;
        decimal Lod_Wt, Rec_Wt;
        CheckBox chk;
        int i;
        bool ATS = true;

        if (SessionUnloadingDetailsGrid.Rows.Count > 0)
        {
            for (i = 0; i <= SessionUnloadingDetailsGrid.Rows.Count - 1; i++)
            {
                Rec_Art = Util.String2Int(SessionUnloadingDetailsGrid.Rows[i]["Received_articles"].ToString());
                Rec_Wt = Util.String2Decimal(SessionUnloadingDetailsGrid.Rows[i]["Received_Wt"].ToString());
                Lod_Art = Util.String2Int(SessionUnloadingDetailsGrid.Rows[i]["Loaded_Articles"].ToString());
                Lod_Wt = Util.String2Decimal(SessionUnloadingDetailsGrid.Rows[i]["Loaded_Actual_Wt"].ToString());

                //if (Rec_Wt <= 0)
                //{
                //    errorMessage = "Recieved Articles Wt. must be greater than zero";
                //    ATS = false;
                //    break;
                //}
                //else 
                if (Rec_Art > Lod_Art)
                {
                    errorMessage = "Recieved Articles should not be greater than Loaded Articles";
                    ATS = false;
                    break;
                }
                else if (Rec_Wt > Lod_Wt)
                {
                    errorMessage = "Recieved Articles Wt. should not be greater than Loaded Articles Wt.";
                    ATS = false;
                    break;
                }
                else
                {
                    ATS = true;
                }
            }
        }
        return ATS;
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndNew";
        if (validateUI())
        {
            Save();
        }
    }
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        if (validateUI())
        {
            Save();
        }
    }
    protected void btn_Save_Print_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndPrint";
        if (validateUI())
        {
            Save();
        }
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

    public void Save()
    {
        Message objMessage = new Message();
        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            objDAL.MakeOutParams("@Print_Doc_ID", SqlDbType.Int, 0),
            objDAL.MakeInParams("@Division_ID",SqlDbType.Int,0, UserManager.getUserParam().DivisionId  ),
            objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0, UserManager.getUserParam().YearCode  ),
            objDAL.MakeInParams("@Un_Loading_Branch_ID",SqlDbType.Int,0, UserManager.getUserParam().MainId  ),
            objDAL.MakeInParams("@Actual_Unloading_Sheet_ID",SqlDbType.Int,0,keyID),
            objDAL.MakeInParams("@Actual_Unloading_Sheet_No",SqlDbType.Int,0, 0 ),
            objDAL.MakeInParams("@Actual_Unloading_Sheet_No_For_Print",SqlDbType.VarChar,20,TURNo),
            objDAL.MakeInParams("@Actual_Unloading_Sheet_Date",SqlDbType.DateTime,0,AUS_Date),
            objDAL.MakeInParams("@Manual_TUR_No",SqlDbType.NVarChar,40, ""),
            objDAL.MakeInParams("@Vehicle_ID",SqlDbType.Int,0, WucVehicleSearch1.VehicleID),
            objDAL.MakeInParams("@Vehicle_Type_ID",SqlDbType.Int,0,Vehicle_Category_Id ),
            objDAL.MakeInParams("@LHPO_ID",SqlDbType.Int,0, LHPO_Id ),
            objDAL.MakeInParams("@Total_Actual_GCs",SqlDbType.Int,0,TotalLRs),
            objDAL.MakeInParams("@Total_Actual_Weight",SqlDbType.Decimal,0,TotalLoadedWeight),
            objDAL.MakeInParams("@Total_Received_Weight",SqlDbType.Decimal,0,TotalReceivedWt),
            objDAL.MakeInParams("@Total_Actual_Articles",SqlDbType.Int,0, TotalLoadedArticles),
            objDAL.MakeInParams("@Total_Received_Articles",SqlDbType.Int,0,TotalReceivedArticles),
            objDAL.MakeInParams("@Scheduled_Arrival_Date",SqlDbType.DateTime,0,AUS_Date),
            objDAL.MakeInParams("@Scheduled_Arrival_Time",SqlDbType.VarChar,0,AUS_Time),
            objDAL.MakeInParams("@Vehicle_Arrival_Date",SqlDbType.DateTime,0,AUS_Date),
            objDAL.MakeInParams("@Vehicle_Arrival_Time",SqlDbType.VarChar  ,0,AUS_Time),
            objDAL.MakeInParams("@Truck_Unloaded_Time",SqlDbType.VarChar,0,AUS_Time),
            objDAL.MakeInParams("@Reason_For_Late_Arrival_ID",SqlDbType.Int,0,0),
            objDAL.MakeInParams("@Reason_For_Late_Unloading_ID",SqlDbType.Int,0,0),
            objDAL.MakeInParams("@Total_Short_Articles",SqlDbType.Int,0,TotalShort),
            objDAL.MakeInParams("@Total_Excess_Articles",SqlDbType.Int,0,TotalExcess),
            objDAL.MakeInParams("@Total_Damaged_Leakage_Articles",SqlDbType.Int,0, TotalDamaged),
            objDAL.MakeInParams("@Total_Damaged_Leakage_Value",SqlDbType.Decimal,0,TotalDamagedValue),

            objDAL.MakeInParams("@Total_Additional_Freight",SqlDbType.Decimal,0,0 ),
            objDAL.MakeInParams("@Total_Delivery_Commision",SqlDbType.Decimal,0,0),
            objDAL.MakeInParams("@Total_To_Pay_Collection",SqlDbType.Decimal,0,0),
            objDAL.MakeInParams("@Lorry_Hire",SqlDbType.Decimal,0,0),
            objDAL.MakeInParams("@Other_Receavable_Charges",SqlDbType.Decimal,0,0),
            objDAL.MakeInParams("@Other_Payable_Charges",SqlDbType.Decimal,0,0),
            objDAL.MakeInParams("@Total_Receable",SqlDbType.Decimal,0,0),
            objDAL.MakeInParams("@Total_Payable",SqlDbType.Decimal,0,0),

            objDAL.MakeInParams("@Unloaded_Supervisor_ID",SqlDbType.Int,0, EmployeeID),
            objDAL.MakeInParams("@Remarks",SqlDbType.VarChar,250, ""),
            objDAL.MakeInParams("@Unloading_Details_Xml",SqlDbType.Xml,0, Unloading_Details_Xml),
            objDAL.MakeInParams("@Excess_Details_Xml",SqlDbType.Xml,0, AUSExcessGridXML),
            objDAL.MakeInParams("@Is_Cancelled",SqlDbType.Bit,0,0),
            objDAL.MakeInParams("@Created_By",SqlDbType.Int,0, UserManager.getUserParam().UserId  ),
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2, UserManager.getUserParam().HierarchyCode),                        
            objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0, Common.GetMenuItemId ()),
            objDAL.MakeInParams("@TAS_ID",SqlDbType.Int,0,0),
            objDAL.MakeInParams("@UpCountry_Receivable",SqlDbType.Decimal,0,0),
            objDAL.MakeInParams("@UpCountry_Crossing_Cost",SqlDbType.Decimal,0,0)
            };

        objDAL.RunProc("EC_Opr_AUS_Save", objSqlParam);


        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {

            string _Msg;
            _Msg = "Saved SuccessFully";

            if (keyID == -1)
            {
                get_GCIDs_AUSID(Convert.ToInt32(objSqlParam[2].Value));
            }
            ClearVariables();
            if (_flag == "SaveAndNew")
            {
                int MenuItemId = Common.GetMenuItemId();
                string Mode = HttpContext.Current.Request.QueryString["Mode"];
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Operations/Inward/FrmAUSNew.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
            }
            else if (_flag == "SaveAndExit")
            {
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            }
            else if (_flag == "SaveAndPrint")
            {
                string AUSTYPE = "";
                int MenuItemId = Common.GetMenuItemId();
                string Mode = HttpContext.Current.Request.QueryString["Mode"];
                int Document_ID = Convert.ToInt32(objSqlParam[2].Value);
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(Document_ID) + "&AUSTYPE=" + ClassLibraryMVP.Util.EncryptString(AUSTYPE)));
            }
        }

        //return objMessage;
        //}
    }


    public void get_GCIDs_AUSID(int AUSID)
    {
        DataTable SessionDT = null;

        //SessionDT = objIAUSView.AUSUnloadingDetailsView.SessionUnloadingDetailsGrid;

        SessionDT = SessionUnloadingDetailsGrid;

        int i, DocumentID;
        DocumentID = AUSID;
        if (SessionDT.Rows.Count > 0)
        {
            for (i = 0; i <= SessionDT.Rows.Count - 1; i++)
            {
                int GC_ID = 0;
                GC_ID = Convert.ToInt32(SessionDT.Rows[i]["GC_ID"]);
                Get_Consignor_Conginee_SMS(GC_ID, DocumentID);
            }
        }
    }

    public void Get_Consignor_Conginee_SMS(int GC_ID, int DocumentID)
    {
        DAL objdal = new DAL();
        DataSet ds = new DataSet();
        int MenuItemId = Common.GetMenuItemId();
        SqlParameter[] sqlPara = { objdal.MakeInParams("@GC_ID", SqlDbType.Int, 0, GC_ID)
             ,objdal.MakeInParams("@MenuItemID", SqlDbType.Int, 0, MenuItemId)
             ,objdal.MakeInParams("@DocumentID", SqlDbType.Int, 0, DocumentID) };

        objdal.RunProc("Ec_Opr_Get_Consignor_Conginee_SMS_MSG", sqlPara, ref ds);


        if (ds.Tables[1].Rows.Count > 0)
        {
            string result = "";
            WebRequest request = null;
            HttpWebResponse response = null;
            try
            {

                String sendToPhoneNumber = ds.Tables[1].Rows[0]["Consignee_Mobile_No"].ToString();
                string msg = ds.Tables[1].Rows[0]["MsgConsignee"].ToString();

                if (ValidateMobileDetails(sendToPhoneNumber, msg))
                {

                    String userid = "2000126072";
                    String passwd = "Rajan@1234";


                    String url = "http://enterprise.smsgupshup.com/GatewayAPI/rest?userid=" + userid + "&password=" + passwd + "&method=SendMessage&send_to=" + sendToPhoneNumber + "&msg=" + msg + "&msg_type=TEXT&auth_scheme=plain&v=1.1&format=text";
                    //String url = "http://raj.com/GatewayAPI/rest?userid=" + userid + "&password=" + passwd + "&method=SendMessage&send_to=" + sendToPhoneNumber + "&msg=" + msg + "&msg_type=TEXT&auth_scheme=plain&v=1.1&format=text";

                    request = WebRequest.Create(url);

                    response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                    }
                    Stream stream = response.GetResponseStream();
                    Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
                    StreamReader reader = new System.IO.StreamReader(stream, ec);
                    result = reader.ReadToEnd();
                    Console.WriteLine(result);
                    reader.Close();
                    stream.Close();
                }
            }
            catch (Exception exp)
            {
                string excep = exp.ToString();
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }


    }
    public bool ValidateMobileDetails(String sendToPhoneNumber, string msg)
    {
        bool Is_Valid = false;
        if (sendToPhoneNumber == "0" || msg == "")
        {
            Is_Valid = false;
        }
        else
        {
            Is_Valid = true;
        }

        return Is_Valid;
    }

    public void ReadValues()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeOutParams("@Delivery_Commision", SqlDbType.Decimal  , 0), 
                                          objDAL.MakeOutParams("@To_Pay_Collection", SqlDbType.Decimal  , 0), 
                                        objDAL.MakeInParams("@Actual_Unloading_Sheet_ID", SqlDbType.Int, 0, keyID),
                                        objDAL.MakeOutParams("@UpCountry_Receivable",SqlDbType.Decimal,0),
                                        objDAL.MakeOutParams("@UpCountry_Crssing_Cost_Payable",SqlDbType.Decimal,0),
                                        objDAL.MakeOutParams("@Delivery_Receivable",SqlDbType.Decimal,0),
                                        objDAL.MakeOutParams("@Service_Charges_Payable",SqlDbType.Decimal,0)
            };


        objDAL.RunProc("dbo.EC_Opr_AUS_ReadValues", objSqlParam, ref objDS);

        if (objDS.Tables[1].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[1].Rows[0];

            TURNo = objDR["Actual_Unloading_Sheet_No_For_Print"].ToString();
            WucVehicleSearch1.VehicleID = Util.String2Int(objDR["Vehicle_Id"].ToString());
            VehicleID = Util.String2Int(objDR["Vehicle_Id"].ToString());
            AUS_Date = Convert.ToDateTime(objDR["Actual_Unloading_Sheet_Date"].ToString());

            SetLHPO(objDR["LHPO_No_For_Print"].ToString(), objDR["LHPO_ID"].ToString());
            Session["AUSNew_LHPO_Id"] = objDR["LHPO_ID"].ToString();
            LHPODate = objDR["LHPO_Date"].ToString();
            LHPOFromLocation = objDR["From_Location"].ToString();
            LHPOToLocation = objDR["To_Location"].ToString();
            BTHAmount = Util.String2Decimal(objDR["Balance_payble_Amount"].ToString());

            TotalLRs = Util.String2Int(objDR["Total_Actual_GCs"].ToString());
            TotalLoadedWeight = Util.String2Decimal(objDR["Total_Actual_Weight"].ToString());
            TotalLoadedArticles = Util.String2Int(objDR["Total_Actual_Articles"].ToString());
            TotalReceivedArticles = Util.String2Int(objDR["Total_Received_Articles"].ToString());
            TotalReceivedWt = Util.String2Decimal(objDR["Total_Received_Weight"].ToString());

            TotalDamaged = Util.String2Int(objDR["Total_Damaged_Leakage_Articles"].ToString());
            TotalDamagedValue = Util.String2Decimal(objDR["Total_Damaged_Leakage_Value"].ToString());
            TotalShort = Util.String2Int(objDR["Total_Short_Articles"].ToString());
            TotalExcess = Util.String2Int(objDR["Total_Excess_Articles"].ToString());

            EmployeeID = Util.String2Int(objDR["Unloaded_Supervisor_ID"].ToString());
        }

        SessionUnloadingDetailsGrid = objDS.Tables[0];
        if (objDS.Tables[0].Rows.Count > 0)
        {
            TotalLoadedArticles = Util.String2Int(objDS.Tables[0].Compute("sum(Loaded_Articles)", "").ToString());
            TotalLoadedWeight = Util.String2Decimal(objDS.Tables[0].Compute("sum(Loaded_Actual_Wt)", "").ToString());

            TotalReceivedArticles = Util.String2Int(objDS.Tables[0].Compute("sum(Received_Articles)", "").ToString());
            TotalReceivedWt = Util.String2Decimal(objDS.Tables[0].Compute("sum(Received_Wt)", "").ToString());

            TotalDamaged = Util.String2Int(objDS.Tables[0].Compute("sum(damaged_articles)", "").ToString());
            TotalDamagedValue = Util.String2Decimal(objDS.Tables[0].Compute("sum(Damaged_Value)", "").ToString());
        }
        Get_VehicleDetails();
    }
}
