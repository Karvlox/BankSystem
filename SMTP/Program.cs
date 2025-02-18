using System;
using System.Net;
using System.Net.Mail;

class Program
{
    static void Main()
    {
        string smtpServer = "smtp.gmail.com";
        int port = 587; 
        string user = "";
        string pass = "";

        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(user);
        mail.To.Add("");
        mail.Subject = "Prueba de correo SMTP";
        mail.Body = "Este es un correo enviado desde C# con SMTP.";
        mail.IsBodyHtml = false;

        SmtpClient smtpClient = new SmtpClient(smtpServer, port);
        smtpClient.Credentials = new NetworkCredential(user, pass);
        smtpClient.EnableSsl = true;

        try
        {
            smtpClient.Send(mail);
            Console.WriteLine("Correo enviado correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al enviar el correo: {ex.Message}");
        }
    }
}
