if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_update_ProjectListStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_update_ProjectListStatus
GO
create proc usp_mining_update_ProjectListStatus
@projectId int,
@runStatus int
as
 update dbo.ProjectList set RunStatus = @runStatus where ProjectId = @projectId
go























