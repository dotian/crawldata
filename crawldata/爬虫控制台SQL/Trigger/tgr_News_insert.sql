 if (object_id('tgr_News_insert', 'tr') is not null)
    drop trigger tgr_News_insert
go
create trigger tgr_News_insert
on News
    instead of insert --����֮ǰ����
as
    --�������
    declare @title varchar(200) , @srcrl varchar(500),@siteid int;
    --��inserted���в�ѯ�Ѿ������¼��Ϣ
  
    select  @srcrl = SrcUrl from inserted;
  
    if exists(select cid from News where SrcUrl = @srcrl  )
       begin
           print '����ʧ��,�Ѵ�����ͬ��¼';
       end   
    else 
        begin
            insert into News(title,content,contentdate,srcurl,siteid,projectid,createdate)
            select title,content,contentdate,srcurl,siteid,projectid,createdate from inserted
          print '����ɹ�';
       end
go