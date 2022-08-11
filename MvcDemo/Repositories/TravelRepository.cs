using Microsoft.EntityFrameworkCore;
using MvcDemo.DbModels;
using MvcDemo.Mappers;
using MvcDemo.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MvcDemo.DbModels;

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MvcDemo.Repositories
{
    public class TravelRepository : Controller
    {
        private readonly travelingContext _dbContext;

        public TravelRepository(travelingContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Models.Putovanje> GetAll()
        {
            return _dbContext.TblTravel.Select(x => TravelMapper.FromDatabase(x));

        }
        public IEnumerable<Models.Putovanje> GetAlll(int user )
        {

            return _dbContext.TblUsertravel
                .Where(ut => ut.UserIdFk == user)
               
                .Select(x => TravelMapper.FromDatabase(x.TravelIdFkNavigation));

        }
        public async Task<Models.Putovanje> GetAsync(int id)
        {

            var result = await _dbContext.TblTravel.SingleOrDefaultAsync(x => x.TravelId == id);
            if (result != null)
            {
                return TravelMapper.FromDatabase(result);

            }
            else
            {
                return null;
            }



        }
        public void Save(Models.Putovanje travel)
        {
            var dbtravel = TravelMapper.ToDatabase(travel); // treba ga pretvorit u bazi prepotznatljiv oblik!
            _dbContext.TblTravel.Add(dbtravel);
            _dbContext.SaveChanges();


        }
        public void Edit(Models.Putovanje travel)
        {

            var dbtravel = TravelMapper.ToDatabase(travel);
            _dbContext.TblTravel.Update(dbtravel);
            _dbContext.SaveChanges();

        }
        public Models.Putovanje SaveandReturn(Models.Putovanje travel)
        {
            var dbtravel = TravelMapper.ToDatabase(travel);
            _dbContext.TblTravel.Add(dbtravel);
            _dbContext.SaveChanges();

            var result = _dbContext.TblTravel
                .First(c => c.TravelId == dbtravel.TravelId);



            return TravelMapper.FromDatabase(result);
        }
        public void DeleteAsync(int id)
        {

            while(_dbContext.TblUsertravel.Where(x => x.TravelIdFk.Equals(id)).FirstOrDefault() != null) //prvo ga obrisat iz tablice user_travel
            {
                var travel_user = _dbContext.TblUsertravel.Where(x => x.TravelIdFk.Equals(id)).FirstOrDefault();
                _dbContext.TblUsertravel.Remove(travel_user);
                _dbContext.SaveChanges();

            }
            var result =  _dbContext.TblTravel

                .FirstOrDefault(x => x.TravelId == id); //pa tek onda iz tablice travel!
            if (result!= null) // ako ga ima u bazi onda ga brisi,nema ga smisla brisat ako ga nema!
            {
                _dbContext.TblTravel.Remove(result);
                _dbContext.SaveChanges();
               
            }
           
          

        }
      
       
        public async Task<bool> ExistsUsername(string username) // vraca true ili false ovisno postoji li taj user u bazi ili ne postoji!
        {
            return await _dbContext.TblUser.AnyAsync(x => x.UserName == username);

        }
        public async Task<bool> InsertUser(TblUser user)
        {
            var result = _dbContext.TblUser.Where(x => x.UserName.Equals(user.UserName)).FirstOrDefault(); 
            if (result == null) // ima li tog usera u bazi ili nema
            {
                _dbContext.TblUser.Add(user); // ako ga nema dodaj ga!
                return await _dbContext.SaveChangesAsync() > 0; // sejvaj promjene jer drukcije ne sejva nista!
            }
            else
            {
                return false;
            }


        }
        public async Task <TblUser> getuser(string username,string password)
        {
            return  _dbContext.TblUser.FirstOrDefault(x => x.UserName == username && x.UserPassword == password);

        }
        public async Task<bool> saveusertravel(int travelid, int userid)
        {
            if(_dbContext.TblTravel.Where(x => x.TravelId.Equals(travelid)).FirstOrDefault() != null) //ako ga nema u bazi vraca null
            {
                if (_dbContext.TblUsertravel.Where(x => x.TravelIdFk == travelid && x.UserIdFk == userid).FirstOrDefault() == null)// ako nije vec dodan u moje favorite! 
                {

                    _dbContext.TblUsertravel.Add(new TblUsertravel { TravelIdFk = travelid, UserIdFk = userid }); //dodaj ga
                    _dbContext.SaveChanges();
                    return true;
                } 
              
            }
            return false;
         
           
             
           

        }
        public async Task<bool> Deleteusertravel(int travelid,int userid)
        {
            var result = _dbContext.TblUsertravel.Where(x => x.TravelIdFk == travelid && x.UserIdFk == userid).FirstOrDefault();
            if (result != null) //ako ga ima obrisi ga!
            {
                _dbContext.TblUsertravel.Remove(result);
                _dbContext.SaveChanges();
            }
            return true;


        }


    }
}
