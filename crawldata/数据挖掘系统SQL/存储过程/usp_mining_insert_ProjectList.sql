if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_Insert_ProjectList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_Insert_ProjectList
GO
create proc usp_mining_Insert_ProjectList
@projectname nvarchar(200),
@matchingruletype int,
@matchingrule nvarchar(200),
@rsskey varchar(50),
@empid nvarchar(50),
@createdate datetime,
@enddate datetime
as
  declare @ruanCount int
  select @ruanCount = count(1) from projectlist where Runstatus = 1 and enddate>getdate()
     if(@ruanCount<20)
       begin
           insert into ProjectList(ProjectName,MatchingRuleType,MatchingRule,RssKey,EmpId,CreateDate,EndDate,RunStatus)
           values(@projectname,@matchingruletype,@matchingrule,@rsskey,@empid,@createdate,@enddate,1)
             select SCOPE_IDENTITY()
       end
     else
        begin
           select '-1';
        end
     
go

