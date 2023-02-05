using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    [MetadataType(typeof(DepartmentView))]
    public partial class Department
    {
    }
   
    public class DepartmentView
    { 
        [Required(ErrorMessage = "Ce champ est requis"), MaxLength(50, ErrorMessage = "Excède 50 caractères")]
        [Display(Name= "Département")]
        public string Name { get; set; }
    }
}