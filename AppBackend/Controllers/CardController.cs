using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppBackend.ViewModels;
using Mapster;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using AppLib.Modules.Card;
using AppLib.Modules.Log;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
//using System.Web.Script.Serialization;

namespace AppBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CardController : Controller
    {

        private ICardService _cardService;
        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        public IActionResult RegisterCard()
        {
            CardModel obj1 = new CardModel();
            obj1.TypeId = 1;
            obj1.Name = "OneWay";
            obj1.StationStartcode = "CEN";
            obj1.StationTerminatecode = "N8";

            CardModel obj2 = new CardModel();
            obj2.TypeId = 2;
            obj2.Name = "OneDayUnlimited";

            CardModel obj3 = new CardModel();
            obj3.TypeId = 3;
            obj3.CardRoundBalance = 0;
            obj3.Name = "RabbitA";

            CardModel obj4 = new CardModel();
            obj4.TypeId = 4;
            obj4.CardValueBalance = 0;
            obj4.Name = "RabbitB";

            var card = _cardService.SaveCard(obj1);
            _cardService.SaveCard(obj2);
            _cardService.SaveCard(obj3);
            _cardService.SaveCard(obj4);
            var view = card.Adapt<CardView>();
            return new JsonResult(new ApiResponse<CardView>(true, view));
        }

        [HttpGet("{id}")]
        public IActionResult GetCard(int id)
        {
            try
            {
                var card = _cardService.GetCardByCardId(id);
                if (card == null)
                    return new JsonResult(new ApiResponse<String>(true, "ไม่มีบัตรในรายการ"));
                var view = card.Adapt<CardView>();
                return new JsonResult(new ApiResponse<CardView>(true, view));
            }
            catch (Exception e)
            {

                return new JsonResult(new ApiResponse<String>(false, "False"));
            }
        }

        [HttpGet("{id}")]
        public IActionResult AddRound(int id)
        {
            try
            {
                Log obj1AddRound = new Log();
                obj1AddRound.AddRound = 15;
                var objCard = _cardService.GetCardByCardId(id);
                var card = _cardService.AddRoundToCard(objCard, obj1AddRound);
                var view = card.Adapt<CardView>();
                return new JsonResult(new ApiResponse<CardView>(true, view));
            }
            catch (Exception e)
            {

                return new JsonResult(new ApiResponse<String>(false, "False"));
            }
        }

        [HttpGet("{id}")]
        public IActionResult AddMoney(int id)
        {
            try
            {
                Log obj1AddRound = new Log();
                obj1AddRound.AddMoney = 100;
                var objCard = _cardService.GetCardByCardId(id);
                var card = _cardService.AddMoneyToCard(objCard, obj1AddRound);
                var view = card.Adapt<CardView>();
                return new JsonResult(new ApiResponse<CardView>(true, view));
            }
            catch (Exception e)
            {

                return new JsonResult(new ApiResponse<String>(false, "False"));
            }
        }

        [HttpGet("{id}")]
        public IActionResult Deduction(int id)
        {
            try
            {
                var objCard = _cardService.GetCardByCardId(id);
                if (objCard == null)
                    return new JsonResult(new ApiResponse<String>(true, "ไม่มีบัตรในรายการ"));
                var card = _cardService.Deduction(objCard);
                var view = card.Adapt<CardView>();
                return new JsonResult(new ApiResponse<CardView>(true, view));
            }
            catch (Exception e)
            {
                return new JsonResult(new ApiResponse<String>(false, "False"));
            }
        }

    }
}