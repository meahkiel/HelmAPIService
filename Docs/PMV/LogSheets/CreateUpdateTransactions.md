# CreateUpdateTransactions API

**Request**
```code
METHOD: POST
http://{url}/api/pmv/logsheet/full
Accept: application/json
```

```json
{
    
    "fueledDate": "{{currentDateTime}}",
    "shiftStartTime": "{{currentDateTime}}",
    "startShiftTankerKm": 5000,
    "startShiftMeterReading": 75256,
    "location": "D191",
    "lVStation": "LV253",
    "employeeCode": "H22095411",
    "shiftEndTime": "{{currentDateTime}}",
    "endShiftTankerKm": 1000,
    "endShiftMeterReading": 75256,
    "fueler": "H22095411",
    "details": [
        {
            "assetCode":"MC003",
            "reading": 25000,
            "quantity": 150,
            "fuelTime": "{{currentDateTime}}",
            "driverQatarIdUrl": "",
            "currentSMUUrl": "",
            "tankMeterUrl": ""
        }
    ]
}
```