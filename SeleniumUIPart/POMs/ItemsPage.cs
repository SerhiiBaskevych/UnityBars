using OpenQA.Selenium;

namespace SeleniumUIPart.POMs
{
    public class ItemsPage
    {
        IWebDriver driver;
        IWebElement item;
        
        public ItemsPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Sort active option
        IWebElement activeSortOption => driver.FindElement(By.CssSelector(".active_option"));

        //Sort select list
        IWebElement sortSelectList => driver.FindElement(By.CssSelector(".product_sort_container"));

        //Add to cart button
        IWebElement addToCartButton => driver.FindElement(By.CssSelector(".product_sort_container"));

        //Shopping cart image
        IWebElement shoppingCartImage => driver.FindElement(By.Id("shopping_cart_container"));

        //List of item cart
        List<IWebElement> itemCarts => new List<IWebElement>(driver.FindElements(By.CssSelector(".inventory_item")));

        public IWebElement GetSortSelectList() => sortSelectList;
        public IWebElement GetActiveSortOption() => activeSortOption;
        public IWebElement GetCartCount() => shoppingCartImage;

        public Dictionary <string, string> GetItemsCarts()
        {
            Dictionary<string, string> itemDetails = new Dictionary<string, string>();

            foreach (IWebElement item in itemCarts)
            {
                string itemName = item.FindElement(By.CssSelector(".inventory_item_name")).Text;
                string itemPrice = item.FindElement(By.CssSelector(".inventory_item_price")).Text;

                itemDetails[itemName] = itemPrice;  
            }
            return itemDetails;
        }

        public IWebElement GetFaforiteItemCart(string favoriteItemName)
        {         
            Assert.That(itemCarts.Count, Is.GreaterThan(0));
            IWebElement favoriteItem = itemCarts.FirstOrDefault(e => e.Text.Contains(favoriteItemName));
            Assert.That(favoriteItem, Is.Not.Null);
            return favoriteItem;
        }

        public string GetFavoriteItemTitle(string favoriteItemName)
        {
            item = GetFaforiteItemCart(favoriteItemName);
            Assert.That(item, Is.Not.Null);
            return item.FindElement(By.CssSelector(".inventory_item_name")).Text;  
        }

        public string GetFavoriteItemPrice(string favoriteItemName)
        {
            item = GetFaforiteItemCart(favoriteItemName);
            Assert.That(item, Is.Not.Null);
            return item.FindElement(By.CssSelector(".inventory_item_price")).Text;
        }

        public void AddFavoriteItemToCart(string favoriteItemName)
        {
            item = GetFaforiteItemCart(favoriteItemName);
            IWebElement addToCartButton = item.FindElement(By.CssSelector("[class*='btn_inventory']"));
            Assert.That(addToCartButton.Text, Is.EqualTo("Add to cart"));

            addToCartButton.Click();
            addToCartButton = item.FindElement(By.CssSelector("[class*='btn_inventory']"));
            Assert.That(addToCartButton.Text, Is.EqualTo("Remove"));
        }

        public void ClickOnShoppingCart()
        {
            shoppingCartImage.Click();
        }
    }
}
