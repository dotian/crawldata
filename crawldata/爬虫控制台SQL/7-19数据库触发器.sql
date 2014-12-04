
IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[Forum_insert]'))
DROP TRIGGER [dbo].[Forum_insert]
GO
CREATE trigger [dbo].[Forum_insert]
on [dbo].Forum
for insert
as
if(select count(1) from Forum,inserted where Forum.SrcUrl = inserted.SrcUrl AND Forum.projectId = inserted.projectId
 and Forum.Title = inserted.Title and Forum.Content = inserted.Content ) > 1
begin
print 'error for Forum insert again'
rollback transaction
end


go

IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[News_insert]'))
DROP TRIGGER [dbo].[News_insert]
GO
CREATE trigger [dbo].[News_insert]
on [dbo].News
for insert
as
if(select count(1) from News,inserted where News.SrcUrl = inserted.SrcUrl AND News.projectId = inserted.projectId
 and News.Title = inserted.Title and News.Content = inserted.Content ) > 1
begin
print 'error for News insert again'
rollback transaction
end

go

IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[MicroBlog_insert]'))
DROP TRIGGER [dbo].[MicroBlog_insert]
GO
CREATE trigger [dbo].[MicroBlog_insert]
on [dbo].MicroBlog
for insert
as
if(select count(1) from MicroBlog,inserted where MicroBlog.SrcUrl = inserted.SrcUrl AND MicroBlog.projectId = inserted.projectId
 and MicroBlog.Title = inserted.Title and MicroBlog.Content = inserted.Content ) > 1
begin
print 'error for MicroBlog insert again'
rollback transaction
end

go



IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[blog_insert]'))
DROP TRIGGER [dbo].[blog_insert]
GO
CREATE trigger [dbo].[blog_insert]
on [dbo].[blog]
for insert
as
if(select count(1) from Blog,inserted where Blog.SrcUrl = inserted.SrcUrl AND Blog.projectId = inserted.projectId
 and Blog.Title = inserted.Title and Blog.Content = inserted.Content ) > 1
begin
print 'error for blog insert again'
rollback transaction
end

GO






