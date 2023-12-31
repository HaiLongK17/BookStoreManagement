USE [master]
GO
/****** Object:  Database [BookStore]    Script Date: 6/22/2023 9:49:06 AM ******/
CREATE DATABASE [BookStore]
 
GO
ALTER DATABASE [BookStore] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BookStore].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BookStore] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BookStore] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BookStore] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BookStore] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BookStore] SET ARITHABORT OFF 
GO
ALTER DATABASE [BookStore] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [BookStore] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BookStore] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BookStore] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BookStore] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BookStore] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BookStore] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BookStore] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BookStore] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BookStore] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BookStore] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BookStore] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BookStore] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BookStore] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BookStore] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BookStore] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [BookStore] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BookStore] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BookStore] SET  MULTI_USER 
GO
ALTER DATABASE [BookStore] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BookStore] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BookStore] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BookStore] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BookStore] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BookStore] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BookStore] SET QUERY_STORE = OFF
GO
USE [BookStore]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/22/2023 9:49:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Authors]    Script Date: 6/22/2023 9:49:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NULL,
 CONSTRAINT [PK_Authors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookCategories]    Script Date: 6/22/2023 9:49:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookCategories](
	[BookId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_BookCategories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC,
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 6/22/2023 9:49:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[AuthorId] [int] NULL,
	[PublisherId] [int] NULL,
	[Summary] [nvarchar](max) NOT NULL,
	[PhotoUrl] [nvarchar](250) NOT NULL,
	[PhotoPublicId] [nvarchar](200) NOT NULL,
	[Price] [money] NOT NULL,
	[Quantity] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedDate] [datetime2](7) NULL,
	[IsActice] [bit] NOT NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 6/22/2023 9:49:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 6/22/2023 9:49:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookId] [int] NULL,
	[UserId] [uniqueidentifier] NULL,
	[AnonymousName] [nvarchar](100) NULL,
	[Content] [nvarchar](500) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Publishers]    Script Date: 6/22/2023 9:49:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Publishers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NULL,
 CONSTRAINT [PK_Publishers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefreshTokens]    Script Date: 6/22/2023 9:49:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshTokens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[Token] [nvarchar](max) NOT NULL,
	[Expires] [datetime2](7) NOT NULL,
	[Revoked] [datetime2](7) NULL,
 CONSTRAINT [PK_RefreshTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleClaims]    Script Date: 6/22/2023 9:49:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_RoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 6/22/2023 9:49:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[NormalizedName] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserClaims]    Script Date: 6/22/2023 9:49:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLogins]    Script Date: 6/22/2023 9:49:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogins](
	[UserId] [uniqueidentifier] NOT NULL,
	[LoginProvider] [nvarchar](max) NULL,
	[ProviderKey] [nvarchar](max) NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserLogins] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 6/22/2023 9:49:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 6/22/2023 9:49:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[DisplayName] [nvarchar](200) NOT NULL,
	[Avatar] [nvarchar](250) NOT NULL,
	[AvatarPublicId] [nvarchar](200) NOT NULL,
	[Status] [bit] NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[NormalizedUserName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NOT NULL,
	[NormalizedEmail] [nvarchar](50) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](10) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTokens]    Script Date: 6/22/2023 9:49:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTokens](
	[UserId] [uniqueidentifier] NOT NULL,
	[LoginProvider] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230530130500_Initial', N'5.0.11')
GO
SET IDENTITY_INSERT [dbo].[Authors] ON 

INSERT [dbo].[Authors] ([Id], [Name], [Description]) VALUES (1, N'Jimmy Chin', N'Jimmy Chin is an Academy Award-winning filmmaker, National Geographic photographer, and professional mountain sports athlete. He has led or participated in cutting-edge expeditions around the world for over twenty years, including significant first ascents on all seven continents, and his photographs have graced the covers of National Geographic Magazine and the New York Times Magazine')
INSERT [dbo].[Authors] ([Id], [Name], [Description]) VALUES (2, N'Joanna Ho', N'Joanna Ho is passionate about equity in books and education. She has been an English teacher, a dean, and a teacher professional development mastermind. She is currently the vice principal of a high school in the San Francisco Bay Area. Homemade chocolate chip cookies, outdoor adventures, and dance parties with her kids make Joanna''s eyes crinkle into crescent moons. Her books for young readers include Eyes That Kiss in the Corners. Visit her at www.joannahowrites.com and @JoannaHoWrites.')
INSERT [dbo].[Authors] ([Id], [Name], [Description]) VALUES (3, N'Stephen King', N'Stephen King is the author of more than sixty books, all of them worldwide bestsellers. His recent work includes Billy Summers, If It Bleeds, The Institute, Elevation, The Outsider, Sleeping Beauties (cowritten with his son Owen King), and the Bill Hodges trilogy: End of Watch, Finders Keepers, and Mr. Mercedes (an Edgar Award winner for Best Novel and a television series streaming on Peacock)')
INSERT [dbo].[Authors] ([Id], [Name], [Description]) VALUES (4, N'Sanmao', N'Sanmao, born Chen Mao Ping, was a novelist, writer, educator and translator. Born in China in 1943, she grew up in Taiwan. She studied in Taiwan, Spain and Germany before moving to the Sahara desert with her Spanish husband, a scuba diver and underwater engineer. In 1976, she gained fame with the publication of her first book, Stories of the Sahara. Her husband died while diving in 1979, and Sanmao returned to Taiwan the following year.')
INSERT [dbo].[Authors] ([Id], [Name], [Description]) VALUES (5, N'Kathe Koja', N'Kathe Koja writes novels and short fiction, and creates and produces immersive fiction performances, both solo and with a rotating ensemble of artists.')
INSERT [dbo].[Authors] ([Id], [Name], [Description]) VALUES (6, N'Rebecca Roanhorse', N'Rebecca Roanhorse is the New York Times bestselling author of Trail of Lightning, Storm of Locusts, Black Sun, and Star Wars: Resistance Reborn. She has won the Nebula, Hugo, and Locus Awards for her fiction, and was the recipient of the 2018 Astounding (formerly Campbell) Award for Best New Writer. The next book in her Between Earth and Sky series, Fevered Star, is out in March 2022. She lives in New Mexico with her family.')
INSERT [dbo].[Authors] ([Id], [Name], [Description]) VALUES (7, N'Nathaniel Rich', N'Nathaniel Rich is the author of the novels King Zeno, Odds Against Tomorrow, and The Mayor''s Tongue. He is a writer at large for The New York Times Magazine and a regular contributor to The Atlantic and The New York Review of Books. He lives in New Orleans.')
INSERT [dbo].[Authors] ([Id], [Name], [Description]) VALUES (8, N'Kate Pentecost', N'Kate Pentecost was born and raised on the Texas/Louisiana state line, where there are more churches, pine trees, and alligators than anything else. She is a stalwart advocate of the weird, and has an MFA in Writing for Children and Young Adults from Vermont College of Fine Arts. Elysium Girls is her debut.')
INSERT [dbo].[Authors] ([Id], [Name], [Description]) VALUES (9, N'Laura Roberts', N'aura Roberts writes about sex, travel, writing, and ninjas - though not necessarily in that order. As the author of the ''V for Vixen'' sex column, Laura began her career chronicling Montrealer''s sexcapades, which are collected together in her book of essays, The Vixen Files. Blurring the lines between fact and fiction, she''s also penned Confessions of a 3-Day Novelist, Ninjas of the 512, parts one and two of her serial novel, Naked Montreal, and a wide assortment of erotic Quickies')
INSERT [dbo].[Authors] ([Id], [Name], [Description]) VALUES (10, N'Margaret Weis', N'Margaret Weis published their first novel in the Dragonlance Chronicles, Dragons of Autumn Twilight, in 1984. Over twenty years later, they are going strong as partners--with over thirty novels as collaborators--and have published over a hundred books, including novels, collections of short stories, role-playing games, and other game products alone or with other co-authors')
SET IDENTITY_INSERT [dbo].[Authors] OFF
GO
INSERT [dbo].[BookCategories] ([BookId], [CategoryId]) VALUES (1, 10)
INSERT [dbo].[BookCategories] ([BookId], [CategoryId]) VALUES (2, 9)
INSERT [dbo].[BookCategories] ([BookId], [CategoryId]) VALUES (3, 8)
INSERT [dbo].[BookCategories] ([BookId], [CategoryId]) VALUES (4, 6)
INSERT [dbo].[BookCategories] ([BookId], [CategoryId]) VALUES (5, 6)
INSERT [dbo].[BookCategories] ([BookId], [CategoryId]) VALUES (6, 4)
GO
SET IDENTITY_INSERT [dbo].[Books] ON 

INSERT [dbo].[Books] ([Id], [Title], [AuthorId], [PublisherId], [Summary], [PhotoUrl], [PhotoPublicId], [Price], [Quantity], [CreatedDate], [ModifiedDate], [IsActice]) VALUES (1, N'San Diego from A to Z: An Alphabetical Guide', 9, 4, N'<p>Tired of the same old guidebooks? Learn where to go and what to do from a local! This alphabetical city guide looks at San Diego - and tourism - from a whole new angle, letting readers browse the city at their own pace. Learn about... * Local favorites * Tourist attractions * Cultural oddities * And enjoy unique trivia you just won''t get from the other guys! Whether you''re a first-time visitor or a life-long resident, San Diego from A to Z will surprise and delight you with plenty of facts, figures and personal experiences from author Laura Roberts. Explore alphabetically as you tour America''s Finest City, starting at the Birch Aquarium and ending with Spanish phrases that begin with the letter Z. Learn more about San Diego landmarks, eateries, bars, museums, bookstores, neighborhoods, cultural oddities and much more. A must-have for the discerning traveler or seasoned flâneur. Find out what you''ve been missing in San Diego and order your copy today.</p>', N'https://m.media-amazon.com/images/I/41+wnvr23nL._SY344_BO1,204,203,200_.jpg', N'BookStore/bookcovers/san_dghfys', 15.8500, 20, CAST(N'2023-05-30T13:04:59.8410098' AS DateTime2), NULL, 1)
INSERT [dbo].[Books] ([Id], [Title], [AuthorId], [PublisherId], [Summary], [PhotoUrl], [PhotoPublicId], [Price], [Quantity], [CreatedDate], [ModifiedDate], [IsActice]) VALUES (2, N'Elysium Girls', 8, 3, N'<p>A lush, dazzlingly original young adult fantasy about an epic clash of witches, gods, and demons. Elysium, Oklahoma, is a town like any other. Respectable. God-fearing. Praying for an end to the Dust Bowl.Until the day the people of Elysium are chosen by two sisters: Life and Death.And the Sisters like to gamble against each other with things like time, and space, and human lives.Elysium is to become the gameboard in a ruthless competition between the goddesses.The Dust Soldiers will return in ten years'' time, and if the people of Elysium have not proved themselves worthy, all will be slain. Nearly ten years later, seventeen-year - old Sal Wilkinson is called upon to lead Elysium as it prepares for the end of the game.But then an outsider named Asa arrives at Elysium''s gates with nothing more than a sharp smile and a bag of magic tricks, and they trigger a terrible accident that gets both Sal and Asa exiled into the brutal Desert of Dust and Steel. There Sal and Asa stumble upon a gang of girls headed by another exile: a young witch everyone in Elysium believes to be dead. As the apocalypse looms, they must do more than simply tip the scales in Elysium''s favor-only by reinventing the rules can they beat Life and Death at their own game in this exciting fantasy debut.</p>', N'https://m.media-amazon.com/images/I/51EvjgbuuKL._SY291_BO1,204,203,200_QL40_FMwebp_.jpg', N'BookStore/bookcovers/elisium_nudddg', 27.5500, 50, CAST(N'2023-05-30T13:04:59.8410098' AS DateTime2), NULL, 1)
INSERT [dbo].[Books] ([Id], [Title], [AuthorId], [PublisherId], [Summary], [PhotoUrl], [PhotoPublicId], [Price], [Quantity], [CreatedDate], [ModifiedDate], [IsActice]) VALUES (3, N'Black Sun, 1', 6, 1, N'<p>In the holy city of Tova, the winter solstice is usually a time for celebration and renewal, but this year it coincides with a solar eclipse, a rare celestial event proscribed by the Sun Priest as an unbalancing of the world. Meanwhile, a ship launches from a distant city bound for Tova and set to arrive on the solstice. The captain of the ship, Xiala, is a disgraced Teek whose song can calm the waters around her as easily as it can warp a man''s mind. Her ship carries one passenger. Described as harmless, the passenger, Serapio, is a young man, blind, scarred, and cloaked in destiny. As Xiala well knows, when a man is described as harmless, he usually ends up being a villain. Crafted with unforgettable characters, Rebecca Roanhorse has created an epic adventure exploring the decadence of power amidst the weight of history and the struggle of individuals swimming against the confines of society and their broken pasts in the most original series debut of the decade.</p>', N'https://m.media-amazon.com/images/I/510m8UDDErL._SX330_BO1,204,203,200_.jpg', N'BookStore/bookcovers/71ZMYPkoNLL_y2she6', 25.7500, 30, CAST(N'2023-05-30T13:04:59.8410098' AS DateTime2), NULL, 1)
INSERT [dbo].[Books] ([Id], [Title], [AuthorId], [PublisherId], [Summary], [PhotoUrl], [PhotoPublicId], [Price], [Quantity], [CreatedDate], [ModifiedDate], [IsActice]) VALUES (4, N'The Shining', 3, 3, N'<p>Jack Torrance''s new job at the Overlook Hotel is the perfect chance for a fresh start. As the off-season caretaker at the atmospheric old hotel, he''ll have plenty of time to spend reconnecting with his family and working on his writing. But as the harsh winter weather sets in, the idyllic location feels ever more remote . . . and more sinister. And the only one to notice the strange and terrible forces gathering around the Overlook is Danny Torrance, a uniquely gifted five-year-old.</p>', N'https://m.media-amazon.com/images/I/51T0YJNyKqL.jpg', N'BookStore/bookcovers/9780345806789_p0_v2_s1200x630_a9ruho', 36.8000, 55, CAST(N'2023-05-30T13:04:59.8410098' AS DateTime2), CAST(N'2023-06-15T14:33:03.3427368' AS DateTime2), 1)
INSERT [dbo].[Books] ([Id], [Title], [AuthorId], [PublisherId], [Summary], [PhotoUrl], [PhotoPublicId], [Price], [Quantity], [CreatedDate], [ModifiedDate], [IsActice]) VALUES (5, N'The Dark Tower I, 1: The Gunslinger', 3, 1, N'<p>An impressive work of mythic magnitude that may turn out to be Stephen King''s greatest literary achievement (The Atlanta Journal-Constitution), The Gunslinger</p>', N'https://m.media-amazon.com/images/I/61Bne-UJNHL.jpg', N'BookStore/bookcovers/715w3hVk-dL_hvnmbd', 28.2000, 45, CAST(N'2023-05-30T13:04:59.8410098' AS DateTime2), NULL, 1)
INSERT [dbo].[Books] ([Id], [Title], [AuthorId], [PublisherId], [Summary], [PhotoUrl], [PhotoPublicId], [Price], [Quantity], [CreatedDate], [ModifiedDate], [IsActice]) VALUES (6, N'Dragons of Autumn Twilight', 10, 4, N'<p>Once merely creatures of legend, the dragons have returned to Krynn. But with their arrival comes the departure of the old gods--and all healing magic. As war threatens to engulf the land, lifelong friends reunite for an adventure that will change their lives and shape their world forever... When Tanis, Sturm, Caramon, Raistlin, Flint, and Tasslehoff see a woman use a blue crystal staff to heal a villager, they wonder if it''s a sign the gods have not abandoned them after all. Fueled by this glimmer of hope, the Companions band together to uncover the truth behind the gods'' absence--though they aren''t the only ones with an interest in the staff. The Seekers, a new religious order, wants the artifact for their own ends, believing it will help them replace the gods and overtake the continent of Ansalon. Now, the Companions must assume the unlikely roles of heroes if they hope to prevent the staff from falling into the hands of darkness.</p>', N'https://m.media-amazon.com/images/I/61B-eksENbL.jpg', N'BookStore/bookcovers/81Mtr5rXctL_bpihhq', 19.9900, 25, CAST(N'2023-05-30T13:04:59.8410098' AS DateTime2), NULL, 1)
SET IDENTITY_INSERT [dbo].[Books] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (1, N'Arts & Photography', NULL)
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (2, N'Children''s Books', NULL)
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (3, N'Comics & Graphic Novels', NULL)
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (4, N'Fantasy', NULL)
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (5, N'History', NULL)
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (6, N'Horror', NULL)
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (7, N'Science Fiction', NULL)
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (8, N'Science & Technology', NULL)
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (9, N'Teen & Young Adult', NULL)
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (10, N'Travel', NULL)
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Publishers] ON 

