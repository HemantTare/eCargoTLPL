using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

using ClassLibraryMVP;

using Raj.EC.FinancePresenter;
using Raj.EC.FinanceView;

public partial class FA_Common_Accounting_Masters_WucAssesseeDetails : System.Web.UI.UserControl,IFBTAssesseeDetailsView
{
    #region ClassVariable
    FBTAssesseeDetailsPresenter objFBTAssesseeDetailsPresenter;
    #endregion

    #region ControlsValue
    public int Assessee_Type_Id
    {
        get{return Convert.ToInt32(ddl_AssesseeType.SelectedValue.ToString());}
    }

    public DataSet SessionAssesseeDetails
    {
        set{StateManager.SaveState("AssesseeDetails", value);}
        get{return StateManager.GetState<DataSet>("AssesseeDetails");}
    }
    #endregion

    #region ControlsBind

    private void BindAssesseeGrid()
    {
        dg_FBTAssesseeDetails.DataSource = SessionAssesseeDetails;
        dg_FBTAssesseeDetails.DataBind();
    }

    public DataSet SessionAssesseeType
    {
        set
        {
            ddl_AssesseeType.DataValueField = "Assessee_Type_Id";
            ddl_AssesseeType.DataTextField = "Assessee_Type_Name";
            ddl_AssesseeType.DataSource = value;
            ddl_AssesseeType.DataBind();


            StateManager.SaveState("AssesseeType", value);
        }
        get
        {
            return StateManager.GetState<DataSet>("AssesseeType");
        }
    }
    #endregion

    #region IView

    public bool validateUI()
    {
        return true;
    }

    public int keyID
    {

        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
       
    }

    public string errorMessage
    {
        set
        {
            lbl_Error.Text=value;
        }
    }
    public void ClearVariables()
    {
        SessionAssesseeDetails = null;
        SessionAssesseeType = null;
    }
  #endregion

    #region PageEvents
    protected void Page_Load(object sender, EventArgs e)
    {
       objFBTAssesseeDetailsPresenter = new FBTAssesseeDetailsPresenter(this, IsPostBack);
       if (!IsPostBack)
       {
          BindAssesseeGrid();
       }
       btn_Save.Visible = false;
       dg_FBTAssesseeDetails.Columns[5].Visible = false;
       dg_FBTAssesseeDetails.Columns[6].Visible = false;
       dg_FBTAssesseeDetails.ShowFooter = false;
    }
       
   #endregion

    #region GridEvents

