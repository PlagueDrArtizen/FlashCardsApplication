using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCardsApplication
{
    class DeckManager
    {
        private const string DeckDirectoryPass = @".\Decks";

        public void AddDeck(string deckName)
        {
            if (!Directory.Exists(DeckDirectoryPass))
            {
                Directory.CreateDirectory(DeckDirectoryPass);
            }

            string normalisedDeckName = deckName.Replace(" ", "_");
            string filePath = $"{DeckDirectoryPass}\\{normalisedDeckName}.json";

            if (File.Exists(filePath))
            {
                throw new DeckAlreadyExistException(filePath);
            }

            var newDeck = new Deck
            {
                Id = Guid.NewGuid(),
                Name = deckName,
            };

            SaveDeck(filePath, newDeck);
        }

        public string[] GetDeckNames()
        {
            string[] decksNames = Directory.GetFiles(DeckDirectoryPass, "*.json");


            for (int i = 0; i < decksNames.Length; i++)
            {
                decksNames[i] = Path.GetFileName(decksNames[i]);
                decksNames[i] = decksNames[i].Replace(".json", "");
                decksNames[i] = decksNames[i].Replace("_", " ");
            }

            return decksNames;
        }

        public void DeleteDeck(string deletableDeckName)
        {
            string fileName = deletableDeckName.Replace(" ", "_");

            File.Delete($"{DeckDirectoryPass}\\{fileName}.json");
        }

        public void DeckRename(string oldName, string newName)
        {
            string fileName = oldName.Replace(" ", "_");
            string filePath = $"{DeckDirectoryPass}\\{fileName}.json";
            var deck = LoadDeck(filePath);

            deck.Name = newName;
            SaveDeck(filePath, deck);
            string newNormalizedName = newName.Replace(" ", "_");

            File.Move(filePath, $"{DeckDirectoryPass}\\{newNormalizedName}.json");
        }

        public void AddCard(string deckName, string newWord, string newTranslate)
        {
            string fileName = deckName.Replace(" ", "_");
            string filePath = $"{DeckDirectoryPass}\\{fileName}.json";
            var deck = LoadDeck(filePath);

            var newCard = new Card
            {
                Id = Guid.NewGuid(),
                Word = newWord,
                Translate = newTranslate
            };

            deck.Cards.Add(newCard);
            SaveDeck(filePath, deck);
        }

        public void UpdateCard(string deckName, Guid cardId, string word, string translate)
        {
            string fileName = deckName.Replace(" ", "_");
            string filePath = $"{DeckDirectoryPass}\\{fileName}.json";
            var deck = LoadDeck(filePath);
            var card = deck.Cards.First(c => c.Id == cardId);

            card.Word = word;
            card.Translate = translate;

            SaveDeck(filePath, deck);
        }

        public void DeleteCard(string deckName, Guid cardId)
        {
            string fileName = deckName.Replace(" ", "_");
            string filePath = $"{DeckDirectoryPass}\\{fileName}.json";
            var deck = LoadDeck(filePath);
            var card = deck.Cards.First(c => c.Id == cardId);

            deck.Cards.Remove(card);

            SaveDeck(filePath, deck);
        }

        public Deck GetDeck(string deckName)
        {
            string fileName = deckName.Replace(" ", "_");
            string filePath = $"{DeckDirectoryPass}\\{fileName}.json";
            var deck = LoadDeck(filePath);

            return deck;
        }

        private Deck LoadDeck(string filePath)
        {
            string deckJson = File.ReadAllText(filePath);
            var deck = JsonConvert.DeserializeObject<Deck>(deckJson);

            return deck;
        }

        private void SaveDeck(string filePath, Deck deck)
        {
            string deckJson = JsonConvert.SerializeObject(deck);

            File.WriteAllText(filePath, deckJson);
        }
    }
}
