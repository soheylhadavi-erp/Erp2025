ERP System - Backend API
Overview
A modular ERP system built with C# and .NET, using Clean Architecture, Microservices, and CQRS patterns. This is a backend-only system with multiple independent modules (General, Accounting, Sales, Inventory, etc.).

Quick Start
Open the solution (*.sln) in Visual Studio.

In Solution Explorer, navigate to the API project of the module you want to run.

For the General module: General/General.Api

For the Accounting module: Accounting/Accounting.Api

Right-click on the API project (e.g., General.Api).

Select "Set as Startup Project".

Press F5 to run the application.

The Swagger UI will open automatically in your browser where you can view and test all RESTful API endpoints.

Architecture
Clean Architecture: Separates concerns into clear layers

Microservices: Each business module is an independent, runnable service

CQRS: Uses separate commands (writes) and queries (reads)

REST API: All endpoints follow standard REST conventions

Module Structure
Each main module (like General or Accounting) contains its own projects. To run a module, set its .Api project as the startup.

Example structure:
General/
├── General.Api/ (Set this as Startup Project)
├── General.Application/
├── General.Domain/
└── General.Infrastructure/

Prerequisites
.NET SDK 8.0+

Visual Studio 2022+

SQL Server

Note: This is a backend system. Use Swagger to interact with the APIs or connect your own frontend application.

