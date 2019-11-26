using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModulePL.PostModels
{
    public class PaymentPostModel
    {
        public DateTime? Date { get; set; }
        public int Value { get; set; }
        public StudentPostModel Student { get; set; }
    }
}
