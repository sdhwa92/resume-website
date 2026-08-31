using Microsoft.EntityFrameworkCore;
using ResumeSite.Api.Models;

namespace ResumeSite.Api.Data;

public static class DataExtensions
{
  public static void MigrateDb(this WebApplication app)
  {
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ResumeSiteContext>();
    dbContext.Database.Migrate();
  }

  public static void AddResumeSiteDb(this WebApplicationBuilder builder)
  {
    var connString = builder.Configuration.GetConnectionString("ResumeSite");

    // DbContext has a Scoped service lifetime because:
    // 1. It ensures that a new instance of DbContext is created per request
    // 2. DB connections are a limited and expensive resource
    // 3. DbContext is not thread-safe. Scoped avoids to concurrency issues
    // 4. Makes it easier to manage transactions and ensure data consistency
    // 5. Reusing a DbContext instance can lead to increased memory usage

    builder.Services.AddSqlite<ResumeSiteContext>(
      connString,
      optionsAction: options => options.UseSeeding((context, _) =>
      {
        if (!context.Set<Skill>().Any())
        {
          context.Set<Skill>().AddRange(
            new Skill { Name = "HTML" },
            new Skill { Name = "CSS" },
            new Skill { Name = "JavaScript" },
            new Skill { Name = "TypeScript" }
          );

          context.SaveChanges();
        }
      })
    );
  }
}
