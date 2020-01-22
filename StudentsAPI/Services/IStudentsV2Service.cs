using StudentsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAPI.Services
{
    public interface IStudentsV2Service
    {
        IEnumerable<StudentV2> Get(Filter filter = null);
        void Add(StudentV2 student);
        void Update(StudentV2 student);
        void Delete(long studentId);
    }
}
