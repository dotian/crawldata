using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCrawler.Model.Crawler
{
    public class EmployeeCustomer
    {
        public string CustomerId { get; set; }
        public string EmpName { get; set; }
        public int UserPermissions { get; set; }
        public string EmpId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
