# Transaction API


```code
METHOD: GET
 http://{{test_mod_local}}/pmv/logsheet/transaction-report?dateFrom=2022-12-01&dateTo=2022-12-31
 ```
 **Result**
 ```json
[
    {
        "logSheetId": "56810fcb-3724-48e2-977a-a3a8d074a8bc",
        "referenceNo": 1,
        "refNoFormatted": "0001",
        "assetCode": "MC003",
        "code": "LV253",
        "location": "D191",
        "fueledDate": "12/03/2022 00:00:00",
        "fuelTime": "12/03/2022 13:44:23",
        "startShiftMeterReading": 75256,
        "endShiftMeterReading": 75256,
        "quantity": 165,
        "reading": 25000,
        "previousReading": 0,
        "diff": 25000,
        "lh": 0.0066,
        "hl": 151.51515151515151515151515152,
        "lhFormat": "0.01",
        "hlFormat": "151.52",
        "isPosted": true,
        "postedAt": "12/03/2022 13:43:43",
        "fueler": "H22095411"
    },
    {
        "logSheetId": "56810fcb-3724-48e2-977a-a3a8d074a8bc",
        "referenceNo": 1,
        "refNoFormatted": "0001",
        "assetCode": "SM005",
        "code": "LV253",
        "location": "D191",
        "fueledDate": "12/03/2022 00:00:00",
        "fuelTime": "12/03/2022 13:46:22",
        "startShiftMeterReading": 75256,
        "endShiftMeterReading": 75256,
        "quantity": 46,
        "reading": 19847,
        "previousReading": 0,
        "diff": 19847,
        "lh": 0.0023177306393913437799163602,
        "hl": 431.45652173913043478260869565,
        "lhFormat": "0.00",
        "hlFormat": "431.46",
        "isPosted": true,
        "postedAt": "12/03/2022 13:43:43",
        "fueler": "H22095411"
    },
   
    
]
 ```

