using PracSevenTeen.Models.Models;

namespace PracSevenTeen.Db.Interfaces
{
    public interface IStudentRepository
    {
        /// <summary>
        /// It return List of all Students
        /// </summary>
        /// <returns></returns>
        Task<List<Student>> GetStudentsAsync();
        /// <summary>
        /// It add student in database
        /// </summary>
        /// <param name="student"></param>
        /// <returns>true if added successfully false if any error occurs </returns>
        Task<bool> AddStudentAsync(Student student);
        /// <summary>
        /// It returns Student details based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Student if exist or null</returns>
        Task<Student?> GetStudentAsync(int id);
        /// <summary>
        /// It update the student based on user inputs
        /// </summary>
        /// <param name="student"></param>
        /// <returns>true if updated else false</returns>
        Task<bool> UpdateStudentAsync(Student student);
        /// <summary>
        /// It delete the record in database based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if record deleted else false</returns>
        Task<bool> DeleteStudentAsync(int id);
    }
}
