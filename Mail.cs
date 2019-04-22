using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NT.Global.Net
{
    public  class Mail
    {


        public static void SendMessage(string fromAddress,  string to, string subject, string body, bool sendAsync = false)
        {
            try
            {
                var mailMessage = new MailMessage();
                mailMessage.To.Add(to);
                mailMessage.From = new MailAddress(fromAddress, fromAddress, Encoding.UTF8);
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.SubjectEncoding = Encoding.UTF8;
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.Normal;
                SmtpClient SMTPServer = new SmtpClient(NT.Global.Common.GetPropertyValue("SMTPHost"));
                if (NT.Global.Common.GetPropertyValue("SMTPEnableSsl") != null && NT.Global.Common.GetPropertyValue("SMTPEnableSsl") != "")
                {
                    SMTPServer.EnableSsl = Boolean.Parse(NT.Global.Common.GetPropertyValue("SMTPEnableSsl"));
                }
                if (NT.Global.Common.GetPropertyValue("SMTPPort") != null && NT.Global.Common.GetPropertyValue("SMTPPort") != "")
                {
                    SMTPServer.Port = int.Parse(NT.Global.Common.GetPropertyValue("SMTPPort"));
                }
                if (NT.Global.Common.GetPropertyValue("SMTPUserName") != null && NT.Global.Common.GetPropertyValue("SMTPPassword") != null && NT.Global.Common.GetPropertyValue("SMTPUserName") != "" && NT.Global.Common.GetPropertyValue("SMTPPassword") != "")
                {
                    SMTPServer.Credentials = new System.Net.NetworkCredential(NT.Global.Common.GetPropertyValue("SMTPUserName"), NT.Global.Common.GetPropertyValue("SMTPPassword"));
                }

                if (sendAsync)
                {
                    Task.Run(() =>
                    {
                        try
                        {
                            SMTPServer.Send(mailMessage);
                        }
                        catch (Exception ex)
                        {
                            NT.Global.Logging.LogHandler.PublishException(ex);
                            //throw;
                        }


                    });
                }
                else
                {
                    SMTPServer.Send(mailMessage);
                }

            }
            catch
            {

                throw;
            }
        }


        public static void SendMessage(string fromAddress, string displayName,
            string smtpUser, string smtpPassword, string smtpHost, int smtpPortNumber, 
             string to, string subject, string body, bool sendAsync = false)
        {
            try
            {

               

                var mailMessage = new MailMessage();
                mailMessage.To.Add(to);
                mailMessage.From = new MailAddress(fromAddress, displayName, Encoding.UTF8);
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.SubjectEncoding = Encoding.UTF8;
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.Normal;

                //SmtpClient SMTPServer = new SmtpClient(NT.Global.Common.GetPropertyValue("SMTPHost"));

                //if (NT.Global.Common.GetPropertyValue("SMTPEnableSsl") != null && NT.Global.Common.GetPropertyValue("SMTPEnableSsl") != "")
                //{
                //    SMTPServer.EnableSsl = Boolean.Parse(NT.Global.Common.GetPropertyValue("SMTPEnableSsl"));
                //}
                //if (NT.Global.Common.GetPropertyValue("SMTPPort") != null && NT.Global.Common.GetPropertyValue("SMTPPort") != "")
                //{
                //    SMTPServer.Port = int.Parse(NT.Global.Common.GetPropertyValue("SMTPPort"));
                //}
                //if (NT.Global.Common.GetPropertyValue("SMTPUserName") != null && NT.Global.Common.GetPropertyValue("SMTPPassword") != null && NT.Global.Common.GetPropertyValue("SMTPUserName") != "" && NT.Global.Common.GetPropertyValue("SMTPPassword") != "")
                //{
                //    SMTPServer.Credentials = new System.Net.NetworkCredential(NT.Global.Common.GetPropertyValue("SMTPUserName"), NT.Global.Common.GetPropertyValue("SMTPPassword"));
                //}

                //string smtpUser, string smtpPassword, string smtpHost, int smtpPortNumber, 

                SmtpClient SMTPServer = new SmtpClient(smtpHost);

                if (NT.Global.Common.GetPropertyValue("SMTPEnableSsl") != null && NT.Global.Common.GetPropertyValue("SMTPEnableSsl") != "")
                {
                    SMTPServer.EnableSsl = Boolean.Parse(NT.Global.Common.GetPropertyValue("SMTPEnableSsl"));
                }

                if (!string.IsNullOrEmpty(smtpPortNumber.ToString()))
                {
                    SMTPServer.Port = smtpPortNumber;
                }
                if (!string.IsNullOrEmpty(smtpUser) && !string.IsNullOrEmpty(smtpPassword))
                {
                    SMTPServer.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPassword);
                }

                if (sendAsync)
                {
                    Task.Run(() =>
                    {
                        try
                        {
                            SMTPServer.Send(mailMessage);
                        }
                        catch (Exception ex)
                        {
                            NT.Global.Logging.LogHandler.PublishException(ex);
                            //throw;
                        }


                    });
                }
                else
                {
                    SMTPServer.Send(mailMessage);
                }

            }
            catch
            {

                throw;
            }
        }



	
        public static void SendMessage(MailAddress from, List<MailAddress> to, string subject, string body, bool sendAsync = false)
        {
            try
            {
                var mailMessage = new MailMessage();
                foreach (MailAddress mail in to)
                {
                    mailMessage.To.Add(mail);
                }
                mailMessage.From = from;//new MailAddress("",from,System.Text.Encoding.UTF8);
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.SubjectEncoding = Encoding.UTF8;
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.Normal;
                SmtpClient SMTPServer = new SmtpClient(NT.Global.Common.GetPropertyValue("SMTPHost"));
                if (NT.Global.Common.GetPropertyValue("SMTPEnableSsl") != null && NT.Global.Common.GetPropertyValue("SMTPEnableSsl") != "")
                {
                    SMTPServer.EnableSsl = Boolean.Parse(NT.Global.Common.GetPropertyValue("SMTPEnableSsl"));
                }
                if (NT.Global.Common.GetPropertyValue("SMTPPort") != null && NT.Global.Common.GetPropertyValue("SMTPPort") != "")
                {
                    SMTPServer.Port = int.Parse(NT.Global.Common.GetPropertyValue("SMTPPort"));
                }
                if (NT.Global.Common.GetPropertyValue("SMTPUserName") != null && NT.Global.Common.GetPropertyValue("SMTPPassword") != null && NT.Global.Common.GetPropertyValue("SMTPUserName") != "" && NT.Global.Common.GetPropertyValue("SMTPPassword") != "")
                {
                    SMTPServer.Credentials = new System.Net.NetworkCredential(NT.Global.Common.GetPropertyValue("SMTPUserName"), NT.Global.Common.GetPropertyValue("SMTPPassword"));
                }

                if (sendAsync)
                {
                    Task.Run(() =>
                    {
                        SMTPServer.Send(mailMessage);
                    });
                }
                else
                {
                    SMTPServer.Send(mailMessage);
                }

            }
            catch
            {
                throw;
            }
        }
        public static void SendMessage(MailAddress from, List<MailAddress> to,List<MailAddress> bcc, string subject, string body, bool sendAsync = false)
        {
            try
            {
                var mailMessage = new MailMessage();
                foreach (MailAddress mail in bcc)
                {
                    mailMessage.Bcc.Add(mail);                

                }
                foreach (MailAddress mail in to)
                {
                    mailMessage.To.Add(mail);
                }
                mailMessage.From = from;//new MailAddress("",from,System.Text.Encoding.UTF8);
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.SubjectEncoding = Encoding.UTF8;
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.Normal;
                SmtpClient SMTPServer = new SmtpClient(NT.Global.Common.GetPropertyValue("SMTPHost"));
                if (NT.Global.Common.GetPropertyValue("SMTPEnableSsl") != null && NT.Global.Common.GetPropertyValue("SMTPEnableSsl") != "")
                {
                    SMTPServer.EnableSsl = Boolean.Parse(NT.Global.Common.GetPropertyValue("SMTPEnableSsl"));
                }
                if (NT.Global.Common.GetPropertyValue("SMTPPort") != null && NT.Global.Common.GetPropertyValue("SMTPPort") != "")
                {
                    SMTPServer.Port = int.Parse(NT.Global.Common.GetPropertyValue("SMTPPort"));
                }
                if (NT.Global.Common.GetPropertyValue("SMTPUserName") != null && NT.Global.Common.GetPropertyValue("SMTPPassword") != null && NT.Global.Common.GetPropertyValue("SMTPUserName") != "" && NT.Global.Common.GetPropertyValue("SMTPPassword") != "")
                {
                    SMTPServer.Credentials = new System.Net.NetworkCredential(NT.Global.Common.GetPropertyValue("SMTPUserName"), NT.Global.Common.GetPropertyValue("SMTPPassword"));
                }

                if (sendAsync)
                {
                    Task.Run(() =>
                    {
                        SMTPServer.Send(mailMessage);
                    });
                }
                else
                {
                    SMTPServer.Send(mailMessage);
                }

            }
            catch
            {
                throw;
            }
        }
    }
}
