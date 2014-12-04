if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_insert_siedateByRssData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_insert_siedateByRssData
GO
create proc usp_mining_insert_siedateByRssData
@title varchar(200),
@content varchar(4000),
@srcurl varchar(500),
@projectId int,
@contentdate datetime,
@sitetype int,
@sitename varchar(100),
@analysis int,
@showstatus int,
@tag1 varchar(50)
as
   
   --ͬһ�� url, �����������ͬһ��  ��Ŀ����
   insert  into  dbo.SiteData(Title,Content,SrcUrl,ProjectId,ContentDate,SiteType,SiteName,Analysis,ShowStatus,Tag1)
    values(@title,@content,@srcurl,@projectId,@contentdate,@sitetype,@sitename,@analysis,@showstatus,@tag1)
go


exec usp_mining_insert_siedateByRssData 

'�ſƺ�����̥�ٻ�2013�й����ֽ������Ѹ�������̥��','reduction',
'http://hankook.xlmediawatch.com/main/Hankook.nsf/pub/DATA-9F7BGN','10','2014/1/7 0:00:00','2','�й���̥������','1','2','����'


-----------------------------------------------------------------------------




































































