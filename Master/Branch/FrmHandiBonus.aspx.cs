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
using ClassLibrary;
using ClassLibraryMVP.General;
using Raj.EC;

public partial class Master_Branch_FrmHandiBonus : ClassLibraryMVP.UI.Page
{
    #region Declaration
    private DataSet ds, dsMonth, dsYear, dsBranch;

    bool ATS = false;
    #endregion
    public decimal NoOfLR1
    {
        set
        {
            txtLR1.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_LR1.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_LR1.Value == string.Empty ? 0 : Util.String2Decimal(hdn_LR1.Value); }
        
    }


    public decimal NoOfLR2
    {
        set
        {
            txtLR2.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_LR2.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_LR2.Value == string.Empty ? 0 : Util.String2Decimal(hdn_LR2.Value); }

    }

    public decimal NoOfLR3
    {
        set
        {
            txtLR3.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_LR3.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_LR3.Value == string.Empty ? 0 : Util.String2Decimal(hdn_LR3.Value); }

    }

    public decimal Bonus1
    {
        set
        {
            txtBonus1.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_Bonus1.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_Bonus1.Value == string.Empty ? 0 : Util.String2Decimal(hdn_Bonus1.Value); }

    }


    public decimal Bonus2
    {
        set
        {
            txtBonus2.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_Bonus2.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_Bonus2.Value == string.Empty ? 0 : Util.String2Decimal(hdn_Bonus2.Value); }

    }


    public decimal Bonus3
    {
        set
        {
            txtBonus3.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_Bonus3.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_Bonus3.Value == string.Empty ? 0 : Util.String2Decimal(hdn_Bonus3.Value); }

    }
    

    #region members
    public bool validateUI()
    {
        bool ATS;
        ATS = false; 
 
        if (Util.String2Int(ddlBranch.SelectedValue) <= 0)
        {
            lblErrors.Text = "Please select Handi Branch";
            ddlBranch.Focus();
        }
        else if (Util.String2Int(ddl_Month.SelectedValue) <= 0)
        {
            lblErrors.Text = "Please select Month";
            ddl_Month.Focus();
        }
        else if (Util.String2Int(ddl_Year.SelectedValue) <= 0)
        {
            lblErrors.Text = "Please select Year";
            ddl_Year.Focus();
        }
        else if (Util.String2Int(txtLR1.Text) <= 0)
        {
            lblErrors.Text = "Please Enter Target LR Per Day";
            txtLR1.Focus();
        }
        else if (Util.String2Decimal(txtBonus1.Text) <= 0)
        {
            lblErrors.Text = "Please Enter Bonus Per Additional LR";
            txtBonus1.Focus();
        }

        else
        {
            ATS = true;
        }

        return ATS;
    }
    private void Assign_Hidden_Value()
    {
        hdn_LR1.Value = txtLR1.Text;
        hdn_LR2.Value = txtLR2.Text;
        hdn_LR3.Value = txtLR3.Text;

        hdn_Bonus1.Value = txtBonus1.Text;
        hdn_Bonus2.Value = txtBonus2.Text;
        hdn_Bonus3.Value = txtBonus3.Text;

    }
    private void Set_Hidden_Value()
    {
        NoOfLR1 = hdn_LR1.Value == string.Empty ? 0 : Util.String2Decimal(hdn_LR1.Value);
        NoOfLR2 = hdn_LR2.Value == string.Empty ? 0 : Util.String2Decimal(hdn_LR2.Value);
        NoOfLR3 = hdn_LR3.Value == string.Empty ? 0 : Util.String2Decimal(hdn_LR3.Value);

        Bonus1 = hdn_Bonus1.Value == string.Empty ? 0 : Util.String2Decimal(hdn_Bonus1.Value);
        Bonus2 = hdn_Bonus2.Value == string.Empty ? 0 : Util.String2Decimal(hdn_Bonus2.Value);
        Bonus3 = hdn_Bonus3.Value == string.Empty ? 0 : Util.String2Decimal(hdn_Bonus3.Value);
    }
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        if (!IsPostBack)
        {
            hdnKeyID.Value = Util.Int2String(Util.DecryptToInt(Request.QueryString["Id"])); 

            ddlBranch.DataTextField = "Branch_Name";
            ddlBranch.DataValueField = "Branch_Id";
            Fill_Month();
            Fill_Year();
            Fill_Branch();

            ReadValues();
        }

