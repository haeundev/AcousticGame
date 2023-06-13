using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using Random = System.Random;

namespace Apin.DialogueSystem
{
    public static class UtilityString
    {       
        
        public static string RemoveAllSubstringAfterLastMark(string inString, char mark)
        {
            if (inString == "") return "";

            inString = inString.Trim();

            int last_index = inString.LastIndexOf(mark);

            return inString.Substring(0, last_index);
        }
        public static string GetSubstringUntilLastMark_Inclusive(string inString, char mark)
        {
            if (inString == "") return "";

            inString = inString.Trim();

            int last_index = inString.LastIndexOf(mark);

            return inString.Substring(0, last_index + 1);
        }
        
        public static string RemoveAllSubstringAfterFirstMark(string inString, char mark)
        {
            if (inString == "") return "";
            
            inString = inString.Trim();

            int last_index = inString.IndexOf(mark);

            return inString.Substring(0, last_index);
        }

        public static string RemoveAllFrontPartSubstringOfLastMark(string inString, char mark)
        {
            if (inString == "") return "";
            
            inString = inString.Trim();

            int last_index = inString.LastIndexOf(mark) + 1; //jks exclusive

            return inString.Substring(last_index);
        }

        
        public static string RemoveAllSubstringBeforeFirstMark(string inString, char mark)
        {
            if (inString == "") return "";

            inString = inString.Trim();

            int first_index = inString.IndexOf(mark);

            return inString.Substring(first_index);
        }
        
        public static string RemoveAllSubstringBeforeFirstMark_Inclusive(string inString, char mark)
        {
            if (inString == "") return "";

            inString = inString.Trim();

            int first_index = inString.IndexOf(mark);

            return inString.Substring(first_index+1);
        }
        
        public static bool IsArticle(this string str)
        {
            return str == "a" || str == "an" || str == "the";
        }
        
        public static string[] SeparateWords(string word_list)
        {
            return word_list.Split(' ');
        }
        
