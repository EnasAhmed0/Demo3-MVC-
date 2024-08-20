using Demo3MVC.DAL.Models;
using System.Buffers.Text;
using System.Net;
using System.Net.Mail;
using System.Runtime.Intrinsics.X86;

namespace Demo3MVC.PL.Helpers
{
	public static class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			//h ttps://www.gmass.co/blog/gmail-smtp/
			// The Settings
			//1. SMTP Server Address: smtp.gmail.com
			//2. Gmail SMTP port: 465(SSL) for Secure or 587(TLS) for not secure

			var Client = new SmtpClient("smtp.gmail.com", 587);
			Client.EnableSsl = true; //at the main server: You Must have A Certificate you are buying it and installin on the server ==> Its benfit is Make your Email Encrypted From Hacks
			// show the video part 12 in session 6 AliaaTarek❤
			Client.Credentials = new NetworkCredential("mohammedyassen444@gmail.com", "xboqwhastyfkoppc");
			Client.Send("mohammedyassen444@gmail.com", email.To, email.Subject, email.Body);
		}
	}
}
