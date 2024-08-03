using Newtonsoft.Json.Linq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading.Tasks;

namespace MyDramaListTests
{
    [TestFixture]
    public class Tests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public async Task Setup()
        {

            driver = new FirefoxDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(100));

        }

        [TearDown]
        public void Teardown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
                driver = null;
            }
        }

        [Test]
        public void Search_CrashLandingOnYou_SuccessfulSearch()
        {
            // Navigate to MyDramaList
            driver.Navigate().GoToUrl("https://mydramalist.com/");

            // Wait until the page is fully loaded
            wait.Until(driver => (string)((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState") == "complete");


            // Find the search input field
            IWebElement searchInput = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div[1]/div/div/div/form/div/div/input"));

            // Enter a search query
            string searchQuery = "Crash Landing on You";
            searchInput.SendKeys(searchQuery);

            // Find the search button
            IWebElement searchButton = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div[1]/div/div/div/form/div/div/span/button"));

            // Click the search button
            searchButton.Click();


            // Wait for the search results to be visible or for the URL to change
            wait.Until(driver => driver.Url.Contains("/search") && (string)((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState") == "complete");

            // Wait for the specific search result to be visible
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("mdl-35729")));

            // Verify that the search result contains the expected title
            IWebElement resultTitle = driver.FindElement(By.CssSelector("#mdl-35729 .title a"));
            ClassicAssert.IsTrue(resultTitle.Text.Contains("Crash Landing on You"), "Expected result title not found.");

        }

        [Test]
        public void AdvancedSearch_TwinklingWatermelonAndDrama_SuccessfulSearch()
        {
            // Navigate to the advanced search page
            driver.Navigate().GoToUrl("https://mydramalist.com/search");

            // Wait until the page is fully loaded
            wait.Until(driver => (string)((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState") == "complete");

            // Enter a search keyword
            IWebElement searchInput = driver.FindElement(By.XPath("//*[@id=\"advanced-search\"]/div[2]/div/div/div/input"));
            string searchQuery = "Twinkling Watermelon";
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].value = 'Twinkling Watermelon';", searchInput);

            // Click on Title
            IWebElement titleMenu = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"advanced-search\"]/div[3]/div[2]/div[1]/a")));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", titleMenu);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", titleMenu);

            // Click the "Dramas" filter
            IWebElement dramasFilter = driver.FindElement(By.XPath("//*[@id=\"advanced-search\"]/div[4]/div[1]/div[2]/a[1]"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", dramasFilter);


            // Click the "Search" button
            IWebElement searchButton = driver.FindElement(By.XPath("//*[@id=\"advanced-search\"]/div[5]/div/div/button[1]"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", searchButton);

            // Wait for the search results to be visible or for the URL to change
            wait.Until(driver => driver.Url.Contains("/search") && (string)((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState") == "complete");

            // Wait for the specific search result to be visible
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("mdl-739603")));

            // Verify the search results contain the expected title
            IWebElement resultTitle = driver.FindElement(By.CssSelector("#mdl-739603 > div > div > div.col-xs-9.row-cell.content > h6 > a:nth-child(1)"));
            ClassicAssert.IsTrue(resultTitle.Text.Contains("Twinkling Watermelon"), "Expected result title not found.");
        }

        [Test]
        public void AdvancedSearch_LeeJongSukAndKorean_SuccessfulSearch()
        {
            // Navigate to the advanced search page
            driver.Navigate().GoToUrl("https://mydramalist.com/search");

            // Wait until the page is fully loaded
            wait.Until(driver => (string)((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState") == "complete");

            // Enter a search keyword
            IWebElement searchInput = driver.FindElement(By.XPath("//*[@id=\"advanced-search\"]/div[2]/div/div/div/input"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].value = 'Lee Jong Suk';", searchInput);

            // Click on Pepole
            IWebElement pepoleMenu = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"advanced-search\"]/div[3]/div[2]/div[2]/a")));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", pepoleMenu);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", pepoleMenu);

            // Click the "Korean" filter
            IWebElement koreanFilter = driver.FindElement(By.XPath("//*[@id=\"advanced-search\"]/div[4]/div[1]/div[2]/a[1]"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", koreanFilter);


            // Click the "Search" button
            IWebElement searchButton = driver.FindElement(By.XPath("//*[@id=\"advanced-search\"]/div[5]/div/div/button[1]"));
            searchButton.Click();

            // Wait for the search results to be visible or for the URL to change
            wait.Until(driver => driver.Url.Contains("/search") && (string)((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState") == "complete");


            // Wait until the search results are visible
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#content > div > div.container-fluid > div > div.col-lg-8.col-md-8 > div > div:nth-child(1) > div > div > div.col-xs-9.row-cell.content > h6 > a")));

            // Verify the search results contain the expected title
            IWebElement resultTitle = driver.FindElement(By.CssSelector("#content > div > div.container-fluid > div > div.col-lg-8.col-md-8 > div > div:nth-child(1) > div > div > div.col-xs-9.row-cell.content > h6 > a"));
            ClassicAssert.IsTrue(resultTitle.Text.Contains("Lee Jong Suk"), resultTitle.Text + "Expected result title not found.");
        }

        [Test]
        public void AdvancedSearch_IUAndFemale_SuccessfulSearch()
        {
            // Navigate to the advanced search page
            driver.Navigate().GoToUrl("https://mydramalist.com/search");

            // Wait until the page is fully loaded
            wait.Until(driver => (string)((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState") == "complete");

            // Enter a search keyword
            IWebElement searchInput = driver.FindElement(By.XPath("//*[@id=\"advanced-search\"]/div[2]/div/div/div/input"));
            searchInput.SendKeys("IU");

            // Click on Pepole
            IWebElement pepoleMenu = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"advanced-search\"]/div[3]/div[2]/div[2]/a")));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", pepoleMenu);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", pepoleMenu);

            // Click the "Female" Option
            IWebElement femaleOption = driver.FindElement(By.XPath("//*[@id=\"advanced-search\"]/div[4]/div[2]/div[2]/a[2]"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", femaleOption);


            // Click the "Search" button
            IWebElement searchButton = driver.FindElement(By.XPath("//*[@id=\"advanced-search\"]/div[5]/div/div/button[1]"));
            searchButton.Click();

            // Wait for the search results to be visible or for the URL to change
            wait.Until(driver => driver.Url.Contains("/search") && (string)((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState") == "complete");


            // Wait until the search results are visible
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#content > div > div.container-fluid > div > div.col-lg-8.col-md-8 > div > div:nth-child(1) > div > div > div.col-xs-9.row-cell.content > h6 > a")));

            // Verify the search results contain the expected title
            IWebElement resultTitle = driver.FindElement(By.CssSelector("#content > div > div.container-fluid > div > div.col-lg-8.col-md-8 > div > div:nth-child(1) > div > div > div.col-xs-9.row-cell.content > h6 > a"));
            ClassicAssert.IsTrue(resultTitle.Text.Contains("IU"), resultTitle.Text + "Expected result title not found.");
        }

        [Test]
        public void AdvancedSearch_MDLAndInterviews_SuccessfulSearch()
        {
            // Navigate to the advanced search page
            driver.Navigate().GoToUrl("https://mydramalist.com/search");

            // Wait until the page is fully loaded
            wait.Until(driver => (string)((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState") == "complete");

            // Wait until the search input is visible
            //wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"advanced-search\"]/div[2]/div/div/div/input")));

            // Enter a search keyword
            IWebElement searchInput = driver.FindElement(By.XPath("//*[@id=\"advanced-search\"]/div[2]/div/div/div/input"));
            searchInput.SendKeys("MDL");


            // Click on Article
            IWebElement articleMenu = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"advanced-search\"]/div[3]/div[2]/div[3]/a")));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", articleMenu);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", articleMenu);

            // Click the "Interviews" Option
            IWebElement interviewsOption = driver.FindElement(By.XPath("//*[@id=\"advanced-search\"]/div[4]/div[1]/div[2]/a[1]"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", interviewsOption);


            // Click the "Search" button
            IWebElement searchButton = driver.FindElement(By.XPath("//*[@id=\"advanced-search\"]/div[5]/div/div/button[1]"));
            searchButton.Click();

            // Wait for the search results to be visible or for the URL to change
            wait.Until(driver => driver.Url.Contains("/search") && (string)((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState") == "complete");


            // Wait until the search results are visible
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#content > div > div.container-fluid > div > div.col-lg-8.col-md-8 > div > div:nth-child(1) > div > div.list-body > h6 > a")));

            // Verify the search results contain the expected title
            IWebElement resultTitle = driver.FindElement(By.CssSelector("#content > div > div.container-fluid > div > div.col-lg-8.col-md-8 > div > div:nth-child(1) > div > div.list-body > h6 > a"));
            ClassicAssert.IsTrue(resultTitle.Text.Contains("MDL"), resultTitle.Text + "Expected result title not found.");
        }

        [Test]
        public void TopShows_NA_SuccessfullyDisplayed()
        {
            // Navigate to the Top Shows page
            driver.Navigate().GoToUrl("https://mydramalist.com/shows/top");

            // Wait until the shows section is visible
            wait.Until(driver => driver.FindElement(By.XPath("//*[@id=\"content\"]/div/div[2]/div/div[1]/div")).Displayed);

            IWebElement resultBody = driver.FindElement(By.XPath("//*[@id=\"content\"]/div/div[2]/div/div[1]/div"));

            // Find all show elements on the page
            var showElements = resultBody.FindElements(By.CssSelector(".box-body"));

            // Ensure that shows are found
            ClassicAssert.IsNotEmpty(showElements, "No shows found on the page.");

            foreach (IWebElement showElement in showElements)
            {
                // Find title, description, ranking, and score elements within each show
                IWebElement titleElement = showElement.FindElement(By.CssSelector(".text-primary.title a"));
                IWebElement descriptionElement = showElement.FindElement(By.CssSelector(".row-cell.content p"));
                IWebElement rankingElement = showElement.FindElement(By.CssSelector(".ranking span"));
                IWebElement scoreElement = showElement.FindElement(By.CssSelector(".score"));

                // Verify that title, description, ranking, and score are displayed
                ClassicAssert.IsTrue(titleElement.Displayed, "Show title is not displayed.");
                ClassicAssert.IsTrue(descriptionElement.Displayed, "Show description is not displayed.");
                ClassicAssert.IsTrue(rankingElement.Displayed, "Show ranking is not displayed.");
                ClassicAssert.IsTrue(scoreElement.Displayed, "Show score is not displayed.");

            }
        }

        [Test]
        public void MostPopularShows_NA_SuccessfullyDisplayed()
        {
            // Navigate to the Most Popular Shows page
            driver.Navigate().GoToUrl("https://mydramalist.com/shows/popular");

            // Wait until the shows section is visible
            wait.Until(driver => driver.FindElement(By.XPath("//*[@id=\"content\"]/div/div[2]/div/div[1]/div")).Displayed);

            IWebElement resultBody = driver.FindElement(By.XPath("//*[@id=\"content\"]/div/div[2]/div/div[1]/div"));

            // Find all show elements on the page
            var showElements = resultBody.FindElements(By.CssSelector(".box-body"));

            // Ensure that shows are found
            ClassicAssert.IsNotEmpty(showElements, "No shows found on the page.");

            foreach (IWebElement showElement in showElements)
            {
                // Find title, description, ranking, and score elements within each show
                IWebElement titleElement = showElement.FindElement(By.CssSelector(".text-primary.title a"));
                IWebElement descriptionElement = showElement.FindElement(By.CssSelector(".row-cell.content p"));
                IWebElement rankingElement = showElement.FindElement(By.CssSelector(".ranking span"));
                IWebElement scoreElement = showElement.FindElement(By.CssSelector(".score"));

                // Verify that title, description, ranking, and score are displayed
                ClassicAssert.IsTrue(titleElement.Displayed, "Show title is not displayed.");
                ClassicAssert.IsTrue(descriptionElement.Displayed, "Show description is not displayed.");
                ClassicAssert.IsTrue(rankingElement.Displayed, "Show ranking is not displayed.");
                ClassicAssert.IsTrue(scoreElement.Displayed, "Show score is not displayed.");

            }
        }

        [Test]
        public void VarietyShows_NA_SuccessfullyDisplayed()
        {
            // Navigate to the Variety Shows page
            driver.Navigate().GoToUrl("https://mydramalist.com/shows/variety");

            // Wait until the shows section is visible
            wait.Until(driver => driver.FindElement(By.XPath("//*[@id=\"content\"]/div/div[2]/div/div[1]/div")).Displayed);

            IWebElement resultBody = driver.FindElement(By.XPath("//*[@id=\"content\"]/div/div[2]/div/div[1]/div"));

            // Find all show elements on the page
            var showElements = resultBody.FindElements(By.CssSelector(".box-body"));

            // Ensure that shows are found
            ClassicAssert.IsNotEmpty(showElements, "No shows found on the page.");

            foreach (IWebElement showElement in showElements)
            {
                // Find title, description, ranking, and score elements within each show
                IWebElement titleElement = showElement.FindElement(By.CssSelector(".text-primary.title a"));
                IWebElement descriptionElement = showElement.FindElement(By.CssSelector(".row-cell.content p"));
                IWebElement rankingElement = showElement.FindElement(By.CssSelector(".ranking span"));
                IWebElement scoreElement = showElement.FindElement(By.CssSelector(".score"));

                // Verify that title, description, ranking, and score are displayed
                ClassicAssert.IsTrue(titleElement.Displayed, "Show title is not displayed.");
                ClassicAssert.IsTrue(descriptionElement.Displayed, "Show description is not displayed.");
                ClassicAssert.IsTrue(rankingElement.Displayed, "Show ranking is not displayed.");
                ClassicAssert.IsTrue(scoreElement.Displayed, "Show score is not displayed.");

            }
        }

        [Test]
        public void NewestShows_NA_SuccessfullyDisplayed()
        {
            // Navigate to the Newest Shows page
            driver.Navigate().GoToUrl("https://mydramalist.com/shows/newest");

            // Wait until the shows section is visible
            wait.Until(driver => driver.FindElement(By.XPath("//*[@id=\"content\"]/div/div[2]/div/div[1]/div")).Displayed);

            IWebElement resultBody = driver.FindElement(By.XPath("//*[@id=\"content\"]/div/div[2]/div/div[1]/div"));

            // Find all show elements on the page
            var showElements = resultBody.FindElements(By.CssSelector(".box-body"));

            // Ensure that shows are found
            ClassicAssert.IsNotEmpty(showElements, "No shows found on the page.");

            foreach (IWebElement showElement in showElements)
            {
                // Find title, description, and score elements within each show
                IWebElement titleElement = showElement.FindElement(By.CssSelector(".text-primary.title a"));
                IWebElement descriptionElement = showElement.FindElement(By.CssSelector(".row-cell.content p"));
                IWebElement scoreElement = showElement.FindElement(By.CssSelector(".score"));

                // Verify that title, description, ranking, and score are displayed
                ClassicAssert.IsTrue(titleElement.Displayed, "Show title is not displayed.");
                ClassicAssert.IsTrue(descriptionElement.Displayed, "Show description is not displayed.");
                ClassicAssert.IsTrue(scoreElement.Displayed, "Show score is not displayed.");

            }
        }

        [Test]
        public void UpcomingShows_NA_SuccessfullyDisplayed()
        {
            // Navigate to the Upcoming Shows page
            driver.Navigate().GoToUrl("https://mydramalist.com/shows/upcoming");

            // Wait until the shows section is visible
            wait.Until(driver => driver.FindElement(By.XPath("//*[@id=\"content\"]/div/div[2]/div/div[1]/div")).Displayed);

            IWebElement resultBody = driver.FindElement(By.XPath("//*[@id=\"content\"]/div/div[2]/div/div[1]/div"));

            // Find all show elements on the page
            var showElements = resultBody.FindElements(By.CssSelector(".box-body"));

            // Ensure that shows are found
            ClassicAssert.IsNotEmpty(showElements, "No shows found on the page.");

            foreach (IWebElement showElement in showElements)
            {
                // Find title, description, and score elements within each show
                IWebElement titleElement = showElement.FindElement(By.CssSelector(".text-primary.title a"));
                IWebElement descriptionElement = showElement.FindElement(By.CssSelector(".row-cell.content p"));
                IWebElement scoreElement = showElement.FindElement(By.CssSelector(".score"));

                // Verify that title, description, ranking, and score are displayed
                ClassicAssert.IsTrue(titleElement.Displayed, "Show title is not displayed.");
                ClassicAssert.IsTrue(descriptionElement.Displayed, "Show description is not displayed.");
                ClassicAssert.IsTrue(scoreElement.Displayed, "Show score is not displayed.");

            }
        }

        [Test]
        public void TopMovies_NA_SuccessfullyDisplayed()
        {
            // Navigate to the top moviews page
            driver.Navigate().GoToUrl("https://mydramalist.com/movies/top");

            // Wait until the shows section is visible
            wait.Until(driver => driver.FindElement(By.XPath("//*[@id=\"content\"]/div/div[2]/div/div[1]/div")).Displayed);

            IWebElement resultBody = driver.FindElement(By.XPath("//*[@id=\"content\"]/div/div[2]/div/div[1]/div"));

            // Find all show elements on the page
            var showElements = resultBody.FindElements(By.CssSelector(".box-body"));

            // Ensure that shows are found
            ClassicAssert.IsNotEmpty(showElements, "No shows found on the page.");

            foreach (IWebElement showElement in showElements)
            {
                // Find title, description, ranking, and score elements within each show
                IWebElement titleElement = showElement.FindElement(By.CssSelector(".text-primary.title a"));
                IWebElement descriptionElement = showElement.FindElement(By.CssSelector(".row-cell.content p"));
                IWebElement rankingElement = showElement.FindElement(By.CssSelector(".ranking span"));
                IWebElement scoreElement = showElement.FindElement(By.CssSelector(".score"));

                // Verify that title, description, ranking, and score are displayed
                ClassicAssert.IsTrue(titleElement.Displayed, "Show title is not displayed.");
                ClassicAssert.IsTrue(descriptionElement.Displayed, "Show description is not displayed.");
                ClassicAssert.IsTrue(rankingElement.Displayed, "Show ranking is not displayed.");
                ClassicAssert.IsTrue(scoreElement.Displayed, "Show score is not displayed.");

            }
        }

        [Test]
        public void MostPopularMovies_NA_SuccessfullyDisplayed()
        {
            // Navigate to the Most Popular Movies page
            driver.Navigate().GoToUrl("https://mydramalist.com/movies/popular");

            // Wait until the shows section is visible
            wait.Until(driver => driver.FindElement(By.XPath("//*[@id=\"content\"]/div/div[2]/div/div[1]/div")).Displayed);

            IWebElement resultBody = driver.FindElement(By.XPath("//*[@id=\"content\"]/div/div[2]/div/div[1]/div"));

            // Find all show elements on the page
            var showElements = resultBody.FindElements(By.CssSelector(".box-body"));

            // Ensure that shows are found
            ClassicAssert.IsNotEmpty(showElements, "No shows found on the page.");

            foreach (IWebElement showElement in showElements)
            {
                // Find title, description, ranking, and score elements within each show
                IWebElement titleElement = showElement.FindElement(By.CssSelector(".text-primary.title a"));
                IWebElement descriptionElement = showElement.FindElement(By.CssSelector(".row-cell.content p"));
                IWebElement rankingElement = showElement.FindElement(By.CssSelector(".ranking span"));
                IWebElement scoreElement = showElement.FindElement(By.CssSelector(".score"));

                // Verify that title, description, ranking, and score are displayed
                ClassicAssert.IsTrue(titleElement.Displayed, "Show title is not displayed.");
                ClassicAssert.IsTrue(descriptionElement.Displayed, "Show description is not displayed.");
                ClassicAssert.IsTrue(rankingElement.Displayed, "Show ranking is not displayed.");
                ClassicAssert.IsTrue(scoreElement.Displayed, "Show score is not displayed.");

            }
        }

        [Test]
        public void UpcomingMovies_NA_SuccessfullyDisplayed()
        {
            // Navigate to the Upcoming Movies page
            driver.Navigate().GoToUrl("https://mydramalist.com/movies/upcoming");

            // Wait until the shows section is visible
            wait.Until(driver => driver.FindElement(By.XPath("//*[@id=\"content\"]/div/div[2]/div/div[1]/div")).Displayed);

            IWebElement resultBody = driver.FindElement(By.XPath("//*[@id=\"content\"]/div/div[2]/div/div[1]/div"));

            // Find all show elements on the page
            var showElements = resultBody.FindElements(By.CssSelector(".box-body"));

            // Ensure that shows are found
            ClassicAssert.IsNotEmpty(showElements, "No shows found on the page.");

            foreach (IWebElement showElement in showElements)
            {
                // Find title, description, and score elements within each show
                IWebElement titleElement = showElement.FindElement(By.CssSelector(".text-primary.title a"));
                IWebElement descriptionElement = showElement.FindElement(By.CssSelector(".row-cell.content p"));
                IWebElement scoreElement = showElement.FindElement(By.CssSelector(".score"));

                // Verify that title, description, and score are displayed
                ClassicAssert.IsTrue(titleElement.Displayed, "Show title is not displayed.");
                ClassicAssert.IsTrue(descriptionElement.Displayed, "Show description is not displayed.");
                ClassicAssert.IsTrue(scoreElement.Displayed, "Show score is not displayed.");

            }
        }

        [Test]
        public void NewestMovies_NA_SuccessfullyDisplayed()
        {
            // Navigate to the Newest Movies page
            driver.Navigate().GoToUrl("https://mydramalist.com/movies/newest");

            // Wait until the shows section is visible
            wait.Until(driver => driver.FindElement(By.XPath("//*[@id=\"content\"]/div/div[2]/div/div[1]/div")).Displayed);

            IWebElement resultBody = driver.FindElement(By.XPath("//*[@id=\"content\"]/div/div[2]/div/div[1]/div"));

            // Find all show elements on the page
            var showElements = resultBody.FindElements(By.CssSelector(".box-body"));

            // Ensure that shows are found
            ClassicAssert.IsNotEmpty(showElements, "No shows found on the page.");

            foreach (IWebElement showElement in showElements)
            {
                // Find title, description, and score elements within each show
                IWebElement titleElement = showElement.FindElement(By.CssSelector(".text-primary.title a"));
                IWebElement descriptionElement = showElement.FindElement(By.CssSelector(".row-cell.content p"));
                IWebElement scoreElement = showElement.FindElement(By.CssSelector(".score"));

                // Verify that title, description, and score are displayed
                ClassicAssert.IsTrue(titleElement.Displayed, "Show title is not displayed.");
                ClassicAssert.IsTrue(descriptionElement.Displayed, "Show description is not displayed.");
                ClassicAssert.IsTrue(scoreElement.Displayed, "Show score is not displayed.");

            }
        }

        [Test]
        public void ShowDetails_NA_SuccessfullyDisplayed()
        {
            // Navigate to the Goblin ahow page
            driver.Navigate().GoToUrl("https://mydramalist.com/18452-goblin");

            // Verify the rating
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='box deep-orange']")));
            IWebElement ratingElement = driver.FindElement(By.XPath("//div[@class='box deep-orange']"));
            string actualRating = ratingElement.Text;
            string expectedRating = "8.8";
            ClassicAssert.AreEqual(expectedRating, actualRating, $"Expected rating: {expectedRating}, Actual rating: {actualRating}");

            // Verify the name
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//a[@href='/18452-goblin' and contains(text(), '도깨비')]")));
            IWebElement nameElement = driver.FindElement(By.XPath("//a[@href='/18452-goblin' and contains(text(), '도깨비')]"));
            string actualName = nameElement.Text;
            string expectedName = "도깨비";
            ClassicAssert.IsTrue(actualName.Contains(expectedName), $"Expected name to contain: {expectedName}, Actual name: {actualName}");

            // Verify the description
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='show-synopsis']//p/span")));
            IWebElement descriptionElement = driver.FindElement(By.XPath("//div[@class='show-synopsis']//p/span"));
            string actualDescription = descriptionElement.Text;
            string expectedDescriptionSnippet = "Kim Shin was once an unbeatable general";
            ClassicAssert.IsTrue(actualDescription.Contains(expectedDescriptionSnippet), $"Expected description to contain snippet: \"{expectedDescriptionSnippet}\", Actual description: {actualDescription}");

            // Verify the image
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//img[@class='img-responsive' and contains(@src, 'JkD8Xc.jpg')]")));
            IWebElement imageElement = driver.FindElement(By.XPath("//img[@class='img-responsive' and contains(@src, 'JkD8Xc.jpg')]"));
            ClassicAssert.IsTrue(imageElement.Displayed, "Expected image to be displayed.");
        }

        [Test]
        public void PeopleDetails_NA_SuccessfullyDisplayed()
        {
            // Navigate to the Gong Yoo page
            driver.Navigate().GoToUrl("https://mydramalist.com/people/440-gong-yoo");

            // Wait for and verify the name
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//h1[@class='film-title m-b-0 m-r-0']")));
            IWebElement nameElement = driver.FindElement(By.XPath("//h1[@class='film-title m-b-0 m-r-0']"));
            string actualName = nameElement.Text;
            string expectedName = "Gong Yoo";
            ClassicAssert.AreEqual(expectedName, actualName, $"Expected name: {expectedName}, Actual name: {actualName}");

            // Verify the image
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//img[@class='img-responsive' and contains(@src, 'kwW2w_5c.jpg')]")));
            IWebElement imageElement = driver.FindElement(By.XPath("//img[@class='img-responsive' and contains(@src, 'kwW2w_5c.jpg')]"));
            ClassicAssert.IsTrue(imageElement.Displayed, "Expected image to be displayed.");

            // Verify the personal details
            // First Name
            IWebElement firstNameElement = driver.FindElement(By.XPath("//li[b[text()='First Name:']]"));
            string actualFirstName = firstNameElement.Text.Replace("First Name: ", "").Trim();
            string expectedFirstName = "Ji Cheol";
            Assert.That(actualFirstName, Is.EqualTo(expectedFirstName), $"Expected First Name: {expectedFirstName}, Actual First Name: {actualFirstName}");

            // Family Name
            IWebElement familyNameElement = driver.FindElement(By.XPath("//li[b[text()='Family Name:']]"));
            string actualFamilyName = familyNameElement.Text.Replace("Family Name: ", "").Trim();
            string expectedFamilyName = "Gong";
            Assert.That(actualFamilyName, Is.EqualTo(expectedFamilyName), $"Expected Family Name: {expectedFamilyName}, Actual Family Name: {actualFamilyName}");

        }

    }
}
