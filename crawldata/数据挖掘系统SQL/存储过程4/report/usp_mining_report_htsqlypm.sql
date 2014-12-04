------------------------------------------------------------------------------------
if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_report_htsqlypm]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_report_htsqlypm
GO
create proc usp_mining_report_htsqlypm
@startdate varchar(20),
@enddate varchar(20),
@projectId int
as
--话题社区来源排名

declare @appearCount decimal(10,2);

select @appearCount = count(1)   FROM SiteData   where projectId = @projectId and sitetype = 1 and  ContentDate between @startdate and @enddate


if(@appearCount=0)
 set @appearCount = 1.00;

 select top(10) row_number() over(order by AppearCount desc ) as Id, * from (
   select SiteName as MessTitle,Count(1) as AppearCount, (COUNT(1)/@appearCount) as AppearRate  from SiteData  
      where projectId = @projectId and sitetype = 1 and  ContentDate between @startdate and @enddate
      group by SiteName     
 ) AS B  order by AppearCount desc

go


--exec usp_mining_report_htsqlypm '2012-12-1','2014-12-1','4'

------------------------------------------------------------------------------------
if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_report_fmhtsqlypm]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_report_fmhtsqlypm
GO
create proc usp_mining_report_fmhtsqlypm
@startdate varchar(20),
@enddate varchar(20),
@projectId int
as

declare @appearCount decimal(10,2);

select @appearCount = count(1) FROM SiteData   where projectId = @projectId and sitetype = 1 and Analysis = 3 and  ContentDate between @startdate and @enddate

if(@appearCount=0)
 set @appearCount = 1.00;

--负面 话题社区来源排名
 select top(10) row_number() over(order by AppearCount desc ) as Id, * from (
   select SiteName as MessTitle,Count(1) as AppearCount,(COUNT(1)/@appearCount) as AppearRate  from SiteData  
      where projectId = @projectId and sitetype = 1 and Analysis = 3 and  ContentDate between @startdate and @enddate
      group by SiteName     
 ) AS B  order by AppearCount desc

go


--exec usp_mining_report_fmhtsqlypm '2012-12-1','2014-12-1','4'
------------------------------------------------------------------------------------

if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_report_cfhtpm]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_report_cfhtpm
GO
create proc usp_mining_report_cfhtpm
@startdate varchar(20),
@enddate varchar(20),
@projectId int
as
--重复话题排名
select top(10) row_number() over(order by AppearCount desc) as Id,* from (
SELECT Title as MessTitle, COUNT(1) AS AppearCount
 FROM SiteData  where projectId = @projectId and sitetype = 1  and  ContentDate between @startdate and @enddate
 GROUP BY Title
 HAVING (COUNT(1) > 1) 
 ) as newTB order by AppearCount desc

/*
select top(10) row_number() over(order by NumCount desc) as RowNum,* from (
SELECT Title, COUNT(1) AS NumCount
 FROM SiteData 
 GROUP BY Title
 HAVING (COUNT(1) > 1) 
 ) as newTB order by NumCount desc
*/
go
------------------------------------------------------------------------------------

if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_report_fmcfhtpm]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_report_fmcfhtpm
GO
create proc usp_mining_report_fmcfhtpm
@startdate varchar(20),
@enddate varchar(20),
@projectId int
as
-- 重复话题排名

select top(10) row_number() over(order by AppearCount desc) as Id,* from (
SELECT Title as MessTitle, COUNT(1) AS AppearCount 
 FROM SiteData  where projectId = @projectId and sitetype = 1 and Analysis = 3 and  ContentDate between @startdate and @enddate
 GROUP BY Title
 HAVING (COUNT(1) > 1) 
 ) as newTB order by AppearCount desc

/*
select top(10) row_number() over(order by NumCount desc) as RowNum,* from (
SELECT Title, COUNT(1) AS NumCount
 FROM SiteData 
 GROUP BY Title
 HAVING (COUNT(1) > 1) 
 ) as newTB order by NumCount desc

*/
go

------------------------------------------------------------------------------------





if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_report_gmjslly]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_report_gmjslly
GO
create proc usp_mining_report_gmjslly
@startdate varchar(20),
@enddate varchar(20),
@projectId int
as
-- 各媒介来源 数量

 declare @forumCount int,@newsCount int,@blogCount int,@microblogCount int

select @forumCount = count(1) from SiteData where projectId = @projectId and sitetype = 1 and  ContentDate between @startdate and @enddate
select @newsCount = count(1) from SiteData where projectId = @projectId and sitetype = 2 and  ContentDate between @startdate and @enddate
select @blogCount = count(1) from SiteData where projectId = @projectId and sitetype = 3 and  ContentDate between @startdate and @enddate
select @microblogCount = count(1) from SiteData where projectId = @projectId and sitetype = 5 and  ContentDate between @startdate and @enddate

select @forumCount as Forum,@newsCount as News,@blogCount as Blog,@microblogCount as Microblog

go

------------------------------------------------------------------------------------


if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_report_gdxhtsl]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_report_gdxhtsl
GO
create proc usp_mining_report_gdxhtsl
@startdate varchar(20),
@enddate varchar(20),
@projectId int
as
-- 各调性话题数量

 declare @wu int,@zheng int,@zhong int,@fu int

select @zheng = count(1) from SiteData where projectId = @projectId and Analysis = 1 and  ContentDate between @startdate and @enddate
select @zhong = count(1) from SiteData where projectId = @projectId and Analysis = 2 and  ContentDate between @startdate and @enddate
select @fu = count(1) from SiteData where projectId = @projectId and Analysis = 3 and  ContentDate between @startdate and @enddate
select @wu = count(1) from SiteData where projectId = @projectId and (Analysis = 0 or Analysis=4) and  ContentDate between @startdate and @enddate

