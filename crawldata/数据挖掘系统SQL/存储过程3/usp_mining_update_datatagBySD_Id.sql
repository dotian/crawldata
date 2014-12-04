if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_update_datatagBySD_Id]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_update_datatagBySD_Id
GO
create proc usp_mining_update_datatagBySD_Id
@sd_id int,
@tag1 varchar(20),
@tag2 varchar(20),
@tag3 varchar(20),
@tag4 varchar(20),
@tag5 varchar(20),
@tag6 varchar(20)
as
       --如果存在,就变成修改
       
      declare @showstatus int;
      select @showstatus = ShowStatus from dbo.SiteData where SD_Id =@sd_id  
       if(@showstatus=0)
         begin
           update SiteData set Tag1=@tag1,Tag2 = @tag2,Tag3 = @tag3,Tag4=@tag4,Tag5 = @tag5,Tag6 = @tag6, ShowStatus =1 where SD_Id =@sd_id
         end
       else
         begin
            update SiteData set Tag1=@tag1,Tag2 = @tag2,Tag3 = @tag3,Tag4=@tag4,Tag5 = @tag5,Tag6 = @tag6 where SD_Id =@sd_id  
         end    
      select 1;
go




















