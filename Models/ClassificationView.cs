using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    [MetadataType(typeof(ClassificationView))]
    public partial class Classification
    {
    }
    public class ClassificationView
    {
        [Required(ErrorMessage = "Ce champ est requis"), MaxLength(50, ErrorMessage = "Excède 50 caractères")]
        [Display(Name = "Classification")]
        public string Name { get; set; }
    }
}