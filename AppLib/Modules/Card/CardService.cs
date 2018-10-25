using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppLib.Core;
using Mapster;

namespace AppLib.Modules.Card {
    public class CardService : BaseService, ICardService {

        private ICardStorage _storage;

        public CardService (ICardStorage storage) {
            _storage = storage;
        }

        public List<CardModel> GetCards () {
            var models = _storage.All().Adapt<List<CardModel>>();
            return models;
        }

        public CardModel SaveCard(CardModel card){
            var models = _storage.Save(card).Adapt<CardModel>();
            return models;
        }

        public CardModel GetCardByCardId(int cardId){
            var models = _storage.ByCardId(cardId).Adapt<CardModel>();
            return models;
        }

         public CardModel AddRoundToCard(CardModel card, Log.Log objAddRemoveMoneyRound){
             var models = _storage.AddRound(card, objAddRemoveMoneyRound).Adapt<CardModel>();
            return models;
         }

        public CardModel AddMoneyToCard(CardModel card, Log.Log objAddRemoveMoneyRound){
            var models = _storage.AddMoney(card, objAddRemoveMoneyRound).Adapt<CardModel>();
            return models;
        }

        public CardModel Deduction(CardModel card){
            var models = _storage.Deduct(card);
            return models;
        }
    }

    
}