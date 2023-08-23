using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    // 덱을 표현하는 클래스
    public class Deck
    {
        private List<Card> cards;

        /// <summary>
        /// 생성자 호출 시 중복 없는 52개의 카드를 자동으로 생성, 카드 섞기까지 완료함.
        /// 매번 새 게임 시 마다 셔플만 할 게 아니고, 그냥 new Deck 을 하는 게 낫겠다.
        /// </summary>
        public Deck()
        {
            cards = new List<Card>();

            foreach (Suit s in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank r in Enum.GetValues(typeof(Rank)))
                {
                    cards.Add(new Card(s, r));
                }
            }

            Shuffle();
        }

        public void Shuffle()
        {
            Random rand = new Random();

            for (int i = 0; i < cards.Count; i++)
            {
                int j = rand.Next(i, cards.Count);
                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
        }

        public Card DrawCard()
        {
            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }
    }
}
