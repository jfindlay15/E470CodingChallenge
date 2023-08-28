using System.ComponentModel.DataAnnotations;

namespace CodingExercise.Dtos
{
    public class VehicleDto
    {
        /// <summary>
        /// Brand of Vehicle
        /// </summary>
        [Required(ErrorMessage = "Make is required")]
        public string Make { get; set; }

        /// <summary>
        /// Model of car
        /// </summary>
        [Required(ErrorMessage = "Model is required")]
        public string Model { get; set; }

        /// <summary>
        /// Year the car was manufactured
        /// </summary>
        [Required(ErrorMessage = "Year is required")]
        
        public string Year { get; set; }
    }
}
