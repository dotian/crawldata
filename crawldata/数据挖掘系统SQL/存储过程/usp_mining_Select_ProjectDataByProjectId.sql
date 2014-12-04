 

 if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_Select_ProjectDataByProjectId]')
     and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
 drop procedure usp_mining_Select_ProjectDataByProjectId
GO

 create proc usp_mining_Select_ProjectDataByProjectId
  @projectId int,
  @pagesize int,
  @pageindex int,
  @sitetype int,
  @searchType int,
  @searchKey varchar(200),
  @startTime varchar(50),
  @endTime varchar(50),
  @analysis int,
  @showstatus int,
  @attention int,
  @hot int
 as
  declare @startindex int
  set @startindex = (@pageIndex-1)*@pageSize
   declare @sqlStr nvarchar(2000) 
  set @sqlStr=''
  
      set @sqlStr = @sqlStr+' select top('+cast(@pagesize as varchar(15))+') * from ' 
      set @sqlStr = @sqlStr+ ' ( '
        set @sqlStr = @sqlStr+ ' select row_number() over(order by SD_Id desc) as RowNum, '
        set @sqlStr = @sqlStr+ ' SD_Id,Title,Content,ContentDate,SrcUrl,SiteId, '
        set @sqlStr = @sqlStr+ ' ProjectId,CreateDate,SiteType,SiteName,Analysis,ShowStatus,Attention,Tag1,Tag2,Tag3,Tag4,Tag5,Tag6,Hot from SiteData ' 
            set @sqlStr = @sqlStr+ ' where ProjectId = ' + cast(@projectId as varchar(15)) 
            set @sqlStr = @sqlStr+ ' and sitetype = '+cast(@sitetype as varchar(15)) 
          
            if(cast(Len(@searchKey) as int)>=1)
              begin
                 if(@searchType=0) --�� ����� ����
					begin
					  set @sqlStr = @sqlStr+ ' and  (Title like ''%'+@searchKey+'%'' or [content] like ''%'+@searchKey+'%'')'
					end
			    else if(@searchType=1)  --�ѱ���
			        begin
			          set @sqlStr = @sqlStr+ ' and  Title like ''%'+@searchKey+'%'''
			        end 
			    else if(@searchType=2)  --������
			        begin
			          set @sqlStr = @sqlStr+ ' and [content] like ''%'+@searchKey+'%'''
			        end 
              end --�����ؼ��ֽ���
            if (Len(@startTime)>0) 
              begin
                set @sqlStr = @sqlStr+ ' and CreateDate>= cast('''+@startTime+''' as datetime)'
              end 
            if (Len(@endTime)>0) 
              begin
                set @sqlStr = @sqlStr+ ' and CreateDate<cast('''+@endTime+''' as datetime)'
              end  --����ʱ��
            
            /*
             ...... ����дwhere ����
            */  
            
                        
            if (@attention>0) -- -1 ������ע
              begin
                set @sqlStr = @sqlStr+ ' and Attention = '+cast(@attention  as varchar(10))+''
              end
              
            if (@hot>0) -- -1 ��������
              begin
                set @sqlStr = @sqlStr+ ' and Hot = '+cast(@hot  as varchar(10))+''
              end  
             
            if (@showstatus>=0 and @showstatus<10)  -- ������ ��ѯ���õ�����
              begin
                set @sqlStr = @sqlStr+ ' and ShowStatus = '+cast(@showstatus as varchar(10))+''
              end
            else if(@showstatus=99)  -- �� ��ɾ��
              begin
                set @sqlStr = @sqlStr+ ' and ShowStatus = 99 '
              end
            else -- -1 ������,����ʾ�Ѿ�ɾ��������  
              begin
                set @sqlStr = @sqlStr+ ' and (ShowStatus = 0 or ShowStatus = 1) '
              end
              
			if (@analysis>0)  -- o ͬ,  1234 ���и���
			  begin
			    set @sqlStr = @sqlStr+ ' and Analysis = '+cast(@analysis as varchar(10))+''
			  end
			  
        set @sqlStr = @sqlStr+ ')as NewTB '
      set @sqlStr = @sqlStr+ ' where NewTB.RowNum>'+cast(@startindex as varchar(15))
      set @sqlStr = @sqlStr+ ' order by SD_Id desc '
      
      print  @sqlStr;
    execute(@sqlStr)
 go
  
  --exec usp_mining_Select_ProjectDataByProjectId 1001,20,1,1,0,'','2013-09-11','2013-09-12',-1,-1,-1,-1
 
  --�õ� RecordCount
   if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_Select_ProjectDataCountByProjectId]')
     and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
 drop procedure usp_mining_Select_ProjectDataCountByProjectId
GO

 create proc usp_mining_Select_ProjectDataCountByProjectId
  @projectId int,
  @sitetype int,
  @searchType int,
  @searchKey varchar(200),
  @startTime varchar(50),
  @endTime varchar(50),
  @analysis int,
  @showstatus int,
  @attention int,
  @hot int
 as

   declare @sqlStr nvarchar(2000) 
  set @sqlStr=''
  
        set @sqlStr = @sqlStr+ ' select count(1) from SiteData ' 
      
            set @sqlStr = @sqlStr+ ' where ProjectId = ' + cast(@projectId as varchar(15)) 
            set @sqlStr = @sqlStr+ ' and sitetype = '+cast(@sitetype as varchar(15)) 
          
            if(cast(Len(@searchKey) as int)>=1)
              begin
                 if(@searchType=0) --�� ����� ����
					begin
					  set @sqlStr = @sqlStr+ ' and  (Title like ''%'+@searchKey+'%'' and [content] like ''%'+@searchKey+'%'')'
					end
			    else if(@searchType=1)  --�ѱ���
			        begin
			          set @sqlStr = @sqlStr+ ' and  Title like ''%'+@searchKey+'%'''
			        end 
			    else if(@searchType=2)  --������
			        begin
			          set @sqlStr = @sqlStr+ ' and [content] like ''%'+@searchKey+'%'''
			        end 
              end --�����ؼ��ֽ���
            if (Len(@startTime)>0) 
              begin
                set @sqlStr = @sqlStr+ ' and CreateDate>= cast('''+@startTime+''' as datetime)'
              end 
            if (Len(@endTime)>0) 
              begin
                set @sqlStr = @sqlStr+ ' and CreateDate<cast('''+@endTime+''' as datetime)'
              end  --����ʱ��
            
            /*
             ...... ����дwhere ����
            */              
            if (@attention>0) -- -1 ������ע
              begin
                set @sqlStr = @sqlStr+ ' and Attention = '+cast(@attention  as varchar(10))+''
              end
              
            if (@hot>0) -- -1 ��������
              begin
                set @sqlStr = @sqlStr+ ' and Hot = '+cast(@hot  as varchar(10))+''
              end  
              
              
            if (@showstatus>=0 and @showstatus<10)  -- ������ ��ѯ���õ�����
              begin
                set @sqlStr = @sqlStr+ ' and ShowStatus = '+cast(@showstatus as varchar(10))+''
              end
            else if(@showstatus=99)  -- �� ��ɾ��
              begin
                set @sqlStr = @sqlStr+ ' and ShowStatus = 99 '
              end
            else -- -1 ������,����ʾ�Ѿ�ɾ��������  
              begin
                set @sqlStr = @sqlStr+ ' and (ShowStatus = 0 or ShowStatus = 1) '
              end
			if (@analysis>0)  -- o ͬ,  1234 ���и���
			  begin
			    set @sqlStr = @sqlStr+ ' and Analysis = '+cast(@analysis as varchar(10))+''
			  end
     
      print  @sqlStr;
    execute(@sqlStr)
 go
 ---------------------------------------------------------------------------------------
 
 