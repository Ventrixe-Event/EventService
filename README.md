# Ventixe EventService

A .NET 9 microservice for managing events in the Ventixe platform, providing comprehensive event management capabilities with rich mock data.

## 🌐 Live Demo

**🚀 Deployed API**: Coming soon - ready for Azure deployment

**📖 API Documentation**: `/swagger` (available after deployment)

## 📋 Overview

The EventService is a RESTful API microservice built with .NET 9 that manages event operations for the Ventixe platform. It provides endpoints for creating, reading, updating, and deleting events with various filtering and search capabilities. The service comes pre-loaded with rich mock data representing various event types.

## 🛠️ Technologies Used

- **.NET 9** - Latest version of .NET framework
- **ASP.NET Core** - Web API framework
- **Entity Framework Core** - Object-Relational Mapping (ORM)
- **SQL Server** - Database (configurable)
- **Swagger/OpenAPI** - API documentation
- **Clean Architecture** - Layered architecture pattern

## 🏗️ Architecture

The service follows Clean Architecture principles with clear separation of concerns:

```
EventService/
├── Presentation/           # API controllers and configuration
│   ├── Controllers/        # REST API controllers
│   └── Program.cs         # Application entry point
├── Application/           # Business logic and services
│   ├── Interfaces/        # Service contracts
│   ├── Models/           # DTOs and view models
│   └── Services/         # Business logic implementation
└── Persistence/          # Data access layer (for future DB implementation)
    ├── Contexts/         # Entity Framework DbContext
    ├── Entities/         # Database entities
    └── Interfaces/       # Repository contracts
```

## 🔧 Setup Instructions

### Prerequisites

- .NET 9 SDK
- Visual Studio 2022 or JetBrains Rider (optional)

### Local Development

1. **Clone the repository**

   ```bash
   git clone <your-repo-url>
   cd EventService
   ```

2. **Restore dependencies**

   ```bash
   dotnet restore
   ```

3. **Start the service**

   ```bash
   cd Presentation
   dotnet run
   ```

4. **Access API documentation**
   Navigate to `https://localhost:7003/swagger` (or the displayed URL)

## 📡 API Endpoints

### Event Management

| Method   | Endpoint                               | Description            |
| -------- | -------------------------------------- | ---------------------- |
| `GET`    | `/api/events`                          | Get all events         |
| `GET`    | `/api/events/{id}`                     | Get event by ID        |
| `GET`    | `/api/events/category/{category}`      | Get events by category |
| `GET`    | `/api/events/status/{status}`          | Get events by status   |
| `GET`    | `/api/events/search?searchTerm={term}` | Search events          |
| `POST`   | `/api/events`                          | Create new event       |
| `PUT`    | `/api/events/{id}`                     | Update existing event  |
| `DELETE` | `/api/events/{id}`                     | Delete event           |

### Request/Response Examples

#### Get All Events

```http
GET /api/events
```

**Response:**

```json
{
  "success": true,
  "data": [
    {
      "id": "1",
      "title": "Adventure Gear Show",
      "description": "Discover the latest in outdoor adventure equipment and gear",
      "category": "Outdoor & Adventure",
      "location": "Rocky Mountain Exhibition Hall, Denver, CO",
      "startDate": "2026-06-05T00:00:00",
      "startTime": "17:00:00",
      "price": 40.0,
      "status": "Active",
      "progress": 65,
      "maxAttendees": 500,
      "currentAttendees": 325,
      "organizerName": "Mountain Adventures Inc",
      "isActive": true,
      "formattedDate": "Jun 05, 2026",
      "formattedTime": "5:00 PM",
      "formattedDateAndTime": "Jun 05, 2026 - 5:00 PM"
    }
  ]
}
```

#### Get Events by Category

```http
GET /api/events/category/Music
```

#### Search Events

```http
GET /api/events/search?searchTerm=festival
```

#### Create Event

```http
POST /api/events
Content-Type: application/json

{
  "title": "New Music Festival",
  "description": "An amazing music experience",
  "category": "Music",
  "location": "Central Park, New York, NY",
  "startDate": "2026-07-15T00:00:00",
  "startTime": "18:00:00",
  "price": 75.00,
  "maxAttendees": 1000,
  "organizerName": "Music Events Co",
  "organizerEmail": "info@musicevents.com"
}
```

## 🗃️ Data Models

### EventDto

```csharp
public class EventDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string Category { get; set; }
    public string Location { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public decimal Price { get; set; }
    public string Status { get; set; }
    public int Progress { get; set; }
    public int MaxAttendees { get; set; }
    public int CurrentAttendees { get; set; }
    public string? OrganizerName { get; set; }
    public string? OrganizerEmail { get; set; }
    public bool IsActive { get; set; }

    // Formatted properties
    public string FormattedDate { get; }
    public string FormattedTime { get; }
    public string FormattedDateAndTime { get; }
}
```

### CreateEventRequest

```csharp
public class CreateEventRequest
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [Required]
    [StringLength(100)]
    public string Category { get; set; }

    [Required]
    [StringLength(500)]
    public string Location { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public TimeSpan StartTime { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue)]
    public int MaxAttendees { get; set; }

    public string? OrganizerName { get; set; }

    [EmailAddress]
    public string? OrganizerEmail { get; set; }
}
```

## 🎯 Features

### 📊 Event Management

- **Full CRUD Operations**: Create, read, update, and delete events
- **Rich Event Data**: Comprehensive event information including dates, times, pricing, and attendance
- **Category Organization**: Events organized by categories (Music, Fashion, Technology, etc.)
- **Status Tracking**: Track event status and progress

### 🔍 Advanced Filtering & Search

