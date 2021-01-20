using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;

namespace IsolatedByInheritanceAndOverride.Test
{
    /// <summary>
    ///     OrderServiceTest 的摘要描述
    /// </summary>
    [TestFixture]
    public class OrderServiceTest
    {
        private OrderServiceForTest _target;
        private IBookDao _bookDao;

        [SetUp]
        public void Setup()
        {
            _target = new OrderServiceForTest();
            _bookDao = Substitute.For<IBookDao>();
            _target.SetBookDao(_bookDao);
        }

        [Test]
        public void Test_SyncBookOrders_3_Orders_Only_2_book_order()
        {
            GivenOrders(
                new Order {Type = "Book"},
                new Order {Type = "CD"},
                new Order {Type = "Book"}
            );

            _target.SyncBookOrders();

            ShouldInsert(2, "Book");
            ShouldNotInsert("CD");
        }

        private void ShouldNotInsert(string type)
        {
            _bookDao.DidNotReceive().Insert(Arg.Is<Order>(order => order.Type == type));
        }

        private void ShouldInsert(int times, string type)
        {
            _bookDao.Received(times).Insert(Arg.Is<Order>(order => order.Type == type));
        }

        private void GivenOrders(params Order[] targetOrders)
        {
            _target.Orders = targetOrders.ToList();
        }

        class OrderServiceForTest : OrderService
        {
            private List<Order> _orders;
            private IBookDao _bookDao;

            public List<Order> Orders
            {
                set => _orders = value;
            }

            public BookDao BookDao
            {
                set => _bookDao = value;
            }

            protected override List<Order> GetOrders()
            {
                return _orders;
            }

            protected override IBookDao GetBookDao()
            {
                return _bookDao;
            }

            internal void SetBookDao(IBookDao bookDao)
            {
                _bookDao = bookDao;
            }
        }
    }
}