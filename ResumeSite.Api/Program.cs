using ResumeSite.Api.Data;
using ResumeSite.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddResumeSiteDb();

var app = builder.Build();

app.MapSkillsEndpoints();

app.MigrateDb();

app.Run();
