# BookingWebApi

*Welcome to BookingWebApi, an ASP.NET Core Web API designed to facilitate accommodation booking for users*

## Technologies Used

- **Architeture style:** Clean architecture
- **Backend:** ASP.NET core API.
- **Data Management:** MSSQL (Entity Framework).
- **Authentication:** Custom implementation based on JWT.
- **Other Libraries:** FluentValidation, AutoMapper.

## Application structure

### Booking.Core
  - Defines the main entities of the application and the enumeration necessary for them
### Booking.Infrastrusture.Data
  - Defines the context of interaction with the database
  - Implements configurations to all tables
  - Contains all migrations
  - Adds storage usage through an extension method
### Booking.Identity
  - Implements services for user authorization and authentication functionality
  - Defines the rules of token generation and its filling
  - Defines available user roles
  - Adds authorization and authentication through an extension method
### Booking.Application
  - Implements the main interfaces and services od the application
  - Adds services through an extension method
### Booking.WebAPI
  - Implements the main controllers and models of the application
  - *PS: detailed description of the purpose of controller methods is described during their implementation*
  - Validation of input models using FluentValidation
  - *PS: the interface is supposed to not allow the user to interact with elements it doesn't own, so no such checks were added. (For example, the client cannot receive information about an order that does not belong to him)*

## Get started

It contains some information about the correct format of input data.

### Registe rModel
  - **Email** must be in the format *example@fmail.com*
  - **PhoneNumber** must be in format *+012345678901*
  - **Role** must be one from the enum:
    ```c#
      public enum UserRole
      {
          Admin,
          Manager,
          Client
      }
    ```
### Add/Update Accommodation Model
- **Type** must be one from the enum:
    ```c#
      public enum AccommodationType
      {
          Bed,
          Room,
          Apartament,
          House
      }
    ```
### Add/Update Hotel Model
- **Type** must be one from the enum:
    ```c#
      public enum HotelType
      {
          Hotel,
          Hostel,
          Apartments,
          House
      }
    ```
### Confirm Order Model
- **StartTime** must be greater than the current time
- **EndTime** must be greater than the *StartTime*
- **PaymentType** must be one from the enum:
    ```c#
      public enum PaymentType
      {
          Online,
          CardOnPlace,
          CashOnPlace
      }
    ```
