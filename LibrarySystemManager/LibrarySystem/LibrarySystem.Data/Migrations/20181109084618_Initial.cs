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
                    BorrowDate = table.Column<DateTime>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "UsersReadBooks",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    BookId = table.Column<Guid>(nullable: false),
                    BackDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersReadBooks", x => new { x.UserId, x.BookId });
                    table.ForeignKey(
                        name: "FK_UsersReadBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersReadBooks_AspNetUsers_UserId",
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
                    { "2", "becdeca8-21b8-4544-a602-3a6a4b2f777c", "User", "USER" },
                    { "1", "0ab55774-3804-4173-8d0f-5d7c265b3609", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 27, "Arthur Flowers" },
                    { 28, "Elizabeth Arthur" },
                    { 29, "Arthur Kempton" },
                    { 30, "Keri Arthur" },
                    { 31, "Arthur Gelb" },
                    { 32, "Arthur Miller" },
                    { 33, "Arthur Schnitzler" },
                    { 34, "Arthur Hailey" },
                    { 35, "Arthur Lubow" },
                    { 36, "Arthur Bradford" },
                    { 38, "Arthur Herman" },
                    { 39, "John Richardson" },
                    { 40, "Karen Tack " },
                    { 41, "Anne Richardson Roiphe" },
                    { 42, "Lady Gaga " },
                    { 43, "Linda Richardson" },
                    { 44, "Joel Richardson" },
                    { 45, "Joan Richardson" },
                    { 46, "Louise Richardson" },
                    { 26, "Arthur Phillips" },
                    { 25, "Arthur B Laffer" },
                    { 37, "Arthur R. Block" },
                    { 23, "Arthur Hertzberg" },
                    { 24, "Arthur Krystal" },
                    { 2, "Stephen King " },
                    { 3, "Scott Snyder " },
                    { 4, "Scott Snyder" },
                    { 5, "George R R Martin" },
                    { 6, "edited  George R R Martin " },
                    { 7, "Paulo Coelho" },
                    { 8, "JK Rowling" },
                    { 9, "Orson Scott Card" },
                    { 10, "Orson Scott Card " },
                    { 11, "Suzanne Collins" },
                    { 1, "Stephen King" },
                    { 13, "Stephenie Meyer " },
                    { 14, "John Green" },
                    { 15, "John Green " },
                    { 16, "Arthur Golden" },
                    { 17, "Yann Martel" },
                    { 18, "Charles Dickens" },
                    { 19, "Dan Brown" },
                    { 20, "Victor Hugo" },
                    { 21, "Veronica Roth" },
                    { 12, "Stephenie Meyer" },
                    { 22, "JRR Tolkien" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "GenreName" },
                values: new object[,]
                {
                    { 9, "Math" },
                    { 8, "History" },
                    { 7, "Science" },
                    { 10, "Comics" },
                    { 5, "Mystery" },
                    { 6, "Horror" },
                    { 1, "Fantasy" },
                    { 3, "Action and Adventure" },
                    { 4, "Romance" },
                    { 2, "Drama" }
                });

            migrationBuilder.InsertData(
                table: "Towns",
                columns: new[] { "Id", "TownName" },
                values: new object[,]
                {
                    { 1, "Westphalia" },
                    { 16, "Dupnitsa" },
                    { 15, "Hatteras" },
                    { 14, "Galesville" },
                    { 13, "Brutus" },
                    { 12, "Zeba" },
                    { 11, "Escondida" },
                    { 9, "Stockdale" },
                    { 8, "Foscoe" },
                    { 6, "Kiskimere" },
                    { 5, "Cressey" },
                    { 4, "Coleville" },
                    { 3, "Brambleton" },
                    { 2, "Jardine" },
                    { 10, "Kimmell" },
                    { 7, "Williamson" }
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
                    { new Guid("ef30482c-676c-4a88-b89e-f6ee867938db"), 1, 10, "", 7, null, "Dreamcatcher" },
                    { new Guid("8fd1531b-2621-4a82-baab-463e859cb79a"), 1, 10, "", 7, null, "Dolores Claiborne" },
                    { new Guid("16cc9e8c-42f1-4cc8-9d98-fc26020f2222"), 1, 10, "", 7, null, "Bag of Bones" },
                    { new Guid("e9e613d7-bf52-449f-b75f-7c2f0aee87af"), 46, 10, "", 6, null, "What Terrorists Want: Understanding the Enemy, Containing the Threat" },
                    { new Guid("b44ba618-e611-438f-84fb-212735398522"), 45, 10, "", 6, null, "Wallace Stevens: The Early Years, 1879-1923" },
                    { new Guid("a28635a1-79ea-43af-aeb7-8f1a53db1599"), 30, 10, "Riley Jensen tracks a new villain while juggling passions for her vampire lover and a rogue wolf.", 6, null, "DEADLY DESIRE" },
                    { new Guid("b7a347c5-912a-449d-bca1-69f3fa521621"), 30, 10, "", 6, null, "DARKNESS RISING" },
                    { new Guid("66c42f55-f89c-4f55-a7bc-49ecad5083f6"), 21, 10, "A faction war looms.", 6, null, "INSURGENT" },
                    { new Guid("45ba79c1-57c0-4026-9cbe-65dc8637fbf5"), 17, 10, "", 6, null, "THE HIGH MOUNTAINS OF PORTUGAL" },
                    { new Guid("2b5e5e64-e81c-4f26-bf8e-8fd5aad7b479"), 7, 10, "The wisdom of a wise man known as the Copt, set in Jerusalem just before the Crusaders’ invasion in 1099.", 6, null, "MANUSCRIPT FOUND IN ACCRA" },
                    { new Guid("5ce7f668-12d8-431c-873f-58602f1e812b"), 1, 10, "A man who is losing weight without getting thinner forms an unlikely alliance with his neighbors who are dealing with prejudices.", 6, null, "ELEVATION" },
                    { new Guid("6ee4c208-acfd-41af-ac64-a8bad800ef2e"), 38, 10, "How American business produced victory in World War II.", 5, null, "FREEDOM'S FORGE" },
                    { new Guid("7a8152b7-5bc6-4b44-90e2-296adb6b7240"), 1, 10, "", 7, null, "Everything's Eventual: 14 Dark Tales" },
                    { new Guid("31c8ee76-12b5-40f9-90e9-d7d42b0753ce"), 31, 10, "", 5, null, "City Room" },
                    { new Guid("eb174b9b-1f36-4214-8ca0-cada4018bdbc"), 14, 10, "Three holiday stories.", 5, null, "LET IT SNOW" },
                    { new Guid("cf20e3db-bbc0-4be3-a3a3-668c1434b06e"), 13, 10, "The graphic novel version of the \"Twilight\" saga continues.", 5, null, "TWILIGHT: NEW MOON, VOL. 1" },
                    { new Guid("702c3756-585b-43cd-aa02-fd93c3d6eb66"), 12, 10, "The comic adaptation of the ever-popular vampire series.", 5, null, "TWILIGHT, VOL. 1" },
                    { new Guid("51ad5160-d2e1-474d-a247-5b91a70f742a"), 12, 10, "A specialist in chemically controlled torture, on the run from her former employers, takes on one last job.", 5, null, "THE CHEMIST" },
                    { new Guid("d348411d-be97-4e19-b801-b9bb4d428a0a"), 12, 10, "", 5, null, "THE  HOST" },
                    { new Guid("f66a46d0-eaae-4a85-9ed6-828d115c4947"), 9, 10, "Bean explores the stars with his genetically engineered children in this entry in the “Ender” science fiction series; a sequel to “Shadow of the Giant.”", 5, null, "SHADOWS IN FLIGHT" },
                    { new Guid("5c9a284d-2545-4319-bf8d-ab2b7f89afd8"), 7, 10, "", 5, null, "THE WINNER STANDS ALONE" },
                    { new Guid("fd1263cd-7e67-40a3-8845-a7b4fb797199"), 7, 10, " A married journalist risks everything when she embarks on an affair; by the Brazilian writer, the author of “The Alchemist.”.", 5, null, "ADULTERY" },
                    { new Guid("ca03cecc-bf28-4390-aa62-c5046ad5d8d0"), 5, 10, "The history of the Westeros and more about the world of \"Game of Thrones.\"", 5, null, "THE WORLD OF ICE AND FIRE" },
                    { new Guid("59af970d-9272-473a-a168-ab75528b7911"), 6, 10, "", 5, null, "DANGEROUS WOMEN" },
                    { new Guid("a2ac1436-ad8b-47ac-816f-0039f76e1806"), 5, 10, "The collected Song of Ice and Fire series.", 5, null, "A GAME OF THRONES: FIVE-BOOK SET" },
                    { new Guid("4ef79a67-de98-45d8-afcf-b17c75f7ff92"), 5, 10, "From the citadel of Dragonstone to the shores of Winterfell, factions vie for control of a divided land; Book 2 of \"A Song of Ice and Fire.\"", 5, null, "A CLASH OF KINGS" },
                    { new Guid("dd38353d-6507-414d-a9dc-f3f217a97947"), 17, 10, "An allegory on the high seas, in which a teenage boy and a 450-pound tiger are thrown together in a lifeboat after a shipwreck.", 5, null, "LIFE OF PI" },
                    { new Guid("93703d1c-3381-4617-bfb8-936c43f6b157"), 1, 10, "The conclusion of the Bill Hodges trilogy.", 5, null, "END OF WATCH" },
                    { new Guid("342d086d-addc-4ca8-b6df-305f4ae60a87"), 5, 10, "In the frozen wastes to the north of Winterfell, sinister and supernatural forces are mustering. Basis of the HBO series.", 7, null, "A GAME OF THRONES" },
                    { new Guid("fb1755eb-bac1-4661-82ff-f7917430460f"), 7, 10, "", 7, null, "WITCH OF PORTOBELLO" },
                    { new Guid("5e0aab77-fe54-47f5-a4df-04e7e8b88a8b"), 39, 10, "", 9, null, "A Life of Picasso, Volume II: 1907-1917 - The Painter of Modern Life" },
                    { new Guid("36e7a7b6-50b5-4305-8b9a-1d218c6f7591"), 20, 10, "", 9, null, "Cosette: The Sequel to Les Miserables" },
                    { new Guid("9d24ec5c-78f0-4152-a422-b78a8f8b0e28"), 19, 10, "After reconnecting with one of his first students, who is now a billionaire futurist, symbology professor Robert Langdon must go on a perilous quest with a beautiful museum director.", 9, null, "ORIGIN" },
                    { new Guid("b5873c9d-b80c-46b1-90af-8340e92e1b64"), 10, 10, "The beginning of the First Formic War; the back story to “Ender’s Game.”", 9, null, "EARTH UNAWARE" },
                    { new Guid("f82b3999-3b89-4d2d-93e6-ecfeee79ca1b"), 9, 10, "One hundred years before \"Ender's Game,\" the aliens arrived on Earth with fire and death. This is the story of the First Formic War.", 9, null, "EARTH AFIRE" },
                    { new Guid("2d592693-508b-4df8-b3ca-fa0165549eea"), 7, 10, "A young Brazillian man and a Dutch woman explore their relationship as they travel across Europe and Central Asia to Kathmandu.", 9, null, "HIPPIE" },
                    { new Guid("c1334977-dd86-4752-9b3f-0f11fa252ae6"), 1, 10, "", 9, null, "Different Seasons (Signet)" },
                    { new Guid("43a3fdc1-66af-4564-9300-e6f7c103bf3f"), 1, 10, "", 9, null, "Christine (Signet)" },
                    { new Guid("1e5f69b5-413d-4e9d-93df-c10e9b99c686"), 44, 10, "", 8, null, "THE ISLAMIC ANTICHRIST" },
                    { new Guid("2f387bd6-7d81-4a9b-8d7e-9c1a4463bcb3"), 41, 10, "", 8, null, "Generation Without Memory: A Jewish Journey in Christian America" },
                    { new Guid("4d20eed5-3009-4e61-8d21-933532cf50f0"), 30, 10, "", 8, null, "DARKNESS UNBOUND" },
                    { new Guid("2ad94fda-90c5-41ba-ba4e-96f2d1402fa1"), 23, 10, "", 8, null, "A Jew In America: My Life and A People's Struggle for Identity" },
                    { new Guid("e548c619-333e-45f2-b29b-d4c4380ffa2e"), 7, 10, "A Spanish shepherd boy ventures to Egypt in search of treasure and his destiny.", 7, null, "THE ALCHEMIST" },
                    { new Guid("8986fccd-a30f-41f1-afeb-ca70486da071"), 22, 10, "Thousands of years before the events of “The Lord of the Rings,” a hero named Tuor visits a secret city. Edited by Christopher Tolkien.", 8, null, "THE FALL OF GONDOLIN" },
                    { new Guid("34c95223-78b2-4590-83e3-2be14c18fbad"), 9, 10, "", 8, null, "THE LOST GATE" },
                    { new Guid("cd46d9cb-9003-425a-9792-df2393a2d973"), 9, 10, "", 8, null, "THE GATE THIEF" },
                    { new Guid("826135e9-b7ae-4871-8955-c68ea4002c57"), 1, 10, "This second novel of the Bill Hodges trilogy involves a dead writer, his obsessed murderer and a coveted manuscript.", 8, null, "FINDERS KEEPERS" },
                    { new Guid("cf33b54c-6aa9-4237-8848-01ad3f9e10b4"), 33, 10, "", 7, null, "Desire and Delusion: Three Novellas" },
                    { new Guid("c57ea465-249f-4a1e-b935-1361fc674edf"), 32, 10, "", 7, null, "Conversations with Miller" },
                    { new Guid("51cc23c6-7718-4aa6-b00a-70cf801d6ad9"), 19, 10, "The Harvard symbologist Robert Langdon among the Masons.", 7, null, "THE LOST SYMBOL" },
                    { new Guid("80e77a12-588d-4cbc-976f-5d1f41eda4d8"), 18, 10, "", 7, null, "The Mystery of Edwin Drood (Penguin Classics)" },
                    { new Guid("3e61c63f-fdbc-4386-9ead-6ebdf1561b0c"), 15, 10, "Two boys with the same name join forces in a reality possible only in musical theater.", 7, null, "WILL GRAYSON, WILL GRAYSON" },
                    { new Guid("e25ec47d-88b6-4a22-ba6d-ed1f19b25036"), 14, 10, "Aza and Daisy investigate a mystery with a reward of $100,000.", 7, null, "TURTLES ALL THE WAY DOWN" },
                    { new Guid("bf66e784-9fe8-4e03-8beb-c819da309f5a"), 11, 10, "In a dystopia, a girl fights for survival on live TV.", 7, null, "THE HUNGER GAMES" },
                    { new Guid("755945e2-7d6d-4e16-8842-732c8ead3bc0"), 9, 10, "Thousands of years after the events of \"Ender's Game,\" a second alien race has been discovered.", 7, null, "SPEAKER FOR THE DEAD" },
                    { new Guid("3137fad9-86be-46af-9802-1bcf10bbc3e3"), 10, 10, "", 7, null, "EARTH AWAKENS" },
                    { new Guid("9b33117f-32ef-4d36-86bd-6d899902035b"), 19, 10, "The symbologist Robert Langdon, on the run in Florence, must decipher a series of codes created by a Dante-loving scientist.", 8, null, "INFERNO" },
                    { new Guid("b95841a7-057d-43c2-984d-1fe23ff243ff"), 1, 10, "A Minnesota contractor moves to Florida to recover from an injury and starts creating paintings with eerie powers.", 5, null, "DUMA KEY" },
                    { new Guid("c39c4de1-d375-4c06-8e50-d9c373164169"), 1, 10, "A tale about the dark side of baseball, circa 1957.", 5, null, "BLOCKADE BILLY" },
                    { new Guid("4c5159cf-fc4a-419f-958c-63b21f78ced4"), 1, 10, "An unsuspecting accountant's wife makes a disturbing discovery; first published in 2010, now a movie.", 5, null, "A GOOD MARRIAGE" },
                    { new Guid("42458cc8-0db0-4a7b-94ed-2162bd433dcb"), 43, 10, "A five-step system for meeting sales objectives and increasing business.", 2, null, "PERFECT SELLING" },
                    { new Guid("35c6ddde-0cc8-4b6a-8c7e-d79529789585"), 42, 10, "Behind-the-scenes photographs of the pop star.", 2, null, "LADY GAGA x TERRY RICHARDSON" },
                    { new Guid("ed707d8a-4114-4fe0-82c8-b65f9bbcb404"), 26, 10, "", 2, null, "Angelica: A Novel" },
                    { new Guid("882fb78c-ff92-4986-b4e6-df5a6ec5f7b8"), 24, 10, "", 2, null, "Agitations: Essays on Life and Literature" },
                    { new Guid("984e485f-acbc-4b2c-a7b8-c6a8baf42ab3"), 22, 10, "The love of a mortal man for an immortal elf, which figures in “The Silmarillion” and is part of the back story of “Lord of the Rings.” Edited by Christopher Tolkien.", 2, null, "BEREN AND LÚTHIEN" },
                    { new Guid("13bd1f9f-595e-40f7-9232-fef23466c82b"), 18, 10, "", 2, null, "Oliver Twist (Dover Thrift Editions)" },
                    { new Guid("55fc6ea9-93de-44d9-9787-1f8bf772e32a"), 16, 10, "", 2, null, "Memoirs of a Geisha: A Novel" },
                    { new Guid("e5e173c2-96a7-4470-99fc-254e4415db8c"), 14, 10, "After a night of mischief, the girl Quentin loves disappears.", 2, null, "PAPER TOWNS" },
                    { new Guid("3f1e8ce6-8039-4785-af39-9c6be4c5df65"), 12, 10, "Vampires and werewolves and their intrigues in high school.", 2, null, "THE TWILIGHT SAGA" },
                    { new Guid("34e7b422-4a79-46c5-bd34-25ba913fcf19"), 11, 10, "", 2, null, "Mockingjay (The Final Book of The Hunger Games)" },
                    { new Guid("fcf19437-9f02-41b6-93e8-97b45e2e8ac3"), 11, 10, "The protagonist of \"The Hunger Games\" returns.", 2, null, "CATCHING FIRE" },
                    { new Guid("5674e733-db4c-4f2e-ac0d-efb9ac5520af"), 9, 10, "To develop a secure defense against a hostile alien race's next attack, government agencies breed child geniuses and train them as soldiers.", 2, null, "ENDER'S GAME" },
                    { new Guid("21826a25-af2e-40b5-98ea-044b9ed7ef25"), 40, 10, "Simple cupcake designs and recipes.", 2, null, "WHAT'S NEW, CUPCAKE?" },
                    { new Guid("288d67c7-a8c3-471e-9b11-8b88909e02c1"), 9, 10, "The latest entry in the “Ender” science fiction series.", 2, null, "ENDER IN EXILE" },
                    { new Guid("92d3dde0-5d2b-45cf-bf87-0d3b6082fbf8"), 5, 10, "After a colossal battle, the Seven Kingdoms face new threats.", 2, null, "A DANCE WITH DRAGONS" },
                    { new Guid("e6c7ee72-32d5-456e-b156-f8f89b727048"), 1, 10, "Now grown up, Dan, the boy with psycho-intuitive powers in “The Shining,” helps another child with a spectacular gift.", 2, null, "DOCTOR SLEEP" },
                    { new Guid("8fa08615-35bb-45ce-bc83-391fb9009014"), 1, 10, "", 2, null, "Cell: A Novel" },
                    { new Guid("bc80f9de-ba60-44b2-9cd2-0add86bd7cc5"), 25, 10, "", 1, null, "AN INQUIRY INTO THE NATURE AND CAUSES OF THE WEALTH OF STATES" },
                    { new Guid("be37797f-23dc-4a8f-ba65-20db8f98216d"), 21, 10, "Cyra and Akos fight Lazmet, the tyrant who was thought to be dead.", 1, null, "THE FATES DIVIDE" },
                    { new Guid("e2d94fb1-9ef4-4ffb-b318-a2166feedb00"), 13, 10, "The comic adaptation of the ever-popular vampire series.", 1, null, "TWILIGHT: THE GRAPHIC NOVEL, VOL. 2" },
                    { new Guid("1b39b13e-ca90-4846-8a71-ef55aa243554"), 12, 10, "In this first adult novel by the author of the Twilight series for teenagers, aliens have taken control of the minds and bodies of most humans, but one woman won’t surrender; originally published in 2008.", 1, null, "THE HOST" },
                    { new Guid("b06fa827-4ecd-4ff9-88bd-cefc18dce592"), 9, 10, "A boy can not only see the past but also revise it.", 1, null, "PATHFINDER" },
                    { new Guid("0ab10760-3a2e-4ee7-8720-29e76a683a75"), 7, 10, "A young Irish girl who desires to become a witch seeks wisdom from teachers of magic and spirituality.", 1, null, "BRIDA" },
                    { new Guid("da0576b1-5f4e-460f-b9fb-3a5de2af3c54"), 7, 10, "A crisis of faith is the impetus for a journey through time and space, on a path that teaches love, forgiveness and courage.", 1, null, "ALEPH" },
                    { new Guid("222bd987-8c23-4567-a926-319af6d8a032"), 5, 10, "Wars continue to rage over the Iron Throne as alliances are made and broken; Book 3 of \"A Song of Ice and Fire.\"", 1, null, "A STORM OF SWORDS" },
                    { new Guid("7fadb4d8-1de0-48b2-83c8-2e204b61c4ba"), 1, 10, "Grisly human behavior and its consequences drive this collection of stories.", 1, null, "FULL DARK, NO STARS" },
                    { new Guid("66d296ce-46d7-4cae-841c-47c367155aa0"), 6, 10, "Twenty-one original stories from well-known writers, including a new “Game of Thrones” story.", 2, null, "ROGUES" },
                    { new Guid("5aa59022-3868-4e72-bafe-82ff91503944"), 3, 10, "This series, about a new species of vampire that does not have the traditional weakness, shifts to Las Vegas in the 1930's and a number of corpses that have turned up drained of blood.", 3, null, "AMERICAN VAMPIRE, VOL. 1" },
                    { new Guid("ae2df220-6ed1-4867-8fd7-25ece0d1db85"), 8, 10, "A wizard hones his conjuring skills in the service of fighting evil.", 3, null, "HARRY POTTER" },
                    { new Guid("05c00020-9ff5-4a81-8d3f-a7501ed9c88f"), 12, 10, "A definitive guide to all things “Twilight.”", 3, null, "THE TWILIGHT SAGA: THE OFFICIAL ILLUSTRATED GUIDE" },
                    { new Guid("4a14c038-049f-46a4-82b8-f290196d2bc8"), 1, 10, "An English teacher travels back to 1958 by way of a time portal in a Maine diner. His assignment: Stop Lee Harvey Oswald.", 5, null, "11/22/63" },
                    { new Guid("6a6a360f-966d-47df-ba97-7d4e6b36d087"), 39, 10, "", 4, null, "The Sorcerer's Apprentice: Picasso, Provence, and Douglas Cooper" },
                    { new Guid("6ce1fffb-6bae-4415-8664-91f60ac39ffc"), 40, 10, "Tricks for new treat creations.", 4, null, "CUPCAKES, COOKIES AND PIE, OH, MY!" },
                    { new Guid("0f26e8e9-ed1a-46c9-8416-e59748533203"), 35, 10, "A biography and assessment of the influential twentieth-century American photographer.", 4, null, "DIANE ARBUS" },
                    { new Guid("69ada2ee-5fc5-4d88-86ef-12a53b5279a1"), 30, 10, "A woman and a man with superhuman powers flee dangerous killers from Scotland.", 4, null, "DESTINY KILLS" },
                    { new Guid("8d389395-16b2-49d3-8847-0761dffa374a"), 30, 10, "Riley Jenson, part vampire, part werewolf, juggles multiple murder cases and a pair of jealous lovers.", 4, null, "BOUND TO SHADOWS" },
                    { new Guid("bb9c7b19-efd7-4b41-9f0d-2cf163374e45"), 27, 10, "", 4, null, "Another Good Loving Blues" },
                    { new Guid("48f372c1-76a0-4298-b721-5e6706ed2910"), 21, 10, "", 4, null, "FREE FOUR" },
                    { new Guid("18441dcf-b21c-43a2-900c-07a23d8d4392"), 21, 10, "A girl must prove her mettle in a faction-ridden dystopia.", 4, null, "DIVERGENT" },
                    { new Guid("a679dd37-41f5-4c78-bbd7-4e20dc33093e"), 14, 10, "Colin Singleton wants to break the pattern of being dumped.", 4, null, "AN ABUNDANCE OF KATHERINES" },
                    { new Guid("43bd8a7d-fe35-470a-8cbd-88ddef4a26d9"), 9, 10, "Adventures and time travel continue in the second book of the “Pathfinder” series.", 4, null, "RUINS" },
                    { new Guid("77feccbe-e4fa-4b08-9893-958c8482f768"), 7, 10, "", 4, null, "THE SPY" },
                    { new Guid("b48a2ddf-48b0-41b5-b249-11ee112ea507"), 5, 10, "A girl tames a dragon and saves her farm.", 4, null, "THE ICE DRAGON" },
                    { new Guid("400e0605-acc4-4cee-bd73-ec76260a77e2"), 5, 10, "A set that includes the first four volumes of Martin's epic fantasy series.", 4, null, "A SONG OF ICE AND FIRE" },
                    { new Guid("b8a6673e-1ac3-4462-8af6-f187787a830e"), 5, 10, "A collection of three official prequels to “A Song of Ice and Fire.”", 4, null, "A KNIGHT OF THE SEVEN KINGDOMS" },
                    { new Guid("28a5ddb3-bc7d-4602-bc05-7f207c2b53df"), 5, 10, "The seven powers dividing the land have reached an uneasy truce; Book 4 of \"A Song of Ice and Fire.\"", 4, null, "A FEAST FOR CROWS" },
                    { new Guid("af10d648-c40c-4ea7-9a78-cc48685ace1e"), 2, 10, "An elderly widower watches baseballt to distract himself from his wife's death, but figures from his past appear every night in the seat behind home plate; a Kindle single.", 4, null, "A FACE IN THE CROWD" },
                    { new Guid("8aeefb9d-4f4d-4938-87e4-02d340e70e58"), 37, 10, "", 3, null, "Equality and Education: Federal Civil Rights Enforcement in the New York City School System" },
                    { new Guid("7771cca8-d331-4d96-9c38-e4a3bd583f09"), 36, 10, "", 3, null, "Dogwalker: Stories" },
                    { new Guid("c5f7f1dd-6f60-4c87-8bc6-65dddba79db4"), 34, 10, "", 3, null, "Detective" },
                    { new Guid("3cd34861-6f52-4004-a5ba-b1cc23d89606"), 29, 10, "", 3, null, "Boogaloo: The Quintessence of American Popular Music" },
                    { new Guid("7f7248e0-a5ac-42cf-a456-b4515b837dd7"), 28, 10, "", 3, null, "Beyond the Mountain" },
                    { new Guid("84c13418-d34d-414e-9c34-764bd8872e5f"), 21, 10, "An oracle's son is able to stop the painful flow of Cyra's powers.", 3, null, "CARVE THE MARK" },
                    { new Guid("c69aaf98-84f7-40b2-b537-da4470d46413"), 19, 10, "A scholar tries to save the Vatican from the machinations of an underground society.", 3, null, "ANGELS AND DEMONS" },
                    { new Guid("4dbc61ab-40fd-4bfe-8e01-468059442bb0"), 17, 10, "The tragic fate of the title characters, a donkey named Beatrice and a monkey named Virgil, stuffed animals in a taxidermy shop, is an allegory for the Holocaust; from the author of “The Life of Pi.”", 3, null, "BEATRICE AND VIRGIL" },
                    { new Guid("6ec99afc-ae0a-46ad-80a4-64b4b0bfa0a5"), 14, 10, "A girl faces new realities when she learns she has cancer.", 3, null, "THE FAULT IN OUR STARS" },
                    { new Guid("2a5af11c-de2f-4748-ae7a-b519c49d6568"), 14, 10, "A boy finds excitement when he meets a girl named Alaska.", 3, null, "LOOKING FOR ALASKA" },
                    { new Guid("ce63426c-81c7-45ae-93df-426c6f6d9ba1"), 39, 10, "", 9, null, "Sacred Monsters, Sacred Masters: Beaton, Capote, Dalí, Picasso, Freud, Warhol, and More" },
                    { new Guid("66b32ec8-50bc-4e43-8b02-a340361c4884"), 1, 10, "", 1, null, "Danse Macabre" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddOnDate", "AddressId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6dd79306-4615-4d65-8137-0800e2eee7bf", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "12b64ac5-7d3e-4758-af49-2daa2189c4d2", "admin@mail.com", true, "Admin", false, "AdminLastName", false, null, null, "ADMIN@MAIL.COM", "ADMINMAIN", "AQAAAAEAACcQAAAAECGnSEpH+224s7a7m2pB/KmkWGxlZKzmgwn4zw/YS9I3WY+zA4tz9vy7wy7oaDXFaQ==", "+111111111", true, "29de4e75-8597-4c98-97d1-c39e9c6176ce", false, "adminMain" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "6dd79306-4615-4d65-8137-0800e2eee7bf", "1" });

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

            migrationBuilder.CreateIndex(
                name: "IX_UsersReadBooks_BookId",
                table: "UsersReadBooks",
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
                name: "UsersReadBooks");

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