INSERT [dbo].[Publishers] ([Id], [Name], [Description]) VALUES (1, N'Hanover Square Press', N'Launched in 2017, Hanover Square Press aims to publish compelling, original fiction and narrative nonfiction—the kind of books that keep you up all night reading and that you want to talk about the next morning. The imprint draws its name from the square that was once known as “Printing House Square” for the American printers, publishers, and booksellers who thrived there in the 1700s and early 1800s.')
INSERT [dbo].[Publishers] ([Id], [Name], [Description]) VALUES (2, N'Grand Central Publishing', N'Grand Central Publishing, formerly Warner Books, came into existence in 1970 when Warner Communications acquired the Paperback Library, subsequently publishing paperback reprints editions of such bestsellers as Harper Lee''s To Kill A Mockingbird and Umberto Eco''s The Name Of The Rose. Today Grand Central Publishing reaches a diverse audience through hardcover, trade paperback and mass market imprints that cater to every kind of reader.')
INSERT [dbo].[Publishers] ([Id], [Name], [Description]) VALUES (3, N'Copper Canyon Press', N'Copper Canyon Press is a nonprofit, independent poetry publisher based in Port Townsend, WA. We believe poetry is vital to language and living. Since 1972, Copper Canyon has published extraordinary poetry from around the world to engage the imaginations and intellects of readers.')
INSERT [dbo].[Publishers] ([Id], [Name], [Description]) VALUES (4, N'Createspace Independent Publishing Platform', N'On-Demand Publishing, LLC, doing business as CreateSpace, is a self-publishing service owned by Amazon. The company was founded in 2000 in South Carolina as BookSurge and was acquired by Amazon in 2005.')
SET IDENTITY_INSERT [dbo].[Publishers] OFF
GO
INSERT [dbo].[Roles] ([Id], [Description], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'8d04dce2-969a-435d-bba4-df3f325983dc', N'Normal User is restricted some rights', N'NormalUser', N'NORMALUSER', N'f2a01991-4b84-4df5-8139-6046e55a51ef')
INSERT [dbo].[Roles] ([Id], [Description], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'69bd714f-9576-45ba-b5b7-f00649be00de', N'Site Owner has all right of website', N'SiteOwner', N'SITEOWNER', N'1f6ca8c7-1f6c-4233-b471-d96df02bd5c9')
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'bfba0d90-a701-4cea-520f-08db68ee13b8', N'8d04dce2-969a-435d-bba4-df3f325983dc')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'c57e5f5c-d954-406a-d3f0-08db6967ea33', N'8d04dce2-969a-435d-bba4-df3f325983dc')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'7e0a9c6e-6e3f-43e3-9ae5-08db696a025b', N'8d04dce2-969a-435d-bba4-df3f325983dc')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'9883fe27-43ce-40bd-036a-08db713e2f1e', N'8d04dce2-969a-435d-bba4-df3f325983dc')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'62f6b2ff-acc9-46f4-f7f1-08db713f3a66', N'8d04dce2-969a-435d-bba4-df3f325983dc')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'd857c54d-0c23-4a86-577c-08db7144ccf8', N'8d04dce2-969a-435d-bba4-df3f325983dc')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'9977fad2-d8b9-4ac4-a73d-14cbf64ed65c', N'69bd714f-9576-45ba-b5b7-f00649be00de')
GO
INSERT [dbo].[Users] ([Id], [DisplayName], [Avatar], [AvatarPublicId], [Status], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'bfba0d90-a701-4cea-520f-08db68ee13b8', N'Guest', N'https://res.cloudinary.com/dglgzh0aj/image/upload/v1634549506/BookStore/users/default/default_avatar.jpg', N'BookStore/users/default/default_avatar', 1, N'carong904@gmail.com', N'CARONG904@GMAIL.COM', N'carong904@gmail.com', N'CARONG904@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEDlbqRIo5EejRkbnDJhTgXRbMurJK1iTkP5WlJDG0XXb0O9U9N0+3aUIHxqf9wGaXQ==', N'SPF4P2SHDN4GPOS4DDJOZGR5AYOVA663', N'475db0d5-48c1-44f4-b465-d702e03a0435', N'0328262315', 0, 0, NULL, 1, 0)
INSERT [dbo].[Users] ([Id], [DisplayName], [Avatar], [AvatarPublicId], [Status], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'9977fad2-d8b9-4ac4-a73d-14cbf64ed65c', N'Hà Hải Long', N'https://img.freepik.com/free-vector/flat-design-library-logo-template_23-2149325326.jpg?w=2000', N'BookStore/users/flat-design-library-logo-template_23-2149325326.jpg?w=2000', 1, N'hailongsn99@gmail.com', N'HAILONGSN99@GMAIL.COM', N'hailongsn99@gmail.com', N'HAILONGSN99@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEDlbqRIo5EejRkbnDJhTgXRbMurJK1iTkP5WlJDG0XXb0O9U9N0+3aUIHxqf9wGaXQ==', N'DANWBR7Y3CR4JYY23CSWLBRKCGAAT6MH', N'486768eb-02b4-447a-871b-d49ab60f37bd', N'0328262315', 1, 0, NULL, 0, 0)
GO
/****** Object:  Index [IX_BookCategories_BookId]    Script Date: 6/22/2023 9:49:06 AM ******/
CREATE NONCLUSTERED INDEX [IX_BookCategories_BookId] ON [dbo].[BookCategories]
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Books_AuthorId]    Script Date: 6/22/2023 9:49:06 AM ******/
CREATE NONCLUSTERED INDEX [IX_Books_AuthorId] ON [dbo].[Books]
(
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Books_PublisherId]    Script Date: 6/22/2023 9:49:06 AM ******/
CREATE NONCLUSTERED INDEX [IX_Books_PublisherId] ON [dbo].[Books]
(
	[PublisherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Comments_BookId]    Script Date: 6/22/2023 9:49:06 AM ******/
CREATE NONCLUSTERED INDEX [IX_Comments_BookId] ON [dbo].[Comments]
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Comments_UserId]    Script Date: 6/22/2023 9:49:06 AM ******/
CREATE NONCLUSTERED INDEX [IX_Comments_UserId] ON [dbo].[Comments]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RefreshTokens_UserId]    Script Date: 6/22/2023 9:49:06 AM ******/
CREATE NONCLUSTERED INDEX [IX_RefreshTokens_UserId] ON [dbo].[RefreshTokens]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Books] ADD  DEFAULT ((0.0)) FOR [Price]
GO
ALTER TABLE [dbo].[Books] ADD  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[Books] ADD  DEFAULT ('2023-05-30T13:04:59.8410098Z') FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Comments] ADD  DEFAULT ('2023-05-30T13:04:59.8459490Z') FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (CONVERT([bit],(1))) FOR [Status]
GO
ALTER TABLE [dbo].[BookCategories]  WITH CHECK ADD  CONSTRAINT [FK_BookCategories_Books_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookCategories] CHECK CONSTRAINT [FK_BookCategories_Books_BookId]
GO
ALTER TABLE [dbo].[BookCategories]  WITH CHECK ADD  CONSTRAINT [FK_BookCategories_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookCategories] CHECK CONSTRAINT [FK_BookCategories_Categories_CategoryId]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Authors_AuthorId] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Authors] ([Id])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Authors_AuthorId]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Publishers_PublisherId] FOREIGN KEY([PublisherId])
REFERENCES [dbo].[Publishers] ([Id])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Publishers_PublisherId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Books_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Books_BookId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Users_UserId]
GO
ALTER TABLE [dbo].[RefreshTokens]  WITH CHECK ADD  CONSTRAINT [FK_RefreshTokens_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[RefreshTokens] CHECK CONSTRAINT [FK_RefreshTokens_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [BookStore] SET  READ_WRITE 
GO
