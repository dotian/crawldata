if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_select_employee_EmpIdAndEmpName]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_select_employee_EmpIdAndEmpName
GO
create proc usp_mining_select_employee_EmpIdAndEmpName

as
  
  select EmpId,EmpName from dbo.Employee where RunStatus =1
go






















