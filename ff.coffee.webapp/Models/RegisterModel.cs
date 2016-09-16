using ff.coffee.repository;
using ff.coffee.webapp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ff.coffee.webapp.Models
{
    public class RegisterModel
    {
        // For Create User
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        // For Update User
        public int Id { get; set; }
        public string Position { get; set; }
        public string Notes { get; set; }
        public string StaffName { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        [Display(Name = "Confirm new password")]
        public string ConfirmNewPassword { get; set; }
        public bool Enable { get; set; }

        [Display(Name = "Role")]
        public int Role { get; set; }

        public IDictionary<string, int> ListRoles
        {
            get
            {
                return new Dictionary<string, int>() 
                {
                    {"Admin", 1},
                    {"ViceManager", 2},
                    {"Manager", 3},
                    {"GroupLeader", 4},
                    {"Cashier", 5},
                    {"Order", 6},
                    {"Chef", 7}
                };
            }
        }

        //  For manage user
        public List<Staff> ListUser { get; set; }

        private Staff dtoUser { get; set; }

        public void GetUserByUserName(string userName)
        {
            IUnitOfWork uow = new UnitOfWork();
            StaffRepository userRepo = new StaffRepository(uow);

            this.dtoUser = userRepo.GetAll().Where(u => u.UserName == userName).SingleOrDefault();
            MapData2Model();
        }

        public void GetListUser(string exceptUserName)
        {
            IUnitOfWork uow = new UnitOfWork();
            StaffRepository userRepo = new StaffRepository(uow);
            this.ListUser = new List<Staff>();
            IEnumerable<Staff> lstUser = userRepo.GetAll();

            foreach (Staff user in lstUser)
            {
                if (user.Enable 
                    && user.UserName != exceptUserName)
                {
                    this.ListUser.Add(user);
                }
            }
        }

        public void UpdateUserInfo(int Id)
        {
            IUnitOfWork uow = new UnitOfWork();
            StaffRepository userRepo = new StaffRepository(uow);

            this.dtoUser = userRepo.SingleOrDefault(Id);

            if (!String.IsNullOrEmpty(this.NewPassword) 
                && this.NewPassword.Equals(this.ConfirmNewPassword))
            {
                this.dtoUser.Password = SecurityHelpers.EncryptText(this.NewPassword);
            }

            this.dtoUser.StaffName = this.StaffName;
            this.dtoUser.Position = this.Position;
            if (this.dtoUser.Role < this.Role)
            {
                this.dtoUser.Role = (byte)this.Role;
            }
            else
            {
                this.Role = (int)this.dtoUser.Role;
            }

            this.dtoUser.Notes = this.Notes;
            this.dtoUser.Enable = this.Enable;

            userRepo.Update(this.dtoUser);
        }
        public int CreateAccount()
        {
            IUnitOfWork uow = new UnitOfWork();
            StaffRepository userRepo = new StaffRepository(uow);
            MapData2Dto();

            return userRepo.Insert(dtoUser);
        }

        private void MapData2Dto()
        {
            dtoUser = new Staff();
            dtoUser.UserName = this.UserName;
            dtoUser.StaffName = this.UserName;
            dtoUser.Password = SecurityHelpers.EncryptText(this.Password);
            dtoUser.Role = (byte)this.Role;
            dtoUser.Enable = this.Enable;
            dtoUser.DateJoin = DateTime.Now;
        }

        private void MapData2Model()
        {
            this.Id = dtoUser.ID;   
            this.UserName = dtoUser.UserName;
            this.StaffName = dtoUser.StaffName;
            this.Position = dtoUser.Position;
            this.Password = dtoUser.Password;
            this.ConfirmPassword = dtoUser.Password;
            this.Role = dtoUser.Role.Value;
            this.Enable = dtoUser.Enable;
        }
    }
}