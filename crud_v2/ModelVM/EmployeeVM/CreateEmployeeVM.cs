using System.ComponentModel.DataAnnotations;

namespace crud_v2.ViewModels
{
    public class CreateEmployeeVM
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number")]
        public double Salary { get; set; }

        [Display(Name = "Upload File")]
        public IFormFile? File { get; set; }
    }
}