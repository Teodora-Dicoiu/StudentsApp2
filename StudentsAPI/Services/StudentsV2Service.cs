using StudentsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAPI.Services
{
    public class StudentsV2Service : IStudentsV2Service
    {
        private readonly List<StudentV2> students;

        public StudentsV2Service()
        {
            students = new List<StudentV2>();
            Initialize();
        }

        public void Add(StudentV2 student)
        {
            lock (students)
            {
                students.Add(student);
            }
        }

        public void Delete(long studentId)
        {
            lock (students)
            {
                students.RemoveAll(s => s.Id == studentId);
            }
        }

        public IEnumerable<StudentV2> Get(Filter filter = null)
        {
            if (filter == null || filter.Values == null) return students;

            IEnumerable<StudentV2> result = new List<StudentV2>();

            foreach (var value in filter.Values)
            {
                switch (filter.Type)
                {
                    case FilterType.Equals:
                        result = result.Union(students.Where(s => EqualsV2(s, value, filter.Field)));
                        break;
                    case FilterType.Contains:
                        result = result.Union(students.Where(s => ContainsV2(s, value, filter.Field)));
                        break;
                    case FilterType.StartsWith:
                        result = result.Union(students.Where(s => StartsWithV2(s, value, filter.Field)));
                        break;
                    case FilterType.EndsWith:
                        result = result.Union(students.Where(s => EndsWithV2(s, value, filter.Field)));
                        break;
                }
            }

            return result;
        }

        private bool EqualsV2(StudentV2 s, string value, Field field)
        {
            return field switch
            {
                Field.Email => s.Email.Equals(value, StringComparison.CurrentCultureIgnoreCase),
                Field.Phone => s.Phone.Equals(value, StringComparison.CurrentCultureIgnoreCase),
                _ => (s.FirstName + " " + s.LastName).Equals(value, StringComparison.CurrentCultureIgnoreCase),
            };
        }

        private bool StartsWithV2(StudentV2 s, string value, Field field)
        {
            return field switch
            {
                Field.Email => s.Email.StartsWith(value, StringComparison.CurrentCultureIgnoreCase),
                Field.Phone => s.Phone.StartsWith(value, StringComparison.CurrentCultureIgnoreCase),
                _ => (s.FirstName + " " + s.LastName).StartsWith(value, StringComparison.CurrentCultureIgnoreCase),
            };
        }

        private bool ContainsV2(StudentV2 s, string value, Field field)
        {
            return field switch
            {
                Field.Email => s.Email.Contains(value, StringComparison.CurrentCultureIgnoreCase),
                Field.Phone => s.Phone.Contains(value, StringComparison.CurrentCultureIgnoreCase),
                _ => (s.FirstName + " " + s.LastName).Contains(value, StringComparison.CurrentCultureIgnoreCase),
            };
        }

        private bool EndsWithV2(StudentV2 s, string value, Field field)
        {
            return field switch
            {
                Field.Email => s.Email.EndsWith(value, StringComparison.CurrentCultureIgnoreCase),
                Field.Phone => s.Phone.EndsWith(value, StringComparison.CurrentCultureIgnoreCase),
                _ => (s.FirstName + " " + s.LastName).EndsWith(value, StringComparison.CurrentCultureIgnoreCase),
            };
        }

        public void Update(StudentV2 student)
        {
            lock (students)
            {
                StudentV2 studentToUpdate = students.Single(s => s.Id == student.Id);
                studentToUpdate.FirstName = student.FirstName;
                studentToUpdate.LastName = student.LastName;
                studentToUpdate.Email = student.Email;
                studentToUpdate.Phone = student.Phone;
            }
        }

        private void Initialize()
        {
            students.Add(new StudentV2() { Id = 1, FirstName = "Test1", LastName = "Student1" ,Email = "test@test.ro", Phone = "1234545"});
            students.Add(new StudentV2() { Id = 2, FirstName = "Test2", LastName = "Student2" ,Email = "test@test.ro", Phone = "1234445" });
        }
    }
}
