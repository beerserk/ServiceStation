using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ServiceStation.Data.Entities
{
    public class Order
    {
        public int OrderId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime Date { get; set; }

        [Range(0.01, 10000, ErrorMessage = "Amount should be in range between 0.01 and 10000")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        public OrderStatus Status { get; set; }

        public int CarId { get; set; }

        public virtual Car Car { get; set; }
    }
    public enum OrderStatus
    { 
        [Display(Name = "In progress")]
        InProgress,

        [Display(Name = "Completed")]
        Completed,

        [Display(Name = "Cancelled")]
        Cancelled
    }
}
