namespace ActivitySimulator.Models.Simulator
{
    using System;

    using OpenQA.Selenium;

    public interface IWebSimulator
    {
        IWebDriver Driver { get; set; }

        Random Random { get; set; }
    }
}
