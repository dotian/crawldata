
USE [DataMiningDB]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_mining_update_hotBySd_id]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_mining_update_hotBySd_id]
GO
 create proc [dbo].[usp_mining_update_hotBySd_id]
  @sd_id int,
  @hot int
 as
   update SiteData set Hot = @hot where SD_Id = @sd_id

GO
