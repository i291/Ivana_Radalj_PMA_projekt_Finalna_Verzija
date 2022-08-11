using MvcDemo.DbModels;
using MvcDemo.Models;
using MvcDemo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDemo.Services
{
    public class TravelService
    {
        public TravelRepository _travelRepository;

        public TravelService(TravelRepository travelRepository)
        {
            _travelRepository = travelRepository;
        }
        public IEnumerable<Putovanje> GetAll()
        {
            return _travelRepository.GetAll();
        }
        public IEnumerable<Putovanje> GetAlll(int userid)
        {
            return _travelRepository.GetAlll(userid);
        }
        public async Task<Putovanje> GetAsync(int id)
        {
            return await _travelRepository.GetAsync(id);

        }
        public void Save(Putovanje travel)
        {
            _travelRepository.Save(travel);
        }
        public void Edit(Putovanje travel)
        {
            _travelRepository.Edit(travel);
        }
        public Putovanje SaveandReturn(Putovanje travel)
        {
            return _travelRepository.SaveandReturn(travel);
        }
        public void DeleteAsync(int id)
        {
             _travelRepository.DeleteAsync(id);
        }
       


        public async Task<bool> InsertUser(TblUser user)
        {
            if (!await _travelRepository.ExistsUsername(user.UserName)) //ako ne postoji taj user u bazi s tim usernamom,onda ga insertaj inace nemoj!
            {
                 return await _travelRepository.InsertUser(user);
            }
            return false;

        }
        public async Task<TblUser> getuser(string username,string passw) //radi brojnih rikvestova!
        {
           return await _travelRepository.getuser(username, passw);

        }

        public async Task<bool> Saveusertravel(int travelid, int userid) //radi brojnih rikvestova!
        {
           
            return await _travelRepository.saveusertravel(travelid, userid);

        }
        public async Task<bool> Deleteusertravel(int travelid,int userid)
        {
             return await _travelRepository.Deleteusertravel(travelid,userid);
        }

    }
}
