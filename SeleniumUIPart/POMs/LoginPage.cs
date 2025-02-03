using OpenQA.Selenium;

namespace SeleniumUIPart.POMs
{
    public class LoginPage
    {
        IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Login field
        IWebElement loginField => driver.FindElement(By.Id("user-name"));

        //Password field
        IWebElement passwordField => driver.FindElement(By.Id("password"));

        //Login button
        IWebElement loginButton => driver.FindElement(By.Id("login-button"));

        public void LoginWithCredentials(string login, string password)
        {
            loginField.SendKeys(login);
            passwordField.SendKeys(password);
            loginButton.Click();
        }
    }
}
