IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee]') AND type in (N'U'))
DROP TABLE [dbo].[Employee]
GO

CREATE TABLE [dbo].[Employee](
	[EmpId] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[SecretKey] [varchar](50) NOT NULL,
	[EmpName] [varchar](50) NOT NULL,
	[Permissions] [int] NOT NULL,
	[RunStatus] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Company] [int]  NULL,
	[District] [varchar](500)  NULL,
	[Email] [varchar](50) NULL,
	[Contend] [varchar](500) NULL
) ON [PRIMARY]
GO