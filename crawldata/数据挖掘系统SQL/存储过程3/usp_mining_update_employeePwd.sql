if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_update_employeePwd]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_update_employeePwd
GO
create proc usp_mining_update_employeePwd
@empid varchar(50),
@loginpwd varchar(50),
@secretkey varchar(50)
as
  update dbo.Employee set LoginPwd=@loginpwd ,SecretKey = @secretkey where EmpId =@empid
go





  
















