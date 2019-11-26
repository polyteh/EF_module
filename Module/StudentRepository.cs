using ModuleDal.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace ModuleDal
{
    public class StudentRepository
    {
        private readonly ModuleContext _context;

        public StudentRepository()
        {
            _context = new ModuleContext();
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        public Student GetById(int id)
        {
            return _context.Students
                .Include(x=>x.Payments)
                .FirstOrDefault(x => x.Id == id);
        }
        public void AddStudent(Student newStudent)
        {
            _context.Students.Add(newStudent);
            _context.SaveChanges();
        
        }
        public IEnumerable<Student> GetStudentsPayLessThan(int value)
        {
            var studentPayLessThan = _context.Students.Include(paym => paym.Payments).Where(x=>(x.Payments.Sum(y=>y.Value)<value)).ToList();
            return studentPayLessThan;
        }


    }
}
