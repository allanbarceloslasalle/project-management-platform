# API

## How to create the skeleton
dotnet new webapi -n api

## S.O.L.I.D:

S - Single Responsability Principle
O - Open/Close Principle
L - Liskov Substitution Principle
I - Interface Segregation Principle
D - Dependecy Inversion Principle

## M.V.C (Model-View-Controller)

Model : Data, business logic
View: Presentation Layer (HTML)
Controller: Processing

## Docker MSSQL

https://hub.docker.com/r/microsoft/azure-sql-edge

docker run --cap-add SYS_PTRACE -e ACCEPT_EULA=1 -e MSSQL_SA_PASSWORD=P@ssw0rd -p 1435:1433 --name AzureSQLEdge -d mcr.microsoft.com/azure-sql-edge

## DbContext
https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext?view=efcore-8.0

## JWT
https://jwt.io/