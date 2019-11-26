using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModulePL.ViewModels
{
    public class StudentWithTotalViewModel
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public int Total { get; internal set; }
        public IEnumerable<PaymentViewModel> Payments { get; set; }
    }
}
