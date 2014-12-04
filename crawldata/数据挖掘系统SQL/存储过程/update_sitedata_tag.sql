


USE [DataMiningDB]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_mining_update_sitedata_tag]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].usp_mining_update_sitedata_tag
GO
 create proc [dbo].usp_mining_update_sitedata_tag
  @pid int,
  @sd_id int,
  @tagid int
 as
 
 declare @tag1 varchar(50),@tag2 varchar(50)
  select  @tag1 = TagName,@tag2 = SecondTag from Tag where Id = @tagid
   update SiteData set ShowStatus =1, Tag1 = @tag1,Tag2 = @tag2 where SD_Id = @sd_id and ProjectId = @pid

GO