    protected void dg_FBTAssesseeDetails_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName.ToUpper() == "ADD")
        {
            errorMessage = "";

            ComponentArt.Web.UI.Calendar Picker_FromDate = (ComponentArt.Web.UI.Calendar)e.Item.FindControl("Picker_FromDate");
            //User_Controls_wuc_Date_Picker dtp_Applicable_From = (User_Controls_wuc_Date_Picker)e.Item.FindControl("dtp_Applicable_From");
            TextBox txt_FBT_Rate = (TextBox)e.Item.FindControl("txt_FBT_Rate");
            TextBox txt_Surcharge = (TextBox)e.Item.FindControl("txt_Surcharge");
            TextBox txt_Addl_Surcharge = (TextBox)e.Item.FindControl("txt_Addl_Surcharge");
            TextBox txt_Addl_Education_Cess = (TextBox)e.Item.FindControl("txt_Addl_Education_Cess");


            if (txt_FBT_Rate.Text.Length == 0)
            {
                errorMessage = "Please Enter FBT Rate ";
                txt_FBT_Rate.Focus();
                return;
            }


            if (txt_Surcharge.Text.Length == 0)
            {
                errorMessage = "Please Enter Surcharge ";
                txt_Surcharge.Focus();
                return;
            }

            if (txt_Addl_Surcharge.Text.Length == 0)
            {
                errorMessage = "Please Enter Additional Surcharge ";
                txt_Addl_Surcharge.Focus();
                return;
            }

            if (txt_Addl_Education_Cess.Text.Length == 0)
            {
                errorMessage = "Please Enter Additional Education Cess ";
                txt_Addl_Education_Cess.Focus();
                return;
            }


            DataSet ds = SessionAssesseeDetails;
            DataRow dr = ds.Tables[0].NewRow();

            dr["Applicable_From"] = String.Format("{0:dd/MMM/yyyy}", Picker_FromDate.SelectedDate);
            dr["FBT_Rate"] = String.Format("{0:0.00}", Convert.ToDouble(txt_FBT_Rate.Text));
            dr["Surcharge"] = String.Format("{0:0.0}", Convert.ToDouble(txt_Surcharge.Text));
            dr["Additional_Surcharge_Cess"] = String.Format("{0:0.00}", Convert.ToDouble(txt_Addl_Surcharge.Text));
            dr["Additional_Educational_Cess"] = String.Format("{0:0.00}", Convert.ToDouble(txt_Addl_Education_Cess.Text));

            ds.Tables[0].Rows.Add(dr);
            ds.AcceptChanges();

            SessionAssesseeDetails = ds;
            BindAssesseeGrid();





        }

        //For Delete Command
        else if (e.CommandName.ToUpper() == "DELETE")
        {
            DataSet ds = SessionAssesseeDetails;
            ds.Tables[0].Rows.RemoveAt(e.Item.ItemIndex);

            ds.Tables[0].AcceptChanges();

            dg_FBTAssesseeDetails.EditItemIndex = -1;
            dg_FBTAssesseeDetails.ShowFooter = true;
            SessionAssesseeDetails = ds;
            BindAssesseeGrid();
        }

    }
    protected void dg_FBTAssesseeDetails_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Footer)
        {
            ComponentArt.Web.UI.Calendar temp = (ComponentArt.Web.UI.Calendar)e.Item.FindControl("Picker_FromDate");
            temp.SelectedDate = System.DateTime.Now;
        }

        if (e.Item.ItemType == ListItemType.EditItem)
        {
            //Find Controls
            ComponentArt.Web.UI.Calendar Picker_FromDate = (ComponentArt.Web.UI.Calendar)e.Item.FindControl("Picker_FromDate");
            //User_Controls_wuc_Date_Picker dtp_Applicable_From = (User_Controls_wuc_Date_Picker)e.Item.FindControl("dtp_Applicable_From");
            TextBox txt_FBT_Rate = (TextBox)e.Item.FindControl("txt_FBT_Rate");
            TextBox txt_Surcharge = (TextBox)e.Item.FindControl("txt_Surcharge");
            TextBox txt_Addl_Surcharge = (TextBox)e.Item.FindControl("txt_Addl_Surcharge");
            TextBox txt_Addl_Education_Cess = (TextBox)e.Item.FindControl("txt_Addl_Education_Cess");


            DataSet ds = SessionAssesseeDetails;
            DataRow dr = ds.Tables[0].Rows[e.Item.ItemIndex];

            Picker_FromDate.SelectedDate = Convert.ToDateTime(dr["Applicable_From"]);
            txt_FBT_Rate.Text = dr["FBT_Rate"].ToString();
            txt_Surcharge.Text = dr["Surcharge"].ToString();
            txt_Addl_Surcharge.Text = dr["Additional_Surcharge_Cess"].ToString();
            txt_Addl_Education_Cess.Text = dr["Additional_Educational_Cess"].ToString();
        }
    }
    protected void dg_FBTAssesseeDetails_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_FBTAssesseeDetails.EditItemIndex = e.Item.ItemIndex;
        dg_FBTAssesseeDetails.ShowFooter = false;
        BindAssesseeGrid();

        for (int i = 0; i < dg_FBTAssesseeDetails.Items.Count; i++)
        {
            dg_FBTAssesseeDetails.Items[i].Cells[6].Enabled = false;
        }

    }
    protected void dg_FBTAssesseeDetails_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        DataSet ds = SessionAssesseeDetails;
        //Find Controls
        ComponentArt.Web.UI.Calendar Picker_FromDate = (ComponentArt.Web.UI.Calendar)e.Item.FindControl("Picker_FromDate");
        TextBox txt_FBT_Rate = (TextBox)e.Item.FindControl("txt_FBT_Rate");
        TextBox txt_Surcharge = (TextBox)e.Item.FindControl("txt_Surcharge");
        TextBox txt_Addl_Surcharge = (TextBox)e.Item.FindControl("txt_Addl_Surcharge");
        TextBox txt_Addl_Education_Cess = (TextBox)e.Item.FindControl("txt_Addl_Education_Cess");

        if (txt_FBT_Rate.Text.Length == 0)
        {
            errorMessage = "Please Enter FBT Rate ";
            txt_FBT_Rate.Focus();
            return;
        }


        if (txt_Surcharge.Text.Length == 0)
        {
            errorMessage = "Please Enter Surcharge ";
            txt_Surcharge.Focus();
            return;
        }

        if (txt_Addl_Surcharge.Text.Length == 0)
        {
            errorMessage = "Please Enter Additional Surcharge ";
            txt_Addl_Surcharge.Focus();
            return;
        }

        if (txt_Addl_Education_Cess.Text.Length == 0)
        {
            errorMessage = "Please Enter Additional Education Cess ";
            txt_Addl_Education_Cess.Focus();
            return;
        }
        DataRow DR = ds.Tables[0].Rows[e.Item.ItemIndex];

        DR["Applicable_From"] = Picker_FromDate.SelectedDate.ToString("dd/MMM/yyyy");
        DR["FBT_Rate"] = txt_FBT_Rate.Text;
        DR["Surcharge"] = txt_Surcharge.Text;
        DR["Additional_Surcharge_Cess"] = txt_Addl_Surcharge.Text;
        DR["Additional_Educational_Cess"] = txt_Addl_Education_Cess.Text;


        dg_FBTAssesseeDetails.EditItemIndex = -1;
        dg_FBTAssesseeDetails.ShowFooter = true;

        SessionAssesseeDetails = ds;
        BindAssesseeGrid();

    }
    protected void dg_FBTAssesseeDetails_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_FBTAssesseeDetails.EditItemIndex = -1;
        dg_FBTAssesseeDetails.ShowFooter = true;
        BindAssesseeGrid();
    }
    #endregion

    protected void btn_Save_Click(object sender, EventArgs e)
    {
       objFBTAssesseeDetailsPresenter.Save();
    }

    protected void ddl_AssesseeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        errorMessage = "";
        objFBTAssesseeDetailsPresenter.FillGridOnAssesseeTypeChange();
        BindAssesseeGrid();
        btn_Save.Visible = false;
        dg_FBTAssesseeDetails.Columns[5].Visible = false;
        dg_FBTAssesseeDetails.Columns[6].Visible = false;
        dg_FBTAssesseeDetails.ShowFooter = false;
    }
}
