if (object_id('tgr_TTestTriget_insert', 'tr') is not null)
    drop trigger tgr_TTestTriget_insert
go
create trigger tgr_TTestTriget_insert
on TTestTriget
    instead of insert --����֮ǰ����
as
    --�������
    declare @id int, @strname varchar(20),@apple varchar(50);
    --��inserted���в�ѯ�Ѿ������¼��Ϣ
    select @id = id, @strname = strname,@apple = apple from inserted;
  
    --select @exitrecord = count(1) from TTestTriget where id =  @id and strname = @strname and apple = @apple 
    if exists(select id from TTestTriget where id =  @id and strname = @strname and apple = @apple )
       begin
           print '����ʧ��,�Ѵ�����ͬ��¼';
       end   
    else 
        begin
          insert into TTestTriget(Id,strname,apple) values(@id,@strname,@apple);
          print '����ɹ�';
       end
go