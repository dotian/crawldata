
use DataMiningDB
go

select * from dbo.ProjectList


select * from ProjectDetail 

select * from dbo.ProjectDetail where ProjectId = '1009'
 
 --delete from ProjectList where ProjectId = 1007


select * from dbo.ProjectCate





select * from dbo.ProjectDetail 
 
 --update ProjectDetail set RunStatus = 99 where ProjectId <1009


select * from dbo.ProjectList

 --璁哄潧甯告姄椤圭洰 ,鎶撴墍鏈夌殑鏁版嵁
 
 
 
 SiteUrl
http://tieba.baidu.com/f?kw=鏃跺皻
 
 select * from dbo.SiteList where  SiteUrl not like '%<>%'
  and( sitetype <0  or sitetype>5)
 
 
  select * from dbo.SiteList where  SiteUrl  like '%<>%'
 

 select * from dbo.ProjectList

update ProjectList set runstatus = 1 where EmpId = 'admin001'

 select * from dbo.RunRecord
 
 select * from dbo.ProjectDetail
 
select * from dbo.Forum
-----------------------------
 select * from dbo.News
 select * from dbo.Blog

---------------------------
 select count(1) from dbo.News
 select count(1) from dbo.Blog
 

 select * from runrecord
 
 exec usp_Spider_Select_TaskTarget 1
 
 
 
 
 
 
 
 select * from dbo.ProjectCate
 
 
 select * from ProjectCate
 select * from dbo.CategoryInfo
 select * from dbo.CategorySiteRelation
 
--------------------------------------------------------
 
 select siteid,tid from sitelist where siteid in
 (
   select siteid from CategorySiteRelation
 )
 
 
 
 
 
 --内容要修改一下
 select * from  dbo.Template where Tid In
 (
	 select tid from sitelist where siteid in
	 (
	    select siteid from CategorySiteRelation
	 )
 )
 
 	 select * from sitelist where siteid in
	 (
	    select siteid from CategorySiteRelation
	 )
	 

 select * from  dbo.ProjectList
 
  update ProjectDetail set KeyWords = '三星+死机+字库门' where projectId = 1009
 
 select * from Template where tid = 766
 
 MatchingRule
三星字库门+三星死机+三星手机


 declare @a varchar(2000)
 
 select @a = T_content from dbo.ContentTB
 update Template set  TemplateContent = @a
  where tid = 766
 
 
 select T_content from dbo.ContentTB
 
 

 
 delete from ContentTB
 
 
 
 
 select * from blog order by cid desc
 
 
 select * from 
 
 
 select * from dbo.Template
 
 select * from sitelist where tid = 766
  
  update SiteList set ContentEncoding  = 'UTF-8'
 where siteid = 88036


select * from dbo.SiteList
 
 select * into SiteList from dbo.SiteList_10_10_back
 
 
 
 select * from sitelist where tid = '766'
 update sitelist set SiteType = 3 where tid = 766 and sitetype = 1 and platename = '新浪博客'
 
 
 SiteType
1
 
 
 
 if (object_id('tgr_MicroBlog_insert', 'tr') is not null)
    drop trigger tgr_MicroBlog_insert
go
create trigger tgr_MicroBlog_insert
on MicroBlog
    instead of insert --插入之前触发
as
    --定义变量
    declare @title varchar(200) , @srcrl varchar(500),@siteid int;
    --在inserted表中查询已经插入记录信息
  
    select @title = Title, @srcrl = SrcUrl,@siteid = SiteId from inserted;
  
    if exists(select cid from MicroBlog where Title =  @title and SrcUrl = @srcrl and SiteId = @siteid )
       begin
           print '插入失败,已存在相同记录';
       end   
    else 
        begin
            insert into MicroBlog(title,content,contentdate,author,srcurl,siteid,projectid,createdate)
            select title,content,contentdate,author,srcurl,siteid,projectid,createdate from inserted
          print '插入成功';
       end
go








select * from dbo.Blog

insert into Blog(Title,SrcUrl,SiteId)
values('瞎扯蛋','http://blog.qq.com/redirect.htm?http://user.qzone.qq.com/874233233','750261')
--插入数据
insert into TTestTriget values('4','张三1','4apple');

select * from TTestTriget
 
 select * from TTestTriget
 
 if  exists(select id from TTestTriget where id = 15)
      begin
           print '插入失败,已存在相同记录';
       end   
      
    else 
        begin
          --insert into TTestTriget(Id,strname,apple) values(@id,@strname,@apple);
          print '插入成功';
       end
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 