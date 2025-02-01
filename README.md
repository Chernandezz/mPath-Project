
# mPATH Assessment Project Documentation


## Table of Contents

1. Introduction
2. Project Overview
3. Setup Instructions
4. Architecture Overview
5. Authentication and Authorization
6. API Documentation
7. Database Connection
8. Deployment Instructions
9. Additional Features
10. Additional Features
11. Developed By

---

## Introduction

The **mPATH Assessment Project** is a healthcare-focused web application designed to manage patient recommendations. The system enables users to log in, browse a list of patients, and view recommendations such as allergy checks and screenings. This document provides a comprehensive guide to setting up, running, and understanding the project.

## Project Overview

- **Technologies Used:**
  - **Backend:** .NET Core 9, Entity Framework
  - **Frontend:** Angular 19 with Angular Material
  - **Database:** SQL Server
  - **Security:** JWT Authentication
  - **Infrastructure:** Docker for containerization
- **Roles:**
  - Admin
  - Doctor
  - Patient
- **Default Users:**
  - **Admins:** `admin@mpath.com / admin`
  - **Patients:**
    - `patient1@mpath.com / patient1`
    - `patient2@mpath.com / patient2`
    - `patient3@mpath.com / patient3`
    - `patient4@mpath.com / patient4`
  - **Doctors:**
    - `doctor1@mpath.com / doctor1`
    - `doctor2@mpath.com / doctor2`
    - `doctor3@mpath.com / doctor3`
    - `doctor4@mpath.com / doctor4`

---

## Setup Instructions

### Prerequisites

Ensure you have Docker and Docker Compose installed. No additional dependencies are required since all necessary services are containerized.

### Running the Application

#### Using Docker (Recommended)

```sh
# Clone the repository
git clone https://github.com/Chernandezz/mPath-Project
cd <repo>

# Run the project using Docker
docker-compose up --build
```

#### Running Manually

```sh
# Backend
cd backend/mPathProject
dotnet build
dotnet run

# Frontend
cd frontend
npm install
npm start
```

### Ports

- **Backend:** `8080`
- **Frontend:** `80`
- **Database:** `1400:1433`

---

## Architecture Overview

The project follows **Clean Architecture**, ensuring separation of concerns:

- **API Layer:** Handles HTTP requests and responses.
- **Domain Layer:** Contains business logic and entities.
- **Infrastructure Layer:** Handles database interactions, external services, and repositories.
- **Application Layer:** Implements use cases and DTOs.

---

## Authentication and Authorization

- Authentication is handled using **JWT (JSON Web Tokens)**.
- **Role-based access control (RBAC)** is implemented for:
  - **Admins:** Full system access.
  - **Doctors:** Can manage and review patients, admissions, and discharges. They can **create** patients, admissions, and discharges.
  - **Patients:** Can only **view** their own admissions, discharges, and recommendations. They **cannot** create admissions, discharges, or recommendations.

---

## API Documentation

The API endpoints can be accessed and tested using Swagger UI at:

```
http://localhost:8080/swagger
```
---

## Database Connection

You can connect to the database using any SQL Server graphical interface, such as **SQL Server Management Studio (SSMS)** or **Azure Data Studio**, with the following credentials:

- **Server:** `localhost, 1400`
- **Database:** `mPathDB`
- **User:** `sa`
- **Password:** `StrongPassword!2025`

This allows direct access for running queries, inspecting data, and performing database management tasks.

---

## Deployment Instructions

### Docker-based Deployment:

```sh
docker-compose up --build -d
```

- **Manual Deployment:**
  1. Deploy the backend on a cloud service (Azure, AWS, etc.).
  2. Deploy the frontend using an Nginx server.
  3. Configure environment variables for database and API endpoints.

---

## Additional Features

- **Angular Material** for a responsive UI.
- **Audit Logging:** Logs all interactions, including data creation events.
- **Optimized for Mobile and Tablet Devices**.


---

## Developed By

**Cristian Hernandez**  
Email: cristianfzhernandez@gmail.com  
Phone: +1 386 450 7386  
