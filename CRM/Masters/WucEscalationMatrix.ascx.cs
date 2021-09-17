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
using Raj.CRM.MasterView;
using Raj.CRM.MasterPresenter;
using System.Data.SqlClient;

public partial class CRM_Masters_WucEscalationMatrix : System.Web.UI.UserControl,IEscalationMatrixView
{
    #region ClassVariables
    DataSet objDS;
    EscalationMatrixPresenter objEscalationMatrixPresenter;   
    DropDownList ddl_TimeFrom;
    DropDownList ddl_TimeTo;
    DropDownList ddl_Profile;
    LinkButton lnk_Profile;
   
    #endregion

    #region ControlBind
    public DataSet SessionEscalationMatrixGrid
    {              
        get { return StateManager.GetState<DataSet>("EscalationMatrixGrid"); }
        set { StateManager.SaveState("EscalationMatrixGrid", value); }
    }
    private void BindGrid()
    {
        dgEscalationMatrix.DataSource = SessionEscalationMatrixGrid;
        dgEscalationMatrix.DataBind();       
    }
    public DataTable BindComplaintNature
    {
        set
        {
            ddl_ComplaintNature.DataSource = value;
            ddl_ComplaintNature.DataTextField = "Complaint_Nature";
            ddl_ComplaintNature.DataValueField = "Complaint_Nature_ID";
            ddl_ComplaintNature.DataBind();
            ddl_ComplaintNature.Items.Insert(0, new ListItem("Select One", "0"));            
        }
    }      

    public DataSet SessionProfile
    {
        set
        {
            StateManager.SaveState("Profile", value);
        }
        get
        {
            return StateManager.GetState<DataSet>("Profile");
        }
    }   
    #endregion

    #region ControlValues

    public int ComplaintNatureId
    {
        get { return Util.String2Int(ddl_ComplaintNature.SelectedValue); }
        set { ddl_ComplaintNature.SelectedValue = Util.Int2String(value); }
    }
   
    public DataSet SessionUserDetails
    {
        get { return StateManager.GetState<DataSet>("UserDetails"); }
        set { StateManager.SaveState("UserDetails", value); }
    }
    #endregion

