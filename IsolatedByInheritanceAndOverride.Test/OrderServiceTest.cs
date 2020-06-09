using NUnit.Framework;

namespace IsolatedByInheritanceAndOverride.Test
{
    /// <summary>
    ///     OrderServiceTest 的摘要描述
    /// </summary>
    [TestFixture]
    public class OrderServiceTest
    {
        [Test]
        public void Test_SyncBookOrders_3_Orders_Only_2_book_order()
        {
            // hard to isolate dependency to unit test

            //var target = new OrderService();
            //target.SyncBookOrders();
        }
    }
}