- **Category Filtering**: Get events by specific categories
- **Status Filtering**: Filter events by status (Active, Draft, Completed)
- **Full-text Search**: Search across event titles, descriptions, categories, and locations
- **Date-based Queries**: Future capability for date range filtering

### 📈 Event Analytics

- **Attendance Tracking**: Monitor current vs. maximum attendees
- **Progress Monitoring**: Track event preparation progress
- **Organizer Information**: Store and retrieve event organizer details

## 🎪 Mock Event Data

The service comes with rich mock data including:

| Event                      | Category            | Location          | Price | Progress |
| -------------------------- | ------------------- | ----------------- | ----- | -------- |
| Adventure Gear Show        | Outdoor & Adventure | Denver, CO        | $40   | 65%      |
| Symphony Under the Stars   | Music               | Los Angeles, CA   | $50   | 75%      |
| Runway Revolution 2029     | Fashion             | New York, NY      | $100  | 50%      |
| Global Wellness Summit     | Health & Wellness   | Miami, FL         | $75   | 40%      |
| Artistry Unveiled Expo     | Art & Design        | Chicago, IL       | $20   | 85%      |
| Culinary Delights Festival | Food & Culinary     | San Francisco, CA | $45   | 60%      |
| Echo Beats Festival        | Music               | Los Angeles, CA   | $60   | 70%      |
| Tech Future Expo           | Technology          | San Jose, CA      | $80   | 55%      |

## 🚀 Deployment

### Azure App Service

The service is ready for deployment to Azure App Service:

```bash
# Build for production
dotnet publish Presentation/Presentation.csproj -c Release -o ./publish

# Deploy to Azure (using Azure CLI)
az webapp deployment source config-zip \
  --name eventservice-[unique-string] \
  --resource-group your-resource-group \
  --src publish.zip
```

### Environment Configuration

For production deployment, configure these settings in Azure:

```json
{
  "ASPNETCORE_ENVIRONMENT": "Production"
}
```

## 🔒 Security Features

- **CORS Configuration**: Allows cross-origin requests from frontend applications
- **Input Validation**: Comprehensive model validation on all endpoints
- **Error Handling**: Consistent error responses with proper HTTP status codes
- **HTTPS**: Enforced in production environments

## 🧪 Testing

### Run Unit Tests

```bash
dotnet test
```

### API Testing with curl

```bash
# Get all events
curl -X GET "https://localhost:7003/api/events"

# Get events by category
curl -X GET "https://localhost:7003/api/events/category/Music"

# Search events
curl -X GET "https://localhost:7003/api/events/search?searchTerm=festival"

# Create new event
curl -X POST "https://localhost:7003/api/events" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Test Event",
    "category": "Technology",
    "location": "Test Location",
    "startDate": "2026-12-01T00:00:00",
    "startTime": "14:00:00",
    "price": 50.00,
    "maxAttendees": 100
  }'
```

## 📊 Event Categories

The service supports various event categories:

| Category                | Description                         | Example Events                |
| ----------------------- | ----------------------------------- | ----------------------------- |
| **Music**               | Concerts, festivals, performances   | Symphony, Echo Beats Festival |
| **Technology**          | Tech conferences, expos             | Tech Future Expo              |
| **Fashion**             | Fashion shows, exhibitions          | Runway Revolution             |
| **Food & Culinary**     | Food festivals, culinary events     | Culinary Delights Festival    |
| **Health & Wellness**   | Health conferences, wellness events | Global Wellness Summit        |
| **Art & Design**        | Art exhibitions, design shows       | Artistry Unveiled Expo        |
| **Outdoor & Adventure** | Outdoor activities, adventure gear  | Adventure Gear Show           |

## 🔄 Integration

### Frontend Integration

The service integrates seamlessly with the Ventixe Frontend:

```javascript
// Frontend service usage
import { eventService } from "./services/eventService";

// Get all events
const events = await eventService.getAllEvents();

// Search events
const searchResults = await eventService.searchEvents("music");

// Create event
const newEvent = await eventService.createEvent({
  title: "New Event",
  category: "Music",
  location: "Venue Name",
  startDate: "2026-12-01T00:00:00",
  startTime: "19:00:00",
  price: 75.0,
});
```

### Microservices Integration

- **InvoiceService**: Events can be linked to invoices for ticket sales
- **FeedbackService**: Collect feedback for specific events
- **Future Services**: User management, booking systems, notifications

## 🔧 Configuration

### Development Configuration (appsettings.Development.json)

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### Production Configuration

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  }
}
```

## 🐛 Troubleshooting

### Common Issues

1. **Service Registration Errors**

   - Ensure all services are properly registered in Program.cs
   - Check project references are correct

2. **CORS Errors**

   - Verify CORS is configured in Program.cs
   - Check allowed origins match your frontend URL

3. **Model Validation Errors**
   - Ensure all required fields are provided
   - Check data annotations on models

## 📈 Performance Considerations

- **Async Operations**: All service methods use async/await patterns
- **Memory Management**: Mock data is stored in memory (consider database for production)
- **Response Time**: Built-in delays simulate realistic API response times
- **Scalability**: Ready for horizontal scaling in cloud environments

## 🔮 Future Enhancements

- **Database Integration**: Full Entity Framework implementation
- **Caching**: Redis caching for improved performance
- **Real-time Updates**: SignalR for live event updates
- **Image Upload**: Support for event images and media
- **Event Registration**: Attendee registration and management
- **Payment Integration**: Direct integration with payment services

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👥 Support

For support and questions:

- Check the API documentation at `/swagger`
- Create an issue in the GitHub repository
- Contact the development team

---

Built with ❤️ using .NET 9 and ready for Azure deployment
