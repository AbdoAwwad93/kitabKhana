using Bookstore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(book => book.Id);
            builder.Property(book => book.Id).ValueGeneratedNever();
            builder.Property(book => book.Title).HasColumnType("nVarchar(100)");
            builder.Property(book => book.ImageURL).HasColumnType("nVarchar(100)");
            builder.Property(book => book.Description).HasColumnType("nVarchar(MAX)");
            builder.Property(book => book.Category).HasConversion<string>();

            builder.HasMany(book => book.Authors)
                .WithMany(author => author.Books)
                .UsingEntity<BookAuthor>();

            builder.HasMany(book => book.Comments)
                .WithOne(comment => comment.Book)
                .HasForeignKey(comment => comment.BookId);

            builder.HasMany(book => book.Orders)
                .WithMany(order => order.Books)
                .UsingEntity<OrderItem>();

            builder.HasData(BooksData());

        }

        private List<Book> BooksData()
        {
            return new List<Book>
            {
                new Book{Id = 1, Title="تراب الماس",Category= Category.Literature,ImageURL="~/Images/Books/تراب الماس.webp", NumberOfPages=448,Description="لم يكن \"طه\" سوى مندوب دعاية طبية فى شركة أدوية, حياة باهتة رتيبة, بدلة وكرافتة وحقيبة جلدية ولسان لبق يستميل أعتى الأطباء لأدويته.. كان ذلك قبل أن يسقط..\r\nجريمة قتل غامضة تتركه خلفها وقد تبدل عالمه.. للأبد.. تتحول حياته إلى جزيرة من الأسرار, يبدأ اكتشافها فى دفتر عتيق يعثر عليه مصادفة, ويجد أداة رهيبة لها فعل السحر.. سنقرأ هنا كيف تتحول هذه الجريمة إلى سلسلة من عمليات القتل. وكيف يصبح القتل بابا يكشف لنا عالما من الفساد, وسطوة السلطة التى تمتد لأجيال فى تتابع مثير لا يؤكد أبدا أن \"طه\" سيصل إلى نهايته.."},
                new Book{Id = 2, Title="الفيل الأزرق",Category= Category.Literature,ImageURL="~/Images/Books/Al-Feel_al-Azraq.jpg",NumberOfPages=485,Description="الرواية فانتازية خيالية تدور أحداثها حول دكتور للأمراض العقلية يدعى - يحيى - كان يعمل في مستشفى العباسية ، كان مدمنا للخمر وتعرض لحادث أدى إلى وفاة زوجته وابنته ، فانعزل عن العالم لمدة خمس سنوات وبعد انقطاع عن العمل عاد مرة أخرى للعمل في نفس المستشفى إلا أنه يتفاجأ بوجود صديق قديم يدعى - شريف - وهو نزيل بالمستشفى كان له ماضي أراد أن ينساه طويلا إلا أنه وقع تحت رحمة يحيى ، فتنقلب حياة يحيى وصديقه راسا على عقب حيث يبدأ يحيى بالتنقيب والبحث عن حقيقة صديقه عبرة رحلة استكشافية مثيرة مليئة بالعالم الغريب والخيالي ليظهر بها الكاتب الخبايا النفسية للبشر وغرائبها ."},
                new Book{Id = 3, Title="1919",Category= Category.Literature,ImageURL="~/Images/Books/1919.jpeg",NumberOfPages=485,Description="سيختطفك أحمد مراد في آلة زمن، ليهبط بك في حقبة تغلي فيها القاهرة بالأحداث.. وثبة زمنية إلى عالم متشابك يمسك المؤلف مقتدرًا بكل مفاتيحه؛ بين سعد زغلول وتعنت البريطانيين، قصة الوفد ومقهى \"متاتيا\" جماعة سرية تعمل تحت مقهى \"ريش\"، وعوالم البغاء المقنن.. شخصيات عديدة سوف تتعاطف معها أو تمقتها، أو تفعل الشيئين بلا تحفظ، بحوار مفعم بالحيوية حتى لتوشك على سماعه يتردد في أذنك، وتفاصيل تاريخية مضنية ودقيقة. سوف تدرك أن البعض ما زال مصرًّا على الجدية والإتقان."},
                new Book
    {
        Id = 4,
        Title = "رجال في الشمس",
        Category = Category.Literature,
        ImageURL = "~/Images/Books/رجال في الشمس.jpg",
        NumberOfPages = 120,
        Description = "قصة فلسطينية مؤلمة تحكي معاناة ثلاثة رجال في طريق هجرتهم إلى الكويت، بحثًا عن الأمل والهروب من الاحتلال، ليواجهوا مصيرًا مفجعًا داخل صهريج شاحنة."
    },
    new Book
    {
        Id = 5,
        Title = "رحلتي من الشك إلى الإيمان",
        Category = Category.Religious,
        ImageURL = "~/Images/Books/رحلتي من الشك إلى الإيمان.jpg",
        NumberOfPages = 160,
        Description = "كتاب فلسفي رائع يروي رحلة مصطفى محمود الفكرية من الإنكار إلى الإيمان، بأسلوب عميق مليء بالتساؤلات والبحث عن الحقيقة."
    },
    new Book
    {
        Id = 6,
        Title = "رقائق القرآن",
        Category = Category.Religious,
        ImageURL = "~/Images/Books/رقائق القرآن.webp",
        NumberOfPages = 300,
        Description = "رحلة تفسيرية متميزة تركز على الجوانب الفكرية والفلسفية للقرآن الكريم، وتقدم فهمًا عميقًا للآيات بعيدًا عن التقليدية."
    },
    new Book
    {
        Id = 7,
        Title = "رواية البنكوت",
        Category = Category.Literature,
        ImageURL = "~/Images/Books/رواية_البنكوت.jpg",
        NumberOfPages = 410,
        Description = "حكاية بوليسية شيقة تدور في أروقة البنوك والمؤامرات المالية، حيث تكشف الأحداث عن لعبة ضخمة من الخداع والسرقة في عالم المصارف."
    },
    new Book
    {
        Id = 8,
        Title = "مسلكيات",
        Category = Category.Religious,
        ImageURL = "~/Images/Books/مسلكيات.avif",
        NumberOfPages = 500,
        Description = "من أهم كتب الحديث الشريف، يجمع بين الأخلاق والعبادات في أحاديث نبوية مختارة بعناية لتربية النفس وتزكيتها."
    },

    new Book
    {
        Id = 9,
        Title = "مدارج السالكين",
        Category = Category.Religious,
        ImageURL = "~/Images/Books/مدارج السالكين.jpg",
        NumberOfPages = 600,
        Description = "كتاب صوفي عميق للإمام ابن القيم، يشرح فيه منازل السائرين إلى الله في طريق العبودية، بلغة قوية وعقيدة سليمة."
    },
        new Book
    {
        Id = 10,
        Title = "حكايات الغرفة 207",
        Category = Category.Literature,
        ImageURL = "~/Images/Books/حكايات الغرفة 207.webp",
        NumberOfPages = 200,
        Description = "مجموعة قصصية مشوقة تدور أحداثها داخل غرفة غامضة تحمل أسرارًا لا تنتهي، حيث يصبح الدخول إليها تجربة تغير حياة كل من يقترب منها."
    },
    new Book
    {
        Id = 11,
        Title = "عقل بلا جسد",
        Category = Category.Literature,
        ImageURL = "~/Images/Books/عقل بلا جسد.webp",
        NumberOfPages = 190,
        Description = "رحلة فكرية مرعبة في دهاليز العقل والروح، تجمع بين التشويق والخيال العلمي، بأسلوب فلسفي عميق يلامس النفس الإنسانية."
    },
    new Book
    {
        Id = 12,
        Title = "رفقاء الليل",
        Category = Category.Literature,
        ImageURL = "~/Images/Books/رفقاء الليل.webp",
        NumberOfPages = 240,
        Description = "مجموعة من القصص القصيرة التي تستعرض مواقف إنسانية في ظلال الليل، حيث تتجلى مشاعر الوحدة والخوف والحنين."
    },
    new Book
    {
        Id = 13,
        Title = "في ممر القديمة",
        Category = Category.History,
        ImageURL = "~/Images/Books/في ممر القديمة.webp",
        NumberOfPages = 310,
        Description = "كتاب تاريخي يقدم عرضًا مبسطًا وممتعًا لحياة المصريين القدماء، وعاداتهم، وعلومهم، وأسلوب حياتهم اليومي."
    },
    new Book
    {
        Id = 14,
        Title = "يوتوبيا",
        Category = Category.Literature,
        ImageURL = "~/Images/Books/يوتوبيا.webp",
        NumberOfPages = 172,
        Description = "رواية سوداوية عن مستقبل مرعب، تتخيل انقسام المجتمع المصري إلى طبقتين: الأغنياء في الجنة والطبقة المسحوقة في الجحيم، في إطار درامي فلسفي."
    }
            };     
        }
    }
}