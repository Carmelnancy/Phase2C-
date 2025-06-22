using System.ComponentModel.DataAnnotations;

namespace Case3.Utilities
{
    public class NoFutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                return date <= DateTime.Today;
            }
            return true;
        }
    }
}
