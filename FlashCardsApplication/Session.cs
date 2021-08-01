using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCardsApplication
{
    class Session
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int RightAnswers { get; set; }

        public int CardsCount { get; set; }

        public override string ToString()
        {
            return $"Session was started at {Start} and ended at {End}. User gave {RightAnswers} right answers from {CardsCount} cards.";
        }
    }
}
