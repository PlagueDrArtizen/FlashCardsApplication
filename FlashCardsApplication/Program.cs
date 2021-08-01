using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FlashCardsApplication
{
    class Program
    {
        private static DeckManager deckManager = new DeckManager();

        private static CardPlay cardPlay = new CardPlay();

        static void Main(string[] args)
        {
            bool exit = true;

            do
            {
                Console.Clear();
                Console.WriteLine("1 - Начать обучение");
                Console.WriteLine("2 - Редактирование колод");
                Console.WriteLine("0 - Выйти");
                Console.WriteLine();
                Console.Write("Введите число: ");
                int menu = int.Parse(Console.ReadLine());
                Console.WriteLine();

                switch (menu)
                {
                    case 1:
                        Console.Clear();
                        PlayStart();
                        break;

                    case 2:
                        Console.Clear();
                        DecksRedactor();
                        break;

                    case 0:
                        exit = false;
                        break;

                    default:
                        Console.WriteLine("Нет действия под таким номером. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            } while (exit);
        }

        private static void PlayStart()
        {
            string[] decksNames = deckManager.GetDeckNames();

            Console.WriteLine("ДОБРО ПОЖАЛОВАТЬ!!!");
            Console.WriteLine();

            for (int i = 0; i < decksNames.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {decksNames[i]}");
            }

            Console.WriteLine();
            Console.Write("Выберите колоды(через запятую): ");

            string userDecksInput = Console.ReadLine();
            string[] userDecksTexts = userDecksInput.Split(',');
            var userDecksNames = new List<string>();

            foreach(string userDeckText in userDecksTexts)
            {
                int deckIndex = int.Parse(userDeckText);
                userDecksNames.Add(decksNames[deckIndex - 1]);
            }

            Console.WriteLine();
            Console.Write("Сколько карточек вы хотите пройти?: ");

            int cardNumber = int.Parse(Console.ReadLine());

            cardPlay.Start(userDecksNames, cardNumber);
        }

        private static void DecksRedactor()
        {
            bool exit = true;

            do
            {
                Console.Clear();
                Console.WriteLine("1 - Создать колоду");
                Console.WriteLine("2 - Изменить колоду");
                Console.WriteLine("3 - Удалить колоду");
                Console.WriteLine("0 - Назад");
                Console.WriteLine();
                Console.Write("Введите число: ");
                int menu = int.Parse(Console.ReadLine());
                Console.WriteLine();

                switch (menu)
                {
                    case 1:
                        Console.Clear();
                        AddDeckMenu();
                        break;

                    case 2:
                        Console.Clear();
                        DeckRedactor();
                        break;

                    case 3:
                        Console.Clear();
                        DeleteDeckMenu();
                        break;

                    case 0:
                        Console.Clear();
                        exit = false;                     
                        break;

                    default:
                        Console.WriteLine("Нет действия под таким номером. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            } while (exit);
        }

        private static void AddDeckMenu()
        {
            Console.Write("Назовите новую колоду: ");
            string name = Console.ReadLine();
            // Оставить else или запустить повтор
            if (name.Contains("_"))
            {
                Console.WriteLine("The name should not contain \"_\".");
                Console.ReadKey();
            }
            else
            {
                try
                {
                    deckManager.AddDeck(name);
                }
                catch (DeckAlreadyExistException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
            }
        }

        private static void DeleteDeckMenu()
        {
            string[] decksNames = deckManager.GetDeckNames();
            int deletableDeckNumber;

            for (int i = 0; i < decksNames.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {decksNames[i]}");
            }

            Console.WriteLine();
            Console.Write("Введите номер удаляемой колоды: ");

            try
            {
                deletableDeckNumber = int.Parse(Console.ReadLine());
                if (deletableDeckNumber > 0 && deletableDeckNumber <= decksNames.Length)
                {
                    deckManager.DeleteDeck(decksNames[deletableDeckNumber - 1]);
                }
            }
            catch (ArgumentOutOfRangeException outOfRange)
            {
                Console.WriteLine("Error: {0}", outOfRange.Message);
                Console.ReadKey();
            }
        }

        private static void DeckRedactor()
        {
            bool exit = true;

            do
            {
                Console.Clear();
                Console.WriteLine("1 - Переименовать колоду");
                Console.WriteLine("2 - Создать карту");
                Console.WriteLine("3 - Изменить карту");
                Console.WriteLine("4 - Удалить карту");
                Console.WriteLine("0 - Назад");
                Console.WriteLine();
                Console.Write("Введите число: ");
                int menu = int.Parse(Console.ReadLine());
                Console.WriteLine();

                switch (menu)
                {
                    case 1:
                        Console.Clear();
                        Rename();
                        break;

                    case 2:
                        Console.Clear();
                        AddCardMenu();
                        break;

                    case 3:
                        Console.Clear();
                        CardRedactor();
                        break;

                    case 4:
                        Console.Clear();
                        DeleteCardMenu();
                        break;

                    case 0:
                        Console.Clear();
                        exit = false;
                        break;

                    default:
                        Console.WriteLine("Нет действия под таким номером. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            } while (exit);
        }

        private static void Rename()
        {
            string[] decksNames = deckManager.GetDeckNames();
            int renamableDeckNumber;
            string newDeckName;

            for (int i = 0; i < decksNames.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {decksNames[i]}");
            }

            Console.WriteLine();
            Console.Write("Введите номер колоды которую хотите периименовать: ");
            renamableDeckNumber = int.Parse(Console.ReadLine());

            if (renamableDeckNumber > 0 && renamableDeckNumber <= decksNames.Length)
            {
                Console.Write($"Введите новое имя для {decksNames[renamableDeckNumber - 1]}: ");
                newDeckName = Console.ReadLine();
                // Оставить else или запустить повтор
                if (newDeckName.Contains("_"))
                {
                    Console.WriteLine("The name should not contain \"_\".");
                    Console.ReadKey();
                }
                else
                {
                    try
                    {
                        deckManager.DeckRename(decksNames[renamableDeckNumber - 1], newDeckName);
                    }
                    catch (ArgumentOutOfRangeException outOfRange)
                    {
                        Console.WriteLine("Error: {0}", outOfRange.Message);
                        Console.ReadKey();
                    }
                }
            }
            
        }

        private static void AddCardMenu()
        {
            string[] decksNames = deckManager.GetDeckNames();
            int deckNumber = 0;
            Deck deck = null;            

            for (int i = 0; i < decksNames.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {decksNames[i]}");
            }

            Console.WriteLine();
            Console.Write("Введите номер колоды куда хотите добавить карту: ");
            try
            {
                deckNumber = int.Parse(Console.ReadLine());
                if (deckNumber > 0 && deckNumber <= decksNames.Length)
                {
                    deck = deckManager.GetDeck(decksNames[deckNumber - 1]);
                }
            }
            catch (ArgumentOutOfRangeException outOfRange)
            {
                Console.WriteLine("Error: {0}", outOfRange.Message);
                Console.ReadKey();
            }

            if(deck != null)
            {
                Console.Write("Введите слово карточки: ");
                string word = Console.ReadLine();
                Console.Write("Введите перевод слова: ");
                string translate = Console.ReadLine();

                deckManager.AddCard(decksNames[deckNumber - 1], word, translate);
            }
        }

        private static void CardRedactor()
        {
            string[] decksNames = deckManager.GetDeckNames();
            int deckNumber = 0;
            Deck deck = null;

            for (int i = 0; i < decksNames.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {decksNames[i]}");
            }

            Console.WriteLine();
            Console.Write("Введите номер колоды где хотите изменить карту: ");
            try
            {
                deckNumber = int.Parse(Console.ReadLine());
                if (deckNumber > 0 && deckNumber <= decksNames.Length)
                {
                    deck = deckManager.GetDeck(decksNames[deckNumber - 1]);
                }
            }
            catch (ArgumentOutOfRangeException outOfRange)
            {
                Console.WriteLine("Error: {0}", outOfRange.Message);
                Console.ReadKey();
            }

            if (deck != null)
            {
                for (int i = 0; i < deck.Cards.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {deck.Cards[i].Word} - {deck.Cards[i].Translate}");
                }

                Console.WriteLine();
                Console.Write("Введите номер карты которую хотите изменить: ");
                int cardNumber = int.Parse(Console.ReadLine());

                var card = deck.Cards[cardNumber - 1];
                string word = ReadWithDefault("Измените новое значение word: ", card.Word);
                string translate = ReadWithDefault("Измените новое значение translate: ", card.Translate);

                deckManager.UpdateCard(decksNames[deckNumber - 1], card.Id, word, translate);
            }
        }

        private static void DeleteCardMenu()
        {
            string[] decksNames = deckManager.GetDeckNames();
            int deckNumber = 0;
            Deck deck = null;

            for (int i = 0; i < decksNames.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {decksNames[i]}");
            }

            Console.WriteLine();
            Console.Write("Введите номер колоды откуда хотите удалить карту: ");
            try
            {
                deckNumber = int.Parse(Console.ReadLine());
                if (deckNumber > 0 && deckNumber <= decksNames.Length)
                {
                    deck = deckManager.GetDeck(decksNames[deckNumber - 1]);
                }
            }
            catch (ArgumentOutOfRangeException outOfRange)
            {
                Console.WriteLine("Error: {0}", outOfRange.Message);
                Console.ReadKey();
            }

            if (deck != null)
            {
                for (int i = 0; i < deck.Cards.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {deck.Cards[i].Word} - {deck.Cards[i].Translate}");
                }

                Console.WriteLine();
                Console.Write("Введите номер карты которую хотите удалить: ");
                int cardNumber = int.Parse(Console.ReadLine());
                var card = deck.Cards[cardNumber - 1];

                deckManager.DeleteCard(decksNames[deckNumber - 1], card.Id);
            }
        }

        private static string ReadWithDefault(string text, string defaultValue)
        {
            Console.Write(text);
            string value = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(value))
            {
                value = defaultValue;
            }

            return value;
        } 
    }
}
