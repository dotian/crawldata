 if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_insert_SiteData_MicroBlog]')
     and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
 drop procedure usp_mining_insert_SiteData_MicroBlog
GO
 create proc usp_mining_insert_SiteData_MicroBlog
  @projectId int,
  @whereStr_or nvarchar(800),
  @whereStr_and  nvarchar(400),
  @whereStr_not nvarchar(400),
  @min_microblogId int,
  @min_microblogId_next int
 as
     declare @sitetype int;
     set @sitetype = 5; --microblog
    declare @assort varchar(100);
    set @assort = '';
    IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[table_microblog_init]') AND type in (N'U'))
      DROP TABLE [dbo].table_microblog_init
     create TABLE table_microblog_init
     (
       [cid] int 
     )
    
     declare @sql nvarchar(1200);
     set @sql = 'insert into table_microblog_init([cid]) '
     
     set @sql = @sql+' select Cid from MicroBlog where '
      set @sql = @sql +' Cid between '+cast(@min_microblogId as varchar(15))  +' and ' + cast(@min_microblogId_next as varchar(15)) 
      set @sql = @sql+ ' and ProjectId ='+cast(@projectId as varchar(15))
     set @sql = @sql+ ' and UseStatus is null and ('+@whereStr_or +')'
     if	(len(@whereStr_and)>0)
      begin
          set @sql = @sql+ ' and ' + @whereStr_and
      end
     if	(len(@whereStr_not)>0)
      begin
        Print '@whereStr_not>0 为true'
        set @sql= @sql + ' and Cid not in(select Cid from MicroBlog where  UseStatus is null and '+@whereStr_not+')'
      end
     
    print @sql
    
    exec(@sql)--数据填充到临时表
    
    declare @cid int,@sitename varchar(50);
   
      declare @sd_id_identity int;
      declare @siteid int;
      
    declare cid_cur scroll cursor for
       select cid from table_microblog_init  
     open cid_cur
       fetch first from cid_cur into @cid
    
   while(@@fetch_status = 0)
	begin
	  
	     select @siteid = SiteId from MicroBlog where Cid =@cid
	     
		  insert into SiteData (Cid, Title,Content, ContentDate, SrcUrl, SiteId, ProjectId, CreateDate)
			select Cid,Title,Content,ContentDate,SrcUrl,SiteId,ProjectId,CreateDate from MicroBlog where Cid =@cid
			
			select @sd_id_identity = scope_identity()
            
            select @sitename = SiteName from  dbo.SiteList where SiteId = @siteid
            update dbo.SiteData set SiteType =@sitetype,SiteName = @sitename,Analysis = 0,
                  ShowStatus = 0,Assort = @assort,Attention=0,Hot=0
               where SD_Id = @sd_id_identity
             
	    update MicroBlog set UseStatus = 0 where Cid =@cid  --更新状态,0表示使用
		fetch next from cid_cur into @cid
	end

	close cid_cur
	deallocate cid_cur
    
 go 
 
 
 ----------------------------------------------------------------------
 
  
  
  
  
  
  
  