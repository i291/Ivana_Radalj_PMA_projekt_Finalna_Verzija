using MvcDemo.DbModels;
using MvcDemo.Models.AuthModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcDemo.Models;

namespace MvcDemo.Mappers
{
    public class UserMapper
    {

        public static TblUser ToDatabase(RegisterData registerData)
        {
            return new TblUser(registerData.Username, registerData.Password);
        }


    }
}
