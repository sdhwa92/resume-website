using System.ComponentModel.DataAnnotations;

namespace ResumeSite.Api.Dtos;

public record CreateSkillDto(
  [Required][StringLength(100)] string Name,
  [Range(1, int.MaxValue)] int? ExperienceId
);