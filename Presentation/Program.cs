using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Presentation.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ITicketInfoRepository, TicketInfoRepository>();
builder.Services.AddScoped<ITicketPurchaseRepository, TicketPurchaseRepository>();

builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddDbContext<DataContext>(x => x.UseLazyLoadingProxies()
    .UseSqlite(builder.Configuration.GetConnectionString("EventsDatabaseConnection")));

// builder.Services.AddDbContext<DataContext>(x => x.UseLazyLoadingProxies()
//     .UseSqlServer(builder.Configuration.GetConnectionString("EventsDatabaseConnection")));

var app = builder.Build();
app.MapOpenApi();
app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();