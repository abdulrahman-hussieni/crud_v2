using crud_v1.DAL.DB;
using crud_v2.DAL.Entities;
using crud_v2.Helper;
using crud_v2.ModelVM.EmployeeVM;
using crud_v2.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace crud_v2.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext context = new ApplicationDbContext();

        // [1] Show all
        public IActionResult showAll()
        {
            var result = context.employees
                .Select(e => new ShowAllEmployeeVM
                {
                    Id = e.Id,
                    Name = e.Name,
                    Salary = e.Salary,
                    File = e.File,
                    CreatedOn = e.CreatedOn
                })
                .ToList();
            return View(result);
        }

        public IActionResult GetByID(int? searchId)
        {
            if (searchId.HasValue)
            {
                var emp = context.employees.FirstOrDefault(x => x.Id == searchId.Value);
                if (emp == null)
                {
                    // If no employee found, show empty list in the table
                    return View("ShowUsers", new List<Employee>());
                }
                // Show the single employee in the table
                return View("ShowUsers", new List<Employee> { emp });
            }
            // If search is empty, go back to the full list
            return RedirectToAction("ShowUsers");
        }

        // [2] Create - GET
        public IActionResult Create()
        {
            return View();
        }

        // [2] Create - POST
        public IActionResult SaveData(CreateEmployeeVM newemployee)
        {
            // Upload the image file and get the saved filename
            string image = Upload.UploadFile("ImageProfile", newemployee.File);

            // Validate required fields
            if (newemployee.Name == null || newemployee.Salary == 0)
            {
                return View(viewName: "Create");
            }

            // Custom Mapping from ViewModel to Entity
            Employee emp = new Employee()
            {
                Name = newemployee.Name,
                Salary = newemployee.Salary,
                File = image,  // Save the filename to database
                CreatedOn = DateTime.Now
            };

            // Add to database
            var context = new ApplicationDbContext();
            context.employees.Add(emp);

            // Save changes
            context.SaveChanges();

            return RedirectToAction("showAll");
        }

        // [3] Edit - GET
        public IActionResult Edit(int id)
        {
            // Find employee by ID
            var employee = context.employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            // Map Employee to EditEmployeeVM
            var model = new EditEmployeeVM
            {
                Id = employee.Id,
                Name = employee.Name,
                Salary = employee.Salary,
                ExistingFile = employee.File
            };

            return View(model);
        }

        // [3] Edit - POST
        public IActionResult UpdateData(EditEmployeeVM editEmployee)
        {
            // Validate required fields
            if (editEmployee.Name == null || editEmployee.Salary == 0)
            {
                return View(viewName: "Edit", editEmployee);
            }

            // Find the existing employee
            var employee = context.employees.Find(editEmployee.Id);

            if (employee == null)
            {
                return NotFound();
            }

            // Update employee data
            employee.Name = editEmployee.Name;
            employee.Salary = editEmployee.Salary;
            employee.UpdatedOn = DateTime.Now;

            // Handle file upload (if user uploaded a new file)
            if (editEmployee.File != null && editEmployee.File.Length > 0)
            {
                // Delete old file if exists
                if (!string.IsNullOrEmpty(employee.File))
                {
                    Upload.RemoveFile("ImageProfile", employee.File);
                }

                // Upload new file
                employee.File = Upload.UploadFile("ImageProfile", editEmployee.File);
            }
            // If no new file uploaded, keep the existing file (do nothing)

            // Save changes
            context.SaveChanges();

            return RedirectToAction("showAll");
        }
        // [4] Delete - GET (Show confirmation page)
        public IActionResult Delete(int id)
        {
            // Find employee by ID
            var employee = context.employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            // Map Employee to DeleteEmployeeVM
            var model = new DeleteEmployeeVM
            {
                Id = employee.Id,
                Name = employee.Name,
                Salary = employee.Salary,
                File = employee.File,
                CreatedOn = employee.CreatedOn
            };

            return View(model);
        }

        // [4] Delete - POST (Actually delete the employee)
        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            // Find the employee
            var employee = context.employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            // Delete the file if exists
            if (!string.IsNullOrEmpty(employee.File))
            {
                Upload.RemoveFile("ImageProfile", employee.File);
            }

            // Remove employee from database
            context.employees.Remove(employee);

            // Save changes
            context.SaveChanges();

            return RedirectToAction("showAll");
        }
    }
}