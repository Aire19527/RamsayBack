using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Models;
using Moq;
using Ramsay.Domain.DTOs.Student;
using Ramsay.Domain.Services;
using Ramsay.Domain.Services.Interfaces;
using Xunit;

namespace Ramsay.Test.Services
{
    public class StudentServicesTest
    {
        #region Attributes
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IStudenServices _studenServices;
        #endregion

        #region Builder
        public StudentServicesTest()
        {
            //arranges
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _studenServices = new StudenServices(_unitOfWorkMock.Object);
        }
        #endregion

        #region Test

        [Fact]
        public void GetPermissionsTest()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.StudentRepository.GetAll())
                                .Returns(new List<StudentEntity>()
                                {
                                    new StudentEntity()
                                    {
                                        Id = 1,
                                        Age = 25,
                                        Career="MockCareer",
                                        FirstName="MockFirstName",
                                        LastName="MockLastName",
                                        Username="MockUserName"
                                    },
                                    new StudentEntity()
                                    {
                                        Id = 1,
                                        Age = 25,
                                        Career="MockCareer",
                                        FirstName="MockFirstName",
                                        LastName="MockLastName",
                                        Username="MockUserName2"
                                    }
                                }.ToList());

            // Act
            var result = _studenServices.GetAll();

            // Assert
            var model = Assert.IsAssignableFrom<IEnumerable<StudentDto>>(result);
            Assert.True(model.Any());
            Assert.Equal(2, model.Count());

        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Insert_Test(bool test)
        {
            //arranges
            _unitOfWorkMock.Setup(x => x.StudentRepository.Insert(It.IsAny<StudentEntity>()));
            if (test)
                _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);
            else
                _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(0);


            // Act
            var result = await _studenServices.Insert(new AddStudentDto()
            {
                Age = 20,
                Career = "MockCareer",
                FirstName = "MockFirstName",
                LastName = "MockLastName",
                Username = "MockUserName3"
            });

            // Assert
            if (test)
                Assert.True(result);
            else
                Assert.False(result);

        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Update_Test(bool test)
        {
            //arranges
            _unitOfWorkMock.Setup(x => x.StudentRepository.FirstOrDefault(x => x.Id == 1)).Returns(new StudentEntity()
            {
                Id = 1,
                Age = 20,
                Career = "MockCareer",
                FirstName = "MockFirstName",
                LastName = "MockLastName",
                Username = "MockUserName"
            });
            _unitOfWorkMock.Setup(x => x.StudentRepository.Update(It.IsAny<StudentEntity>()));
            if (test)
                _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);
            else
                _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(0);


            // Act
            var result = await _studenServices.Update(new StudentDto()
            {
                Id = 1,
                Age = 26,
                Career = "MockCareer_test",
                FirstName = "MockFirstName_test",
                LastName = "MockLastName_test",
                Username = "MockUserName_test"
            });

            // Assert
            if (test)
                Assert.True(result);
            else
                Assert.False(result);
        }


        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Delete_Test(bool test)
        {
            _unitOfWorkMock.Setup(x => x.StudentRepository.FirstOrDefault(x => x.Id == 1)).Returns(new StudentEntity()
            {
                Id = 1,
                Age = 20,
                Career = "MockCareer",
                FirstName = "MockFirstName",
                LastName = "MockLastName",
                Username = "MockUserName"
            });

            _unitOfWorkMock.Setup(x => x.StudentRepository.Delete(It.IsAny<StudentEntity>()));
            if (test)
                _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);
            else
                _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(0);

            // Act
            var result = await _studenServices.Delete(id: 1);

            // Assert
            if (test)
                Assert.True(result);
            else
                Assert.False(result);
        }


        #endregion
    }
}
