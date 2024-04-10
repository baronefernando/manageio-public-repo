using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace ManageIO.Models
{
    //Table model
    public class Roles
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleID { get; set; }

        [Required(ErrorMessage = "Please enter a role.")]
        [Key]
        public string RoleName { get; set; }
    }
}
