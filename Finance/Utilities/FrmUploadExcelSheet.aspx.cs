using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ClassLibrary.UIControl;

//Created: Ankit champaneriya
//Date   : 05/12/08
//Description : Attach only Excell file

public partial class FA_Common_Utilities_FrmUploadExcelSheet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        file_ExcelSheet.Attributes.Add("onchange", "return checkFileExtension(" + file_ExcelSheet.ClientID+");");
        btn_Upload.Attributes.Add("onclick", "return checkFileExtension(" + file_ExcelSheet.ClientID+");");
    }
    protected void btn_Upload_Click(object sender, EventArgs e)
    {
        string AttachmentPath;
        AttachmentPath = Server.MapPath(Request.ApplicationPath + "/Finance/Utilities/BankBookExcelFiles/" + file_ExcelSheet.FileName);
        DeleteAttachement(AttachmentPath);

        string[] splitted = AttachmentPath.Split(new char[] { '\\' });

        string _fileName = splitted[splitted.Length - 1];

        Session["fileName"] = _fileName; 

        file_ExcelSheet.SaveAs(AttachmentPath);
////        Raj.eCargo.Common o = new Raj.eCargo.Common();
////        Response.Redirect( o.getBaseURL()+"/FA_Common/Reports/frm_Bank_Reco_ExcelImport.aspx");
//////file:///E:\E-Cargo(FA)\FA_Common\
        string CloseScript = "<script language='javascript'> " + "window.opener.location.reload();window.close();" + "</script>";
        Page.RegisterStartupScript("CloseScript", CloseScript);

    }

    private void DeleteAttachement(string filePath)
    {
        //try
        //{
           FileInfo serverFile = new FileInfo(filePath);
           if (serverFile.Exists)
           {
               if (serverFile.IsReadOnly)
               { serverFile.IsReadOnly = false; }

               File.Delete(filePath);
           }



           
        //}
        //catch (Exception ex)
        //{
        //    throw new Exception(ex.Message);
        //}
    }
}