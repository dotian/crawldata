using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using DataCrawler.DAL.Crawler;
using DataCrawler.Model.Crawler;

namespace DataCrawler.BLL.Crawler
{
    public class EmployeeBLL
    {
        public int LoginActionManager(string loginName, string loginPwd)
        {
            int loginResult = 0;
            EmployeeDAL dal = new EmployeeDAL();
            Employee employee = dal.GetEmployeeByEmpIdService(loginName);

            bool b = Md5Helper.CompareMd5pwd(loginPwd, employee.SecretKey, employee.LoginPwd);
            if (b)
            {
                //登录成功
                loginResult = 1;
            }
            else
            {
                //密码错误
                loginResult = 2;
            }

            return loginResult;

        }

        public int AddAccountManager(string userName, string pwd, string empName, int permission)
        {
            string key = "";
            string md5Pwd = Md5Helper.GetMd5PwdEncrypt(pwd, ref key);

            int result = new EmployeeDAL().AddAccountService(userName, md5Pwd, key, empName, permission);
            //存入数据库
            return result;
           
        }

        public int UpdateAccountManager(string userName, string pwd, string empName, int permission)
        {
            //修改密码 使用新key,新密码

            string key = "";
            string md5Pwd = Md5Helper.GetMd5PwdEncrypt(pwd, ref key);
            int result = new EmployeeDAL().UpdateAccountService(userName, md5Pwd, key, empName, permission);
            //存入数据库

            return result;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public int UpdateAccountPwdManager(string userName, string pwd)
        {
            string key = "";
            string md5Pwd = Md5Helper.GetMd5PwdEncrypt(pwd, ref key);
            int result = new EmployeeDAL().UpdateAccountPwdService(userName, md5Pwd, key);
            return result;
        }

        public List<Employee> GetEmployeeListManager()
        {
            List<Employee> list = new EmployeeDAL().GetEmpListService();
            return list;
        }

        public List<Employee> GetEmployeeListBySearchManager(string searchKey,int searchType)
        {
            List<Employee> list = new EmployeeDAL().GetEmployeeListBySearchService(searchKey, searchType);
            return list;
        }

        public int DeleteAccountManager(string userName)
        {
            int result = new EmployeeDAL().DeleteAccountService(userName);
            return result;
        }

        public int AddEmployeeCustomerManager(string customerId, string pwd, string empname, int permissions, string empId)
        {
            string key = "";
            string md5Pwd = Md5Helper.GetMd5PwdEncrypt(pwd, ref key);
            int result = new EmployeeDAL().AddEmployeeCustomerService(customerId, md5Pwd, key, empname, permissions, empId);
            // 0插入失败, 1插入成功 2账号已存在
            return result;
        }




        public List<EmployeeCustomer> GetEmployeeCustomerListManager(int searchType,string searchKey)
        {
            return new EmployeeDAL().GetEmployeeCustomerListService(searchType, searchKey);
        }



    }
}
