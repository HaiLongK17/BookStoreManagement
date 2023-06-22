using BookStoreManagement.Core.Entities;
using BookStoreManagement.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManagement.Data
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Publisher>().HasData(
               new Publisher
               {
                   Id=1,
                   Name = "Hanover Square Press",
                   Description = "Launched in 2017, Hanover Square Press aims to publish compelling, original fiction and narrative nonfiction—the kind of books that keep you up all night reading and that you want to talk about the next morning. The imprint draws its name from the square that was once known as “Printing House Square” for the American printers, publishers, and booksellers who thrived there in the 1700s and early 1800s."
               },
                    new Publisher
                    {
                        Id = 2,
                        Name = "Grand Central Publishing",
                        Description = "Grand Central Publishing, formerly Warner Books, came into existence in 1970 when Warner Communications acquired the Paperback Library, subsequently publishing paperback reprints editions of such bestsellers as Harper Lee's To Kill A Mockingbird and Umberto Eco's The Name Of The Rose. Today Grand Central Publishing reaches a diverse audience through hardcover, trade paperback and mass market imprints that cater to every kind of reader."
                    },
                    new Publisher
                    {
                        Id = 3,
                        Name = "Copper Canyon Press",
                        Description = "Copper Canyon Press is a nonprofit, independent poetry publisher based in Port Townsend, WA. We believe poetry is vital to language and living. Since 1972, Copper Canyon has published extraordinary poetry from around the world to engage the imaginations and intellects of readers."
                    },
                    new Publisher
                    {
                        Id = 4,
                        Name = "Createspace Independent Publishing Platform",
                        Description = "On-Demand Publishing, LLC, doing business as CreateSpace, is a self-publishing service owned by Amazon. The company was founded in 2000 in South Carolina as BookSurge and was acquired by Amazon in 2005."
                    }
               );
           

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Arts & Photography",
                },
                    new Category
                    {
                        Id = 2,
                        Name = "Children's Books",
                    },
                    new Category
                    {
                        Id = 3,
                        Name = "Comics & Graphic Novels",
                    },
                    new Category
                    {
                        Id = 4,
                        Name = "Fantasy",
                    },
                    new Category
                    {
                        Id = 5,
                        Name = "History",
                    },
                    new Category
                    {
                        Id = 6,
                        Name = "Horror",
                    },
                    new Category
                    {
                        Id = 7,
                        Name = "Science Fiction",
                    },
                    new Category
                    {
                        Id = 8,
                        Name = "Science & Technology",
                    },
                    new Category
                    {
                        Id = 9,
                        Name = "Teen & Young Adult",
                    },
                    new Category
                    {
                        Id = 10,
                        Name = "Travel",
                    });

            modelBuilder.Entity<Author>().HasData(
                  new Author
                  {
                      Id = 1,
                      Name = "Jimmy Chin",
                      Description = "Jimmy Chin is an Academy Award-winning filmmaker, National Geographic photographer, and professional mountain sports athlete. He has led or participated in cutting-edge expeditions around the world for over twenty years, including significant first ascents on all seven continents, and his photographs have graced the covers of National Geographic Magazine and the New York Times Magazine"
                  },
                    new Author
                    {
                        Id = 2,
                        Name = "Joanna Ho",
                        Description = "Joanna Ho is passionate about equity in books and education. She has been an English teacher, a dean, and a teacher professional development mastermind. She is currently the vice principal of a high school in the San Francisco Bay Area. Homemade chocolate chip cookies, outdoor adventures, and dance parties with her kids make Joanna's eyes crinkle into crescent moons. Her books for young readers include Eyes That Kiss in the Corners. Visit her at www.joannahowrites.com and @JoannaHoWrites."
                    },
                    new Author
                    {
                        Id = 3,
                        Name = "Stephen King",
                        Description = "Stephen King is the author of more than sixty books, all of them worldwide bestsellers. His recent work includes Billy Summers, If It Bleeds, The Institute, Elevation, The Outsider, Sleeping Beauties (cowritten with his son Owen King), and the Bill Hodges trilogy: End of Watch, Finders Keepers, and Mr. Mercedes (an Edgar Award winner for Best Novel and a television series streaming on Peacock)"
                    },
                    new Author
                    {
                        Id = 4,
                        Name = "Sanmao",
                        Description = "Sanmao, born Chen Mao Ping, was a novelist, writer, educator and translator. Born in China in 1943, she grew up in Taiwan. She studied in Taiwan, Spain and Germany before moving to the Sahara desert with her Spanish husband, a scuba diver and underwater engineer. In 1976, she gained fame with the publication of her first book, Stories of the Sahara. Her husband died while diving in 1979, and Sanmao returned to Taiwan the following year."
                    },
                    new Author
                    {
                        Id = 5,
                        Name = "Kathe Koja",
                        Description = "Kathe Koja writes novels and short fiction, and creates and produces immersive fiction performances, both solo and with a rotating ensemble of artists."
                    },
                    new Author
                    {
                        Id = 6,
                        Name = "Rebecca Roanhorse",
                        Description = "Rebecca Roanhorse is the New York Times bestselling author of Trail of Lightning, Storm of Locusts, Black Sun, and Star Wars: Resistance Reborn. She has won the Nebula, Hugo, and Locus Awards for her fiction, and was the recipient of the 2018 Astounding (formerly Campbell) Award for Best New Writer. The next book in her Between Earth and Sky series, Fevered Star, is out in March 2022. She lives in New Mexico with her family."
                    },
                    new Author
                    {
                        Id = 7,
                        Name = "Nathaniel Rich",
                        Description = "Nathaniel Rich is the author of the novels King Zeno, Odds Against Tomorrow, and The Mayor's Tongue. He is a writer at large for The New York Times Magazine and a regular contributor to The Atlantic and The New York Review of Books. He lives in New Orleans."
                    },
                    new Author
                    {
                        Id = 8,
                        Name = "Kate Pentecost",
                        Description = "Kate Pentecost was born and raised on the Texas/Louisiana state line, where there are more churches, pine trees, and alligators than anything else. She is a stalwart advocate of the weird, and has an MFA in Writing for Children and Young Adults from Vermont College of Fine Arts. Elysium Girls is her debut."
                    },
                    new Author
                    {
                        Id = 9,
                        Name = "Laura Roberts",
                        Description = "aura Roberts writes about sex, travel, writing, and ninjas - though not necessarily in that order. As the author of the 'V for Vixen' sex column, Laura began her career chronicling Montrealer's sexcapades, which are collected together in her book of essays, The Vixen Files. Blurring the lines between fact and fiction, she's also penned Confessions of a 3-Day Novelist, Ninjas of the 512, parts one and two of her serial novel, Naked Montreal, and a wide assortment of erotic Quickies"
                    },
                    new Author
                    {
                        Id = 10,
                        Name = "Margaret Weis",
                        Description = "Margaret Weis published their first novel in the Dragonlance Chronicles, Dragons of Autumn Twilight, in 1984. Over twenty years later, they are going strong as partners--with over thirty novels as collaborators--and have published over a hundred books, including novels, collections of short stories, role-playing games, and other game products alone or with other co-authors"
                    }
                    );

            modelBuilder.Entity<Book>().HasData(
             new Book
             {
               Id = 1,
               Title = "San Diego from A to Z: An Alphabetical Guide",
               AuthorId = 9,
               PublisherId = 4,
               Summary = "<p>Tired of the same old guidebooks? Learn where to go and what to do from a local! This alphabetical city guide looks at San Diego - and tourism - from a whole new angle, letting readers browse the city at their own pace. Learn about... * Local favorites * Tourist attractions * Cultural oddities * And enjoy unique trivia you just won't get from the other guys! Whether you're a first-time visitor or a life-long resident, San Diego from A to Z will surprise and delight you with plenty of facts, figures and personal experiences from author Laura Roberts. Explore alphabetically as you tour America's Finest City, starting at the Birch Aquarium and ending with Spanish phrases that begin with the letter Z. Learn more about San Diego landmarks, eateries, bars, museums, bookstores, neighborhoods, cultural oddities and much more. A must-have for the discerning traveler or seasoned flâneur. Find out what you've been missing in San Diego and order your copy today.</p>",
               PhotoUrl = "https://res.cloudinary.com/dglgzh0aj/image/upload/v1634554755/BookStore/bookcovers/san_dghfys.jpg",
               PhotoPublicId = "BookStore/bookcovers/san_dghfys",
               Price = 15.85m,
               Quantity = 20,
               IsActice = true,
               
           },
                    new Book
                    {
                        Id = 2,
                        Title = "Elysium Girls",
                        AuthorId = 8,
                        PublisherId = 3,
                        Summary = "<p>A lush, dazzlingly original young adult fantasy about an epic clash of witches, gods, and demons. Elysium, Oklahoma, is a town like any other. Respectable. God-fearing. Praying for an end to the Dust Bowl.Until the day the people of Elysium are chosen by two sisters: Life and Death.And the Sisters like to gamble against each other with things like time, and space, and human lives.Elysium is to become the gameboard in a ruthless competition between the goddesses.The Dust Soldiers will return in ten years' time, and if the people of Elysium have not proved themselves worthy, all will be slain. Nearly ten years later, seventeen-year - old Sal Wilkinson is called upon to lead Elysium as it prepares for the end of the game.But then an outsider named Asa arrives at Elysium's gates with nothing more than a sharp smile and a bag of magic tricks, and they trigger a terrible accident that gets both Sal and Asa exiled into the brutal Desert of Dust and Steel. There Sal and Asa stumble upon a gang of girls headed by another exile: a young witch everyone in Elysium believes to be dead. As the apocalypse looms, they must do more than simply tip the scales in Elysium's favor-only by reinventing the rules can they beat Life and Death at their own game in this exciting fantasy debut.</p>",
                        PhotoUrl = "https://res.cloudinary.com/dglgzh0aj/image/upload/v1634555234/BookStore/bookcovers/elisium_nudddg.jpg",
                        PhotoPublicId = "BookStore/bookcovers/elisium_nudddg",
                        Price = 27.55m,
                        Quantity = 50,
                        IsActice = true,
                    
                    },
                    new Book
                    {
                        Id = 3,
                        Title = "Black Sun, 1",
                        AuthorId = 6,
                        PublisherId = 1,
                        Summary = "<p>In the holy city of Tova, the winter solstice is usually a time for celebration and renewal, but this year it coincides with a solar eclipse, a rare celestial event proscribed by the Sun Priest as an unbalancing of the world. Meanwhile, a ship launches from a distant city bound for Tova and set to arrive on the solstice. The captain of the ship, Xiala, is a disgraced Teek whose song can calm the waters around her as easily as it can warp a man's mind. Her ship carries one passenger. Described as harmless, the passenger, Serapio, is a young man, blind, scarred, and cloaked in destiny. As Xiala well knows, when a man is described as harmless, he usually ends up being a villain. Crafted with unforgettable characters, Rebecca Roanhorse has created an epic adventure exploring the decadence of power amidst the weight of history and the struggle of individuals swimming against the confines of society and their broken pasts in the most original series debut of the decade.</p>",
                        PhotoUrl = "https://res.cloudinary.com/dglgzh0aj/image/upload/v1634555700/BookStore/bookcovers/71ZMYPkoNLL_y2she6.jpg",
                        PhotoPublicId = "BookStore/bookcovers/71ZMYPkoNLL_y2she6",
                        Price = 25.75m,
                        Quantity = 30,
                        IsActice = true,
                      
                    },
                    new Book
                    {
                        Id = 4,
                        Title = "The Shining",
                        AuthorId = 3,
                        PublisherId =3,
                        Summary = "<p>Jack Torrance's new job at the Overlook Hotel is the perfect chance for a fresh start. As the off-season caretaker at the atmospheric old hotel, he'll have plenty of time to spend reconnecting with his family and working on his writing. But as the harsh winter weather sets in, the idyllic location feels ever more remote . . . and more sinister. And the only one to notice the strange and terrible forces gathering around the Overlook is Danny Torrance, a uniquely gifted five-year-old.</p>",
                        PhotoUrl = "https://res.cloudinary.com/dglgzh0aj/image/upload/v1634556141/BookStore/bookcovers/9780345806789_p0_v2_s1200x630_a9ruho.jpg",
                        PhotoPublicId = "BookStore/bookcovers/9780345806789_p0_v2_s1200x630_a9ruho",
                        Price = 36.80m,
                        Quantity = 55,
                        IsActice = true,
                    
                    },
                    new Book
                    {
                        Id = 5,
                        Title = "The Dark Tower I, 1: The Gunslinger",
                        AuthorId = 3,
                        PublisherId = 1,
                        Summary = "<p>An impressive work of mythic magnitude that may turn out to be Stephen King's greatest literary achievement (The Atlanta Journal-Constitution), The Gunslinger</p>",
                        PhotoUrl = "https://res.cloudinary.com/dglgzh0aj/image/upload/v1634556342/BookStore/bookcovers/715w3hVk-dL_hvnmbd.jpg",
                        PhotoPublicId = "BookStore/bookcovers/715w3hVk-dL_hvnmbd",
                        Price = 28.20m,
                        Quantity = 45,
                        IsActice = true,
                      
                    },
                    new Book
                    {
                        Id = 6,
                        Title = "Dragons of Autumn Twilight",
                        AuthorId = 10,
                        PublisherId = 4,
                        Summary = "<p>Once merely creatures of legend, the dragons have returned to Krynn. But with their arrival comes the departure of the old gods--and all healing magic. As war threatens to engulf the land, lifelong friends reunite for an adventure that will change their lives and shape their world forever... When Tanis, Sturm, Caramon, Raistlin, Flint, and Tasslehoff see a woman use a blue crystal staff to heal a villager, they wonder if it's a sign the gods have not abandoned them after all. Fueled by this glimmer of hope, the Companions band together to uncover the truth behind the gods' absence--though they aren't the only ones with an interest in the staff. The Seekers, a new religious order, wants the artifact for their own ends, believing it will help them replace the gods and overtake the continent of Ansalon. Now, the Companions must assume the unlikely roles of heroes if they hope to prevent the staff from falling into the hands of darkness.</p>",
                        PhotoUrl = "https://res.cloudinary.com/dglgzh0aj/image/upload/v1634556692/BookStore/bookcovers/81Mtr5rXctL_bpihhq.jpg",
                        PhotoPublicId = "BookStore/bookcovers/81Mtr5rXctL_bpihhq",
                        Price = 19.99m,
                        Quantity = 25,
                        IsActice = true,                      
                    });
            modelBuilder.Entity<BookCategories>().HasData(
                new BookCategories
                {
                    BookId = 1,
                    CategoryId = 10
                },
                 new BookCategories
                 {
                     BookId = 2,
                     CategoryId = 9
                 },
                   new BookCategories
                   {
                       BookId = 3,
                       CategoryId = 8
                   },
                  new BookCategories
                  {
                      BookId = 4,
                      CategoryId = 6
                  },
                  new BookCategories
                  {
                      BookId = 5,
                      CategoryId = 6
                  },
                    new BookCategories
                    {
                        BookId = 6,
                        CategoryId = 4
                    });

            // any guid
            Guid siteOwnerId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            // Guid siteOwnerRoleId = new Guid("9977fad2-d8b9-4ac4-a73d-14cbf64ed65c"); ;
            Guid normalUserRoleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            Guid userId = new Guid("9977fad2-d8b9-4ac4-a73d-14cbf64ed65c");
            modelBuilder.Entity<Role>().HasData(
              new Role
              {
                  Id = siteOwnerId,
                  Name = Roles.SiteOwner.ToString(),
                  Description = "Site Owner has all right of website"
              },
              new Role
              {
                  Id = normalUserRoleId,
                  Name = Roles.NormalUser.ToString(),
                  Description = "Normal User is restricted some rights"
               });

            var hasher = new PasswordHasher<User>();
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = userId,
                    Email = "hailongsn99@gmail.com",
                    PhoneNumber = "0375274267",
                    UserName = "hailongsn99@gmail.com",
                    DisplayName = "Hà Hải Long",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Long904@"),
                    SecurityStamp = string.Empty,
                    Avatar = "https://img.freepik.com/free-vector/flat-design-library-logo-template_23-2149325326.jpg?w=2000",
                    AvatarPublicId = "BookStore/users/flat-design-library-logo-template_23-2149325326.jpg?w=2000"
                });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = siteOwnerId,
                UserId = userId
            });
         
        }
    }
}
