using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDemo.Models
{
    public class usertravel
    {
        public int? Idusera { get; set; }
        public int? Idtravela { get; set; }






        public usertravel(int? idusera, int? idtravela)
        {
            Idusera = idusera;
            Idtravela = idtravela;





        }
    }
}
