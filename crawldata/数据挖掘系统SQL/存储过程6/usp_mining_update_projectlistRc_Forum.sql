if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_update_projectlistRc_Forum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_update_projectlistRc_Forum
GO
create proc usp_mining_update_projectlistRc_Forum
@projectid int,
@rc_forum int
as
  update dbo.ProjectList set RC_Forum = @rc_forum where ProjectId = @projectid
go





















