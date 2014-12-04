if (object_id('tgr_hankook_insert', 'tr') is not null)
    drop trigger tgr_hankook_insert
go
create trigger tgr_hankook_insert
on hankook
    instead of insert --插入之前触发
as
    --定义变量
   declare @url varchar(1000);
    --在inserted表中查询已经插入记录信息
  
    select @url = url from inserted;
     --不能存入相同的Url Url相同则表示同一个地址,同一个地址内容就相同
    if exists(select id from hankook where url = @url)
       begin
           print '插入失败,已存在相同记录';
       end   
    else 
        begin
              insert into hankook(title,content,url,analysis,tags,published,contend,[type],sitename)
            select title,content,url,analysis,tags,published,contend,[type],sitename from inserted
          print '插入成功';
       end
go