 Create FUNCTION [dbo].[SplitToTable]
(
      @SplitString nvarchar(max),
      @Separator nvarchar(10)=' '
  )
  RETURNS @SplitStringsTable TABLE
  (
  [id] int identity(1,1),
  [value] nvarchar(max)
 )
 AS
 BEGIN
     DECLARE @CurrentIndex int;
    DECLARE @NextIndex int;   
      DECLARE @ReturnText nvarchar(max);
     SELECT @CurrentIndex=1;
     WHILE(@CurrentIndex<=len(@SplitString))
         BEGIN
             SELECT @NextIndex=charindex(@Separator,@SplitString,@CurrentIndex);
             IF(@NextIndex=0 OR @NextIndex IS NULL)
                 SELECT @NextIndex=len(@SplitString)+1;
                SELECT @ReturnText=substring(@SplitString,@CurrentIndex,@NextIndex-@CurrentIndex);
                INSERT INTO @SplitStringsTable([value]) VALUES(@ReturnText);
                 SELECT @CurrentIndex=@NextIndex+1;
             END
     RETURN;
 END
 
 --select * FROm dbo.SplitToTable('111,b2222,323232,32d,e,323232f,g3222', ',')
 
 
 
 
 Create function [dbo].[GetSplitLength]
 (
   @String nvarchar(max),  --要分割的字符串
   @Split nvarchar(10)  --分隔符号
  )
  returns int
  as
  begin
   declare @location int
  declare @start int
  declare @length int
 
  set @String=ltrim(rtrim(@String))
  set @location=charindex(@split,@String)
  set @length=1
  while @location<>0
  begin
    set @start=@location+1
   set @location=charindex(@split,@String,@start)   set @length=@length+1
  end
 return @length
 end
 
 --select dbo.GetSplitLength('111,b2222,323232,32d,e,323232f,g3222',',')
 
 
 
 
 
 
 
 create function [dbo].[GetSplitOfIndex]
  (
   @String nvarchar(max),  --要分割的字符串
   @split nvarchar(10),  --分隔符号
   @index int --取第几个元素
  )
  returns nvarchar(1024)
  as
  begin
  declare @location int
  declare @start int
  declare @next int
 declare @seed int

  set @String=ltrim(rtrim(@String))
  set @start=1
  set @next=1
  set @seed=len(@split)
  
  set @location=charindex(@split,@String)
  while @location<>0 and @index>@next
 begin
    set @start=@location+@seed
    set @location=charindex(@split,@String,@start)
    set @next=@next+1
  end
  if @location =0 select @location =len(@String)+1 
  
  return substring(@String,@start,@location-@start)
 end
 
 
 
 --select dbo.GetSplitOfIndex('111,b2222,323232,32d,e,323232f,g3222',',', 17)
 
 

 
 DECLARE @Tags nvarchar(max);
 SELECT @Tags='111,b2222,323232,32d,e,323232f,g3222';
 DECLARE @Tag nvarchar(1000)
 DECLARE @next int;
 set @next=1
 
 DECLARE @Length int;
 SELECT @Length=dbo.GetSplitLength(@Tags,',')
  
 while @next<=@Length
 begin
     SET @Tag = left(dbo.GetSplitOfIndex(@Tags,',',@next), 16);
     print @Tag
     SET @Next=@Next+1;
 END
 
go

 
 
 select Cid,Title from forum where  Title like '%方%' and Title like '%太%' 
  and cid not in(select Cid from forum where Title like '%到底%')
  
 --866914
 
 
 
 select Cid,Title from forum where UseStatus is null and Title like '%方%' and Title like '%太%' and Cid not in(select Cid from forum where Title like '%到%' or Title like '%底%')
 
 

 
 
 
 
 
 
 