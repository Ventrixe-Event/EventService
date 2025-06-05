/*
 * Event Service - Entry Point (Program.cs)
 * ==========================================
 *
 * This is the main entry point for the Event Service microservice.
 * The Event Service is part of the Ventixe event management platform,
 * following clean architecture principles with separate layers for
 * Presentation, Application, and Persistence.
 *
 * Key Features:
 * - RESTful API for event management operations
 * - Mock data implementation for development/demonstration
 * - JSON serialization with camelCase naming for frontend compatibility
 * - Swagger/OpenAPI documentation for API exploration
 * - CORS configuration for cross-origin requests
 *
 * Architecture:
 * - Presentation Layer: Controllers and API configuration (this project)
 * - Application Layer: Business logic and service interfaces
 * - Persistence Layer: Data entities and repositories (currently mocked)
 *
 * Development Notes:
 * - AI assistance provided by Claude 4 (Anthropic) for architecture design,
 *   code structure, commenting, and best practices implementation
 * - Uses dependency injection for service registration and management
 * - Configured for both development and production environments
 *
 * Author: Kim Hammerstad (with AI assistance from Claude 4)
 * Created: 2024
 */

using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Interfaces;
using Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure JSON serialization options for API responses
// This ensures consistent JSON formatting for frontend consumption
builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Use camelCase naming policy for JavaScript/TypeScript frontend compatibility
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

        // Exclude null values from JSON responses to reduce payload size
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

        // Format JSON with indentation for better readability during development
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Configure API documentation and testing tools
// OpenAPI/Swagger provides interactive API documentation
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Register application services using dependency injection
// This follows the dependency inversion principle for loose coupling
builder.Services.AddScoped<IEventService, EventService>();

var app = builder.Build();

// Configure Cross-Origin Resource Sharing (CORS) for frontend access
// Allows the React frontend to make API calls from different origins
app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());

// Configure the HTTP request pipeline for API documentation
app.MapOpenApi();

// Enable Swagger UI for interactive API documentation
// Accessible at the root path (/) for easy development and testing
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Event Service API");
    c.RoutePrefix = string.Empty; // Serve Swagger UI at root path
});

// Enable HTTPS redirection for security
app.UseHttpsRedirection();

// Enable authorization middleware (for future authentication features)
app.UseAuthorization();

// Map controller routes for API endpoints
app.MapControllers();

// Start the application
app.Run();
