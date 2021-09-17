
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
using ClassLibraryMVP.General;
using Raj.CRM.TransactionView;
using Raj.CRM.TransactionPresenter;
using ClassLibraryMVP;
using Raj.EC;
using Infragistics.WebUI.UltraWebGrid;

public partial class CRM_wuc_Complaint_Assignment : System.Web.UI.UserControl, IComplaint_AssignmentView
{
    private Complaint_AssignmentPresenter objComplaint_AssignmentPresenter;
    bool isValid = false;
 
    #region IView Members

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public int keyID
    {
        get
        {
           return Util.DecryptToInt(Request.QueryString["Ticket_Id"]);
        }
    }

    #endregion

    #region InitInterface

    public DataTable SetHeadingCaption
    {
        set
        {
            if (value.Rows.Count!=0)
            { 
                DataRow Dr=value.Rows[0];
                lbl_TicketNo.Text = Dr["Ticket_No"].ToString();
                lbl_GcNoCaption.Text = CompanyManager.getCompanyParam().GcCaption + " No :";
                lbl_GcNo.Text = Dr["GC_No"].ToString();               
            }
        }
    }
    public string SearchById
    {
        set { ddl_SearchBy.SelectedValue = value; }
        get { return ddl_SearchBy.SelectedValue; }
    }

    public int FilterById
    {
        get
        {
            if (SearchById == "U" || SearchById == "E")
                return Util.String2Int(ddl_FilterBy2.SelectedValue);
            else
                return Util.String2Int(ddl_FilterBy1.SelectedValue);
        }
    }

    public string XML_User
    {
        get 
            {
                if (StateManager.IsValidSession("AssignUser"))
                {
                    DataSet objDs = new DataSet();
                    objDs.Tables.Add(GetSelectedUsers());
                    return objDs.GetXml();
                }
                else 
                {
                    return "<NewDataSet></NewDataSet>";
                }
            }
    }

    public string XMLAllUser
    {
        get
        {
            if (StateManager.IsValidSession("AssignUser"))
            {
                DataSet objDs = new DataSet();
                objDs.Tables.Add(Session_Ds_User.Copy());
                objDs.Tables[0].TableName = "Table";
                return objDs.GetXml();
            }
            else
            {
                return "<NewDataSet></NewDataSet>";
            }
        }
    }

    public DataTable Session_Ds_User 
    {
        get { return StateManager.GetState<DataTable>("AssignUser"); }
        set { StateManager.SaveState("AssignUser", value); }
    }

    private DataTable Session_CheckSelection
    {
        get { return StateManager.GetState<DataTable>("CheckSelection"); }
        set { StateManager.SaveState("CheckSelection", value); }
    }
 
    public DataTable Bind_dg_AssginUser
    {
        set
        {
            dg_AssignUser.DataSource = value;
            dg_AssignUser.DataBind();
            Session_Ds_User = value;
        }
    }

    public DataTable Bind_ddl_FilterBy1
    {
        set
        {
            ddl_FilterBy1.DataTextField = "Name";
            ddl_FilterBy1.DataValueField = "Id";
            ddl_FilterBy1.DataSource = value;
            ddl_FilterBy1.DataBind();
        }
    }

    #endregion
     
    #region Function
    
    public bool validateUI()
    {
        bool _isValid = false;

        if (dg_AssignUser.Rows.Count == 0)
        {
            errorMessage = "Please Select Atleast One Users";
        }
        else if (GetSelectedUsers().Rows.Count == 0)
        {
            errorMessage = "Please Select Atleast One Users";
        }
        else 
        {
            _isValid = true;
        }
         return _isValid;
    }

 
    private  DataTable GetSelectedUsers()
    {
       DataRow  dr ;
       DataTable dt_temp=new DataTable();
       dt_temp.Columns.Add("ID");
       foreach (UltraGridRow row1 in dg_AssignUser.Rows)
        {
            if ( Convert.ToBoolean( row1.Cells[0].Value )== true)
            {
               dr = dt_temp.NewRow();
               dr[0] = row1.Cells[1].Value;
               dt_temp.Rows.Add(dr);
            }
        }
        dt_temp.AcceptChanges();
        dt_temp.TableName = "Table";
        return  dt_temp;
    }

   
    #endregion
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddl_FilterBy2.DataTextField = "Name";
            ddl_FilterBy2.DataValueField = "Id";

