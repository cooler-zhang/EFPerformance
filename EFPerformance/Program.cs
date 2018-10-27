using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            //CreateCategory();
            //CreateProduct();
            DeleteProduct();
        }

        private static void CreateProduct()
        {
            using (TestDbContext ctx = new TestDbContext())
            {
                DateTime start = DateTime.Now;
                for (int i = 0; i < 1000; i++)
                {
                    string model = "Model" + i;
                    string name = "测试EF循环添加的性能" + i;
                    string note = @"在这个测试中，通过比较针对 Navision 模型的跟踪和无跟踪查询，我们探讨填充 ObjectStateManager 的成本。有关 Navision 模型的说明以及执行的查询类型，请参见附录。在这个测试中，我们循环访问查询列表，对每个查询执行一次。我们进行了两种不同的测试，一次使用 NoTracking 查询，一次使用“AppendOnly”的默认合并选项。每种测试都进行 3 遍，取测试结果的平均值。在这些测试之间，我们清除了 SQL Server 上的查询缓存，并且通过运行以下命令缩小了 tempdb：" + i;
                    Product product = ctx.Products.Include("Category").Where(a => a.Model.Equals(model)).FirstOrDefault();
                    if (product == null)
                    {
                        product = new Product();
                        product.Model = model;
                    }
                    //int categoryId = product.Category.Id;
                    product.Name = name + "    耗时:" + (DateTime.Now - start).TotalMilliseconds;
                    product.Note = note;
                    if (product.Id == 0)
                    {
                        ctx.Products.Add(product);
                    }
                    Console.WriteLine(string.Format("产品第{0}个,累计耗时{1}ms", i, (DateTime.Now - start).TotalMilliseconds));
                    ctx.SaveChanges();
                }
                Console.ReadLine();
            }
        }

        public static void DeleteProduct()
        {
            using (TestDbContext ctx = new TestDbContext())
            {

                var category = ctx.Categories.Where(s => s.Id == 2003).FirstOrDefault();
                var product = ctx.Products.FirstOrDefault(s => s.Id == 3);
                category.Products.Remove(product);
                ctx.SaveChanges();
            }
        }


        private static void CreateCategory()
        {
            using (TestDbContext ctx = new TestDbContext())
            {
                DateTime start = DateTime.Now;
                for (int i = 0; i < 1000; i++)
                {
                    string name = "目录" + i;
                    Category category = ctx.Categories.Where(a => a.Name.Equals(name)).FirstOrDefault();
                    if (category == null)
                    {
                        category = new Category();
                    }
                    category.Name = name;
                    category.Note = "Hello Category Note";
                    if (category.Id == 0)
                    {
                        ctx.Categories.Add(category);
                    }
                    Console.WriteLine(string.Format("目录第{0}个,累计耗时{1}ms", i, (DateTime.Now - start).TotalMilliseconds));
                    ctx.SaveChanges();
                }
                Console.ReadLine();
            }
        }
    }
}
