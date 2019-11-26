using ModuleBL.Models;
using ModuleDal;
using ModuleDal.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModuleBL
{
    public class StudentService
    {
        private readonly StudentRepository _studentRepository;
        public StudentService()
        {
            _studentRepository = new StudentRepository();
        }

        public StudentModel GetById(int id)
        {
            var studentEntity = _studentRepository.GetById(id);

            if (studentEntity == null)
                throw new ArgumentException("Student not fount");

            var resultStudent = new StudentModel
            {
                FirstName = studentEntity.FirstName,
                Lastname = studentEntity.LastName,
            };

            resultStudent.Payments = studentEntity.Payments.Select(payment => new PaymentModel
            {
                Student = resultStudent,
                Value = payment.Value,
                Date = payment.Date
            });

            return resultStudent;
        }
        public void AddStudent(StudentModel newStModel)
        {

            List<Payment> mappedPayment = new List<Payment>();
            foreach (var item in newStModel.Payments)
            {
                if (item.Date > DateTime.Now)
                {
                    Console.WriteLine("payment shoup be completed before now");
                    return;
                }
                mappedPayment.Add(new Payment() { Date = item.Date, Value = item.Value });
            }
            Student studentToAdd = new Student() { FirstName = newStModel.FirstName, LastName = newStModel.Lastname, Age = newStModel.Age, Payments = mappedPayment };
            _studentRepository.AddStudent(studentToAdd);
        }

        public IEnumerable<StudentModel> GetStudentPayLessThan(int value)
        {
            var studentModelList = _studentRepository.GetStudentsPayLessThan(value);

            if (studentModelList != null)
            {
                List<StudentModel> stModelResult = new List<StudentModel>();
                foreach (var item in studentModelList)
                {
                    stModelResult.Add(ModelToStudentModel(item));
                }
                return stModelResult;
            }
            return null;
        }
        private StudentModel ModelToStudentModel(Student modelToMap)
        {
            List<PaymentModel> mappedPayments = new List<PaymentModel>();
            foreach (var item in modelToMap.Payments)
            {
                mappedPayments.Add(new PaymentModel() { Date = item.Date, Value = item.Value });
            }
            StudentModel stModel = new StudentModel() { FirstName = modelToMap.FirstName, Lastname = modelToMap.LastName, Age = modelToMap.Age, Payments = mappedPayments };
            return stModel;
        }

    }
}
