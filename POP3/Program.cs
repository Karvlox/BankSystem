using System;
using MailKit.Net.Pop3;
using MimeKit;

class Program
{
    static void Main()
    {
        string host = "pop.gmail.com";
        int port = 995;
        string user = "";
        string pass = "";

        using (var client = new Pop3Client())
        {
            client.Connect(host, port, true);
            client.Authenticate(user, pass);

            int messageCount = client.Count;
            Console.WriteLine($"Total de correos: {messageCount}");

            for (int i = 0; i < messageCount; i++)
            {
                MimeMessage message = client.GetMessage(i);
                Console.WriteLine($"De: {message.From}");
                Console.WriteLine($"Asunto: {message.Subject}");
                Console.WriteLine($"Contenido: {message.TextBody}");
            }

            client.Disconnect(true);
        }
    }
}
