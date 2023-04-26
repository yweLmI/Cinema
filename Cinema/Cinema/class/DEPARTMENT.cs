namespace CinemaAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DEPARTMENT")]
    public partial class DEPARTMENT
    {
        [StringLength(10)]
        public string DepartmentID { get; set; }

        [StringLength(100)]
        public string DepartmentName { get; set; }
    }
}
