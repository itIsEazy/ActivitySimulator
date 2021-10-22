namespace ActivitySimulator.Models.Simulator
{
    using System;
    using System.IO;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    public class WebSimulator : IWebSimulator
    {
        public WebSimulator()
        {
            this.Driver = new ChromeDriver();
            this.Random = new Random();
        }

        public IWebDriver Driver { get; set; }

        public Random Random { get; set; }

        public void TryTo(Action func)
        {
            try
            {
                func();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void TryTo<T>(Func<T> func)
        {
            try
            {
                func();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveTextInNewFile(string path, string text)
        {
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(text);
                }
            }
        }
    }
}
