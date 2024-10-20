using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Exception
{
    public class BookingNotFoundException : ApplicationException
    {
        public BookingNotFoundException(string message) : base(message)
        {
        }
    }
}

