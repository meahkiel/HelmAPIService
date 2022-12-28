# ASSETS API


## -Get Assets
Get all Assets

**Inbound**
```
 - Portal
 - Mobile / Teams
```

**Business Rules**
- Get assets either by categories or by assetcode

**Request**

```sh
    http://{{url}}api/pmv/asset?category=&assetcode=
    Action: GET
    Header: Bearer: {{token}}
```
```json
    {
        "assetCode": "",
        "category": ""
    }
```

**Response**
```json
[
    {
        "id": "{000-000-000-000}",
        "category": "",
        "assetCode" : "",
        "subCategory": "",
        "description": ""
    }
]
```


## -View Asset By Id
View Asset By Id view

**Response**
```json
{
    "id": "",
    "assetCode": "",
    "companyCode": "",
    "color": "",
    "chasisNo": "",
    "lpoNo": "",
    "rateType": "",
    "accountCategory": "",
    "assetDesc": "",
    "conditionRank": "",
    "firsRegDate": "",
    "engineNo": "",
    "netValue": 0,
    "modifiedAsset": "",
    "rate": 0,
    "yearMakeModel": "",
    "category":"",
    "subCategory":"",
    "currentReading":0,
    "lastServiceDate":"{{Date}}",
    "latestTransactionId": "",
    "lastInspectionDate": "{{date}}",
    "serviceLogs": [
        {
            "serviceCode": "",
            "transactionId": "",
            "source": "",
            "lastServiceDate": "",
            "lastReading": 0,
            "kmAlert":0,
            "kmInterval": 0,
            "alertAtKm":0,
            "intervalAtKm":0,
            "status": ""
        }
    ]
}
```

