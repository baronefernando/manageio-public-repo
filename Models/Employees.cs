using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace ManageIO.Models
{
    //Table model
    public class Employees
    {
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Please select a role.")]
        [ForeignKey("RoleName")]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        public string FullName { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid email address: example@example.com")]
        [Required(ErrorMessage = "Please enter an email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter an address.")]
        public string Address { get; set; }

        [RegularExpression(@"^(\+44\s?7\d{3}|\(?07\d{3}\)?)\s?\d{3}\s?\d{3}$",ErrorMessage ="Please enter a valid mobile number.")]
        [Required(ErrorMessage = "Please enter a Mobile number.")]
        public string Mobile { get; set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Please enter a numerical value.")]
        [Required(ErrorMessage = "Please enter a salary.")]
        public int Salary {  get; set; }

    }
}
