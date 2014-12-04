
 if (object_id('tgr_Blog_insert', 'tr') is not null)
    drop trigger tgr_Blog_insert
go
create trigger tgr_Blog_insert
on Blog
    instead of insert --插入之前触发
as
    --定义变量
    declare @title varchar(200) , @srcrl varchar(500),@siteid int;
    --在inserted表中查询已经插入记录信息
  
    select @srcrl = SrcUrl from inserted;
     --不能存入相同的Url Url相同则表示同一个地址,同一个地址内容就相同
    if exists(select cid from Blog where SrcUrl = @srcrl)
       begin
           print '插入失败,已存在相同记录';
       end   
    else 
        begin
            insert into Blog(title,content,contentdate,author,srcurl,siteid,projectid,createdate)
            select title,content,contentdate,author,srcurl,siteid,projectid,createdate from inserted
          print '插入成功';
       end
go
