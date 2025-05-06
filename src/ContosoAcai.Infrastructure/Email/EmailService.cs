using SendGrid;
using SendGrid.Helpers.Mail;

namespace ContosoAcai.Infrastructure.Email;

public class EmailService(string secret, string senderEmail)
{
    public async Task<string> SendAsync(
        string receiver,
        string subject, 
        string body)
    {
        var client = new SendGridClient(secret);
        var from = new EmailAddress(senderEmail, senderEmail);
        
        var to = new EmailAddress(receiver, receiver);

        var msg = MailHelper.CreateSingleEmailToMultipleRecipients(
            from, 
            [to], 
            subject, 
            null, 
            body);

        var result = await client.SendEmailAsync(msg);
        var bodyContent  = await result.Body.ReadAsStringAsync();

        if (!result.IsSuccessStatusCode)
        {
            return $"Unable to send a email. Status: {result.StatusCode}. Content: {bodyContent}";
        }
        
        return $"Email sent successfully to {receiver}. Status: {result.StatusCode}. Content: {bodyContent}";
    }
}