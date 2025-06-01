using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bookstore.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nVarChar(50)", nullable: false),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nVarchar(100)", nullable: false),
                    ImageURL = table.Column<string>(type: "nVarchar(100)", nullable: false),
                    Description = table.Column<string>(type: "nVarchar(MAX)", nullable: false),
                    NumberOfPages = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookAuthors",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthors", x => new { x.AuthorId, x.BookId });
                    table.ForeignKey(
                        name: "FK_BookAuthors_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthors_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentText = table.Column<string>(type: "nVarChar(max)", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => new { x.BookId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_OrderItems_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "About", "ImageURL", "Name" },
                values: new object[,]
                {
                    { 1, "هو طبيب وأديب مصري، ويعتبر من أشهر الكتاب في مجال أدب الرعب وأدب الشباب. ولد في مدينة طنطا عاصمة محافظة الغربية في مصر. متزوج وأب لطفلين هما محمد (12 سنة) ومريم (8 سنوات).\r\nتخرج أحمد خالد توفيق في كلية الطب في جامعة طنطا عام 1985 م وحصل على الدكتوراه في طب المناطق الحارة عام 1997 م. يقدم أحمد خالد توفيق ستة سلاسل للروايات وصلت إلى ما يقرب من 236 عددا، وقد قام بترجمة عدد من الروايات الأجنبية ضمن سلسلة روايات عالمية للجيب. كما قدّم الترجمة العربية الوحيدة لرواية نادي القتال (fight club) للروائي الأمريكي تشاك بولانيك. كما أن له بعض التجارب الشعرية.\r\nوتُوفى إلى رحمة الله تعالى عن عمر يناهز 55 عامًا أثر أزمة صحية ألمت به(عليه رحمة الله تعالى وأسكنه الله فسيح جناته وأبدله سيئاته حسنات إن شاء الله)", "~/images/Authors/أحمد_خالد_توفيق.jpg", "أحمد خالد توفيق" },
                    { 2, " عاش مصطفى محمود في مدينة طنطا إلى جوار مسجد السيد البدوي الذي يعد أحد مزارات الصوفية الشهيرة في مصر مما ترك فيه نفسه أثرا واضحا ظهر في توجهاته وأفكاره ولقد تحدث مصطفى محمود عن تكوينه الفكري وعن طفولته التي هي مفتاح ذلك التكوين قائلا : لا اذكر من طفولتي إلا الأحلام التي كنت أتخيل فيها أنى عالم ومخترع أو رحال أو بطل من إبطال التاريخ كما أذكر حبي للموسيقى وللشعر ، وفى صباي تعلمت العزف على الناي وفى شبابي درست العزف على العود ، وكنت أكتب في أيام الدراسة الابتدائية الزجل والشعر وفى الثانوية القصص والمقالات والمسرحيات وهويت العلوم ، وأنشأت معملا للكيمياء والبيولوجيا في بيتى وكنت أحضر الغازات واشرح الضفادع ، نشرت لي أول قصة في مجلة الرسالة عام 1947 ثم بعد ذلك نشرت القصة الثانية في جريدة المصري ، ثم اشتغلت في أخر ساعة وأخبار اليوم وفى عام 1952 كنت احد مؤسسي مجلة التحرير وفى عام 1956 اشتغلت بمجلة روزاليوسف.\r\n\r\nكانت حياتي الأدبية خلال ثلاثين عاما هجرة مستمرة نحو إدراك الحياة والبحث عن الحقيقة وكان كل كتاب محطة على طريق هذا السفر الطويل.\r\n\r\n", "~/images/Authors/مصطفى محمود.jpg", "مصطفى محمود" },
                    { 3, "إبراهيم السكران (ولد في 5 ربيع الآخر 1396 هـ الموافق 4 أبريل 1976م) باحث ومُفكِّر إسلامي، مهتمٌ بمنهج الفقه الإسلامي وبالمذاهب العقدية والفكرية، له العديد من المؤلفات والأبحاث والمقالات المنشورة وله عدد من الكتب المطبوعة.\r\n\r\nسيرته\r\nهو أبو عُمر، إبراهيم بن عمر بن إبراهيم السكران المشرف الوهبي التميمي، درس في جامعة الملك فهد للبترول والمعادن سنة واحدة، ثم تركها متوجهًا إلى كلية الشريعة في جامعة الإمام محمد بن سعود الإسلامية بالرياض وتخرج منها، نال درجة الماجستير في السياسة الشرعيّة من المعهد العالي للقضاء التابع لجامعة الإمام محمد بن سعود الإسلامية بالرياض، ثم توجه إلى بريطانيا ونال درجة الماجستير في القانون التجاري الدولي في جامعة إسكس بمدينة كولشيستر.\r\n\r\nظهر تحوّل إبراهيم السكران إلى الفكر السلفي عام 1428 هـ 2007 م في ورقة فكريّة انتشرَت وقتها الكترونيًا، عنوانها: مآلات الخِطاب المدني، وقد أثارت هذه الورقة الكثير من المحسوبين على التيار التنويري.  ", "~/images/Authors/إبراهيم السكران.jpeg", "إبراهيم السكران" },
                    { 4, "أحمد مراد أحمد عبسلام (مواليد القاهرة 1978) كاتب ومصور ومصمم جرافيك مصري، تخرّج في مدرسة ليسيه الحرّية بباب اللوق عام 1996 قبل أن يلتحق بالمعهد العالي للسينما ليدرس التصوير السينمائي، وتخرّج عام 2001 بترتيب الأول على القسم، ونالت أفلام تخرّجه \"الهائمون - الثلاث ورقات - وفي اليوم السابع\" جوائز للأفلام القصيرة في مهرجانات بإنجلترا وفرنسا وأوكرانيا.\r\nبدأ مراد كتابة روايته الأولي \"فيرتيجو\" في شتاء عام 2007، ونُشِرت في نفس العام عن دار ميريت قبل أن تُترجم للّغة الإنجليزية عن دار بلومزبيري، ثم للإيطالية عن دار مارسيليو، والفرنسية عن دار فلاماريون، ثم تحولت الرواية لمسلسل تليفزيوني في رمضان من عام 2012 بعنوان فيرتيجو بطولة هند صبري، ونالت الرواية جائزة \"البحر الأبيض المتوسط للثقافة\" لعام 2013 من إيطاليا.\r\nفي فبراير 2010 أصدر مراد روايته الثانية بعنوان \"تراب الماس\" وتُرجمت للإيطالية عن دار مارسيليو وللألمانية، وتم تحويلها لفيلم سينمائي هذا العام 2018.من بطوله اسر ياسين و ماجد الكدوانى.\r\nوفي أكتوبر 2012 صدرت روايته الثالثة \"الفيل الأزرق\" والتي نالت المركز الأول في مبيعات الكتب بمعرض القاهرة الدولي للكتاب 2013، ورشحت الرواية في القائمة القصيرة لجائزة البوكر العربية عام 2014.", "~/images/Authors/احمد مراد.jpg", "أحمد مراد" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Category", "Description", "ImageURL", "NumberOfPages", "Title" },
                values: new object[,]
                {
                    { 1, "Literature", "لم يكن \"طه\" سوى مندوب دعاية طبية فى شركة أدوية, حياة باهتة رتيبة, بدلة وكرافتة وحقيبة جلدية ولسان لبق يستميل أعتى الأطباء لأدويته.. كان ذلك قبل أن يسقط..\r\nجريمة قتل غامضة تتركه خلفها وقد تبدل عالمه.. للأبد.. تتحول حياته إلى جزيرة من الأسرار, يبدأ اكتشافها فى دفتر عتيق يعثر عليه مصادفة, ويجد أداة رهيبة لها فعل السحر.. سنقرأ هنا كيف تتحول هذه الجريمة إلى سلسلة من عمليات القتل. وكيف يصبح القتل بابا يكشف لنا عالما من الفساد, وسطوة السلطة التى تمتد لأجيال فى تتابع مثير لا يؤكد أبدا أن \"طه\" سيصل إلى نهايته..", "~/Images/Books/تراب الماس.webp", 448, "تراب الماس" },
                    { 2, "Literature", "الرواية فانتازية خيالية تدور أحداثها حول دكتور للأمراض العقلية يدعى - يحيى - كان يعمل في مستشفى العباسية ، كان مدمنا للخمر وتعرض لحادث أدى إلى وفاة زوجته وابنته ، فانعزل عن العالم لمدة خمس سنوات وبعد انقطاع عن العمل عاد مرة أخرى للعمل في نفس المستشفى إلا أنه يتفاجأ بوجود صديق قديم يدعى - شريف - وهو نزيل بالمستشفى كان له ماضي أراد أن ينساه طويلا إلا أنه وقع تحت رحمة يحيى ، فتنقلب حياة يحيى وصديقه راسا على عقب حيث يبدأ يحيى بالتنقيب والبحث عن حقيقة صديقه عبرة رحلة استكشافية مثيرة مليئة بالعالم الغريب والخيالي ليظهر بها الكاتب الخبايا النفسية للبشر وغرائبها .", "~/Images/Books/Al-Feel_al-Azraq.jpg", 485, "الفيل الأزرق" },
                    { 3, "Literature", "سيختطفك أحمد مراد في آلة زمن، ليهبط بك في حقبة تغلي فيها القاهرة بالأحداث.. وثبة زمنية إلى عالم متشابك يمسك المؤلف مقتدرًا بكل مفاتيحه؛ بين سعد زغلول وتعنت البريطانيين، قصة الوفد ومقهى \"متاتيا\" جماعة سرية تعمل تحت مقهى \"ريش\"، وعوالم البغاء المقنن.. شخصيات عديدة سوف تتعاطف معها أو تمقتها، أو تفعل الشيئين بلا تحفظ، بحوار مفعم بالحيوية حتى لتوشك على سماعه يتردد في أذنك، وتفاصيل تاريخية مضنية ودقيقة. سوف تدرك أن البعض ما زال مصرًّا على الجدية والإتقان.", "~/Images/Books/1919.jpeg", 485, "1919" },
                    { 4, "Literature", "قصة فلسطينية مؤلمة تحكي معاناة ثلاثة رجال في طريق هجرتهم إلى الكويت، بحثًا عن الأمل والهروب من الاحتلال، ليواجهوا مصيرًا مفجعًا داخل صهريج شاحنة.", "~/Images/Books/رجال في الشمس.jpg", 120, "رجال في الشمس" },
                    { 5, "Religious", "كتاب فلسفي رائع يروي رحلة مصطفى محمود الفكرية من الإنكار إلى الإيمان، بأسلوب عميق مليء بالتساؤلات والبحث عن الحقيقة.", "~/Images/Books/رحلتي من الشك إلى الإيمان.jpg", 160, "رحلتي من الشك إلى الإيمان" },
                    { 6, "Religious", "رحلة تفسيرية متميزة تركز على الجوانب الفكرية والفلسفية للقرآن الكريم، وتقدم فهمًا عميقًا للآيات بعيدًا عن التقليدية.", "~/Images/Books/رقائق القرآن.webp", 300, "رقائق القرآن" },
                    { 7, "Literature", "حكاية بوليسية شيقة تدور في أروقة البنوك والمؤامرات المالية، حيث تكشف الأحداث عن لعبة ضخمة من الخداع والسرقة في عالم المصارف.", "~/Images/Books/رواية_البنكوت.jpg", 410, "رواية البنكوت" },
                    { 8, "Religious", "من أهم كتب الحديث الشريف، يجمع بين الأخلاق والعبادات في أحاديث نبوية مختارة بعناية لتربية النفس وتزكيتها.", "~/Images/Books/مسلكيات.avif", 500, "مسلكيات" },
                    { 9, "Religious", "كتاب صوفي عميق للإمام ابن القيم، يشرح فيه منازل السائرين إلى الله في طريق العبودية، بلغة قوية وعقيدة سليمة.", "~/Images/Books/مدارج السالكين.jpg", 600, "مدارج السالكين" },
                    { 10, "Literature", "مجموعة قصصية مشوقة تدور أحداثها داخل غرفة غامضة تحمل أسرارًا لا تنتهي، حيث يصبح الدخول إليها تجربة تغير حياة كل من يقترب منها.", "~/Images/Books/حكايات الغرفة 207.webp", 200, "حكايات الغرفة 207" },
                    { 11, "Literature", "رحلة فكرية مرعبة في دهاليز العقل والروح، تجمع بين التشويق والخيال العلمي، بأسلوب فلسفي عميق يلامس النفس الإنسانية.", "~/Images/Books/عقل بلا جسد.webp", 190, "عقل بلا جسد" },
                    { 12, "Literature", "مجموعة من القصص القصيرة التي تستعرض مواقف إنسانية في ظلال الليل، حيث تتجلى مشاعر الوحدة والخوف والحنين.", "~/Images/Books/رفقاء الليل.webp", 240, "رفقاء الليل" },
                    { 13, "History", "كتاب تاريخي يقدم عرضًا مبسطًا وممتعًا لحياة المصريين القدماء، وعاداتهم، وعلومهم، وأسلوب حياتهم اليومي.", "~/Images/Books/في ممر القديمة.webp", 310, "في ممر القديمة" },
                    { 14, "Literature", "رواية سوداوية عن مستقبل مرعب، تتخيل انقسام المجتمع المصري إلى طبقتين: الأغنياء في الجنة والطبقة المسحوقة في الجحيم، في إطار درامي فلسفي.", "~/Images/Books/يوتوبيا.webp", 172, "يوتوبيا" }
                });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "AuthorId", "BookId" },
                values: new object[,]
                {
                    { 1, 10 },
                    { 1, 11 },
                    { 1, 12 },
                    { 1, 13 },
                    { 1, 14 },
                    { 2, 5 },
                    { 2, 7 },
                    { 3, 6 },
                    { 3, 8 },
                    { 3, 9 },
                    { 4, 1 },
                    { 4, 2 },
                    { 4, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_BookId",
                table: "BookAuthors",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BookId",
                table: "Comments",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthors");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
