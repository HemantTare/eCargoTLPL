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
using ClassLibraryMVP.General;
using ClassLibraryMVP.Security;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using Raj.EC.UserDesk;


public partial class Reports_wuc_eWayBillVerification : System.Web.UI.UserControl
{ 
    Common CommonObj = new Common();
    UserDesk UserDeskObj = new UserDesk();
    public DataSet ds_eWayBills_details = null;
    string Type;

    protected void Page_Load(object sender, EventArgs e)
    {
        
                       
            Btn_Save_attributes();

            if (!IsPostBack)
            {
                Session.Remove("ds_eWayBills_details");
                Wuc_From_To_Datepicker1.SelectedFromDate = DateTime.Now;
                Wuc_From_To_Datepicker1.SelectedToDate = DateTime.Now;

                Get_Details();
                FillCombo();
               
            } 
    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        lbl_errors.Visible = false;
        lbl_errors.Text = "";
        Get_Details();
    }
    
    private void Btn_Save_attributes()
    {
        System.Text.StringBuilder sbValid = new System.Text.StringBuilder();
        sbValid.Append("if (typeof(Page_ClientValidate) == 'function'){");
        sbValid.Append("if (Page_ClientValidate() == false) { return false; }}");
        sbValid.Append("this.value = 'Wait...';");
        sbValid.Append("this.disabled = true;");
        sbValid.Append(Page.ClientScript.GetPostBackEventReference(Btn_Save, ""));
        sbValid.Append(";");

        Btn_Save.Attributes.Add("onclick", sbValid.ToString());
    }

    private void Get_Details()
    {
        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;

        int Region_Id = Wuc_Region_Area_Branch1.RegionID;
        int Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;

        int _mainID = UserManager.getUserParam().MainId;
        string _hierarchyCode = UserManager.getUserParam().HierarchyCode;

        if (Branch_id == 0 && Area_id == 0 && Region_Id == 0)
        {

            if (_hierarchyCode == "BO")
            {
                Branch_id = _mainID;
            }
            else if (_hierarchyCode == "AO")
            {
                Area_id = _mainID;
            }
            else if (_hierarchyCode == "RO")
            {
                Region_Id = _mainID;
            }
        }

        ds_eWayBills_details = UserDeskObj.Get_eWayBills(Region_Id,Area_id,Branch_id,From_Date,To_date);
        datagrid1.DataSource = ds_eWayBills_details;
        datagrid1.DataBind();
        Session["ds_eWayBills_details"] = ds_eWayBills_details;
    }

    protected void datagrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            
            CheckBox chk_verified = (CheckBox)e.Item.FindControl("Chk_Verified");
            LinkButton lbtn_MultipleEWayBill = (LinkButton)e.Item.FindControl("lbtn_MultipleEWayBill");

            int ds_row = (datagrid1.PageSize * datagrid1.CurrentPageIndex) + e.Item.ItemIndex;
            string GC_id = ds_eWayBills_details.Tables[0].Rows[ds_row]["GC_ID"].ToString();
            GC_id = ClassLibrary.crypt.EncryptInteger(Convert.ToInt32(GC_id));

            Boolean Is_Multiple_eWayBill = Convert.ToBoolean(ds_eWayBills_details.Tables[0].Rows[ds_row]["Is_Multiple_eWayBill"].ToString());
            Boolean Is_Multiple_eWayBills_Entered = Convert.ToBoolean(ds_eWayBills_details.Tables[0].Rows[ds_row]["Is_Multiple_eWayBills_Entered"].ToString());

            string Chk_Verified = ds_eWayBills_details.Tables[0].Rows[ds_row]["IseWayBillVerified"].ToString();

            if (Chk_Verified == "True")
            {
                chk_verified.Enabled = false;
                
                e.Item.BackColor = System.Drawing.Color.Lime;

            }
            else if (Is_Multiple_eWayBill == true && Is_Multiple_eWayBills_Entered == false)
            {
                chk_verified.Enabled = false;
                e.Item.BackColor = System.Drawing.Color.Yellow;
            
            }
            else if (Is_Multiple_eWayBill == true && Is_Multiple_eWayBills_Entered == true)
            {
                chk_verified.Enabled = true;
                e.Item.BackColor = System.Drawing.Color.Pink;

            }

