if exists (select * from dbo.sysobjects where id = object_id(N'[usp_minmig_select_customer_project]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_minmig_select_customer_project
GO
create proc usp_minmig_select_customer_project
@username varchar(50)
as
    --���� �ж� Ȩ��,�ж���Ŀ
    
     declare @premessions int,@empid varchar(20)
     select @premessions =UserPermissions ,@empid = EmpId from  CustomerInfo where CustomerId  =  @username   
     
     if(@premessions=4)            
       begin
         if(len(@empid)>1) 
           begin
               --��Ա��ʹ�õ�
			   select ProjectId,ProjectName,MatchingRule,EmpId,CreateDate,EndDate,RunStatus
				 from ProjectList  where RunStatus<>99
				and EmpId = @empid
           end
         else 
           begin
              --����Աʹ��
               select ProjectId,ProjectName,MatchingRule,EmpId,CreateDate,EndDate,RunStatus
				 from ProjectList  where RunStatus<>99
           end   
      end
  
go





