select @zheng as Zheng,@zhong as Zhong,@fu as Fu,@wu as WU
go

------------------------------------------------------------------------------------

if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_report_htslqst]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_report_htslqst
GO
create proc usp_mining_report_htslqst
@startdate varchar(20),
@enddate varchar(20),
@projectId int
as
-- 话题声量趋势图

declare @i int, @contentdate datetime,@preparecount int;
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#tab_htslqst]') AND type in (N'U'))
DROP TABLE [dbo].#tab_htslqst
create table #tab_htslqst
(
   ContentDate datetime,
   PrepareCount int
)

set @i = 0;
while(@i<=6)
   begin
     set @contentdate = DATEADD(day,@i-6,@enddate);
      select @preparecount = count(1) from SiteData where projectId = @projectId 
         and  ContentDate between DATEADD(day,@i-6,@enddate) and DATEADD(day,@i-5,@enddate)
      insert into #tab_htslqst(ContentDate,PrepareCount)
      values(@contentdate,@preparecount);
      set @i = @i+1;
   end
 select * from #tab_htslqst
go
------------------------------------------------------------------------------------


--exec usp_mining_report_htslqst '2013-12-22','2014-1-5',4

------------------------------------------------------------------------------------

if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_report_fmhtslqst]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_report_fmhtslqst
GO
create proc usp_mining_report_fmhtslqst
@startdate varchar(20),
@enddate varchar(20),
@projectId int
as
-- 负面话题声量趋势图

declare @i int, @contentdate datetime,@preparecount int;
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#tab_fmhtslqst]') AND type in (N'U'))
DROP TABLE [dbo].#tab_fmhtslqst
create table #tab_fmhtslqst
(
   ContentDate datetime,
   PrepareCount int
)

set @i = 0;
while(@i<=6)
   begin
     set @contentdate = DATEADD(day,@i-6,@enddate);
      select @preparecount = count(1) from SiteData where projectId = @projectId and Analysis = 3
         and  ContentDate between DATEADD(day,@i-6,@enddate) and DATEADD(day,@i-5,@enddate)
         
      insert into #tab_fmhtslqst(ContentDate,PrepareCount)
      values(@contentdate,@preparecount);
      set @i = @i+1;
   end
 select * from #tab_fmhtslqst
go

------------------------------------------------------------------------------------


if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_report_htqsb]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_report_htqsb
GO
create proc usp_mining_report_htqsb
@startdate varchar(20),
@enddate varchar(20),
@projectId int
as
-- 话题趋势表

declare @i int, @contentdate datetime,@preparecount int;
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#tab_htqsb]') AND type in (N'U'))
DROP TABLE [dbo].#tab_htqsb
create table #tab_htqsb
(
   ContentDate datetime,  --内容时间
   News_Z_Num int, --新闻(正)
   News_F_Num int, --新闻(负)
   Blog_Z_Num int, --博客(正)
   Blog_F_Num int, --博客(负)
   Forum_Z_Num int,--论坛(正)
   Forum_F_Num int,--论坛(负)
   Microblog_F_Num int --微博(负)
   
)

declare @news_z_num int,@news_f_num int,@blog_z_num int,@blog_f_num int,
 @forum_z_num int,@forum_f_num int,@microblog_f_num int;
set @i = 0;
while(@i<=9)
   begin
     set @contentdate = DATEADD(day,@i-9,@enddate);
      select @news_z_num = count(1) from SiteData where projectId = @projectId 
         and SiteType = 2 and Analysis = 1
         and  ContentDate between DATEADD(day,@i-9,@enddate) and DATEADD(day,@i-8,@enddate)
       
       select @news_f_num = count(1) from SiteData where projectId = @projectId 
         and SiteType = 2 and Analysis = 3
         and  ContentDate between DATEADD(day,@i-9,@enddate) and DATEADD(day,@i-8,@enddate) 
         
          select @blog_z_num = count(1) from SiteData where projectId = @projectId 
         and SiteType = 3 and Analysis = 1
         and  ContentDate between DATEADD(day,@i-9,@enddate) and DATEADD(day,@i-8,@enddate) 
         
          select @blog_f_num = count(1) from SiteData where projectId = @projectId 
         and SiteType = 3 and Analysis = 3
         and  ContentDate between DATEADD(day,@i-9,@enddate) and DATEADD(day,@i-8,@enddate) 
         
          select @forum_z_num = count(1) from SiteData where projectId = @projectId 
         and SiteType = 1 and Analysis = 1
         and  ContentDate between DATEADD(day,@i-9,@enddate) and DATEADD(day,@i-8,@enddate) 
         
          select @forum_f_num = count(1) from SiteData where projectId = @projectId 
         and SiteType = 1 and Analysis = 3
         and  ContentDate between DATEADD(day,@i-9,@enddate) and DATEADD(day,@i-8,@enddate) 
         
         select @microblog_f_num = count(1) from SiteData where projectId = @projectId 
         and SiteType = 5 and Analysis = 3
         and  ContentDate between DATEADD(day,@i-9,@enddate) and DATEADD(day,@i-8,@enddate) 
         
      insert into #tab_htqsb(ContentDate,News_Z_Num,News_F_Num,Blog_Z_Num,Blog_F_Num,Forum_Z_Num,Forum_F_Num,Microblog_F_Num)
      values(@contentdate,@news_z_num,@news_f_num,@blog_z_num,@blog_f_num,@forum_z_num,@forum_f_num,@microblog_f_num);

      set @i = @i+1;
   end
 select row_number() over(order by ContentDate ASC) as Id,* from #tab_htqsb 
go


exec usp_mining_report_htqsb '','2013-12-31',4






























