using StudentsAPI.Models;
using System.Collections.Generic;

namespace StudentsAPI.Services
{
    public interface IStudentsService
    {
        IEnumerable<Student> Get(Filter filter = null);
        void Add(Student student);
        void Update(Student student);
        void Delete(long studentId);
    }
}
