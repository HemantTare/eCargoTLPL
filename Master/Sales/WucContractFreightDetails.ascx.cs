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

/// <summary>
/// Author        : Aashish Lad
/// Created On    : 14th October 2008
/// Description   : This is the Page For Master Contract Freight Details
/// </summary> 
public partial class Master_Sales_WucContractFreightDetails : System.Web.UI.UserControl, IContractFreightDetailsView
{
    #region ClassVariables
    ContractFreightDetailsPresenter objContractFreightDetailsPresenter;
    TextBox txt_SrNo, txt_ID,txt_FromKMS, txt_ToKMS, txt_FromDays, txt_ToDays;
    TextBox txt_KMS, txt_TransitDays, txt_Freight, txt_FromID, txt_ToID, txt_RangeFrom, txt_RangeTo;
    //DropDownList ddl_ToLocation;
    ClassLibrary.UIControl.DDLSearch ddl_FromLocation, ddl_ToLocation;    
    Label lbl_FreightHeading, lbl_DetailsHeader;
    LinkButton lbtn_Delete, lbtn_Details;
    private ScriptManager scm_ContractFreightDetails;
    DataSet objDS;
    bool isValid = false;
    int NextSrNo = 0;
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    
    #endregion

    #region ControlsValue
    public int UnitOfFreightID
    {
        set
        {
            ddl_UnitOfFreight.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_UnitOfFreight.SelectedValue);
        }

    }

    public int SrNo
    {
        set
        {
            txt_SrNo.Text = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(txt_SrNo.Text);
        }

    }
    public int SubUnitID
    {
        set
        {
            ddl_SubUnit.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_SubUnit.SelectedValue);
        }

    }
    public int FreightBasisID
    {
        set
        {
            ddl_FreightBasis.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_FreightBasis.SelectedValue);
        }

    }
    public int UnitFreightID
    {
        set
        {
            ddl_UnitFreight.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_UnitFreight.SelectedValue);
        }

    }
    public int SubUnitFreightID
    {
        set
        {
            ddl_SubUnitFreight.SelectedValue = Util.Int2String(value);
        }
        get
        {
            if (Util.String2Int(ddl_SubUnitFreight.SelectedValue) == -1)
            {
                return 0;
            }
            else
            {
                return Util.String2Int(ddl_SubUnitFreight.SelectedValue);
            }
        }

    }
    public int FromLocationID
    {
        //set
        //{
        //    ddl_FromLocation.SelectedValue = Util.Int2String(value);
        //}
        get
        {
            return Util.String2Int(ddl_FromLocation.SelectedValue);
        }

    }

    public int ToLocationID
    {
        //set
        //{
        //    ddl_ToLocation.SelectedValue = Util.Int2String(value);
        //}
        get
        {
            return Util.String2Int(ddl_ToLocation.SelectedValue);
        }

    }

    public decimal CFTFactor
    {
        set
        {
            txt_CFTFactor.Text = Util.Decimal2String(value);
        }
        get
        {
            return Util.String2Decimal(txt_CFTFactor.Text);
        }

    }

    public string FreightDetailsGridXML
    {
        get
        {
            return SessionFreightDetailsGrid.GetXml().ToLower();
        }
    }
    public string OtherChargesFreightRateDetailsXML
    {
        get
        {
            return SessionOtherChargesFreightRateDetails.GetXml().ToLower();
        }
    }
    #endregion

    #region ControlsBind

    public DataTable Bind_ddl_UnitOfFreight
    {
        set
        {
            ddl_UnitOfFreight.DataSource = value;
            ddl_UnitOfFreight.DataTextField = "Freight_Unit";
            ddl_UnitOfFreight.DataValueField = "Freight_Unit_ID";
            ddl_UnitOfFreight.DataBind();
            //Raj.EC.Common.InsertItem(ddl_UnitOfFreight);
        }
    }

    public DataTable Bind_ddl_SubUnit
    {
        set
        {
            ddl_SubUnit.DataSource = value;
            ddl_SubUnit.DataTextField = "Freight_Sub_Unit";
            ddl_SubUnit.DataValueField = "Freight_Sub_Unit_ID";
            ddl_SubUnit.DataBind();
           // Raj.EC.Common.InsertItem(ddl_SubUnit);
        }
    }

    public DataTable Bind_ddl_FreightBasis
    {
        set
        {
            ddl_FreightBasis.DataSource = value;
            ddl_FreightBasis.DataTextField = "Freight_Basis";
            ddl_FreightBasis.DataValueField = "Freight_Basis_ID";
            ddl_FreightBasis.DataBind();
           // Raj.EC.Common.InsertItem(ddl_FreightBasis);
        }
    }

    public DataTable Bind_ddl_UnitFreight
    {
        set
        {
            ddl_UnitFreight.DataSource = value;
            ddl_UnitFreight.DataTextField = "UnitFreightName";
            ddl_UnitFreight.DataValueField = "UnitFreightID";
            ddl_UnitFreight.DataBind();
           // Raj.EC.Common.InsertItem(ddl_UnitFreight);
        }
    }

    public DataTable Bind_ddl_SubUnitFreight
    {
        set
        {
            ddl_SubUnitFreight.DataSource = value;
            ddl_SubUnitFreight.DataTextField = "FreightSubUnit";
            ddl_SubUnitFreight.DataValueField = "FreightSubUnitID";
            ddl_SubUnitFreight.DataBind();
            //Raj.EC.Common.InsertItem(ddl_SubUnitFreight);
        }
    }

    

    public DataSet Bind_dg_FreightDetails
    {
        set
        {
            //SessionFreightDetailsGrid = value;
            //GetSrNo();
            dg_FreightDetails.DataSource = value;
            dg_FreightDetails.DataBind();
        }
    }

    public DataSet SessionOtherChargesFreightRate
    {
        set { StateManager.SaveState("OtherChargesFreightRate", value); }
        get { return StateManager.GetState<DataSet>("OtherChargesFreightRate"); }
    }

    public DataSet SessionFreightDetailsGrid
    {
        get { return StateManager.GetState<DataSet>("FreightDetails"); }
        set 
        {
            if (value != null)
            {
                for (int i = 1; i <= value.Tables[0].Rows.Count; i++)
                {
                    value.Tables[0].Rows[i - 1]["SrNo"] = i;
                }
                StateManager.SaveState("FreightDetails", value);
            }
        }
    }
    public DataSet SessionOtherChargesFreightRateDetails
    {
        get { return StateManager.GetState<DataSet>("OtherChargesFreightRate"); }
        set { StateManager.SaveState("OtherChargesFreightRate", value); }
    }
    //public DataTable SessionFromToLocation
    //{
    //    get { return StateManager.GetState<DataTable>("FromToLocation"); }
    //    set { StateManager.SaveState("FromToLocation", value); }
    //}
    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;
        if (SessionFreightDetailsGrid.Tables[0].Rows.Count <= 0)
        {
            lbl_Errors.Text = "Please Insert At Least One Record In Freight Details";
            scm_ContractFreightDetails.SetFocus(dg_FreightDetails);
            _isValid = false;
        }
        else
        {
            _isValid = true;
        }

        return _isValid;
    }


    public string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;
        }
    }


    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
            // return -1;
        }
    }

    #endregion


    #region OtherProperties
    public ScriptManager SetScriptManager
    {
        set { scm_ContractFreightDetails = value; }
    }
    #endregion


    #region OtherMethods

    private void SetLabelForUnitFreightDetails()
    {
        lbl_CFTFactor.Visible = false;
        txt_CFTFactor.Visible = false;
        if (UnitOfFreightID == 1)
        {
            lbl_UnitFreightDetails.Text = "Vehicle Type:";
        }
        else if (UnitOfFreightID == 2)
        {
            lbl_UnitFreightDetails.Text = "Wt Range:";
        }
        else if (UnitOfFreightID == 3)
        {
            lbl_UnitFreightDetails.Text = "CFT Range:";
            lbl_CFTFactor.Visible = true;
            txt_CFTFactor.Visible = true;
        }
        else if (UnitOfFreightID == 4)
        {
            lbl_UnitFreightDetails.Text = "Articles Range:";
        }
        else if (UnitOfFreightID == 5)
        {
            lbl_UnitFreightDetails.Text = "Wt Range:";
        }

    }
    private void SetLabelForSubUnitFreightDetails()
    {
        ddl_SubUnitFreight.Visible = true;
        lbl_SubUnitFreightDetails.Visible = true;
        if (SubUnitID == 1)
        {
            lbl_SubUnitFreightDetails.Text = "";
            ddl_SubUnitFreight.Visible = false;
            lbl_SubUnitFreightDetails.Visible = false;
        }
        else if (SubUnitID == 2)
        {
            lbl_SubUnitFreightDetails.Text = "Commodity:";
        }
        else if (SubUnitID == 3)
        {
            lbl_SubUnitFreightDetails.Text = "Item:";
        }
        else if (SubUnitID == 4)
        {
            lbl_SubUnitFreightDetails.Text = "Article Type:";
        }
        else if (SubUnitID == 5)
        {
            lbl_SubUnitFreightDetails.Text = "Vehicle Type:";
        }

    }
    private void GetSrNo()
    {
        int Sr_No;
        DataSet ds = new DataSet();
        ds = SessionFreightDetailsGrid;

        DataRow DR = null;
        for (Sr_No = 0; Sr_No <= ds.Tables[0].Rows.Count - 1; Sr_No++)
        {
            DR = ds.Tables[0].Rows[Sr_No];
            DR["SrNo"] = Sr_No + 1;
        }
        SessionFreightDetailsGrid = ds;
    }
    private int AssignSrNo(int SrNo)
    {
        int NextSrNo = 0;
        if (SrNo == 0 && SessionFreightDetailsGrid.Tables[0].Rows.Count > 0)
        {
            NextSrNo = (int)SessionFreightDetailsGrid.Tables[0].Compute("max(SrNo)", "");
            NextSrNo = NextSrNo + 1;
        }
        else if (SessionFreightDetailsGrid.Tables[0].Rows.Count <= 0)
        {
            NextSrNo = 1;
        }
        else if (SrNo != 0)
        {
            NextSrNo = (int)SessionFreightDetailsGrid.Tables[0].Compute("max(SrNo)", "");
        }
        return NextSrNo;
    }
    private void EnabledDisabledGridColumns()
    {
        int FromLocation = 1;
        int ToLocation = 2;
        int FromKMS = 3;
        int ToKMS = 4;
        int FromDays = 5;
        int ToDays = 6;
        int KMS = 7;
        int TransitDays = 8;
        int Freight = 9;

        dg_FreightDetails.Columns[FromLocation].Visible = true;
        dg_FreightDetails.Columns[ToLocation].Visible = true;
        dg_FreightDetails.Columns[FromKMS].Visible = true;
        dg_FreightDetails.Columns[ToKMS].Visible = true;
        dg_FreightDetails.Columns[FromDays].Visible = true;
        dg_FreightDetails.Columns[ToDays].Visible = true;
        dg_FreightDetails.Columns[KMS].Visible = true;
        dg_FreightDetails.Columns[TransitDays].Visible = true;
        dg_FreightDetails.Columns[Freight].Visible = true;
        dg_FreightDetails.Columns[14].Visible = false;
        if (FreightBasisID == 1)//FOR LOCATION
        {
            dg_FreightDetails.Columns[FromKMS].Visible = false;
            dg_FreightDetails.Columns[ToKMS].Visible = false;
            dg_FreightDetails.Columns[FromDays].Visible = false;
            dg_FreightDetails.Columns[ToDays].Visible = false;

        }
        else if (FreightBasisID == 2)//FOR KMS
        {
            dg_FreightDetails.Columns[FromLocation].Visible = false;
            dg_FreightDetails.Columns[ToLocation].Visible = false;
            dg_FreightDetails.Columns[FromDays].Visible = false;
            dg_FreightDetails.Columns[ToDays].Visible = false;
            dg_FreightDetails.Columns[KMS].Visible = false;
        }

        else if (FreightBasisID == 3)//FOR TRANSIT DAYS
        {
            dg_FreightDetails.Columns[FromLocation].Visible = false;
            dg_FreightDetails.Columns[ToLocation].Visible = false;
            dg_FreightDetails.Columns[FromKMS].Visible = false;
            dg_FreightDetails.Columns[ToKMS].Visible = false;
            dg_FreightDetails.Columns[KMS].Visible = false;
            dg_FreightDetails.Columns[TransitDays].Visible = false;
        }
        if (FreightBasisID == 1 && UnitOfFreightID == 2)
        {
            dg_FreightDetails.Columns[KMS].Visible = false;
        }
    }
    private bool Allow_To_Add_Update()
    {
        if (FreightBasisID == 1)
        {
            if (Util.String2Int(ddl_FromLocation.SelectedValue) <=0)
            {
                lbl_Errors.Text = "Please Select From Location";
                scm_ContractFreightDetails.SetFocus(ddl_FromLocation);
            }
            else if (Util.String2Int(ddl_ToLocation.SelectedValue) <= 0)
            {
                lbl_Errors.Text = "Please Select To Location";
                scm_ContractFreightDetails.SetFocus(ddl_ToLocation);
            }
            else if (Util.String2Int(ddl_ToLocation.SelectedValue) == Util.String2Int(ddl_FromLocation.SelectedValue))
            {
                lbl_Errors.Text = "Please Select Different Location";
                scm_ContractFreightDetails.SetFocus(ddl_ToLocation);
            }
            else if (txt_Freight.Text == string.Empty)
            {
                lbl_Errors.Text = "Please Enter Freight";
                scm_ContractFreightDetails.SetFocus(txt_Freight);
            }
            else if (Duplicate_Range() == false)
                lbl_Errors.Text = "Duplicate";
            else
                isValid = true;
        }
        if (FreightBasisID == 2)
        {
            if (txt_FromKMS.Text == string.Empty)
            {
                lbl_Errors.Text = "Please Enter From KMS";
                scm_ContractFreightDetails.SetFocus(txt_FromKMS);
            }
            else if (txt_ToKMS.Text == string.Empty)
            {
                lbl_Errors.Text = "Please Enter To KMS";
                scm_ContractFreightDetails.SetFocus(txt_ToKMS);
            }
            else if (Check_FromKMSToKMS(txt_FromKMS.Text, txt_ToKMS.Text) == false)
            {
                scm_ContractFreightDetails.SetFocus(txt_FromKMS);
                isValid = false;
                return isValid;
            }
            else if (txt_Freight.Text == string.Empty)
            {
                lbl_Errors.Text = "Please Enter Freight";
                scm_ContractFreightDetails.SetFocus(txt_Freight);
            }
            else if (Duplicate_Range() == false)
                lbl_Errors.Text = "Duplicate";
            else
                isValid = true;
        }
        if (FreightBasisID == 3)
        {
            if (txt_FromDays.Text == string.Empty)
            {
                lbl_Errors.Text = "Please Enter From Days";
                scm_ContractFreightDetails.SetFocus(txt_FromDays);
            }
            else if (txt_ToDays.Text == string.Empty)
            {
                lbl_Errors.Text = "Please Enter To Days";
                scm_ContractFreightDetails.SetFocus(txt_ToDays);
            }
            else if (Check_FromDaysToDays(txt_FromDays.Text, txt_ToDays.Text) == false)
            {
                scm_ContractFreightDetails.SetFocus(txt_FromDays);
                isValid = false;
                return isValid;
            }
            else if (txt_Freight.Text == string.Empty)
            {
                lbl_Errors.Text = "Please Enter Freight";
                scm_ContractFreightDetails.SetFocus(txt_Freight);
            }
            else if (Duplicate_Range() == false)
                lbl_Errors.Text = "Duplicate Record Found";
            else
                isValid = true;
        }

        return isValid;
    }



    private Boolean Duplicate_Range()
    {
        Boolean returnvalue = true;

        DataSet dsfreightdetails = new DataSet();
        dsfreightdetails = SessionFreightDetailsGrid;

        int minunit = 0;
        int maxunit = 0;

        if (FreightBasisID == 2 || FreightBasisID == 3)
        {
            TextBox txtfromunit = new TextBox();
            TextBox txttounit = new TextBox();

            if (FreightBasisID == 2)
            {
                txtfromunit = txt_FromKMS;
                txttounit = txt_ToKMS;
            }
            else if (FreightBasisID == 3)
            {
                txtfromunit = txt_FromDays;
                txttounit = txt_ToDays;
            }

            minunit = Util.String2Int(dsfreightdetails.Tables[0].Compute("Min(FromID)", "SrNo <> " + NextSrNo + " and FreightUnitItemID=" + UnitFreightID + " and FreightSubUnitItemID=" + SubUnitFreightID).ToString());
            maxunit = Util.String2Int(dsfreightdetails.Tables[0].Compute("Max(ToID)", "SrNo <> " + NextSrNo + " and FreightUnitItemID=" + UnitFreightID + " and FreightSubUnitItemID=" + SubUnitFreightID).ToString());

            foreach (DataRow dr in dsfreightdetails.Tables[0].Rows)
            {
                if (Util.String2Int(dr["SrNo"].ToString()) != NextSrNo && Util.String2Int(dr["FreightUnitItemID"].ToString()) == UnitFreightID && Util.String2Int(dr["FreightSubUnitItemID"].ToString()) == SubUnitFreightID)
                {
                    if (((Util.String2Int(txtfromunit.Text) >= Util.String2Int(dr["FromID"].ToString()) && Util.String2Int(txtfromunit.Text) <= Util.String2Int(dr["ToID"].ToString())) ||
                    (Util.String2Int(txttounit.Text) >= Util.String2Int(dr["FromID"].ToString()) && Util.String2Int(txttounit.Text) <= Util.String2Int(dr["ToID"].ToString())) ||
                    (Util.String2Int(txtfromunit.Text) <= maxunit && Util.String2Int(txttounit.Text) >= maxunit && maxunit != 0) ||
                    (Util.String2Int(txtfromunit.Text) <= minunit && Util.String2Int(txttounit.Text) >= minunit && minunit != 0)))
                    {
                        returnvalue = false;
                        break;
                    }
                }
            }
        }

        if (FreightBasisID == 1)
        {
            foreach (DataRow dr in dsfreightdetails.Tables[0].Rows)
            {
                if (Util.String2Int(dr["SrNo"].ToString()) != NextSrNo && Util.String2Int(dr["FreightUnitItemID"].ToString()) == UnitFreightID && Util.String2Int(dr["FreightSubUnitItemID"].ToString()) == SubUnitFreightID)
                {
                    if (Util.String2Int(ddl_FromLocation.SelectedValue) == Util.String2Int(dr["FromLocation"].ToString()) && Util.String2Int(ddl_ToLocation.SelectedValue) == Util.String2Int(dr["ToLocation"].ToString()))
                    {
                        returnvalue = false;
                    }
                }
            }
        }

        if (UnitOfFreightID == 2 || UnitOfFreightID == 3 || UnitOfFreightID == 4 || UnitOfFreightID == 5)
        {
            minunit = Util.String2Int(dsfreightdetails.Tables[0].Compute("Min(Range_From)", "FreightUnitItemID<>" + UnitFreightID + " and FreightSubUnitItemID=" + SubUnitFreightID).ToString());
            maxunit = Util.String2Int(dsfreightdetails.Tables[0].Compute("Max(Range_To)", "FreightUnitItemID<>" + UnitFreightID + " and FreightSubUnitItemID=" + SubUnitFreightID).ToString());


            foreach (DataRow dr in dsfreightdetails.Tables[0].Rows)
            {
                if (Util.String2Int(dr["FreightUnitItemID"].ToString()) != UnitFreightID && Util.String2Int(dr["FreightSubUnitItemID"].ToString()) == SubUnitFreightID)
                {
                    if (((Util.String2Int(txt_RangeFrom.Text) >= Util.String2Int(dr["Range_From"].ToString()) && Util.String2Int(txt_RangeFrom.Text) <= Util.String2Int(dr["Range_To"].ToString())) ||
                    (Util.String2Int(txt_RangeTo.Text) >= Util.String2Int(dr["Range_From"].ToString()) && Util.String2Int(txt_RangeTo.Text) <= Util.String2Int(dr["Range_To"].ToString())) ||
                    (Util.String2Int(txt_RangeTo.Text) <= maxunit && Util.String2Int(txt_RangeTo.Text) >= maxunit && maxunit != 0) ||
                    (Util.String2Int(txt_RangeFrom.Text) <= minunit && Util.String2Int(txt_RangeTo.Text) >= minunit && minunit != 0)))
                    {
                        returnvalue = false;
                        break;
                    }
                }
            }
        }


        return returnvalue;
    }

    public void BindGrid()
    {
        /* DataRow Filter From DataSet
        DataSet objDS_Filter = new DataSet();
        objDS_Filter.Tables.Add(SessionFreightDetailsGrid.Tables[0].Clone());
        DataRow[] dr;
        dr = SessionFreightDetailsGrid.Tables[0].Select("FreightUnitItemID= '" + UnitFreightID.ToString() + " ' and FreightSubUnitItemID= '" + SubUnitFreightID.ToString() + "'");
        objDS_Filter.Tables[0].Rows.CopyTo(dr, 0);
         */

        DataSet ds_Temp = new DataSet();
        DataTable dt = new DataTable();
        DataView dv = new DataView();

        dv = ObjCommon.Get_View_Table(SessionFreightDetailsGrid.Tables[0], "FreightUnitItemID= '" + UnitFreightID.ToString() + " ' and FreightSubUnitItemID= '" + SubUnitFreightID.ToString() + "'");
        dt = dv.ToTable();
        ds_Temp.Tables.Add(dt);
        dg_FreightDetails.DataSource = ds_Temp;
        dg_FreightDetails.DataBind();
        dg_FreightDetails.Columns[14].Visible = true;

        EnableDropDown();
    }
    private int GetSrNo_ForUnitAndSubUnitFreightDetails(int ItemIndex)
    {
        int i, SrNo = 0, SrNoForEditDeleteCancel = 0;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        dv = ObjCommon.Get_View_Table(SessionFreightDetailsGrid.Tables[0], "FreightUnitItemID= '" + UnitFreightID.ToString() + " ' and FreightSubUnitItemID= '" + SubUnitFreightID.ToString() + "'");
        dt = dv.ToTable();
        ds.Tables.Add(dt);

        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (i == ItemIndex)
            {
                SrNo = Util.String2Int(ds.Tables[0].Rows[i]["SrNo"].ToString());
                SrNoForEditDeleteCancel = ds.Tables[0].Rows.IndexOf(ds.Tables[0].Rows[i]);
            }
        }
        return SrNoForEditDeleteCancel;
    }
    private DataRow GetDataRow_ForEdit(int ItemIndex, DataSet objDS)
    {
        int i, SrNo = 0, SrNoForEditDeleteCancel = 0;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        dv = ObjCommon.Get_View_Table(SessionFreightDetailsGrid.Tables[0], "FreightUnitItemID= '" + UnitFreightID.ToString() + " ' and FreightSubUnitItemID= '" + SubUnitFreightID.ToString() + "'");
        dt = dv.ToTable();
        ds.Tables.Add(dt);
        DataRow dr = null;
        DataRow drNew = null;
        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (i == ItemIndex)
            {
                SrNo = Util.String2Int(ds.Tables[0].Rows[i]["SrNo"].ToString());
                drNew = objDS.Tables[0].Rows.Find(SrNo);
                //dr = ds.Tables[0].Rows[i];                
                //drNew = objDS.Tables[0].Rows[SrNo];
                //SrNoForEditDeleteCancel = objDS.Tables[0].Rows.IndexOf(drNew);                


            }
        }
        return drNew;
    }
    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        objDS = SessionFreightDetailsGrid;
        DataRow DR = null;
        ddl_FromLocation = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_FromLocation"));
        ddl_ToLocation = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_ToLocation"));
        //ddl_FromLocation = (DropDownList)e.Item.FindControl("ddl_FromLocation");
        //ddl_ToLocation = (DropDownList)e.Item.FindControl("ddl_ToLocation");
        txt_ID = (TextBox)e.Item.FindControl("txt_ID");
        txt_SrNo = (TextBox)e.Item.FindControl("txt_SrNo");
        txt_FromKMS = (TextBox)e.Item.FindControl("txt_FromKMS");
        txt_ToKMS = (TextBox)e.Item.FindControl("txt_ToKMS");
        txt_FromDays = (TextBox)e.Item.FindControl("txt_FromDays");
        txt_ToDays = (TextBox)e.Item.FindControl("txt_ToDays");
        txt_KMS = (TextBox)e.Item.FindControl("txt_KMS");
        txt_TransitDays = (TextBox)e.Item.FindControl("txt_TransitDays");
        txt_Freight = (TextBox)e.Item.FindControl("txt_Freight");
        txt_FromID = (TextBox)e.Item.FindControl("txt_FromID");
        txt_ToID = (TextBox)e.Item.FindControl("txt_ToID");
        txt_RangeFrom = (TextBox)e.Item.FindControl("txt_RangeFrom");
        txt_RangeTo = (TextBox)e.Item.FindControl("txt_RangeTo");

        if (e.CommandName == "ADD")
        {
            DR = objDS.Tables[0].NewRow();
            NextSrNo = AssignSrNo(0);
        }
        if (e.CommandName == "Update")
        {            
           // DR = GetDataRow_ForEdit(e.Item.ItemIndex, objDS);
          //  NextSrNo = Util.String2Int(DR["SrNo"].ToString());
            NextSrNo = Util.String2Int(txt_SrNo.Text);
            DR = objDS.Tables[0].Rows.Find(NextSrNo);
        }

        char[] c = new char[1];
        c[0] = '-';
        string[] RangeArray = new string[2];
        RangeArray = ddl_UnitFreight.SelectedItem.Text.Split(c);

        txt_RangeFrom.Text = RangeArray[0].ToString();
        if (UnitOfFreightID >= 2)
            txt_RangeTo.Text = RangeArray[1].ToString();

        if (Allow_To_Add_Update() == true)
        {

            if (e.CommandName == "ADD")
            {
                DR["ID"] = NextSrNo;
            }
            DR["SrNo"] = NextSrNo;            
            DR["FreightUnitItemID"] = UnitFreightID;
            DR["FreightSubUnitItemID"] = SubUnitFreightID;

            if (FreightBasisID == 1)//For Location
            {

                DR["FromLocation"] = FromLocationID;
                DR["ToLocation"] = ToLocationID;
                DR["FromLocationName"] = ddl_FromLocation.SelectedText;
                DR["ToLocationName"] = ddl_ToLocation.SelectedText;
                DR["FromID"] = ddl_FromLocation.SelectedValue;
                DR["ToID"] = ddl_ToLocation.SelectedValue;

            }
            else
            {
                DR["FromLocation"] = 0;
                DR["ToLocation"] = 0;
            }
            if (FreightBasisID == 2)//For KMS
            {
                DR["FromKMS"] = Util.String2Int(txt_FromKMS.Text);
                DR["ToKMS"] = Util.String2Int(txt_ToKMS.Text);
                DR["FromID"] = Util.String2Int(txt_FromKMS.Text);
                DR["ToID"] = Util.String2Int(txt_ToKMS.Text);

            }
            else
            {
                DR["FromKMS"] = 0;
                DR["ToKMS"] = 0;
            }
            if (FreightBasisID == 3)//For Transit Days
            {

                DR["FromDays"] = Util.String2Int(txt_FromDays.Text);
                DR["ToDays"] = Util.String2Int(txt_ToDays.Text);
                DR["FromID"] = Util.String2Int(txt_FromDays.Text);
                DR["ToID"] = Util.String2Int(txt_ToDays.Text);
            }
            else
            {

                DR["FromDays"] = 0;
                DR["ToDays"] = 0;
            }
            DR["CFTFactor"] = Util.String2Decimal(txt_CFTFactor.Text);
            DR["FreightRate"] = Util.String2Decimal(txt_Freight.Text);



            int TDays = 0;
            if (Util.String2Int(txt_TransitDays.Text) < 0)
            {
                TDays = 0;

            }
            else
            {
                TDays = Util.String2Int(txt_TransitDays.Text);
            }

            int km = 0;

            if (Util.String2Int(txt_KMS.Text) < 0)
            {
                km = 0;
            }
            else
            {
                km = Util.String2Int(txt_KMS.Text);

            }
            DR["TransitDays"] = TDays;
            DR["Kms"] = km;


            if (UnitOfFreightID == 2 || UnitOfFreightID == 3 || UnitOfFreightID == 4 || UnitOfFreightID == 5)
            {
                DR["Range_From"] = Util.String2Int(RangeArray[0]);
                DR["Range_To"] = Util.String2Int(RangeArray[1]);
            }
            else
            {
                DR["Range_From"] = 0;
                DR["Range_To"] = 0;
            }

            if (e.CommandName == "ADD")
            {
                objDS.Tables[0].Rows.Add(DR);
            }
            SessionFreightDetailsGrid = objDS;
        }
    }

    private bool Check_FromDaysToDays(string FromDays, string ToDays)
    {
        if (Util.String2Int(FromDays) >= Util.String2Int(ToDays))
        {
            errorMessage = "From Days Should Not be Greater then To Days";
            return false;
        }
        return true;
    }
    private bool Check_FromKMSToKMS(string FromKMS, string ToKMS)
    {
        if (Util.String2Int(FromKMS) >= Util.String2Int(ToKMS))
        {
            errorMessage = "From KMS Should Not be Greater then To KMS";
            return false;
        }
        return true;
    }
    private void EnabledDisabledDropDown()
    {
        ddl_FreightBasis.Enabled = false;
        ddl_UnitOfFreight.Enabled = false;
        ddl_SubUnit.Enabled = false;
    }
    public void SetFromLocationID(string FromLocation_Name, string FromLocationID)
    {
        ddl_FromLocation.DataTextField = "Service_Location_Name";
        ddl_FromLocation.DataValueField = "Service_Location_ID";
        Raj.EC.Common.SetValueToDDLSearch(FromLocation_Name, FromLocationID, ddl_FromLocation);
    }
    public void SetToLocationID(string ToLocation_Name, string ToLocationID)
    {
        ddl_ToLocation.DataTextField = "Service_Location_Name";
        ddl_ToLocation.DataValueField = "Service_Location_ID";
        Raj.EC.Common.SetValueToDDLSearch(ToLocation_Name, ToLocationID, ddl_ToLocation);
    }
    
    //private bool Allow_ToAddFromKMSToKMS(int Sr_No) 
    //{ 
    //     //DR["FromKMS"] =Util.String2Int(txt_FromKMS.Text);
    //     //       DR["ToKMS"] = Util.String2Int(txt_ToKMS.Text);              


    //    bool functionReturnValue = false; 
    //    functionReturnValue = true; 
    //    DataSet ds_Records_Enterd = new DataSet(); 
    //    ds_Records_Enterd = SessionFreightDetailsGrid; 
    //    DataRow dr = default(DataRow); 

    //    int FromKMS = 0; 
    //    int ToKMS = 0; 

    //    if (ds_Records_Enterd.Tables[0].Rows.Count > 0) 
    //    { 

    //        if ((ds_Records_Enterd.Tables[0].Compute("Min(FromKMS)", "SrNo <> " + Sr_No))  == true)
    //        { 
    //            FromKMS = 0; 
    //            ToKMS = 0; 
    //        } 
    //        else
    //        { 
    //            FromKMS = Convert.ToInt32(ds_Records_Enterd.Tables[0].Compute("Min(FromKMS)", "Sr_No <> " + Sr_No)); 
    //            ToKMS = Convert.ToInt32(ds_Records_Enterd.Tables[0].Compute("Max(ToKMS)", "Sr_No <> " + Sr_No)); 
    //        } 

    //        foreach ( dr in ds_Records_Enterd.Tables[0].Rows) 
    //        { 
    //            if (Conversion.Val(dr["SrNo"]) != Sr_No) { 
    //                if (((Util.String2Int(txt_weight_From.Text) >= Util.String2Int(dr["FromKMS"]) & Util.String2Int[txt_weight_From.Text] <= Conversion.Val(dr["ToKMS"])) | (Conversion.Val(txt_weight_to.Text) >= Conversion.Val(dr("FromKMS")) & Conversion.Val(txt_weight_to.Text) <= Conversion.Val(dr("ToKMS"))) | (Conversion.Val(txt_weight_From.Text) <= Conversion.Val(ToKMS) & Conversion.Val(txt_weight_to.Text) >= Conversion.Val(ToKMS) & ToKMS != 0) | (Conversion.Val(txt_weight_From.Text) <= Conversion.Val(FromKMS) & Conversion.Val(txt_weight_to.Text) >= Conversion.Val(FromKMS) & FromKMS != 0))) 
    //                {                        
    //                    functionReturnValue = false; 
    //                    errorMessage = "Weight Range Already Exist";                        
    //                    return; 
    //                } 
    //            } 
    //        } 
    //    }
    //    return functionReturnValue; 
    //} 
    #endregion

    #region ControlsEvent
    protected void Page_Load(object sender, EventArgs e)
    {
        objContractFreightDetailsPresenter = new ContractFreightDetailsPresenter(this, IsPostBack);
        if (!IsPostBack)
        {
            SetLabelForUnitFreightDetails();
            SetLabelForSubUnitFreightDetails();
            if (keyID > 0)
            {
                EnabledDisabledDropDown();
                BindGrid();
            }
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ddl_UnitFreight.Enabled = true;
    }

    protected void ddl_UnitOfFreight_SelectedIndexChanged(object sender, EventArgs e)
    {
        objContractFreightDetailsPresenter.FillUnitFreightDropdown();
        SetLabelForUnitFreightDetails();
        objContractFreightDetailsPresenter.FillGrid();
    }
    protected void ddl_SubUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        objContractFreightDetailsPresenter.FillSubUnitFreightDropdown();
        SetLabelForSubUnitFreightDetails();
    }
    protected void dg_FreightDetails_EditCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        txt_SrNo = (TextBox)e.Item.FindControl("txt_SrNo");
        SrNo = GetSrNo_ForUnitAndSubUnitFreightDetails(e.Item.ItemIndex);
        lbtn_Delete = (LinkButton)e.Item.FindControl("lbtn_Delete");
        lbtn_Delete.Enabled = false;
        dg_FreightDetails.EditItemIndex = SrNo;
        dg_FreightDetails.ShowFooter = false;
        BindGrid();
        dg_FreightDetails.Columns[14].Visible = true;

        ddl_UnitFreight.Enabled = false;
        ddl_SubUnitFreight.Enabled = false;

    }
    protected void dg_FreightDetails_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        errorMessage = "";
        EnabledDisabledGridColumns();
        if (e.Item.ItemType == ListItemType.Header)
        {
            lbl_FreightHeading = (Label)e.Item.FindControl("lbl_FreightHeading");
            lbl_DetailsHeader = (Label)e.Item.FindControl("lbl_DetailsHeader");
            lbl_DetailsHeader.Text = "Details";
            if (UnitOfFreightID == 1)
            {
                lbl_FreightHeading.Text = "Freight/Vehicle";
            }
            else if (UnitOfFreightID == 2)
            {
                lbl_FreightHeading.Text = "Freight/" + CompanyManager.getCompanyParam().StandardFreightRatePer + "Kg";
            }
            else if (UnitOfFreightID == 3)
            {
                lbl_FreightHeading.Text = "Freight/CFT";
            }
            else if (UnitOfFreightID == 4)
            {
                lbl_FreightHeading.Text = "Freight/Article";
            }
            else if (UnitOfFreightID == 5)
            {
                lbl_FreightHeading.Text = "Freight/Km";
            }

        }

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_FromLocation = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_FromLocation"));
                ddl_ToLocation = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_ToLocation"));
                txt_ID = (TextBox)e.Item.FindControl("txt_ID");
                txt_SrNo = (TextBox)e.Item.FindControl("txt_SrNo");
                txt_FromKMS = (TextBox)e.Item.FindControl("txt_FromKMS");
                txt_ToKMS = (TextBox)e.Item.FindControl("txt_ToKMS");
                txt_FromDays = (TextBox)e.Item.FindControl("txt_FromDays");
                txt_ToDays = (TextBox)e.Item.FindControl("txt_ToDays");
                txt_KMS = (TextBox)e.Item.FindControl("txt_KMS");
                txt_TransitDays = (TextBox)e.Item.FindControl("txt_TransitDays");
                txt_Freight = (TextBox)e.Item.FindControl("txt_Freight");
                txt_FromID = (TextBox)e.Item.FindControl("txt_FromID");
                txt_ToID = (TextBox)e.Item.FindControl("txt_ToID");
                txt_RangeFrom = (TextBox)e.Item.FindControl("txt_RangeFrom");
                txt_RangeTo = (TextBox)e.Item.FindControl("txt_RangeTo");
            }
            if (e.Item.ItemType == ListItemType.EditItem)
            {
                objDS = SessionFreightDetailsGrid;
                DataTable dt = new DataTable();
                DataView dv = new DataView();

                dv = ObjCommon.Get_View_Table(SessionFreightDetailsGrid.Tables[0], "SrNo= '" + txt_SrNo.Text.Trim() + "'");
                dt = dv.ToTable();

                if (dt.Rows.Count > 0)
                {
                    DataRow DR = dt.Rows[0];
                    SetFromLocationID(DR["FromLocationName"].ToString(),DR["FromLocation"].ToString());
                    SetToLocationID(DR["ToLocationName"].ToString(), DR["ToLocation"].ToString());
                }
            }

            if (e.Item.ItemIndex != -1)
            {
                EnabledDisabledDropDown();
                string URL, OtherChargesID;
                txt_SrNo = (TextBox)e.Item.FindControl("txt_SrNo");
                txt_ID = (TextBox)e.Item.FindControl("txt_ID");
                OtherChargesID = txt_ID.Text;
                int MenuItemId = Common.GetMenuItemId();
                URL = "FrmContractChargeDetails.aspx?SrNo=" + ClassLibraryMVP.Util.EncryptString(OtherChargesID) + "&Mode=" + Request.QueryString["Mode"].ToString() + "&Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId);
                lbtn_Details = (LinkButton)e.Item.FindControl("lbtn_Details");
                lbtn_Details.Attributes.Add("onclick", "return OpenPopUp('" + URL + "')");
            }
        }
    }
    protected void dg_FreightDetails_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        dg_FreightDetails.EditItemIndex = -1;
        dg_FreightDetails.ShowFooter = true;
        BindGrid();
        dg_FreightDetails.Columns[14].Visible = true;
        ddl_UnitFreight.Enabled = true;
        ddl_SubUnitFreight.Enabled = true;
    }
    protected void dg_FreightDetails_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        try
        {
            objDS = SessionFreightDetailsGrid;
            Insert_Update_Dataset(source, e);
            if (isValid == true)
            {
                dg_FreightDetails.EditItemIndex = -1;
                dg_FreightDetails.ShowFooter = true;
                BindGrid();
            }
        }
        catch (ConstraintException)
        {
            lbl_Errors.Visible = true;
            lbl_Errors.Text = "Duplicate Record";
            scm_ContractFreightDetails.SetFocus(ddl_FromLocation);
            return;
        }

        ddl_UnitFreight.Enabled = true;
        ddl_SubUnitFreight.Enabled = true;
    }
    protected void dg_FreightDetails_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        if (e.CommandName == "ADD")
        {
            objDS = SessionFreightDetailsGrid;
            try
            {
                Insert_Update_Dataset(source, e);
                if (isValid == true)
                {
                    BindGrid();
                    dg_FreightDetails.EditItemIndex = -1;
                    dg_FreightDetails.ShowFooter = true;
                    dg_FreightDetails.Columns[14].Visible = true;
                }
            }
            catch (ConstraintException)
            {
                lbl_Errors.Visible = true;
                lbl_Errors.Text = "Duplicate Record";
                scm_ContractFreightDetails.SetFocus(ddl_FromLocation);
                return;
            }
        }
    }
    protected void dg_FreightDetails_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        txt_SrNo = (TextBox)e.Item.FindControl("txt_SrNo");
        objDS = SessionFreightDetailsGrid;
        DataRow dr = null;
        SrNo = Util.String2Int(txt_SrNo.Text);
        dr = objDS.Tables[0].Rows.Find(SrNo);
        if (e.Item.ItemIndex != -1)
        {
            objDS = SessionFreightDetailsGrid;
            objDS.Tables[0].Rows.Remove(dr);
            objDS.Tables[0].AcceptChanges();
            SessionFreightDetailsGrid = objDS;
            dg_FreightDetails.EditItemIndex = -1;
            dg_FreightDetails.ShowFooter = true;
            BindGrid();
            dg_FreightDetails.Columns[14].Visible = true;
        }
        EnableDropDown();
    }
    protected void ddl_UnitFreight_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();

    }
    protected void ddl_SubUnitFreight_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void ddl_FreightBasis_SelectedIndexChanged(object sender, EventArgs e)
    {
        objContractFreightDetailsPresenter.FillGrid();
    }

    private void EnableDropDown()
    {

        if (SessionFreightDetailsGrid.Tables[0].Rows.Count <= 0)
        {
            ddl_FreightBasis.Enabled = true;
            ddl_UnitOfFreight.Enabled = true;
            ddl_SubUnit.Enabled = true;
        }
        else
        {
            ddl_FreightBasis.Enabled = false;
            ddl_UnitOfFreight.Enabled = false;
            ddl_SubUnit.Enabled = false;
        }
    }

    #endregion

}
