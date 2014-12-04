
  if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_Insert_ProjectTagByProjectId]')
     and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
 drop procedure usp_mining_Insert_ProjectTagByProjectId
GO

 create proc usp_mining_Insert_ProjectTagByProjectId
  @projectId int,
  @tagId int
 as
   declare @projectName varchar(100);
   select @projectName = ProjectName from dbo.ProjectList where ProjectId=@projectId 
   
   insert into dbo.ProjetTagRelation(ProjectId,ProjectName,TagId)
   values(@projectId,@projectName,@tagId)
 go 
 
 

 