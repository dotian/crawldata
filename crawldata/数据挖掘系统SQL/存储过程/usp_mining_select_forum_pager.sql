if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_select_forum_pager]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_select_forum_pager
GO
create proc usp_mining_select_forum_pager
@pagesize int,
@pageindex int,
@parms_date1 varchar(15),
@parms_date2 varchar(15),
@parms_url varchar(500),
@parms_matchType varchar(5),
@parms_matchRule varchar(10)

as
  declare @startindex int,@sqlExe nvarchar(2000) 
  set @startindex = (@pageindex-1)*@pagesize

   set @sqlExe = 'select top('+cast(@pagesize as varchar(10))+') Title,Author,SrcUrl,PageView,Reply,CreateDate  from forum where cid not in (select top('+cast(@startindex as varchar(10))+') cid from forum order by cid asc) ' 
  
    if(@parms_date1!='')
		set @sqlExe = @sqlExe+ ' and CreateDate>= '''+@parms_date1 +'00:00:00'''
	if(@parms_date2!='')
		set @sqlExe = @sqlExe+ ' and CreateDate<= '''+@parms_date2 +' 23:59:59'''
    if(@parms_url!='')	
        set @sqlExe = @sqlExe+ ' and  SrcUrl ='+@parms_url +''''
   if(@parms_matchRule!='0' and @parms_matchRule!='')
      begin
          if(@parms_matchType = 1)
             set @sqlExe = @sqlExe+ ' and  Title like ''%'+@parms_matchRule+'%'''
          if(@parms_matchType = 2)
             set @sqlExe = @sqlExe+ ' and  Content like ''%'+@parms_matchRule+'%'''
       end
    	
 set @sqlExe = @sqlExe+ ' order by cid asc'
  print @sqlExe
  exec(@sqlExe)
 
go