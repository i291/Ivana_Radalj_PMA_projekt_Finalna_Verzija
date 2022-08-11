using MvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDemo.Mappers
{
    public class usertravelMapper
    {
        public static usertravel FromDatabase(DbModels.TblUsertravel ut)
        {
            return new usertravel(ut.TravelIdFk,ut.UserIdFk);
        }
        public static DbModels.TblUsertravel ToDatabase(usertravel usertravel )
        {

            return new DbModels.TblUsertravel
            {

                TravelIdFk = usertravel.Idtravela.HasValue ? usertravel.Idtravela.Value : 0,
                UserIdFk = usertravel.Idusera.HasValue ? usertravel.Idusera.Value : 0

            };



        }
    }
}