            if (Is_Multiple_eWayBill == true && Is_Multiple_eWayBills_Entered == false )
            {
                lbtn_MultipleEWayBill.Enabled = true;
            }
            else
            {
                lbtn_MultipleEWayBill.Enabled = false;
            }


            LinkButton lnk_IsDuplicate = (LinkButton)e.Item.FindControl("lnk_IsDuplicate");
            int eWayBillNoCount = Util.String2Int(ds_eWayBills_details.Tables[0].Rows[ds_row]["eWayBillNoCount"].ToString());

            if (eWayBillNoCount > 1)
            {
                lnk_IsDuplicate.Text = "Yes";
                e.Item.BackColor = System.Drawing.Color.OrangeRed;
            }

        }

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int GC_ID, ReasonID;
            LinkButton lnk_GC_No, lnk_MultipleEWayBill, lnk_Pending_Reason, lnk_eWayBillNo, lnk_IsDuplicate, lnk_PartBUpdated;

            String eWayBillNo;

            int ds_row = (datagrid1.PageSize * datagrid1.CurrentPageIndex) + e.Item.ItemIndex;
            Boolean Is_Multiple_eWayBill = Convert.ToBoolean(ds_eWayBills_details.Tables[0].Rows[ds_row]["Is_Multiple_eWayBill"].ToString());
            Boolean Is_Multiple_eWayBills_Entered = Convert.ToBoolean(ds_eWayBills_details.Tables[0].Rows[ds_row]["Is_Multiple_eWayBills_Entered"].ToString());

            GC_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "GC_ID").ToString());

            eWayBillNo = DataBinder.Eval(e.Item.DataItem, "eWayBillNo").ToString();
            ReasonID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "UnVerifiedReasonId").ToString());

            lnk_GC_No = (LinkButton)e.Item.FindControl("lnk_GC_No");
            lnk_MultipleEWayBill = (LinkButton)e.Item.FindControl("lbtn_MultipleEWayBill");

            lnk_Pending_Reason = (LinkButton)e.Item.FindControl("lnk_Pending_Reason");

            lnk_eWayBillNo = (LinkButton)e.Item.FindControl("lnk_eWayBillNo");

            lnk_PartBUpdated = (LinkButton)e.Item.FindControl("lnk_PartBUpdated");

            lnk_GC_No.Attributes.Add("onclick", "return viewwindow_general('" + ClassLibraryMVP.Util.EncryptInteger(GC_ID) + "'),'" + lnk_GC_No.Text + "'");


            if (Is_Multiple_eWayBill == true && Is_Multiple_eWayBills_Entered == false)
            {
                lnk_MultipleEWayBill.Attributes.Add("onclick", "return viewwindow_UpdateMultipleeWayBills('" + ClassLibraryMVP.Util.EncryptInteger(GC_ID) + "','" + ClassLibraryMVP.Util.EncryptString(lnk_GC_No.Text) + "','" + ClassLibraryMVP.Util.EncryptString(lnk_eWayBillNo.Text) + "')");
            }

            CheckBox chk_verified = (CheckBox)e.Item.FindControl("Chk_Verified");

            HiddenField hdn_PartBUpdated = (HiddenField)e.Item.FindControl("hdn_PartBUpdated");

            if (chk_verified.Checked == false)
            {

                lnk_Pending_Reason.Attributes.Add("onclick", "return viewwindow_PendingReason('" + GC_ID + "','" + lnk_GC_No.Text + "','" + eWayBillNo + "','" + ReasonID + "')");

                if (UserManager.getUserParam().UserName.ToLower() == "ho0003" || UserManager.getUserParam().UserName.ToLower() == "ho0016" || UserManager.getUserParam().UserName.ToLower() == "ad0001" || UserManager.getUserParam().UserName.ToLower() == "ho0002")
                {
                    lnk_eWayBillNo.Attributes.Add("onclick", "return viewwindow_eWayBillUpdate('" + GC_ID + "','" + lnk_GC_No.Text + "','" + eWayBillNo + "')");
                }

                lnk_PartBUpdated.Attributes.Add("onclick", "return viewwindow_PartBUpdate('" + GC_ID + "','" + lnk_GC_No.Text + "','" + eWayBillNo + "','" + hdn_PartBUpdated.ClientID + "','" + chk_verified.ClientID + "');");

            }
            else
            {
                lnk_Pending_Reason.Enabled = false;
            }
            
            lnk_IsDuplicate = (LinkButton)e.Item.FindControl("lnk_IsDuplicate");
            int eWayBillNoCount = Util.String2Int(ds_eWayBills_details.Tables[0].Rows[ds_row]["eWayBillNoCount"].ToString());

            if (eWayBillNoCount > 1)
            {
                lnk_IsDuplicate.Attributes.Add("onclick", "return viewwindow_Duplicate_eWayBill('" + ClassLibraryMVP.Util.EncryptString(eWayBillNo) + "')");
            }

            ImageButton btnCopy;
            btnCopy = (ImageButton)e.Item.FindControl("btnCopy");
            btnCopy.Attributes.Add("onclick", "return copyToClipboard('" + (lnk_eWayBillNo.Text) + "');");

            TextBox txt_Distance = (TextBox)e.Item.FindControl("txt_Distance");

            chk_verified.Attributes.Add("onclick", "checkuncheckIsVerify('" + chk_verified.ClientID + "','" + txt_Distance.ClientID + "');");



        }

    }

    protected void datagrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        ds_eWayBills_details = (DataSet)Session["ds_eWayBills_details"];
        Update_Ds_eWayBills_Details();
        datagrid1.CurrentPageIndex = e.NewPageIndex;
        datagrid1.DataSource = ds_eWayBills_details;
        datagrid1.DataBind();
    }

    private void Update_Ds_eWayBills_Details()
    {
        ds_eWayBills_details = (DataSet)Session["ds_eWayBills_details"];
        int ds_row = 0;

        int i = 0;
        CheckBox Chk_Verified = default(CheckBox);
        TextBox txt_Distance = default(TextBox);
        LinkButton lnk_PartBUpdated = default(LinkButton);
        HiddenField hdn_PartBUpdated = default(HiddenField);

        for (i = 0; i <= datagrid1.Items.Count - 1; i++)
        {
            ds_row = (datagrid1.PageSize * datagrid1.CurrentPageIndex) + i;
            Chk_Verified = (CheckBox)datagrid1.Items[i].Cells[7].FindControl("Chk_Verified");

            txt_Distance = (TextBox)datagrid1.Items[i].Cells[8].FindControl("txt_Distance");

            lnk_PartBUpdated = (LinkButton)datagrid1.Items[i].Cells[9].FindControl("lnk_PartBUpdated");

            hdn_PartBUpdated = (HiddenField)datagrid1.Items[i].Cells[9].FindControl("hdn_PartBUpdated");

            ds_eWayBills_details.Tables[0].Rows[ds_row]["IseWayBillVerified"] = Convert.ToBoolean(Chk_Verified.Checked);
            ds_eWayBills_details.Tables[0].Rows[ds_row]["Distance"] = Convert.ToInt32(txt_Distance.Text == string.Empty ? "0" : txt_Distance.Text);

            if (hdn_PartBUpdated.Value.ToLower() != "no")
            {
                ds_eWayBills_details.Tables[0].Rows[ds_row]["eWayBillValidUpTo"] = hdn_PartBUpdated.Value;
            }

        }

        Session["ds_eWayBills_details"] = ds_eWayBills_details;
    }


    private void Update_Ds_eWayBills_Details_Save()
    {
        ds_eWayBills_details = (DataSet)Session["ds_eWayBills_details"];
        int ds_row = 0;

        int i = 0;
        CheckBox Chk_Verified = default(CheckBox);
        TextBox txt_Distance = default(TextBox);
        LinkButton lnk_PartBUpdated = default(LinkButton);
        HiddenField hdn_PartBUpdated = default(HiddenField);

        for (i = 0; i <= datagrid1.Items.Count - 1; i++)
        {
            ds_row = (datagrid1.PageSize * datagrid1.CurrentPageIndex) + i;
            Chk_Verified = (CheckBox)datagrid1.Items[i].Cells[7].FindControl("Chk_Verified");

            txt_Distance = (TextBox)datagrid1.Items[i].Cells[8].FindControl("txt_Distance");

            lnk_PartBUpdated = (LinkButton)datagrid1.Items[i].Cells[9].FindControl("lnk_PartBUpdated");

            hdn_PartBUpdated = (HiddenField)datagrid1.Items[i].Cells[9].FindControl("hdn_PartBUpdated");

            ds_eWayBills_details.Tables[0].Rows[ds_row]["IseWayBillVerified"] = Convert.ToBoolean(Chk_Verified.Checked);
            ds_eWayBills_details.Tables[0].Rows[ds_row]["Distance"] = Convert.ToInt32(txt_Distance.Text == string.Empty ? "0" : txt_Distance.Text);

            if (hdn_PartBUpdated.Value.ToLower() != "no")
            {
                ds_eWayBills_details.Tables[0].Rows[ds_row]["IsPartBUpdated"] = "true";
                ds_eWayBills_details.Tables[0].Rows[ds_row]["eWayBillValidUpTo"] = hdn_PartBUpdated.Value;
            }
            else
            {
                ds_eWayBills_details.Tables[0].Rows[ds_row]["IsPartBUpdated"] = "false";
            }

        }


        ds_row = 0;
        i = 0;

        for (i = 0; i <= ds_eWayBills_details.Tables[0].Rows.Count- 1; i++)
        {

            if (ds_eWayBills_details.Tables[0].Rows[ds_row]["IsPartBUpdated"].ToString().ToLower() == "false")
            {
                ds_eWayBills_details.Tables[0].Rows[ds_row]["IsPartBUpdated"] = "false";
            }
            else
            {
                if (ds_eWayBills_details.Tables[0].Rows[ds_row]["IseWayBillVerified"].ToString().ToLower() == "true")
                {
                    ds_eWayBills_details.Tables[0].Rows[ds_row]["IsPartBUpdated"] = "true";
                }
                else
                {
                    ds_eWayBills_details.Tables[0].Rows[ds_row]["IsPartBUpdated"] = "false";
                }
            }

            ds_row = ds_row + 1;
        }


        Session["ds_eWayBills_details"] = ds_eWayBills_details;
    }


    protected void datagrid1_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
    {
        if (sortCriteria == e.SortExpression)
        {
            if (sortDir == "Desc")
            {
                sortDir = "Asc";
            }
            else
            {
                sortDir = "Desc";
            } 
        } 
        // Assign the column clicked to the sortCriteria property    
        sortCriteria = e.SortExpression; 
        MakeDataView(); 
    } 

    protected void Btn_Save_Click(object sender, System.EventArgs e)
    {
        //Update_Ds_eWayBills_Details();
        Update_Ds_eWayBills_Details_Save();

        if (Allow_To_Save() == true)
        {
            Save();
            Response.Redirect("~/Display/CloseForm.aspx");
        }
    }

    private void Save()
    {
        int UserID = UserManager.getUserParam().UserId;
        string xml = null;
        ds_eWayBills_details = (DataSet)Session["ds_eWayBills_details"];
        xml = ds_eWayBills_details.GetXml();

        UserDeskObj.EditeWayBill(xml, UserID);
    }


    private bool grid_validation()
    {
        int i;
        bool ATS = true;

        TextBox txt_Distance;
        CheckBox Chk_Verified ;
        LinkButton lnk_PartBUpdated;
        HiddenField hdn_PartBUpdated;

        if (datagrid1.Items.Count > 0)
        {
            for (i = 0; i <= datagrid1.Items.Count - 1; i++)
            {
                txt_Distance = (TextBox)datagrid1.Items[i].FindControl("txt_Distance");
                Chk_Verified = (CheckBox)datagrid1.Items[i].FindControl("Chk_Verified");
                lnk_PartBUpdated = (LinkButton)datagrid1.Items[i].FindControl("lnk_PartBUpdated");

                hdn_PartBUpdated = (HiddenField)datagrid1.Items[i].FindControl("hdn_PartBUpdated");
                lnk_PartBUpdated.Text = hdn_PartBUpdated.Value;

                if (Chk_Verified.Checked == true && lnk_PartBUpdated.Text == "No" &&  Util.String2Int(txt_Distance.Text.Trim()) <= 0 )
                {
                    txt_Distance.Enabled = true;
                    lbl_errors.Visible = true;
                    lbl_errors.Text = "Please Enter Distance";
                    ScriptManager.SetFocus(txt_Distance);
                    ATS = false;
                    break;
                }
                else
                {
                    ATS = true;
                }
            }
        }

        for (i = 0; i <= datagrid1.Items.Count - 1; i++)
        {
            txt_Distance = (TextBox)datagrid1.Items[i].FindControl("txt_Distance");
            Chk_Verified = (CheckBox)datagrid1.Items[i].FindControl("Chk_Verified");

            if (Chk_Verified.Checked == true)
            {
                txt_Distance.Enabled = true;
            }
            else
            {
                txt_Distance.Enabled = false;
                txt_Distance.Text = "0";
            }
        }

        return ATS;
    }

    private bool Allow_To_Save()
    {
        bool functionReturnValue = false;
        ds_eWayBills_details = (DataSet)Session["ds_eWayBills_details"];

        if (ds_eWayBills_details != null)
        {
            DataView view = default(DataView);
            view = CommonObj.Get_View_Table(ds_eWayBills_details.Tables[0], "IseWayBillVerified = 1");
            if (view.Count > 0)
            {
                lbl_errors.Visible = false;
                functionReturnValue = true;
            }
            else
            {
                lbl_errors.Visible = true;
                lbl_errors.Text = "No eWay Bill Selected";
            }
        }
        else
        {
            lbl_errors.Visible = true;
            lbl_errors.Text = "No eWay Bill Selected";
        }

        if (grid_validation() == false)
        {
            functionReturnValue = false;
        }
        
        
        return functionReturnValue;
    }
     
    private void FillCombo()
    {
        int i = 0;
        DataSet DS1 = null;
        DataSet DS2 = new DataSet();
        DataRow DRow = default(DataRow);

        DS1 = (DataSet)Session["ds_eWayBills_details"];


        DS2.Tables.Add("ComboTable");
        DS2.Tables[0].Columns.Add("Col_ID");
        DS2.Tables[0].Columns.Add("Col_Name");


        for (i = 0; i <= 4; i++)
        {
            DRow = DS2.Tables[0].NewRow();
            if (i == 0)
            {
                DRow["Col_Id"] = "LRNo";
                DRow["Col_Name"] = "LR No";

            }
            else if (i == 1)
            {
                DRow["Col_Id"] = "eWayBillNo";
                DRow["Col_Name"] = "eWay Bill No";
            }

            DS2.Tables[0].Rows.Add(DRow);
        }

        ddl_Serach.DataSource = DS2;
        ddl_Serach.DataTextField = "Col_Name";
        ddl_Serach.DataValueField = "Col_ID";
        ddl_Serach.DataBind();

    }

    private void MakeDataView()
    {
        ds_eWayBills_details = (DataSet)Session["ds_eWayBills_details"];

        DataView view = new DataView();
        System.Type Datatype;

        view.Table = ds_eWayBills_details.Tables[0];
        view.AllowNew = true;

        string k = null;
        k = Convert.ToString(txt_Search.Text);

        Datatype = view.Table.Columns[ddl_Serach.SelectedValue].DataType;


        //if (Datatype.Name == "Integer" || Datatype.Name == "datetime")
        if (CommonObj.IsNumeric(k))
        {
            view.RowFilter = ddl_Serach.SelectedValue + " = '" + k + "'";
        }
        else
        {
            view.RowFilter = ddl_Serach.SelectedValue + " Like '" + k + "*'";
        }

        view.RowStateFilter = DataViewRowState.CurrentRows; 
        view.Sort = sortCriteria + " " + sortDir;

        DataTable newTable = view.ToTable();

        ds_eWayBills_details.Tables.Clear();
        ds_eWayBills_details.Tables.Add(newTable);
        Session["ds_eWayBills_details"] = ds_eWayBills_details; 
        datagrid1.DataSource = view;
        datagrid1.DataBind();

        // Simple-bind to a TextBox control
        //'Text1.DataBindings.Add("Text", view, "CompanyName")
    }  

    protected void btn_Search_Click(object sender, System.EventArgs e)
    {
        datagrid1.CurrentPageIndex = 0;
        Search();
    }

    private void Search()
    {
        if (!string.IsNullOrEmpty(txt_Search.Text))
        {
            MakeDataView();
        }
        else
        {
            Get_Details();
        }
    }

    protected void txt_Search_TextChanged(object sender, System.EventArgs e)
    {
        datagrid1.CurrentPageIndex = 0;
        Search();
    }
  
    private string sortCriteria
    {
        get { return Convert.ToString(ViewState["sortCriteria"]); }
        set { ViewState["sortCriteria"] = value; }
    }

    // Holds the direction to be sorted...
    private string sortDir
    {
        get { return Convert.ToString(ViewState["sortDir"]); }
        set { ViewState["sortDir"] = value; }
    } 


}
