using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SelectListUtilities;

namespace ERP.Models
{
    public static class DAL
    {
        public static SelectList DepartmentsToSelectList(this ERPDBEntities DB, string defaultText = "")
        {
            return SelectListItems<Department>.Convert(DB.Departments.OrderBy(d => d.Name), defaultText);
        }
        public static SelectList ClassificationsToSelectList(this ERPDBEntities DB, string defaultText = "")
        {
            return SelectListItems<Classification>.Convert(DB.Classifications.OrderBy(d => d.Name), defaultText);
        }
        public static Employee AddEmployee(this ERPDBEntities DB, Employee employee)
        {
            DB.Employees.Add(employee);
            DB.SaveChanges();
            return employee;
        }
        public static bool UpdateEmployee(this ERPDBEntities DB, Employee employee)
        {
            DB.Entry(employee).State = EntityState.Modified;
            DB.SaveChanges();
            return true;
        }
public static bool EmailAvailable(this ERPDBEntities DB, string email, int excludedId = 0)
{
    var employee = DB.Employees.Where(e => (e.Email == email) && (e.Id != excludedId)).FirstOrDefault();
    return employee == null;
}

        // Todo add extensions : EditEmployee, DeleteEmployee, AddDepartment, EditDepartment, AddClassification, EditClassification
    }
}