# sbat-portal 🚀❤
Portal for client serive for a small company to see thier profiles.

# About Project Structure:
The project consist of two main projects:
- [backend](https://github.com/Takobz/sbat-portal/tree/master/backend/src) which is .NET WebAPI project
- [client](https://github.com/Takobz/sbat-portal/tree/master/client/src) which is React + Typescript project
- [database dir](https://github.com/Takobz/sbat-portal/tree/master/database) a simple nosql database used for testing, can be ignored.

The .NET Web API uses [Clean Architecture](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures) and a little bit of [Domain Driven Design](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/net-core-microservice-domain-model) design patterns to saparate concerns. This project is by no means a perfect implementation of the mentioned practices and shouldn't be used as such.

## Sources used:
- https://github.com/ardalis/CleanArchitecture
- https://github.com/jasontaylordev/CleanArchitecture
- https://www.typescriptlang.org/docs/
- https://mui.com/material-ui/
- oh can't forget https://stackoverflow.com and https://google.com 😉

# How To run project 🐱‍🏍
## .NET Web API
To run the api make sure you run it in dev environment (using dev appsettings) like this:
- navigate to the [SBAT.Web](https://github.com/Takobz/sbat-portal/tree/master/backend/src/SBAT.Web) project
- The run the command `dotent run --environment Development` all my "testing settings" are in the [appsettings.Development.json](https://github.com/Takobz/sbat-portal/blob/master/backend/src/SBAT.Web/appsettings.Development.json) file.

## React App
To then run the UI go to the [client project](https://github.com/Takobz/sbat-portal/tree/master/client)
- Then simply go `npm run start`
