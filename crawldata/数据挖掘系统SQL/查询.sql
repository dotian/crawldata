

select * from dbo.MicroBlog





--�д�����
insert into News(title,content,contentdate,srcurl,siteid,createdate,projectid)
select title,content,ContentDate,SrcUrl,SiteId,CreateDate,ProjectId from DataMiningDB.dbo.News
where Cid>=811063
and title not in (select title from News)

--���� ֱ�ӵ���
insert into forums(title,content,contentdate,Author,[Views],Replies,createdate,srcurl,siteid,projectid)
select  title,content,ContentDate,Author,PageView,Reply,CreateDate,SrcUrl,SiteId,ProjectId from DataMiningDB.dbo.Forum
where Cid>=1007716 


--����ֱ�� ����
insert into MicroBloggings(title,content,contentdate,Author,createdate,srcurl,siteid,projectid)
select  title,content,ContentDate,Author,CreateDate,SrcUrl,SiteId,ProjectId from DataMiningDB.dbo.MicroBlog
where Cid>=1959

--�д�����
insert into blogs(title,content,contentdate,Author,createdate,srcurl,siteid,projectid)
select  title,content,ContentDate,Author,CreateDate,SrcUrl,SiteId,ProjectId from DataMiningDB.dbo.Blog
where Cid>=23056 
and title not in (select title from blogs)

select * from dbo.RunRecord order by runid




insert into News(title,content,contentdate,srcurl,siteid,createdate,projectid) select * from 
OPENROWSET('Microsoft.ACE.OLEDB.12.0','Excel 12.0;HDR=YES;DATABASE=D:\QQ��ʣ�µı�.xlsx',sheet1$)


select * from dbo.ProjectList


select * from ProjectDetail


--ĳһ����Ŀ �� ��̳��,������,������,΢����



select count(1) from Blog where ProjectId = '55'
select count(1) from Forum where ProjectId = '55'
select count(1) from News where ProjectId = '55'
select count(1) from MicroBlog where ProjectId = '55'

select * from Blog


select distinct ProjectId,KeyWords from dbo.ProjectDetail
where KeyWords in( select MatchingRule from ProjectList where ProjectId<100 )
and ProjectType>0


select * from dbo.ProjectList


select * from dbo.ItemProjects










