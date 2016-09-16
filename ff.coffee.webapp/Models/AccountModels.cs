using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Text;
using ff.coffee.repository;
using ff.coffee.webapp.Helpers;

namespace ff.coffee.webapp.Models
{
    public class AccountModel
    {
        private IUnitOfWork uow;
        private StaffRepository repo;

        public Staff dtoUser;
        public bool RememberMe { get; set; }
        
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public bool Login(string UserName, string Password,bool rememberMe = false)
        {
            uow = new UnitOfWork();
            repo = new StaffRepository(uow);
            IEnumerable<Staff> lstUser = repo.GetAll();
            Password = SecurityHelpers.EncryptText(Password);

            if (CheckUserValid(lstUser, UserName, Password))
            {
                this.RememberMe = rememberMe;
                return true;
            }

            return false;
        }

        private bool CheckUserValid(IEnumerable<Staff> listUser, string UserName, string Password)
        {
            foreach (Staff user in listUser)
            {
                if (user.UserName == UserName && user.Password == Password && user.Enable)
                {
                    dtoUser = user;
                    return true;
                }
            }
            return false;
        }
    }
}