    #region IView
    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    } 

    public bool validateUI()
    {
        bool _isValid = false;
        objDS = SessionEscalationMatrixGrid;
        errorMessage = "";

        if (ddl_ComplaintNature.SelectedValue == "0")
        {
           lbl_Errors.Text = "Please Select Complaint Nature";
            ddl_ComplaintNature.Focus();            
        }
        else if (ComplaintNatureId >= 0 && objDS.Tables[0].Rows.Count <= 0)
        {            
            errorMessage = "Please Select At Least One Profile";
        }
        else if (SessionUserDetails.Tables[0].Rows.Count <= 0)
        {
            errorMessage = "Please Select AtLeast one User";
        }
        else
        {
            _isValid = true;
        }
        return _isValid;
    }
    
    #endregion

    #region GridEvents
    protected void dgEscalationMatrix_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_Profile = (DropDownList)(e.Item.FindControl("ddl_Profile"));
                ddl_TimeFrom = (DropDownList)(e.Item.FindControl("ddl_TimeFrom"));
                ddl_TimeTo = (DropDownList)(e.Item.FindControl("ddl_TimeTo"));                               
            }

            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
               
                ddl_Profile.DataSource = SessionProfile;
                ddl_Profile.DataTextField = "Profile_Name";
                ddl_Profile.DataValueField = "Profile_Id";
                ddl_Profile.DataBind();
                ddl_Profile.Items.Insert(0, new ListItem("Select One", "0"));
                MakeDT();
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                LinkButton lnklbtn;
                lnklbtn = (LinkButton)e.Item.FindControl("lnk_Delete");
               
                DataRow DR = null;
               
                     DataTable dt = SessionEscalationMatrixGrid.Tables[0];
                     DR = dt.Rows[e.Item.ItemIndex];

                     ddl_Profile.SelectedValue = DR["Profile_Id"].ToString();
                     ddl_TimeFrom.SelectedValue = DR["Time_From"].ToString();
                     ddl_TimeTo.SelectedValue = DR["Time_To"].ToString();
                    
                     //dgEscalationMatrix.Items[dgEscalationMatrix.EditItemIndex].Cells[5].Enabled = false;
                     dgEscalationMatrix.Columns[5].Visible = false;
                      //lnklbtn.Enabled = false;                    
            }                  
        }
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lbl_ProfileId;          
            ComplaintNatureId = Util.String2Int(ddl_ComplaintNature.SelectedValue);
            int Profile_Id = Convert.ToInt32(((Label)e.Item.Cells[0].FindControl("lbl_ProfileId")).Text);
            lnk_Profile = ((LinkButton)e.Item.Cells[1].FindControl("lnk_Profile"));
            lnk_Profile.Attributes.Add("onclick", "return OpenProfile('" + ClassLibraryMVP.Util.EncryptInteger(Profile_Id) + "','" + ClassLibraryMVP.Util.EncryptInteger(ComplaintNatureId) + "')");
        }
    }
  
    protected void dgEscalationMatrix_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add" || e.CommandName == "Update")
        {
            objDS = SessionEscalationMatrixGrid;
           
            try
            {                
                DataColumn[] _dataColumPrimaryKey;
                _dataColumPrimaryKey = new DataColumn[1];
                _dataColumPrimaryKey[0] = objDS.Tables[0].Columns["Profile_ID"];
                objDS.Tables[0].PrimaryKey = _dataColumPrimaryKey;
                InsertAndUpdateGrid(source, e);                
            }
            catch (ConstraintException ex)
            {
                errorMessage = "Duplicate Profile";
            }           
        }        
    }
    protected void dgEscalationMatrix_EditCommand(object source, DataGridCommandEventArgs e)
    {
        LinkButton lnklbtn;
        lnklbtn = (LinkButton)e.Item.FindControl("lnk_Delete");
        dgEscalationMatrix.EditItemIndex = e.Item.ItemIndex;
        ddl_ComplaintNature.Enabled = false;
        dgEscalationMatrix.ShowFooter = false;
        BindGrid(); 
        dgEscalationMatrix.Columns[5].Visible = false;
    }
    protected void dgEscalationMatrix_DeleteCommand(object source, DataGridCommandEventArgs e)
    {        
        objDS = SessionEscalationMatrixGrid;
        objDS.Tables[0].Rows[e.Item.ItemIndex].Delete();
        objDS.AcceptChanges();
        SessionEscalationMatrixGrid = objDS;
        BindGrid();
        dgEscalationMatrix.ShowFooter = true;
        dgEscalationMatrix.Columns[5].Visible = true;
    }
    protected void dgEscalationMatrix_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgEscalationMatrix.EditItemIndex = -1;
        ddl_ComplaintNature.Enabled = true;
        BindGrid();
        dgEscalationMatrix.ShowFooter = true;
        dgEscalationMatrix.Columns[5].Visible = true;
    }
    #endregion

    #region OtherFunction    
    private void InsertAndUpdateGrid(object Source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        ddl_Profile = (DropDownList)e.Item.FindControl("ddl_Profile");
        ddl_TimeFrom = (DropDownList)e.Item.FindControl("ddl_TimeFrom");
        ddl_TimeTo = (DropDownList)e.Item.FindControl("ddl_TimeTo");

        objDS = SessionEscalationMatrixGrid;      
       
        DataRow DR = null;
        if (e.CommandName == "Add")
        {
            DR = objDS.Tables[0].NewRow();
            dgEscalationMatrix.Columns[5].Visible = true;
        }
        else
        {
            DR = objDS.Tables[0].Rows[e.Item.ItemIndex];
            dgEscalationMatrix.Columns[5].Visible = true;
        }
        if (Allow_To_Add_Update_Grid() == true)
        {
           // DR["Complaint_Nature_Id"] = ComplaintNatureId;
            DR["Profile_Id"] = ddl_Profile.SelectedValue;
            DR["Profile_Name"] = ddl_Profile.SelectedItem.Text;
            DR["Time_From"] = ddl_TimeFrom.SelectedValue;
            DR["Time_To"] = ddl_TimeTo.SelectedValue;

            if (e.CommandName == "Add")
            {
                objDS.Tables[0].Rows.Add(DR);
                dgEscalationMatrix.Columns[5].Visible = true;
            }
            else if (e.CommandName == "Update")
            {
                dgEscalationMatrix.EditItemIndex = -1;
                SessionEscalationMatrixGrid = objDS;
                ddl_ComplaintNature.Enabled = true;
                dgEscalationMatrix.ShowFooter = true;
                dgEscalationMatrix.Columns[5].Visible = true;                
            }
            BindGrid();
        }        
    }
    private bool Allow_To_Add_Update_Grid()
    {
        bool _IsValid = true;
        if (Util.String2Int(ddl_ComplaintNature.SelectedValue) <=0)
        {
             errorMessage = "Please Select Complaint Nature";
            _IsValid = false;
        }
        else if (Util.String2Int(ddl_Profile.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Profile";
            _IsValid = false;
        }
        else if (Util.String2Int(ddl_TimeTo.SelectedValue) <= Util.String2Int(ddl_TimeFrom.SelectedValue))
        {
            errorMessage = "Time To Must be Greater Than Time From";
            _IsValid = false;
        }        
        return _IsValid;     
    }

    #endregion

    # region OtherMethod

    private DataTable MakeDT()
    {
            DataTable DT = new DataTable();
           
            DataRow DR = null;
            DT.Columns.Add("TimeFromId");
            DT.Columns.Add("TimeFrom");

            for (int i = 0; i <= 999; i++)
            {
                DR = DT.NewRow();
                DR["TimeFromId"] = i;
                DR["TimeFrom"] = i;
                DT.Rows.Add(DR);               
            }
            ddl_TimeFrom.DataSource = DT;
            ddl_TimeFrom.DataTextField = "TimeFrom";
            ddl_TimeFrom.DataValueField = "TimeFromId";
            ddl_TimeFrom.DataBind();

            ddl_TimeTo.DataSource = DT;
            ddl_TimeTo.DataTextField = "TimeFrom";
            ddl_TimeTo.DataValueField = "TimeFromId";
            ddl_TimeTo.DataBind();
            
             return DT;        
        } 
    #endregion

    #region ControlEvents
    protected void Page_Load(object sender, EventArgs e)
    {
       objEscalationMatrixPresenter = new EscalationMatrixPresenter(this, IsPostBack);
        if (!IsPostBack)
        {
            BindGrid();
        }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            objEscalationMatrixPresenter.save();
            Response.Redirect("~/CRM/Masters/FrmEscalationMatrix.aspx");
        }       
    }
    protected void ddl_ComplaintNature_SelectedIndexChanged(object sender, EventArgs e)
    {        
        objEscalationMatrixPresenter.FillGrid();       
        objEscalationMatrixPresenter.FillUserValues();        
        BindGrid();
    }
    #endregion   
}
