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
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
//using Raj.eCargo.Init;
using Raj.CRM.TransactionsPresenter;
using Raj.CRM.TransactionsView;

 

public partial class CRM_Transaction_WucTicketInfo : System.Web.UI.UserControl, IDisplayInfoView
{
    #region ClassVariables
    DisplayInfoPresenter objDisplayInfoPresenter;
    string Url="";
    #endregion 

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        return _isValid;
    }

    public string errorMessage
    {
        set
        {
             // lbl_Errors.Text = value;
        }
    }
      

    public string keyID
    {
       get
        {
            if (Request.QueryString["Id"] != null)
            { return Util.DecryptToString(Request.QueryString["Id"]); }
            else { return "-1"; }
        }
    }
    public string Type
    {
      get {
            //return "Assigned";
            return Util.DecryptToString(Request.QueryString["Type"]); 
          }
    }

    public int Ticket_Id
    {
        get
        {
            //return "Assigned";
            return Util.DecryptToInt(Request.QueryString["Ticket_Id"]);
        }
    }
     public DataTable bind_dg_DisplayInfo
    {
        set
        {
            dg_DisplayInfo.DataSource =value;
            dg_DisplayInfo.DataBind();
        }
    }
    #endregion

    #region OtherProperties

    

    #endregion


    #region OtherMethods
    private void DefaultSettings()
    {
    
    }

    #endregion
    

    #region ControlsEvents
    protected void Page_Load(object sender, EventArgs e)
    {

       DefaultSettings();
       if (Type == "TicketInfo")
       {
           Url = ClassLibraryMVP.Util.GetBaseURL() + "/CRM/Transaction/FrmTicketHistory.aspx?Type=" + ClassLibraryMVP.Util.EncryptString("Other");
       }

       objDisplayInfoPresenter = new DisplayInfoPresenter(this, IsPostBack);

       dg_DisplayInfo.Width = 880;
       dg_DisplayInfo.Height = 400;

       if (Type == "TicketInfo")
       {
           dg_DisplayInfo.Columns[0].Hidden = true;
           lbl_Heading.Text = "GC No. " + keyID.ToString();
       }
    }

    #endregion


    protected void dg_DisplayInfo_InitializeRow(object sender, Infragistics.WebUI.UltraWebGrid.RowEventArgs e)
    {
        if (Type == "TicketInfo")
        {
            if (e.Row.Cells.Count > 0 && e.Row.Band.Index == 0)
            {
                int TicketId = Convert.ToInt32(e.Row.Cells[0].Value);

                //if ((Convert.ToInt32(e.Row.Cells[12].Value)) != 0 || (Convert.ToString(e.Row.Cells[12].Value)) != "")
                //{

                e.Row.Cells.FromKey("Ticket No").Value = "<a href=# style='text-decoration:none;cursor:hand' onClick=" + (char)34 + "return OpenWindow('" + Url + "&Id=" + ClassLibraryMVP.Util.EncryptInteger(TicketId) + "')" + (char)34 + ">" + e.Row.Cells.FromKey("Ticket No") + "</a>";
                // e.Row.Cells.FromKey("Ticket No").Value = "<a href=# style='text-decoration:none;cursor:hand' onClick=return OpenWindow('" + Url + "&Ticket_Id=" + "" + "')>" + e.Row.Cells.FromKey("Ticket No").Value + "</a>";
                //}
            }
        }
    }

}

