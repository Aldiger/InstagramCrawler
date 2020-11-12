using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Crawler.Service;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async  Task Authentication()
        {
            var service = new Crawler.Service.Crawler();
            await service.Authenticate();


        }
    }
}
