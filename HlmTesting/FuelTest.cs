using Core.PMV.Assets;
using Core.PMV.Fuels;
using Core.PMV.LogSheets;
using System.Reflection.Metadata;

namespace HlmTesting
{
    public class FuelTest
    {
        private IFuelService _fuelService;

        [SetUp]
        public void Setup()
        {
            _fuelService = new FuelService();
        }

        [TestCase("D1983Station","2023-01-05T11:00:00",1 ,1,0,10000)]
        public void Station_Refill_InitialBalanceMatch(
            string fuelStation,
            string date,
            int documentNo,
            int referenceNo,
            int beginingMeter,
            int adjustedMeter)
        {   

            var log = _fuelService.CreateLog(
                fuelStation,
                documentNo,
                referenceNo,
                DateTime.Today,
                null,
                1000,
                0,
                10000,
                1000);
            
            log.UpsertRestockTransaction(fuelStation, DateTime.Now, 50000);
            Assert.That(log.OpeningBalance, Is.EqualTo(1000f));
            Assert.That(log.RemainingBalance, Is.EqualTo(51000f));
            Assert.That(log.OpeningMeter, Is.EqualTo(10000f));
            Assert.That(log.ClosingMeter,Is.EqualTo(10000f));
        }

        [TestCase("D1983Station", "2023-01-05T11:00:00", 1, 1, 0, 10000)]
        public void Station_Reload_MustHavePositiveValue(
           string fuelStation,
           string date,
           int documentNo,
           int referenceNo,
           int beginingMeter,
           int adjustedMeter)
        {

            var log = _fuelService.CreateLog(
                fuelStation,
                documentNo,
                referenceNo,
                DateTime.Today,
                null,
                1000,
                0,
                10000,
                1000);

            log.UpsertRestockTransaction(fuelStation, DateTime.Now, 50000);
            log.UpsertDispenseTransaction("LV435",3000,3100,"Driver", DateTime.Now, 3000,"");
            log.UpsertDispenseTransaction("LV425", 3000, 3100, "Driver", DateTime.Now, 3000,"");

            Assert.That(log.OpeningBalance, Is.EqualTo(1000f));
            Assert.That(log.RemainingBalance, Is.EqualTo(45000f));
            Assert.That(log.OpeningMeter, Is.EqualTo(10000f));
            Assert.That(log.ClosingMeter, Is.EqualTo(16000f));
        }


    }
}