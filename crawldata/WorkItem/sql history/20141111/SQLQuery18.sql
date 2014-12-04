USE [DataMingDB]
GO
/****** Object:  Trigger [dbo].[tgr_SiteData_insert]    Script Date: 11/11/2014 23:51:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER trigger [dbo].[tgr_SiteData_insert]
on [dbo].[SiteData]
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
            insert into SiteData(Title,Content,SrcUrl,ProjectId,ContentDate,CreateDate,SiteType,SiteName,Analysis,ShowStatus,Tag1,PlateName)
             select Title,Content,SrcUrl,ProjectId,ContentDate,CreateDate,SiteType,SiteName,Analysis,ShowStatus,Tag1,PlateName from inserted 
            print '插入成功';
        end     
    else 
     begin
            insert into SiteData(Cid,Title,Content,ContentDate,SrcUrl,ProjectId,CreateDate,SiteType,SiteName,PlateName)
            select Cid,Title,Content,ContentDate,SrcUrl,ProjectId,CreateDate,SiteType,SiteName,PlateName from inserted 
          print '插入成功';
       end
