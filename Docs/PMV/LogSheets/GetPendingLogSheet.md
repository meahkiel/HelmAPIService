# GET PENDING LOGSHEET API

```code
 METHOD: GET
 http://{url}/pmv/logsheet?lvStation={station}
 ```
 **Result**
 ```json
[
    {
        "id": "90D96131-AD68-44F4-E6DD-08DAE7F2A87C",
        "referenceNo": 2,
        "fueledDate": "2022-12-27T01:10:51",
        "shiftStartTime": "1:10 AM",
        "shiftEndTime": null,
        "startShiftTankerKm": 1000,
        "endShiftTankerKm": null,
        "startShiftMeterReading": 75256,
        "endShiftMeterReading": null,
        "location": "",
        "lvStation": "LV375",
        "remarks": null,
        "employeeCode": "H22095411",
        "details": [
            {
                "logSheetId": null,
                "id": "62D74852-F03B-4DA6-AF61-2482CA8504E6",
                "assetCode": "BHL014",
                "fuelTime": "2022-12-27T03:13:07",
                "operatorDriver": "Muhendra",
                "reading": 1254,
                "previousReading": 0,
                "quantity": 120,
                "driverQatarIdUrl": "",
                "currentSMUUrl": null,
                "tankMeterUrl": null
            }
        ]
    },
    {
        "id": "4C3380BA-B53D-4892-7638-08DAE7F91326",
        "referenceNo": 3,
        "fueledDate": "2022-12-27T01:56:36",
        "shiftStartTime": "1:56 AM",
        "shiftEndTime": null,
        "startShiftTankerKm": 1000,
        "endShiftTankerKm": null,
        "startShiftMeterReading": 75256,
        "endShiftMeterReading": null,
        "location": "",
        "lvStation": "LV375",
        "remarks": null,
        "employeeCode": "H22095411",
        "details": []
    }
]
 ```