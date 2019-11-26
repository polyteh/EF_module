using ModulePL;
using ModulePL.PostModels;
using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var ctrl = new StudentsController();

            DateTime date1 = new DateTime(2017, 7, 20);
            DateTime date2 = new DateTime(2018, 8, 22);
            DateTime date3 = new DateTime(2019, 2, 12);
            DateTime date4 = new DateTime(2015, 5, 9);

            PaymentPostModel payment1 = new PaymentPostModel() {Date=date1, Value=100 };
            PaymentPostModel payment2 = new PaymentPostModel() { Date = date2, Value = 120 };
            PaymentPostModel payment3 = new PaymentPostModel() { Date = date3, Value = 110 };
            PaymentPostModel payment4 = new PaymentPostModel() { Date = date4, Value = 90 };

            List<PaymentPostModel> paymentList1 = new List<PaymentPostModel>() { payment1, payment2 };
            List<PaymentPostModel> paymentList2 = new List<PaymentPostModel>() { payment3, payment4 };
            List<PaymentPostModel> paymentList3 = new List<PaymentPostModel>() { payment1 };
            List<PaymentPostModel> paymentList4 = new List<PaymentPostModel>() { payment4 };

            StudentPostModel stToAdd_1 = new StudentPostModel() {FirstName="Ivan", Lastname="Ivanov",Age=20,Payments=paymentList1 };
            StudentPostModel stToAdd_2 = new StudentPostModel() { FirstName = "Petr", Lastname = "Petrov", Age = 20, Payments = paymentList2 };
            StudentPostModel stToAdd_3 = new StudentPostModel() { FirstName = "Homer", Lastname = "Simson", Age = 45, Payments = paymentList3 };
            StudentPostModel stToAdd_4 = new StudentPostModel() { FirstName = "Burt", Lastname = "Simson", Age = 20, Payments = paymentList4 };

            ctrl.AddStudent(stToAdd_1);
            ctrl.AddStudent(stToAdd_2);
            ctrl.AddStudent(stToAdd_3);
            ctrl.AddStudent(stToAdd_4);

            int minimumTotal = 150;
            var stPayLessThan = ctrl.GetStudentWhichPayLessThan(minimumTotal);
            if (stPayLessThan!=null)
            {
                Console.WriteLine($"Student(s), who paid less, than {minimumTotal}");
                foreach (var item in stPayLessThan)
                {
                    Console.WriteLine($"Student {item.FullName} paid in total {item.Total}");
                }
            }

            Console.WriteLine("Finish work");


            Console.ReadKey();

            //var student = ctrl.GetById(1);
        }
    }
}
