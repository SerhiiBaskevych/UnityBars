using OpenQA.Selenium.Support.UI;
using SeleniumUIPart.POMs;
using SeleniumUIPart.DataModels;
using SeleniumUIPart.Helpers;
using OpenQA.Selenium;

namespace SeleniumUIPart.SauceDemo
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class SauceDemo : Base
    {
        string? ItemPriceOnInventoryPage { get; set; }
        string? ItemTitleOnInventoryPage { get; set; }

        [Test]
        [TestCaseSource(nameof(GetTestData))]
        public void AddToCartFavoriteItem(TestDataModel testDataModel)
        {
            IWebDriver driver = GetDriver();

            //Login process
            LoginPage loginPage = new LoginPage(driver);
            loginPage.LoginWithCredentials(testDataModel.Username, testDataModel.Password);

            //Inventory page
            TestContext.Out.WriteLine($"Current page is: {driver.Url}");
            Assert.That($"{driver.Url}".Contains(INVENTORY_END_POINT));

            ItemsPage itemsPage = new ItemsPage(driver);
            SelectElement select = new SelectElement(itemsPage.GetSortSelectList());

            //Sorting Process
            select.SelectByValue("hilo");
            Assert.That(itemsPage.GetActiveSortOption().Text, Is.EqualTo("Price (high to low)"));

            //Display all items from page in output
            foreach (var item in itemsPage.GetItemsCarts())
            {
                TestContext.Out.WriteLine(item.Key + " has price: "+ item.Value);
            }

            //Find favorite item          
            ItemPriceOnInventoryPage = itemsPage.GetFavoriteItemPrice(testDataModel.FavoriteItemTitle);
            ItemTitleOnInventoryPage = itemsPage.GetFavoriteItemTitle(testDataModel.FavoriteItemTitle);

            Assert.That(ItemTitleOnInventoryPage, Is.EqualTo(testDataModel.FavoriteItemTitle));
            Assert.That(ItemPriceOnInventoryPage, Is.EqualTo(testDataModel.FavoriteItemPrice));

            //Add to cart favorite item
            Assert.That(itemsPage.GetCartCount().Text, Is.Empty);

            itemsPage.AddFavoriteItemToCart(testDataModel.FavoriteItemTitle);
            Assert.That(itemsPage.GetCartCount().Text, Is.EqualTo("1"));

            //Go to shopping cart          
            itemsPage.ClickOnShoppingCart();

            TestContext.Out.WriteLine($"Current page is: {driver.Url}");
            Assert.That($"{driver.Url}".Contains(CART_END_POINT));

            //Compare price & item title on cart page
            ShoppingCartPage shoppingCartPage = new ShoppingCartPage(driver);

            Assert.That(shoppingCartPage.GetItemTitleOnCartPage().Text, Is.EqualTo(ItemTitleOnInventoryPage));
            Assert.That(shoppingCartPage.GetItemPriceOnCartPage().Text, Is.EqualTo(ItemPriceOnInventoryPage));
        }

        private static IEnumerable<TestDataModel> GetTestData()
        {
            return Helper.JsonDataRetriever(new TestDataModel());
        }     
    }
}   