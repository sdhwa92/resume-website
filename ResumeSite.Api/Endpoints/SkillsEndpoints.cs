using Microsoft.EntityFrameworkCore;
using ResumeSite.Api.Data;
using ResumeSite.Api.Dtos;
using ResumeSite.Api.Models;

namespace ResumeSite.Api.Endpoints;

public static class SkillsEndpoints
{
  const string GetSkillEndpointName = "GetSkill";

  public static void MapSkillsEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("/skills");

    // GET /skills
    group.MapGet("/", async (ResumeSiteContext dbContext) =>
      await dbContext.Skills
        .Select(skill => new SkillDto(skill.Id, skill.Name))
        .AsNoTracking()
        .ToListAsync());

    // GET /skills/{id}
    group.MapGet("/{id}", async (int id, ResumeSiteContext dbContext) =>
    {
      var skill = await dbContext.Skills.FindAsync(id);

      return skill is null ? Results.NotFound($"Skill {id} not found.") : Results.Ok(
        new SkillDto(
          skill.Id,
          skill.Name
        )
      );
    })
    .WithName(GetSkillEndpointName);

    // POST /skills
    group.MapPost("/", async (CreateSkillDto newSkill, ResumeSiteContext dbContext) =>
    {
      Skill skill = new()
      {
        Name = newSkill.Name,
      };

      if (newSkill.ExperienceId is not null)
      {
        var experience = await dbContext.Experiences.FindAsync(newSkill.ExperienceId);
        if (experience is null)
        {
          return Results.NotFound($"Experience {newSkill.ExperienceId} not found.");
        }

        skill.Experiences.Add(experience);
      }

      dbContext.Skills.Add(skill);
      await dbContext.SaveChangesAsync();

      return Results.CreatedAtRoute(GetSkillEndpointName, new SkillDto(skill.Id, skill.Name));
    });

    // DELETE /skills/{id}
    group.MapDelete("/{id}", async (int id, ResumeSiteContext dbContext) =>
    {
      await dbContext.Skills
        .Where(skill => skill.Id == id)
        .ExecuteDeleteAsync();

      return Results.NoContent();
    });
  }
}