use DataMiningDB
go


  if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_select_CategorySiteRelationByCateId]')
     and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
 drop procedure usp_mining_select_CategorySiteRelationByCateId
GO

 create proc usp_mining_select_CategorySiteRelationByCateId
  @cateid int
as

 select A.SiteId,A.SiteName,B.PlateName,B.SiteUrl from dbo.CategorySiteRelation A, dbo.SiteList B
  where A.SiteId = B.SiteId and A.CateId = @cateid
 go 
 
 
 exec usp_mining_select_CategorySiteRelationByCateId 1



--select * from dbo.CategorySiteRelation


select CateId,CategoryName,ClassId from dbo.CategoryInfo




select * from Template where TemplateName like '%����%'


/*

765	�ѹ�����
85	zol����
766	���˲���
922	������IT����
973	��������
991	��Ѷ����
*/

select * from dbo.SiteList where tid in
(765,85,766,922,973,991)


insert into CategorySiteRelation(SiteId,SiteName,CateId,CreateData)

select '88036','���˲���','5','2013-11-4' union all 
 select '88038','�ѹ�����','5','2013-11-4' union all 
 select '75026','д������','5','2013-11-4' union all 
 select '63562','����������������','5','2013-11-4' union all 
 select '70148','�̺�-CIOAge.com','5','2013-11-4' 





select * from CategorySiteRelation






select CateId,CategoryName,count(1) as C, from dbo.CategoryInfo A, 






<table style=' width:100%;' border='0' cellpadding='0' cellspacing='0'><tr><th style='width:15%;'></th><th style='width:30%;'>��������</th><th style='width:15%;'>վ������</th><th style='width:30%;'>����ʱ��</th></tr><tr><td><input type='checkbox' id='1' value='1'/></td><td>�滢��̳����</td><td>1</td><td>1</td></tr></table>













