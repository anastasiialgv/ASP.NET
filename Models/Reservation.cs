using System.ComponentModel.DataAnnotations;

namespace APBD_Task_7.Models;

public class Reservation : IValidatableObject
{
    public int Id { get; set; }

    [Required]
    public int RoomId { get; set; }

    [Required]
    public string OrganizerName { get; set; }

    [Required]
    public string Topic { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public TimeSpan StartTime { get; set; }

    [Required]
    public TimeSpan EndTime { get; set; }

    [Required]
    public string Status { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndTime <= StartTime)
        {
            yield return new ValidationResult(
                "EndTime should be bigger than EndTime",
                new[] { nameof(EndTime) }
            );
        }
    }
}