namespace Hotel.Web.Controllers
{
    using System.Threading.Tasks;

    using Common;
    using ViewModels.InputModels.Contact;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using SendGrid;
    using SendGrid.Helpers.Mail;

    public class ContactController : Controller
    {
        private readonly IConfiguration configuration;

        public ContactController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var apiKey = configuration.GetValue<string>("ExternalProviders:SendGrid:ApiKey");

            var client = new SendGridClient(apiKey);
            var name = model.FirstName + " " + model.LastName;
            var from = new EmailAddress(model.Email, name);
            var subject = model.Title;
            var to = new EmailAddress("dimitarkrustanov@gmail.com", "Hotel");
            var plainTextContent = model.Content;
            var htmlContent = $"<p>{model.Content}</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            await client.SendEmailAsync(msg);

            TempData["InfoMessage"] = GlobalConstants.SuccessfullySentAnEmail;

            return Redirect("/");
        }
    }
}
