using Microsoft.EntityFrameworkCore;
using PracSevenTeen.Db.DatabaseContext;
using PracSevenTeen.Db.Interfaces;
using PracSevenTeen.Models.Models;

namespace PracSevenTeen.Db.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDbContext _studentDbContext;

        public StudentRepository(StudentDbContext studentDbContext)
        {
            _studentDbContext = studentDbContext;
        }



        public async Task<List<Student>> GetStudentsAsync()
        {
            List<Student> students = await _studentDbContext.Students.ToListAsync();
            return students;
        }

        public async Task<bool> AddStudentAsync(Student student)
        {
            Student stud = new Student()
            {
                Name = student.Name,
                Standard = student.Standard,
                Age = student.Age,
            };
            _studentDbContext.Add(stud);
            await _studentDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Student?> GetStudentAsync(int id)
        {
            Student? student = await _studentDbContext.Students.FindAsync(id);
            if(student == null)
            {
                return null;
            }
            return student;
        }

        public async Task<bool> UpdateStudentAsync(Student student)
        {
            _studentDbContext.Entry(student).State = EntityState.Modified;
            await _studentDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            Student? student = await _studentDbContext.Students.FindAsync(id);
            if (student != null)
            {
                _studentDbContext.Remove(student);
                await _studentDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
