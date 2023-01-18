# GET DRAFTS LOG API

```code
 METHOD: GET
 http://{url}/api/pmv/FuelLog/draft?Fueler=F19044206'
 ```
 **Result**
 ```json
[
  {
    "id": "ce745fb9-933f-4395-9f14-184a39f84183",
    "station": "LV215",
    "referenceNo": 123456,
    "documentNo": 13,
    "fueledDate": "2023-01-11T09:00:00",
    "shiftStartTime": "2023-01-11T09:00:00",
    "shiftEndTime": "2023-01-11T19:00:00",
    "startShiftTankerKm": 12100,
    "endShiftTankerKm": 1300,
    "openingMeter": 0,
    "closingMeter": 340,
    "openingBalance": 0,
    "remainingBalance": -340,
    "location": "",
    "remarks": null,
    "fueler": "F19044206",
    "isPosted": false,
    "locationSelections": [],
    "stationSelections": [],
    "assetCodeSelections": [],
    "fuelerSelections": [],
    "fuelTransactions": [
      {
        
        "id": "e9730e25-e5e3-413d-abce-02c9bc8f3376",
        "assetCode": "BHL015",
        "fuelDateTime": "2023-01-01T14:26:28",
        "fuelDate": "2023-01-01T14:26:28",
        "fuelTime": "2023-01-01T14:26:28",
        "operatorDriver": "F19044206",
        "remarks": null,
        "logType": "Dispense",
        "reading": 10000,
        "quantity": 125,
        "driverQatarIdUrl": null
      },
      {
        
        "id": "cc06beb0-db6f-48b4-a5d9-e1e451321d0c",
        "assetCode": "CE-M002",
        "fuelDateTime": "2023-01-11T08:13:30",
        "fuelDate": "2023-01-11T08:13:30",
        "fuelTime": "2023-01-11T08:13:30",
        "operatorDriver": "F19044208",
        "remarks": null,
        "logType": "Dispense",
        "reading": 10000,
        "quantity": 120,
        "driverQatarIdUrl": null
      },
      {
        
        "id": "829958ee-f9eb-4d15-9431-ef4234f13080",
        "assetCode": "CE-M012",
        "fuelDateTime": "2023-01-11T12:59:06",
        "fuelDate": "2023-01-11T12:59:06",
        "fuelTime": "2023-01-11T12:59:06",
        "operatorDriver": "F16031813",
        "remarks": null,
        "logType": "Dispense",
        "reading": 10000,
        "quantity": 95,
        "driverQatarIdUrl": null
      }
    ]
  }
]
 ```


 # CREATE NEW / EDIT  LOG API
 ```code
 METHOD: GET
 http://{url}/api/pmv/FuelLog/single?id=1,IsPostBack=false
 ```



 ```code
 METHOD: GET
 http://{url}/api/pmv/FuelLog/new?station=LV145&IsPostBack=false

 ```

 **Response**
 ```json 
{
  "id": null,
  "station": "",
  "stationType": null,
  "referenceNo": 0,
  "documentNo": 0,
  "fueledDate": "2023-01-16T00:00:00+03:00",
  "shiftStartTime": "2023-01-16T00:00:00+03:00",
  "shiftEndTime": "2023-01-16T00:00:00+03:00",
  "startShiftTankerKm": 0,
  "endShiftTankerKm": 0,
  "openingMeter": 340,
  "closingMeter": 0,
  "openingBalance": 0,
  "remainingBalance": 0,
  "location": "",
  "remarks": null,
  "fueler": null,
  "isPosted": false,
  "fuelTransactions": [ 
    {
        
        "id": "e9730e25-e5e3-413d-abce-02c9bc8f3376",
        "assetCode": "BHL015",
        "fuelDateTime": "2023-01-01T14:26:28",
        "fuelDate": "2023-01-01T14:26:28",
        "fuelTime": "2023-01-01T14:26:28",
        "operatorDriver": "F19044206",
        "remarks": null,
        "logType": "Dispense",
        "reading": 10000,
        "quantity": 125,
        "displayQty": -97,
        "driverQatarIdUrl": null
      }
    ],
  "stationSelections": [
    {
      "code": "LLV463",
      "description": "LLV463",
      "openingMeter": 0,
      "openingBalance": 0,
      "stationType": "Tanker"
    }
  ],
  "locationSelections": [
    {
      "text": "AL THAKIRA",
      "value": "AL THAKIRA",
      "isSelected": false
    }
  ],
  "assetCodeSelections": [
    {
      "text": "GD009 - Diesel Generator 150 KVA",
      "value": "GD009",
      "isSelected": false
    }
  ],
  "fuelerSelections":[]
}
 ```