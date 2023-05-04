using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Models;
using Ramsay.Common.Exceptions;
using Ramsay.Common.Resources;
using Ramsay.Domain.DTOs.Student;
using Ramsay.Domain.Services.Interfaces;

namespace Ramsay.Domain.Services
{
    public class StudenServices : IStudenServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Builder
        public StudenServices(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods

        public List<StudentDto> GetAll()
        {
            IEnumerable<StudentEntity> students = _unitOfWork.StudentRepository.GetAll();
            List<StudentDto> result = students.Select(x => new StudentDto()
            {
                Id = x.Id,
                Age = x.Age,
                Career = x.Career,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Username = x.Username
            }).ToList();

            return result;
        }

        private StudentEntity GetById(int id) => _unitOfWork.StudentRepository.FirstOrDefault(x => x.Id == id);
        private StudentEntity GetByUserName(string userName) => _unitOfWork.StudentRepository.FirstOrDefault(x => x.Username == userName);

        public async Task<bool> Insert(AddStudentDto addStudent)
        {
            StudentEntity student = GetByUserName(addStudent.Username);
            if (student!=null)
                throw new BusinessException("El nombre de usuario ya existe, por favor asignar otro");

            StudentEntity entity = new StudentEntity()
            {
                Age = addStudent.Age,
                Career = addStudent.Career,
                FirstName = addStudent.FirstName,
                LastName = addStudent.LastName,
                Username = addStudent.Username
            };
            _unitOfWork.StudentRepository.Insert(entity);

            return await _unitOfWork.Save() > 0;
        }

        public async Task<bool> Update(StudentDto student)
        {
            StudentEntity entity = GetById(student.Id);
            if (entity == null)
                throw new BusinessException(GeneralMessages.ItemNoFound);

            entity.Age = student.Age;
            entity.Career = student.Career;
            entity.FirstName = student.FirstName;
            entity.LastName = student.LastName;
            entity.Username = student.Username;
            _unitOfWork.StudentRepository.Update(entity);

            return await _unitOfWork.Save() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            StudentEntity entity = GetById(id);
            if (entity == null)
                throw new BusinessException(GeneralMessages.ItemNoFound);

            _unitOfWork.StudentRepository.Delete(entity);

            return await _unitOfWork.Save() > 0;
        }


        
        #endregion
    }
}
