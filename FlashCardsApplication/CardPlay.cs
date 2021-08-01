using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCardsApplication
{
    class CardPlay
    {
        private readonly DeckManager deckManager = new DeckManager();

        public void Start(List<string> userDecksNames, int cardNumber)
        {
            var cardsToPlay = GetCardsToPlay(userDecksNames, cardNumber);
            var session = new Session
            {
                Start = DateTime.Now,
                RightAnswers = 0,
                CardsCount = cardsToPlay.Count
            };
            Console.WriteLine("Игра началась");

            for(int i = 0; i < cardsToPlay.Count; i++)
            {                
                var card = cardsToPlay[i];
                Console.WriteLine();
                Console.Write($"{i + 1}/{cardsToPlay.Count} | {card.Word} - ");
                string answer = Console.ReadLine();

                if (answer.ToLower() == card.Translate.ToLower())
                {
                    session.RightAnswers++;
                    Console.WriteLine("Верно");
                }
                else
                {
                    Console.WriteLine($"Неверно. Верный ответ {card.Translate}");
                }
            }

            session.End = DateTime.Now;

            SaveSessionResult(session);

            Console.WriteLine();
            Console.WriteLine("Конец. Спасибо за игру. Нажмите любую кнопку...");
            Console.ReadKey();
        }

        private void SaveSessionResult(Session session)
        {
            File.AppendAllText("User_Sessions.txt", $"{session}\n");
        }

        private List<Card> GetCardsToPlay(List<string> userDecksNames, int cardNumber)
        {
            var cards = new List<Card>();
            var result = new List<Card>();

            foreach (string deckName in userDecksNames)
            {
                var deck = deckManager.GetDeck(deckName);
                cards.AddRange(deck.Cards);
            }

            var random = new Random();
            int n = cards.Count;

            while(n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                var card = cards[k];
                cards[k] = cards[n];
                cards[n] = card;
            }

            if (cards.Count < cardNumber)
            {
                result = cards;
            }
            else
            {
                result = cards.GetRange(0, cardNumber);
            }

            return result;
        }
    }
}
