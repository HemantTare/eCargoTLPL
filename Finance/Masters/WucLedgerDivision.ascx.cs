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
using Raj.EC.FinanceView;
using ClassLibraryMVP.DataAccess;
public partial class Finance_Masters_WucDivision : System.Web.UI.UserControl,IDivisionView
{

  public  string getDivisonXML 
  {
      get 
      {
          DataSet Ds = new DataSet();
          Ds.Tables.Add(GetSlectedIds());
          return Ds.GetXml();
      }
  }

    public DataTable GetSlectedIds()
    { 
          DataTable Dt = new DataTable("Division");
          Dt.Columns.Add("Division_ID");
          DataRow Dr;
          if (UserManager.getUserParam().IsDivisionReq)
          {
              for (int i = 0; i < chkl_Division.Items.Count; i++)
              {
                  Dr = Dt.NewRow();
                  if (chkl_Division.Items[i].Selected)
                  {
                      Dr["Division_ID"] = chkl_Division.Items[i].Value;
                      Dt.Rows.Add(Dr);
                  }
              }
          }
          else
          {
              Dr = Dt.NewRow();
              Dr["Division_ID"] ="1";
              Dt.Rows.Add(Dr);
          }
          return Dt;
    }

  public DataTable bind_chkl_Division 
  {
      set
      {
          chkl_Division.DataValueField = "Division_Id";
          chkl_Division.DataTextField = "Division_Name";
          chkl_Division.DataSource = value;
          chkl_Division.DataBind();
      }
  }

    public DataTable SetDivisions
    {
        set
        {
            for (int i = 0; i < value.Rows.Count; i++)
            {
                ListItem item = chkl_Division.Items.FindByValue(value.Rows[i]["Division_ID"].ToString());
                if (item != null)
                {
                    item.Selected = true;
                }
            }
        }
    }

  

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DAL objDAL = new DAL();
            DataSet Ds = new DataSet();
            objDAL.RunProc("EC_FA_Mst_Ledger_FillDivision", ref Ds);
            bind_chkl_Division = Ds.Tables[0];
        }
    }
}