        Assign_Hidden_Value();
        Set_Hidden_Value();

    }
    private void ReadValues()
    {

        DAL objDAL = new DAL();
        DataSet ds = new DataSet();
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@HandiID", SqlDbType.Int, 0, Util.String2Int(hdnKeyID.Value)) };
        
        objDAL.RunProc("EC_Mst_Branch_HundiBonus_ReadValues", objSqlParam, ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            string text, value;
            DataRow objDR = ds.Tables[0].Rows[0]; 

            text = objDR["Branch_Name"].ToString();
            value = objDR["BranchID"].ToString();
            Raj.EC.Common.SetValueToDDLSearch(text, value, ddlBranch);
             
            
            ddl_Month.SelectedValue = objDR["Month"].ToString();
            ddl_Year.SelectedValue = objDR["Year"].ToString();

            ddl_Desti1.SelectedValue = objDR["Dest1LocationID"].ToString();
            NoOfLR1 = Util.String2Int(objDR["NoOfLR1PerDay"].ToString());
            Bonus1 = Util.String2Decimal(objDR["Bonus1PerLR"].ToString());

            ddl_Desti2.SelectedValue = objDR["Dest2LocationID"].ToString();
            NoOfLR2 = Util.String2Int(objDR["NoOfLR2PerDay"].ToString());
            Bonus2 = Util.String2Decimal(objDR["Bonus2PerLR"].ToString());

            ddl_Desti3.SelectedValue = objDR["Dest3LocationID"].ToString();
            NoOfLR3 = Util.String2Int(objDR["NoOfLR3PerDay"].ToString());
            Bonus3 = Util.String2Decimal(objDR["Bonus3PerLR"].ToString());


            ddl_Desti1As.SelectedValue = objDR["Dest1As"].ToString();
            ddl_Desti2As.SelectedValue = objDR["Dest2As"].ToString();

            //DiscountPercent = Util.String2Decimal(objDR["DiscountPercent"].ToString());
            
        } 
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            Save();
        }
    }

    private Message Save()
    {
        //DataTable DT = (DataTable)(Session["DiscountDetails"]);
        //DT.TableName = "discountdetails";
        //DataTable DT1 = DT.Copy();

        //DataSet ds = new DataSet();
        //ds.Tables.Add(DT1);

        //string discountDetailsXML = ds.GetXml().ToLower();

        int ddlMonth = 0, ddlYear = 0, ddlDest1 = 0, ddlDest2 = 0, ddlDest3 = 0, ddlDest1As = 1, ddlDest2As = 1;

        ddlMonth = Util.String2Int(ddl_Month.SelectedValue);
        ddlYear = Util.String2Int(ddl_Year.SelectedValue);

        ddlDest1 = Util.String2Int(ddl_Desti1.SelectedValue);
        ddlDest2 = Util.String2Int(ddl_Desti2.SelectedValue);
        ddlDest3 = Util.String2Int(ddl_Desti3.SelectedValue);

        ddlDest1As = Util.String2Int(ddl_Desti1As.SelectedValue);
        ddlDest2As = Util.String2Int(ddl_Desti2As.SelectedValue);

         
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            objDAL.MakeOutParams("@HandiIDOutput", SqlDbType.Int, 0), 
            objDAL.MakeInParams("@HandiID",SqlDbType.Int,0,Util.String2Int(hdnKeyID.Value)), 
            objDAL.MakeInParams("@BranchID",SqlDbType.Int,0,Util.String2Int(ddlBranch.SelectedValue)), 
            objDAL.MakeInParams("@Month",SqlDbType.Int,0,ddlMonth),
            objDAL.MakeInParams("@Year",SqlDbType.Int,0,ddlYear),

            objDAL.MakeInParams("@Dest1LocationID",SqlDbType.Int,0,ddlDest1),
            objDAL.MakeInParams("@NoOfLR1PerDay",SqlDbType.Int,0,Util.String2Int(txtLR1.Text)),
            objDAL.MakeInParams("@Bonus1PerLR",SqlDbType.Decimal,0,Util.String2Decimal(txtBonus1.Text)),

            objDAL.MakeInParams("@Dest2LocationID",SqlDbType.Int,0,ddlDest2),
            objDAL.MakeInParams("@NoOfLR2PerDay",SqlDbType.Int,0,Util.String2Int(txtLR2.Text)),
            objDAL.MakeInParams("@Bonus2PerLR",SqlDbType.Decimal,0,Util.String2Decimal(txtBonus2.Text)),

            objDAL.MakeInParams("@Dest3LocationID",SqlDbType.Int,0,ddlDest3),
            objDAL.MakeInParams("@NoOfLR3PerDay",SqlDbType.Int,0,Util.String2Int(txtLR3.Text)),
            objDAL.MakeInParams("@Bonus3PerLR",SqlDbType.Decimal,0,Util.String2Decimal(txtBonus3.Text)),

            objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0,UserManager.getUserParam().UserId),

            objDAL.MakeInParams("@Dest1As",SqlDbType.Int,0,ddlDest1As),
            objDAL.MakeInParams("@Dest2As",SqlDbType.Int,0,ddlDest2As)
            
            
        };

        objDAL.RunProc("dbo.EC_Mst_BranchHandiBonus_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {
            lblErrors.Text = "Saved SuccessFully";
            hdnKeyID.Value = Convert.ToString(objSqlParam[2].Value);
        }
        else
        {
            lblErrors.Text = objMessage.message;
        }
        return objMessage;
    }

    #region grid operation

    
    #endregion

    private void Fill_Month()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam ={  
            objDAL.MakeInParams("@MonthID", SqlDbType.Int,0,0) 
        };

        objDAL.RunProc("EC_Master_Get_Finacial_Month", objSqlParam, ref dsMonth);

        ddl_Month.DataSource = dsMonth;
        ddl_Month.DataTextField = "MonthName";
        ddl_Month.DataValueField = "MonthID";
        ddl_Month.DataBind();
    }

    private void Fill_Year()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam ={  
            objDAL.MakeInParams("@YearID", SqlDbType.Int,0,0) 
        };

        objDAL.RunProc("EC_Master_Get_Year", objSqlParam, ref dsYear);

        ddl_Year.DataSource = dsYear;
        ddl_Year.DataTextField = "YearName";
        ddl_Year.DataValueField = "YearID";
        ddl_Year.DataBind();
    }

    private void Fill_Branch()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam ={  
            objDAL.MakeInParams("@BranchID", SqlDbType.Int,0,0) 
        };

        objDAL.RunProc("EC_Master_Fill_HandiBranch", objSqlParam, ref dsBranch);

        ddl_Desti1.DataSource = dsBranch;
        ddl_Desti1.DataTextField = "BranchName";
        ddl_Desti1.DataValueField = "BranchID";
        ddl_Desti1.DataBind();

        ddl_Desti2.DataSource = dsBranch;
        ddl_Desti2.DataTextField = "BranchName";
        ddl_Desti2.DataValueField = "BranchID";
        ddl_Desti2.DataBind();

        //ddl_Desti3.DataSource = dsBranch;
        //ddl_Desti3.DataTextField = "BranchName";
        //ddl_Desti3.DataValueField = "BranchID";
        //ddl_Desti3.DataBind();

        ddl_Desti3.Items.Insert(0, new ListItem("All", "0"));
        //ScriptManager.SetFocus(ddl_Desti3);  


        ddl_Desti1As.Items.Insert(0, new ListItem("Delivery Branch", "1"));
        ddl_Desti1As.Items.Insert(1, new ListItem("Delivery Location", "2"));

        ddl_Desti2As.Items.Insert(0, new ListItem("Delivery Branch", "1"));
        ddl_Desti2As.Items.Insert(1, new ListItem("Delivery Location", "2"));

    }
}
