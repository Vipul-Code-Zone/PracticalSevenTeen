using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracSevenTeen.Db.Interfaces;
using PracSevenTeen.Models.Models;

namespace PracticalSevenTeen.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        // GET: StudentController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Student> students = await _studentRepository.GetStudentsAsync();
            return View(students);
        }

        // GET: StudentController/Details/id
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Student? student = await _studentRepository.GetStudentAsync(id);
            if(student == null)
            {
                return View("Index");
            }
            return View(student);
        }

        // GET: StudentController/Create
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public Task<IActionResult> Create()
        {
            return Task.FromResult<IActionResult>(View());
        }

        // POST: StudentController/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsEmpAdded = await _studentRepository.AddStudentAsync(student);
                    ViewBag.EmployeeAdded = IsEmpAdded;
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View("Error");
            }
            return View(student);
        }

        // GET: StudentController/Edit/id
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Student? student = await _studentRepository.GetStudentAsync(id);
            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // POST: StudentController/Edit/id
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = await _studentRepository.UpdateStudentAsync(student);
                    ViewBag.EmployeeUpdated = IsUpdated;
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View("Error");
            }

            return View(student);
        }

        // GET: StudentController/Delete/id
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Student? student = await _studentRepository.GetStudentAsync(id);
            if (student == null)
            {
                return View("Index");
            }
            return View(student);
        }

        // POST: StudentController/Delete/id
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Student student)
        {
            try
            {
                bool IsDeleted = await _studentRepository.DeleteStudentAsync(id);
                ViewBag.EmployeeDeleted = IsDeleted;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
