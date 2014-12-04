if (object_id('tgr_SiteData_insert', 'tr') is not null)
    drop trigger tgr_SiteData_insert
go
create trigger tgr_SiteData_insert
on SiteData
    instead of insert --插入之前触发
as
    --定义变量
    declare @title varchar(200) , @srcrl varchar(500),@projectid int;
    --在inserted表中查询已经插入记录信息
   declare @showstatus int;
   set @showstatus = 0;
    select @srcrl = SrcUrl,@projectid = ProjectId,@showstatus = ShowStatus from inserted;
     --不能存入相同的Url Url相同则表示同一个地址,同一个地址内容就相同
    if exists(select SD_Id from SiteData where SrcUrl = @srcrl and ProjectId = @projectid)
       begin
           print '插入失败,已存在相同记录';
           return;
       end   
       
    if(@showstatus>0)
        begin
            insert into SiteData(Title,Content,SrcUrl,ProjectId,ContentDate,SiteType,SiteName,Analysis,ShowStatus,Tag1)
             select Title,Content,SrcUrl,ProjectId,ContentDate,SiteType,SiteName,Analysis,ShowStatus,Tag1 from inserted 
            print '插入成功';
        end     
    else 
     begin
            insert into SiteData(Cid,Title,Content,ContentDate,SrcUrl,ProjectId,CreateDate,SiteType,SiteName)
            select Cid,Title,Content,ContentDate,SrcUrl,ProjectId,CreateDate,SiteType,SiteName from inserted 
          print '插入成功';
       end
go
