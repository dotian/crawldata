
if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_Select_ProjectList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_Select_ProjectList
GO
CREATE proc [dbo].usp_mining_Select_ProjectList
@searchType int,
@sarchkey nvarchar(200),
@runStatus int,
@empId varchar(50)
as
  --RunStatus = 1 表示正在运行状态
   declare @execSQl nvarchar(2000)
   --select PL.ProjectId,ProjectName,MatchingRuleType,MatchingRule,
 --EmpId,CreateDate,EndDate,ForumNum,NewsNum,BlogNum,MicroBlogNum from ProjectList PL,GetDataTable() B 
 -- where RunStatus =1 and PL.ProjectId =B.ProjectId
    
    --//@empId
    declare @permission int;
    select @permission =UserPermissions from Employee where EmpId=@empId
    
    set @execSQl = 'select PL.ProjectId,ProjectName,MatchingRuleType,MatchingRule,EmpId,CreateDate,EndDate,'
    set @execSQl = @execSQl+ ' DT.ForumNum,DT.NewsNum,DT.BlogNum,DT.MicroBlogNum from ProjectList PL,GetDataTable() DT '
  
    set @execSQl = @execSQl+ ' where PL.ProjectId = DT.ProjectId and PL.RunStatus>=0 and PL.RunStatus <= '+cast(@runStatus as varchar(10))
	
  if(@permission=1)
	begin
		 --1 是低级用户,2 是高级用户
		  set @execSQl = @execSQl+' and EmpId ='''+@empId +''''
	end
  if (Len(@sarchkey)>0)
    begin
	   if @searchType = 0
		 begin
		   set @execSQl = @execSQl+' and ProjectName like ''%'+@sarchkey+'%'''
		 end
	   else   
		 begin
		   set @execSQl = @execSQl+' and MatchingRule like ''%'+@sarchkey+'%'''
		 end
    end
    
    print @execSQl
   exec(@execSQl) 
GO

--select * from GetDataTable()
--exec usp_mining_Select_ProjectList 0,'',1,'git000'
--select * from GetDataTable()

















