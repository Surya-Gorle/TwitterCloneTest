USE [TCApp]
GO
/****** Object:  StoredProcedure [dbo].[DeleteTweet]    Script Date: 8/5/2019 4:10:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteTweet]
@TweetID int
as 
BEGIN
DELETE FROM TweetDetails WHERE ID=@TweetID
END

GO
/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 8/5/2019 4:10:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteUser]

@UserID int

as 

BEGIN

UPDATE Users SET Active = 0

WHERE ID = @UserID

END

GO
/****** Object:  StoredProcedure [dbo].[EditTweet]    Script Date: 8/5/2019 4:10:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EditTweet]

@TweetID int,

@Message varchar(max)

as 

BEGIN

UPDATE TweetDetails SET Message = @Message, Updated = GetDate()

WHERE ID = @TweetID

END

GO
/****** Object:  StoredProcedure [dbo].[GetAllDetails]    Script Date: 8/5/2019 4:10:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllDetails] 
@id int
as 
begin
select 
ID as UserID,
Password,
FullName,
DisplayName,
Email,
Joined,
Active
from users where id = @id
select 
T.ID as Tweet_ID,
U.DisplayName as UserName,
T.User_ID,
T.Message,
T.Created,
T.Updated from 
TweetDetails T 
INNER JOIN User_Following UF ON UF.FollowingUser_ID = T.User_ID
INNER JOIN USERS U ON T.User_ID = U.ID
where UF.User_ID = @id 
UNION ALL
select 
T.ID as Tweet_ID,
U.DisplayName as UserName,
T.User_ID,
T.Message,
T.Created,
T.Updated from 
TweetDetails T 
INNER JOIN USERS U ON T.User_ID = U.ID
AND u.ID=@id
order by t.Updated,t.Created desc
select
User_ID as BaseUserID,
FollowingUser_ID from
User_Following where User_ID = @id
select
User_ID as BaseUserID,
FollowingUser_ID from
User_Following where FollowingUser_ID = @id
end
GO
/****** Object:  StoredProcedure [dbo].[GetAllTweets]    Script Date: 8/5/2019 4:10:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllTweets] 
@id int
as 
begin
SELECT * FROM 
(select 
T.ID as ID,
U.DisplayName as UserName,
T.User_ID,
T.Message,
T.Created as Created,
T.Updated as Updated from 
TweetDetails T 
INNER JOIN User_Following UF ON UF.FollowingUser_ID = T.User_ID
INNER JOIN USERS U ON T.User_ID = U.ID
AND U.Active = 1
where UF.User_ID = @id 
UNION ALL
select 
T.ID as ID,
U.DisplayName as UserName,
T.User_ID,
T.Message,
T.Created as Created,
T.Updated as Updated from 
TweetDetails T 
INNER JOIN USERS U ON T.User_ID = U.ID
AND u.ID=@id AND U.Active = 1) T
order by T.Updated desc
end
GO
/****** Object:  StoredProcedure [dbo].[GetFollowersList]    Script Date: 8/5/2019 4:10:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetFollowersList] 

@id int

as 

begin

select

UF.ID,

UF.User_ID,

U.DisplayName as DisplayName,

FollowingUser_ID from

User_Following UF INNER JOIN 
USERS U ON U.ID = UF.User_ID AND U.Active=1 where FollowingUser_ID = @id

end
GO
/****** Object:  StoredProcedure [dbo].[GetFollowingList]    Script Date: 8/5/2019 4:10:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetFollowingList] 

@id int

as 

begin

select

UF.ID,

UF.User_ID,

UF.FollowingUser_ID,
U.DisplayName AS DisplayName from

User_Following UF INNER JOIN 
USERS U ON U.ID = UF.FollowingUser_ID AND U.Active = 1 where User_ID = @id

end


GO
/****** Object:  StoredProcedure [dbo].[InsertTweet]    Script Date: 8/5/2019 4:10:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertTweet]
@UserID int,
@Message varchar(max)
as 
BEGIN
INSERT INTO TweetDetails VALUES
(@UserID,@Message,GETDATE(),GETDATE())
END

GO
/****** Object:  StoredProcedure [dbo].[InsertUserFollowing]    Script Date: 8/5/2019 4:10:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUserFollowing]

@ID int,
@FollowingUserID int

as 

BEGIN

INSERT INTO User_Following VALUES

(@ID,@FollowingUserID)

END

GO
/****** Object:  StoredProcedure [dbo].[SearchUser]    Script Date: 8/5/2019 4:10:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SearchUser]

@displayName varchar(50)

as 

BEGIN

SELECT TOP 1 ID,
Password,
FullName,
DisplayName,
Email,
Joined,
Active
FROM Users WHERE DisplayName LIKE '%' + @displayName + '%'
AND Active = 1

END

GO
/****** Object:  Table [dbo].[TweetDetails]    Script Date: 8/5/2019 4:10:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TweetDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[User_ID] [int] NOT NULL,
	[Message] [varchar](140) NOT NULL,
	[Created] [datetime] NOT NULL,
	[Updated] [datetime] NULL,
 CONSTRAINT [pk_TweetDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User_Following]    Script Date: 8/5/2019 4:10:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Following](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[User_ID] [int] NOT NULL,
	[FollowingUser_ID] [int] NOT NULL,
 CONSTRAINT [pk_User_Following] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 8/5/2019 4:10:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[FullName] [varchar](100) NOT NULL,
	[DisplayName] [varchar](100) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Joined] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [pk_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[TweetDetails]  WITH CHECK ADD FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[User_Following]  WITH CHECK ADD FOREIGN KEY([FollowingUser_ID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[User_Following]  WITH CHECK ADD FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([ID])
GO
