if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_insert_employee]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_insert_employee
GO
create proc usp_mining_insert_employee
@empid varchar(20),
@loginpwd varchar(50),
@secretkey varchar(10),
@empName varchar(20),
@permission int


as
  if exists(select * from dbo.Employee where EmpId =@empid)
   begin
      select '2'
    end
   else
      begin
         insert into dbo.Employee(EmpId,LoginPwd,SecretKey,EmpName,UserPermissions,RunStatus,CreateDate)
          values(@empid,@loginpwd,@secretkey,@empName,@permission,1,getdate())
         select '1'
      end
go



--select * from dbo.ProjectList















