
 if (object_id('tgr_Blog_insert', 'tr') is not null)
    drop trigger tgr_Blog_insert
go
create trigger tgr_Blog_insert
on Blog
    instead of insert --����֮ǰ����
as
    --�������
    declare @title varchar(200) , @srcrl varchar(500),@siteid int;
    --��inserted���в�ѯ�Ѿ������¼��Ϣ
  
    select @srcrl = SrcUrl from inserted;
     --���ܴ�����ͬ��Url Url��ͬ���ʾͬһ����ַ,ͬһ����ַ���ݾ���ͬ
    if exists(select cid from Blog where SrcUrl = @srcrl)
       begin
           print '����ʧ��,�Ѵ�����ͬ��¼';
       end   
    else 
        begin
            insert into Blog(title,content,contentdate,author,srcurl,siteid,projectid,createdate)
            select title,content,contentdate,author,srcurl,siteid,projectid,createdate from inserted
          print '����ɹ�';
       end
go
