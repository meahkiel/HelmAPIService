# Service Alert API

## **End Points**

 ***CreateUpdateAlert***
 ```sh
 POST - http://{{url}}api/pmv/assetalert/
 ```
 ***GetAlerts***
 ```sh
 GET - http://{{url}}api/pmv/assetalert/
 ```
 ***GetAlertById***
 ```sh
 GET - http://{{url}}api/pmv/assetalert/{id}
 ```

## - CreateUpdateAlert

**Inbound Rules**
- Single Entry
- Detail Rules
    - if delete component must change tracker to delete
    - if modified must change tracker to mod
    - if new id should be empty or null

**Business Rules**
 - Group must be unique
 - Update the detail only once it saved
 - Trigger a Create Alert event 

**Request**

```sh
    http://{{url}}api/pmv/assetalert/
    Action: POST
    Accept: application/json
    Header: Bearer: {{token}}
```
```json
{
    "id": "",
    "groupName": "",
    "description":"",
    "assigned":"",
    "categories": "heavy lift,land",
    "details": [ 
        {
            "id": "{000-000-000-000-000}",
            "serviceCode": "SRVC01",
            "kmAlert": 0,
            "kmInterval": 0,
            "markDelete":false
        }
    ]
}
```
**Validation / Input**
```sh
 - ServiceAlertRequet
 
 - Group name must be unique
 - must have assigned 
 - if assigned is category then
    - category must have value 
```

**Error Exception**
```sh
 - UniqueGroupException()
 - RequiredFieldException()
 - UknownErrorException()
 - NotFoundException()
```

**TODO**
```code
var user = userAccessor.GetEmployeeCode();
if(req.Id is null) 
    var alert = map.Map(request.Alert);
    rep.Add(alert);
 else
    var alert = rep.getById(id);
    alert.update(map.Map(request.Alert));
    rep.Update(alert);

rep.saveAsync(user.empCode);
```

**Response**
```sh
    Response: 200 Ok 
    Response Error: 400
```


## - GetAlerts 
Get All service alerts for editing

**Inbound Rules**
```code
- use for portal app
- no parameter require
```

**Business Rules**
- No parameter required

**Request**

```sh
    http://{{url}}api/pmv/assetalert/
    Action: GET
    Accept: application/json
    Header: Bearer: {{token}}
```

**Response**
```sh
Content/Type:  application/json
```
```json 

[
    {
        "id": "{0000-0000-0000-00000}",
        "groupName": "",
        "description": "",
        "assigned":"",
        "category":""
        "details": [
            {
                "id": "",
                "serviceCode" :"",
                "kmAlert":0,
                "kmInterval": 0
            }
        ]
    }
]
```

## - GetAlertById
Get service alert by Id for editing

**Inbound Rules**
```code
- use for portal app
- Id required
```

**Business Rules**
- Id

**Request**

```sh
    http://{{url}}api/pmv/assetalert/{id}
    Action: GET
    Accept: application/json
    Header: Bearer: {{token}}
```

**Response**
```sh
Content/Type:  application/json
```
```json 

[
    {
        "id": "{0000-0000-0000-00000}",
        "groupName": "",
        "description": "",
        "assigned":"",
        "category":""
        "details": [
            {
                "id": "",
                "serviceCode" :"",
                "kmAlert":0,
                "kmInterval": 0
            }
        ]
    }
]
```

