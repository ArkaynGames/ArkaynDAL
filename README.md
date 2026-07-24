ArkaynDAL — Data Access Layer for Eldritch Adventures
ArkaynDAL is the standalone, reusable SQLite Data Access Layer powering Eldritch Adventures: Aide, Launcher, and future EA ecosystem tools. It provides a clean abstraction over SQLite, including connection management, command execution, migrations, versioning, and mapping helpers — all without any dependency on EA‑specific domain logic.

ArkaynDAL is designed to be generic, lightweight, and engine‑agnostic, making it suitable for any .NET project requiring structured SQLite access.
<p align="center">

  <!-- Build Status -->
  <img alt="Build Status" src="https://img.shields.io/badge/build-passing-brightgreen?style=for-the-badge">

  <!-- License -->
  <img alt="License" src="https://img.shields.io/badge/license-EA%20Engine-blue?style=for-the-badge">

  <!-- .NET Version -->
  <img alt=".NET" src="https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet">

  <!-- SQLite Provider -->
  <img alt="SQLite Provider" src="https://img.shields.io/badge/SQLite-System.Data.SQLite-orange?style=for-the-badge&logo=sqlite">

  <!-- NuGet Package (placeholder) -->
  <img alt="NuGet" src="https://img.shields.io/badge/NuGet-coming_soon-lightgrey?style=for-the-badge&logo=nuget">

  <!-- Code Quality -->
  <img alt="Code Quality" src="https://img.shields.io/badge/code%20quality-verified-success?style=for-the-badge">

  <!-- Contributions -->
  <img alt="Contributions" src="https://img.shields.io/badge/contributions-welcome-ff69b4?style=for-the-badge">

</p>

Overview
ArkaynDAL provides:

A safe, async‑friendly wrapper around System.Data.SQLite

Clean interfaces for connection, command, reader, and transaction handling

A migration engine for deterministic schema evolution

Versioning support for launcher‑driven database enforcement

Logging hooks via IDalLogger (implemented in EACore_Logging)

QOL helpers for model mapping and list/single‑row retrieval

Zero EA‑specific assumptions — fully reusable

ArkaynDAL is the first module loaded by EA_Launcher and EA_Aide, ensuring the database is ready before any engine components initialize.

Project Structure
Code
ArkaynDAL/
├── Core/
│   ├── ArkaynConnection.cs
│   ├── ArkaynCommand.cs
│   ├── ArkaynReader.cs
│   ├── ArkaynTransaction.cs
│   ├── ArkaynParameter.cs
│   ├── ArkaynMapper.cs
│   └── ArkaynQueryHelpers.cs
│
├── Interfaces/
│   ├── IArkaynConnection.cs
│   ├── IArkaynCommand.cs
│   ├── IArkaynReader.cs
│   └── IArkaynTransaction.cs
│
├── Logging/
│   └── IDalLogger.cs
│
├── Migrations/
│   ├── IMigration.cs
│   ├── MigrationRunner.cs
│   └── BaseMigration.cs
│
└── Models/
    └── SchemaVersion.cs
Core Components
IArkaynConnection
Manages SQLite connection lifecycle and exposes high‑level execution helpers.

IArkaynCommand
Wraps parameterized SQL commands with async and sync execution paths.

IArkaynReader
Safe abstraction over SQLiteDataReader, including metadata access.

IArkaynTransaction
Simple wrapper for commit/rollback operations.

ArkaynMapper / ArkaynQueryHelpers
QOL utilities for mapping rows to POCOs and retrieving lists/single objects.

Migrations
ArkaynDAL includes a deterministic migration engine:

IMigration defines a migration unit

MigrationRunner executes migrations in ascending Order

BaseMigration simplifies SQL execution

EA‑CT provides its own migration classes; ArkaynDAL only provides the mechanism.

Schema Versioning
SchemaVersion tracks:

Major version

Minor version

Build (YYWW)

ArkaynConnection exposes:

GetSchemaVersion()

SetSchemaVersion()

The launcher uses this to enforce schema compatibility before EA_Aide loads.

Logging
ArkaynDAL defines IDalLogger, implemented externally by:

Code
EACore_Logging.dll
This avoids circular dependencies and keeps ArkaynDAL reusable.

Loggable events include:

Connection opened/closed

Query execution duration

Errors/exceptions

Dependency Injection
ArkaynDAL is DI‑friendly:

csharp
services.AddSingleton<IDalLogger, EALogger>();
services.AddSingleton<IArkaynConnection>(sp =>
    new ArkaynConnection(connString, sp.GetRequiredService<IDalLogger>())
);
NuGet Dependencies
ArkaynDAL uses:

Code
System.Data.SQLite.Core
No dependency on Microsoft.Data.Sqlite.

Testing
ArkaynDAL interfaces are fully mockable, enabling:

unit tests for repositories

migration tests

versioning tests

DAL behavior tests

Future Enhancements
Reflection‑based migration discovery

Optional connection pooling

Extended mapper customization

Structured logging metadata

Performance instrumentation hooks

License
ArkaynDAL is part of the Eldritch Adventures engine ecosystem and follows the same licensing model as the EA project
