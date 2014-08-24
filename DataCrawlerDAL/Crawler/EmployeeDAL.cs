using System;
using System.Collections.Generic;

using System.Text;
using System.Data;

using System.Data.SqlClient;
using RionSoft.IBRS.Business.DAL;

using DataCrawler.Model.Crawler;
using LogNet;

namespace DataCrawler.DAL.Crawler
{
    public class EmployeeDAL
    {
        public Employee GetEmployeeByEmpIdService(string empId)
        {
            Employee employee = new Employee();
            try
            {
                SqlParameter[] parms = new SqlParameter[]{
                 new SqlParameter("EmpId",empId)
                };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_Select_EmployeeByEmpId", parms);
                DataTable dt = dal.SelectData("usp_mining_Select_EmployeeByEmpId", parms);
                if (dt!=null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        //EmpId,LoginPwd,SecretKey,EmpName,UserPermissions,RunStatus
                        employee.EmpId = row["EmpId"].ToString();
                        employee.LoginPwd = row["LoginPwd"].ToString();
                        employee.SecretKey =row["SecretKey"].ToString();
                        employee.EmpName = row["EmpName"].ToString();
                        employee.UserPermissions = Convert.ToInt32(row["UserPermissions"]);
                        employee.RunStatus = Convert.ToInt32(row["RunStatus"]);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetEmployeeByEmpIdService", ex);
            }
            return employee;
        }

        /// <summary>
        /// 添加账户
        /// </summary>
        /// <param name="userName">登录名</param>
        /// <param name="md5Pwd">密码</param>
        /// <param name="key">密钥</param>
        /// <param name="empName">真实姓名</param>
        /// <param name="permission">权限</param>
        /// <returns></returns>
        public int AddAccountService(string userName, string md5Pwd, string key,string empName, int permission)
        {
            int result = 0;
            try
            {
                SqlParameter[] parms = new SqlParameter[]{
                    new SqlParameter("empid",userName),
                    new SqlParameter("loginpwd",md5Pwd),
                    new SqlParameter("secretkey",key),
                    new SqlParameter("empName",empName),
                    new SqlParameter("permission",permission)
                };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_insert_employee", parms);
              object objResult =  dal.ExecuteScalar("usp_mining_insert_employee", parms);
              result = Convert.ToInt32(objResult);

            }
            catch (Exception ex)
            {

                LogBLL.Error("AddAccountService", ex);
            }
            return result;
            
        }

        public int UpdateAccountService(string userName, string md5Pwd, string key, string empName, int permission)
        {
            int result = 0;
            try
            {
                SqlParameter[] parms = new SqlParameter[]{
                    new SqlParameter("empid",userName),
                    new SqlParameter("loginpwd",md5Pwd),
                    new SqlParameter("secretkey",key),
                     new SqlParameter("empname",empName),
                    new SqlParameter("permissions",permission)
                };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_update_employee", parms);
                result = dal.ExecuteNonQuery("usp_mining_update_employee", parms);
            }
            catch (Exception ex)
            {

                LogBLL.Error("UpdateAccountService", ex);
            }
            return result;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="md5Pwd"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public int UpdateAccountPwdService(string userName, string md5Pwd, string key)
        {
            int result = 0;
            try
            {
                SqlParameter[] parms = new SqlParameter[]{
                    new SqlParameter("empid",userName),
                    new SqlParameter("loginpwd",md5Pwd),
                    new SqlParameter("secretkey",key)
                };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_update_employeePwd", parms);
                result = dal.ExecuteNonQuery("usp_mining_update_employeePwd", parms);
            }
            catch (Exception ex)
            {

                LogBLL.Error("UpdatePwdService", ex);
            }
            return result;
        }

        public List<Employee> GetEmpListService()
        {
            List<Employee> list = new List<Employee>();
            try
            {
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_select_employee");
                DataTable dt = dal.SelectData("usp_mining_select_employee", null);
                if (dt!=null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Employee employee = new Employee();
                        employee.EmpId = row["EmpId"].ToString();
                        employee.EmpName = row["EmpName"].ToString();
                        employee.UserPermissions = Convert.ToInt32(row["UserPermissions"]);
                        employee.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                        list.Add(employee);
                    }
                }
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetEmpListService", ex);
            }
            return list;
        }

        public List<Employee> GetEmployeeListBySearchService(string searchKey, int searchType)
        {
            List<Employee> list = new List<Employee>();
            try
            {
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_select_employeeBySearch");
                DataTable dt = dal.SelectData("usp_mining_select_employeeBySearch", null);
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Employee employee = new Employee();
                        employee.EmpId = row["EmpId"].ToString();
                        employee.EmpName = row["EmpName"].ToString();
                        employee.UserPermissions = Convert.ToInt32(row["UserPermissions"]);
                        employee.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                        list.Add(employee);
                    }
                }
            }
            catch (Exception ex)
            {
                LogBLL.Error("usp_mining_select_employeeBySearch", ex);
            }
            return list;
        }

        public int DeleteAccountService(string userName)
        {
            int result = 0;
            try
            {
                SqlParameter[] parms = new SqlParameter[]{
                    new SqlParameter("empid",userName)
                };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_delete_employeeByEmpId", parms);
                result = dal.ExecuteNonQuery("usp_mining_delete_employeeByEmpId", parms);
            }
            catch (Exception ex)
            {

                LogBLL.Error("DeleteAccountService", ex);
            }
            return result;
        }

        public int AddEmployeeCustomerService(string customerId,string pwd,string secretkey,string empname,int permissions,string empId)
        {

            // 0插入失败, 1插入成功 2账号已存在
            int result = 0;
            try
            {
                SqlParameter[] parms = new SqlParameter[]{
                    new SqlParameter("customerId",customerId),
                    new SqlParameter("pwd",pwd),
                    new SqlParameter("secretkey",secretkey),
                    new SqlParameter("empname",empname),
                    new SqlParameter("permissions",permissions),
                    new SqlParameter("empId",empId)
                };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_miming_insert_EmployeeCustomer", parms);
               
                object objResult = dal.ExecuteScalar("usp_miming_insert_EmployeeCustomer", parms);
                result = Convert.ToInt32(objResult);
            }
            catch (Exception ex)
            {

                LogBLL.Error("AddEmployeeCustomerService", ex);
            }
            return result;
        }

        public List<EmployeeCustomer> GetEmployeeCustomerListService(int searchType,string searchKey)
        {
            List<EmployeeCustomer> list = new List<EmployeeCustomer>();
            try
            {

                SqlParameter[] parms = new SqlParameter[] { 
                  new SqlParameter("searchType",searchType),
                  new SqlParameter("searchKey",searchKey)
                };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_select_customerBySearch", parms);
                DataTable dt = dal.SelectData("usp_mining_select_customerBySearch", parms);
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        EmployeeCustomer employee = new EmployeeCustomer();
                        employee.CustomerId = row["CustomerId"].ToString();
                        employee.EmpName = row["EmpName"].ToString();
                        employee.UserPermissions = Convert.ToInt32(row["UserPermissions"]);
                        employee.EmpId = row["EmpId"]==DBNull.Value?"":row["EmpId"].ToString();
                        employee.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                        list.Add(employee);
                    }
                }
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetEmployeeCustomerListService", ex);
            }
            return list;
        }


    }
}
