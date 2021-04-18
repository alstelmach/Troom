# Purpose of this repository
The repository contains microservice which provides authentication and authorization functionalities. It may be used in any microservice-oriented system. Service was meant to be compliant with DDD methodology, i've also wanted to use event sourcing here. A lot of functionalities used here are encapsulated in my seed work library AsCore which you may find in one of mine repositories.

# How do i get set up?

### 1) Configuration

In the User.Api project you will find two JSON configuration files. Application chooses a file, depending on runtime environment.
###### 1.1) EncryptionSettings - passwords hashing parameters
###### 1.2) TokenSettings - token generation settings
###### 1.3) ConnectionStrings - read/write model connection strings (they might differ if you want to use different db's) for PostgreSQL database
###### 1.4) EventStoreSettings - database settings which library Marten needs
###### 1.5) Messaging - message broker settings
###### 1.6) Utility - tools like Swagger configuration
###### 1.7) Logging - default ASP.NET Core logging section (use them as you wish)

### 2) Database

The database provider for the project is PostgreSQL (for both, read/write models). Application may build it's own database if no database is found, in any of provided connection strings. Application uses default PostgreSQL management database for database creation and yet there is no possibility to configure it.

### 3) Tests

The solution contains domain logic unit tests, written with xUnit framework.

### 4) Deployment

The easiest way to deploy a service is to use Docker. To build an image run the following example command from the root directory:
*docker build -t user-service:test -f User.Api/Dockerfile .*
Then simply configure and run the container based on the image. If you are not using Docker just run a standard dotnet command with example parameters:
*dotnet publish -r win-x64 -c Release*

# Conclusions
Well, in my opinion authentication/authorization issues does not really fit the DDD methodology, and trying to be compliant with rules, complicates the process a little. The project could take more performance advantages from CQRS if we used different database providers for read/write models. I've used Dapper to improve query handling performance.

# Who do i talk to?
If you would like to contact me, please send me a mail, on the following address: 
alstelmach@outlook.com
Thanks a lot!

