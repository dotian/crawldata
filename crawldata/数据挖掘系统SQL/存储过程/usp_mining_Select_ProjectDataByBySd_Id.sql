   

   if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_Select_ProjectDataByBySd_Id]')
     and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
 drop procedure usp_mining_Select_ProjectDataByBySd_Id
GO

 create proc usp_mining_Select_ProjectDataByBySd_Id
 @sd_id int
 as
   select SD_Id,Title,Content,ContentDate,SrcUrl,SiteId,ProjectId,CreateDate,
     SiteName,Analysis,Attention,ShowStatus,Tag1,Tag2,Tag3,Tag4,Tag5,Tag6,Hot
    
     from SiteData
      where SD_Id = @sd_id  
 go 
 