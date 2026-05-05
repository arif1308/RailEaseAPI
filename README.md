# 🚂 RailEase API — Backend

ASP.NET Core Web API for RailEase Train Reservation System.

## 🔗 Links
- **Frontend:** [RailEase](https://github.com/arif1308/RailEase)
- **Backend:** [RailEaseAPI](https://github.com/arif1308/RailEaseAPI)

## 🛠️ Tech Stack
- ASP.NET Core Web API (C#)
- Entity Framework Core
- SQL Server

## 📁 Structure
- **Controllers/** — Auth, Train, Booking APIs
- **Models/** — User, Train, Booking, TrainCategory
- **Data/** — AppDbContext
- **Migrations/** — Database migrations

## 🚀 How to Run
cd RailEaseAPI
dotnet run

## API Endpoints
- POST /api/auth/register
- POST /api/auth/login
- PUT /api/auth/update/{id}
- PUT /api/auth/change-password/{id}
- GET /api/train
- GET /api/train/search
- POST /api/booking
- GET /api/booking/user/{userId}
- PUT /api/booking/cancel/{id}

## 👨‍💻 Developer
**Arif Siddique**