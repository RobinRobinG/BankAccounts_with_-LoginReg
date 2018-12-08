using System;
using System.ComponentModel.DataAnnotations;

namespace BankAccount.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Amount { get; set; }
        public DateTime Date {get;set;} = DateTime.Now;
        public int UserId {get;set;}
        public User TransactionUser { get; set; }

    }
}