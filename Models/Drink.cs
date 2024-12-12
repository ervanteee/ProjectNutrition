namespace ProjectNutrition.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Drink
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Company { get; set; }

        [Required]
        [StringLength(1, MinimumLength = 1)]
        public string Grade { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Admin { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Img_Path { get; set; }

        public virtual Admin Admin1 { get; set; }
        public virtual Grade Grade1 { get; set; }
    }
}
