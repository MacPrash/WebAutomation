using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using TechTalk.SpecFlow;
using Xunit;


namespace GlassLewisWebPage
{
    [Binding]
    public class GlassWebSteps
    {
//        [assembly: CollectionBehavior(DisableTestParallelization = true)]
        private IWebDriver _driver;
        string AppRoot = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        string strDriverPath = null;
        //        List<string> columnValues = new List<string>();
        string p1;
        List<string> countryValues = new List<string>();

        public IWebElement waitForPageUntilElementIsVisisble(By identifier, int maxsec)
        {
            return new WebDriverWait(_driver, TimeSpan.FromSeconds(maxsec)).Until(ExpectedConditions.ElementExists((identifier)));
        }

        [Given(@"user is on the landing page for WD site on ""(.*)""")]
        public void GivenUserIsOnTheLandingPageForWDSiteOn(string p0)
        {

            if(p0.ToLower() == "chrome")
            {
                _driver = new ChromeDriver();
            }
            else if(p0.Equals("IE", StringComparison.InvariantCultureIgnoreCase))
            {
                InternetExplorerOptions options = new InternetExplorerOptions()
                {
                    EnableNativeEvents = true,
                    IgnoreZoomLevel = true
                };
//                System.Environment.SetEnvironmentVariable("webdriver.ie.driver", @"C:\Windows");
                _driver = new InternetExplorerDriver(@"C:\Windows", options, TimeSpan.FromMinutes(3.0));
            }
            else
            {
                System.Environment.SetEnvironmentVariable("webdriver.gecko.driver", @"C:\Windows");
                _driver = new FirefoxDriver();

            }
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl("https://viewpoint.glasslewis.com/WD/?siteId=DemoClient");
        }

        [Given(@"the country filter is available")]
        public void GivenTheCountryFilterIsAvailable()
        {
//            waitForPageUntilElementIsVisisble(By.Id("txt-multiselect-static-search-CountryFilter"), 20);
                        System.Threading.Thread.Sleep(10000);
            IWebElement Country = _driver.FindElement(By.Id("txt-multiselect-static-search-CountryFilter"));
            bool filterName = Country.Enabled;
            Assert.True(filterName);
        }

        [When(@"user selects ""(.*)"" from the Country filter list on left panel")]
        public void WhenUserSelectsFromTheCountryFilterListOnLeftPanel(string p0)
        {
//            waitForPageUntilElementIsVisisble(By.Id("filter-country"), 5);
            System.Threading.Thread.Sleep(3000);
            IWebElement countryName1 = _driver.FindElement(By.Id("filter-country"));
            IList<IWebElement> web = countryName1.FindElements(By.TagName("label"));
            System.IO.File.WriteAllText(@"D:\123.txt", web.Count.ToString());
            foreach (var label in web)
            {
//                System.IO.File.WriteAllText(@"D:\123.txt", label.Text);
                if (label.Text.Trim() == p0)
                {
                    p1 = p0;
                    label.Click();
                    //                    break;
                }
                else
                {
                    countryValues.Add(label.Text);
                }

            }
        }

        [When(@"clicks on Update button for the country filter list")]
        public void WhenClicksOnUpdateButtonForTheCountryFilterList()
        {
            //            System.Threading.Thread.Sleep(2000);
            waitForPageUntilElementIsVisisble(By.Id("filter-country"), 2);
            IWebElement updateButton = _driver.FindElement(By.Id("filter-country"));
            var up1 = updateButton.FindElements(By.TagName("button"));
            foreach (var label in up1)
            {

                if (label.Text == "Update")
                {

                    label.Click();
                    break;
                }
            }

        }

        [Then(@"the grid display all meetings that are associated with the country ""(.*)""")]
        public void ThenTheGridDisplayAllMeetingsThatAreAssociatedWithTheCountry(string p0)
        {
            //int count = 0;
            IWebElement updateButton = _driver.FindElement(By.ClassName("k-grid-content"));
            IList<IWebElement> rows = updateButton.FindElements(By.TagName("tr"));

            int rows_count = rows.Count;

            Console.WriteLine(rows_count);



            foreach (var table in rows)
            {

                IList<IWebElement> columns = table.FindElements(By.TagName("td"));
                //columnValues.Add(columns[3].Text);
                Assert.Equal(columns[3].Text.ToString(), p0);

            }

        }

        [Then(@"no meetings associated with any other country appears on the list")]
        public void ThenNoMeetingsAssociatedWithAnyOtherCountryAppearsOnTheList()
        {

            Assert.DoesNotContain(p1, countryValues);
            _driver.Quit();
            System.Threading.Thread.Sleep(3000);
        }


        [When(@"user clicks the Company Name ""(.*)"" hyperlink")]
        public void WhenUserClicksTheCompanyNameHyperlink(string p2)
        {
//            waitForPageUntilElementIsVisisble(By.XPath("/html/body/div[2]/div[3]/div[2]/div[1]/div[3]/a[3]/span"), 10);
            System.Threading.Thread.Sleep(6000);
            //            System.IO.File.WriteAllText(@"D:\123.txt", _driver.Title);
            IWebElement nextButton = _driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div[2]/div[1]/div[3]/a[3]/span"));
            IWebElement updateButton = _driver.FindElement(By.ClassName("k-grid-content"));
            bool flag = true;
            do
            {
//                waitForPageUntilElementIsVisisble(By.TagName("a"), 2);
                //                System.Threading.Thread.Sleep(2000);
                IList<IWebElement> aTags = updateButton.FindElements(By.TagName("a"));
                foreach (var label in aTags)
                {
//                    System.IO.File.WriteAllText(@"D:\123.txt", label.Text);
                    if (label.Text == p2)
                    {

                        label.Click();
                        flag = false;
                        break;

                    }
                }
                if (flag)
                {
                    nextButton.Click();
                }
                else
                {
                    break;
                }
            } while (nextButton.Enabled);
        }

        [Then(@"the user land onto the ""(.*)"" vote card page")]
        public void ThenTheUserLandOntoTheVoteCardPage(string p2)
        {
//            waitForPageUntilElementIsVisisble(By.Id("wd-site-page"), 10);

            System.Threading.Thread.Sleep(6000);
            IWebElement webChek = _driver.FindElement(By.Id("lnk-bar-next"));
//            System.IO.File.WriteAllText(@"E:\123.txt", webChek.Enabled.ToString());
            Assert.True(webChek.Enabled);
        }

        [Then(@"""(.*)"" should appear in the top banner")]
        public void ThenShouldAppearInTheTopBanner(string p2)
        {
 //           waitForPageUntilElementIsVisisble(By.Id("detail-issuer-name"), 5);
            System.Threading.Thread.Sleep(4000);
//            System.IO.File.WriteAllText(@"D:\123.txt", "Heloo");
            IWebElement finalChek = _driver.FindElement(By.Id("detail-issuer-name"));
//            System.IO.File.WriteAllText(@"D:\123.txt", finalChek.Text);
            Assert.Equal(finalChek.Text, p2);

            _driver.Quit();
        }

    }
}
