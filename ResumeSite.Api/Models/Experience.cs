namespace ResumeSite.Api.Models;

public class Experience
{
  public int Id { get; set; }

  public required string CompanyName { get; set; }

  public required string Position { get; set; }

  public required DateOnly StartDate { get; set; }

  public DateOnly? EndDate { get; set; }

  public bool IsCurrent { get; set; }

  public string? Description { get; set; }

  public List<string> Highlights { get; set; } = [];

  public string? Location { get; set; }

  public EmploymentType? EmploymentType { get; set; }

  public string? CompanyUrl { get; set; }

  public List<Skill> Skills { get; set; } = [];
}
