using SeleniumCSharp.FunctionalTests.PageComponents;

namespace SeleniumCSharp.FunctionalTests.Tests
{
    public class FiltersTests : BaseTest
    {
        private Filters filters;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            filters = new Filters(_driver);
            navMenu.SelectTrainings();
        }

        [Test]
        public void TestFilterIsSelected()
        {
            // Act
            filters.SelectFilterByName("Bezpłatne");

            // Assert
            Assert.That(filters.IsFilterSelected("Bezpłatne"), Is.True, "The 'Bezpłatne' filter checkbox is not selected.");
        }

        [Test]
        public void TestFilterIsNotSelected()
        {
            // Assert
            Assert.That(filters.IsFilterSelected("Bezpłatne"), Is.False, "The 'Bezpłatne' filter checkbox is selected, but it should not be.");
        }

        [Test]
        public void TestUrlAfterApplyingFilter()
        {
            // Act
            filters.SelectFilterByName("Bezpłatne");
            filters.ClickApplyFilters();

            // Assert
            string currentUrl = trainingsPage.GetCurrentUrl();
            Assert.That(currentUrl, Does.Contain("withSubscription=false"), "The URL does not contain the expected filter parameter 'free=true'.");
        }
    }
}