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

/// <summary>
/// Author        : Aashish Lad
/// Created On    : 08th October 2008
/// Description   : This is the Page For Master Transit Schedule
/// </summary>

public partial class Master_Branch_WucTransitSchedule : System.Web.UI.UserControl,ITransitScheduleView 
{
    #region ClassVariables
    TransitSchedulePresenter objTransitSchedulePresenter;
    #endregion

    #region ControlsValue

    
    public int FromStateID
    {
        set
        {
            ddl_FromState.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_FromState.SelectedValue);
        }

    }
    public int ToStateID
    {
        set
        {
            ddl_ToState.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_ToState.SelectedValue);
        }

    }
    public int VehicleID
    {
        set
        {
            ddl_VehicleType.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_VehicleType.SelectedValue);
        }

    }
    #endregion

    #region ControlsBind
    public DataTable Bind_ddl_ToState
    {
        set
        {
            ddl_ToState.DataSource = value;
            ddl_ToState.DataTextField = "State_Name";
            ddl_ToState.DataValueField = "State_Id";
            ddl_ToState.DataBind();

            ddl_ToState.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable Bind_ddl_FromState
    {
        set
        {
            ddl_FromState.DataSource = value;
            ddl_FromState.DataTextField = "State_Name";
            ddl_FromState.DataValueField = "State_Id";
            ddl_FromState.DataBind();
            ddl_FromState.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable Bind_ddl_Vehicle
    {
        set
        {
            ddl_VehicleType.DataSource = value;
            ddl_VehicleType.DataTextField = "Vehicle_Type";
            ddl_VehicleType.DataValueField = "Vehicle_Type_Id";
            ddl_VehicleType.DataBind();
            ddl_VehicleType.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataSet Bind_dg_TransitSchedule
    {
        set
        {
            SessionTransitScheduleGrid = value;
            dg_TransitSchedule.DataSource = value;
            dg_TransitSchedule.DataBind();
        }
    }
    public DataSet SessionTransitScheduleGrid
    {
        get { return StateManager.GetState<DataSet>("TransitSchedule"); }
        set { StateManager.SaveState("TransitSchedule", value); }
    }
    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;
        if (Util.String2Int(ddl_FromState.SelectedValue) == 0)
        {
            lbl_Errors.Text = "Please Select From State";
            _isValid = false;
        }
        else if (Util.String2Int(ddl_ToState.SelectedValue) == 0)
        {
            lbl_Errors.Text = "Please Select To State";
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
            //return Util.DecryptToInt(Request.QueryString["Id"]);
            return -1;
        }
    }

    #endregion


    #region OtherProperties

    #endregion


    #region OtherMethods

    #endregion

    #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        objTransitSchedulePresenter = new TransitSchedulePresenter(this, IsPostBack);
    }
    protected void ddl_FromState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (FromStateID > 0 && ToStateID > 0 && VehicleID > 0)
        {
            dg_TransitSchedule.CurrentPageIndex = 0;
            objTransitSchedulePresenter.FillGrid(FromStateID, ToStateID, VehicleID);
            dg_TransitSchedule.Visible = true;
        }
        else
            dg_TransitSchedule.Visible = false;
    }

    protected void ddl_ToState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (FromStateID > 0 && ToStateID > 0 && VehicleID > 0)
        {
            dg_TransitSchedule.CurrentPageIndex = 0;
            objTransitSchedulePresenter.FillGrid(FromStateID, ToStateID, VehicleID);
            dg_TransitSchedule.Visible = true;
        }
        else
            dg_TransitSchedule.Visible = false;
    }

    protected void ddl_VehicleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (FromStateID > 0 && ToStateID > 0 && VehicleID > 0)
        {
            dg_TransitSchedule.CurrentPageIndex = 0;
            objTransitSchedulePresenter.FillGrid(FromStateID, ToStateID, VehicleID);
            dg_TransitSchedule.Visible = true;
        }
        else
            dg_TransitSchedule.Visible = false;
    }

    protected void dg_TransitSchedule_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
            int i;
           
            bool isGreater = false;
            if (e.Item.ItemIndex != -1)
            {
                for (i = 1; i <= e.Item.Cells.Count - 1; i++)
                {                    
                    if (e.Item.Cells[i].Text.ToUpper() != "NA")
                    {
                        if (Convert.ToInt32(e.Item.Cells[i].Text) > 20)
                        {                            
                            isGreater = true;
                        }
                    }
                    switch (e.Item.Cells[i].Text )
                    {
                        case "0":

                            e.Item.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#CCFFFF");
                            break;
                        case "1":
                            e.Item.Cells[i].Text = "ND";
                            e.Item.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#00ECEC");
                            break;
                        case "2":
                            e.Item.Cells[i].Text = "1";
                            e.Item.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#0EB6B6");
                            break;
                        case "3":
                            e.Item.Cells[i].Text = "2";
                            e.Item.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFDBA4");
                            break;
                        case "4":
                            e.Item.Cells[i].Text = "3";
                            e.Item.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFA317");
                            break;
                        case "5":
                            e.Item.Cells[i].Text = "4";
                            e.Item.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#D58000");
                            break;
                        case "6":
                            e.Item.Cells[i].Text = "5";
                            e.Item.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFBBBB");
                            break;
                        case "7":
                            e.Item.Cells[i].Text = "6";
                            e.Item.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF7A59");
                            break;
                        case "8":
                            e.Item.Cells[i].Text = "7";
                            e.Item.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF3300");
                            break;
                        case "9":
                            e.Item.Cells[i].Text = "8";
                            e.Item.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF3300");
                            break;
                        case "10":
                            e.Item.Cells[i].Text = "9";
                            e.Item.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF3300");
                            break;
                        case "20":
                            e.Item.Cells[i].Text = "NC";
                            e.Item.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF3300");
                            break;
                        case "NA":
                            e.Item.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#00FF00");
                            break;
                    }
                    if (isGreater == true)
                    {
                        e.Item.Cells[i].Text = "NC";
                        e.Item.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF3300");
                        isGreater = false;
                    }
                }
            }
         
    }
    protected void dg_TransitSchedule_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_TransitSchedule.CurrentPageIndex = e.NewPageIndex;
        Bind_dg_TransitSchedule = SessionTransitScheduleGrid;
    }
    #endregion
}
