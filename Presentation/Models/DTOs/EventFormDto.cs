using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.DTOs;


public class EventFormDto
{
    [Required(ErrorMessage = "Event name is required.")]
    [Display(Name = "Event Name", Prompt = "Enter the name of the event")]
    [StringLength(150)]
    public string EventName { get; set; } = null!;

    [Display(Name = "Description", Prompt = "Enter event description (optional)")]
    [StringLength(500)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Start date is required.")]
    [Display(Name = "Start Date", Prompt = "Select the event start date")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [Display(Name = "End Date", Prompt = "Select the event end date (optional)")]
    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }

    [Required(ErrorMessage = "Location is required.")]
    [Display(Name = "Location", Prompt = "Select the location")]
    public string LocationId { get; set; } = null!;

    [Required(ErrorMessage = "Ticket info is required.")]
    [Display(Name = "Ticket Info", Prompt = "Select ticket details")]
    public string TicketInfoId { get; set; } = null!;

    [Required(ErrorMessage = "Tag is required.")]
    [Display(Name = "Tag", Prompt = "Select a tag")]
    public int TagId { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    [Display(Name = "Status", Prompt = "Select a status")]
    public int StatusId { get; set; }

    [Display(Name = "Is Active", Prompt = "Is the event active?")]
    public bool IsActive { get; set; } = true;

    [Display(Name = "Is Public", Prompt = "Is the event public?")]
    public bool IsPublic { get; set; } = true;
}