using System;

namespace Demo.Solid;

public class ReportGenerator
{
    private readonly MailSender mailSender;
    public ReportGenerator(MailSender mailSender)
    {
        this.mailSender = mailSender;
    }
    public void Send()
    {
        //MailSender mailSender = new MailSender();
        mailSender.Send();
    }

}

public class MailSender
{
   public void Send()
    {
        System.Console.WriteLine("Gönderildi");
    }
}
