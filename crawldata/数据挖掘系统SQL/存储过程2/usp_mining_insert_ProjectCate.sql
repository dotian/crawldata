use DataMiningDB
go

  if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_insert_ProjectCate]')
     and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
 drop procedure usp_mining_insert_ProjectCate
GO

 create proc usp_mining_insert_ProjectCate
  @projectid int,
  @cateId int

as

 insert into dbo.ProjectCate(ProjectId,CateId,CreateDate)
  values(@projectid,@cateId,getdate())

--把对应的分类下面的站点,规划到 新建的项目下面

 declare @classid int ,@keywords varchar(100)
select @classid = ClassId from dbo.CategoryInfo where CateId = @cateId
select @keywords  =MatchingRule from  dbo.ProjectList where ProjectId = @projectid


declare @siteid int
  declare pcurr cursor for 
        select SiteId from dbo.CategorySiteRelation where CateId = @cateId
      open pcurr
      fetch next from pcurr into  @siteid
      while (@@FETCH_STATUS = 0)
		 begin
             insert into ProjectDetail(ProjectId,KeyWords,ClassId,SiteId,StartDate,RunStatus,ProjectType)
             values(@projectid,@keywords,@classid,@siteid,getdate(),0,1)
           fetch next from pcurr into @siteid
         end
      close pcurr
   deallocate pcurr  
 go 
 




 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 





