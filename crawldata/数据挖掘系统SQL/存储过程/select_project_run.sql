USE [DataMiningDB]
GO

/****** Object:  StoredProcedure [dbo].[usp_mining_select_project_run]    Script Date: 10/10/2013 13:19:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_mining_select_project_run]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_mining_select_project_run]
GO

USE [DataMiningDB]
GO

/****** Object:  StoredProcedure [dbo].[usp_mining_select_project_run]    Script Date: 10/10/2013 13:19:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 create proc [dbo].[usp_mining_select_project_run]
 
 as
select distinct A.ProjectId,A.MatchingRuleType,A.MatchingRule,B.ProjectType,B.ClassId from ProjectList A,ProjectDetail B

 where A.RunStatus = 1 and A.ProjectId = B.ProjectId and B.EndDate>=getdate()
 

GO



