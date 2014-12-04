
----------------------------------------------------------------------------------------------------

if exists (select * from dbo.sysobjects where id = object_id(N'view_mining_ProjetTagRelation') 
	and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view view_mining_ProjetTagRelation
go

create view view_mining_ProjetTagRelation
     
as
	-- select A.ProjectId,ProjectName,TagId,TagName,SecondTag from ProjetTagRelation A,Tag B where A.TagId= B.Id
	 select B.ProjectId,B.ProjectName,A.Id,A.Tid,A.TagName,A.KoreanTranslate from ProjetTagRelation B,TagList A  where B.TagId = A.Id  

go

----------------------------------------------------------------------------------------------------

 if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_Select_Tag]')
     and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
 drop procedure usp_mining_Select_Tag
GO

 create proc usp_mining_Select_Tag
  @searchKey varchar(50),
  @pageSize int,
  @pageIndex int
 as
   declare @startindex int
  set @startindex = (@pageIndex-1)*@pageSize
  
   if (len(@searchkey)>0)
     begin
         select top(@pageSize) Id,TagName,SecondTag,KoreanTranslate from 
         (
            select row_number() over(order by Id ASC) as RowNum,Id,TagName,SecondTag,KoreanTranslate 
             from Tag where TagName like '%'+@searchKey+'%' or SecondTag like '%'+@searchKey+'%'
         ) as NewTB
          where NewTB.RowNum > @startindex
     end
   else
     begin
        select top(@pageSize) Id,TagName,SecondTag,KoreanTranslate from 
         (
            select row_number() over(order by Id ASC) as RowNum,Id,TagName,SecondTag,KoreanTranslate 
             from Tag
         ) as NewTB
          where NewTB.RowNum > @startindex
     end
  go
  
 -- exec usp_mining_Select_Tag '',10,2
----------------------------------------------------------------------------------------------------

 
  if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_Select_TagDataCount]')
     and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
 drop procedure usp_mining_Select_TagDataCount
GO
 
  create proc usp_mining_Select_TagDataCount
  @searchKey varchar(50)
 as
    if (len(@searchkey)>0)
     begin
       select count(1) from Tag where TagName like '%'+@searchKey+'%' or SecondTag like '%'+@searchKey+'%'
     end
   else
     begin
        select count(1) from Tag
     end
  go
  
  /*---------------------------------------------------------------------------------------*/
  
if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_Delete_TagById]')
  and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
  drop procedure usp_mining_Delete_TagById
GO
 
  create proc usp_mining_Delete_TagById
  @tagId int
 as
   delete from dbo.Tag where Id = @tagId
  go
 ----------------------------------------------------------------------------------------------------
 
   
if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_Insert_Tag]')
  and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
  drop procedure usp_mining_Insert_Tag
GO
  create proc usp_mining_Insert_Tag
    @tagName varchar(50),
    @secondTag varchar(50),
    @koreanTranslate varchar(200)
  as
   if not exists( select TagName,SecondTag from Tag where TagName = @tagName and SecondTag = @secondTag and KoreanTranslate = @koreanTranslate) 
      begin
		 insert into Tag(TagName,SecondTag,KoreanTranslate) values(@tagName,@secondTag,@koreanTranslate)
		 
		 select 1
      end
   else
      begin
         select 2
      end
  go
----------------------------------------------------------------------------------------------------

  
 if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_Update_TagByTagId]')
  and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
  drop procedure usp_mining_Update_TagByTagId
 GO
  create proc usp_mining_Update_TagByTagId
    @tagId int,
    @tagName varchar(50),
    @secondTag varchar(50),
    @koreanTranslate varchar(200)
  as 
  if not exists(select TagName,SecondTag from Tag where TagName = @tagName and SecondTag = @secondTag and KoreanTranslate = @koreanTranslate) 
      begin
        update Tag set TagName = @tagName, SecondTag = @secondTag, KoreanTranslate = @koreanTranslate where Id = @tagId
		
		select 1
      end
   else
      begin
         select 2
      end
  go
----------------------------------------------------------------------------------------------------

 if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_Select_ProjectListRunStatus]')
  and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
  drop procedure usp_mining_Select_ProjectListRunStatus
 GO
   create proc usp_mining_Select_ProjectListRunStatus
   as
     select ProjectId,ProjectName from projectlist where RunStatus = 1
   go



 if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_Select_ProjetTagRelationByProlectId]')
  and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
  drop procedure usp_mining_Select_ProjetTagRelationByProlectId
 GO
   create proc usp_mining_Select_ProjetTagRelationByProlectId
    @projectId int
   as
     -- select ProjectId,ProjectName,TagId,TagName,SecondTag,KoreanTranslate from view_mining_ProjetTagRelation where ProjectId = @projectId
     select ProjectId,ProjectName,Id,Tid,TagName,KoreanTranslate from view_mining_ProjetTagRelation where ProjectId = @projectId
   go




  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  