using System.ComponentModel.DataAnnotations;
using ResumeSite.Api.Dtos.Validation;
using ResumeSite.Api.Models;

namespace ResumeSite.Api.Dtos;

public record CreateExperienceDto(
  [Required][StringLength(200)] string CompanyName,
  [Required][StringLength(100)] string Position,
  [Required] DateOnly StartDate,
  DateOnly EndDate,
  bool IsCurrent,
  [StringLength(350)] string Description,
  [MaxElementLength(200)] List<string> Highlights,
  [StringLength(200)] string Location,
  EmploymentType? EmploymentType,
  [StringLength(200)] string CompanyUrl,
  List<int> SkillIds
);