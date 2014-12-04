if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_Select_ProjectListDetilByProjectId]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_Select_ProjectListDetilByProjectId
GO
create proc usp_mining_Select_ProjectListDetilByProjectId
@projectId int
as
  declare  @forumnum int,
           @newsnum int,
           @blognum int,
           @microblognum int,
           @empname nvarchar(50),
           @MatchingRuleType int,
           @matchingtypename nvarchar(20),
           @matchingRule nvarchar(200),
           @empid nvarchar(50),
           @createdate datetime,
           @enddate datetime,
           @projectname nvarchar(200),
           @runStatus int
           --当前项目的 论坛站点数量
      
    --准备变量数据
    
     select @projectname = ProjectName,@MatchingRuleType =MatchingRuleType ,@matchingRule =MatchingRule, 
        @empid = Employee.EmpName,@createdate =PL.CreateDate,@enddate = PL.EndDate,@runStatus = PL.RunStatus  
        from ProjectList PL,Employee 
        where PL.EmpId = Employee.EmpId and ProjectId = @projectId 
			 --select ProjectName ,MatchingRuleType,MatchingRule,Employee.EmpName,PL.CreateDate,PL.EndDate,PL.RunStatus
			 --from ProjectList PL,Employee 
			 --       where PL.EmpId = Employee.EmpId and ProjectId = 218 
			 

 
 if(@runStatus=2)
   begin
    select '' as ProjectName,'' as MatchngTypeName,'' as EmpId,'' as CreateDate,'' as EndDate ,
        '' as ForumNum,'' as NewsNum,'' as BlogNum,'' as MicroBlogNum
     return 
   end
 else
   begin
	select @forumnum = count(PD.SiteId) from ProjectDetail PD,SiteList SL where PD.SiteId = SL.SiteId and SL.SiteType = 1 and  PD.ProjectId = @projectId  
	select @newsnum =count(PD.SiteId) from ProjectDetail PD,SiteList SL where PD.SiteId = SL.SiteId and SL.SiteType = 2 and  PD.ProjectId = @projectId 
	select @blognum = count(PD.SiteId) from ProjectDetail PD,SiteList SL where PD.SiteId = SL.SiteId and SL.SiteType = 3 and  PD.ProjectId = @projectId 
	select @microblognum =count(PD.SiteId) from ProjectDetail PD,SiteList SL where PD.SiteId = SL.SiteId and SL.SiteType = 5 and  PD.ProjectId = @projectId 
   
	select @matchingtypename = case @MatchingRuleType 
		when 0 then '标题及内容' 
		when 1 then '标题'
		when 2 then '关键字' 
		else '标题及内容' 
    end 
 
       select @projectname as ProjectName,@matchingtypename as MatchngTypeName,@matchingRule as MatchingRule, @empid as EmpId,@createdate as CreateDate,
         @enddate as EndDate,@forumnum as ForumNum,@newsnum as NewsNum,@blognum as BlogNum,@microblognum as MicroBlogNum
        
    end
go