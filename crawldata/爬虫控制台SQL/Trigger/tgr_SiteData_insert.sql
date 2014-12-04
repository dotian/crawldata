if (object_id('tgr_SiteData_insert', 'tr') is not null)
    drop trigger tgr_SiteData_insert
go
create trigger tgr_SiteData_insert
on SiteData
    instead of insert --����֮ǰ����
as
    --�������
    declare @title varchar(200) , @srcrl varchar(500),@projectid int;
    --��inserted���в�ѯ�Ѿ������¼��Ϣ
   declare @showstatus int;
   set @showstatus = 0;
    select @srcrl = SrcUrl,@projectid = ProjectId,@showstatus = ShowStatus from inserted;
     --���ܴ�����ͬ��Url Url��ͬ���ʾͬһ����ַ,ͬһ����ַ���ݾ���ͬ
    if exists(select SD_Id from SiteData where SrcUrl = @srcrl and ProjectId = @projectid)
       begin
           print '����ʧ��,�Ѵ�����ͬ��¼';
           return;
       end   
       
    if(@showstatus>0)
        begin
            insert into SiteData(Title,Content,SrcUrl,ProjectId,ContentDate,SiteType,SiteName,Analysis,ShowStatus,Tag1)
             select Title,Content,SrcUrl,ProjectId,ContentDate,SiteType,SiteName,Analysis,ShowStatus,Tag1 from inserted 
            print '����ɹ�';
        end     
    else 
     begin
            insert into SiteData(Cid,Title,Content,ContentDate,SrcUrl,ProjectId,CreateDate,SiteType,SiteName)
            select Cid,Title,Content,ContentDate,SrcUrl,ProjectId,CreateDate,SiteType,SiteName from inserted 
          print '����ɹ�';
       end
go
