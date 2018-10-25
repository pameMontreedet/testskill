using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppLib.Modules.Card {
    public interface ICardService {
        List<CardModel> GetCards();
        CardModel SaveCard(CardModel card);
        CardModel AddRoundToCard(CardModel card, Log.Log objAddRemoveMoneyRound);
        CardModel AddMoneyToCard(CardModel card, Log.Log objAddRemoveMoneyRound);
        CardModel GetCardByCardId(int cardId);
        CardModel Deduction(CardModel card);
    }
}