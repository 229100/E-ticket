using eTicket.Data.Enums;
using eTicket.Data.Static;
using eTicket.Models;
using Microsoft.AspNetCore.Identity;

namespace eTicket.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                //Cinema
                if (!context.Cinemas.Any())
                {
                    context.Cinemas.AddRange(new List<Cinema>()
                    {
                        new Cinema()
                        {
                            Name = "Концертный зал Государственной академической филармонии им Е.Рахмадиева",
                            Logo = "https://avatars.mds.yandex.net/i?id=a655a0fa96be83c10fd984861b5fef8744e03673-4284582-images-thumbs&n=13",
                            Description = "Один из самых красивых концертных залов Астаны, где проводиться огромное количество масштабных концертов."
                        },
                        new Cinema()
                        {
                            Name = "Астана Опера",
                            Logo = "https://avatars.mds.yandex.net/i?id=946e7aeccb2ffa5d901754c11c128b98362fe796-10640295-images-thumbs&n=13",
                            Description = "Государственный театр оперы и балета «Астана Опера» был открыт в 2013 году по инициативе Первого Президента Республики Казахстан Нурсултана Назарбаева. «Астана Опера» – крупнейший театр Центральной Азии — построен с учётом лучших классических традиций мирового зодчества, при этом в архитектуре подчеркнут казахский национальный колорит; его техническое оснащение отвечает мировым стандартам."
                        },
                        new Cinema()
                        {
                            Name = "Дворец мира и согласия",
                            Logo = "https://avatars.mds.yandex.net/i?id=a4cbd8f6f3653c4bd59d775f1cae5d1881901fd3-11844875-images-thumbs&n=13",
                            Description = "Оперный зал – гордость Дворца Мира и Согласия. Уникальный по своей архитектуре он отвечает всем требованиям мирового стандарта. Зал оборудован по последнему слову техники в области звукового обеспечения, что дает возможность организовать самые сложные интерпретации самых необычных постановок. "
                        },
                        new Cinema()
                        {
                            Name = "Дворец Жастар",
                            Logo = "https://avatars.mds.yandex.net/i?id=1e9059b9a98584762d6d9f486e99ab2aaa76ac35-9654453-images-thumbs&n=13",
                            Description = "Один из старейших дворцов города, в котором до сих пор проводят разного рода мероприятия."
                        },
                        new Cinema()
                        {
                            Name = "Летняя сцена Expo",
                            Logo = "https://avatars.mds.yandex.net/i?id=2a16c5aa76747c87d2f70c74ee11ba20-5234314-images-thumbs&n=13",
                            Description = "Летняя сцена Expo пользуется большой популярностью из-за своего местоположения, а также открытой сцены."
                        },
                    });
                    context.SaveChanges();
                }
                //Actors
                if (!context.Actors.Any())
                {
                    context.Actors.AddRange(new List<Actor>()
                    {
                        new Actor()
                        {
                            FullName = "V S X V Prince",
                            Bio = "Казахский рэпер, хип-хоп исполнитель и автор песен.",
                            ProfilePictureURL = "https://avatars.mds.yandex.net/get-entity_search/2338017/727545093/SUx182_2x"

                        },
                        new Actor()
                        {
                            FullName = "Светлана Касьян",
                            Bio = "Российская оперная певица",
                            ProfilePictureURL = "https://avatars.mds.yandex.net/get-entity_search/1670941/793368012/SUx182_2x"
                        },
                        new Actor()
                        {
                            FullName = "Оразбаев Сабит",
                            Bio = "Советский и казахский актёр, театральный педагог, профессор.",
                            ProfilePictureURL = "https://avatars.mds.yandex.net/get-entity_search/2223725/473772995/S122x183_2x"
                        },
                        new Actor()
                        {
                            FullName = "Мышбаева Нукетай",
                            Bio = "Советская и казахская актриса, певица (меццо-сопрано).",
                            ProfilePictureURL = "https://avatars.mds.yandex.net/i?id=23eb1b4f11182f167f124cc06c00c240d3f969d3-10700817-images-thumbs&n=13"
                        },
                        new Actor()
                        {
                            FullName = "Мадина Рахимбердиева",
                            Bio = "Профессиональная танцовщица и хореограф",
                            ProfilePictureURL = "https://avatars.mds.yandex.net/i?id=2a974caf28af1536e1950a8d47a2ef04337a0d5f-10641531-images-thumbs&n=13"
                        }
                    });
                    context.SaveChanges();
                }
                //Producers
                if (!context.Producers.Any())
                {
                    context.Producers.AddRange(new List<Producer>()
                    {
                        new Producer()
                        {
                            FullName = "Artishock",
                            Bio = "Один из крупнейших и наиболее известных организаторов культурных событий в Казахстане.",
                            ProfilePictureURL = "https://avatars.mds.yandex.net/i?id=1cd91757187d0617cafbeedc6a6d106a39a599dd-10878189-images-thumbs&n=13"

                        },
                        new Producer()
                        {
                            FullName = "Agama Events Group",
                            Bio = "Компания, занимающаяся организацией мероприятий и конференций.",
                            ProfilePictureURL = "https://avatars.mds.yandex.net/i?id=5c432bdd54047b46297ffe4646d277d63eae11ea-5193708-images-thumbs&n=13"
                        },
                        new Producer()
                        {
                            FullName = "Event Time Group",
                            Bio = "Компания, занимающаяся организацией мероприятий и конференций.",
                            ProfilePictureURL = "https://avatars.mds.yandex.net/i?id=068db2d929f5109419d23e4a9e974351_sr-5235083-images-thumbs&n=13"
                        },
                        new Producer()
                        {
                            FullName = "Dostar Events",
                            Bio = "Специализируется на организации культурных и искусственных событий в Казахстане.",
                            ProfilePictureURL = "https://avatars.mds.yandex.net/i?id=5a4bb0faea925f0c829fdd8b547d9f20da002bc3-11509297-images-thumbs&n=13"
                        },
                        new Producer()
                        {
                            FullName = "Kazakh Concert",
                            Bio = "Организатор концертов и культурных мероприятий, пользующийся популярностью и признанием как в Казахстане, так и за его пределами.",
                            ProfilePictureURL = "https://avatars.mds.yandex.net/i?id=7220bcd7fadad62a95b8fe0f66f3cb94632c4634-9706559-images-thumbs&n=13"
                        }
                    });
                    context.SaveChanges();
                }
                //Movies
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>()
                    {
                        new Movie()
                        {
                            Name = "Ох, уж эти девушки",
                            Description = "Захватывающий комедийный спектакль, который расскажет зрителям о веселых, искрометных приключениях главных героинь.",
                            Price = 39.50,
                            ImageURL = "https://avatars.mds.yandex.net/i?id=3e1acfeabd2fa261b2770ae77f6d0b02fd89ed56-8497242-images-thumbs&n=13",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(10),
                            CinemaId = 3,
                            ProducerId = 3,
                            MovieCategory = MovieCategory.Концерт
                        },
                        new Movie()
                        {
                            Name = "Волчонок под шапкой",
                            Description = "Захватывающий театральный спектакль, в основе которого лежит сказка, переплетающая мотивы приключений, дружбы и сказочного волшебства.",
                            Price = 29.50,
                            ImageURL = "https://avatars.mds.yandex.net/i?id=bb6b41b108e5fa238060dfb3126cc7147fe7422c-12545559-images-thumbs&n=13",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(3),
                            CinemaId = 1,
                            ProducerId = 1,
                            MovieCategory = MovieCategory.Концерт
                        },
                        new Movie()
                        {
                            Name = "Салем Казахстан",
                            Description = "Это шоу перенесет вас в мир, где традиции встречают современность, обещая вечер полный вдохновения и красоты.",
                            Price = 39.50,
                            ImageURL = "https://avatars.mds.yandex.net/i?id=1a20fda8d78f57b583adff60ff58506f410bc6cb-5351536-images-thumbs&n=13",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(7),
                            CinemaId = 4,
                            ProducerId = 4,
                            MovieCategory = MovieCategory.Концерт
                        },
                        new Movie()
                        {
                            Name = "Меня зовут Кожа",
                            Description = "это драматический спектакль, который затрагивает темы самопознания, индивидуальности и взаимоотношений.",
                            Price = 39.50,
                            ImageURL = "https://tele2.server-img.lfstrm.tv/image/aHR0cDovL3RlbGUyLnNlcnZlci1jbXMubGZzdHJtLnR2L2FyY2hpdmUtaW1nL3N0YXRpYy9tZWRpYS9mYS8zMy9mYTMzNWQyNDQ1ZTYwOGVlODBmMjFmZmIwN2YxODI3Mg==",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(-5),
                            CinemaId = 1,
                            ProducerId = 2,
                            MovieCategory = MovieCategory.Концерт
                        },
                        new Movie()
                        {
                            Name = "Концерт Prince",
                            Description = "Концерт одного из самых извсетных казахских рэпером современности.",
                            Price = 39.50,
                            ImageURL = "https://100biografiy.ru/wp-content/uploads/2023/02/1-30.jpeg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(-2),
                            CinemaId = 1,
                            ProducerId = 3,
                            MovieCategory = MovieCategory.Концерт
                        },
                        new Movie()
                        {
                            Name = "Музыкальный спектакль «Оттепель»",
                            Description = "Попытка проникнуться особенной атмосферой начала 60-х, воссоздать среду того далекого поколения молодежи.",
                            Price = 39.50,
                            ImageURL = "https://avatars.mds.yandex.net/i?id=f12be9bd2ff398e707c124aa40400492b12d160e-10805116-images-thumbs&n=13",
                            StartDate = DateTime.Now.AddDays(3),
                            EndDate = DateTime.Now.AddDays(20),
                            CinemaId = 1,
                            ProducerId = 5,
                            MovieCategory = MovieCategory.Концерт
                        }
                    });
                    context.SaveChanges();
                }
                //Actors & Movies
                if (!context.Actors_Movies.Any())
                {
                    context.Actors_Movies.AddRange(new List<Actor_Movie>()
                    {
                        new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 1
                        },
                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 1
                        },

                         new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 2
                        },
                         new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 2
                        },

                        new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 3
                        },
                        new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 3
                        },
                        new Actor_Movie()
                        {
                            ActorId = 5,
                            MovieId = 3
                        },


                        new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 4
                        },
                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 4
                        },
                        new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 4
                        },


                        new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 5
                        },
                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 5
                        },
                        new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 5
                        },
                        new Actor_Movie()
                        {
                            ActorId = 5,
                            MovieId = 5
                        },


                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 6
                        },
                        new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 6
                        },
                        new Actor_Movie()
                        {
                            ActorId = 5,
                            MovieId = 6
                        },
                    });
                    context.SaveChanges();
                }
            }

        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                }
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                }

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@etickets.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "ADMIN USER",
                        UserName = "admin_user",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                    };

                    await userManager.CreateAsync(newAdminUser, "eTicket@Admin?123");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (adminUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "APPLICATION USER",
                        UserName = "app_user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                    };

                    await userManager.CreateAsync(newAppUser, "eTicket@User?123");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }

            }
        }
    }
}
