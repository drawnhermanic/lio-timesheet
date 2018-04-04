# Sample Timesheet Application

__Requirements__

* Login form with username and password
* Form to enter timesheet
* View report

## Pre requisites

* SQL Server 2016
* .NET Framework 4.7.1

## Build and run

First, publish the database project. This will create a database in your local SQL Server instance  ((LocalDb)\MSSQLLocalDB)
Build the project (this will restore the packages)  
Run the solution

## Error handling
See Elmah, e.g. http://localhost:50035/elmah

# Notes
Minimal implementation.  
No client side validation. Basic view model validation.  
View Models and domain models have not been separated.  

Deployment to Azure web app is simple addition (can include CI & CD). Web app and database to be hosted
