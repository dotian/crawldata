 if (object_id('tgr_Forum_insert', 'tr') is not null)
    drop trigger tgr_Forum_insert
go
create trigger tgr_Forum_insert
on Forum
    instead of insert --����֮ǰ����
as
    --�������
    declare @title varchar(200) , @srcrl varchar(500),@siteid int;
    --��inserted���в�ѯ�Ѿ������¼��Ϣ
  
    select @srcrl = SrcUrl from inserted;
  
    if exists(select cid from Forum where  SrcUrl = @srcrl )
       begin
           print '����ʧ��,�Ѵ�����ͬ��¼';
       end   
    else 
        begin
            insert into Forum(title,content,contentdate,author,srcurl,pageview,reply,siteid,projectid,createdate)
            select title,content,contentdate,author,srcurl,pageview,reply,siteid,projectid,createdate from inserted
          print '����ɹ�';
       end
go
