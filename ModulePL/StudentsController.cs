using ModuleBL;
using ModuleBL.Models;
using ModulePL.ViewModels;
using System.Linq;
using System.Collections.Generic;
using ModulePL.PostModels;
using System;

namespace ModulePL
{
    public class StudentsController
    {
        public StudentViewModel GetById(int id)
        {
            var service = new StudentService();

            var student = service.GetById(id);

            return new StudentViewModel
            {
                Age = student.Age.GetValueOrDefault(),
                FullName = $"{student.FirstName} {student.Lastname}",
                Payments = student.Payments.Select(payment => new PaymentViewModel
                {
                    Date = payment.Date,
                    Value = payment.Value
                })
            };
        }
        public void AddStudent(StudentPostModel newPostStModel)
        {
            if ((newPostStModel.Age <= 0) || (newPostStModel.Payments == null))
            {
                Console.WriteLine("Input error");
                return;
            }
            var service = new StudentService();

            List<PaymentModel> mappedPayments = new List<PaymentModel>();
            foreach (var item in newPostStModel.Payments)
            {
                mappedPayments.Add(new PaymentModel() { Date = item.Date, Value = item.Value });
            }

            StudentModel studModelToAdd = new StudentModel()
            {
                FirstName = newPostStModel.FirstName,
                Lastname = newPostStModel.Lastname,
                Age = newPostStModel.Age,
                Payments = mappedPayments
            };
            service.AddStudent(studModelToAdd);
        }
        public IEnumerable<StudentWithTotalViewModel> GetStudentWhichPayLessThan(int value)
        {
            var service = new StudentService();
            var studModel=service.GetStudentPayLessThan(value);
            List<StudentWithTotalViewModel> stViewModel = new List<StudentWithTotalViewModel>();
            foreach (var item in studModel)
            {
                stViewModel.Add(StudentModelToStudentWithTotalViewModel(item));
            }
            return stViewModel;

        }
        private StudentWithTotalViewModel StudentModelToStudentWithTotalViewModel(StudentModel modelToMap)
        {
            List<PaymentViewModel> mappedPayments = new List<PaymentViewModel>();
            foreach (var item in modelToMap.Payments)
            {
                mappedPayments.Add(new PaymentViewModel() { Date = item.Date, Value = item.Value });
            }
            int totalAmount = modelToMap.Payments.Sum(x => x.Value);
            StudentWithTotalViewModel stTotalView = new StudentWithTotalViewModel() { FullName = $"{modelToMap.FirstName} {modelToMap.Lastname}", 
                Total = totalAmount, Payments = mappedPayments };
            return stTotalView;
        }
    }
}
