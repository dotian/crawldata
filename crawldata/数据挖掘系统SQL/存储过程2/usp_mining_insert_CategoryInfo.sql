use DataMiningDB
go

  if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_insert_CategoryInfo]')
     and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
 drop procedure usp_mining_insert_CategoryInfo
GO

 create proc usp_mining_insert_CategoryInfo
  @catename varchar(50),
  @empname varchar(50),
  @classid int
as
  insert into dbo.CategoryInfo(CategoryName,EmployeeId,ClassId)
  values (@catename,@empname,@classid)
  
  select SCOPE_IDENTITY()
 go 
 
 






















