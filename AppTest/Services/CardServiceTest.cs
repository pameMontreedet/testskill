using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using AppLib.Modules.Card;
using AppLib.Modules.Log;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;

namespace AppTest.Services.Test {
    public class CardServiceTest {
        [Fact]
        public void TestGetCardByCardId__ExpectCard()
        {
            Mock<ICardStorage> mockCardType1Storage = new Mock<ICardStorage>();
            mockCardType1Storage.Setup(x => x.ByCardId(5))
                .Returns(new CardModel()
                {
                    Id = 5,
                    TypeId = 1,
                    Name = "OneWay",
                    CardValue = 50,
                    StationStartcode = "CEN",
                    StationTerminatecode = "N8",
                    CreatedTime = DateTime.Parse("2018-10-19 06:34:04.7396870"),
                });
            CardService serviceGetCardType1 = new CardService(mockCardType1Storage.Object);
            var getCardType1Preview = serviceGetCardType1.GetCardByCardId(5);
            Assert.Equal(5, getCardType1Preview.Id);
            Assert.Equal(1, getCardType1Preview.TypeId);
            Assert.Equal("OneWay", getCardType1Preview.Name);
            Assert.Equal(50, getCardType1Preview.CardValue);
            Assert.Equal("CEN", getCardType1Preview.StationStartcode);
            Assert.Equal("N8", getCardType1Preview.StationTerminatecode);
            Assert.Equal(DateTime.Parse("2018-10-19 06:34:04.7396870"), getCardType1Preview.CreatedTime);
            mockCardType1Storage.Verify(x => x.ByCardId(5), Times.Once);


            Mock<ICardStorage> mockCardType2Storage = new Mock<ICardStorage>();
            mockCardType2Storage.Setup(x => x.ByCardId(6))
                .Returns(new CardModel()
                {
                    Id = 6,
                    TypeId = 2,
                    Name = "OneDayUnlimited",
                    CreatedTime = DateTime.Parse("2018-10-19 06:34:04.9464940")
                });
            CardService serviceGetCardType2 = new CardService(mockCardType2Storage.Object);
            var getCardType2Preview = serviceGetCardType2.GetCardByCardId(6);
            Assert.Equal(6, getCardType2Preview.Id);
            Assert.Equal(2, getCardType2Preview.TypeId);
            Assert.Equal("OneDayUnlimited", getCardType2Preview.Name);
            Assert.Equal(DateTime.Parse("2018-10-19 06:34:04.9464940"), getCardType2Preview.CreatedTime);
            mockCardType2Storage.Verify(x => x.ByCardId(6), Times.Once);

            Mock<ICardStorage> mockCardType3Storage = new Mock<ICardStorage>();
            mockCardType3Storage.Setup(x => x.ByCardId(7))
                .Returns(new CardModel()
                {
                    Id = 7,
                    TypeId = 3,
                    Name = "RabbitA",
                    CardRoundBalance = 0,
                    CreatedTime = DateTime.Parse("2018-10-19 06:34:05.1733420")
                });
            CardService serviceGetCardType3 = new CardService(mockCardType3Storage.Object);
            var getCardType3Preview = serviceGetCardType3.GetCardByCardId(7);
            Assert.Equal(7, getCardType3Preview.Id);
            Assert.Equal(3, getCardType3Preview.TypeId);
            Assert.Equal("RabbitA", getCardType3Preview.Name);
            Assert.Equal(0, getCardType3Preview.CardRoundBalance);
            Assert.Equal(DateTime.Parse("2018-10-19 06:34:05.1733420"), getCardType3Preview.CreatedTime);
            mockCardType3Storage.Verify(x => x.ByCardId(7), Times.Once);

            Mock<ICardStorage> mockCardType4Storage = new Mock<ICardStorage>();
            mockCardType4Storage.Setup(x => x.ByCardId(8))
                .Returns(new CardModel()
                {
                    Id = 8,
                    TypeId = 4,
                    Name = "RabbitB",
                    CardValueBalance = 0,
                    CreatedTime = DateTime.Parse("2018-10-19 06:34:05.2631470")
                });
            CardService serviceGetCardType4 = new CardService(mockCardType4Storage.Object);
            var getCardType4Preview = serviceGetCardType4.GetCardByCardId(8);
            Assert.Equal(8, getCardType4Preview.Id);
            Assert.Equal(4, getCardType4Preview.TypeId);
            Assert.Equal("RabbitB", getCardType4Preview.Name);
            Assert.Equal(0, getCardType4Preview.CardValueBalance);
            Assert.Equal(DateTime.Parse("2018-10-19 06:34:05.2631470"), getCardType4Preview.CreatedTime);
            mockCardType4Storage.Verify(x => x.ByCardId(8), Times.Once);
        }

