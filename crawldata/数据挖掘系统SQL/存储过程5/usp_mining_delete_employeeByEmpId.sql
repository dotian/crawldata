if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_delete_employeeByEmpId]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_delete_employeeByEmpId
GO
create proc usp_mining_delete_employeeByEmpId
@empid varchar(50)
as
 update dbo.Employee set RunStatus = 99 where EmpId = @empid
go






















