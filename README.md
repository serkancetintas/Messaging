# Messaging

Run
----------
```
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```

User SignUp Api
--------------------------
**Url:** http://localhost:5050/api/User \
**Request Type:** POST 

**Body:**
```json
{        
    "Email": "huseyin@gmail.com",
    "UserName": "huseyintest",
    "Password": "abRcdf14"
}
```

User Sign In Api
------------------
**Url:** http://localhost:5050/api/User/SignIn \
**Request Type:** POST 

**Body:**
```json
{
    "UserName":"huseyintest",
    "Password": "abRcdf14"
}
}
```

**Result:**
```json
{
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI0ODM0Yzc5MS01MWExLTQyNGItOGYzNi05M2E3ODhiNzNjZDUiLCJ1bmlxdWVfbmFtZSI6IjQ4MzRjNzkxLTUxYTEtNDI0Yi04ZjM2LTkzYTc4OGI3M2NkNSIsImp0aSI6ImVjOWE2ZDIyLTVmNWQtNDJjNy05MmM2LTM2ODRhMDgxNDM5MiIsImlhdCI6IjE2MDkxMzg4MzIiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJ1c2VyIiwibmJmIjoxNjA5MTM4ODMyLCJleHAiOjE2MDkxNDA2MzIsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NjQwNzUiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwMDAifQ.NmZoAU3_-anYRd5A4O05IyhBiKESCjM2uD5dORh4FG0",
    "expires": 1609140632
}
```
Send Message Api
----------------
**Url:** http://localhost:5050/api/Message \
**Request Type:** POST \
**Authorization:** Bearer {{ accessToken }}

**Body:** 
```json
{
    "ToUserName":"serkancetintas",
    "Content": "selam"
}
```

Get Messages  Api
------------------
You can see your messaging history with a user. 

**Url:** http://localhost:5050/api/Message/{{ userName }} \
**Sample Url:** http://localhost:5050/api/Message/huseyintest \
**Request Type:** GET \
**Authorization:** Bearer {{ accessToken }} 

**Result:**
```json
[
    {
        "id": "d5b920fb-dfd2-4b3e-a213-53ce96e0d999",
        "fromUserName": "huseyintest",
        "myUserName": "serkancetintas",
        "content": "selam",
        "createdAt": "2020-12-28T07:03:41.846Z"
    }
]
```

Block User Api
---------------
**Url:** http://localhost:5050/api/BlockUser \
**Request Type:** POST \
**Authorization:** Bearer {{ accessToken }} 

**Body:**
```json
{
    "UserName": "huseyintest"
}
```
Loglara Erişim
---------------
**Url:** http://localhost:5341/

