using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    // 딜러 클래스를 작성하고, 딜러의 행동 로직을 구현하세요.
    public class Dealer : Player
    {
        // 코드를 여기에 작성하세요

        public Dealer() { }

        /// <summary>
        /// 핸드의 카드 값 합이 17 미만이라면 덱에서 카드 한장 뽑고 뽑은 카드 반환, 17 이상이라면 null 반환.
        /// </summary>
        public new Card DrawCardFromDeck(Deck deck)
        {
            if (Hand.GetTotalValue() < 17)
            {
                Card drawnCard = deck.DrawCard();
                Hand.AddCard(drawnCard);
                return drawnCard;
            }
            else { return null;}

        }
    }
}
