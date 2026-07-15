using BikeShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BikeShop.Infrastructure.Persistence
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(BikeShopDbContext context)
        {
            if (!await context.Categories.AnyAsync()) {
                await context.Categories.AddRangeAsync(
                    new Category("Mountain Bikes"),
                    new Category("Road Bikes"),
                    new Category("City Bikes"));

                await context.SaveChangesAsync();
            }

            if (!await context.Products.AnyAsync()) {
                var categories = await context.Categories
                    .OrderBy(c => c.Id)
                    .ToListAsync();

                var products = new[]
                {
                    new Product("Горный велосипед Stinger Reload Evo 29\"(2024)", categories[0], 71100, "Reload Evo 29\" - одно из самых интересных предложений бренда Stinger для Cross-Country и XC. Байк отлично сочетает в себе стабильность, предсказуемость и интуитивность поведения, за счет чего станет отличным вариантом для тренировочного катания и входа в любительские соревновательные группы. Главная изюминка данной серии - рама, которая выполнена по всем актуальным требованиям и стандартам жесткости и надежности. В сочетании с не менее надежной трансмиссией, тормозами, кокпитом и вилсетом, байк получился очень сбитым и безотказным к любым мыслимым маршрутам и задачам.", 12  ),
                    new Product("Складной велосипед Stels Pilot 710 C Z010 (2025)", categories[1], 13120, "710 C Z010 - популярный городской велосипед от компании Stels. Велосипед построен по всем знакомой концепции советских велосипедов - комфортная, прямая посадка, складная рама, небольшие колеса и аксессуары для комфортного катания в городских условиях. Скоростная трансмиссия и ручные тормоза позволят комфортно ощущать себя на спусках и подъемах. Также у этой модели есть одно из самых главных преимуществ - стоимость, за счет которой она и является одной из самой популярной в России!", 2),
                    new Product("Гравийный велосипед Stark Gravel 700.2 D (2025)", categories[2], 61990, "Stark Gravel 700.2 D - один из самых интересных и конструктивно надежных гравийных байков в своем бюджете. Архитектура, геометрия и технологии постройки рамы выполнены по всем актуальным требованиям. Байк динамичен, маневрен, легко проходит подъемы, спуски, извилистые участки и серьезные препятствия в виде коряг и песчаных зон. Модификация 700.2 D подразумевает в себе среднее оснащение - система переключения скоростей любительского уровня, дисковые тормоза и 28 колеса.", 7),

                };

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
    }
}
