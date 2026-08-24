using Microsoft.EntityFrameworkCore;
using ResumeSite.Api.Models;

namespace ResumeSite.Api.Data;

public class ResumeSiteContext(DbContextOptions<ResumeSiteContext> options) : DbContext(options)
{
  public DbSet<Experience> Experiences => Set<Experience>();

  public DbSet<Skill> Skills => Set<Skill>();
}
