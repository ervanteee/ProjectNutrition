//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectNutrition.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Food
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
