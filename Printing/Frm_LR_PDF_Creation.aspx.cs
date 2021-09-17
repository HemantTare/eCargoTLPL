using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using System.Text;


public partial class Printing_Frm_LR_PDF_Creation : System.Web.UI.Page
{
    #region Declaration

    private DataSet ds;
    DAL objDAL = new DAL();


    #endregion


    public bool IsDocumentNoWise
    {
        set
        {
            Boolean IsChk = value;

            if (IsChk == false)
                rbl_DocumentWiseLRWise.SelectedValue = "1";
            else
                rbl_DocumentWiseLRWise.SelectedValue = "0";

           hdn_IsDocumentNoWise.Value = value.ToString();
        }
        get { return rbl_DocumentWiseLRWise.SelectedValue == "0" ? true : false; }
    }

    public int DocumentTypeId
    {

        set
        {

            hdn_DocumentTypeId.Value = ddl_DocumentType.SelectedValue.ToString();
        }

        get { return Util.String2Int(ddl_DocumentType.SelectedValue); }
    }

    public string DocumentNo
    {
        set 
        { 
            txt_Document_No.Text = value;
            hdn_DocumentNo.Value = value.ToString();
        }
        get { return txt_Document_No.Text; }
    }

    public string GCNos
    {
        set 
        { 
            txt_GCNos.Text = value;
            hdn_GCNos.Value = value.ToString();
        }
        get { return txt_GCNos.Text; }
    }


    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack == false)
        {
            rbl_DocumentWiseLRWise_SelectedIndexChanged(this,e);

            hdn_DocumentTypeId.Value = "30";
        }

        StringBuilder Path = new StringBuilder(Util.GetBaseURL());
        Path.Append("/Printing/Frm_LR_PDF_Creation_Viewer.aspx?");

        btnCreatePDF.Attributes.Add("onclick", "return Open_PDF_Window('" + Path + "'," + hdn_IsDocumentNoWise.ClientID + "," + hdn_DocumentTypeId.ClientID + "," + hdn_DocumentNo.ClientID + "," + hdn_GCNos.ClientID + ");");

    }

    

    #endregion 

    public void ClearVariables()
    {
        ds = null;
    }

    protected void rbl_DocumentWiseLRWise_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbl_DocumentWiseLRWise.Items[0].Selected == true)
        {
            tr_DocumentWise.Visible = true;
            tr_GCWise.Visible = false;

            hdn_IsDocumentNoWise.Value = "true";

        }
        else
        {
            tr_DocumentWise.Visible = false;
            tr_GCWise.Visible = true;

            hdn_IsDocumentNoWise.Value = "false";

        }
    }

   

    protected void btnCreatePDF_Click(object sender, EventArgs e)
    {

        //bool IsDocumentNoWise;

        //if (rbl_DocumentWiseLRWise.SelectedValue == "1")
        //{
        //    IsDocumentNoWise = true;
        //}
        //else
        //{
        //    IsDocumentNoWise = false;
        //}


        //String PDFPath = Util.GetBaseURL() + "/Printing/Frm_LR_PDF_Creation_Viewer.aspx?IsDocumentNoWise=" + ClassLibraryMVP.Util.EncryptBool(IsDocumentNoWise) + "&DocumentTypeId=" + ddl_DocumentType.SelectedValue + "&DocumentNo=" + txt_Document_No.Text + "&GCNoXml=" + txt_GCNos.Text;
        //btnCreatePDF.Attributes.Add("onclick", "return Open_PDF_Window('" + PDFPath + "');");


    }

    protected void ddl_DocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {


        hdn_DocumentTypeId.Value = ddl_DocumentType.SelectedValue;

    }

}
