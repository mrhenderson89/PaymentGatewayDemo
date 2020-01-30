# PaymentGatewayDemo
 Demo of a Payment Gateway application
 
 # Requirements
The product requirements for this initial phase are the following:
1. A merchant should be able to process a payment through the payment gateway and receive either a
successful or unsuccessful response.
2. A merchant should be able to retrieve the details of a previously made payment.
 
 # Overview:
 The solution contains 2 projects:
## 1) BankMock
This is a console app which uses the WireMock .NET library to mock responses from the aquiring bank. 
A payment with an amount of 10 is rejected by the bank, to simulate a failed payment request.
A payment with any other amount will be authorized successfully.

## 2) PaymentGatewayDemo API
This API is responsible for handling payment requests. It can create a payment, and allow retrieval of a specific payment.

## 3) Postman collection
There is a Postman collection within the solution, containing requests to create/retrive payments from the API, and also to send requests to the BankMock

## 4) Unit tests
Unit tests for the project can be found within the PaymentGatewayDemo.Tests project. These unit tests use NUnit. 

## 5) Swagger
By default, the API starts at the URL for the Swagger documentation:
http://localhost:49579/swagger

This contains details on the 2 API endpoints, as well as sample requests
 
 # Development Environment:
Visual Studio 2017
SQL Server 2017
.NET Framework 4.6.1

# Run the solution locally:
1) The solution contains a Database folder, with 2 SQL scripts:
## Database_Create.sql
## Database_Populate.sql

2) Run these 2 scripts in that order to create a database called PaymentGatewayDemo, and the tables required for this application. Database_Populate will also populate the tables with sample data for Currency and Payment Status values

3) Change the connection string in the PaymentGatewayDemo Web.Config file to point to your local SQL Server database.

# Assumptions Made
1) The merchant is happy to send and receive unencrypted data to the API.
2) There are no concrete requirements on response times for the API.
3) The Payment Gateway has no need to store card details in full. Only the last 4 digits of the card number are stored, in order to return to the user when requested for their reconcilliation purposes.

# Areas of improvement
1) The Bank could post authorization responses to an endpoint in the API, which would update the status of the Payment to mark it as Successful/Declined/Cancelled etc. This would improve security, and also mean the API would not be reliant on a response from the bank in order to return the results to the user. 
A Payment could be created, but not immediately completed. And the merchant consuming the API can poll the Payment, until it has been marked complete. 
2) Access to the API could be restricted with the use of access tokens. This would improve security by restricted access only to authorized clients, for a limited amount of time. 
3) Encryption of data submitted to, and returned from the API, which would improve security by not post unencrypted payment details over a network.
4) Encryption of database tables containing merchant customer card details, to prevent access by unauthorized applications/users.
5) The Swagger documentation could be improved to contain more sample requests, as well as detailing validation on the input, and examples of error messages, and why they might occur. 
