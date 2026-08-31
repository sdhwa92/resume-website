using System.Text.Json.Serialization;
using ResumeSite.Api.Data;
using ResumeSite.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
  options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddValidation();
builder.AddResumeSiteDb();

var app = builder.Build();

app.MapSkillsEndpoints();

app.MigrateDb();

app.Run();
