using System.ComponentModel.DataAnnotations;

namespace crud_v2.ViewModels
{
    public class EditEmployeeVM
    {
        // Hidden field - to know which employee to update
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number")]
        public double Salary { get; set; }

        [Display(Name = "Upload New File")]
        public IFormFile? File { get; set; }

        // To store the existing filename (if user doesn't upload a new one)
        public string? ExistingFile { get; set; }
    }
}