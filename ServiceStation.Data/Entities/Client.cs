using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ServiceStation.Data.Entities
{
    public class Client
    {
        public int ClientId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string FullName { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }

        [Required]
        [Phone]
        [MinLength(7, ErrorMessage = "The field should contain at least 7 symbols")]
        public string Phone { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
