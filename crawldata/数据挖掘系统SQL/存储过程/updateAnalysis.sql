   if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_update_analysisBySd_id]')
     and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
 drop procedure usp_mining_update_analysisBySd_id
GO

 create proc usp_mining_update_analysisBySd_id
  @sd_id int,
  @analysis int
 as
   update SiteData set Analysis = @analysis where SD_Id = @sd_id
 go 
 
 
   if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_update_attentionBySd_id]')
     and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
 drop procedure usp_mining_update_attentionBySd_id
GO

 create proc usp_mining_update_attentionBySd_id
  @sd_id int,
  @attention int
 as
   declare @attId int
   select  @attId= Attention from SiteData where SD_Id = @sd_id
   if(@attId<=0)		
      update SiteData set Attention = 1 where SD_Id = @sd_id
    else  
       update SiteData set Attention = 0 where SD_Id = @sd_id
 go 
 
 
    if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_update_showstatusBySd_id]')
     and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
 drop procedure usp_mining_update_showstatusBySd_id
GO

 create proc usp_mining_update_showstatusBySd_id
  @sd_id int,
  @showstatus int
 as
   update SiteData set ShowStatus = @showstatus where SD_Id = @sd_id
 go 
----------------------------------------------------------------------------------------- 



 select * from SiteData where ShowStatus = 2
 
 select * from SiteData where ShowStatus = 0
 
 exec usp_mining_Select_ProjectDataByProjectId 1,13,1,1,0,'','','',2,0,-1,0
 
 

 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 