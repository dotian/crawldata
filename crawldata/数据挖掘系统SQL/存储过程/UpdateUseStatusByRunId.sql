 use DataMiningDB
 go
 if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_Update_UseStatusByRunId]')
     and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
 drop procedure usp_mining_Update_UseStatusByRunId
GO

 create proc usp_mining_Update_UseStatusByRunId

 as
  declare @min_runId int;
  declare @min_runId_next int;
  declare @max_runId int;
  
  declare @min_formId int,@min_formId_next int;
  declare @min_newsId int,@min_newsId_next int;
  declare @min_blogId int,@min_blogId_next int;
  declare @min_microblogId int,@min_microblogId_next int;
  
  select @min_runId = min(RunId),@max_runId = max(RunId) from RunRecord where UseStatus is null 
  set @min_runId_next=@min_runId+1;
  
  if(@min_runId_next>@max_runid-2)
     return;
  
  --print @min_runId
  --print @min_runId_next
  --print @max_runid
  
  --得到 i
  
  select @min_formId = MaxForumId,@min_newsId = MaxNewsId ,
    @min_blogId = MaxBlogId,@min_microblogId = MaxMicroBlogId
    from dbo.RunRecord where RunId = @min_runId
  
   select @min_formId_next = MaxForumId,@min_newsId_next = MaxNewsId ,
    @min_blogId_next = MaxBlogId,@min_microblogId_next = MaxMicroBlogId
    from dbo.RunRecord where RunId = @min_runId_next

  --更新 新闻,论坛,博客,微博
	declare @execSQl varchar(300);
	
	set @execSQl = 'update Forum set UseStatus = 1 where UseStatus is null and Cid between '+ cast(@min_formId as varchar(15))  
	set @execSQl = @execSQl + ' and ' + cast(@min_formId_next as varchar(15)) 
       exec(@execSQl); --更新论坛

    set @execSQl = 'update News set UseStatus = 1 where UseStatus is null and Cid between '+ cast(@min_newsId as varchar(15))  
	set @execSQl = @execSQl + ' and ' + cast(@min_newsId_next as varchar(15)) 
	   exec(@execSQl);--更新新闻
	   
	set @execSQl = 'update Blog set UseStatus = 1 where UseStatus is null and Cid between '+ cast(@min_blogId as varchar(15))  
	set @execSQl = @execSQl + ' and ' + cast(@min_blogId_next as varchar(15)) 
	   exec(@execSQl);--更新博客
	
    set @execSQl = 'update MicroBlog set UseStatus = 1 where UseStatus is null and Cid between '+ cast(@min_microblogId as varchar(15))  
	set @execSQl = @execSQl + ' and ' + cast(@min_microblogId_next as varchar(15)) 
	   exec(@execSQl);--更新微博
	  
	 update  RunRecord  set UseStatus = 0 where RunId = @min_runId
  go
  


----------------------------------------------------------------------------------------------------



  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  