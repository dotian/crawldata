if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_select_contendProjectByName]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_select_contendProjectByName
GO
create proc usp_mining_select_contendProjectByName
@projectname varchar(50)
as
   if(len(@projectname)<=0)
     begin
       select ProjectId,ProjectName,RssKey,RunStatus from dbo.ProjectList where RunStatus<>99
     end
   else
      begin
          select ProjectId,ProjectName,RssKey,RunStatus from dbo.ProjectList where  RunStatus<>99 and ProjectName like '%'+@projectname+'%'
      end 
go
























