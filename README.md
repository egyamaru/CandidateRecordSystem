# CandidateRecordSystem

## Dependencies
- Asp.Net Core 6
- Entity Framework
- Moq for unit testing

## How to run
The application is self-deploying. Database migrations are applied when the application is run for the first time.
1. Clone this repo
2. Open in Visual Studio
3. Run the project(Press F5 or CTRL+ F5)

This should open https://localhost:7282/swagger/index.html in the browser

## Functionality

The project is a Asp.Net Core WebAPI project. It exposes a single endpoint for inserting and updating candidate information. The decision whether to insert or update is made based on if there is any exisiting candidate with the given email.

Following is the raw request and response as seen in the Fiddler.



```HTTP
PUT https://localhost:7282/api/candidate HTTP/1.1
content-type: application/json
Host: localhost:7282
Content-Length: 311

{
  "firstName": "Test",
  "lastName": "user",
  "phoneNumber": "9841123456",
  "email": "test.user@gmaill.com",
  "preferredTimeToCall": "9AM - 11AM",
  "linkedInProfileUrl": "https://linkedin.com/testuser",
  "githubUrl": "https://github.com/testuser",
  "comments": "Asp.Net Core Backend Frontend"
}
```

*Response*:

```HTTP
HTTP/1.1 201 Created
Content-Type: application/json; charset=utf-8
Date: Thu, 28 Nov 2024 15:07:21 GMT
Server: Kestrel
Location: /candidate/1
Content-Length: 285

{"candidateId":1,"firstName":"Test","lastName":"user","phoneNumber":"9841123456","email":"test.user@gmaill.com","preferredTimeToCall":"9AM - 11AM","linkedInProfileUrl":"https://linkedin.com/testuser","githubUrl":"https://github.com/testuser","comments":"Asp.Net Core Backend Frontend"}
```

Sending the same payload again to the endpoint will update it and return 200
```HTTP
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
Date: Thu, 28 Nov 2024 15:08:04 GMT
Server: Kestrel
Content-Length: 285

{"candidateId":1,"firstName":"Test","lastName":"user","phoneNumber":"9841123456","email":"test.user@gmaill.com","preferredTimeToCall":"9AM - 11AM","linkedInProfileUrl":"https://linkedin.com/testuser","githubUrl":"https://github.com/testuser","comments":"Asp.Net Core Backend Frontend"}
```
## Project structure
There are two projects inside the solution. 
1. Web API (CandidateRecordSystem.WebApi)
2. Tests (CandidateRecordSystem.Tests)

CandidateRecordSystem.WebApi

    - Core: Contains Candidate Entity and repository interface
    - Infrastructure: Contains data access logic. Entity Framework related codes are placed here.
    - Services: Contains the actual business logic of the app that decides wether to insert or update.
    - Controller: Contains Web API endpoint.
    - Migrations: Contains EF migration files

CandidateRecordSystem.Tests

    Tests: Contains tests.

- The project uses clean/layered architecture and folders such as (Core, Infrastructure, Services, Controllers) can be placed as a seperate class library or project. But for simplicity, I have decided to place everything inside the WebAPI project itself.
- Repository pattern has been implemented to seperate the business logic from data access. To make things simple the repository interface `ICandidateRepository` has not been made a generic one and also not all functionalities that we expect from a repository have been defined.
- SQLite has been used as database.

## Changing Database later
Since Entity Framework is used as the ORM, database can be changed by changing the database provider.
For example, right now SQLite is used.

```c#
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteDb")
));
```
But if there is any database that is not supported by EF, we can still use that database by implementing `ICandidateRepository`. `ICandidateRepository` has three methods right now
1. `InsertAsync`
2. `UpdateAsync`
3. `GetByEmail`

Then we can use this new database repository by registering it to dependency inejection container in `program.cs` as show below
```c#
        builder.Services.AddScoped<ICandidateRepository, SomeNewDbCandidateRepository>();
```

If we choose to use a different ORM, we can follow the same process.

## Assumption
- Although the requirement document doesn't say database lookup will be done from the UI, I have assumed that data will filtered by `FirstName`, `LastName` and also by `Email`. Email has a unique constraint thus an index as well. Also I have created a composite index on FirstName and Lastname as I assumed thats how normally people filter or order records about candidate. This should help for efficient read even when there is a very large number of records.

## Feature I have not implemented
- Caching: I haven't added caching to this application. However, we could implement it to reduce database reads. For example, during insert or update operations, we can first check if the candidate exists in the cache. If not, we fetch the data from the database. After updating or inserting, we can store the candidate back to the cache. Asp.Net core has a great support for in-memory caching which should suffice for a demo project like this. But if we later deploy the app in multi-server environment, we can use a distributed cache like Redis. Given the possibility of adding millions of records in the future, the applicaiton would be more read-heavy. So having a cache at that time will definitely be helpful.

## Improvements that can be done
- Its good to have fields like CreatedAt, ModifiedAt columns in the DB tables for auditing and reporting. I have chosed to not add them to stick with the requirement doc.
- I have implemented basic model validation that just checks for required fields and if Email field is indeed and email. The validation can be done for character length, URI formats etc.
- The `preferredTimeToCall` is of `string` type now. This is not efficient for filtering or sorting. Instead we can use `TimeOnly` data types and split it into stard and end time.

## Time taken
Around 8 hours


