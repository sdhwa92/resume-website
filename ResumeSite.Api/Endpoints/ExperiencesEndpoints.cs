using Microsoft.EntityFrameworkCore;
using ResumeSite.Api.Data;
using ResumeSite.Api.Dtos;
using ResumeSite.Api.Models;

namespace ResumeSite.Api.Endpoints;

public static class ExperiencesEndpoints
{
  const string GetExperienceEndpointName = "GetExperience";


  public static void MapExperiencesEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("/experiences");

    // GET /experiences
    group.MapGet("/", async (ResumeSiteContext dbContext) =>
      await dbContext.Experiences
        .Select(experience => new ExperienceDto(
          experience.Id,
          experience.CompanyName,
          experience.Position,
          experience.StartDate,
          experience.EndDate,
          experience.IsCurrent,
          experience.Description,
          experience.Highlights,
          experience.Location,
          experience.EmploymentType,
          experience.CompanyUrl,
          experience.Skills
            .Select(skill => new SkillDto(skill.Id, skill.Name))
            .ToList()
        ))
        .AsNoTracking()
        .ToListAsync()
    );

    // GET /experiences/{id}
    group.MapGet("/{id}", async (int id, ResumeSiteContext dbContext) =>
    {
      var experience = await dbContext.Experiences.FindAsync(id);

      return experience is null ? Results.NotFound($"Experience {id} is not found.") : Results.Ok(
        new ExperienceDto(
          experience.Id,
          experience.CompanyName,
          experience.Position,
          experience.StartDate,
          experience.EndDate,
          experience.IsCurrent,
          experience.Description,
          experience.Highlights,
          experience.Location,
          experience.EmploymentType,
          experience.CompanyUrl,
          experience.Skills.Select(skill => new SkillDto(skill.Id, skill.Name)).ToList()
        )
      );
    })
    .WithName(GetExperienceEndpointName);

    // POST /experiences
    group.MapPost("/", async (CreateExperienceDto newExperience, ResumeSiteContext dbContext) =>
    {
      List<Skill> skills = [];

      if (newExperience.SkillIds is { Count: > 0 })
      {
        skills = await dbContext.Skills
          .Where(skill => newExperience.SkillIds.Contains(skill.Id))
          .ToListAsync();

        if (skills.Count != newExperience.SkillIds.Distinct().Count())
        {
          return Results.BadRequest("One or more SkillIds do not exist.");
        }
      }

      Experience experience = new()
      {
        CompanyName = newExperience.CompanyName,
        Position = newExperience.Position,
        StartDate = newExperience.StartDate,
        EndDate = newExperience.EndDate,
        IsCurrent = newExperience.IsCurrent,
        Description = newExperience.Description,
        Highlights = newExperience.Highlights,
        Location = newExperience.Location,
        EmploymentType = newExperience.EmploymentType,
        CompanyUrl = newExperience.CompanyUrl,
        Skills = skills,
      };

      dbContext.Experiences.Add(experience);
      await dbContext.SaveChangesAsync();

      var experienceDto = new ExperienceDto(
        experience.Id,
        experience.CompanyName,
        experience.Position,
        experience.StartDate,
        experience.EndDate,
        experience.IsCurrent,
        experience.Description,
        experience.Highlights,
        experience.Location,
        experience.EmploymentType,
        experience.CompanyUrl,
        experience.Skills.Select(skill => new SkillDto(skill.Id, skill.Name)).ToList()
      );

      return Results.CreatedAtRoute(GetExperienceEndpointName, new { id = experience.Id }, experienceDto);
    });

    // PUT /experiences/{id}
    group.MapPut("/{id}", async (int id, CreateExperienceDto updatedExperience, ResumeSiteContext dbContext) =>
    {
      var experience = await dbContext.Experiences
        .Include(experience => experience.Skills)
        .FirstOrDefaultAsync(experience => experience.Id == id);

      if (experience is null)
      {
        return Results.NotFound($"Experience {id} is not found.");
      }

      List<Skill> skills = [];

      if (updatedExperience.SkillIds is { Count: > 0 })
      {
        skills = await dbContext.Skills
          .Where(skill => updatedExperience.SkillIds.Contains(skill.Id))
          .ToListAsync();

        if (skills.Count != updatedExperience.SkillIds.Distinct().Count())
        {
          return Results.BadRequest("One or more SkillIds do not exist.");
        }
      }

      experience.CompanyName = updatedExperience.CompanyName;
      experience.Position = updatedExperience.Position;
      experience.StartDate = updatedExperience.StartDate;
      experience.EndDate = updatedExperience.EndDate;
      experience.IsCurrent = updatedExperience.IsCurrent;
      experience.Description = updatedExperience.Description;
      experience.Highlights = updatedExperience.Highlights;
      experience.Location = updatedExperience.Location;
      experience.EmploymentType = updatedExperience.EmploymentType;
      experience.CompanyUrl = updatedExperience.CompanyUrl;
      experience.Skills = skills;

      await dbContext.SaveChangesAsync();

      return Results.NoContent();
    });

    // DELETE /experiences/{id}
    group.MapDelete("/{id}", async (int id, ResumeSiteContext dbContext) =>
    {
      var rowsDeleted = await dbContext.Experiences
        .Where(experience => experience.Id == id)
        .ExecuteDeleteAsync();

      return rowsDeleted == 0 ? Results.NotFound($"Experience {id} is not found.") : Results.NoContent();
    });
  }
}