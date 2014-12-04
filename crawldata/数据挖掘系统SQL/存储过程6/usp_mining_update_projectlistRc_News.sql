if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_update_projectlistRc_News]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_update_projectlistRc_News
go
create proc usp_mining_update_projectlistRc_News
@projectid int,
@rc_news int
as
  update dbo.ProjectList set RC_News = @rc_news where ProjectId = @projectid
go






















