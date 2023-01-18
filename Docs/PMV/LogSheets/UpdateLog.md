## Update Log API ##

```code
METHOD: POST
END POINT: http://{url}/api/pmv/fuellog/update
```
**Request Full Entry**
```json
{
  "id": "string",
  "employeeCode": "string",
  "fueledDate": "string",
  "referenceNo": 0,
  "shiftStartTime": "string",
  "shiftEndTime": "string",
  "startShiftTankerKm": 0,
  "endShiftTankerKm": 0,
  "location": "string",
  "station": "string",
  "remarks": "string",
  "fueler": "string",
  "fuelTransactions": [
    {
      "id": "string",
      "fuelStation": "string",
      "assetCode": "string",
      "fuelDate": "string",
      "fuelTime": "string",
      "remarks": "string",
      "operatorDriver": "string",
      "reading": 0,
      "quantity": 0,
      "driverQatarIdUrl": "string",
      "logType": "string",
      "isMarkDeleted": true
    }
  ],
  "isPosted": true,
  "delCollection": {
    "ids": [
      "string"
    ]
  },
  "isPartial": false
}
```

**Request Partial Entry**
```json
{
  "id": "4FF14B7E-D5E0-418B-A599-2603D1FB8B00",
  "employeeCode": "H22095411",
  "fueledDate": "2023-01-16",
  "referenceNo": 0,
  "shiftStartTime": "2023-01-16 08:00",
  "shiftEndTime": "2023-01-16 08:00",
  "startShiftTankerKm": 12500,
  "endShiftTankerKm": 0,
  "location": "D191",
  "station": "LV215",
  "remarks": "",
  "fueler": "H22095411",
  "fuelTransactions": [
   {
      "id": {$guid},
      "assetCode": "BHL010",
      "fuelTime": "2023-01-16 10:02",
      "remarks": "",
      "operatorDriver": "MoHENDRA",
      "reading": 10000,
      "quantity": 125,
      "driverQatarIdUrl": "",
      "logType": "Dispense",
      "isMarkDeleted": true
    }
  ],
  "isPosted": false,
  "isPartial": true
}
```

**Posting Partial Entry**
```json
    {
       "isPosted": true, 
    }
```

**Response**
```code
200 OK
```