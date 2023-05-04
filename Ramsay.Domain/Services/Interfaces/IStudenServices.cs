using Ramsay.Domain.DTOs.Student;

namespace Ramsay.Domain.Services.Interfaces
{
    public interface IStudenServices
    {
        List<StudentDto> GetAll();
        Task<bool> Insert(AddStudentDto addStudent);
        Task<bool> Update(StudentDto student);
        Task<bool> Delete(int id);
    }
}
