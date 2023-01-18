## Create Log API ##

```code
METHOD: POST
END POINT: http://{url}/api/pmv/fuellog
```

**Request Full Entry**
```json
{
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
      "fuelLogId": "string",
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
  "isPartial": true,
  "isPosted": false
}
```

**Response**
```json
{
  "id": "4ff14b7e-d5e0-418b-a599-2603d1fb8b00",
  "documentNo": 16,
  "openingMeter": 1500
}
```