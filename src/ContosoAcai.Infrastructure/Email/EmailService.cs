using SendGrid;
using SendGrid.Helpers.Mail;

namespace ContosoAcai.Infrastructure.Email;

public class EmailService()
{
    public async Task SendAsync(
        string secret,
        string senderEmail,
        string receiverEmail,
        string subject, 
        string body)
    {
        var client = new SendGridClient(secret);
        var from = new EmailAddress(senderEmail, senderEmail);
        
        var to = new EmailAddress(receiverEmail, receiverEmail);

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
            throw new Exception($"Unable to send a email. Status: {result.StatusCode}. Content: {bodyContent}");
        }
    }
}