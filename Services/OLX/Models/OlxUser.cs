namespace ActivitySimulator.Services.OLX.Models
{
    using ActivitySimulator.Services.Common.Models;

    public class OlxUser
    {
        public OlxUser()
        {
            this.GetEmailAndPassword();
        }

        public string Email { get; set; }

        public string Password { get; set; }

        private void GetEmailAndPassword()
        {
            var loginModel = new LogInModel(OlxConstants.olxMainLogInPattern);
            this.Email = loginModel.Email;
            this.Password = loginModel.Password;
        }
    }
}
