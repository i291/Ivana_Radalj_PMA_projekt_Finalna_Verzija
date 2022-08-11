using System;
using System.Collections.Generic;

namespace MvcDemo.DbModels
{
    public partial class TblUser
    {

        public TblUser(string userName, string userPassword)
        {
            UserName = userName;
            UserPassword = userPassword;
            TblUsertravel = new HashSet<TblUsertravel>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }

        public bool IsAdmin { get; set; }
        public virtual ICollection<TblUsertravel> TblUsertravel { get; set; }
    }
}
