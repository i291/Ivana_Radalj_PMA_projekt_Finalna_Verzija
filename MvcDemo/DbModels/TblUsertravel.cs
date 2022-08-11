using System;
using System.Collections.Generic;

namespace MvcDemo.DbModels
{
    public partial class TblUsertravel
    {
        public int UserIdFk { get; set; }
        public int TravelIdFk { get; set; }

        public virtual TblTravel TravelIdFkNavigation { get; set; }
        public virtual TblUser UserIdFkNavigation { get; set; }
    }
}
