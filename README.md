# Packit

**Grade Achieved: A**

Packit is a comprehensive trip planning and backpack management application developed as an exam project for the .NET course at Østfold University College. This project demonstrates modern software architecture principles with minimal external dependencies, following academic requirements to implement core functionality from scratch.

## 🎯 Project Overview

Packit is a Universal Windows Platform (UWP) application that helps users plan trips by managing backpacks and their contents. The application supports creating custom backpacks, adding items, planning trips, and sharing backpacks with other users. It integrates with weather APIs to provide trip-relevant information and includes image management capabilities.

### Key Features

- **Trip Management**: Create and organize trips with destinations and departure dates
- **Backpack Management**: Design custom backpacks with specific items for different trip types
- **Item Management**: Add, categorize, and track items across multiple backpacks
- **Sharing System**: Share backpacks with other users for collaborative trip planning
- **Weather Integration**: Get weather information for trip destinations via OpenWeatherMap API
- **Image Support**: Upload and manage images for trips and items
- **User Authentication**: Secure user registration and login system
- **Offline Support**: Local data storage with online synchronization

## 🏗️ Architecture

The project follows a modular, layered architecture with clear separation of concerns:

### Core Projects

- **Packit.Model**: Domain models and entities (Trip, Backpack, Item, User)
- **Packit.App**: UWP client application with MVVM pattern
- **Packit.App.Core**: Shared business logic and helpers
- **Packit.Database.Api**: RESTful Web API with Entity Framework Core
- **Packit.Database.Api.Repository**: Generic repository pattern implementation
- **Packit.DataAccess**: Data access layer with Entity Framework
- **Packit.Image.Api**: Dedicated image handling service

### Supporting Libraries

- **Packit.Extensions**: Custom extension methods
- **Packit.Exceptions**: Custom exception handling
- **Packit.Model.Observable**: INotifyPropertyChanged implementation
- **Packit.Database.Migrations**: Entity Framework migrations

## 📚 Academic Requirements & Minimal Dependencies

As per the .NET course requirements at Østfold University College, this project emphasizes **minimal external package usage** and demonstrates manual implementation of common patterns:

### Custom Implementations

- **Generic Repository Pattern**: Manual implementation instead of using third-party ORM abstractions
- **Data Access Layer**: Custom HTTP clients and local storage management
- **MVVM Framework**: Custom implementation of ViewModels, RelayCommand, and data binding
- **Dependency Injection**: Manual dependency management with Microsoft.Extensions.DependencyInjection
- **Factory Patterns**: Custom factory classes for data access strategies
- **Authentication Service**: Manual JWT token handling and user management

### Minimal External Dependencies

The project intentionally uses only essential packages:

- **Microsoft.NETCore.UniversalWindowsPlatform**: Core UWP framework
- **Microsoft.Extensions.DependencyInjection**: For dependency injection container
- **Newtonsoft.Json**: For JSON serialization
- **Microsoft.EntityFrameworkCore**: For database operations
- **Microsoft.Toolkit.Uwp.UI.Controls**: Essential UWP controls
- **Microsoft.CodeAnalysis.FxCopAnalyzers**: Code quality analysis

### Educational Value

This approach demonstrates:

- Understanding of fundamental .NET patterns without relying on heavy frameworks
- Manual implementation of repository and unit of work patterns
- Custom data access strategies with both HTTP and local storage
- MVVM pattern implementation from scratch
- Clean architecture principles with proper layer separation

## 🛠️ Technology Stack

- **Frontend**: Universal Windows Platform (UWP) with XAML
- **Backend**: ASP.NET Core Web API
- **Database**: Entity Framework Core with SQL Server
- **Architecture**: MVVM pattern, Repository pattern, Clean Architecture
- **Authentication**: Custom JWT implementation
- **External APIs**: OpenWeatherMap for weather data
- **Development**: Visual Studio 2019, .NET Framework/Core

## 📱 Application Structure

### Data Models

The application centers around four main entities:

- **User**: Authentication and user management
- **Trip**: Represents a planned journey with destination and dates
- **Backpack**: Container for items, can be shared between users
- **Item**: Individual objects that can be packed in backpacks

### Key Relationships

- Users can create multiple trips and backpacks
- Backpacks can be associated with multiple trips
- Items belong to backpacks through a many-to-many relationship
- Backpacks can be shared between users for collaborative planning

## 🎓 Academic Achievement

This project received an **A grade** for demonstrating:

- **Comprehensive Architecture**: Well-structured, maintainable codebase with clear separation of concerns
- **Minimal Dependencies**: Successful implementation of core patterns without relying on heavy external frameworks
- **Modern Practices**: Proper use of async/await, dependency injection, and MVVM patterns
- **Code Quality**: Consistent coding standards, comprehensive documentation, and clean code principles
- **Functionality**: Fully working application with all required features implemented
- **Technical Depth**: Understanding of .NET ecosystem, Entity Framework, and UWP development

## 📝 Course Context

**Institution**: Østfold University College  
**Course**: .NET Development  
**Year**: 2020  
**Constraint**: Minimal external package usage to demonstrate fundamental understanding  
**Achievement**: Grade A
