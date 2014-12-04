   if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_Delete_ProjetTagRelationByProjectId]')
     and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
 drop procedure usp_mining_Delete_ProjetTagRelationByProjectId
GO

 create proc usp_mining_Delete_ProjetTagRelationByProjectId
  @projectId int,
  @tagId int
 as
   delete from dbo.ProjetTagRelation where ProjectId=@projectId and TagId =@tagId;
 go 
 