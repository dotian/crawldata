if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_update_projectlistRc_Blog]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_update_projectlistRc_Blog
GO
create proc usp_mining_update_projectlistRc_Blog
@projectid int,
@rc_blog int
as
  update dbo.ProjectList set RC_Blog = @rc_blog where ProjectId = @projectid
go





















