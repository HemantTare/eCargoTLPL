using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;

/// <summary>
/// Summary description for MailSender
/// </summary>
public class MailSender
{
    string EmailID = "";
    string ReferenceNo = "";
    string MailSubject = "";
    string htmlmsg = "";
    byte CallFrom;

	public MailSender(string EmailID,string ReferenceNo, byte CallFrom)
	{
        this.EmailID = EmailID;
        this.ReferenceNo = ReferenceNo;
        this.CallFrom = CallFrom;

        SendeMailToClient();
	}


    private void SendeMailToClient()
    {

        string MailFrom = System.Configuration.ConfigurationManager.AppSettings["MailAddressFrom"];
        string MailFromDisplayName = System.Configuration.ConfigurationManager.AppSettings["MailAddressFromDisplayName"];
        string SMTPServer = System.Configuration.ConfigurationManager.AppSettings["SMTPServer"];
        string SMTPPort = System.Configuration.ConfigurationManager.AppSettings["SMTPPort"];

        try
        {
            if (EmailID.Trim() != "")
            {
                MailMessage message = new MailMessage();

                message.From = new MailAddress(MailFrom, MailFromDisplayName);
                message.To.Add(new MailAddress(EmailID));

                GetSubjectAndBody();
                message.Subject = MailSubject;
                message.Body = htmlmsg;
                message.IsBodyHtml = true;

                SmtpClient client = new SmtpClient(SMTPServer, Convert.ToInt32(SMTPPort));
                client.Send(message);
                message.Dispose();
            }
        }
        catch (Exception ex)
        {
            string d;
        }
    }

    private void GetSubjectAndBody()
    {
        if (CallFrom == 1) //compliant
        {
            htmlmsg = htmlmsg + "<HTML><HEAD><TITLE></TITLE></HEAD>";
            htmlmsg = htmlmsg + "<BODY>";
            htmlmsg = htmlmsg + "<b>Your Complain has been successfully registered with us.</b>";
            htmlmsg = htmlmsg + "</br>";
            htmlmsg = htmlmsg + "<b>Your Ticket number is: " + ReferenceNo + "</b>";
            htmlmsg = htmlmsg + "</br>";
            htmlmsg = htmlmsg + "<b>Please provide this ticket number for all your further queries</b>";
            htmlmsg = htmlmsg + "</BODY></HTML>";

            MailSubject = "Your Ticket Number Is: " + ReferenceNo;
        }
        else if (CallFrom == 2) //Ticket Closing
        {
            htmlmsg = htmlmsg + "<HTML><HEAD><TITLE></TITLE></HEAD>";
            htmlmsg = htmlmsg + "<BODY>";
            htmlmsg = htmlmsg + "<b>Your Ticket number i.e: " + ReferenceNo + " has been closed.</b>";
            htmlmsg = htmlmsg + "</BODY></HTML>";

            MailSubject = "Your Ticket number i.e: " + ReferenceNo + " has been closed.";
        }
        else if (CallFrom == 3) //Pick Request
        {
            htmlmsg = htmlmsg + "<HTML><HEAD><TITLE></TITLE></HEAD>";
            htmlmsg = htmlmsg + "<BODY>";
            htmlmsg = htmlmsg + "<b>Your Pickup Request has been successfully registered with us</b>";
            htmlmsg = htmlmsg + "</br>";
            htmlmsg = htmlmsg + "<b>Your Pickup number is: " + ReferenceNo + ".</b>";
            htmlmsg = htmlmsg + "</BODY></HTML>";

            MailSubject = "Pickup Request Registration Confirmation";
        }
    }

    //private static bool isEmail(string inputEmail)
    //{
    //    inputEmail = NulltoString(inputEmail);
    //    string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
    //          @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
    //          @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
    //    RegexStringValidator re = new RegexStringValidator(strRegex);
    //    if (re.Validate(inputEmail))
    //        return (true);
    //    else
    //        return (false);
    //}
}
