using System;
using System.Collections.Generic;

namespace MvcDemo.DbModels
{
    public partial class TblTravel
    {
        public TblTravel()
        {
            TblAdmin = new HashSet<TblAdmin>();
            TblUsertravel = new HashSet<TblUsertravel>();
        }

        public int TravelId { get; set; }
        public string TravelName { get; set; }
        public string TravelDuration { get; set; }
        public string TravelPrice { get; set; }

        public virtual ICollection<TblAdmin> TblAdmin { get; set; }
        public virtual ICollection<TblUsertravel> TblUsertravel { get; set; }
    }
}
