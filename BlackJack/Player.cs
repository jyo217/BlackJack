using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    // 플레이어를 표현하는 클래스
    public class Player
    {
        public Hand Hand { get; private set; }
        private bool isStay = false;
        public Player()
        {
            Hand = new Hand();
        }

        public Card DrawCardFromDeck(Deck deck)
        {
            Card drawnCard = deck.DrawCard();
            Hand.AddCard(drawnCard);
            return drawnCard;
        }

        public bool IsStay()
        {
            return isStay;
        }
        public void Stay()
        {
            isStay = true;
        }
    }
}
