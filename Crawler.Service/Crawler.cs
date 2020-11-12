using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Transactions;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Crawler.Service
{
    public interface ICrawler
    {
    }

    public class Crawler : ICrawler
    {
        public async Task Authenticate()
        {
            var link1 = "https://www.instagram.com";
            var link = "https://www.instagram.com/accounts/login/";
            var login_url = "https://www.instagram.com/accounts/login/ajax/";

            var username = "s";
            var password = "";

            using (var driver = new ChromeDriver("C:\\Users\\aldig\\Downloads\\chromedriver_win32 (1)"))
            {
                //Navigate to DotNet website
                driver.Navigate().GoToUrl(link);
                //Click the Get Started button

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                bool jsLoaded = (bool)js.ExecuteScript("return (document.readyState == \"complete\" || document.readyState == \"interactive\")");

                //WebDriverWait wait = new WebDriverWait(driver, 30);

                //try
                //{
                //    do
                //    {
                //        if (driver.FindElementByName("username").Enabled)
                //        {
                            driver.FindElementByName("username").SendKeys(username);
                            driver.FindElementByName("password").SendKeys(password);
                            driver.FindElementByXPath("//*[@id='loginForm']/div/div[3]/button").Click();
                        //}

                //    } while (!driver.FindElementByName("username").Enabled);
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex);
                //    //throw;
                //}




                //new WebDriverWait(driver).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//input[@name='username']"))).SendKeys(username);


                var waitTime = 100;
                new WebDriverWait(driver, TimeSpan.FromMilliseconds(waitTime)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='react-root']/section/main/div/div/div/div/button"))).Click();


                driver.FindElementByXPath("/html/body/div[4]/div/div/div/div[3]/button[2]").Click();

                driver.FindElementByXPath("//*[@id='react-root']/section/nav/div[2]/div/div/div[2]/input").SendKeys("#aldiger");
                driver.FindElementByXPath("//*[@id='react-root']/section/nav/div[2]/div/div/div[2]/input").SendKeys(Keys.Enter);

                // Get Started section is a multi-step wizard
                // The following sections will find the visible next step button until there's no next step button left
                IWebElement nextLink = null;
                do
                {
                    nextLink?.Click();
                    nextLink = driver.FindElements(By.CssSelector(".step:not([style='display:none;']):not([style='display: none;']) .step-select")).FirstOrDefault();
                } while (nextLink != null);
                

                driver.FindElementByXPath("//*[@id='react-root']/section/nav/div[2]/div/div/div[2]/div[4]/div/a[1]").Click();

                
                // on the last step, grab the title of the step wich should be equal to "Next steps"
                var lastStepTitle = driver.FindElement(By.CssSelector(".step:not([style='display:none;']):not([style='display: none;']) h2")).Text;

                // verify the title is the expected value "Next steps"
                //Assert.AreEqual(lastStepTitle, "Next steps");
            }

            //var cookies = new CookieContainer();
            //HttpClientHandler handler = new HttpClientHandler {CookieContainer = cookies, UseCookies = true};
            //using ( var client = new HttpClient(handler))
            //{

            //    client.DefaultRequestHeaders.Add("Accept",
            //        "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
            //    client.DefaultRequestHeaders.Add("User-Agent",
            //        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.131 Safari/537.36");
            //    client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9,it;q=0.8,sq;q=0.7");
   


            //    var payload = new List<KeyValuePair<string, string>>();
            //    payload.Add(new KeyValuePair<string,string>("username", username));
            //    payload.Add(new KeyValuePair<string, string>("enc_password", $"#PWD_INSTAGRAM_BROWSER:0:{DateTime.Now.TimeOfDay}:{password}"));
            //    payload.Add(new KeyValuePair<string, string>("queryParams", ""));
            //    payload.Add(new KeyValuePair<string, string>("optIntoOneTap", "false"));

                //var payload = new Dictionary<string,string>
                //{
                //    "username": "<USERNAME HERE>",
                //    "enc_password":   //# <-- note the '0' - that means we want to use plain passwords
                //    'queryParams': { },
                //    'optIntoOneTap': 'false'
                //}


                //HtmlWeb web = new HtmlWeb();
                //HtmlDocument doc = web.Load(link);
                //await client.GetAsync(link1);
                //var result =await client.GetAsync(link);

                //result = await client.PostAsync(login_url, new FormUrlEncodedContent(payload));

                //var ddd = await result.Content.ReadAsStringAsync();
                
            //}
        }


        public void Search()
        {

        }

        public void GetData()
        {

        }

        private HttpClient GetClient()
        {
            var proxyIp = "";
            var ipUsername = "";
            var ipPassword = "";
            var proxy = new WebProxy()
            {
                Address = new Uri($"http://{proxyIp}"), /*:{proxyPort}*/
                BypassProxyOnLocal = false,
                UseDefaultCredentials = false,

                // *** These creds are given to the proxy server, not the web server ***
                Credentials = new NetworkCredential(
                    userName: ipUsername,
                    password: ipPassword)
            };

            // Now create a client handler which uses that proxy

            var httpClientHandler = new HttpClientHandler()
            {
                //Proxy = proxy,
            };


            var client = new HttpClient(/*handler: httpClientHandler, disposeHandler: true*/);

            client.DefaultRequestHeaders.Add("Accept",
                "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
            client.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.131 Safari/537.36");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9,it;q=0.8,sq;q=0.7");

            //var links = client.GetStringAsync(portal.Url + urlType.UrlPortion + url.PropertyCode + ".html").Result;
            return client;
        }
    }
}
