if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_Delete_SiteListBySiteId]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_Delete_SiteListBySiteId
GO
create proc usp_mining_Delete_SiteListBySiteId
@siteId int
as

 delete from SiteList where SiteId = @siteId
go