using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ERP.Models
{
    [MetadataType(typeof(EmployeeView))]
    public partial class Employee
    {
        public Employee()
        {
            BirthDate = DateTime.Now;
        }
    }
    public class EmployeeView
    {
        [Display(Name = "Prénom")]
        [Required(ErrorMessage = "Ce champ est requis"), MaxLength(50, ErrorMessage = "Excède 50 caractères")]
        public string FirstName { get; set; }
        [Display(Name = "Nom")]
        [Required(ErrorMessage = "Ce champ est requis"), MaxLength(50, ErrorMessage = "Excède 50 caractères")]
        public string LastName { get; set; }
        [Display(Name = "Date de naissance")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public System.DateTime BirthDate { get; set; }
        [Display(Name = "Téléphone")]
        [RegularExpression(@"^\(\d\d\d\) \d\d\d-\d\d\d\d$", ErrorMessage = "Format incorrect, utilisez (999) 999-9999")]
        public string Phone { get; set; }
        [Display(Name = "Poste"), MaxLength(10, ErrorMessage = "Excède 10 chiffress")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Doit comporter uniquement des chiffres")]
        public string PhoneExt { get; set; }
        [Display(Name = "Courriel")]
        [DataType(DataType.EmailAddress)]
        /* [Remote(Action, Controller, HttpMethod = "POST", [AdditionalFields = "..."], ErrorMessage = "...")]*/
        /* Controller/Action must return Json(bool expression) */
        [Remote(
            "EmailAvailable", /* Action */
            "Employees", /* Controller */
            HttpMethod = "POST", /* Http verb */
            AdditionalFields = "Id", /* champ supplémentaire à récupérer du formulaire */
            ErrorMessage = "Cette adresse de courriel est attribuée à un autre employé."
         )]
        public string Email { get; set; }
        [Display(Name = "Département")]
        [Required(ErrorMessage = "Veuillez faire une sélection")]
        public int DepartmentId { get; set; }
        [Display(Name = "Titre de l'emploi")]
        [Required(ErrorMessage = "Ce champ est requis"), MaxLength(50, ErrorMessage = "Excède 50 caractères")]
        public string JobTitle { get; set; }
        [Display(Name = "Classification")]
        [Required(ErrorMessage = "Veuillez faire une sélection")]
        public int ClassificationId { get; set; }
    }
}