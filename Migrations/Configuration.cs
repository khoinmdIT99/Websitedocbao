using ltweb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ltweb.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ltweb.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ltweb.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            SeedRoles(context);
            SeedUsers(context);
            SeedCategories(context);
            SeedNews(context);
        }

        private void SeedRoles(ApplicationDbContext context)
        {
            var roleMan = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            IdentityRole[] roles =
            {
                new IdentityRole("Admin"),
                new IdentityRole("Writer"),
                new IdentityRole("User"),
            };

            foreach (IdentityRole role in roles)
            {
                if (!context.Roles.Any(r => r.Name == role.Name))
                    roleMan.Create(role);
            }
        }

        private void SeedUsers(ApplicationDbContext context)
        {
            var userMan = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            IdentityResult result;

            ApplicationUser admin = new ApplicationUser()
            {
                //Id = "4C1D99EE-931F-4E57-B84D-763EE3451475",
                Email = "admin@badauhoi.com",
                EmailConfirmed = true,
                UserName = "admin",
                //PasswordHash = new PasswordHasher().HashPassword("admin"),
            };
            if (!context.Users.Any(u => u.UserName == admin.UserName))
            {
                if ((result = userMan.Create(admin, "Mk@1234")).Succeeded)
                    userMan.AddToRole(admin.Id, "Admin");
                else
                    throw new Exception("[CREATE \"admin\"] " + string.Join("\n", result.Errors));
            }

            ApplicationUser writer = new ApplicationUser()
            {
                //Id = "8EF0DD97-206F-43F1-B34B-4A4ADB651DFB",
                Email = "writer1@badauhoi.com",
                EmailConfirmed = true,
                UserName = "writer1",
                //PasswordHash = new PasswordHasher().HashPassword("writer1")
            };
            if (!context.Users.Any(u => u.UserName == writer.UserName))
            {
                if ((result = userMan.Create(writer, "Mk@1234")).Succeeded)
                    userMan.AddToRole(writer.Id, "Writer");
                else
                    throw new Exception("[CREATE \"writer1\"] " + string.Join("\n", result.Errors));
            }
            context.SaveChanges();

            ApplicationUser user = new ApplicationUser()
            {
                //Id = "2F437318-FEDC-4A7B-AD6F-51E7411EA828",
                Email = "user@email.com",
                UserName = "user",
                EmailConfirmed = true,
                //PasswordHash = new PasswordHasher().HashPassword("user")
            };
            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                if ((result = userMan.Create(user, "Mk@1234")).Succeeded)
                    userMan.AddToRole(user.Id, "User");
                else
                    throw new Exception("[CREATE \"user\"] " + string.Join("\n", result.Errors));
            }
            context.SaveChanges();
        }

        private void SeedCategories(ApplicationDbContext context)
        {
            Category[] cats =
            {
                new Category(1, "Thời sự"), 
                new Category(2, "Thể thao"), 
                new Category(3, "Giải trí"), 
            };

            context.Categories.AddOrUpdate(cats);
            context.SaveChanges();
        }

        private void SeedNews(ApplicationDbContext context)
        {
            // Scrape from VnExpress at 14/06/20
            News[] ns =
            {
                new News()
                {
                    Id = 1,
                    CategoryId = 1,
                    Title = "Đánh bả khiến xác chó rải khắp đường",
                    Description =
                        "Hai người tình nghi đánh bả khiến hơn 40 con chó, mèo chết trong đêm ở xã Thanh Hoà, huyện Như Xuân đã bị người dây vây bắt.",
                    CoverImage = "/images/cho-7730-1592134452.jpg",
                    SubDescription = " Hai người tình nghi đánh bả khiến hơn 30 con chó, mèo chết trong đêm ở xã Thanh Hoà, huyện Như Xuân đã bị người dân vây bắt.Rạng sáng 14/6, người dân xã Thanh Hòa phát hiện xác các con vật nằm la liệt khắp đường, nghi có kẻ xấu đến gây án nên tổ chức truy tìm, báo nhà chức trách. Ông Lê Văn Tuyên, Chủ tịch UBND xã Thanh Hòa, cho biết một đôi nam nữ quê Hải Dương bị nghi là thủ phạm đã bị khống chế, bàn giao Công an huyện. Nhà chức trách xác định hơn 30 con chó, mèo đã chết do bị đánh bả. Một số đựng trong bao tải được đôi nam nữ chở trên xe máy khi bị vây bắt."
                },
                new News()
                {
                    Id = 2,
                    CategoryId = 1,
                    Title = "Mưa lớn, máy bay trượt khỏi đường băng",
                    Description =
                        "Máy bay VJ322 hãng Vietjet từ Phú Quốc, Kiên Giang đáp xuống sân bay Tân Sơn Nhất trong cơn mưa lớn đã trượt khỏi đường băng, trưa 14/6.",


                     CoverImage = "/images/Vietjet-tru-o-t-kho-i-du-o-ng-5700-5271-1592121752.jpg",
                     SubDescription = "Mưa lớn gây trơn trượt, gió mạnh làm nghiêng cánh máy bay... hoặc lỗi của phi công đều có thể khiến chuyến bay VJ322 lao khỏi đường băng Tân Sơn Nhất.Tổng Công ty quản lý bay Việt Nam (Bộ Giao thông Vận tải) cho biết, chuyến bay VJ322 của hãng Vietjet chở 217 hành khách hạ cánh xuống Tân Sơn Nhất lúc có mưa lớn, trưa 14/6. Tuy nhiên, điều kiện thời tiết về tầm nhìn, độ cao mây, gió vẫn đảm bảo cho việc cất hạ cánh.Trang Flightradar24 ghi nhận, trong 30 phút trước khi VJ322 gặp sự cố có 7 chuyến bay hạ cánh bình thường tại sân bay Tân Sơn Nhất. Máy bay VJ322 được phi công điều khiển tiếp đất đúng đường băng 25L/07R, sau khi chạy xả đà một đoạn bị trượt ra mép ngoài đường băng.Ngoài ra, một chuyến VJ137 từ Hà Nội đã chuyển hướng hạ cánh tại sân bay Cần Thơ, hai chuyến khác của Vietnam Airlines và Jetstar bị hủy.Bước đầu chúng tôi đánh giá việc điều hành của kíp trực không lưu đối với chuyến bay VJ322 đúng quy trình. Hiện, Tổng công ty chưa nhận được yêu cầu đình chỉ kíp trực để phục vụ điều tra, đại diện Tổng Công ty quản lý bay nói và cho rằng việc máy bay trượt khỏi đường băng có thể từ nhiều nguyên nhân, khi máy bay chạy xả đà với tốc độ cao (200-300 km/h) chỉ cần có sơ suất nhỏ về kỹ thuật hay thao tác sai của người lái có thể gây ra sự cố."
                },
                new News()
                {
                    Id = 3,
                    CategoryId = 1,
                    Title = "Vựa phế liệu 2.000 m2 cháy rụi",
                    Description =
                        "Vựa phế liệu rộng 2.000 m2 trong khu dân cư phường Thái Hòa, thị xã Tân Uyên, bị lửa thiêu rụi hoàn toàn, sáng 14/6.",
                    CoverImage = "/images/chay-2-3523-1592113869.jpg",
                    SubDescription = "Vựa phế liệu rộng 2.000 m2 trong khu dân cư phường Thái Hòa, thị xã Tân Uyên, bị lửa thiêu rụi hoàn toàn, sáng 14/6.Lửa bốc lên lúc 9h, tại khu vực chứa nhiều nguyên liệu dễ cháy. Trong chốc lát, đám cháy bao trùm cả vùng rộng lớn. Cột khói bốc cao hàng trăm mét kèm nhiều tiếng nổ lớn. Người dân sống gần vựa phế liệu nháo nhào di dời tài sản.Lực lượng PCCC Bình Dương điều hơn 10 xe chuyên dụng đến hiện trường, ưu tiên dập lửa không cho cháy lan sang nhà dân. Hơn hai giờ, đám cháy được khống chế.Hoả hoạn không gây thương vong. Nguyên nhân cháy thiệt hại tài sản đang được làm rõ."
                },
                new News()
                {
                    Id = 4,
                    CategoryId = 3,
                    Title = "Bằng Kiều cùng vợ cũ và các con nhảy múa, đàn hát",
                    Description =
                        "Bằng Kiều cùng vợ cũ - Trizzie Phương Trinh - và ba con trai nhảy múa, đàn hát trong những ngày ở nhà tránh dịch.",
                    CoverImage = "/images/BB15sqv1.jfif",
                    SubDescription="Trong vlog mới đây, ca sĩ Bằng Kiều đã chia sẻ một ngày quây quần bên vợ cũ Trizzie Phương Trinh và 3 cậu con trai.Khi nhìn thấy hình ảnh cả gia đình nam ca sĩ tụ họp, rất nhiều người đã thể hiện sự ngưỡng mộ khi Bằng Kiều và Phương Trinh dù ly hôn nhưng vẫn giữ mối quan hệ tốt, các con được sống vui vẻ, hạnh phúc và được nhận tình cảm từ cả bố lẫn mẹ.Bên cạnh đó, 3 cậu con trai của Bằng Kiều cũng đều đã lớn, ra dáng thiếu niên và vô cùng điển trai. Không chỉ đưa các con sang nhà Bằng Kiều chơi, Trizzie Phương Trinh còn làm đồ uống mang qua cho nam ca sĩ và mẹ anh. Nữ ca sĩ còn ở lại làm bữa tối, khoảnh khắc cả gia đình 5 người quây quần bên bàn ăn vô cùng ấm áp. Chính vì vậy mà dưới phần bình luận, nhiều cư dân mạng cũng nói rằng họ mong Trizzie Phương Trinh và Bằng Kiều tái hợp. Hiện tại, do tình hình ở Mỹ cũng đang khó khăn do dịch bệnh nên cả Bằng Kiều và vợ cũ đều không đi hát mà chỉ làm những công việc khác để trang trải cuộc sống. Trong khi Trizzie Phương Trinh phải đóng cửa club, thay vào đó là bán đồ uống và giao hàng tận nơi thì Bằng Kiều lại tranh thủ thời gian này ở nhà làm vườn, trồng cây trái, sửa sang nhà cửa."
                },
                new News()
                {
                    Id = 5,
                    CategoryId = 3,
                    Title = "Nhan sắc tuổi 20 của con gái diễn viên 'Bao Thanh Thiên'",
                    Description =
                        "Lâm Khải Linh, con gái 20 tuổi của Cung Từ Ân - diễn viên \"Bao Thanh Thiên\" - được các thương hiệu săn đón nhờ có gu thẩm mỹ.",
                    CoverImage = "/images/Khai-li-5119-1592107285.jpg",
                    SubDescription= "Lâm Khải Linh, con gái 20 tuổi của Cung Từ Ân - diễn viên Bao Thanh Thiên - được các thương hiệu săn đón nhờ có gu thẩm mỹ. Khải Linh vừa học Đại học Hong Kong khoa Kiến trúc, vừa làm người mẫu. Cô thường cùng Cung Từ Ân (phải) tham gia sự kiện giải trí. Cung Từ Ân 57 tuổi, đóng nhiều phim như Bao Thanh Thiên 1993,Tuyết sơn phi hồ 1991, Bồ công anh, Tuyết hoa thần kiếm, Năm ấy hoa nở trăng vừa tròn... Appledaily nhận xét Lâm Khải Linh thừa hưởng nhan sắc từ mẹ. Cung Từ Ân ly hôn năm 2019, sống cùng hai con ở Hong Kong. Nữ diễn viên dẫn dắt con gái vào làng giải trí. Trên tạp chí Jessica hồi tháng 5, Khải Linh nói may mắn vì có mẹ kèm cặp, chia sẻ kinh nghiệm trong công việc.Người đẹp được nhiều thương hiệu quần áo, mỹ phẩm, trang sức mời hợp tác, liên tục lên trang bìa các tạp chí ở Hong Kong như Elle, Cosmopolitan..."
                },
                new News()
                {
                    Id = 6,
                    CategoryId = 2,
                    Title = "Braithwaite và cuốn sổ 'Giấc mơ'",
                    Description =
                        "Vô danh trước khi đến Barca hồi tháng 2, nhưng Martin Braithwaite đang tận hưởng từng khoảnh khắc của việc đứng trong hàng ngũ CLB lớn bậc nhất thế giới. ",
                    CoverImage = "/images/martin-braithwaite-v-marcelo-1-3398-2066-1592116190.jpg",
                    SubDescription= "Người duy nhất tin Braithwaite sẽ chơi bóng ở trận Kinh điển (El Clasico) chính là... Braithwaite. Và điều đó đã được kiểm chứng. Hôm 1/3, tiền đạo người Đan Mạch giẫm lên mặt cỏ Bernabeu trong áo đấu số 19 của Barca, để chơi cùng Lionel Messi, Sergio Ramos, Toni Kroos, Gerard Pique, Karim Benzema... – những biểu tượng của El Clasico.Vào sân thay người ở phút 21, Braithwaite có thể là một biến số bất đắc dĩ. Anh không làm được điều gì để đảo ngược thế trận mà Barca thua 0-2. Nhưng thất bại đó không làm hỏng kế hoạch mà tiền đạo này đã vạch ra cả 10 năm trước: được tham dự trận đấu trong mơ này.Thật tuyệt vời! Tôi đã nói với chính mình hãy giữ tinh thần thoải mái nếu được đá trận này. Vì vậy, khi vào sân, tôi đã thi đấu như thể đó là một trận bình thường. Nhưng sau đó, tôi rơi vào cảm xúc ngập tràn. ‘Này tôi ơi, mày đã được đá trận El Clasico rồi đấy’, Braithwaite kể The Athletic. Đó là một giấc mơ mà tôi ấp ủ trong nhiều năm qua. Thật sự. Tôi đã suy nghĩ về nó, viết vào cuốn sổ ước mơ rằng tôi muốn chơi một trận El Clasico. Không chỉ có thế, tôi còn treo những tấm ảnh về trận đấu này trong nhà. Vâng, bây giờ tôi đã ở đó, đã có một trải nghiệm tuyệt vời. Nó không diễn ra như tôi muốn, nhưng chúng tôi sẽ báo thù.Thậm chí, một vài tuần trước khi xuất hiện tại Bernabeu, có vẻ như Braithwaite sẽ không có mảy may 0 % cơ hội hiện thực hoá giấc mơ. Khi đó, tiền đạo người Đan Mạch đang chơi cho Leganes, đội bóng đang đua tranh trụ hạng. Khi đó, thị trường chuyển nhượng mùa Đông đã đóng cửa. Chấm hết cho mọi giả tưởng điên rồ.Nhưng bão chấn thương đã cuốn bay hàng công Barca. Phép màu ngoại lệ xuất hiện. Barca được cấp quyền bổ sung cầu thủ để thay thế cho thương binh gia truyền Ousmane Dembele. Quyết định đó của La Liga đã mở ra cơ hội cho Braithwaite thực hiện bước tiến lớn nhất trong sự nghiệp, giúp anh có cơ hội hoàn thành giấc mơ được đặt ra trước khi có sự nghiệp cầu thủ chuyên nghiệp.Khi còn trẻ, tôi chỉ muốn được chơi cho đội bóng đá địa phương, ví dụ như CLB Esbjerg ở giải VĐQG Đan Mạch. Sau đó là một giấc mơ được lên tuyển. Khi tôi trưởng thành, những giấc mơ tiếp tục phát triển. Tôi mơ ước được chơi ở La Liga, và cho các CLB nhất thế giới. Tôi bắt đầu ghi sổ mơ khi 18 tuổi và tôi thực sự yêu thích việc này. Cho đến ngày nay, tôi vẫn đang viết sổ mơ.Các chương đầu của thiên cổ tích Braithwaite gồm: một Cup Quốc gia với Esbjerg mùa 2012 - 2013, sau đó là bốn mùa giải với Toulouse ở Ligue 1 và vụ chuyển nhượng trị giá 11 triệu USD sang Middlesbrough năm 2017. Tôi lớn lên như chỉ như một thảo dân, một cầu thủ phải trải qua nhiều phong cách bóng đá khác nhau, nhiều HLV khác nhau, và mỗi lần đổi thay, tôi lại học được một điều mới lạ. Tôi có rất nhiều kỷ niệm đẹp ở Pháp và ở Anh. Tôi và gia đình có một cuộc sống tươi đẹp tại vùng Teesside - Middlesbrough, với bóng đá và CĐV tuyệt vời.Sau khi chơi cả bốn trận của Đan Mạch tại World Cup 2018 ở Nga, Leganes đã liên lạc anh. Không muốn Middlesbrough mất một hảo thủ, nhưng HLV Tony Pulis không cản được quyết tâm đến La Liga của Braithwaite. Cuối cùng, anh sang Tây Ban Nha theo hợp đồng cho mượn vào tháng 1 / 2019.Tôi từng xem La Liga khi trẻ, nhưng không biết gì về Leganes. Đây là một CLB tí hon, nhiều năm đá ở các hạng dưới. Tôi đã nghiên cứu các cầu thủ,  tham vọng của đội bóng và mọi thứ, để hiểu mình phải làm gì để thích nghi. Tôi thấy đó là sự kết hợp tốt giữa phong cách chơi bóng của tôi với các đồng đội mới. Nhìn lại, tôi thấy đó là quyết định đúng đắn. Tôi đã có một số khoảnh khắc tuyệt vời, gặp rất nhiều người tốt, và đó là một CLB gia đình. Với Leganes, tôi chỉ biết nói những lời tụng ca, anh nói về đội bóng Tây Ban Nha đầu tiên trong sự nghiệp.Braithwaite rất nhanh tạo ấn tượng với La Liga.Hai tuần sau khi gia nhập Leganes, anh đã ghi bàn trong trận gặp Real ở Cup Nhà Vua Tây Ban Nha.Vài ngày sau, anh có bàn thắng ở La Liga đầu tiên ngay trên sân Camp Nou của Barca.Ở một đội bóng đang vật lộn tránh xa nhóm cầm đèn đỏ như Leganes, những bàn thắng của Braithwaite quý như vàng. Một bàn nữa ở phút 89 trên sân nhà trước Valencia hồi tháng Hai giúp Leganes có được một điểm quan trọng. Một bàn khác và hai pha kiến tạo của anh đem về chiến thắng 3 - 0 trước đội chủ nhà Sevilla, qua đó, đẩy Leganes lên vị trí an toàn.Khi được hỏi rằng, đâu là trận đấu hay nhất của mình ở Legane, Braithwaite hào hứng trả lời ngay trước khi nghe trọn câu hỏi: Ồ tất nhiên, khi tôi đi đến đâu, tôi luôn có ý định sẽ làm gì đó. Vì vậy, khi đến La Liga, có thể Leganes không phải là điểm đến trong mơ, nhưng họ đã cho tôi thấy tình yêu dạt dào cho mình và tôi sẽ phải có nghĩa vụ đáp đền tình yêu đó bằng bàn thắng và điểm số. Tất nhiên, trong sâu thẳm tâm hồn, tôi có tham vọng cao. Tôi muốn khoác áo các CLB lớn hơn tại La Liga. Tôi muốn nói với họ rằng, tôi thừa khả năng đá cho họ. Vì vậy, khi đương đầu với các đối thủ này, tôi có thêm 200% động lực để phô diễn thông điệp ngầm: Này các ông, tôi không phải dạng vừa đâu.Từ hợp đồng mượn, Leganes nhanh chóng mua đứt Braithwaite với giá gần sáu triệu USD.Trên sân tập và trong thi đấu đấu, anh hoàn toàn tập trung vào việc giúp Leganes đua tranh trụ hạng. Nhưng Braithwaite vẫn tích cực làm việc với kế hoạch của anh, bao gồm xem các trận đấu của Barca, để tìm ra cách thi đấu phù hợp nhất với đội bóng này. Khi xem các trận đấu, tôi luôn cố gắng học hỏi. Tôi thích xem các đội chơi hay nhất, và sau đó tôi thích tưởng tượng mình sẽ chơi như thế nào trong đội này. Tất nhiên, tôi có thể thấy mình ở Barca.Vì vậy, trong khi nhiều người ngạc nhiên khi Barca trả 20 triệu USD để mua đứt tiền đạo này vào tháng Hai, Braithwaite đã dành cả thập kỷ trước để chuẩn bị sẵn sàng. Dù vậy, tự bản thân anh cũng thấy được kéo vọt lên về đẳng cấp từ khi bắt đầu tập luyện cùng các đồng đội mới."
                },
            };

            try
            {
                context.News.AddOrUpdate(ns);
                context.SaveChanges();

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
    }
}