        public static string[] SeparateWords(string word_list, char[] seperator)
        {
            return word_list.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string[] SeperateByString(string word_list, string seperator)
        {
            return word_list.Split(new string[] { seperator }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string RemoveBetween(string input_string, string start, string end)
        {
            string result = GetSubstringBetween(input_string, start, end);
            
            return input_string.Replace(result, "");
        }
        
        public static string RemoveBetween_Inclusive(string input_string, string start, string end)
        {           
            int start_index = input_string.IndexOf(start);
            int end_index = input_string.IndexOf(end);
            string result = input_string.Substring(start_index, end_index+1 - start_index);
            
            return input_string.Replace(result, "");
        }

        // ex: HAVE.NOT(s:i)(o:money)(x:now)  --->  HAVE.NOT
        public static string RemoveAllBetween_Inclusive(string input_string, string start, string end)
        {
            if (!input_string.Contains(start) || !input_string.Contains(end))
                return input_string;

            return RemoveAllBetween_Inclusive(RemoveBetween_Inclusive(input_string, start, end), start, end);
        }



        // ex: ab(cd (ef))(gh)ij  --->  abij
        public static string RemoveAllSubstringBetweenParentheses(string input)
        {
            int index_open = input.IndexOf('(');
            if (index_open < 0) return input;

            int index_close = input.IndexOf(')');

            int start = index_open + 1;
            int count = index_close - start;
            int index_open2 = input.IndexOf('(', start, count);

            if (index_open2 >= 0)
                return RemoveAllSubstringBetweenParentheses(RemoveInnerMostParentheses(input, start, index_close));

            return RemoveAllSubstringBetweenParentheses(RemoveBetween(input, index_open, index_close));
        }


        public static string RemoveInnerMostParentheses(string input, int start, int end)
        {
            int count = end - start;
            int index_open = input.IndexOf('(', start, count);

            if (index_open >= 0)
                RemoveInnerMostParentheses(input, index_open + 1, end);

            return RemoveBetween(input, start, end);

        }
        
        
        
        public static string RemoveBetween(string input_string, int start, int end)
        {           
            string result = input_string.Substring(start, end+1 - start);
            
            return input_string.Replace(result, "");
        }

        

        public static string ReplaceBetween_inclusive(string input, string new_word, string start, string end)
        {
            string old = GetSubstringBetween_Inclusive(input, start, end);
            string output = input.Replace(old, new_word);
            return output;
        }



        public static string RemoveEndCharacter(string input, char character_to_remove)
        {
            if (input.Length < 1)
                return input;
                
            char last = input[input.Length - 1];
            if (character_to_remove != last)
                return input;

            return input.Substring(0, input.Length - 1);
        }
        

        public static string GetSubstringBetween(string input_string, string start, string end)
        {
            if (!input_string.Contains(start) || !input_string.Contains(end))
                return string.Empty;

            int start_index = input_string.IndexOf(start);
            int end_index = input_string.IndexOf(end);
            string result = input_string.Substring(start_index+1, end_index - start_index-1);
            return result.RemoveWhitespace();
        }
        public static string GetSubstringBetween_Inclusive(string input_string, string start, string end)
        {
            if (!input_string.Contains(start) || !input_string.Contains(end))
                return string.Empty;

            int start_index = input_string.IndexOf(start);
            int end_index = input_string.IndexOf(end);
            string result = input_string.Substring(start_index, end_index+1 - start_index);
            return result.RemoveWhitespace();
        }

        
        public static List<string> GetAllSubstringBetweenParenthesis(string input)
        {
            List<string> output = new List<string>();

            while (input.Contains("("))
            {
                string content = input.Split('(', ')')[1]; 
                output.Add(content);
                input = input.Replace("(" + content + ")", "");
            }
            
            return output;
        }
        
        public static string[] SeperateStringBy(string word_list, params char[] delimiter)
        {
            string[] splited = word_list.Split(delimiter);

            for (int i = 0; i < splited.Length; i++)
            {
                splited[i] = splited[i].Trim();
            }
            
            return splited;
        }


        public static string GetSubstringBeforeIfHaveMark(string input_string, string mark)
        {
            if (input_string == "") return "";
            
            if (!input_string.Contains(mark))
            {
                return input_string;
            }
            
            int mark_index = input_string.IndexOf(mark);

            if (mark_index == 0)
                return string.Empty;
            
            return input_string.Substring(0, mark_index).RemoveWhitespace();
        }
        
        
        public static string GetSubstringBefore(string input_string, string mark)
        {
            if (input_string == "") return "";

            if (!input_string.Contains(mark))
            {
                return string.Empty;
            }

            input_string = input_string.Trim();

            int mark_index = input_string.IndexOf(mark);

            if (mark_index == 0)
                return string.Empty;
            
            return input_string.Substring(0, mark_index).RemoveWhitespace();
        }


        public static string GetSubstringAfter(string input_string, string mark)
        {
            if (input_string == "") return "";
            
            input_string = input_string.Trim();
            
            int last_index = input_string.LastIndexOf(mark) + 1; //jks exclusive

            if (input_string.Length <= last_index)
                return string.Empty;
            
            return input_string.Substring(last_index);
        }



        public static string RemoveSubstring(string orignal, string sub)
        {
            return orignal.Replace(sub, "");
        }
        public static string RemoveSubstring2(this string orignal, string sub)
        {
            return orignal.Replace(sub, "");
        }
        
        
        public static string RemovePunctuations(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            return 
                input.Replace("?","").Replace(",","")
                    .Replace("!","").Replace(".","").
                    Replace("'","").Replace(";","")
                    .Replace(":","").Replace("\"","").
                    Replace("\n","");
        }
        
        public static string RemovePunctuations_PreserveApostrophe(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            return input
                .Replace("?","").Replace(",","")
                .Replace("!","").Replace(".","")
                //.Replace("'","").Replace(";","")
                .Replace(":","").Replace("\"","")
                .Replace("\n","");
        }
        
        public static string RemoveNonAlphanumericCharacters(this string input)
        {
            var nonAlphanumerics = new Regex("[^a-zA-Z0-9 -]");
            return nonAlphanumerics.Replace(input, string.Empty);
        }
        
        public static List<string> GetLowerAlphanumericWords(this string input)
        {
            return input.RemoveNonAlphanumericCharacters().ToLower().Split(' ').ToList();
        }
        
        public static bool IsDemonstrative(this string str)
        {
            return str == "this" || str == "that" || str == "these" || str == "those";
        }
        
        public static string GetUntilNonAlphabet(this string input)
        {
            var charArr = new List<char>();
            foreach (var ch in input)
            {
                if (char.IsLetter(ch))
                    charArr.Add(ch);
                else return new string(charArr.ToArray());
            }
            return new string(charArr.ToArray());
        }
        
        public static string RemovePunctuationsForTestBot(this string input) //ex) "There is an A."  --> "There is an A"
        {
            if (string.IsNullOrEmpty(input)) return input;

            return 
                input.Replace("?","").Replace(",","")
                    .Replace("!","").Replace(".","").Replace(";","")
                    .Replace(":","").Replace("\"","").Replace("\n","");
        }

        public static List<string> GetStringsBetweenAngleBrackets(this string input)
        {
            var regex = new Regex(@"(?<=<).*?(?=>)");
            return (from Match match in regex.Matches(input) select match.ToString()).ToList();
        }

        public static string GetStringsBetweenSquareBrackets(this string input)
        {
            var regex = new Regex(@"(?<=<).*?(?=>)");
            return (from Match match in regex.Matches(input) select match.ToString()).FirstOrDefault();
        }

        public static string MakeFirstLetterUppercase(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            
            return input.First().ToString().ToUpper() + input.Substring(1);
        }


        public static string RemoveWhitespace(this string input)
        {
            return new string(input.Where(c => !char.IsWhiteSpace(c)).ToArray());
        }
        
        public static List<(string, string)> GetIntentRequirementCallsInParenthesis(this string input) // "PIK(x:how)(o:kind)(o:of)"
        {
            var result = new List<(string, string)>();
            var calls = input.Split(new[] { "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
            // string[] {("x", "how"), ("o", "kind"), ("o", "of")}
            foreach (var call in calls)
            {
                var split = call.Split(':');
                if (split.Length < 2)
                {
                    Debug.LogError($"IntentGroup Error : {input}");
                    continue;
                }
                result.Add((split[0], split[1]));
            }
            return result;
        }
        
        public static string RemoveSquareBrackets(this string input)
        {
            return new string(input.Where(c => c != '[' && c != ']').ToArray());
        }
        
        public static string RemoveCurlyBrackets(this string input)
        {
            return new string(input.Where(c => c != '{' && c != '}').ToArray());
        }
        
        public static string RemoveWhitespace2(this string str) {
            return string.Join("", str.Split(default(string[])
                                , System.StringSplitOptions.RemoveEmptyEntries));
        }
        
        public static void PrintStringArray(string[] strings)
        {
            Debug.Log("PrintStringArray: ===============");
            foreach (var str in strings)
            {
                Debug.Log(str + "\n");
            }

            Debug.Log("=================================");

        }



        /// <summary>
        /// Contains approximate string matching
        /// </summary>
        /// <summary>
        /// Compute the distance between two strings.
        /// </summary>
        public static int LevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }

            // Step 7
            return d[n, m];
        }

        
        
        public static string NumberToLetter(string val)
        {
            switch(val)
            {
                case "1" : return "one";
                case "2" : return "two";
                case "3" : return "three";
                case "4" : return "four";
                case "5" : return "five";
                case "6" : return "six";
                case "7" : return "seven";
                case "8" : return "eight";
                case "9" : return "nine";
                case "10": return "ten";
                case "11": return "eleven";
                case "12": return "twelve";
                case "13": return "thirteen";
                case "14": return "fourteen";
                case "15": return "fifteen";
            }

            return val;
        }


        public static string LetterToNumber(string val)
        {
            switch (val)
            {
                case "one": return "1";
                case "two": return "2";
                case "three": return "3";
                case "four": return "4";
                case "five": return "5";
                case "six": return "6";
                case "seven": return "7";
                case "eight": return "8";
                case "nine": return "9";
                case "ten": return "10";
                case "eleven": return "11";
                case "twelve": return "12";
                case "thirteen": return "13";
                case "fourteen": return "14";
                case "fifteen": return "15";
            }

            return val;
        }
        
        public static int StringToInt(string val)
        {
            switch (val)
            {
                case "one": return 1;
                case "two": return 2;
                case "three": return 3;
                case "four": return 4;
                case "five": return 5;
                case "six": return 6;
                case "seven": return 7;
                case "eight": return 8;
                case "nine": return 9;
                case "ten": return 10;
                case "eleven": return 11;
                case "twelve": return 12;
                case "thirteen": return 13;
                case "fourteen": return 14;
                case "fifteen": return 15;
            }

            return 0;
        }

        static string[] letters = new[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten" };

        public static string ReplaceAllLetterToNumberInJObject(string sentence)
        {
            foreach (string letter in letters)                
                sentence = sentence.Replace("\""+letter+ "\"", "\"" + LetterToNumber(letter) + "\"");      //ex: "four" --> "4" 
                //sentence = Regex.Replace(sentence, letter, LetterToNumber(letter));  //ex: "four" --> "4" // fixes NLG issue


            return sentence;
        }

        
        public static string RemoveDeterminers(string word)
        {
            if (!(word.Contains("a ") || word.Contains("an ") || word.Contains("the ")))
                return word;

            string s = "";
            if (word.Contains("an "))
                s = word.Substring(3, word.Length - 3);
            else if (word.Contains("a "))
                s = word.Substring(2, word.Length - 2);            
            else if (word.Contains("the "))
                s = word.Substring(4, word.Length - 4);
            return s;
        }

        
        public static Dictionary<TKey, TValue> RandomSortDictionary<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            Random r = new Random();
            return dictionary.OrderBy(x => r.Next())
                .ToDictionary(item => item.Key, item => item.Value);
        }
        
        
        public static string [] SeparateAllParenthesis(this string input, bool with_parenthesis = true)
        {
            string[] ops = {};

            if (input == string.Empty)
                return ops;

            input = RemoveWhitespace(input);

            if (input.Contains(")("))
            {
                ops = SeperateByString(input, ")(");
            }
            else
            {
                ops = new[] {input};
            }
            
            for (int i=0; i < ops.Length; i++)
            {
                ops[i] = RemoveSubstring(ops[i], "(");
                ops[i] = RemoveSubstring(ops[i], ")");
            }

            if (with_parenthesis)
            {
                for (int i=0; i < ops.Length; i++)
                {
                    ops[i] = "(" + ops[i] + ")";
                }
            }
            

            return ops;
        }


        public static bool IsNumber(this string input)
        {
            return int.TryParse(input, out var output);
        }

        public static string ConvertNonBreakingHyphen(this string input)
        {
            return input.Replace("-", "\u2011");
        }

        public static string ConvertNonBreakingWhitespace(this string input)
        {
            return input.Replace(" ", "\u00A0");
        }

        public static bool IsNullOrEmpty(this List<string> list)
        {
            if (list == default)
                return true;
            return list.Count == 0;
        }
    }
}
