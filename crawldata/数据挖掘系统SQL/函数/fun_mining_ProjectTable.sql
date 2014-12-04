
alter function GetDataTable()
  returns @tb_projectlistdetail table 
	(
	  ProjectId int,
	  ForumNum int,
	  NewsNum int,
	  BlogNum int,
	  MicroBlogNum int
	)
as
  begin
declare @forumnum int,@newsnum int,@blognum int,@microblognum int,@projectid int;
 
  declare pcurr cursor for 
    select distinct ProjectId from ProjectList where RunStatus = 1 or RunStatus = 0
	  open pcurr
	   fetch next from pcurr into @projectid
		  while (@@FETCH_STATUS = 0)
			begin
				select @forumnum = count(1) from SiteData where ProjectId = @projectid and SiteType = 1
				select @newsnum =count(1) from SiteData where ProjectId = @projectid and SiteType = 2
				select @blognum = count(1) from SiteData where ProjectId = @projectid and SiteType = 3
				select @microblognum =count(1) from SiteData where ProjectId = @projectid and SiteType = 5
				
				insert into @tb_projectlistdetail 
				--@tb_projectlistdetail(ProjectId,ForumNum,NewsNum,BlogNum,MicroBlogNum)
				values(@projectid,@forumnum,@newsnum,@blognum,@microblognum)
				fetch next from pcurr into @projectid
			end
	  close pcurr
 deallocate pcurr

return
 end
go
---------------------------------------------------------------------------------------------------------------



 
 
 
 
 
 
 
 
 
 
 
 
 
 