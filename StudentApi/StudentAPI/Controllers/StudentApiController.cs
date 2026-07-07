using Microsoft.AspNetCore.Mvc;
using StudentAPIBussinessLayer;
using StudentDataAccess;

namespace StudentAPIWithDB.Controllers
{
    [ApiController]
    [Route("api/Students")]
    public class StudentApiController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<StudentDTO>> GetAllStudents()
        {
            List<StudentDTO> StudentList = Student.GetAllStudents();
            if (StudentList.Count == 0)
            {
                return NotFound("No Students Found!");
            }
            return Ok(StudentList);
        }
        [HttpGet("Passed", Name = "GetPassedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<StudentDTO>> GetPassedStudents()
        {
            List<StudentDTO> GetPassedStudents = Student.GetPassedStudents();
            if (GetPassedStudents.Count == 0)
            {
                return NotFound("No Students Found");
            }
            return Ok(GetPassedStudents);
        }
        [HttpGet("AverageGrade", Name = "GetAverageGrade")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<double> GetAverageGrade()
        {
            double AverageGrade = Student.GetAverageGrade();
            return Ok(AverageGrade);
        }
        [HttpGet("{id}", Name = "GetStudentById")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDTO> GetStudentByID(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }
            Student student = Student.Find(id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            StudentDTO SDTO = student.SDTO;
            return Ok(SDTO);
        }
        [HttpPost(Name = "AddStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentDTO> AddStudent(StudentDTO newStudentDTO)
        {
            if (newStudentDTO == null || string.IsNullOrEmpty(newStudentDTO.Name) || newStudentDTO.Age < 0 || newStudentDTO.Grade < 0)
            {
                return BadRequest("Invalid student data.");
            }
            Student student = new Student(new StudentDTO(newStudentDTO.Id, newStudentDTO.Name, newStudentDTO.Age, newStudentDTO.Grade), Student.enMode.AddNew);
            student.Save();

            newStudentDTO.Id = student.ID;
            return CreatedAtRoute("GetStudentById", new { id = newStudentDTO.Id }, newStudentDTO);
        }
        [HttpPut("{id}", Name = "UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDTO> UpdateStudent(int id, StudentDTO updatedStudent)
        {
            if (id < 1 || updatedStudent == null || string.IsNullOrEmpty(updatedStudent.Name) || updatedStudent.Age < 0 || updatedStudent.Grade < 0)
            {
                return BadRequest("Invalid student data.");
            }
            Student student = Student.Find(id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            student.Name = updatedStudent.Name;
            student.Age = updatedStudent.Age;
            student.Grade= updatedStudent.Grade;
            student.Save(); 
            return Ok(student);
        }
        [HttpDelete("{id}", Name = "DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteStudent(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }
            if (Student.DeleteStudent(id))

                return Ok($"Student with ID {id} has been deleted.");
            else
                return NotFound($"Student with ID {id} not found. no rows deleted!");
        }

    }

}
