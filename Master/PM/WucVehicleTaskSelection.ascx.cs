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
using ClassLibraryMVP.Security;
using Raj.EF;
using Raj.EF.MasterPresenter;
using Raj.EF.MasterView;
using System.Text;


/// <summary>
/// Author        : Ashish Lad
/// Created On    : 31/04/2008
/// Description   : This is the Form  For Master Vehicle Task Selection
/// </summary>


public partial class Master_PM_WucVehicleTaskSelection : System.Web.UI.UserControl,IVehicleTaskSelectionView 
{
    #region ClassVariables
    VehicleTaskSelectionPresenter objVehicleTaskSelectionPresenter;
    #endregion

    #region ControlsValues
    public DataSet SessionTaskSelectionGrid
    {
        get 
        { 
            return StateManager.GetState<DataSet>("TaskSelection");      
            
        }
        set 
        { 
            StateManager.SaveState("TaskSelection", value);
            Session["SessionTaskSelectionGrid"] = value;
        }
    }
    public int Vehicle_Id
    {
        get
        {
            return Util.String2Int(hdn_Vehicle_ID.Value);

        }
        set
        {
            hdn_Vehicle_ID.Value = Util.Int2String(value);
        }
    }
   
    public DataSet TaskSelectionDS
    {
        get
        {

            DataGridItemCollection _items = dg_TaskSelection.Items;
            DataKeyCollection _dataKeys = dg_TaskSelection.DataKeys;
            DataTable Dt = new DataTable("TaskSelection");

            Dt.Columns.Add("Template_Task_ID");
            Dt.Columns.Add("Task_Name");
            Dt.Columns.Add("Template_Name");
            Dt.Columns.Add("Is_Custom");
            Dt.Columns.Add("Task_Schedule_By");
            Dt.Columns.Add("Cost");
            Dt.Columns.Add("Kms");
            Dt.Columns.Add("Days");
            Dt.Columns.Add("Task_ID");
            
            Dt.PrimaryKey = new DataColumn[] { Dt.Columns["Template_Task_ID"] };



            for (int i = 0; i < _items.Count; i++)
            {
                DataRow Dr;
                CheckBox chk_IsSelect;
                Label lbl_Template_Task_ID, lbl_Task_Name, lbl_Template_Name;
                Label lbl_Is_Custom, lbl_Task_Schedule_By, lbl_Cost;
                Label lbl_Kms, lbl_Days, lbl_Task_ID;

                Dr = Dt.NewRow();

                chk_IsSelect = (CheckBox)_items[i].FindControl("chk_IsSelect");

                if (chk_IsSelect.Checked)
                {
                    lbl_Template_Task_ID = (Label)_items[i].FindControl("lbl_Template_Task_ID");
                    Dr["Template_Task_ID"] = lbl_Template_Task_ID.Text;
                    
                    lbl_Task_Name = (Label)_items[i].FindControl("lbl_Task_Name");
                    Dr["Task_Name"] = lbl_Task_Name.Text;

                    lbl_Template_Name = (Label)_items[i].FindControl("lbl_Template_Name");
                    Dr["Template_Name"] = lbl_Template_Name.Text;

                    lbl_Is_Custom = (Label)_items[i].FindControl("lbl_Is_Custom");
                    Dr["Is_Custom"] = lbl_Is_Custom.Text;

                    lbl_Task_Schedule_By = (Label)_items[i].FindControl("lbl_Task_Schedule_By");
                    Dr["Task_Schedule_By"] = lbl_Task_Schedule_By.Text;

                    lbl_Cost = (Label)_items[i].FindControl("lbl_Cost");
                    Dr["Cost"] = lbl_Cost.Text;


                    lbl_Kms = (Label)_items[i].FindControl("lbl_Kms");
                    Dr["Kms"] = lbl_Kms.Text;

                    lbl_Days = (Label)_items[i].FindControl("lbl_Days");
                    Dr["Days"] = lbl_Days.Text;
                    

                    lbl_Task_ID = (Label)_items[i].FindControl("lbl_Task_ID");
                    Dr["Task_ID"] = lbl_Task_ID.Text;

                    Dt.Rows.Add(Dr);
                }
                
            }
            DataSet Ds_Temp = new DataSet();
            Ds_Temp.Tables.Add(Dt);
            SessionTaskSelectionGrid = Ds_Temp;
            return Ds_Temp;

        }
    }

    #endregion

    #region ControlsBind
    public DataSet Bind_dg_TaskSelection
    {
        set
        {
            SessionTaskSelectionGrid = value;
            dg_TaskSelection.DataSource = value;
            dg_TaskSelection.DataBind();
        }
    }
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = true;

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
            //return -1;
        }
    }

    #endregion
    #region ControlsEvent
    protected void Page_Load(object sender, EventArgs e)
    {
        Vehicle_Id = ClassLibraryMVP.Util.DecryptToInt(Request.QueryString["Vehicle_Id"]);
        objVehicleTaskSelectionPresenter = new VehicleTaskSelectionPresenter(this, IsPostBack);
       
    }
    
    protected void btn_Apply_Click(object sender, EventArgs e)
    {
        string _str=TaskSelectionDS.GetXml();
        bool _IsValid=true;
        if (SessionTaskSelectionGrid.Tables[0].Rows.Count <= 0)
        {
            errorMessage= "Please Select Atleast One Task";
            _IsValid=false;
        }
       
        if(_IsValid)
        {
            objVehicleTaskSelectionPresenter.Save();
            string _popopScript;
            _popopScript = "<script language='javascript'> window.opener.location.reload();window.close();</script>";
            Page.ClientScript.RegisterStartupScript(typeof(string), "PopupScript", _popopScript);
        }
        
    }
    #endregion
}
