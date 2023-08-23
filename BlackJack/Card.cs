using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    // 카드 한 장을 표현하는 클래스
    /// <summary>
    /// 카드값은 2~10까지는 그대로, JQK 는 10, Ace 는 11로 취급한다.
    /// Card.ToString 은 카드의 랭크와 문장을 하나의 문자열 양식으로 반환한다. 
    /// </summary>
    public class Card
    {
        public Suit Suit { get; private set; }
        public Rank Rank { get; private set; }

        public Card(Suit s, Rank r)
        {
            Suit = s;
            Rank = r;
        }

        public int GetValue()
        {
            if ((int)Rank <= 10)
            {
                return (int)Rank;
            }
            else if ((int)Rank <= 13)
            {
                return 10;
            }
            else
            {
                return 11;
            }
        }

        public override string ToString()
        {
            char suit = ' ';
            switch (Suit)
            {
                case Suit.Hearts: suit = '♥';  break;

                case Suit.Diamonds: suit = '◆'; break;

                case Suit.Clubs: suit = '♣'; break;

                case Suit.Spades: suit = '♠'; break;
            }
            return $"{Rank}{suit}";
        }
    }
}
