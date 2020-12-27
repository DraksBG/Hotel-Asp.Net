namespace Hotel.Web.Infrastructure
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateCheckAnnotation : ValidationAttribute
    {
        public DateCheckAnnotation()
        {
        }

        public override bool IsValid(object value)
        {
            var dt = (DateTime)value;
            if (dt.Day >= DateTime.Now.Day)
            {
                return true;
            }

            return false;
        }
    }
}
