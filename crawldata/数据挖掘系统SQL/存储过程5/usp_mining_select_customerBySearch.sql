if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_select_customerBySearch]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_select_customerBySearch
GO
create proc usp_mining_select_customerBySearch
@searchType int,
@searchKey varchar(20)
as
  if(len(@searchKey)<=0)
    begin
       select CustomerId,EmpName,UserPermissions,EmpId,CreateDate
         from CustomerInfo where RunStatus = 1
    end
  else if(@searchType=0)
    begin
     select CustomerId,EmpName,UserPermissions,EmpId,CreateDate 
      from CustomerInfo where  CustomerId like '%'+@searchKey+'%' and RunStatus = 1
    end
  else
      select CustomerId,EmpName,UserPermissions,EmpId,CreateDate 
        from CustomerInfo  where  EmpName like '%'+@searchKey+'%'  and RunStatus = 1
go




















