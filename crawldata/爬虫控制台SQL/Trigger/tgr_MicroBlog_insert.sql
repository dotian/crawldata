 if (object_id('tgr_MicroBlog_insert', 'tr') is not null)
    drop trigger tgr_MicroBlog_insert
go
create trigger tgr_MicroBlog_insert
on MicroBlog
    instead of insert --����֮ǰ����
as
    --�������
    declare @title varchar(200) , @srcrl varchar(500),@siteid int;
    --��inserted���в�ѯ�Ѿ������¼��Ϣ
  
    select @srcrl = SrcUrl from inserted;
  
    if exists(select cid from MicroBlog where SrcUrl = @srcrl  )
       begin
           print '����ʧ��,�Ѵ�����ͬ��¼';
       end   
    else 
        begin
            insert into MicroBlog(title,content,contentdate,author,srcurl,siteid,projectid,createdate)
            select title,content,contentdate,author,srcurl,siteid,projectid,createdate from inserted
          print '����ɹ�';
       end
go