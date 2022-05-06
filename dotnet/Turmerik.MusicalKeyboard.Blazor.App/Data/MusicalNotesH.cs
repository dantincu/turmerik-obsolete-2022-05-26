using System.Collections.ObjectModel;
using Turmerik.Core.Helpers;

namespace Turmerik.MusicalKeyboard.Blazor.App.Data
{
    public class MusicalNotesH
    {
        public static readonly ReadOnlyCollection<string> MusicalNotes = new string[]
        {
            "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B"
        }.RdnlC();

        public static string GetMusicalNote(string baseNote, int scale)
        {
            char noteChar = baseNote.First();

            string noteStr = string.Concat(
                noteChar,
                scale,
                baseNote.Substring(1));

            return noteStr;
        }
    }
}
