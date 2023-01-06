using Core.PMV.Fuels;
using System.Reflection.Metadata;

namespace HlmTesting;


public interface IFuelService
{

	FuelLog CreateLog(string stationCode,
        int lastDocumentNo,
        int referenceNo,
        DateTime? shiftStartTime,
        DateTime? shiftEndTime,
        int startShiftTankerKm,
        int endShiftTankerKm,
        float adjustedMeter,
        float adjustedBalance);
	FuelLog? GetLog(string id);
	void UpdateLog(FuelLog log);
}
public class FuelService : IFuelService
{
    private IList<FuelLog> _tankLogs;

	public FuelService()
	{
        //initialize
        _tankLogs = new List<FuelLog>
        {
            FuelLog.Create(
                stationCode: "D1982Station",
                lastDocumentNo: 1,
                referenceNo: 1,
                shiftStartTime: DateTime.Today,
                shiftEndTime: null,
                startShiftTankerKm: 15000,
                endShiftTankerKm: 0,
                openingMeter: 0,
                adjustmentMeter: 2000,
                openingBalance: 0,
                adjustmentBalance: 1000),
             FuelLog.Create(
                stationCode: "D1981Station",
                lastDocumentNo: 1,
                referenceNo: 1,
                shiftStartTime: DateTime.Today,
                shiftEndTime: null,
                startShiftTankerKm: 15000,
                endShiftTankerKm: 0,
                openingMeter: 0,
                openingBalance: 0,
                adjustmentMeter: 2000,
                adjustmentBalance: 1000),
           
		};

		foreach(var log in _tankLogs)
		{
			if(log.StationCode == "D1982Station")
			{
				log.AddRestockTransaction(log.StationCode, DateTime.Today, 50000);
				log.AddDispenseTransaction("LV145", 0, 25000, "Mohendra",DateTime.Now,3000);
				log.AddDispenseTransaction("LV125", 0, 25000, "Mohendra", DateTime.Now, 3000);
				log.AddDispenseTransaction("LV165", 0, 25000, "Mohendra", DateTime.Now, 3000);
				log.AddDispenseTransaction("LV135", 0, 25000, "Mohendra", DateTime.Now, 3000);
			}
			else
			{
                log.AddRestockTransaction(log.StationCode, DateTime.Today, 50000);
                log.AddDispenseTransaction("LV245", 0, 25000, "Mohendra", DateTime.Now, 3000);
                log.AddDispenseTransaction("LV225", 0, 25000, "Mohendra", DateTime.Now, 3000);
                log.AddDispenseTransaction("LV265", 0, 25000, "Mohendra", DateTime.Now, 3000);
                log.AddDispenseTransaction("LV235", 0, 25000, "Mohendra", DateTime.Now, 3000);
            }
        }

	}

  

   
    public FuelLog? GetLog(string id)
    {
        return _tankLogs.SingleOrDefault(d => d.Id == Guid.Parse(id));
    }

    public void UpdateLog(FuelLog log)
    {
		var current = _tankLogs.SingleOrDefault(d => d.Id == log.Id);
		current = FuelLog.Clone(log);
    }

    

    public FuelLog CreateLog(
        string stationCode,
        int lastDocumentNo,
        int referenceNo,
        DateTime? shiftStartTime,
        DateTime? shiftEndTime,
        int startShiftTankerKm,
        int endShiftTankerKm,
        float adjustedMeter,
        float adjustedBalance) {

        //get the 
        FuelLog? record = _tankLogs
                        .Where(l => l.StationCode == stationCode)
                        .OrderByDescending(l => l.Date)
                        .OrderByDescending(l => l.ReferenceNo)
                        .FirstOrDefault();
        
        float openingMeter = 0f;
        float openingBalance = 0f;
        if(record != null)
        {
            openingMeter = record.ClosingMeter;
            openingBalance = record.RemainingBalance;
        }

        var log = FuelLog.Create(
            stationCode,
            lastDocumentNo,
            referenceNo,
            shiftStartTime,
            shiftEndTime,
            startShiftTankerKm,
            endShiftTankerKm,
            openingMeter,
            openingBalance,
            adjustedBalance,
            adjustedMeter);

        _tankLogs.Add(log);
        return log;
    }
}
