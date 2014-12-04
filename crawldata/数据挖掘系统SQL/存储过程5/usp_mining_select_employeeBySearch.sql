if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_select_employeeBySearch]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_select_employeeBySearch
GO
create proc usp_mining_select_employeeBySearch
@searchKey varchar(20),
@searchType int
as
  if(@searchType=0)
   begin
     select EmpId,EmpName,UserPermissions,CreateDate 
      from employee where  EmpId like '%'+@searchKey+'%' and RunStatus = 1
   end
  else
      select EmpId,EmpName,UserPermissions,CreateDate 
        from employee  where  EmpName like '%'+@searchKey+'%'  and RunStatus = 1
go

















