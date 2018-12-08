using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using BankAccount.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace BankAccount.Controllers
{
    public class BankAccountController : Controller
    {
        private ProjectContext dbContext;
        public BankAccountController(ProjectContext context)
        {
            dbContext = context;
        }

        // GET: user aacount page/
        [HttpGet]
        [Route("account/{target}")]
        public IActionResult Account(int target)
        {
            ViewBag.userID = HttpContext.Session.GetInt32("UserID");
            if(ViewBag.userID == target)
            {
                User LoggedInUser = dbContext.Users.Where( u => u.UserId == target).Include( u => u.Transactions).FirstOrDefault();
                UserAccount AccountInfo = new UserAccount
                {
                    UserName = LoggedInUser.FirstName + " " + LoggedInUser.LastName,
                    Balance = LoggedInUser.Transactions.Sum(t => t.Amount),
                    DepositOrWithdrawalAmount = null,
                    Transactions = LoggedInUser.Transactions
                };
                return View(AccountInfo);
            }
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "LoginReg");
        }

        [HttpPost]
        [Route("transaction")]
        public IActionResult AddTransaction(UserAccount AccountInfo)
        {
            int? userID = HttpContext.Session.GetInt32("UserID");
            User user = dbContext.Users.Where( u => u.UserId == userID).Include( u => u.Transactions).FirstOrDefault();

            AccountInfo.Transactions = user.Transactions;

            if(AccountInfo.DepositOrWithdrawalAmount == null)
            {
                ModelState.AddModelError("DepositOrWithdrawalAmount", "This field cannot be blank!");
                return View("Account", AccountInfo);
            }
            if(AccountInfo.Balance + AccountInfo.DepositOrWithdrawalAmount < 0)
            {
                ModelState.AddModelError("DepositOrWithdrawalAmount", "You cannot withdraw more than your balance.");
                return View("Account", AccountInfo);
            }
            Transaction transaction = new Transaction
            {
                Amount = (int)AccountInfo.DepositOrWithdrawalAmount,
                UserId = user.UserId
            };
            dbContext.Add(transaction);
            dbContext.SaveChanges();
            return RedirectToAction("Account",new {target = user.UserId});
        }
    }
}