using System.Threading.Tasks;
using Hotel.Common;
using Hotel.Web.ViewModels.InputModels.Contact;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Hotel.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IConfiguration configuration;

        public ContactController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var apiKey = this.configuration.GetValue<string>("SendGrid:ApiKey");

            var client = new SendGridClient(apiKey);
            var name = model.FirstName + " " + model.LastName;
            var from = new EmailAddress(model.Email, name);
            var subject = model.Title;
            var to = new EmailAddress("dimitarkrustanov@abv.bg", "Hotel");
            var plainTextContent = model.Content;
            var htmlContent = $"<p>{model.Content}</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            await client.SendEmailAsync(msg);

            this.TempData["InfoMessage"] = GlobalConstants.SuccessfullySentAnEmail;

            return this.Redirect("/");
        }
    }
}
