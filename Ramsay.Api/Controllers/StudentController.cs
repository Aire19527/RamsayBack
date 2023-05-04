using Microsoft.AspNetCore.Mvc;
using Ramsay.Api.Handlers;
using Ramsay.Common.Resources;
using Ramsay.Domain.DTOs;
using Ramsay.Domain.DTOs.Student;
using Ramsay.Domain.Services.Interfaces;
using Serilog;

namespace Ramsay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(CustomExceptionFilterAttribute))]
    [ServiceFilter(typeof(CustomValidationFilterAttribute))]
    public class StudentController : ControllerBase
    {
        #region Attributes
        private readonly IStudenServices _studenServices;
        #endregion

        #region Builder
        public StudentController(IStudenServices studenServices)
        {
            this._studenServices = studenServices;
        }
        #endregion

        #region Services
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            List<StudentDto> list = _studenServices.GetAll();
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = list
            };

            string logMessage = $"[Service]: Api/Student/GetAll [LogMessage]: Consult";
            Log.Warning(logMessage);

            return Ok(response);
        }

        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> Insert(AddStudentDto addStudent)
        {
            IActionResult action;
            bool result = await _studenServices.Insert(addStudent);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = result,
                Result = result,
                Message = result ? GeneralMessages.ItemInserted : GeneralMessages.ItemNoInserted
            };

            if (result)
                action = Ok(response);
            else
                action = BadRequest(response);

            string logMessage = $"[Service]: Api/Student/Insert [LogMessage]: Insert";
            Log.Warning(logMessage);

            return action;
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(StudentDto student)
        {
            IActionResult action;
            bool result = await _studenServices.Update(student);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = result,
                Result = result,
                Message = result ? GeneralMessages.ItemUpdated : GeneralMessages.ItemNoUpdated
            };

            if (result)
                action = Ok(response);
            else
                action = BadRequest(response);

            string logMessage = $"[Service]: Api/Student/Update [LogMessage]: Update";
            Log.Warning(logMessage);

            return action;
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            IActionResult action;
            bool result = await _studenServices.Delete(id);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = result,
                Result = result,
                Message = result ? GeneralMessages.ItemDeleted : GeneralMessages.ItemNoDeleted
            };

            if (result)
                action = Ok(response);
            else
                action = BadRequest(response);

            string logMessage = $"[Service]: Api/Student/Delete [LogMessage]: Delete";
            Log.Warning(logMessage);

            return action;
        }


        #endregion
    }
}
