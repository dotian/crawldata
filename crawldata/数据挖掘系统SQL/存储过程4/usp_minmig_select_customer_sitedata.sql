if exists (select * from dbo.sysobjects where id = object_id(N'[usp_minmig_select_customer_sitedata]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_minmig_select_customer_sitedata
GO
create proc usp_minmig_select_customer_sitedata
@projectid int,
@start varchar(50), --开始时间
@end varchar(50),  --结束时间
@datatype varchar(50), --数据类型 news 还是 sitedata
@pageIndex int,
@pagesize int, 
@file1 int,  --查询内容
@file2 int,
@file3 varchar(50), --搜索关键字
@sitetype int,
@analysis int,
@attention int,
@showstatus int
as
   declare @sql varchar(2000),@select_table varchar(500),@where varchar(1000);
   declare @startIndex int,@endIndex int
   
  set @sql = '';
  set @select_table='';
  set @where = '';
   set @startIndex = @pagesize*(@pageIndex-1)+1;
   set @endIndex = @pagesize*@pageIndex ;
   
     set @select_table =' select row_number() over(order by SD_Id desc) as RowId,SD_Id as Id,Title,Content,ContentDate,SrcUrl,ProjectId,SiteType,SiteName,Analysis,Tag1,Tag2,Tag3,Smessage from sitedata where 1=1 and projectId ='+cast(@projectid as varchar(10))
    if(@showstatus=0)
      begin
         set @where = @where+ ' and (ShowStatus=1 or ShowStatus=2)'
      end
    else
       begin
         set @where = @where+ ' and ShowStatus='+cast(@showstatus as varchar(10))
       end
     if(@attention=1)   
       begin
         set @where = @where+ ' and Attention=1'
      end
    
   set @where = @where+   ' and SiteType = '+cast(@sitetype as varchar(10))
     
       
    if(@analysis>0)
       begin
           set @where = @where+  ' and Analysis ='+cast(@analysis as varchar(10)) 
       end
    if(len(@start)>0)
       begin
          set @where = @where+  ' and ContentDate  >= '''+@start+''''
       end   
    if(len(@end)>0)   
       begin
          set @where = @where+  ' and ContentDate  <= '''+@end+''''
       end
    
    if(len(@file3)>0)
      begin
           if(@file1=1)  --标题+内容
			 begin
		        set @where = @where+ ' and (Title like ''%'+@file3+'%'' or Content like ''%'+@file3+'%'')'
			 end
		   else if(@file1=2)  -- 媒体名
			 begin
		        set @where = @where+  ' and SiteName like ''%'+@file3+'%'''
			 end
		   else if(@file1=3)  --标签
			 begin
		        set @where = @where+  ' and (Tag1 like ''%'+@file3+'%'' or Tag2 like ''%'+@file3+'%'' or tag3 like ''%'+@file3+'%'')'
			 end
      end 
   
    set @sql = 'select * from (' +@select_table +@where+') as newTb where RowId  between '+cast(@startIndex as varchar(10)) +' and ' +cast(@endIndex as varchar(10)) 
    print(@sql)
    exec(@sql)
go

-----------------------------------------------------------------------------------------------

--exec usp_minmig_select_customer_sitedata 4,'2012-12-14','2014-12-27','sitedata','1','20','','1','','1','0','0','0'

--select * from sitedata



------------------------------------------------------------------------------------------------------------------------
if exists (select * from dbo.sysobjects where id = object_id(N'[usp_minmig_select_customer_sitedataCount]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_minmig_select_customer_sitedataCount
GO
create proc usp_minmig_select_customer_sitedataCount
@projectid int,
@start varchar(50), --开始时间
@end varchar(50),  --结束时间
@datatype varchar(50), --数据类型 news 还是 sitedata
@file1 int,  --查询内容
@file2 int,
@file3 varchar(50), --搜索关键字
@sitetype int,
@analysis int,
@attention int,
@showstatus int
as
   declare @sql varchar(2000),@select_table varchar(500),@where varchar(1000);
  
  set @sql = '';
  set @select_table='';
  set @where = '';
     set @select_table =' select count(1) from sitedata where 1=1 and projectId ='+cast(@projectid as varchar(10))
     if(@showstatus=0)
      begin
         set @where = @where+ ' and (ShowStatus=1 or ShowStatus=2)'
      end
    else
       begin
         set @where = @where+ ' and ShowStatus='+cast(@showstatus as varchar(10))
       end
     if(@attention=1)   
       begin
         set @where = @where+ ' and Attention=1'
      end
   
   set @where = @where+  ' and SiteType = '+cast(@sitetype as varchar(10))
      
    if(@analysis>0)
       begin
           set @where = @where+  ' and Analysis ='+cast(@analysis as varchar(10)) 
       end
       
    if(len(@start)>0)
       begin
          set @where = @where+  ' and ContentDate  >= '''+@start+''''
       end   
    if(len(@end)>0)   
       begin
          set @where = @where+  ' and ContentDate  <= '''+@end+''''
       end
    
    if(len(@file3)>0)
      begin
           if(@file1=1)  --标题+内容
			 begin
		        set @where = @where+ ' and (Title like ''%'+@file3+'%'' or Content like ''%'+@file3+'%'')'
			 end
		   else if(@file1=2)  -- 媒体名
			 begin
		        set @where = @where+  ' and SiteName like ''%'+@file3+'%'''
			 end
		   else if(@file1=3)  --标签
			 begin
		        set @where = @where+  ' and (Tag1 like ''%'+@file3+'%'' or Tag2 like ''%'+@file3+'%'' or tag3 like ''%'+@file3+'%'')'
			 end
      end 
   
    set @sql = @select_table + @where
    print(@sql)
    exec(@sql)
go





-------------------------------------------------------------------------------------------------------------



---------------------------------------------------------------------------------------------------------



if exists (select * from dbo.sysobjects where id = object_id(N'[usp_minmig_select_customer_news]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_minmig_select_customer_news
GO
create proc usp_minmig_select_customer_news
@start varchar(50), --开始时间
@end varchar(50),  --结束时间
@pageIndex int,
@pagesize int, 
@file1 int,  --查询内容
@file2 int,
@file3 varchar(50), --搜索关键字
@analysis int,
@keys varchar(50)
as
   declare @sql varchar(2000),@select_table varchar(500),@where varchar(1000);
   declare @startIndex int,@endIndex int
   
  set @sql = '';
  set @select_table='';
  set @where = '';
   set @startIndex = @pagesize*(@pageIndex-1)+1;
   set @endIndex = @pagesize*@pageIndex ;
   
     set @select_table =' select row_number() over(order by id desc) as RowId,id ,title,content,url,analysis,tags,published,contend,[type],sitename,message from hankook where contend ='''+ @keys+''''

    if(@analysis>0)
       begin
           set @where = @where+  ' and Analysis = '+cast(@analysis as varchar(10)) 
       end
    if(len(@start)>0)
       begin
          set @where = @where+  ' and published  >= '''+@start+''''
       end   
    if(len(@end)>0)   
       begin
          set @where = @where+  ' and published  <= '''+@end+''''
       end
    
    if(len(@file3)>0)
      begin
           if(@file1=1)  --标题+内容
			 begin
		        set @where = @where+ ' and (title like ''%'+@file3+'%'' or content like ''%'+@file3+'%'')'
			 end
		   else if(@file1=2)  -- 媒体名
			 begin
		        set @where = @where+  ' and type like ''%'+@file3+'%'''
			 end
		   else if(@file1=3)  --标签
			 begin
		        set @where = @where+  ' and tags like ''%'+@file3+'%'''
			 end
      end 
    set @sql = 'select * from (' +@select_table +@where+') as newTb where RowId  between '+cast(@startIndex as varchar(10)) +' and ' +cast(@endIndex as varchar(10)) 
    print(@sql)
    exec(@sql)
  
go


-----------------------------------------------------------------------------------------------------------


-----------------------------------------------------------------------------------------------------------

if exists (select * from dbo.sysobjects where id = object_id(N'[usp_minmig_update_customer_sitedata_mess]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_minmig_update_customer_sitedata_mess
GO
create proc usp_minmig_update_customer_sitedata_mess
@id int,
@mess varchar(500)
as
   update  dbo.SiteData set Smessage = @mess where SD_Id  = @id
   select '1'
go


--------------------------------------------------------------------------------------------------------------------
if exists (select * from dbo.sysobjects where id = object_id(N'[usp_minmig_update_customer_hankook_mess]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_minmig_update_customer_hankook_mess
GO
create proc usp_minmig_update_customer_hankook_mess
@id int,
@mess varchar(500)
as
   update  hankook set message = @mess where id  = @id
   select '1'
go

--------------------------------------------------------------------------------------------------------------------



--------------------------------------------------------------------------------------------------------------------




if exists (select * from dbo.sysobjects where id = object_id(N'[usp_minmig_select_customer_ByCustomerId]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_minmig_select_customer_ByCustomerId
GO
create proc usp_minmig_select_customer_ByCustomerId
@customerid varchar(50)
as
    select CustomerId,LoginPwd,SecretKey,EmpName,UserPermissions,ProjectId,RunStatus 
      from  CustomerInfo where CustomerId = @customerid
go
--------------------------------------------------------------------------------------------------------------------















