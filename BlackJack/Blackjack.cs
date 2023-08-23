using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    // 블랙잭 게임을 구현하세요. 
    public class Blackjack
    {
        int PLAYER_WIN = 1;
        int DEALER_WIN = 2;
        int DRAW = 3;
        // 코드를 여기에 작성하세요
        public Blackjack() { }

        public void GameStart() {

            

            //플레이어, 딜러, 카드 생성
            Player player = new Player();
            Dealer dealer = new Dealer();
            Deck deck;

            int flag = 0;
            bool isPlaying = true;

            //시작. 종료 시 까지 반복
            while (true)
            {
                Card card;

                //카드 섞기(생성 과정에서 셔플까지 포함임)
                player = new Player();
                dealer = new Dealer();
                deck = new Deck();

                //서로 핸드에 카드를 2장씩 넣음
                for(int i = 0; i< 2; i++)
                {
                    player.DrawCardFromDeck(deck);
                    dealer.DrawCardFromDeck(deck);
                }

                PrintGameScreen(player, dealer);

                if (IsBlackJack(player) && !(IsBlackJack(dealer))) { flag = PLAYER_WIN; }
                else if (IsBlackJack(dealer) && !(IsBlackJack(player))) { flag = DEALER_WIN; }
                else if (IsBlackJack(dealer) && IsBlackJack(player)) { flag = DRAW; }
                else
                {
                    while (true)
                    {
                        //카드를 뽑을 때 마다 버스트 여부 계속 확인

                        //플레이어는 더 Hit 할지 Stay 할지 입력받아서 선택
                        flag = PlayerHit(player, dealer, deck);
                        if (flag == DEALER_WIN) { break; }

                        //딜러는 알아서 뽑음. 
                        flag = DealerHit(player, dealer, deck);
                        if (flag == PLAYER_WIN) { break; }

                        //누구도 버스트가 나지 않았고 둘 모두 Stay 상태라면 승부를 가리고 카드 뽑기 종료
                        if (player.IsStay() && dealer.IsStay()) 
                        {
                            flag = WhosWinner(player.Hand.GetTotalValue(), dealer.Hand.GetTotalValue());
                            break; 
                        }
                    }
                }

                //게임 결과 표시.
                PrintResult(flag);

                //새 게임 여부 입력받음
                if (!ReGame()) { break; }
            }
        }

        public int DealerHit(Player player, Dealer dealer, Deck deck)
        {
            Card card;
            int flag = 0;

            if (!dealer.IsStay())
            {
                card = dealer.DrawCardFromDeck(deck);
                if (card == null)
                {
                    dealer.Stay(); Console.WriteLine("딜러 - Stay\n");
                }
                else
                {
                    PrintGameScreen(player, dealer);
                    Console.WriteLine($"딜러가 뽑은 카드는 {card.ToString()} 입니다.\n");
                }

                //버스트라면 상대편이 승리
                if (IsBurst(dealer))
                {
                    flag = PLAYER_WIN;
                    PrintGameScreen(player, dealer);
                    Console.WriteLine($"딜러 {dealer.Hand.GetTotalValue()} 점으로 BURST.\n");
                }
            }
            //stay 라면 더 뽑지 않음.
            else
            {
                Console.WriteLine("\n딜러 - Stay\n");
            }

            return flag;
        }

        public int PlayerHit(Player player, Dealer dealer, Deck deck)
        {
            Card card;
            int flag = 0;

            if (!(player.IsStay()))
            {
                Console.WriteLine("[1] HIT!  |  [2] STAY\n");
                while (true)
                {
                    string? msg = Console.ReadLine();
                    Console.WriteLine("\n");
                    if (msg == "1") { break; }
                    else if (msg == "2") { player.Stay(); break; }
                    else { Console.WriteLine("\n잘못된 입력입니다. 다시 입력해주세요."); break; }
                }
            }

            if (!player.IsStay())
            {
                card = player.DrawCardFromDeck(deck);
                if (card == null)
                {
                    player.Stay(); Console.WriteLine("\n플레이어 - Stay\n");
                }
                else
                {
                    PrintGameScreen(player, dealer);
                    Console.WriteLine($"플레이어가 뽑은 카드는 {card.ToString()} 입니다.\n");
                }

                //버스트라면 상대편이 승리
                if (IsBurst(player))
                {
                    flag = DEALER_WIN;
                    PrintGameScreen(player, dealer);
                    Console.WriteLine($"플레이어 {player.Hand.GetTotalValue()} 점으로 BURST.\n");
                }
            }
            //stay 라면 더 뽑지 않음.
            else
            {
                Console.WriteLine("플레이어 - Stay\n");
            }

            return flag;
        }

        public bool IsBurst(Player player)
        {
            if (player.Hand.GetTotalValue() > 21) { return true; }
            return false;
        }

        public bool IsBlackJack(Player player)
        {
            if(player.Hand.GetTotalValue() == 21) { return true; }
            return false;
        }
        
        /// <summary>
        /// 각자의 손패, 점수 표시
        /// </summary>
        public void PrintGameScreen(Player player, Dealer dealer)
        {
            Console.Clear();

            List<Card> cards;

            cards = dealer.Hand.GetCards();
            Console.Write("[딜러]\n\n 손패 : | ? |  ");
            for (int i = 1; i < cards.Count; i++)
            {
                Console.Write($"|{cards[i].ToString()}|  ");
            }
            Console.WriteLine($"현재 점수 : {dealer.Hand.GetTotalValue() - dealer.Hand.GetCards().First().GetValue()} + a\n\n");

            cards = player.Hand.GetCards();
            Console.Write("[플레이어]\n\n 손패 : ");
            for (int i = 0; i < cards.Count; i++)
            {
                Console.Write($"|{cards[i].ToString()}|  ");
            }
            Console.WriteLine($"현재 점수 : {player.Hand.GetTotalValue()}\n\n");
        }

        public int WhosWinner(int playerScore, int dealerScore)
        {
            if (playerScore > dealerScore) { return PLAYER_WIN; }
            else if (playerScore < dealerScore) { return DEALER_WIN; }
            return DRAW;
        }

        public void PrintResult(int flag)
        {
            if (flag == PLAYER_WIN) 
            {
                Console.WriteLine("\n당신의 승리입니다!!");
            }
            else if (flag == DEALER_WIN)
            {
                Console.WriteLine("\n딜러의 승리입니다.");
            }
            else
            {
                Console.WriteLine("\n무승부!!");
            }
        }

        public bool ReGame ()
        {
            while (true)
            {
                Console.WriteLine("\n새 게임을 이어가시겠습니까?\n");
                Console.WriteLine("\n[1] 예  |  [2] 아니오\n");
                string? msg = Console.ReadLine();
                if (msg == "1")
                {
                    return true;
                }
                else if (msg == "2")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("\n잘못된 입력입니다.");
                }
            }
        }
    }
}
