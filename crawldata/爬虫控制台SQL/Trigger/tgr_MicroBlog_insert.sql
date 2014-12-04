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
  
    select @srcrl = SrcUrl from inserted;
  
    if exists(select cid from MicroBlog where SrcUrl = @srcrl  )
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