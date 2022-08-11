using System;
using System.Collections.Generic;

namespace MvcDemo.DbModels
{
    public partial class TblAdmin
    {
        public int AdminId { get; set; }
        public string AdminName { get; set; }
        public string AdminPassword { get; set; }
        public int TravelIdFk { get; set; }

        public virtual TblTravel TravelIdFkNavigation { get; set; }
    }
}
