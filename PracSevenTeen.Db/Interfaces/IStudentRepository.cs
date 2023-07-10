using PracSevenTeen.Models.Models;

namespace PracSevenTeen.Db.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsAsync();

        Task<bool> AddStudentAsync(Student student);
        Task<Student?> GetStudentAsync(int id);
        Task<bool> UpdateStudentAsync(Student student);
        Task<bool> DeleteStudentAsync(int id);
    }
}
