# REST-service "City Telephone Network"

A RESTful API for managing the data of a city telephone network. The project was developed on the .NET 6 platform using ASP.NET Core Web API and Entity Framework Core.

---

## Project Description

The API provides an interface for managing the main entities of the telephone network:

*   **Subscribers:** information about the network's clients.
*   **Automatic Telephone Exchanges (ATE):** information about the exchanges to which subscribers are connected.
*   **Subscription Payments:** history of subscription payments.

## Technology Stack

*   **Platform:** .NET 6
*   **ORM:** Entity Framework Core
*   **Database:** MS SQL Server (LocalDB)
*   **Architectural Patterns:**
    *   Repository Pattern
    *   Unit of Work
    *   Layered Architecture
*   **Tools:**
    *   **AutoMapper:** for mapping domain models to DTOs (Data Transfer Objects).
    *   **Swagger (OpenAPI):** for interactive documentation and API testing.

## How to run the project

### Prerequisites

*   .NET 6 SDK
*   SQL Server LocalDB (usually installed with Visual Studio)
*   Git

### Instructions

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/Drakollla/TelephoneNetworkApi.git
    ```

2.  **Configure the database connection string:**
    The project is configured by default to use MS SQL Server LocalDB. The connection string can be found in `Program.cs` or `appsettings.json`.
    ```json
    "Server=(localdb)\\mssqllocaldb;Database=TelephoneNetworDB;Trusted_Connection=True;"
    ```
    **Note:** It is not necessary to run migrations (`dotnet ef database update`). On the first launch, the `TelephoneNetworDB` database will be created and seeded with test data automatically.

3.  **Open the Swagger documentation:**
    After launching, navigate to `http://localhost:5000/swagger` in your browser (the port may vary, check the console output). You can test all available endpoints through Swagger.

## Database Schema

### Subscribers

| Column          | Type       | Constraints | Description                                   |
| --------------- | ---------- | ----------- | --------------------------------------------- |
| `Id`            | `int`      | Primary Key | Unique identifier                             |
| `SecondName`    | `nvarchar` |             | Last Name                                     |
| `Name`          | `nvarchar` |             | First Name                                    |
| `Surname`       | `nvarchar` |             | Patronymic/Middle Name                        |
| `PhoneNumber`   | `nvarchar` |             | Phone Number                                  |
| `IsIntercityOpen` | `bit`    |             | Flag indicating if long-distance calls are enabled |

### AutomaticTelephoneExchanges (ATEs)

| Column            | Type       | Constraints | Description                         |
| ----------------- | ---------- | ----------- | ----------------------------------- |
| `Id`              | `int`      | Primary Key | Unique identifier                   |
| `Name`            | `nvarchar` |             | Name/number of the ATE              |
| `Town`            | `nvarchar` |             | City of location                    |
| `CountSubscriber` | `int`      |             | Number of connected subscribers     |

### RegistrySubscriptionPayments

| Column                | Type      | Constraints | Description                             |
| --------------------- | --------- | ----------- | --------------------------------------- |
| `Id`                  | `int`     | Primary Key | Unique identifier                       |
| `Mounth`              | `int`     |             | Billing month                           |
| `Year`                | `int`     |             | Billing year                            |
| `TownshipMinuteCount` | `tinyint` |             | Number of local call minutes            |
| `IntecityMinuteCount` | `tinyint` |             | Number of long-distance call minutes    |
| `Price`               | `decimal` |             | Amount to be paid                       |
| `SubscriberId`        | `int`     | Foreign Key | Foreign key referencing the subscriber |

### AtsSubscribers (ATE-Subscriber Junction Table)

This is a join table that implements a many-to-many relationship between `AutomaticTelephoneExchanges` and `Subscribers`.

| Column                       | Type  | Constraints | Description                                  |
| ---------------------------- | ----- | ----------- | -------------------------------------------- |
| `Id`                         | `int` | Primary Key | Surrogate primary key                        |
| `AutomaticTelephoneExchangeId` | `int` | Foreign Key | Foreign key to `AutomaticTelephoneExchanges` |
| `SubscriberId`               | `int` | Foreign Key | Foreign key to `Subscribers`                 |
