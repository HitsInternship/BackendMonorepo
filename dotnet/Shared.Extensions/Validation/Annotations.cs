using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Extensions.Validation
{
    public static class Annotations
    {
        public class EmailAttribute : RegularExpressionAttribute
        {
            public EmailAttribute() : base("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.(?:[a-zA-Z]{2,})(?:\\.[a-zA-Z]{2,})*$")
            {
                ErrorMessage = "Invalid email";
            }
        }
    }
}
