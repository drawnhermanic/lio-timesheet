# Sample Timesheet Application

__Requirements__

1. Login form with username and password
2. Form to enter timesheet - *data not persisted*
3. View report - *not completed*

## Pre requisites

* SQL Server 2016
* .NET Framework 4.7.1

## Components

* MVC application `Timesheet`
* Dapper repository `UserRepository` in `Timesheet.Data`
* StructureMap Ioc
* SpecsFor and SpecsFor.Mvc for unit and integration tests
* Database project `Timesheet.Database`
## Build and run

First, build the project (this will restore the packages).  
Publish the database project. This will create a database in your local SQL Server instance  ((LocalDb)\MSSQLLocalDB). Use the DEV publish profile.

Run the solution

## Error handling
See Elmah, e.g. http://localhost:50035/elmah

# Notes
Minimal implementation.  
No client side validation. Basic view model validation.  
View Models and domain models have not been separated.  

Deployment to Azure web app is simple addition (can include CI & CD). Web app and database to be hosted
