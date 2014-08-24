using System;
using System.Collections.Generic;
using ConGetData.Model;

namespace ConGetData.DAL
{
    public interface IServiceDAL
    {
        List<CrawlTarget> GetTargetService();
        List<CK_SiteList> GetCK_listService();
        int UpdateCKStatusService(int siteId, int status);
    }

    public class ServiceLocator
    {
        public static Type serviceDalType = typeof(ServiceDAL); 

        public static IServiceDAL GetServiceDAL()
        {
            return (IServiceDAL)Activator.CreateInstance(serviceDalType);
        }

        public static void SetDalType<T>() where T : IServiceDAL
        {
            serviceDalType = typeof(T); 
        }
    }
}
