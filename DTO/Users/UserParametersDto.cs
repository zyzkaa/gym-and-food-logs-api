using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO;

    public class UserParametersDto
    {
        [Required(ErrorMessage = "Weight is required")]
        [Range(30, 300, ErrorMessage = "Weight must be between 30 and 300 kg")]
        public int Weight { get; set; }

        [Required(ErrorMessage = "Height is required")]
        [Range(100, 250, ErrorMessage = "Height must be between 100 and 250 cm")]
        public int Height { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(1, 120, ErrorMessage = "Age must be between 1 and 120 years")]
        public int Age { get; set; }
    }