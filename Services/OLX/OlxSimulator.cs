﻿namespace ActivitySimulator.Services.OLX
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using ActivitySimulator.Models.Simulator;

    using OpenQA.Selenium;

    public class OlxSimulator : WebSimulator, IOlxSimulator
    {
        public OlxSimulator() : this(OlxConstants.defaultLegoSearch)
        {

        }

        public OlxSimulator(string search)
        {
            this.SearchInputs = new List<string>
            {
                search,
            };
        }

        public List<string> SearchInputs { get; set; }

        public async Task CollectAllOffersFor(string searchTerm)
        {
            // there is 2 tables 
            // 1st is for promoted offers
            // 2nd is for all offers -> id = "offers_table"

            // get all those <tr> where THERE IS class "wrap" (all <tr> tags with NO "wrap" class ARE Adds) 

            // in every tr there is

            // 1 td which holds the img for the offer
            // <a> with href with the link for the offer
            // <strong> with the name
            // <p> with class"price" for the price
            // <p> wth class "lheight16" for the date

            // collect all those
            // if You want to collect every sinlge offer (meaning all foreach all the pages)
            // the link is : "https://www.olx.bg/ads/q-lego/?page=2" and then the same procedure
        }

        public async Task SearchInOlx()
        {
            await this.OpenOlxAsync();

            Thread.Sleep(Random.Next(1, 2) * 1000);

            Driver.Url = OlxConstants.baseSearchUrl + SearchInputs[0] + "/";
        }

        private async Task TryToAcceptCookiesAsync()
        {
            try
            {
                var btn = Driver.FindElement(By.Id("onetrust-accept-btn-handler")); // they use this id attribute so we are lucky to get it that ez

                btn.Click();
            }
            catch (Exception)
            {
                // log error here
            }
        }

        private async Task OpenOlxAsync()
        {
            Driver.Url = OlxConstants.olxUrl;

            Thread.Sleep(Random.Next(3, 4) * 1000);

            await this.TryToAcceptCookiesAsync();
        }
    }
}