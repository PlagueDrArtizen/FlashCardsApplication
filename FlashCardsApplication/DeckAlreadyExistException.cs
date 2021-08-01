using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCardsApplication
{
    class DeckAlreadyExistException : Exception
    {

        public DeckAlreadyExistException(string deckName) : base($"Deck with name {deckName} already exist")
        {

        }
    }
}
