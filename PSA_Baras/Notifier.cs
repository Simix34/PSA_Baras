using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PSA_Baras
{
    public class Notifier
    {
		private const string EmailFrom = "notifications579@gmail.com";
		private const string EmailTo = "gen35a@gmail.com";

		private readonly MailMessage mail;
		private readonly SmtpClient smtp;
		public Notifier()
		{
			//smtp = new SmtpClient
			//{
			//	Port = 587,
			//	EnableSsl = true,
			//	DeliveryMethod = SmtpDeliveryMethod.Network,
			//	UseDefaultCredentials = false,
			//	Credentials = new NetworkCredential(EmailFrom, EmailFromPassword),
			//	Host = "smtp.gmail.com"
			//};
			smtp = new SmtpClient("smtp.mailtrap.io", 2525)
			{
				Credentials = new NetworkCredential("2292c1c050be2d", "93dab865b4bddd"),
				EnableSsl = false,
			};
			mail = new MailMessage
			{
				From = new MailAddress(EmailFrom),
				IsBodyHtml = true
			};
			mail.To.Add(new MailAddress(EmailTo));
		}
		public void SendMessage(string subject, string body)
		{
			mail.Subject = subject;
			mail.Body = body;

			smtp.Send(mail);
		}
	}
}
