if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_update_employee]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_update_employee
GO
create proc usp_mining_update_employee
 @empid varchar(50),
 @loginpwd varchar(50),
 @secretkey varchar(10),
 @empname varchar(50),
 @permissions int
as
   update dbo.Employee set LoginPwd = @loginpwd,
     SecretKey = @secretkey,EmpName=@empname,UserPermissions=@permissions,CreateDate =getdate()
     where EmpId = @empid
go






































