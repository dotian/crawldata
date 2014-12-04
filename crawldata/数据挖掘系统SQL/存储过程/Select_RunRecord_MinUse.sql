use DataMiningDB
 go
 if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_Select_RunRecord_MinUse]')
     and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
 drop procedure usp_mining_Select_RunRecord_MinUse
GO

 create proc usp_mining_Select_RunRecord_MinUse

 as
  declare @min_runId int;
  declare @min_runId_next int;
  declare @max_runId int;
  
  declare @min_forumId int,@min_forumId_next int;
  declare @min_newsId int,@min_newsId_next int;
  declare @min_blogId int,@min_blogId_next int;
  declare @min_microblogId int,@min_microblogId_next int;
  
  select @min_runId = min(RunId),@max_runId = max(RunId) from RunRecord where UseStatus is null 
  set @min_runId_next=@min_runId+1;
  
  select @min_forumId = MaxForumId,@min_newsId = MaxNewsId ,
    @min_blogId = MaxBlogId,@min_microblogId = MaxMicroBlogId
    from dbo.RunRecord where RunId = @min_runId
  
   select @min_forumId_next = MaxForumId,@min_newsId_next = MaxNewsId ,
    @min_blogId_next = MaxBlogId,@min_microblogId_next = MaxMicroBlogId
    from dbo.RunRecord where RunId = @min_runId_next                        
                        
  
  select @min_runId as min_runId,@min_runId_next as min_runId_next,@max_runId as max_runId,
	@min_forumId as min_forumId,@min_forumId_next as min_forumId_next,
	@min_newsId as min_newsId,@min_newsId_next as min_newsId_next,
	@min_blogId as min_blogId,@min_blogId_next as min_blogId_next,
	@min_microblogId as min_microblogId,@min_microblogId_next as min_microblogId_next
     
 go
  