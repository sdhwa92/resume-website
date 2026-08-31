using ResumeSite.Api.Models;

namespace ResumeSite.Api.Dtos;

public record ExperienceDto(
  int Id,
  string CompanyName,
  string Position,
  DateOnly StartDate,
  DateOnly? EndDate,
  bool IsCurrent,
  string? Description,
  List<string> Highlights,
  string? Location,
  EmploymentType? EmploymentType,
  string? CompanyUrl,
  List<SkillDto> Skills
);