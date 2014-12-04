if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_select_customer_existcontendId]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_select_customer_existcontendId
GO
create proc usp_mining_select_customer_existcontendId
@projectId int,
@contendId int
--@runStatus int
as
  select count(1) from ContendInfo where ProjectId=@projectId and ContendId = @contendId
go























