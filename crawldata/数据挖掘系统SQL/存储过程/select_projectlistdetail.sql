
if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_select_projectlistdetail]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_select_projectlistdetail
GO

Create proc usp_mining_select_projectlistdetail
as

 if exists (select * from sysobjects where id = OBJECT_ID('tb_projectlistdetail') and OBJECTPROPERTY(id, 'IsUserTable') = 1)  
 DROP TABLE tb_projectlistdetail;
 
  create table tb_projectlistdetail
	(
	  ProjectId int,
	  ProjectName nvarchar(200),
	  MatchingTypeName nvarchar(50),
	  MatchingRule nvarchar(200),
	  EmpName nvarchar(50),
	  CreateDate datetime,
	  EndDate datetime,
	  ForumNum int,
	  NewsNum int,
	  BlogNum int,
	  MicroBlogNum int
	)		

declare @forumnum int,@newsnum int,@blognum int,@microblognum int,@empname nvarchar(50),
 @ProjectName varchar(200),@MatchingRuleType int,@MatchingRule varchar(200),@CreateDate datetime,@EndDate datetime;


declare @projectid int,@empid varchar(50),@matchingtypename varchar(50);
--��ʼ�����α�
  declare pcurr cursor for 
    select distinct ProjectId,ProjectName,MatchingRuleType,MatchingRule,EmpId,CreateDate,EndDate from ProjectList where RunStatus = 1 
	  open pcurr
	   fetch next from pcurr into @projectid,@ProjectName,@MatchingRuleType,@MatchingRule,@empid,@CreateDate,@EndDate
		  while (@@FETCH_STATUS = 0)
			begin
			   --׼����������
				select @forumnum = count(1) from SiteData where ProjectId = @projectid and SiteType = 1
				select @newsnum =count(1) from SiteData where ProjectId = @projectid and SiteType = 2
				select @blognum = count(1) from SiteData where ProjectId = @projectid and SiteType = 3
				select @microblognum =count(1) from SiteData where ProjectId = @projectid and SiteType = 5
				select @empname = EmpName from Employee where empid = @empid
				
				select @matchingtypename = case @MatchingRuleType 
					when 0 then '���⼰����' 
					when 1 then '����'
					when 2 then '�ؼ���' 
					else '���⼰����' 
                end 
				  --������ʱ��
				
				insert into tb_projectlistdetail(ProjectId,ProjectName,MatchingTypeName,MatchingRule,EmpName,CreateDate,EndDate,ForumNum,NewsNum,BlogNum,MicroBlogNum)
				values(@projectid,@ProjectName,@matchingtypename,@MatchingRule,@empname,@CreateDate,@EndDate,@forumnum,@newsnum,@blognum,@microblognum)
				
				fetch next from pcurr into @projectid,@ProjectName,@MatchingRuleType,@MatchingRule,@empid,@CreateDate,@EndDate
			end
	  close pcurr
 deallocate pcurr
--�α꺯������

select * from tb_projectlistdetail
DROP TABLE tb_projectlistdetail;
go


--exec usp_mining_select_projectlistdetail

select SiteType from dbo.SiteData