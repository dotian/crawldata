   if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_Delete_SiteDataBySdId]')
     and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
 drop procedure usp_mining_Delete_SiteDataBySdId
GO

 create proc usp_mining_Delete_SiteDataBySdId
  @sd_id int
 as
   update SiteData set ShowStatus = 99 where SD_Id = @sd_id
 go 
 
 
