using PawsAndTailsWebAPISwagger.Data;
using PawsAndTailsWebAPISwagger.Models;

namespace PawsAndTailsWebAPISwagger
{
    public static class Seed
    {
        public static void SeedDataContext(ApplicationDbContext context)
        {
            if(!context.Categories.Any())
            {
                var CategoryList = new List<Category>
                {
                    new Category {Name = "Dog"},
                    new Category {Name = "Puppy"},
                    new Category {Name = "Large Breed"},
                    new Category {Name = "Small Breed"},
                    new Category {Name = "Adult Dog"},
                    new Category {Name = "Mother Dog"},
                    new Category {Name = "Cat"},
                    new Category {Name = "Kitten"},
                    new Category {Name = "Adult Cat"},
                    new Category {Name = "Mother Cat"}
                };

                context.Categories.AddRange(CategoryList);
                context.SaveChanges();
            }


            if (!context.Products.Any())
            {
                var ProductList = new List<Product>
                {
                    new Product
                    {
                        Name = "Acana Large Breed Dry Puppy Food",
                        Description = "High Protein, Grain-Free, Easy to Digest Dog Food",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 117.58,
                        OurPrice = 110.11,
                        Rating = 4.97,
                        Stock = 400
                    },

                    new Product
                    {
                        Name = "Signature Grain Zero Adult Dry Cat Food",
                        Description = "Hypoallergenic Complete Nutrition for All Adult Cats",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 20.63,
                        OurPrice = 18.67,
                        Rating = 4.67,
                        Stock = 420
                    },
                    new Product
                    {
                        Name = "HUFT Dehydrated Anchovies Crunchies Cat Treats",
                        Description = "Tempting Smell and Irresistible Crunchy Texture",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 2.91,
                        OurPrice = 2.61,
                        Rating = 4,
                        Stock = 320
                    },
                    new Product
                    {
                        Name = "Cesar Premium Sasami Adult Wet Dog Food (Gourmet Meal)",
                        Description = "Low-Fat, Steamed, Irresistible, International Dog Food",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 11.49,
                        OurPrice = 10.31,
                        Rating = 5,
                        Stock = 560
                    },
                    new Product
                    {
                        Name = "Drools Chicken and Egg Adult Dog Food",
                        Description = "A Great Combination of ingredients, Vitamins, Minerals and Nutrition for Everything from Strong Bones to Optimal Digestion",
                        ImgUrl = $"{Guid.NewGuid()}.jpg",
                        OrigPrice = 17.99,
                        OurPrice = 15.32,
                        Rating = 4.89,
                        Stock = 630
                    },
                    new Product
                    {
                        Name = "Drools Focus Starter Super Premium Dry Dog Food",
                        Description = "Specially Designed for the Weaning Puppies, from 3 Weeks to 3 Months Who Require More Amount of Protein, Energy and Essential Nutrients",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 71.82,
                        OurPrice = 68.35,
                        Rating = 5,
                        Stock = 600
                    },
                    new Product
                    {
                        Name = "Farmina NnD Low Grain Starter Puppy Food",
                        Description = "Preservative-free, GMO-free, International Dog Food",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 89.65,
                        OurPrice = 83.27,
                        Rating = 4.95,
                        Stock = 252
                    },
                    new Product
                    {
                        Name = "Huft Saras Wholesome Food-Classic Chicken and Brown Rice",
                        Description = "Optimal Protein for Growth & Healthy Living.",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 1.19,
                        OurPrice = 0.99,
                        Rating = 5,
                        Stock = 300
                    },
                    new Product()
                    {
                        Name = "Arden Grange Dry Puppy Junior Food",
                        Description = "Hypoallergenic, GMO-free, Easy to Digest & Perfect for Fussy Eaters",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 24,
                        OurPrice = 23.22,
                        Rating = 5,
                        Stock = 230
                    },
                    new Product
                    {
                        Name = "Kennel Kitchen Lamb with Pumpkin Supreme Cuts in Gravy",
                        Description = "Contains High Levels of Boneless Lamb that Provide High Levels of Protein, Healthy Energy Levels and Easier Digestibility",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 0.84,
                        OurPrice = 0.8,
                        Rating = 5,
                        Stock = 330
                    },
                    new Product
                    {
                        Name = "Kitty Licks",
                        Description = "Rich in Tuna and is High in Fiber, Which Helps the Digestive System.",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 0.67,
                        OurPrice = 0.6,
                        Rating = 3.71,
                        Stock = 220
                    },
                    new Product
                    {
                        Name = "Kennel Kitchen Fish Chunks in Gravy",
                        Description = "Preservative-free, Delicious & Made from Real Ingredients",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 0.71,
                        OurPrice = 0.7,
                        Rating = 4.1,
                        Stock = 200
                    },
                    new Product
                    {
                        Name = "Loveabowl Cat Kibble Chicken",
                        Description = "Fortified with Taurine for Better Cardiovascular Health.",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 27.99,
                        OurPrice = 26.54,
                        Rating = 4.7,
                        Stock = 460
                    },
                    new Product
                    {
                        Name = "Loveabowl Grain Free Chicken with Atlantic Lobster",
                        Description = "The Grain-free Formulation Makes it Easy to Digest, and Reduces the Risk of an Upset Tummy.",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 16.79,
                        OurPrice = 14.32,
                        Rating = 4,
                        Stock = 350
                    },
                    new Product
                    {
                        Name = "Natures Hug Junior Growth Toy & Small Breed Vegan Dog Food",
                        Description = "Supports Bone Structure and Immunity",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 17.99,
                        OurPrice = 15.77,
                        Rating = 5,
                        Stock = 450
                    },
                    new Product
                    {
                        Name = "Natures Hug Adult Maintenance Indoor Hairball Vegan Food",
                        Description = "Maintains Weight, Prevents Hairball and Improves Digestion",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 5.91,
                        OurPrice = 5.01,
                        Rating = 4.02,
                        Stock = 360
                    },
                    new Product
                    {
                        Name = "Orijen Cat Food",
                        Description = "The First 5 Ingredients are Fresh or Raw Poultry and Fish Ingredients.",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 7.98,
                        OurPrice = 6.9,
                        Rating = 4,
                        Stock = 420
                    },
                    new Product()
                    {
                        Name = "Orijen Grain Free Large Breed Dry Puppy Food",
                        Description = "High Protein, Grain-free Dog Food with Fresh Ingredients",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 82.79,
                        OurPrice = 81.7,
                        Rating = 4.88,
                        Stock = 700
                    },
                    new Product
                    {
                        Name = "Pedigree Chicken & Vegetables Adult Dry Dog Food",
                        Description = "High-Quality, Wholesome, Balanced Dog Food",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 1.1,
                        OurPrice = 1.0,
                        Rating = 4.83,
                        Stock = 400
                    },
                    new Product
                    {
                        Name = "Pedigree Grilled Liver Chunks with Vegetables Gravy Adult Wet Dog Food",
                        Description = "Complete, Balanced, Tempting Dog Food",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 0.6,
                        OurPrice = 0.59,
                        Rating = 4.83,
                        Stock = 500
                    },
                    new Product
                    {
                        Name = "Pedigree Pro Expert Nutrition Active Adult Dry Dog Food",
                        Description = "High-Quality, Tasty, Easy to Consume Dog Food",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 47.73,
                        OurPrice = 46.03,
                        Rating = 5,
                        Stock = 200
                    },
                    new Product
                    {
                        Name = "Pedigree Pro Expert Nutrition Senior Adult Dog Food",
                        Description = "Professional Dog Food for Highly Active Large Breed Adult Dogs",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 13.09,
                        OurPrice = 12.31,
                        Rating = 5,
                        Stock = 330
                    },
                    new Product
                    {
                        Name = "Pedigree Small Breed Dry Puppy Food Lamb & Milk",
                        Description = "High-Quality, Tasty, Wholesome, Balanced Dog Food",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 9.07,
                        OurPrice = 8.67,
                        Rating = 5,
                        Stock = 478
                    },
                    new Product
                    {
                        Name = "Royal Canin Maxi Puppy Dry Dog Food",
                        Description = "High-Quality, Balanced, International Dog Food",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 9.35,
                        OurPrice = 8.78,
                        Rating = 4.9,
                        Stock = 470
                    },
                    new Product
                    {
                        Name = "Royal Canin Mother and Baby Cat Food",
                        Description = "Grain-free, Wholesome, Flavorful Cat Food",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 16.77,
                        OurPrice = 15.33,
                        Rating = 4.1,
                        Stock = 320
                    },
                    new Product
                    {
                        Name = "Royal Canin Kitten Food in Gravy",
                        Description = "Delicious, Nutritious, Healthy, International Cat Food",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 4.99,
                        OurPrice = 4.09,
                        Rating = 4.18,
                        Stock = 550
                    },
                    new Product
                    {
                        Name = "Schesir Chicken with Carrot Canned Wet Dog Food",
                        Description = "A Schesir Recipe Based on Real Chicken, Combined with a Topping of Sweet and Tasty Carrots, Served in Mouthwatering Jelly Format, for an Indulgent and Original Meal",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 2.99,
                        OurPrice = 2.67,
                        Rating = 5,
                        Stock = 230
                    },
                    new Product
                    {
                        Name = "Schesir Tuna with Seabass Adult Cat Wet Food",
                        Description = "Natural, Steamed, Preservative-Free, Hand-Processed & Cruelty-Free",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 1.55,
                        OurPrice = 1.44,
                        Rating = 4.5,
                        Stock = 800
                    },
                    new Product
                    {
                        Name = "Sheba Melty Maguro Seafood Cat Food",
                        Description = "High-Quality, Savourity & Delicious",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 4.99,
                        OurPrice = 3.99,
                        Rating = 4.63,
                        Stock = 200
                    },
                    new Product
                    {
                        Name = "Sheba Rich Premium Tuna Pumpkin & Carrot food for Cats",
                        Description = "Tender Delicious Tuna Pumpkin & Carrot in Gravy Flavor That Improves Cats Bowel Movements",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 4.32,
                        OurPrice = 4,
                        Rating = 4.35,
                        Stock = 302
                    },
                    new Product
                    {
                        Name = "Sheba Succulent Chicken Breast in Gravy",
                        Description = "Tender, High-Quality, Delicious, International Cat Food",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 8.99,
                        OurPrice = 8.67,
                        Rating = 5,
                        Stock = 250
                    },
                    new Product
                    {
                        Name = "Signature Grain Zero Adult Dry Dog Food",
                        Description = "Hypoallergenic, Complete Nutrition for All Adult Dogs",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 65.16,
                        OurPrice = 63.71,
                        Rating = 5,
                        Stock = 270
                    },
                    new Product
                    {
                        Name = "Signature Grain Zero Puppy Dry Food",
                        Description = "Hypoallergenic, Complete Nutrition for Puppies",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 70.59,
                        OurPrice = 68.25,
                        Rating = 1,
                        Stock = 330
                    },
                    new Product
                    {
                        Name = "Signature Grain Zero Starter Food for Mother and Puppy",
                        Description = "Hypoallergenic Starter Formula for Mother Dog and Puppy",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 78.19,
                        OurPrice = 72.53,
                        Rating = 5,
                        Stock = 350
                    },
                    new Product
                    {
                        Name = "Temptations Creamy Purrrr-ee Cat Treats",
                        Description = "Creamy, Flavorful & Mess-Free",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 0.45,
                        OurPrice = 0.39,
                        Rating = 2.99,
                        Stock = 890
                    },
                    new Product
                    {
                        Name = "Whiskas Chicken Adult Dry Cat Food",
                        Description = "High-Quality, Crunchy, Balanced & Delicious",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 27.32,
                        OurPrice = 24.23,
                        Rating = 4.99,
                        Stock = 850
                    },
                    new Product
                    {
                        Name = "Whiskas Chicken with Tuna and Carrot in Gravy Cat Food",
                        Description = "A New, Balanced, and Flavorful Cat Food. Made with Real Fish, Chicken, and Nutritious Veggies in a Tasty Gravy Sauce.",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 3.99,
                        OurPrice = 3.67,
                        Rating = 4.59,
                        Stock = 560
                    },
                    new Product
                    {
                        Name = "Whiskas Ocean Fish Adult Wet Cat Food",
                        Description = "Complete, Balanced, Easy to Eat & Digest",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 17.9,
                        OurPrice = 15.77,
                        Rating = 4.1,
                        Stock = 276
                    },
                    new Product
                    {
                        Name = "Canine Creek Starter Ultra-Premium Dry Dog Food",
                        Description = "Grain-free Advanced Pet Nutrition Formula",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 76.95,
                        OurPrice = 25.52,
                        Rating = 22.99,
                        Stock = 900
                    },
                    new Product
                    {
                        Name = "Kitty Yums Adult Dry Cat Food - Ocean Fish",
                        Description = "High Protein, Healthy & Balanced Meal",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 25.52,
                        OurPrice = 22.99,
                        Rating = 5,
                        Stock = 680
                    },
                    new Product
                    {
                        Name = "Arden Grange Grain Free Fresh Chicken & Potato Dry Kitten Food",
                        Description = "Contains Large Portions of Fresh Chicken That Provides an Easily Digestible Source of Protein",
                        ImgUrl = $"{Guid.NewGuid()}.webp",
                        OrigPrice = 7.56,
                        OurPrice = 7.04,
                        Rating = 4.92,
                        Stock = 750
                    }
                };
                       
                context.Products.AddRange(ProductList);
                context.SaveChanges();

                //Associate Products with Categories

                var ProductCategoryList = new List<ProductCategory>
                {
                    new ProductCategory{ ProductId = ProductList[0].ProductId, CategoryId = context.Categories.First(c => c.Name == "Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[0].ProductId, CategoryId = context.Categories.First(c => c.Name == "Puppy").CategoryId },
                    new ProductCategory{ ProductId = ProductList[0].ProductId, CategoryId = context.Categories.First(c => c.Name == "Large Breed").CategoryId },
                    new ProductCategory{ ProductId = ProductList[1].ProductId, CategoryId = context.Categories.First(c => c.Name == "Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[1].ProductId, CategoryId = context.Categories.First(c => c.Name == "Adult Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[2].ProductId, CategoryId = context.Categories.First(c => c.Name == "Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[3].ProductId, CategoryId = context.Categories.First(c => c.Name == "Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[3].ProductId, CategoryId = context.Categories.First(c => c.Name == "Adult Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[4].ProductId, CategoryId = context.Categories.First(c => c.Name == "Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[4].ProductId, CategoryId = context.Categories.First(c => c.Name == "Adult Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[5].ProductId, CategoryId = context.Categories.First(c => c.Name == "Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[6].ProductId, CategoryId = context.Categories.First(c => c.Name == "Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[6].ProductId, CategoryId = context.Categories.First(c => c.Name == "Puppy").CategoryId },
                    new ProductCategory{ ProductId = ProductList[7].ProductId, CategoryId = context.Categories.First(c => c.Name == "Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[7].ProductId, CategoryId = context.Categories.First(c => c.Name == "Puppy").CategoryId },
                    new ProductCategory{ ProductId = ProductList[8].ProductId, CategoryId = context.Categories.First(c => c.Name == "Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[8].ProductId, CategoryId = context.Categories.First(c => c.Name == "Puppy").CategoryId },
                    new ProductCategory{ ProductId = ProductList[9].ProductId, CategoryId = context.Categories.First(c => c.Name == "Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[9].ProductId, CategoryId = context.Categories.First(c => c.Name == "Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[10].ProductId, CategoryId = context.Categories.First(c => c.Name == "Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[11].ProductId, CategoryId = context.Categories.First(c => c.Name == "Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[11].ProductId, CategoryId = context.Categories.First(c => c.Name == "Kitten").CategoryId },
                    new ProductCategory{ ProductId = ProductList[12].ProductId, CategoryId = context.Categories.First(c => c.Name == "Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[13].ProductId, CategoryId = context.Categories.First(c => c.Name == "Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[14].ProductId, CategoryId = context.Categories.First(c => c.Name == "Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[14].ProductId, CategoryId = context.Categories.First(c => c.Name == "Small Breed").CategoryId },
                    new ProductCategory{ ProductId = ProductList[15].ProductId, CategoryId = context.Categories.First(c => c.Name == "Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[15].ProductId, CategoryId = context.Categories.First(c => c.Name == "Adult Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[16].ProductId, CategoryId = context.Categories.First(c => c.Name == "Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[17].ProductId, CategoryId = context.Categories.First(c => c.Name == "Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[17].ProductId, CategoryId = context.Categories.First(c => c.Name == "Puppy").CategoryId },
                    new ProductCategory{ ProductId = ProductList[17].ProductId, CategoryId = context.Categories.First(c => c.Name == "Large Breed").CategoryId },
                    new ProductCategory{ ProductId = ProductList[18].ProductId, CategoryId = context.Categories.First(c => c.Name == "Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[18].ProductId, CategoryId = context.Categories.First(c => c.Name == "Adult Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[19].ProductId, CategoryId = context.Categories.First(c => c.Name == "Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[19].ProductId, CategoryId = context.Categories.First(c => c.Name == "Adult Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[20].ProductId, CategoryId = context.Categories.First(c => c.Name == "Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[20].ProductId, CategoryId = context.Categories.First(c => c.Name == "Adult Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[20].ProductId, CategoryId = context.Categories.First(c => c.Name == "Large Breed").CategoryId },
                    new ProductCategory{ ProductId = ProductList[21].ProductId, CategoryId = context.Categories.First(c => c.Name == "Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[21].ProductId, CategoryId = context.Categories.First(c => c.Name == "Adult Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[22].ProductId, CategoryId = context.Categories.First(c => c.Name == "Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[22].ProductId, CategoryId = context.Categories.First(c => c.Name == "Puppy").CategoryId },
                    new ProductCategory{ ProductId = ProductList[22].ProductId, CategoryId = context.Categories.First(c => c.Name == "Small Breed").CategoryId },
                    new ProductCategory{ ProductId = ProductList[23].ProductId, CategoryId = context.Categories.First(c => c.Name == "Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[23].ProductId, CategoryId = context.Categories.First(c => c.Name == "Puppy").CategoryId },
                    new ProductCategory{ ProductId = ProductList[23].ProductId, CategoryId = context.Categories.First(c => c.Name == "Adult Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[24].ProductId, CategoryId = context.Categories.First(c => c.Name == "Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[24].ProductId, CategoryId = context.Categories.First(c => c.Name == "Mother Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[24].ProductId, CategoryId = context.Categories.First(c => c.Name == "Kitten").CategoryId },
                    new ProductCategory{ ProductId = ProductList[25].ProductId, CategoryId = context.Categories.First(c => c.Name == "Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[25].ProductId, CategoryId = context.Categories.First(c => c.Name == "Kitten").CategoryId },
                    new ProductCategory{ ProductId = ProductList[26].ProductId, CategoryId = context.Categories.First(c => c.Name == "Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[27].ProductId, CategoryId = context.Categories.First(c => c.Name == "Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[27].ProductId, CategoryId = context.Categories.First(c => c.Name == "Adult Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[28].ProductId, CategoryId = context.Categories.First(c => c.Name == "Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[29].ProductId, CategoryId = context.Categories.First(c => c.Name == "Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[29].ProductId, CategoryId = context.Categories.First(c => c.Name == "Adult Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[30].ProductId, CategoryId = context.Categories.First(c => c.Name == "Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[30].ProductId, CategoryId = context.Categories.First(c => c.Name == "Adult Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[31].ProductId, CategoryId = context.Categories.First(c => c.Name == "Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[31].ProductId, CategoryId = context.Categories.First(c => c.Name == "Adult Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[32].ProductId, CategoryId = context.Categories.First(c => c.Name == "Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[32].ProductId, CategoryId = context.Categories.First(c => c.Name == "Puppy").CategoryId },
                    new ProductCategory{ ProductId = ProductList[33].ProductId, CategoryId = context.Categories.First(c => c.Name == "Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[33].ProductId, CategoryId = context.Categories.First(c => c.Name == "Mother Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[33].ProductId, CategoryId = context.Categories.First(c => c.Name == "Puppy").CategoryId },
                    new ProductCategory{ ProductId = ProductList[34].ProductId, CategoryId = context.Categories.First(c => c.Name == "Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[35].ProductId, CategoryId = context.Categories.First(c => c.Name == "Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[35].ProductId, CategoryId = context.Categories.First(c => c.Name == "Adult Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[36].ProductId, CategoryId = context.Categories.First(c => c.Name == "Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[36].ProductId, CategoryId = context.Categories.First(c => c.Name == "Adult Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[37].ProductId, CategoryId = context.Categories.First(c => c.Name == "Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[37].ProductId, CategoryId = context.Categories.First(c => c.Name == "Adult Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[38].ProductId, CategoryId = context.Categories.First(c => c.Name == "Dog").CategoryId },
                    new ProductCategory{ ProductId = ProductList[39].ProductId, CategoryId = context.Categories.First(c => c.Name == "Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[39].ProductId, CategoryId = context.Categories.First(c => c.Name == "Adult Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[40].ProductId, CategoryId = context.Categories.First(c => c.Name == "Cat").CategoryId },
                    new ProductCategory{ ProductId = ProductList[40].ProductId, CategoryId = context.Categories.First(c => c.Name == "Kitten").CategoryId }
                };
                context.ProductCategories.AddRange(ProductCategoryList);
                context.SaveChanges();
            }
        }
    }
}
