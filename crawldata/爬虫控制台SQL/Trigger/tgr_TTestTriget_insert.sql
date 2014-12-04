if (object_id('tgr_TTestTriget_insert', 'tr') is not null)
    drop trigger tgr_TTestTriget_insert
go
create trigger tgr_TTestTriget_insert
on TTestTriget
    instead of insert --插入之前触发
as
    --定义变量
    declare @id int, @strname varchar(20),@apple varchar(50);
    --在inserted表中查询已经插入记录信息
    select @id = id, @strname = strname,@apple = apple from inserted;
  
    --select @exitrecord = count(1) from TTestTriget where id =  @id and strname = @strname and apple = @apple 
    if exists(select id from TTestTriget where id =  @id and strname = @strname and apple = @apple )
       begin
           print '插入失败,已存在相同记录';
       end   
    else 
        begin
          insert into TTestTriget(Id,strname,apple) values(@id,@strname,@apple);
          print '插入成功';
       end
go