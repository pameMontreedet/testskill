using System;
using System.Collections.Generic;

namespace AppLib.Modules.Card {
    public interface ICardStorage {
        List<Card> All();
        CardModel Save(CardModel card);
        CardModel ByCardId(int cardId);
        CardModel AddRound(CardModel card, Log.Log objAddRemoveMoneyRound);
        // string RemoveRound(CardModel card);
        CardModel AddMoney(CardModel card, Log.Log objAddRemoveMoneyRound);
        // string RemoveMoney(CardModel card);
        // string DropOneWayCard(CardModel card);
        // string DropOneDayUnlimitedCard(CardModel card);
        CardModel Deduct(CardModel card);
    }
}