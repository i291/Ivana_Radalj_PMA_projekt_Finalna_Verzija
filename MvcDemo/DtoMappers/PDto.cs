using Newtonsoft.Json.Linq;
using MvcDemo.Models;

namespace MvcDemo.DtoMappers
{
    public static class PDto //data tranver object, iz api risponsa u model pretvara!
    {
        public static Putovanje FromJson(JObject json)
        {
            var name = json["travel_name"].ToObject<string>();
            var id = json["travel_id"].ToObject<int?>();
            var price = json["travel_price"].ToObject<string>();
            var duration = json["travel_duration"].ToObject<string>();


            return new Putovanje(id,name,duration,price);
        }
    }
}