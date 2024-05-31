using Microsoft.AspNetCore.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetsJo.Entities;
using ProjetsJo.BLL.Services;
using ProjetsJo.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingService = ProjetsJo.BLL.Services.TicketingService;

namespace ProjetsJo.BLL.Services.Tests
{
    [TestClass()]
    public class TicketingServiceTests
    {
        private TicketingService _service { get; set; }
        private ITicketingData _data { get; set; }
        private Guid accountKey { get; set; } = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e");
        private Dictionary<Offer, int> _cart { get; set; } = new();
        private int quantity = 3;

        [TestInitialize]
        public void TestInitialize()
        {
            _service = new(_data);
            Offer offertoAdd = new Offer();
            offertoAdd.Id = 1;
            offertoAdd.Name = "TestOffer";
            offertoAdd.TicketNumber = 2;
            offertoAdd.Price = 35m;
            offertoAdd.State = true;
            _cart.Add(offertoAdd, quantity);
        }

        [TestMethod()]
        public async Task MockPaymentApiTest()
        {
            DateTime dateTimeStart = DateTime.Now;
            await _service.MockPaymentApi();
            DateTime dateTimeEnd = DateTime.Now;
            bool intervalTime = (dateTimeEnd - dateTimeStart).Seconds >= 8;
            Assert.IsTrue(intervalTime);
        }

        [TestMethod()]
        public void GenerateQrCodeTest()
        {
            Dictionary<Ticket, int> tickets = new();
            tickets = _service.GenerateQrCode(accountKey, _cart);
            Assert.IsNotNull(tickets);
            Assert.IsInstanceOfType(tickets, typeof(Dictionary<Ticket, int>));

            foreach (Ticket ticket in tickets.Keys.ToList())
            {
                Assert.IsInstanceOfType(ticket, typeof(Ticket));
                Assert.IsNotNull(tickets[ticket]);
                Assert.IsNotNull(ticket.Id);
                Assert.IsInstanceOfType(ticket.Id, typeof(int));
                Assert.IsNotNull(ticket.Qrcode);
                Assert.IsInstanceOfType(ticket.Qrcode, typeof(string));
                Assert.IsNotNull(ticket.Date);
                Assert.IsInstanceOfType(ticket.Date, typeof(DateTime));
            }
            int ticketAmount = 0;
            foreach (Offer offer in _cart.Keys.ToList())
                ticketAmount += _cart[offer] * offer.TicketNumber;
            bool result = tickets.Keys.ToList().Count - ticketAmount == 0;
            Assert.IsTrue(result);
        }
    }
}