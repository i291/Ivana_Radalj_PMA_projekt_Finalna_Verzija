using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MvcDemo.Models;


namespace MvcDemo.Mappers
{
    public class TravelMapper
    {   
        public static Putovanje FromDatabase(DbModels.TblTravel t)
        {    
                return new Putovanje(t.TravelId, t.TravelName,t.TravelDuration,t.TravelPrice);
        }
        public static DbModels.TblTravel ToDatabase(Putovanje putovanje)
        {
            
            return new DbModels.TblTravel
            {

                TravelId = putovanje.Id.HasValue ? putovanje.Id.Value : 0,
                TravelName = putovanje.Name,
                TravelDuration=putovanje.Duration,
                TravelPrice=putovanje.Price
                
               
            };



        }
    }
}
