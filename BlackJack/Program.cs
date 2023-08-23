namespace BlackJack
{
    using System;
    using System.Collections.Generic;

    public enum Suit { Hearts, Diamonds, Clubs, Spades }
    public enum Rank { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }


    class Program
    {
        static void Main(string[] args)
        {
            // 블랙잭 게임을 실행하세요
            Blackjack blackjack = new Blackjack();
            blackjack.GameStart();
        }
    }
}