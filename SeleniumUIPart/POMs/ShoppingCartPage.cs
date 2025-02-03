using OpenQA.Selenium;

namespace SeleniumUIPart.POMs
{
    public class ShoppingCartPage
    {
        IWebDriver driver;

        public ShoppingCartPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Shopping cart price field
        IWebElement shoppingCartItemPriceField => driver.FindElement(By.CssSelector(".inventory_item_price"));

        //Shopping cart name field
        IWebElement shoppingCartItemNameField => driver.FindElement(By.CssSelector(".inventory_item_name"));

        public IWebElement GetItemTitleOnCartPage() => shoppingCartItemNameField;
        public IWebElement GetItemPriceOnCartPage() => shoppingCartItemPriceField;

    }
}
