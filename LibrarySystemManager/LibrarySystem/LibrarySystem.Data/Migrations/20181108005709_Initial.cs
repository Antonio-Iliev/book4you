using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibrarySystem.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GenreName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Towns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TownName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Towns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    BooksInStore = table.Column<int>(nullable: false),
                    GenreId = table.Column<int>(nullable: false),
                    AuthorId = table.Column<int>(nullable: false),
                    ImageName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StreetAddress = table.Column<string>(maxLength: 50, nullable: false),
                    TownId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Towns_TownId",
                        column: x => x.TownId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 20, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 20, nullable: true),
                    LastName = table.Column<string>(maxLength: 20, nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: false),
                    AddOnDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AddressId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersBooks",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    BookId = table.Column<Guid>(nullable: false),
                    IsReturn = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersBooks", x => new { x.UserId, x.BookId });
                    table.ForeignKey(
                        name: "FK_UsersBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersBooks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2", "c6f664fa-dd0b-409a-b3a2-b4538b5810dc", "User", "USER" },
                    { "1", "343f2249-cfe0-48d7-b417-c8fe0133ad88", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 18, "John Richardson" },
                    { 17, "Arthur Hertzberg" },
                    { 16, "JRR Tolkien" },
                    { 15, "Veronica Roth" },
                    { 13, "Victor Hugo" },
                    { 12, "Dan Brown" },
                    { 11, "Charles Dickens" },
                    { 1, "Stephen King" },
                    { 9, "Arthur Golden" },
                    { 8, "John Green" },
                    { 7, "Stephenie Meyer" },
                    { 6, "Suzanne Collins" },
                    { 5, "Orson Scott Card" },
                    { 4, "JK Rowling" },
                    { 3, "Paulo Coelho" },
                    { 10, "Yann Martel" },
                    { 2, "George R R Martin" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "GenreName" },
                values: new object[,]
                {
                    { 10, "Comics" },
                    { 9, "Math" },
                    { 8, "History" },
                    { 7, "Science" },
                    { 6, "Horror" },
                    { 4, "Romance" },
                    { 3, "Action and Adventure" },
                    { 2, "Drama" },
                    { 1, "Fantasy" },
                    { 5, "Mystery" }
                });

            migrationBuilder.InsertData(
                table: "Towns",
                columns: new[] { "Id", "TownName" },
                values: new object[,]
                {
                    { 16, "Dupnitsa" },
                    { 15, "Hatteras" },
                    { 14, "Galesville" },
                    { 13, "Brutus" },
                    { 12, "Zeba" },
                    { 11, "Escondida" },
                    { 10, "Kimmell" },
                    { 8, "Foscoe" },
                    { 7, "Williamson" },
                    { 6, "Kiskimere" },
                    { 5, "Cressey" },
                    { 4, "Coleville" },
                    { 3, "Brambleton" },
                    { 2, "Jardine" },
                    { 9, "Stockdale" },
                    { 1, "Westphalia" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "StreetAddress", "TownId" },
                values: new object[] { 1, "AdminAddres", 1 });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "BooksInStore", "Description", "GenreId", "ImageName", "Title" },
                values: new object[,]
                {
                    { new Guid("de27622d-b4d5-4b47-b6f0-30b101ee4594"), 1, 10, "An unsuspecting accountant's wife makes a disturbing discovery; first published in 2010, now a movie.", 8, null, "A GOOD MARRIAGE" },
                    { new Guid("0fe17341-4f5f-421b-a3c1-16299037a011"), 18, 10, "", 7, null, "What Terrorists Want: Understanding the Enemy, Containing the Threat" },
                    { new Guid("e2176637-b035-4681-bfbf-94a12e1e1366"), 17, 10, "", 7, null, "Desire and Delusion: Three Novellas" },
                    { new Guid("57f4ea15-b5db-4aa9-902c-87c486b7162b"), 17, 10, "", 7, null, "DARKNESS RISING" },
                    { new Guid("dc7cc85f-8045-4cce-a89c-2388f826e969"), 17, 10, "Riley Jenson, part vampire, part werewolf, juggles multiple murder cases and a pair of jealous lovers.", 7, null, "BOUND TO SHADOWS" },
                    { new Guid("f3ac5ab8-a7a1-4e1e-8ba2-f90359a1e30d"), 15, 10, "In this “Divergent” follow-up, a faction war looms.", 7, null, "INSURGENT" },
                    { new Guid("54b0c36e-bc17-466b-aa84-7ba90d588e64"), 15, 10, "", 7, null, "FREE FOUR" },
                    { new Guid("8c3c2a5c-49da-4cf1-aa20-dc01bcda64c7"), 8, 10, "After a night of mischief, the girl Quentin loves disappears.", 7, null, "PAPER TOWNS" },
                    { new Guid("cd40b365-e25a-462f-9fb9-7c2d01df33c4"), 7, 10, "The comic adaptation of the ever-popular vampire series.", 7, null, "TWILIGHT, VOL. 1" },
                    { new Guid("283280ea-2c14-480a-b91a-6de1c2dfdea7"), 6, 10, "In a dystopian future, a girl fights for survival on live TV.", 7, null, "THE HUNGER GAMES" },
                    { new Guid("0b21f6e9-5255-457a-a10c-3a4b5b5ccd5b"), 5, 10, "A boy can not only see the past but also revise it.", 7, null, "PATHFINDER" },
                    { new Guid("1da8f275-0bcb-4f34-bab5-5ac178ab14ab"), 2, 10, "A set that includes the first four volumes of Martin's epic fantasy series.", 7, null, "A SONG OF ICE AND FIRE" },
                    { new Guid("68aeb701-1261-4584-9d4e-35706c95ae03"), 2, 10, "After a colossal battle, the Seven Kingdoms face new threats.", 7, null, "A DANCE WITH DRAGONS" },
                    { new Guid("1e4e0434-ac01-4922-9c7a-d0ead7c9b573"), 1, 10, "", 7, null, "Dreamcatcher" },
                    { new Guid("551056a3-b407-42f6-980e-a3f2898d071e"), 1, 10, "", 7, null, "Dolores Claiborne" },
                    { new Guid("328160ca-dfa4-4a24-bfb1-5a999e6ef1ae"), 18, 10, "", 6, null, "THE ISLAMIC ANTICHRIST" },
                    { new Guid("54bca25e-a5d5-4b11-8dc6-205b07e70449"), 18, 10, "", 6, null, "Generation Without Memory: A Jewish Journey in Christian America" },
                    { new Guid("e316b5d5-3952-4539-a8e6-59099cfc3e08"), 18, 10, "", 6, null, "A Life of Picasso, Volume II: 1907-1917 - The Painter of Modern Life" },
                    { new Guid("fc69b81f-57f0-4f51-be29-97c9ca952d97"), 10, 10, "An allegory on the high seas, in which a teenage boy and a 450-pound tiger are thrown together in a lifeboat after a shipwreck.", 6, null, "LIFE OF PI" },
                    { new Guid("a49feace-badd-4103-89de-81a37049a5af"), 7, 10, "The comic adaptation of the ever-popular vampire series.", 6, null, "TWILIGHT: THE GRAPHIC NOVEL, VOL. 2" },
                    { new Guid("af5c9a6b-78e8-4c24-827f-9f19a4d252f4"), 5, 10, "", 6, null, "THE LOST GATE" },
                    { new Guid("1d7f1953-64a6-4c74-bb6c-83d6a4dc9208"), 5, 10, "", 6, null, "THE GATE THIEF" },
                    { new Guid("a1b2f15b-0b3b-4f0b-87fa-b6184a1c5bbe"), 1, 10, "A man who is losing weight without getting thinner forms an unlikely alliance with his neighbors who are dealing with prejudices.", 6, null, "ELEVATION" },
                    { new Guid("f7fc354e-1233-4321-b1ea-0ebccb56a422"), 1, 10, "", 6, null, "Danse Macabre" },
                    { new Guid("b4ffaee2-4fd5-4355-a714-646e69a9cd71"), 18, 10, "Behind-the-scenes photographs of the pop star.", 5, null, "LADY GAGA x TERRY RICHARDSON" },
                    { new Guid("99bfe89d-e4ea-48ce-836f-8de1e519a1fa"), 17, 10, "", 5, null, "DARKNESS UNBOUND" },
                    { new Guid("6d6943ae-8aa9-4dba-825a-ba95b9155e09"), 13, 10, "", 5, null, "Cosette: The Sequel to Les Miserables" },
                    { new Guid("6efad4db-a380-4dc6-9b45-4253788bad68"), 1, 10, "What do the roaring 20’s and the wild west have in common? Vampires.", 8, null, "AMERICAN VAMPIRE, VOL. 1" },
                    { new Guid("3bf3d883-b7ee-48d2-b13c-9a5c78735f55"), 1, 10, "", 8, null, "Bag of Bones" },
                    { new Guid("f2057be3-d32c-43b2-858a-1ce25a38e9bb"), 1, 10, "", 8, null, "Cell: A Novel" },
                    { new Guid("2c5093e3-82e3-4e0b-bf73-c467cbbb204e"), 1, 10, "Now grown up, Dan, the boy with psycho-intuitive powers in “The Shining,” helps another child with a spectacular gift.", 8, null, "DOCTOR SLEEP" },
                    { new Guid("31496102-4d73-41f3-85d6-81305f2a82b6"), 18, 10, "Tricks for new treat creations.", 9, null, "CUPCAKES, COOKIES AND PIE, OH, MY!" },
                    { new Guid("7c73afc8-f3c1-4144-90a1-3255ab53277a"), 17, 10, "", 9, null, "Dogwalker: Stories" },
                    { new Guid("7885fd30-2771-42eb-a3ea-60339ac3c1af"), 17, 10, "", 9, null, "Angelica: A Novel" },
                    { new Guid("a244b14b-e5ab-4dbc-aaf8-df44f650757e"), 12, 10, "After reconnecting with one of his first students, who is now a billionaire futurist, symbology professor Robert Langdon must go on a perilous quest with a beautiful museum director.", 9, null, "ORIGIN" },
                    { new Guid("2e9c0164-11ef-4de1-8a6a-2ee50d5ea502"), 8, 10, "Two boys with the same name join forces in a reality possible only in musical theater.", 9, null, "WILL GRAYSON, WILL GRAYSON" },
                    { new Guid("47b05e58-2287-4731-bb94-b811bd4e339b"), 8, 10, "Colin Singleton wants to break the pattern of being dumped.", 9, null, "AN ABUNDANCE OF KATHERINES" },
                    { new Guid("af5433f7-17f5-43e7-aba8-3c26052fc05d"), 7, 10, "Vampires and werewolves and their intrigues in high school.", 9, null, "THE TWILIGHT SAGA" },
                    { new Guid("c495dffe-7966-4cc7-8ec2-bb0148981636"), 7, 10, "", 9, null, "THE  HOST" },
                    { new Guid("eb7f7257-6fa7-4a09-aed0-a6b201ed8a82"), 5, 10, "Thousands of years after the events of \"Ender's Game,\" a second alien race has been discovered.", 9, null, "SPEAKER FOR THE DEAD" },
                    { new Guid("512223e3-1c6a-4e7d-b351-056f3ce80621"), 5, 10, "Bean explores the stars with his genetically engineered children in this entry in the “Ender” science fiction series; a sequel to “Shadow of the Giant.”", 9, null, "SHADOWS IN FLIGHT" },
                    { new Guid("592b084f-57c2-456d-89d9-8c45c01ec133"), 5, 10, "Adventures and time travel continue in the second book of the “Pathfinder” series.", 9, null, "RUINS" },
                    { new Guid("a8fda031-f01f-4df6-ac52-4197bd498675"), 5, 10, "The beginning of the First Formic War; the back story to “Ender’s Game.”", 9, null, "EARTH UNAWARE" },
                    { new Guid("28a1a6f6-ce28-4f67-ba32-2963d79b07db"), 3, 10, "", 9, null, "WITCH OF PORTOBELLO" },
                    { new Guid("62c4d4b0-4a8c-46d8-8449-2895b6142644"), 11, 10, "", 5, null, "The Mystery of Edwin Drood (Penguin Classics)" },
                    { new Guid("9153b951-7a4f-4b85-9275-5e7d43dc65b0"), 3, 10, "The wisdom of a wise man known as the Copt, set in Jerusalem just before the Crusaders’ invasion in 1099.", 9, null, "MANUSCRIPT FOUND IN ACCRA" },
                    { new Guid("9982535d-c9cf-431b-9181-d0559ada599d"), 1, 10, "This second novel of the Bill Hodges trilogy involves a dead writer, his obsessed murderer and a coveted manuscript.", 9, null, "FINDERS KEEPERS" },
                    { new Guid("9382b92a-dddc-4fd4-9397-be21b4756d26"), 1, 10, "", 9, null, "Different Seasons (Signet)" },
                    { new Guid("7519f087-4685-439f-bb3f-2c390bd89017"), 17, 10, "How American business produced victory in World War II.", 8, null, "FREEDOM'S FORGE" },
                    { new Guid("09dbe2b5-2a0b-4535-a2e9-9cf1ee723014"), 17, 10, "", 8, null, "Detective" },
                    { new Guid("41a0fa19-685b-4843-b57b-c7b687601d21"), 17, 10, "Riley Jensen tracks a new villain while juggling passions for her vampire lover and a rogue wolf.", 8, null, "DEADLY DESIRE" },
                    { new Guid("57dc09a9-9a78-4961-8d91-a4c210ef12a4"), 16, 10, "Thousands of years before the events of “The Lord of the Rings,” a hero named Tuor visits a secret city. Edited by Christopher Tolkien.", 8, null, "THE FALL OF GONDOLIN" },
                    { new Guid("3762fdf7-e8ec-4bbf-ba36-6f2d18b14703"), 8, 10, "Aza and Daisy investigate a mystery with a reward of $100,000.", 8, null, "TURTLES ALL THE WAY DOWN" },
                    { new Guid("a987c3e2-81a3-45f6-819f-52a618b62774"), 7, 10, "A definitive guide to all things “Twilight.”", 8, null, "THE TWILIGHT SAGA: THE OFFICIAL ILLUSTRATED GUIDE" },
                    { new Guid("0e263c64-b18c-47cc-9243-ccddb933a5a1"), 7, 10, "In this first adult novel by the author of the Twilight series for teenagers, aliens have taken control of the minds and bodies of most humans, but one woman won’t surrender; originally published in 2008.", 8, null, "THE HOST" },
                    { new Guid("3f2a0e4a-f0cd-46a4-9c30-4335bff743fc"), 6, 10, "In a dystopia, a girl fights for survival on live TV.", 8, null, "THE HUNGER GAMES" },
                    { new Guid("c7c2cc1e-a286-4d28-b45d-aa8f2acaa36d"), 6, 10, "The protagonist of \"The Hunger Games\" returns.", 8, null, "CATCHING FIRE" },
                    { new Guid("bb35f7b5-c729-40d7-a9f6-9d3b796a602e"), 2, 10, "", 8, null, "DANGEROUS WOMEN" },
                    { new Guid("f3b320e9-69ef-4c2c-a28d-4722e127a8ee"), 2, 10, "From the citadel of Dragonstone to the shores of Winterfell, factions vie for control of a divided land; Book 2 of \"A Song of Ice and Fire.\"", 8, null, "A CLASH OF KINGS" },
                    { new Guid("217597ec-ffee-470e-8707-b8671f320184"), 2, 10, "In the frozen wastes to the north of Winterfell, sinister and supernatural forces are mustering. Basis of the HBO series.", 9, null, "A GAME OF THRONES" },
                    { new Guid("04c079a1-c0c5-4a37-8bcc-8fbbdf5c5be8"), 18, 10, "A five-step system for meeting sales objectives and increasing business.", 9, null, "PERFECT SELLING" },
                    { new Guid("84bc750b-697e-490f-a235-3e607a785425"), 10, 10, "The tragic fate of the title characters, a donkey named Beatrice and a monkey named Virgil, stuffed animals in a taxidermy shop, is an allegory for the Holocaust; from the author of “The Life of Pi.”", 5, null, "BEATRICE AND VIRGIL" },
                    { new Guid("84cc6356-e091-40a8-914f-86c1908df2db"), 5, 10, "To develop a secure defense against a hostile alien race's next attack, government agencies breed child geniuses and train them as soldiers.", 5, null, "ENDER'S GAME" },
                    { new Guid("b2481438-4e95-450f-aa59-bf9bf6ba4ae6"), 17, 10, "", 2, null, "Conversations with Miller" },
                    { new Guid("8bff6cb7-79cf-4f6a-b4f1-a72104760a56"), 17, 10, "", 2, null, "City Room" },
                    { new Guid("d77c7b19-ce96-45d1-86f7-6adc7583a11c"), 17, 10, "", 2, null, "Beyond the Mountain" },
                    { new Guid("10e6c695-a490-4201-bac7-b4b10cc643ba"), 17, 10, "", 2, null, "Another Good Loving Blues" },
                    { new Guid("43d825dd-1b38-4436-9e30-72501037ba50"), 15, 10, "A faction war looms.", 2, null, "INSURGENT" },
                    { new Guid("5f41652a-ee06-4211-bfd9-95355b2daeb8"), 15, 10, "A girl must prove herself in a dystopia divided into five factions.", 2, null, "DIVERGENT" },
                    { new Guid("430bb4f6-a512-406e-bc47-668e0ccc5cb2"), 15, 10, "A girl must prove her mettle in a faction-ridden dystopia.", 2, null, "DIVERGENT" },
                    { new Guid("609d1bfa-3264-4441-b26b-24ff47b33eb7"), 15, 10, "An oracle's son is able to stop the painful flow of Cyra's powers.", 2, null, "CARVE THE MARK" },
                    { new Guid("b6ed3a2f-7a85-409e-aff4-4222a165ce80"), 12, 10, "The Harvard symbologist Robert Langdon among the Masons.", 2, null, "THE LOST SYMBOL" },
                    { new Guid("fe563320-0367-4cf5-832e-5343bbef5b87"), 10, 10, "", 2, null, "THE HIGH MOUNTAINS OF PORTUGAL" },
                    { new Guid("435e463c-75f6-4e38-b290-af2d6979cc5f"), 9, 10, "", 2, null, "Memoirs of a Geisha: A Novel" },
                    { new Guid("2bb7301f-7e77-4b18-9042-f1de3e527f2e"), 8, 10, "A girl faces new realities when she learns she has cancer.", 2, null, "THE FAULT IN OUR STARS" },
                    { new Guid("2d148554-48f9-4b0b-9785-747d2b7743a4"), 8, 10, "Three holiday stories.", 2, null, "LET IT SNOW" },
                    { new Guid("28b3ba36-0014-4115-bb92-1d6ab0b0366f"), 4, 10, "A wizard hones his conjuring skills in the service of fighting evil.", 2, null, "HARRY POTTER" },
                    { new Guid("94d9540a-7498-4384-b0aa-210100738c2a"), 3, 10, "A young Brazillian man and a Dutch woman explore their relationship as they travel across Europe and Central Asia to Kathmandu.", 2, null, "HIPPIE" },
                    { new Guid("1d28826c-3e2d-462d-80d2-fa29908a15d6"), 2, 10, "Wars continue to rage over the Iron Throne as alliances are made and broken; Book 3 of \"A Song of Ice and Fire.\"", 2, null, "A STORM OF SWORDS" },
                    { new Guid("339f6e00-7eb3-479d-8b6d-583ae8777336"), 2, 10, "A collection of three official prequels to “A Song of Ice and Fire.”", 2, null, "A KNIGHT OF THE SEVEN KINGDOMS" },
                    { new Guid("09c80279-1942-4eaf-924d-27051bbfd71c"), 2, 10, "The seven powers dividing the land have reached an uneasy truce; Book 4 of \"A Song of Ice and Fire.\"", 2, null, "A FEAST FOR CROWS" },
                    { new Guid("53eb6de1-ae8a-4a4b-9ff1-e32a0732353e"), 1, 10, "The conclusion of the Bill Hodges trilogy.", 2, null, "END OF WATCH" },
                    { new Guid("3a13cf7c-c91c-42b3-9ac2-f36d8bb28f50"), 18, 10, "Simple cupcake designs and recipes.", 1, null, "WHAT'S NEW, CUPCAKE?" },
                    { new Guid("5dcddf2d-13e6-4da6-9f89-e50cfe91cde4"), 18, 10, "", 1, null, "Wallace Stevens: The Early Years, 1879-1923" },
                    { new Guid("b0ba7365-cfb0-46fb-91fb-f91fbeb175e1"), 17, 10, "A biography and assessment of the influential twentieth-century American photographer.", 1, null, "DIANE ARBUS" },
                    { new Guid("66ed702e-5662-4ccd-a975-c45e078d6f82"), 12, 10, "A scholar tries to save the Vatican from the machinations of an underground society.", 1, null, "ANGELS AND DEMONS" },
                    { new Guid("506c3447-de84-4085-88f1-a54611cb340e"), 8, 10, "A boy finds excitement when he meets a girl named Alaska.", 1, null, "LOOKING FOR ALASKA" },
                    { new Guid("808fa1c0-ebeb-4c39-895b-ba91498280e7"), 7, 10, "The graphic novel version of the \"Twilight\" saga continues.", 1, null, "TWILIGHT: NEW MOON, VOL. 1" },
                    { new Guid("2fa26ab6-8643-4f2f-a819-1ef1151e58a1"), 5, 10, "", 1, null, "EARTH AWAKENS" },
                    { new Guid("aa0f0042-7104-4208-9543-ffa677c9b486"), 3, 10, "A Spanish shepherd boy ventures to Egypt in search of treasure and his destiny.", 1, null, "THE ALCHEMIST" },
                    { new Guid("7d53ecee-5476-4832-9dcf-a352a0be25bb"), 17, 10, "A woman and a man with superhuman powers flee dangerous killers from Scotland.", 2, null, "DESTINY KILLS" },
                    { new Guid("05ca6f0b-7142-4b8d-9fd7-75b0bd0a8010"), 17, 10, "", 2, null, "Equality and Education: Federal Civil Rights Enforcement in the New York City School System" },
                    { new Guid("072f585b-495c-4885-9ad1-970c486279a3"), 18, 10, "", 2, null, "The Sorcerer's Apprentice: Picasso, Provence, and Douglas Cooper" },
                    { new Guid("a5865078-7742-4c1c-aa3d-08cac69d5ac1"), 1, 10, "An elderly widower watches baseballt to distract himself from his wife's death, but figures from his past appear every night in the seat behind home plate; a Kindle single.", 3, null, "A FACE IN THE CROWD" },
                    { new Guid("6080adc9-53b8-409c-ac6e-0a02256a92a7"), 5, 10, "The latest entry in the “Ender” science fiction series.", 5, null, "ENDER IN EXILE" },
                    { new Guid("db40e97a-c060-455e-a067-ff7b972f7cb1"), 5, 10, "One hundred years before \"Ender's Game,\" the aliens arrived on Earth with fire and death. This is the story of the First Formic War.", 5, null, "EARTH AFIRE" },
                    { new Guid("ba0a372c-9366-4efd-93ea-11193ee007cb"), 3, 10, "", 5, null, "THE WINNER STANDS ALONE" },
                    { new Guid("ee7047b2-e8c0-4928-9310-ff9822e87061"), 3, 10, "", 5, null, "THE SPY" },
                    { new Guid("c6a5b7e8-c28c-46fc-928f-7565be9708f8"), 3, 10, "A young Irish girl who desires to become a witch seeks wisdom from teachers of magic and spirituality.", 5, null, "BRIDA" },
                    { new Guid("6f55e385-72f9-48e5-9b05-e08b446fa1cb"), 3, 10, " A married journalist risks everything when she embarks on an affair; by the Brazilian writer, the author of The Alchemist.", 5, null, "ADULTERY" },
                    { new Guid("d22a4520-b6bd-4671-979a-186347514def"), 1, 10, "A Minnesota contractor moves to Florida to recover from an injury and starts creating paintings with eerie powers.", 5, null, "DUMA KEY" },
                    { new Guid("10700046-9601-4b38-960d-9ff07762edb0"), 1, 10, "", 5, null, "Christine (Signet)" },
                    { new Guid("f2d4b2e7-215c-4cb3-8290-5b9dcd3a9823"), 1, 10, "A tale about the dark side of baseball, circa 1957.", 5, null, "BLOCKADE BILLY" },
                    { new Guid("69dc432f-ad47-4066-908d-9244b623bbbb"), 1, 10, "This series, about a new species of vampire that does not have the traditional weakness, shifts to Las Vegas in the 1930's and a number of corpses that have turned up drained of blood.", 5, null, "AMERICAN VAMPIRE, VOL. 1" },
                    { new Guid("01776fc2-b579-4627-a00e-12c500de5f09"), 17, 10, "", 4, null, "Agitations: Essays on Life and Literature" },
                    { new Guid("afe0f5ba-0976-4487-9232-b75916c1a42b"), 17, 10, "", 4, null, "A Jew In America: My Life and A People's Struggle for Identity" },
                    { new Guid("0454abbe-57b7-4349-9fcf-82bd3ec263f6"), 15, 10, "Cyra and Akos fight Lazmet, the tyrant who was thought to be dead.", 4, null, "THE FATES DIVIDE" },
                    { new Guid("f2125477-d1e5-45f2-8704-8de3e0453ca8"), 7, 10, "A specialist in chemically controlled torture, on the run from her former employers, takes on one last job.", 5, null, "THE CHEMIST" },
                    { new Guid("dc0a2c8c-125d-4bb4-821d-014e3a32e871"), 11, 10, "", 4, null, "Oliver Twist (Dover Thrift Editions)" },
                    { new Guid("b255cc8e-b5a2-488f-963d-4158a4df5635"), 3, 10, "", 4, null, "ALEPH" },
                    { new Guid("fb0de218-6e74-42c3-9c27-0a380a9e25b3"), 3, 10, "A crisis of faith is the impetus for a journey through time and space, on a path that teaches love, forgiveness and courage.", 4, null, "ALEPH" },
                    { new Guid("2f506d66-9092-4333-90db-ae107d31fc20"), 2, 10, "A girl tames a dragon and saves her farm.", 4, null, "THE ICE DRAGON" },
                    { new Guid("7e178ce1-1ce2-4303-bf8d-6e162f1c1a99"), 2, 10, "Twenty-one original stories from well-known writers, including a new “Game of Thrones” story.", 4, null, "ROGUES" },
                    { new Guid("d1f1057c-8e97-419b-9822-6d7e913d445d"), 1, 10, "", 4, null, "Everything's Eventual: 14 Dark Tales" },
                    { new Guid("8fb9cdd3-66a0-4681-8362-bac679eb1386"), 1, 10, "An English teacher travels back to 1958 by way of a time portal in a Maine diner. His assignment: Stop Lee Harvey Oswald.", 4, null, "11/22/63" },
                    { new Guid("b1692bd6-76f0-4273-905c-7ae9fef53b6c"), 18, 10, "", 3, null, "Sacred Monsters, Sacred Masters: Beaton, Capote, Dalí, Picasso, Freud, Warhol, and More" },
                    { new Guid("842a9353-816b-4257-9c4c-485849b2ad37"), 17, 10, "", 3, null, "Boogaloo: The Quintessence of American Popular Music" },
                    { new Guid("e2e15a74-dffb-4b50-a3fb-6d2ba35cf320"), 17, 10, "", 3, null, "AN INQUIRY INTO THE NATURE AND CAUSES OF THE WEALTH OF STATES" },
                    { new Guid("6064eda7-ff6c-47a3-a469-3c824450179a"), 16, 10, "The love of a mortal man for an immortal elf, which figures in “The Silmarillion” and is part of the back story of “Lord of the Rings.” Edited by Christopher Tolkien.", 3, null, "BEREN AND LÚTHIEN" },
                    { new Guid("83611111-079a-4b5c-8c53-c3003efd2321"), 12, 10, "The symbologist Robert Langdon, on the run in Florence, must decipher a series of codes created by a Dante-loving scientist.", 3, null, "INFERNO" },
                    { new Guid("93c37228-ecb9-4c87-9f40-bdaa3e85fcbc"), 2, 10, "The collected Song of Ice and Fire series.", 3, null, "A GAME OF THRONES: FIVE-BOOK SET" },
                    { new Guid("82d6e6f0-2344-43af-91cd-839cec19d7fa"), 1, 10, "Grisly human behavior and its consequences drive this collection of stories.", 3, null, "FULL DARK, NO STARS" },
                    { new Guid("bb38aa31-bde9-49ad-93d2-50d7d1c0dd2e"), 6, 10, "", 4, null, "Mockingjay (The Final Book of The Hunger Games)" },
                    { new Guid("3ffca685-977b-493a-bb44-fd1153d6de2e"), 2, 10, "The history of the Westeros and more about the world of \"Game of Thrones.\"", 1, null, "THE WORLD OF ICE AND FIRE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddOnDate", "AddressId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "88ae1ada-d287-4b45-b048-e1b11b1c048b", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "f21fcb8f-8c21-468b-985e-c04cb475dd0c", "admin@mail.com", true, "Admin", false, "AdminLastName", false, null, null, "ADMIN@MAIL.COM", "ADMINMAIN", "AQAAAAEAACcQAAAAEAfBUytfNPbQ+L/8uRYSAOhsZbt6rpJfRei8yOEvW92FCmTK3zTW+gAmrqj//jxIyA==", "+111111111", true, "0e905a28-dcfb-4fb1-8869-129035dc9486", false, "adminMain" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "88ae1ada-d287-4b45-b048-e1b11b1c048b", "1" });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_TownId",
                table: "Addresses",
                column: "TownId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AddressId",
                table: "AspNetUsers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenreId",
                table: "Books",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersBooks_BookId",
                table: "UsersBooks",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "UsersBooks");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Towns");
        }
    }
}
