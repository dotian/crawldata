 if (object_id('tgr_News_insert', 'tr') is not null)
    drop trigger tgr_News_insert
go
create trigger tgr_News_insert
on News
    instead of insert --插入之前触发
as
    --定义变量
    declare @title varchar(200) , @srcrl varchar(500),@siteid int;
    --在inserted表中查询已经插入记录信息
  
    select  @srcrl = SrcUrl from inserted;
  
    if exists(select cid from News where SrcUrl = @srcrl  )
       begin
           print '插入失败,已存在相同记录';
       end   
    else 
        begin
            insert into News(title,content,contentdate,srcurl,siteid,projectid,createdate)
            select title,content,contentdate,srcurl,siteid,projectid,createdate from inserted
          print '插入成功';
       end
go