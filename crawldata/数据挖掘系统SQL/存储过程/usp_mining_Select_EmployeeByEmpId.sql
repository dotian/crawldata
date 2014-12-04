use DataMiningDB
go

if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_Select_EmployeeByEmpId]')
     and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
 drop procedure usp_mining_Select_EmployeeByEmpId
GO

 create proc usp_mining_Select_EmployeeByEmpId
  @EmpId varchar(15)
 as
  
  select EmpId,LoginPwd,SecretKey,EmpName,UserPermissions,RunStatus from dbo.Employee
   where EmpId = @EmpId
  
  go























