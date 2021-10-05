namespace ActivitySimulator.Services.OLX
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using ActivitySimulator.Data;
    using ActivitySimulator.Data.Models.Olx;
    using ActivitySimulator.Models.Simulator;
    using ActivitySimulator.Services.OLX.Models;

    using OpenQA.Selenium;

    public class OlxSimulator : WebSimulator, IOlxSimulator
    {
        private readonly ApplicationDbContext dbContext;

        public OlxSimulator(ApplicationDbContext dbContext)
        {
            this.User = new OlxUser();
            this.LogInUser();
            this.dbContext = dbContext;
        }

        public OlxUser User { get; set; }

        public List<string> SearchInputs { get; set; }

        public async Task<bool> DeleteOfferAsync(string offerId)
        {
            var offer = this.dbContext.Offers.Find(offerId);
            if (offer == null)
            {
                return false;
            }

            this.dbContext.Offers.Remove(offer);
            await this.dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<string> GetOfferUrlAsync(string offerId)
        {
            return this.dbContext.Offers.Where(x => x.Id == offerId).Select(x => x.Url).FirstOrDefault();
        }

        public async Task SaveOfferAsync(OfferModel offer)
        {
            var dbModelOffer = new Offer();
            dbModelOffer.Title = offer.Title;
            dbModelOffer.Description = offer.Description;
            dbModelOffer.UserName = offer.UserName;
            dbModelOffer.UserAccountUrl = offer.UserAccountUrl;
            dbModelOffer.UserPhoneNumber = offer.UserPhoneNumber;
            dbModelOffer.PriceInfo = offer.PriceInfo;
            dbModelOffer.LocationInfo = offer.LocationInfo;
            dbModelOffer.DateInfo = offer.DateInfo;
            dbModelOffer.VisitationInfo = offer.VisitationInfo;
            dbModelOffer.DeliveryConditionInfo = offer.DeliveryConditionInfo;
            dbModelOffer.Url = offer.Url;

            await dbContext.Offers.AddAsync(dbModelOffer);
            await dbContext.SaveChangesAsync();
        }

        public async Task CommentOfferAsync(string content, string offerId)
        {
            Driver.Url = "https://www.olx.bg/d/ad/hot-wheels-honda-prelude-1998g-honda-prelyud-1998g-novo-CID618-ID8Br3I.html#5e3015fe57";

            Thread.Sleep(1000);

            int counter = 0;
            while (counter < 100)
            {
                try
                {
                    var textarea = Driver.FindElement(By.TagName("textarea"));
                    textarea.SendKeys(content);

                    var form = Driver.FindElement(By.TagName("form"));
                    var btns = form.FindElements(By.TagName("button"));
                    foreach (var btn in btns)
                    {
                        if (btn.Text == "Изпрати" || btn.Text == "Send" || btn.Text == "Send message")
                        {
                            btn.Click();
                            break;
                        }
                    }
                }
                catch (Exception)
                {
                }
            }

            // TODO : add comment in the database
        }

        public async Task<OfferModel> OpenOffer(string url)
        {
            OfferModel model = new OfferModel();

            Driver.Url = url;

            Thread.Sleep(Random.Next(2, 3) * 1000);

            Task.Run(() => this.TryToAcceptCookiesAsync());

            model.Url = Driver.Url;
            model.ImageUrls = await this.TryToExtractAllImageLinksFromOffer();

            await this.TryToCollectOfferInfo(model);
            await this.TryToCollectOfferOwnerInfo(model);
            await this.TryToCollectOfferLocationInfo(model);

            return model;
        }

        private async Task TryToCollectOfferLocationInfo(OfferModel model)
        {
            try
            {
                var containers = Driver.FindElements(By.ClassName("css-1wws9er"));
                var offerlocationInfo = containers[2];

                var ps = offerlocationInfo.FindElements(By.TagName("p"));

                foreach (var p in ps)
                {
                    model.LocationInfo += p.Text + " ";
                }
            }
            catch (Exception)
            {
                // LOG ERROR HERE
            }
        }

        private async Task TryToCollectOfferOwnerInfo(OfferModel model)
        {
            try
            {
                var containers = Driver.FindElements(By.ClassName("css-1wws9er"));
                var offerOwnerInfo = containers[1];

                model.UserAccountUrl = offerOwnerInfo.FindElement(By.TagName("a")).GetAttribute("href");
                model.UserPhoneNumber = await this.TryToGetTheUserPhoneNumber(offerOwnerInfo);
                model.UserName = offerOwnerInfo.FindElement(By.TagName("h2")).Text;
            }
            catch (Exception)
            {
                // LOG ERROR HERR
            }
        }

        private async Task<string> TryToGetTheUserPhoneNumber(IWebElement offerOwnerInfo)
        {
            var buttons = offerOwnerInfo.FindElements(By.TagName("button"));
            var phoneButton = buttons[1];
            phoneButton.Click();

            Thread.Sleep(1300);

            return phoneButton.Text;
        }

        private async Task TryToCollectOfferInfo(OfferModel model)
        {
            try
            {
                var containers = Driver.FindElements(By.ClassName("css-1wws9er"));
                var offerInfo = containers[0];

                var dateTitlePrice = offerInfo.FindElements(By.TagName("div"));

                model.DateInfo = dateTitlePrice[0].Text;
                model.Title = dateTitlePrice[2].Text;
                model.PriceInfo = dateTitlePrice[3].Text;
                model.Description = dateTitlePrice[10].Text;
                model.VisitationInfo = dateTitlePrice[12].Text;

                var lis = offerInfo.FindElements(By.TagName("li"));

                string deliveryAndCondition = "";

                foreach (var li in lis)
                {
                    deliveryAndCondition += li.Text + " ";
                }

                model.DeliveryConditionInfo = deliveryAndCondition;
            }
            catch (Exception)
            {
                // LOG ERROR HERE
            }
        }

        private async Task<List<string>> TryToExtractAllImageLinksFromOffer()
        {
            var allImageLinks = new List<string>();

            try
            {
                var imagesDiv = Driver.FindElement(By.ClassName("swiper-wrapper"));

                var allImgTags = imagesDiv.FindElements(By.TagName("img"));

                foreach (var imgTag in allImgTags)
                {
                    allImageLinks.Add(imgTag.GetAttribute("src"));
                }
            }
            catch (Exception)
            {
                // LOG ERROR HERE
            }

            return allImageLinks;
        }

        public async Task<List<MainPageOfferModel>> CollectAllOffersFor(string searchTerm)
        {
            return await this.CollectAllOffersFor(searchTerm, 10000);
        }

        public async Task<List<MainPageOfferModel>> CollectAllOffersFor(string searchTerm, int maxPage)
        {
            List<MainPageOfferModel> allOffers = new List<MainPageOfferModel>();

            await this.OpenOlxAsync();

            string link = OlxConstants.baseSearchUrl + searchTerm + "/";

            allOffers.AddRange(await this.CollectOffersInformationFromLink(link));

            for (int i = 2; i <= maxPage; i++)
            {
                string currLink = link + "?page=" + i.ToString();

                var currOffers = await this.CollectOffersInformationFromLink(currLink);

                if (currOffers == null)
                {
                    break;
                }

                allOffers.AddRange(currOffers);
            }

            return allOffers;
        }

        private async Task<List<MainPageOfferModel>> CollectOffersInformationFromLink(string url)
        {
            Driver.Url = url;

            Thread.Sleep(Random.Next(2, 3) * 1000);

            if (Driver.Url != url)
            {
                return null;
            }

            return await this.TryToCollectInfoForMainPageOffers();
        }

        private async Task<List<MainPageOfferModel>> TryToCollectInfoForMainPageOffers()
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

            var list = new List<MainPageOfferModel>();

            var offersTable = Driver.FindElement(By.Id("offers_table"));
            var tbody = offersTable.FindElement(By.TagName("tbody"));

            var offersList = tbody.FindElements(By.ClassName("wrap"));

            foreach (var offer in offersList)
            {
                var innerTbody = offer.FindElement(By.TagName("tbody"));

                list.Add(this.TryToCollectInfoForOfferForMainPage(innerTbody));
            }

            return list;
        }

        private MainPageOfferModel TryToCollectInfoForOfferForMainPage(IWebElement innerTbody)
        {
            MainPageOfferModel offerModel = new MainPageOfferModel();

            try
            {
                var trList = innerTbody.FindElements(By.TagName("tr"));
                var tdList = trList[0].FindElements(By.TagName("td"));

                offerModel.ImageUrl = tdList[0].FindElement(By.TagName("img")).GetAttribute("src");
                offerModel.Url = tdList[1].FindElement(By.TagName("a")).GetAttribute("href");
                offerModel.Title = tdList[1].FindElement(By.TagName("strong")).Text;
                offerModel.PriceInfo = tdList[2].FindElement(By.TagName("strong")).Text;

                offerModel.LocationInfo = trList[1].FindElement(By.TagName("p")).Text;
            }
            catch (Exception)
            {
            }

            return offerModel;
        }

        public async Task SearchInOlx()
        {
            await this.OpenOlxAsync();

            Thread.Sleep(Random.Next(1, 2) * 1000);

            Driver.Url = OlxConstants.baseSearchUrl + SearchInputs[0] + "/";
        }

        private void LogInUser()
        {
            Driver.Url = OlxConstants.olxLogInUrl;

            Task.Run(() => this.TryToAcceptCookiesAsync());

            Thread.Sleep(1000);

            var loginForm = Driver.FindElement(By.Id("loginForm"));

            var inputs = loginForm.FindElements(By.TagName("input"));
            inputs[0].SendKeys(this.User.Email);
            inputs[1].SendKeys(this.User.Password);

            var logInBtn = Driver.FindElement(By.Id("se_userLogin"));
            logInBtn.Click();
        }

        private async Task TryToAcceptCookiesAsync()
        {
            int counter = 0;
            while (counter < 10)
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

                counter++;
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