        [Fact]
        public void TestAddRoundToCard()
        {
            CardModel objCard = new CardModel();
            objCard.Id = 3;
            objCard.TypeId = 3;
            objCard.CardRoundBalance = 0;
            objCard.Name = "RabbitA";

            Log objAddRound = new Log();
            objAddRound.AddRound = 15;

            Mock<ICardStorage> mockCardStorage = new Mock<ICardStorage>();
            mockCardStorage.Setup(x => x.AddRound(objCard, objAddRound))
                .Returns(new CardModel()
                {
                    Id = 3,
                    TypeId = 3,
                    Name = "RabbitA",
                    CardRoundBalance = 30,
                    CreatedTime = DateTime.Parse("2018-10-19 06:34:00.1251290"),
                    LastTimeAddRound = DateTime.Parse("2018-10-19 07:07:08.1991610")
                });
            CardService serviceGetCardAfterAddRound = new CardService(mockCardStorage.Object);
            var getCardPreview = serviceGetCardAfterAddRound.AddRoundToCard(objCard, objAddRound);
            Assert.Equal(3, getCardPreview.Id);
            Assert.Equal(3, getCardPreview.TypeId);
            Assert.Equal("RabbitA", getCardPreview.Name);
            Assert.Equal(30, getCardPreview.CardRoundBalance);
            Assert.Equal(DateTime.Parse("2018-10-19 06:34:00.1251290"), getCardPreview.CreatedTime);
            Assert.Equal(DateTime.Parse("2018-10-19 07:07:08.1991610"), getCardPreview.LastTimeAddRound);
            mockCardStorage.Verify(x => x.AddRound(objCard, objAddRound), Times.Once);    
        }

        [Fact]
        public void TestAddMoneyToCard()
        {
            CardModel objCard = new CardModel();
            objCard.Id = 4;
            objCard.TypeId = 4;
            objCard.CardValueBalance = 0;
            objCard.Name = "RabbitB";

            Log objAddMoney = new Log();
            objAddMoney.AddMoney = 100;

            Mock<ICardStorage> mockCardStorage = new Mock<ICardStorage>();
            mockCardStorage.Setup(x => x.AddMoney(objCard, objAddMoney))
                .Returns(new CardModel()
                {
                    Id = 4,
                    TypeId = 4,
                    Name = "RabbitB",
                    CardValueBalance = 100,
                    CreatedTime = DateTime.Parse("2018-10-19 06:34:00.1496640")
                });
            CardService serviceGetCardAfterAddMony = new CardService(mockCardStorage.Object);
            var getCardPreview = serviceGetCardAfterAddMony.AddMoneyToCard(objCard, objAddMoney);
            Assert.Equal(4, getCardPreview.Id);
            Assert.Equal(4, getCardPreview.TypeId);
            Assert.Equal("RabbitB", getCardPreview.Name);
            Assert.Equal(100, getCardPreview.CardValueBalance);
            Assert.Equal(DateTime.Parse("2018-10-19 06:34:00.1496640"), getCardPreview.CreatedTime);
           
            mockCardStorage.Verify(x => x.AddMoney(objCard, objAddMoney), Times.Once);    
        }

        [Fact]
        public void TestIntegrationGetCardById(){
            // HttpWebResponse response = null;
            // var url = "https://localhost:5001/api/Card/GetCard/4";
            // HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            // request.Method = "GET";
            // response = (HttpWebResponse)request.GetResponse();
            // Assert.True(response.IsSuccessStatusCode);
            // Assert.NotNull(response.Content);
        }
    }
}