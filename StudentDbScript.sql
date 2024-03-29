USE [master]
GO
/****** Object:  Database [StudentSystem]    Script Date: 11/24/2012 22:04:34 ******/
CREATE DATABASE [StudentSystem] ON  PRIMARY 
( NAME = N'StudentSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.DBSERVER\MSSQL\DATA\StudentSystem.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'StudentSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.DBSERVER\MSSQL\DATA\StudentSystem_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [StudentSystem] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [StudentSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [StudentSystem] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [StudentSystem] SET ANSI_NULLS OFF
GO
ALTER DATABASE [StudentSystem] SET ANSI_PADDING OFF
GO
ALTER DATABASE [StudentSystem] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [StudentSystem] SET ARITHABORT OFF
GO
ALTER DATABASE [StudentSystem] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [StudentSystem] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [StudentSystem] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [StudentSystem] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [StudentSystem] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [StudentSystem] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [StudentSystem] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [StudentSystem] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [StudentSystem] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [StudentSystem] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [StudentSystem] SET  DISABLE_BROKER
GO
ALTER DATABASE [StudentSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [StudentSystem] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [StudentSystem] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [StudentSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [StudentSystem] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [StudentSystem] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [StudentSystem] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [StudentSystem] SET  READ_WRITE
GO
ALTER DATABASE [StudentSystem] SET RECOVERY SIMPLE
GO
ALTER DATABASE [StudentSystem] SET  MULTI_USER
GO
ALTER DATABASE [StudentSystem] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [StudentSystem] SET DB_CHAINING OFF
GO
USE [StudentSystem]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 11/24/2012 22:04:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[StudentId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DOB] [datetime] NOT NULL,
	[GradePointAvg] [decimal](8, 3) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_StudentName] ON [dbo].[Student] 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'This is secondary key of table. this may use for seaching' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Student', @level2type=N'INDEX',@level2name=N'IX_StudentName'
GO
/****** Object:  StoredProcedure [dbo].[SaveStudent]    Script Date: 11/24/2012 22:04:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Anusha Gunasekara
-- Create date: <2012 - Nov - 24>
-- Description:	<Save Student Data>
-- =============================================
CREATE PROCEDURE [dbo].[SaveStudent]
@Name  nvarchar(50),
@DOB datetime,
@GradePointAvg decimal(8,3),
@Active bit
AS
BEGIN
	SET NOCOUNT ON;

	begin transaction
    INSERT INTO   Student(Name, DOB, GradePointAvg, Active)
	Values(@Name,@DOB,@GradePointAvg,@Active)         
	
	if(@@ERROR=0)
		commit transaction
	else
		rollback transaction
END
GO
/****** Object:  StoredProcedure [dbo].[GetStudent]    Script Date: 11/24/2012 22:04:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Anusha Gunasekara
-- Create date: <2012 - Nov - 24>
-- Description:	<Get Student Data>
-- =============================================
CREATE PROCEDURE [dbo].[GetStudent]
@StudentId bigint
AS
BEGIN
	SET NOCOUNT ON;

    SELECT     StudentId, Name, DOB, GradePointAvg, Active
	FROM         Student
	Where StudentId=@StudentId or @StudentId=0
END
GO
/****** Object:  Default [DF_Student_Active]    Script Date: 11/24/2012 22:04:35 ******/
ALTER TABLE [dbo].[Student] ADD  CONSTRAINT [DF_Student_Active]  DEFAULT ((1)) FOR [Active]
GO
