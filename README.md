# Bullky-Book-DotNetCoreMVC5

This project was generated with [DotNet Core MVC 5](https://docs.microsoft.com/en-us/dotnet/core/dotnet-five)

## Development server

To run locally:

- Install IIS from Windows Features
- Change your Email address & password from appsetting.json
- Run it to IIS 
- Navigate to `https://localhost:44303/`

## Application Structure

- `BulkyBook - Basic App` - The entry point to our application. The folder named BulkyBook contain the basic structure.

- `BulkyBook.DataAccess - Data Access Layer` - This folder contains Migrations and Repositories. Connection is establised through this. The app built on `Code-First Approach`

- `BulkyBook.Models` - This folder contains the models required to run BulkyBook.

- `BulkyBook.Utility` - This folder contains the constant variables and methods which are used thoughout the application.

- `Stripe` - The application has a payment gateway integration. The payment system has been attached to Stripe. Nuget package will take care of stripe

- `Unit of Work` - The application has only a connection with unit of work, which will responsible to connect repositories and database to perform crud operations.

## Dependencies
- nuget packages are the only dependancy here   
