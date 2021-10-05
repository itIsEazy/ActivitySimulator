namespace ActivitySimulator.Services.Common.Models
{
    using System.IO;
    using System.Text.RegularExpressions;

    public class LogInModel
    {
        public LogInModel(string pattern) : this(Constants.structurePath, pattern)
        {
            
        }

        public LogInModel(string path, string pattern)
        {
            this.ExtractLogInInformation(path, pattern);
        }

        public string Email { get; set; }

        public string Password { get; set; }

        private void ExtractLogInInformation(string path, string pattern)
        {
            string text = File.ReadAllText(path);

            Regex regex = new Regex(pattern);
            Match match = regex.Match(text);

            for (int i = 0; i < match.Groups.Count; i++)
            {
                var value = match.Groups[i];

                if (i == 1)
                {
                    this.Email = value.ToString();
                }

                if (i == 2)
                {
                    this.Password = value.ToString();
                }
            }
        }
    }
}
