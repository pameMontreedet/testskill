using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppLib.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace AppLib.Modules.Card
{
    public class CardStorage : BaseStorage, ICardStorage
    {
        public CardStorage(AppDbContext dbContext) : base(dbContext)
        {

        }

        public enum CardType
        {
            OneWayCard = 1,
            CheckOneDayUnlimitedCard = 2,
            RabbitACard = 3,
            RabbitBCard = 4
        }
        public List<Card> All()
        {
            List<Card> cards = Database.Cards.ToList();
            return cards;
        }

        public CardModel Save(CardModel card)
        {
            Card obj = card.Adapt<Card>();
            obj.CreatedTime = DateTime.Now;

            if (obj.TypeId == (int)CardType.OneWayCard)
            {
                obj.CardValue = CalFee(obj.StationStartcode, obj.StationTerminatecode);
            }
            Database.Cards.Add(obj);
            Database.SaveChanges();

            Card cards = Database.Cards.Where(a => a.Id == obj.Id).FirstOrDefault();
            return cards.Adapt<CardModel>();
        }


        public CardModel AddRound(CardModel card, Log.Log addRound)
        {
            Card objCard = Database.Cards.Where(a => a.Id == card.Id).FirstOrDefault();
            if (objCard.TypeId == (int)CardType.RabbitACard)
            {
                if (objCard.LastTimeAddRound != null)
                {
                    TimeSpan? x = (DateTime.Now - objCard.LastTimeAddRound);
                    double daysDif = x.Value.TotalDays;
                    if (daysDif <= 30)
                    {
                        //กรณีวันที่เติมครั้งสุดท้ายไม่ถึง 30 วัน
                        objCard.CardRoundBalance = card.CardRoundBalance + addRound.AddRound;
                        objCard.LastTimeAddRound = DateTime.Now;
                    }
                    else
                    {
                        //กรณีวันที่เติมครั้งสุดท้ายเกิน 30 วัน
                        objCard.CardRoundBalance = addRound.AddRound;
                        objCard.LastTimeAddRound = DateTime.Now;
                    }
                }
                else
                {
                    //กรณียังไม่เคยเติม
                    objCard.CardRoundBalance = addRound.AddRound;
                    objCard.LastTimeAddRound = DateTime.Now;

                }

                Log.Log objLog = new Log.Log();
                objLog.CardId = (int)objCard.Id;
                objLog.AddRound = addRound.AddRound;
                objLog.TypeId = objCard.TypeId;
                objLog.CreatedTime = DateTime.Now;
                Database.Logs.Add(objLog);
                Database.SaveChanges();
            }
            return objCard.Adapt<CardModel>();
        }


        public CardModel AddMoney(CardModel card, Log.Log btsCost)
        {
            Card objCard = Database.Cards.Where(a => a.Id == card.Id).FirstOrDefault();
            if (objCard.TypeId == (int)CardType.RabbitBCard)
            {
                objCard.CardValueBalance = objCard.CardValueBalance + btsCost.AddMoney;
                Log.Log objLog = new Log.Log();
                objLog.CardId = (int)objCard.Id;
                objLog.TypeId = objCard.TypeId;
                objLog.AddMoney = btsCost.AddMoney;
                objLog.CreatedTime = DateTime.Now;

                Database.Logs.Add(objLog);
                Database.SaveChanges();
                return objCard.Adapt<CardModel>();
            }
            return objCard.Adapt<CardModel>();
        }

        public CardModel ByCardId(int cardId)
        {
            Card objCard = Database.Cards.Where(a => a.Id == cardId).FirstOrDefault();
            if (objCard == null)
            {
                return null;
            }
            return objCard.Adapt<CardModel>();
        }

        public CardModel Deduct(CardModel card)
        {
            string message;
            Card objCard = Database.Cards.Where(a => a.Id == card.Id).FirstOrDefault();
            CardModel objReturn = new CardModel();
            if (objCard.TypeId == (int)CardType.OneWayCard)
                objReturn = DropOneWayCard(objCard);
            else if (objCard.TypeId == (int)CardType.CheckOneDayUnlimitedCard)
                objReturn = CheckOneDayUnlimitedCard(objCard);
            else if (objCard.TypeId == (int)CardType.RabbitACard)
            {
                objReturn = RemoveRound(objCard);
            }
            else if (objCard.TypeId == (int)CardType.RabbitBCard)
            {
                objReturn = RemoveMoney(objCard);
            }
            else
            {
                objReturn = null;
                //    message = "ไม่สามารถทำรายการได้";
            }
            return objReturn;
        }

        private CardModel DropOneWayCard(Card objCard)
        {
            int? value = objCard.CardValue - CalFee(objCard.StationStartcode, "S1");
            if (value >= 0)
            {
                Database.Cards.Remove(objCard);
                Database.SaveChanges();
                return objCard.Adapt<CardModel>();
            }
            else
            {
                return null;
            }
        }

        private CardModel CheckOneDayUnlimitedCard(Card objCard)
        {
            TimeSpan? x = (DateTime.Now - objCard.CreatedTime);
            double daysDif = x.Value.TotalDays;
            if (daysDif <= 1)
            {
                return objCard.Adapt<CardModel>();
            }
            else
            {
                return null;
            }
        }

        public CardModel RemoveRound(Card objCard)
        {
            if (objCard.LastTimeAddRound != null)
            {
                TimeSpan? x = (DateTime.Now - objCard.LastTimeAddRound);
                double daysDif = x.Value.TotalDays;
                if (daysDif <= 30 && objCard.CardRoundBalance != 0)
                {
                    objCard.CardRoundBalance = objCard.CardRoundBalance - 1;
                    Log.Log objLog = new Log.Log();
                    objLog.CardId = (int)objCard.Id;
                    objLog.TypeId = objCard.TypeId;
                    objLog.RemoveRound = 1;
                    objLog.CreatedTime = DateTime.Now;
                    Database.Logs.Add(objLog);
                    Database.SaveChanges();
                    //return "หักรอบสำเร็จ รอบคงเหลือ " + objCard.CardRoundBalance + " รอบ";
                    return objCard.Adapt<CardModel>();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public CardModel RemoveMoney(Card objCard)
        {
            objCard.StationStartcode = "S2";
            objCard.StationTerminatecode = "E1";
            if (objCard.CardValueBalance >= 15)
            {
                int fee = CalFee(objCard.StationStartcode, objCard.StationTerminatecode);
                objCard.CardValueBalance = objCard.CardValueBalance - fee;
                Log.Log objLog = new Log.Log();
                objLog.CardId = (int)objCard.Id;
                objLog.TypeId = objCard.TypeId;
                objLog.RemoveMoney = fee;
                objLog.CreatedTime = DateTime.Now;
                Database.Logs.Add(objLog);
                Database.SaveChanges();
                // return "หักเงินสำเร็จ ยอดคงเหลือ " + objCard.CardValueBalance + " บาท";
                return objCard.Adapt<CardModel>();
            }
            else
            {
                return null;
            }
        }

        private int CalFee(string stationStartCode, string stationTerminateCode)
        {
            int pay = 0;
            List<string[]> list = new List<string[]>();
            string[] arr1 = new string[] { "CEN", "E1", "E2", "E3", "E4", "E5", "E6", "E7", "E8", "E9", "E10", "E11", "E12", "E13", "E14", "E15" };
            string[] arr2 = new string[] { "CEN", "N1", "N2", "N3", "N4", "N5", "N7", "N8" };
            string[] arr3 = new string[] { "CEN", "S1", "S2", "S3", "S5", "S6", "S7", "S8", "S9", "S10", "S11", "S12" };
            string[] arr4 = new string[] { "CEN", "W1" };
            list.Add(arr1);
            list.Add(arr2);
            list.Add(arr3);
            int indexInList1 = 0, indexInList2 = 0;
            int point1 = 0, point2 = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Contains(stationStartCode))
                {
                    indexInList1 = i;
                    point1 = Array.IndexOf(list[i], stationStartCode);
                }
                if (list[i].Contains(stationTerminateCode))
                {
                    indexInList2 = i;
                    point2 = Array.IndexOf(list[i], stationTerminateCode);
                }
            }
            int distanceP1P2;
            if (indexInList1 == indexInList2)
            {
                distanceP1P2 = Math.Abs(point2 - point1);
                pay = 15 + (distanceP1P2 * 5);
                if (pay > 59)
                    return 59;
                else
                    return pay;
            }
            else
            {
                distanceP1P2 = point1 + point2;
                pay = 15 + (distanceP1P2 * 5);
                if (pay > 59)
                    return 59;
                else
                    return pay;
            }
           
        }
    }
}