using bmbox.DAL.Entities;
using Bmbox.DAL.Repos;
using bmbox_main.Models;
using bmbox_main.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmbox_main.Helpers
{
    public class LogHelper
    {
        private static AbsRepo<Log, int> logRepo;

        static LogHelper()
        {
            logRepo = new LogRepo();
        }

        public static void Info(Log log)
        {
            log.LogType = Constants.LOG_INFO;
            logRepo.Create(log);
        }
        public static void Error(Log log)
        {
            log.LogType = Constants.LOG_ERROR;
            logRepo.Create(log);
        }

    }
}