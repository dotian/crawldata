if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_select_forum_RecordCount]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_select_forum_RecordCount
GO
create proc usp_mining_select_forum_RecordCount
@parms_date1 varchar(15),
@parms_date2 varchar(15),
@parms_url varchar(500),
@parms_matchType varchar(5),
@parms_matchRule varchar(10)
as
  declare @sqlExe nvarchar(2000) 
   set @sqlExe = 'select count(1) from forum where 1=1' 
  
    if(@parms_date1!='')
		set @sqlExe = @sqlExe+ ' and CreateDate> '''+@parms_date1 +''''
	if(@parms_date2!='')
		set @sqlExe = @sqlExe+ ' and CreateDate< '''+@parms_date2 +''''
    if(@parms_url!='')	
        set @sqlExe = @sqlExe+ ' and  SrcUrl ='+@parms_url +''''
   if(@parms_matchRule!='0' and @parms_matchRule!='')
      begin
          if(@parms_matchType = 1)
             set @sqlExe = @sqlExe+ ' and  Title like ''%'+@parms_matchRule+'%'''
          if(@parms_matchType = 2)
             set @sqlExe = @sqlExe+ ' and  Content like ''%'+@parms_matchRule+'%'''
       end
  print @sqlExe
  exec(@sqlExe)
go