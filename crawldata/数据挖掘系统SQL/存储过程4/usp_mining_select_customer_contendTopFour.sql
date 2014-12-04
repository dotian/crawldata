if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_select_customer_contendTopFour]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_select_customer_contendTopFour
GO
create proc usp_mining_select_customer_contendTopFour
@projectId int
as
  select top(4) ContendId,ContendName from dbo.ContendInfo where ProjectId = @projectId 
go


exec usp_mining_select_customer_contendTopFour 4




















