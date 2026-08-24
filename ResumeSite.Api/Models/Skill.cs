namespace ResumeSite.Api.Models;

public class Skill
{
  public int Id { get; set; }

  public required string Name { get; set; }

  public List<Experience> Experiences { get; set; } = [];
}
