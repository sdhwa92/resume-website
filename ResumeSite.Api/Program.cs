using ResumeSite.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.AddResumeSiteDb();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MigrateDb();

app.Run();
