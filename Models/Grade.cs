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

    public partial class Grade
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Grade()
        {
            this.Drinks = new HashSet<Drink>();
            this.Foods = new HashSet<Food>();
        }

        [Required]
        [StringLength(1, MinimumLength = 1)]
        public string Grade1 { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string SugarContent { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FatContent { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string LabelRequirments { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Admin { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Drink> Drinks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Food> Foods { get; set; }
    }
}
