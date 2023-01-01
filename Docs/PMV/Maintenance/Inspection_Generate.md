# Inspection Generate Inspection List API

**Inbound Source**
- Mobile
- Portal


**Business Rule**

Generate an initial inspection list from the alert setup

- check if the asset has an existing record
- if no record then start to create new record
- return a new inspection record

**Request**

```
METHOD: GET
http:\\{url}\api\pmv\asset\create-service\{assetId}
```

```json
{
    "AssetId":0,
}
```
**TODO**
```
    var asset = unitWork.Assets.GetAssetById(assetCode);
    if(asset == null) throw NotFoundEntityException("asset");

    var alertSetup = await unitWork.ServiceAlert.GetAlertSetup(asset.Category);
    
    return alertSetup



```

**Result**
