namespace Opgave
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class TextProcessor
    {
        public string Text { get; set; }

/// <summary>
/// Constucts a string of words and the total number of times the words occurre in the text sorted by most frequent first. Each word and number is seperadted by a new line character
/// </summary>
/// <returns></returns>
        public string GetListOfWordsSortedByCountAsString(){
            List<KeyValuePair<string, int>> wordsWithCOunt = GetListOfWordCountsAsKeyValuePairs();
            if (wordsWithCOunt == null) return string.Empty;
            wordsWithCOunt = wordsWithCOunt.OrderByDescending(element => element.Value).ToList();

            string wordCountList = string.Empty;
            foreach (KeyValuePair<string, int> kvp in wordsWithCOunt){
                wordCountList += $"{kvp.Key} - {kvp.Value}\n";
            }

            return wordCountList;
        }

/// <summary>
/// Breaks the text down into a list of words with all special characters and white space removed
/// </summary>
/// <returns></returns>
        public List<string> GetListOfWords (){
            if (Text.Length == 0){
                return null;
            }

            List<string> ListOfWords = Text.Split(" ").ToList();
            
            //clean all sets of non alphanumeric characters
            for (int i = 0; i < ListOfWords.Count(); i++){
                Regex rgx = new Regex("[^a-zA-Z0-9ÆØÅæøå -]");
                ListOfWords[i] = rgx.Replace(ListOfWords[i], "");
            }

            //remove all empty sets
            ListOfWords.RemoveAll(element => element.Length.Equals(0));

            return ListOfWords;
        }

/// <summary>
/// Consturcts a list of KeyValuePairs where the key is a word from the text and the value is the amount of times the word occurres in the text
/// </summary>
/// <returns></returns>
        public List<KeyValuePair<string, int>> GetListOfWordCountsAsKeyValuePairs(){
            List<string> words = GetListOfWords();
            if (words == null) return null;
            List<KeyValuePair<string, int>> kVPList = new List<KeyValuePair<string, int>>();
            foreach (string s in words){
                if (kVPList.Where(element => element.Key.Equals(s)).Count() == 0){
                    kVPList.Add(new KeyValuePair<string, int>(
                        s,
                        words.FindAll(element => element.Equals(s)).Count())
                    );
                }
            }

            return kVPList;
        }
    }
}