-- Add plate name into site data
ALTER TABLE [DataMingDB].[dbo].[SiteData] 
  ADD PlateName nvarchar(100) NOT NULL
  
-- insert site id & plate name into site data
ALTER proc [dbo].[usp_mining_insert_SiteData]
  @cid int,
  @title varchar(500),
  @content varchar(max),
  @contentdate datetime,
  @srcurl varchar(500),
  @projectid int,
  @createdate datetime,
  @sitetype int,
  @sitename varchar(50),  
  @siteId int,  
  @platename nvarchar(100) = NULL
as
  insert into SiteData (Cid,Title,Content,ContentDate,SrcUrl,ProjectId,CreateDate,SiteType,SiteName,SiteId,PlateName)
	values(@cid,@title,@content,@contentdate,@srcurl,@projectid,@createdate,@sitetype,@sitename,@siteId,@platename)
	
-- Get plate name from site data
ALTER proc [dbo].[usp_mining_Select_ProjectDataByProjectId]
	-- hide the middle part
...
	-- add PlateName in the sqlstr
        set @sqlStr = @sqlStr+ ' ProjectId,CreateDate,SiteType,SiteName,PlateName,Analysis,ShowStatus,Attention,Tag1,Tag2,Tag3,Tag4,Tag5,Tag6,Hot from SiteData ' 
            set @sqlStr = @sqlStr+ ' where ProjectId = ' + cast(@projectId as varchar(15)) 
...
	
--Update site id and plate name in site data
update SiteData set SiteId = f.siteId
	from SiteData inner join Forum f on SiteData.Cid=f.Cid and SiteData.SiteType=1

update sd set SiteId= f.SiteId 
	from SiteData sd,News f where sd.Cid=f.Cid and sd.SiteType=2
	
update sd set SiteId= f.SiteId 
	from SiteData sd,Blog f where sd.Cid=f.Cid and sd.SiteType=3
	
update sd set SiteId= f.SiteId 
	from SiteData sd,MicroBlog f where sd.Cid=f.Cid and sd.SiteType=5
  
update sd set PlateName= sl.PlateName from SiteData sd,SiteList sl where sd.SiteId=sl.SiteId
