
use DataMiningDB
go

  if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_insert_PullInfo]')
     and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
 drop procedure usp_mining_insert_PullInfo
GO

 create proc usp_mining_insert_PullInfo
  @projectId int,
  @classId int,
  @mdifference int
as
  if not exists(select PullId from  dbo.PullInfo where ProjectId=@projectId and ClassId = @classId)
    begin	
	  insert into dbo.PullInfo(ProjectId,ClassId,MinCid,MDifference)
	  values (@projectId,@classId,0,@mdifference)
    end
  else 
    begin
       print '已存在相同的数据';
    end 
 go 































