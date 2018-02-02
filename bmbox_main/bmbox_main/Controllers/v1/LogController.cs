using Bmbox.DAL.Entities;
using Bmbox.DAL.Repos;
using bmbox_main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace bmbox_main.Controllers.v1
{
    public class LogController : ApiController
    {
        private AbsRepo<Log, int> repo = new LogRepo();

        [HttpGet]
        public List<Log> GetLogs()
        {
            return repo.GetAll().ToList();
        }

        //private LogsDTO MapToModel(Log p)
        //{
        //    return new LogsDTO
        //    {
        //        Id = p.Id,
        //        Action = p.Action,
        //        Controller = p.Controller, 
        //        IPAddress = p.IPAddress,
        //        Method = p.Method,
        //        Date = p.Date,
        //        LogType = p.LogType,
        //        User = p.User
        //    };
        //}
    }
}
