 if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_insert_SiteData_News]')
     and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
 drop procedure usp_mining_insert_SiteData_News
GO
 create proc usp_mining_insert_SiteData_News
  @projectId int,
  @whereStr_or nvarchar(800),
  @whereStr_and  nvarchar(400),
  @whereStr_not nvarchar(400),
  @min_newsId int,
  @min_newsId_next int
 as
     declare @sitetype int;
     set @sitetype = 2; --news
    declare @assort varchar(100);
    set @assort = '';
    IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[table_news_init]') AND type in (N'U'))
      DROP TABLE [dbo].table_news_init
     create TABLE table_news_init
     (
       [cid] int 
     )
    
      declare @sql nvarchar(1200);
     set @sql = 'insert into table_news_init([cid]) '
     
     set @sql = @sql+' select Cid from News where '
      set @sql = @sql +' Cid between '+cast(@min_newsId as varchar(15))  +' and ' + cast(@min_newsId_next as varchar(15)) 
      set @sql = @sql+ ' and ProjectId ='+cast(@projectId as varchar(15))
     set @sql = @sql+ ' and UseStatus is null and ('+@whereStr_or +')'
     if	(len(@whereStr_and)>0)
      begin
          set @sql = @sql+ ' and ' + @whereStr_and
      end
     if	(len(@whereStr_not)>0)
      begin
        Print '@whereStr_not>0 为true'
        set @sql= @sql + ' and Cid not in(select Cid from News where  UseStatus is null and '+@whereStr_not+')'
      end
    
    
    print @sql
   
    
    exec(@sql)--数据填充到临时表
    
    declare @cid int,@sitename varchar(50);
   
      declare @sd_id_identity int;
      declare @siteid int;
      
    declare cid_cur scroll cursor for
       select cid from table_news_init
     open cid_cur
       fetch first from cid_cur into @cid
    
   while(@@fetch_status = 0)
	begin
	  
	     select @siteid = SiteId from News where Cid =@cid
	     
		  insert into SiteData (Cid, Title,Content, ContentDate, SrcUrl, SiteId, ProjectId, CreateDate)
			select Cid,Title,Content,ContentDate,SrcUrl,SiteId,ProjectId,CreateDate from News where Cid =@cid
			
			select @sd_id_identity = scope_identity()
            
            select @sitename = SiteName from  dbo.SiteList where SiteId = @siteid
            update SiteData set SiteType =@sitetype,SiteName = @sitename,Analysis = 0,
                  ShowStatus = 0,Assort = @assort,Attention=0,Hot=0
               where SD_Id = @sd_id_identity
             
	    update News set UseStatus = 0 where Cid =@cid  --更新状态,表示已经使用过了
		fetch next from cid_cur into @cid
	end

	close cid_cur
	deallocate cid_cur
    
 go 
 

/*
 exec usp_mining_insert_SiteData_News
   1002,' Title like ''%购买%'' or Title like ''%iphine%''','','',0,591929
   
   
   select * from news where projectid = 1002
    
 
-- select * from forum
 ------------------------------------------------------------------
591735
 
 
select Cid from News where  Cid between 0 and 591735 and ProjectId =1001 
 and UseStatus is null and (Title like '%苹果%' or Title like '%iphone%')


select * from 
 
 select Cid from News where  Cid between 0 and 591735 and ProjectId =1001 and
  UseStatus is null and 
  (Title like '%苹果%' or Title like '%iphone%')

 */
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 