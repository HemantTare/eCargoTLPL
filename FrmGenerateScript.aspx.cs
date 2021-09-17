using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.DataAccess;
using System.IO;
using Raj.EC;
public partial class FrmGenerateScript : System.Web.UI.Page
{
    string companyname;
    string Filename;
    string path;

    protected void Page_Load(object sender, EventArgs e)
    {
        companyname = CompanyManager.getCompanyParam().CompanyName;
        Filename = Request.PhysicalApplicationPath + "\\SQL\\" + companyname + " SQL Scripts.txt";

        if (IsPostBack == false)
        {
            try
            {
                Common commonobj = new Common();
                DataSet ds = new DataSet();
                ds = commonobj.EC_Common_Pass_Query("select max(Proc_last_updated_on) from Raj_Table");
                WucDatePicker1.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        browsefolder(Request.PhysicalApplicationPath);
        Response.Write("</br>");
        Response.Write("Folder Creted Successfully");
    }

    private void browsefolder(string folderpath)
    {
        browsefiles(folderpath);
        string[] dirs;

        dirs = Directory.GetDirectories(folderpath);

        int i = 0;
        for (i = 0; i <= dirs.Length - 1; i++)
        {
            folderpath = dirs[i].ToString();

            if ((folderpath.ToLower() == Request.PhysicalApplicationPath.ToLower().ToString() + "attachments")
                || (folderpath.ToLower() == Request.PhysicalApplicationPath.ToLower().ToString() + "error_log")
                || (folderpath.ToLower() == Request.PhysicalApplicationPath.ToLower().ToString() + "sql"))

            {
                int r = 0;
            }
            else
            {
                if (Directory.GetDirectories(folderpath).Length > 0)
                    browsefolder(folderpath);
                else
                    browsefiles(folderpath);
            }
        }
    }

    private void browsefiles(string folderpath)
    {
        string[] foldernames = folderpath.Trim().Split(new char[] { '\\' });
        path = Request.PhysicalApplicationPath + "\\SQL\\Upload (" + DateTime.Now.Date.ToString("dd MMM yyyy") + ")\\";
        path = path + folderpath.Substring(Request.PhysicalApplicationPath.Length);

        string newfilename;


        string[] files;
        files = Directory.GetFiles(folderpath);

        int i = 0;
        for (i = 0; i <= files.Length - 1; i++)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo file = new FileInfo(files[i].ToString());

            if (file.LastWriteTime.Date >= WucDatePicker1.SelectedDate.Date)
            {
                newfilename = dir.ToString() + "\\" + file.Name;
                if (!dir.Exists)
                    dir.Create();

                if (file.Name.ToLower() == "web.config")
                {
                    int r = 0;
                }
                else
                {
                    file.CopyTo(newfilename);
                }
            }
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        GenerateSQL();
    }

    private void GenerateSQL()
    {
        try
        {
            Common commonobj = new Common();

            DAL objDAL = new DAL();
            DataSet ds = new DataSet();

            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@max_modified_date", SqlDbType.DateTime, 0, WucDatePicker1.SelectedDate) };
            objDAL.RunProc("zzz_stored_procdures_insert", objSqlParam, ref ds);

            FileInfo TheFile = new FileInfo(Filename);
            if (TheFile.Exists)
            {
                File.Delete(Filename);
            }



            FileStream fs1 = new FileStream(Filename, FileMode.OpenOrCreate, FileAccess.Write);

            StreamWriter st2 = new StreamWriter(fs1);

            int i = 0;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                st2.WriteLine("GO");
                st2.WriteLine("if exists (select * From sys.objects where name = '" + dr["sp_name"].ToString() + "')");

                if (dr["xtype"].ToString().ToLower() == "p")
                    st2.WriteLine("Drop Proc " + dr["schemaname"].ToString() + "." + dr["sp_name"].ToString());
                else if (dr["xtype"].ToString().ToLower() == "fn" || dr["xtype"].ToString().ToLower() == "tf")
                    st2.WriteLine("Drop function " + dr["schemaname"].ToString() + "." + dr["sp_name"].ToString());
                else if (dr["xtype"].ToString().ToLower() == "v")
                    st2.WriteLine("Drop view " + dr["schemaname"].ToString() + "." + dr["sp_name"].ToString());
                else if (dr["xtype"].ToString().ToLower() == "tr")
                    st2.WriteLine("Drop trigger " + dr["schemaname"].ToString() + "." + dr["sp_name"].ToString());
            }

            st2.WriteLine("");
            st2.WriteLine("");
            st2.WriteLine("");
            st2.WriteLine("");
            st2.WriteLine("");
            st2.WriteLine("");
            st2.WriteLine("");

            i = 0;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                i = i + 1;
                st2.WriteLine("GO");
                st2.WriteLine("SET ANSI_NULLS ON");
                st2.WriteLine("GO");
                st2.WriteLine("SET QUOTED_IDENTIFIER ON");
                st2.WriteLine("GO");

                st2.WriteLine(dr["sp_content"].ToString());

                st2.WriteLine("");
                //st2.WriteLine("----" + i.ToString() + "--------------------------------------------------------------------------------");
            }

            st2.WriteLine("exec raj_soft_updating");
            st2.Flush();
            st2.Close();





            Response.Write(TheFile + " file generated sucessfully");
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}
