using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;



namespace Lab6
{
    public class Tests
    {
        private int delay = 4000;
        private IWebDriver driver;
        private readonly By _historyButton = By.XPath("/html/body/header/div[1]/nav/a[4]");
        private readonly By _infoButton = By.XPath("/html/body/header/div/nav/a[3]");
        private readonly By _mainButton = By.XPath("/html/body/header/div/nav/a[1]/img");
        private readonly By _issueButton = By.XPath("/html/body/aside/button");
        private readonly By _issueTitleField = By.XPath("/html/body/div[2]/div[2]/input");
        private readonly By _issueMainField = By.XPath("/html/body/div[2]/div[2]/textarea");
        private readonly By _issueSendButton = By.XPath("/html/body/div[2]/div[2]/button");
        private readonly By _itemCricket = By.XPath("/html/body/main/section[1]/div[1]/button[4]/div[1]");
        private readonly By _infoItemCricket = By.XPath("/html/body/div[1]/div[2]/div/div/h4");
        private readonly By _popup = By.XPath("/html/body/div[1]/div[2]");
        private readonly By _search = By.XPath("/html/body/header/label/input");
        private readonly By _searchClear = By.XPath("/html/body/header/label/button");
        private readonly By _filterButton = By.XPath("/html/body/header/button[2]");
        private readonly By _hiddenFilterSetting = By.XPath("/html/body/header/div[2]/div[1]/div[2]/div[1]/label[2]");



        private readonly string _historyTitle = "История изменений — Dead God";
        private readonly string _infoTitle = "О сайте — Dead God";


        [SetUp]
        public void Setup()
        {
            driver = new EdgeDriver("..\\..\\..\\..\\MicrosoftWebDriver.exe");
            driver.Navigate().GoToUrl("https://dead-god.ru");
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test1()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(delay));




            //Проверка перехода на страницу истории изменений
            Thread.Sleep(delay);
            var currentButton = driver.FindElement(_historyButton);
            currentButton.Click();
            string title = driver.Title;
            Assert.That(title, Is.EqualTo(_historyTitle));

            //Проверка перехода на страницу с информацией
            Thread.Sleep(delay);
            currentButton = driver.FindElement(_infoButton);
            currentButton.Click();
            title = driver.Title;
            Assert.That(title, Is.EqualTo(_infoTitle));

            //Переход на главную страницу сайта
            Thread.Sleep(delay);
            currentButton = driver.FindElement(_mainButton);
            currentButton.Click();


            //Клик на предмет
            Thread.Sleep(delay);
            currentButton = driver.FindElement(_itemCricket);
            currentButton.Click();

            //Развернуть и свернуть описание
            wait.Until(driver => driver.FindElement(_infoItemCricket).Displayed);
            currentButton = driver.FindElement(_infoItemCricket);
            currentButton.Click();
            Thread.Sleep(delay);
            currentButton = driver.FindElement(_infoItemCricket);
            currentButton.Click();

            //Зыкрыть всплывающее окно
            Thread.Sleep(delay);
            var popup = driver.FindElement(_popup);
            Actions actions = new Actions(driver);
            actions.MoveToElement(popup, 0, 160).Perform();
            actions.Click().Perform();


            //Проверка поля поиска
            Thread.Sleep(delay);
            var searchField = driver.FindElement(_search);
            searchField.SendKeys("Мама");
            Assert.That(searchField.GetAttribute("value"), Is.Not.Empty);


            Thread.Sleep(delay);
            currentButton = driver.FindElement(_searchClear);
            currentButton.Click();
            Assert.That(searchField.GetAttribute("value"), Is.Empty);


            //Проверка видимости при фильтрации
            Thread.Sleep(delay);
            currentButton = driver.FindElement(_filterButton);
            currentButton.Click();

            wait.Until(driver => driver.FindElement(_hiddenFilterSetting).Displayed);
            currentButton = driver.FindElement(_hiddenFilterSetting);
            currentButton.Click();

            Thread.Sleep(delay);
            var item = driver.FindElement(_itemCricket);
            Assert.That(item.Displayed, Is.True);


            currentButton = driver.FindElement(_filterButton);
            currentButton.Click();

            searchField.SendKeys("Мама");
            wait.Until(driver => !driver.FindElement(_hiddenFilterSetting).Displayed);
            Assert.That(item.Displayed, Is.False);



            //Проверка отправки формы
            currentButton = driver.FindElement(_issueButton);
            currentButton.Click();
            var issueTitleField = driver.FindElement(_issueTitleField);
            var issueMainField = driver.FindElement(_issueMainField);
            issueTitleField.SendKeys("Ошибка");
            Assert.That(issueTitleField.GetAttribute("value"), Is.Not.Empty);
            issueMainField.SendKeys("Описание ошибки");
            Assert.That(issueMainField.GetAttribute("value"), Is.Not.Empty);
            Thread.Sleep(delay);
            currentButton = driver.FindElement(_issueSendButton);
            currentButton.Click();
            Thread.Sleep(delay);


            Assert.Pass();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }

    }
}

