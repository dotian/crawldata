if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_insert_ContendInfo]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_insert_ContendInfo
GO
create proc usp_mining_insert_ContendInfo
@projectId int,
@contendId int,
@empId varchar(50)
as 
  declare @contendname varchar(50);
   select @contendname = ProjectName  from ProjectList where ProjectId=@contendId
  
  if(@projectId=@contendId)
   return;
  if exists(select Id from ContendInfo where ProjectId=@projectId and ContendId=@contendId)
    begin
      print '竞争社项目已存在';
    end
   else
     begin
		  insert into ContendInfo(ProjectId,ContendId,ContendName,EmpId)
		  values(@projectId,@contendId,@contendname,@empId) 
     end
go

exec usp_mining_insert_ContendInfo 4,12,'admin'


select * from ContendInfo


















