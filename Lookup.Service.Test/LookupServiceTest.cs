using Microsoft.Extensions.Options;
using Moq;

namespace Lookup.Service.Test
{
    public class LookupServiceTest
    {
        private LookupService targetService;
        private Mock<IOptions<AppSettings>> appSettings;
        [SetUp]
        public void Setup()
        {
            appSettings = new Mock<IOptions<AppSettings>>();
            appSettings.Setup(x => x.Value).Returns(
                new AppSettings()
                {
                    LookupUrl = "http://production.shippingapis.com/ShippingAPI.dll?API=CityStateLookup",
                    Username = "340HEXAW6349"
                }
                );
            targetService = new LookupService(appSettings.Object);
        }

        [TestCase("90210", "CA")]
        [TestCase("10008", "NY")]
        public void ValidZipCodeTest(string zipCode, string expectedState)
        {
            var result = targetService.GetStateByZipCode(zipCode);
            Assert.AreEqual(expectedState, result);
        }

        [TestCase("99999", "Invalid Zip Code.")]
        [TestCase("00000", "Invalid Zip Code.")]
        [TestCase("", "Invalid Zip Code.")]
        [TestCase(null, "Invalid Zip Code.")]
        public void InvalidZipCodeTest(string zipCode, string expectedState)
        {
            var result = targetService.GetStateByZipCode(zipCode);
            Assert.AreEqual(expectedState, result);
        }

        [TestCase("902108", "ZIPCode must be 5 characters")]
        [TestCase("100088", "ZIPCode must be 5 characters")]
        [TestCase("1000", "ZIPCode must be 5 characters")]
        [TestCase("9999", "ZIPCode must be 5 characters")]
        public void ZipCodeMoreOrLessthan5Char_Test(string zipCode, string expectedState)
        {
            var result = targetService.GetStateByZipCode(zipCode);
            Assert.AreEqual(expectedState, result);
        }
    }
}