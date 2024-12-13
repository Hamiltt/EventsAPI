using Application;
using Application.Mapping;
using Application.Providers;
using Application.Services;
using Application.UseCases.Auth;
using Application.UseCases.Events;
using Application.UseCases.Participants;
using Application.Validators;
using Domain.UnitOfWork;
using EventsAPI.Middleware;
using FluentValidation;
using Infrastructure.Context;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EventsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();

builder.Services.AddAutoMapper(typeof(EventProfile), typeof(ParticipantProfile));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<CreateEventUseCase>();
builder.Services.AddScoped<GetEventByIdUseCase>();
builder.Services.AddScoped<UpdateEventUseCase>();
builder.Services.AddScoped<DeleteEventUseCase>();

builder.Services.AddScoped<GetParticipantsByEventUseCase>();
builder.Services.AddScoped<GetParticipantByIdUseCase>();
builder.Services.AddScoped<RegisterParticipantUseCase>();
builder.Services.AddScoped<UnregisterParticipantUseCase>();

builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<TokenValidationService>();
builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddScoped<RefreshTokenUseCase>();

builder.Services.AddScoped<IUserProvider, UserProvider>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateEventRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RefreshTokenRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterParticipantRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateEventRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<EventFilterDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TokenResponseValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ParticipantDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<EventDTOValidator>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:Secret"]);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<JwtMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();