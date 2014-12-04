if exists (select * from dbo.sysobjects where id = object_id(N'[usp_miming_insert_EmployeeCustomer]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_miming_insert_EmployeeCustomer
GO
create proc usp_miming_insert_EmployeeCustomer
@customerId varchar(50),
@pwd varchar(50),
@secretkey varchar(50),
@empname varchar(50),
@permissions int,
@empId varchar(50)
as
  if exists(select CustomerId from dbo.CustomerInfo where  CustomerId=@customerId )
    begin
      select '2';
    end
  else
    begin  
      insert into dbo.CustomerInfo(CustomerId,LoginPwd,SecretKey,EmpName,UserPermissions,ProjectId,EmpId,RunStatus,CreateDate)
        values(@customerId,@pwd,@secretkey,@empname,@permissions,0,@empId,1,getdate())
      select '1';
    end    
go




















