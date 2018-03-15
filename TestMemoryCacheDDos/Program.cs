using ETMall.B2C.DataService.Cache.Categories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace TestMemoryCacheDDos
{
    class Program
    {
        static void Main()
        {
            //先清除 cache 中資料
            MemoryCache _cache = MemoryCache.Default;
            _cache.Remove("Categories_Dictionary");
            IAllCategoryDictionaryCache cache = new AllCategroyDictionayCache();

            //起三個 thread
            var tasks = new List<Task>();
            try
            {
                for (var i = 0; i < 3; i++)
                {
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        var data = cache.Get();
                        Console.WriteLine("Data:" + data.Count() + " Thread:" + Thread.CurrentThread.ManagedThreadId);//JsonConvert.SerializeObject(data)
                }));
                }
            }catch(Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            tasks.ForEach(t => t.Wait());
            Console.WriteLine("Done");
        }
    }
}
