using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStoreManagement.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    AvatarPublicId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorId = table.Column<int>(type: "int", nullable: true),
                    PublisherId = table.Column<int>(type: "int", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PhotoPublicId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "Money", nullable: false, defaultValue: 0m),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 5, 30, 13, 4, 59, 841, DateTimeKind.Utc).AddTicks(98)),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActice = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Books_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Revoked = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookCategories",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCategories", x => new { x.CategoryId, x.BookId });
                    table.ForeignKey(
                        name: "FK_BookCategories_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AnonymousName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 5, 30, 13, 4, 59, 845, DateTimeKind.Utc).AddTicks(9490)),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Jimmy Chin is an Academy Award-winning filmmaker, National Geographic photographer, and professional mountain sports athlete. He has led or participated in cutting-edge expeditions around the world for over twenty years, including significant first ascents on all seven continents, and his photographs have graced the covers of National Geographic Magazine and the New York Times Magazine", "Jimmy Chin" },
                    { 2, "Joanna Ho is passionate about equity in books and education. She has been an English teacher, a dean, and a teacher professional development mastermind. She is currently the vice principal of a high school in the San Francisco Bay Area. Homemade chocolate chip cookies, outdoor adventures, and dance parties with her kids make Joanna's eyes crinkle into crescent moons. Her books for young readers include Eyes That Kiss in the Corners. Visit her at www.joannahowrites.com and @JoannaHoWrites.", "Joanna Ho" },
                    { 3, "Stephen King is the author of more than sixty books, all of them worldwide bestsellers. His recent work includes Billy Summers, If It Bleeds, The Institute, Elevation, The Outsider, Sleeping Beauties (cowritten with his son Owen King), and the Bill Hodges trilogy: End of Watch, Finders Keepers, and Mr. Mercedes (an Edgar Award winner for Best Novel and a television series streaming on Peacock)", "Stephen King" },
                    { 4, "Sanmao, born Chen Mao Ping, was a novelist, writer, educator and translator. Born in China in 1943, she grew up in Taiwan. She studied in Taiwan, Spain and Germany before moving to the Sahara desert with her Spanish husband, a scuba diver and underwater engineer. In 1976, she gained fame with the publication of her first book, Stories of the Sahara. Her husband died while diving in 1979, and Sanmao returned to Taiwan the following year.", "Sanmao" },
                    { 5, "Kathe Koja writes novels and short fiction, and creates and produces immersive fiction performances, both solo and with a rotating ensemble of artists.", "Kathe Koja" },
                    { 6, "Rebecca Roanhorse is the New York Times bestselling author of Trail of Lightning, Storm of Locusts, Black Sun, and Star Wars: Resistance Reborn. She has won the Nebula, Hugo, and Locus Awards for her fiction, and was the recipient of the 2018 Astounding (formerly Campbell) Award for Best New Writer. The next book in her Between Earth and Sky series, Fevered Star, is out in March 2022. She lives in New Mexico with her family.", "Rebecca Roanhorse" },
                    { 7, "Nathaniel Rich is the author of the novels King Zeno, Odds Against Tomorrow, and The Mayor's Tongue. He is a writer at large for The New York Times Magazine and a regular contributor to The Atlantic and The New York Review of Books. He lives in New Orleans.", "Nathaniel Rich" },
                    { 8, "Kate Pentecost was born and raised on the Texas/Louisiana state line, where there are more churches, pine trees, and alligators than anything else. She is a stalwart advocate of the weird, and has an MFA in Writing for Children and Young Adults from Vermont College of Fine Arts. Elysium Girls is her debut.", "Kate Pentecost" },
                    { 9, "aura Roberts writes about sex, travel, writing, and ninjas - though not necessarily in that order. As the author of the 'V for Vixen' sex column, Laura began her career chronicling Montrealer's sexcapades, which are collected together in her book of essays, The Vixen Files. Blurring the lines between fact and fiction, she's also penned Confessions of a 3-Day Novelist, Ninjas of the 512, parts one and two of her serial novel, Naked Montreal, and a wide assortment of erotic Quickies", "Laura Roberts" },
                    { 10, "Margaret Weis published their first novel in the Dragonlance Chronicles, Dragons of Autumn Twilight, in 1984. Over twenty years later, they are going strong as partners--with over thirty novels as collaborators--and have published over a hundred books, including novels, collections of short stories, role-playing games, and other game products alone or with other co-authors", "Margaret Weis" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 10, null, "Travel" },
                    { 9, null, "Teen & Young Adult" },
                    { 8, null, "Science & Technology" },
                    { 7, null, "Science Fiction" },
                    { 6, null, "Horror" },
                    { 4, null, "Fantasy" },
                    { 3, null, "Comics & Graphic Novels" },
                    { 2, null, "Children's Books" },
                    { 1, null, "Arts & Photography" },
                    { 5, null, "History" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Launched in 2017, Hanover Square Press aims to publish compelling, original fiction and narrative nonfiction—the kind of books that keep you up all night reading and that you want to talk about the next morning. The imprint draws its name from the square that was once known as “Printing House Square” for the American printers, publishers, and booksellers who thrived there in the 1700s and early 1800s.", "Hanover Square Press" },
                    { 2, "Grand Central Publishing, formerly Warner Books, came into existence in 1970 when Warner Communications acquired the Paperback Library, subsequently publishing paperback reprints editions of such bestsellers as Harper Lee's To Kill A Mockingbird and Umberto Eco's The Name Of The Rose. Today Grand Central Publishing reaches a diverse audience through hardcover, trade paperback and mass market imprints that cater to every kind of reader.", "Grand Central Publishing" },
                    { 3, "Copper Canyon Press is a nonprofit, independent poetry publisher based in Port Townsend, WA. We believe poetry is vital to language and living. Since 1972, Copper Canyon has published extraordinary poetry from around the world to engage the imaginations and intellects of readers.", "Copper Canyon Press" },
                    { 4, "On-Demand Publishing, LLC, doing business as CreateSpace, is a self-publishing service owned by Amazon. The company was founded in 2000 in South Carolina as BookSurge and was acquired by Amazon in 2005.", "Createspace Independent Publishing Platform" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"), "1f6ca8c7-1f6c-4233-b471-d96df02bd5c9", "Site Owner has all right of website", "SiteOwner", null },
                    { new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"), "f2a01991-4b84-4df5-8139-6046e55a51ef", "Normal User is restricted some rights", "NormalUser", null }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"), new Guid("9977fad2-d8b9-4ac4-a73d-14cbf64ed65c") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "AvatarPublicId", "ConcurrencyStamp", "DisplayName", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("9977fad2-d8b9-4ac4-a73d-14cbf64ed65c"), 0, "https://img.freepik.com/free-vector/flat-design-library-logo-template_23-2149325326.jpg?w=2000", "BookStore/users/flat-design-library-logo-template_23-2149325326.jpg?w=2000", "88554467-4552-4956-b6dc-b2ba681890f9", "Hà Hải Long", "hailongsn99@gmail.com", true, false, null, null, null, "AQAAAAEAACcQAAAAEF2VIoLU2t5VbHOZvIClem25wb4G+ITkbSkmFU2lrpAp1sZWJ0/RLhaqKgK532Qv8Q==", "0375274267", true, "", false, "hailongsn99@gmail.com" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "IsActice", "ModifiedDate", "PhotoPublicId", "PhotoUrl", "Price", "PublisherId", "Quantity", "Summary", "Title" },
                values: new object[,]
                {
                    { 3, 6, true, null, "BookStore/bookcovers/71ZMYPkoNLL_y2she6", "https://res.cloudinary.com/dglgzh0aj/image/upload/v1634555700/BookStore/bookcovers/71ZMYPkoNLL_y2she6.jpg", 25.75m, 1, 30, "<p>In the holy city of Tova, the winter solstice is usually a time for celebration and renewal, but this year it coincides with a solar eclipse, a rare celestial event proscribed by the Sun Priest as an unbalancing of the world. Meanwhile, a ship launches from a distant city bound for Tova and set to arrive on the solstice. The captain of the ship, Xiala, is a disgraced Teek whose song can calm the waters around her as easily as it can warp a man's mind. Her ship carries one passenger. Described as harmless, the passenger, Serapio, is a young man, blind, scarred, and cloaked in destiny. As Xiala well knows, when a man is described as harmless, he usually ends up being a villain. Crafted with unforgettable characters, Rebecca Roanhorse has created an epic adventure exploring the decadence of power amidst the weight of history and the struggle of individuals swimming against the confines of society and their broken pasts in the most original series debut of the decade.</p>", "Black Sun, 1" },
                    { 5, 3, true, null, "BookStore/bookcovers/715w3hVk-dL_hvnmbd", "https://res.cloudinary.com/dglgzh0aj/image/upload/v1634556342/BookStore/bookcovers/715w3hVk-dL_hvnmbd.jpg", 28.20m, 1, 45, "<p>An impressive work of mythic magnitude that may turn out to be Stephen King's greatest literary achievement (The Atlanta Journal-Constitution), The Gunslinger</p>", "The Dark Tower I, 1: The Gunslinger" },
                    { 2, 8, true, null, "BookStore/bookcovers/elisium_nudddg", "https://res.cloudinary.com/dglgzh0aj/image/upload/v1634555234/BookStore/bookcovers/elisium_nudddg.jpg", 27.55m, 3, 50, "<p>A lush, dazzlingly original young adult fantasy about an epic clash of witches, gods, and demons. Elysium, Oklahoma, is a town like any other. Respectable. God-fearing. Praying for an end to the Dust Bowl.Until the day the people of Elysium are chosen by two sisters: Life and Death.And the Sisters like to gamble against each other with things like time, and space, and human lives.Elysium is to become the gameboard in a ruthless competition between the goddesses.The Dust Soldiers will return in ten years' time, and if the people of Elysium have not proved themselves worthy, all will be slain. Nearly ten years later, seventeen-year - old Sal Wilkinson is called upon to lead Elysium as it prepares for the end of the game.But then an outsider named Asa arrives at Elysium's gates with nothing more than a sharp smile and a bag of magic tricks, and they trigger a terrible accident that gets both Sal and Asa exiled into the brutal Desert of Dust and Steel. There Sal and Asa stumble upon a gang of girls headed by another exile: a young witch everyone in Elysium believes to be dead. As the apocalypse looms, they must do more than simply tip the scales in Elysium's favor-only by reinventing the rules can they beat Life and Death at their own game in this exciting fantasy debut.</p>", "Elysium Girls" },
                    { 4, 3, true, null, "BookStore/bookcovers/9780345806789_p0_v2_s1200x630_a9ruho", "https://res.cloudinary.com/dglgzh0aj/image/upload/v1634556141/BookStore/bookcovers/9780345806789_p0_v2_s1200x630_a9ruho.jpg", 36.80m, 3, 55, "<p>Jack Torrance's new job at the Overlook Hotel is the perfect chance for a fresh start. As the off-season caretaker at the atmospheric old hotel, he'll have plenty of time to spend reconnecting with his family and working on his writing. But as the harsh winter weather sets in, the idyllic location feels ever more remote . . . and more sinister. And the only one to notice the strange and terrible forces gathering around the Overlook is Danny Torrance, a uniquely gifted five-year-old.</p>", "The Shining" },
                    { 1, 9, true, null, "BookStore/bookcovers/san_dghfys", "https://res.cloudinary.com/dglgzh0aj/image/upload/v1634554755/BookStore/bookcovers/san_dghfys.jpg", 15.85m, 4, 20, "<p>Tired of the same old guidebooks? Learn where to go and what to do from a local! This alphabetical city guide looks at San Diego - and tourism - from a whole new angle, letting readers browse the city at their own pace. Learn about... * Local favorites * Tourist attractions * Cultural oddities * And enjoy unique trivia you just won't get from the other guys! Whether you're a first-time visitor or a life-long resident, San Diego from A to Z will surprise and delight you with plenty of facts, figures and personal experiences from author Laura Roberts. Explore alphabetically as you tour America's Finest City, starting at the Birch Aquarium and ending with Spanish phrases that begin with the letter Z. Learn more about San Diego landmarks, eateries, bars, museums, bookstores, neighborhoods, cultural oddities and much more. A must-have for the discerning traveler or seasoned flâneur. Find out what you've been missing in San Diego and order your copy today.</p>", "San Diego from A to Z: An Alphabetical Guide" },
                    { 6, 10, true, null, "BookStore/bookcovers/81Mtr5rXctL_bpihhq", "https://res.cloudinary.com/dglgzh0aj/image/upload/v1634556692/BookStore/bookcovers/81Mtr5rXctL_bpihhq.jpg", 19.99m, 4, 25, "<p>Once merely creatures of legend, the dragons have returned to Krynn. But with their arrival comes the departure of the old gods--and all healing magic. As war threatens to engulf the land, lifelong friends reunite for an adventure that will change their lives and shape their world forever... When Tanis, Sturm, Caramon, Raistlin, Flint, and Tasslehoff see a woman use a blue crystal staff to heal a villager, they wonder if it's a sign the gods have not abandoned them after all. Fueled by this glimmer of hope, the Companions band together to uncover the truth behind the gods' absence--though they aren't the only ones with an interest in the staff. The Seekers, a new religious order, wants the artifact for their own ends, believing it will help them replace the gods and overtake the continent of Ansalon. Now, the Companions must assume the unlikely roles of heroes if they hope to prevent the staff from falling into the hands of darkness.</p>", "Dragons of Autumn Twilight" }
                });

            migrationBuilder.InsertData(
                table: "BookCategories",
                columns: new[] { "BookId", "CategoryId" },
                values: new object[,]
                {
                    { 3, 8 },
                    { 5, 6 },
                    { 2, 9 },
                    { 4, 6 },
                    { 1, 10 },
                    { 6, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookCategories_BookId",
                table: "BookCategories",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BookId",
                table: "Comments",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookCategories");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
