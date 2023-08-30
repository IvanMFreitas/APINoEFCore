
  

# .NET Core API

  

This is a generic API, created, using some technologies and design patterns.


## :desktop_computer:Technology Stack

**.NET 7**

**ADO.NET**

**Microsoft SQL Server w/ Docker**

**JWT Authentication**

**nUnit for testing**

### Also used as Pattern:

**Dependency Injection**

**Repository Pattern**

## :books:Models

There are 3 models on the Database that you should know:

**Person** with fields:

- Id (Guid)
- Name (String)
- Email (String)
- PasswordHash (String)
- Salt(String)
- IsAdmin (Boolean)
- CreatedAt (DateTime)

**Product** with fields:

- Id (Guid)
- Name (String)
- Price (Decimal)
- CreatedAt (DateTime)

**Order** with fields:

- Id (Guid)
- PersonId (Guid - FK)
- ProductId (Guid - FK)
- Qty (Integer)
- Total (Decimal)
- CreatedAt (DateTime)


## :bookmark_tabs:API Documentation

### :arrow_down:GET Endpoints

#### :arrow_down:Returns a Product by its Id
```http
GET /api/product/{id}
```

#### :arrow_down:Returns a Person by its Id
```http
GET /api/person/id/{id}
```

#### :arrow_down:Returns a Person by its Email address
```http
GET /api/person/email/{email}
```

#### :arrow_down:Returns an Order by its Id
```http
GET /api/order/{id}
```

#### :arrow_down:Returns a valid TOKEN, to use on "Create Order" for authentication
```
GET /api/person/login/{email}/{password}
```

### :arrow_up:POST Endpoints

#### :arrow_up:Creates a new Person
```
POST /api/person/create
```
and the body for Person Creation is:
```
{
	"name": "Ivan Freitas",
	"email": "ivan.freitas@outlook.com",
	"password": "123456",
	"isAdmin": true
}
```
#### :arrow_up:Creates a new Order

**:closed_lock_with_key:Should have as Auth, a valid JWT Token, generated previously on "Login" (GET Method):closed_lock_with_key:**

```http
POST /api/order/createOrder
```

and the body for this POST invoke, should be like this:

```
{
	"productId": "36B119C3-55FA-42FC-948D-F7F314CBDA60",
	"qty": 5
}
```
### :arrows_counterclockwise:PATCH Endpoints

#### :arrows_counterclockwise:Updates a Person
```
PATCH /api/person/update/{email}
```
and the body for this PATCH invoke, should be like this:
```
{
	"name": "Ivan Freitas Updated",
	"isAdmin": false
}
```
#### :arrows_counterclockwise:Changes a Password
```
PATCH /api/person/changePwd/
```
and the body for this PATCH invoke, should be like this:
```
{
	"email": "ivan.freitas@outlook.com",
	"password": "654321"
}
```
### :no_entry:DELETE Endpoints

#### :no_entry:Deletes a Person
```
DELETE /api/person/delete/{email}
```

## :package:Docker, Compose and SQL Server

The project uses a docker-compose manifest 
```
docker-compose.yml
```
to lift a valid Microsoft SQL Database and execute the solution.  When you run the command: 
```
docker-compose up --build
```
the solution should be built and you can run it on "localhost".

But unfortunately, you should STILL (for now) run the 
```
01.InitialScript.sql
```
on the Database, after the first run. One of the evolutions to implement on this sample API consists of running this script, as soon as the database is online for the first time.

## :fast_forward:API Evolution - Next Steps

**:next_track_button:Add Authentication for all methods**

**:next_track_button:Add Swagger for API documentation**

**:next_track_button:Add command to run Initial Script at Database creation on Docker Compose**

**:next_track_button:Add Stress tests using K6 Framework**