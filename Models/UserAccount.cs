using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankAccount.Models
{
    public class UserAccount
    {
        public int AccountId { get; set; }
        
        [Display(Name = "UserName:")]
        public string UserName { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Display(Name = "Balance:")]
        public decimal Balance { get; set; }
        
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Display(Name = "Deposit/Withdrawal:")]
        public decimal? DepositOrWithdrawalAmount { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}