# BankApi

Simple banking API built with .NET 10 using a layered architecture (Core, Application, Infrastructure, Api) and PostgreSQL.

## Architecture

This project follows a Clean Architecture / Onion-style organization:

- `BankApi.Core`: domain entities, enums, and interfaces (repositories, unit of work, security abstractions).
- `BankApi.Application`: use cases (application services), request/response contracts, DI registration.
- `BankApi.Infrastructure`: EF Core `DbContext`, repository implementations, unit of work implementations, JWT and password hashing.
- `BankApi.Api`: minimal API endpoints, auth configuration, validation, endpoint mapping.

Main dependency direction:

- `Api` -> `Application` + `Infrastructure`
- `Application` -> `Core`
- `Infrastructure` -> `Core`
- `Core` has no dependency on other layers.

## Project Structure

```text
BankApi/
  src/
    BankApi.Api/
    BankApi.Application/
    BankApi.Core/
    BankApi.Infrastructure/
  tests/
    BankApi.Core.UnitTests/
```

## Implemented Business Rules

### Customer

- A customer cannot be registered with an email that already exists.
- A customer cannot be registered with a document that already exists.
- Password is hashed before persistence.
- Register flow creates customer + initial account in one unit of work.

### Authentication

- Login requires an existing customer email.
- Password must match stored hash.
- On success, returns JWT token with customer id (`sub`) and email claims.

### Account and Transactions

- Deposit amount must be greater than zero.
- Withdraw amount must be greater than zero.
- Savings account withdraw requires sufficient balance.
- Current account withdraw requires sufficient balance including a 1% fee.
- Current account cannot withdraw more than 50% of current balance per operation.
- Transfer requires different source and destination accounts.
- Transfer creates two transaction records: `TransferDebit` for source account and `TransferCredit` for destination account.

## Validation

Endpoints use FluentValidation.

Current request validations include:

- `RegisterCustomerRequest`: required fields, email format, password minimum length, document/phone length, birth date not in future, valid account type.
- `AuthenticateCustomerRequest`: required email/password, email format.
- `CreateInternalTransactionRequest`: non-empty account id and amount > 0.
- `CreateTransferRequest`: non-empty source/destination ids, amount > 0, source != destination.

## API Routes

Base routes:

- `POST /api/v1/customers` (public): register customer.
- `POST /api/v1/customers/authenticate` (public): authenticate and receive JWT.
- `GET /api/v1/accounts` (authenticated): list accounts.
- `POST /api/v1/accounts/deposit` (authenticated): create deposit transaction.
- `POST /api/v1/accounts/withdraw` (authenticated): create withdraw transaction.
- `POST /api/v1/accounts/transfer` (authenticated): transfer between accounts.

## Persistence

- Database: PostgreSQL.
- ORM: EF Core with Npgsql provider.
- Entities mapped in `AppDbContext`: `Customers`, `Accounts`, `Transactions`.

## Local Run

### Prerequisites

- .NET SDK 10
- Docker (optional, to run PostgreSQL)

### 1) Start PostgreSQL (Docker)

```bash
docker compose up -d
```

### 2) Apply migrations

```bash
dotnet ef database update --project src/BankApi.Infrastructure --startup-project src/BankApi.Api
```

### 3) Run API

```bash
dotnet run --project src/BankApi.Api
```

## Configuration

Main settings are in:

- `src/BankApi.Api/appsettings.json`
- `src/BankApi.Api/appsettings.Development.json`

Important keys:

- `ConnectionStrings:DefaultConnection`
- `Jwt:Key`
- `Jwt:Issuer`
- `Jwt:Audience`
- `Jwt:ExpiresInMinutes`

## Tests

Unit tests are in `tests/BankApi.Core.UnitTests`.

Run:

```bash
dotnet test tests/BankApi.Core.UnitTests/BankApi.Core.UnitTests.csproj -c Debug --no-restore -v minimal -m:1
```

Covered use cases:

- Register customer
- Authenticate customer
- Deposit
- Withdraw
- Transfer