            DataTable Dt = new DataTable();

            Dt.Columns.Add("SearchById");
            Dt.Columns.Add("FiletrById");
            Raj.EC.Common.SetPrimaryKeys(new string[] { "SearchById", "FiletrById" }, Dt);
            Session_CheckSelection = Dt;

           
            dg_AssignUser.Width = 700;

          
            ddl_FilterBy2.OtherColumns = SearchById;
            tr_FilterBy2.Visible = true;
            tr_FilterBy1.Visible = false;
            lbl_CaptionFilterBy2.Text = ddl_SearchBy.SelectedItem.Text + " :";
        }

   
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EC.CRM.CallBack));
        objComplaint_AssignmentPresenter = new Complaint_AssignmentPresenter(this, IsPostBack);

        dg_AssignUser.Columns[1].Hidden = true;
        dg_AssignUser.Columns[2].Hidden = true;
        dg_AssignUser.Columns[7].Hidden = true;

        if (!IsPostBack)
        {
          SetChecked();

          StateManager.SaveState("Attachments", null);
          btn_Attachments.Attributes.Add("onclick", " return OpenWindow('" + Util.GetBaseURL() + "/CRM/Transaction/FrmAttachments.aspx?Id=" + Util.EncryptInteger(keyID) + "&Type=" + Util.EncryptString("Complaint") + "')");
        }

    }
 
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            objComplaint_AssignmentPresenter.Save();
        }
    }

 
    protected void ddl_SearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (SearchById == "U" || SearchById == "E")
        {
            Raj.EC.Common.SetValueToDDLSearch("", "-1", ddl_FilterBy2);
            ddl_FilterBy2.OtherColumns = SearchById;
            tr_FilterBy2.Visible =true;
            tr_FilterBy1.Visible = false;
            lbl_CaptionFilterBy2.Text = ddl_SearchBy.SelectedItem.Text + " :";
        }
        else
        {

            if (SearchById == "H")
            {
                ddl_FilterBy1.Items.Clear();
                ddl_FilterBy1.Items.Add(new ListItem("All HO Users","1"));
            }
            else 
            {
                objComplaint_AssignmentPresenter.FillOnSearchByChanged();
            }
            tr_FilterBy2.Visible = false;
            tr_FilterBy1.Visible = true;
            lbl_CaptionFilterBy1.Text = ddl_SearchBy.SelectedItem.Text + " :";
        }
    }
     
   
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        if (validateBeforeAdd())
        {
            AddInGrid();
        }
    }

    private bool validateBeforeAdd()
    {
        DataRow Dr = Session_CheckSelection.NewRow();
        Dr["SearchById"] = SearchById;
        Dr["FiletrById"] = FilterById.ToString();

        try
        {
            Session_CheckSelection.Rows.Add(Dr);
        }
        catch (DataException ex)
        {
            return false;
        }

        if (FilterById<= 0)
        {
            errorMessage = "Pease Select "+ddl_SearchBy.SelectedItem.Text;
        }
        return true;
    }

    private void AddInGrid()
    {
        DataSet objDS;
        if (StateManager.IsValidSession("AssignUser"))
        {
            objDS = objComplaint_AssignmentPresenter.FillOnAddClick();
            objDS.Tables[0].Merge(Session_Ds_User);
            //Session_Ds_User.Merge(objDS.Tables[0]);
            Session_Ds_User = objDS.Tables[0];
            Session_Ds_User.AcceptChanges();
            Bind_dg_AssginUser = Session_Ds_User;
        }
        else
        {
            objDS = objComplaint_AssignmentPresenter.FillOnAddClick();
            Bind_dg_AssginUser = objDS.Tables[0];
        }

        SetChecked();

    }

    private void SetChecked()
    {
        int i = 0;
       foreach (UltraGridRow row1 in dg_AssignUser.Rows)
        {
           row1.Cells[0].Value=Convert.ToInt32(Session_Ds_User.Rows[i]["Selected"])>0?true:false;
            i++;
        }
    }
}
