if (object_id('tgr_hankook_insert', 'tr') is not null)
    drop trigger tgr_hankook_insert
go
create trigger tgr_hankook_insert
on hankook
    instead of insert --����֮ǰ����
as
    --�������
   declare @url varchar(1000);
    --��inserted���в�ѯ�Ѿ������¼��Ϣ
  
    select @url = url from inserted;
     --���ܴ�����ͬ��Url Url��ͬ���ʾͬһ����ַ,ͬһ����ַ���ݾ���ͬ
    if exists(select id from hankook where url = @url)
       begin
           print '����ʧ��,�Ѵ�����ͬ��¼';
       end   
    else 
        begin
              insert into hankook(title,content,url,analysis,tags,published,contend,[type],sitename)
            select title,content,url,analysis,tags,published,contend,[type],sitename from inserted
          print '����ɹ�';
       end
go