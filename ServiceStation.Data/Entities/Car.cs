using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation.Data.Entities
{
    [DisplayColumn("VIN")]
    public class Car
    {
        public int CarId { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [Range(1970, 2020, ErrorMessage = "Please, enter correct year")]
        public int Year { get; set; }

        [Required]
        [MinLength(17, ErrorMessage = "VIN code should contain 17 symbols")]
        [MaxLength(17, ErrorMessage = "VIN code should contain 17 symbols")]
        public string VIN { get; set; }

        public int ClientId { get; set; }

        public virtual Client Owner { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
