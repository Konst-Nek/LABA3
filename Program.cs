using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace main
{
    public class Prog
    {
        public class Token
        {
            public int token_value;
            public Token()
            {
                token_value = 0;
            }
            public void Print_Token_Value()
            {
                Console.WriteLine($"Token value: {this.token_value}");
            }
        }
        [Serializable]
        public class Text
        {
            public List<Sentence> sentences;
            public Text()
            {
                this.sentences = new List<Sentence>();
            }
            public Text(List<Sentence> sentences)
            {
                this.sentences = new List<Sentence>(sentences);
            }
            public void Add(Sentence sentence)
            {
                this.sentences.Add(sentence);
            }
            public void Print()
            {
                foreach (Sentence sentence in this.sentences)
                {
                    sentence.Print();
                }
                Console.WriteLine("---------------------------------------------------------------");
            }
            public void Print_Words()
            {
                foreach (Sentence sentence in this.sentences)
                {
                    sentence.Print_Words();
                    Console.WriteLine();
                }
            }
            public Sentence this[int index]
            {
                get
                {
                    return this.sentences[index];
                }
                set
                {
                    this.sentences[index] = value;
                }
            }
            public int Count_Sentences()
            {
                return this.sentences.Count;
            }
            public int Count_Token_Value()
            {
                int token_value = 0;
                foreach (Sentence sentence in this.sentences)
                {
                    token_value += sentence.token_value;
                }
                return token_value;
            }
            public void Print_Token_Values()
            {
                Console.WriteLine($"Text token value: {this.Count_Token_Value()}");
            }
            public int Count_Token_Amount()
            {
                int token_amount = 0;
                foreach (Sentence sentence in this.sentences)
                {
                    token_amount += sentence.Count_Token_Amount();
                }
                return token_amount;
            }
            public void Print_Token_Amount()
            {
                Console.WriteLine($"Text token amount: {this.Count_Token_Amount()}");
            }
        }
        [Serializable]
        public class Sentence : Token
        {
            public List<Word> words;
            public List<Punctuation> signs;
            public Sentence()
            {
                this.words = new List<Word>();
                this.signs = new List<Punctuation>();
                this.token_value = 0;
            }
            public Sentence(List<Word> words, List<Punctuation> punctuations)
            {
                this.words = new List<Word>(words);
                this.signs = new List<Punctuation>(punctuations);
                foreach (Word word in words)
                {
                    this.token_value += word.token_value;
                }
                foreach (Punctuation punctuation in punctuations)
                {
                    this.token_value += 1;
                }
            }
            public Word this[int index]
            {
                get
                {
                    return this.words[index];
                }
                set
                {
                    this.words[index] = value;
                }
            }
            public void Add(Word word) // Добавить 1 слово
            {
                this.words.Add(word);
                this.token_value += word.token_value;
            }
            public void Add(Punctuation punctuation) // Добавить 1 знак
            {
                this.signs.Add(punctuation);
                this.token_value += 1;
            }
            public void Add(Word word, Punctuation punctuation) // Добавить 1 знак и 1 слово
            {
                this.words.Add(word);
                this.token_value += word.token_value;
                this.signs.Add(punctuation);
                this.token_value += 1;
            }
            public void Add(List<Word> words) // Добавить несколько слов
            {
                foreach (Word word in words)
                {
                    this.Add(word);
                }
            }
            public void Add(List<Punctuation> punctuations) // Добавить несколько знаков
            {
                foreach (Punctuation punctuation in punctuations)
                {
                    this.Add(punctuation);
                }
            }
            public void Add(List<Word> words, List<Punctuation> punctuations) // Добавить несколько слов и знаков
            {
                foreach (Word word in words)
                {
                    this.Add(word);
                }
                foreach (Punctuation punctuation in punctuations)
                {
                    this.Add(punctuation);
                }
            }
            public void Remove_Word(Word word, bool ignore_register = false)
            {
                foreach (Word this_word in this.words)
                {
                    if (ignore_register == true)
                    {
                        if (this_word.ToLower() == word.ToLower())
                        {
                            this.words.Remove(this_word);
                            break;
                        }
                    }
                    else
                    {
                        if (this_word == word)
                        {
                            this.words.Remove(this_word);
                            break;
                        }
                    }
                }
            }
            public void Remove_Word(String word, bool ignore_register = false)
            {
                foreach (Word this_word in this.words)
                {
                    if (ignore_register == true)
                    {
                        if (this_word.ToString().ToLower() == word.ToLower())
                        {
                            this.words.Remove(this_word);
                            break;
                        }
                    }
                    else
                    {
                        if (this_word.ToString() == word)
                        {
                            this.words.Remove(this_word);
                            break;
                        }
                    }
                }
            }
            public void Remove_Word(int index)
            {
                this.words.RemoveAt(index);
            }
            public void Print()
            {
                foreach (Word word in this.words)
                {
                    word.Print();
                    Console.Write(" ");
                }
                Console.WriteLine();
                foreach (Punctuation punctuation in this.signs)
                {
                    punctuation.Print();
                    Console.Write(" ");
                }
                Console.WriteLine("\n---------------");
            }
            public void Print_Words()
            {
                foreach (Word word in this.words)
                {
                    word.Print();
                    Console.Write(" ");
                }
            }
            public void Print_Punctuation()
            {
                foreach (Punctuation punctuation in this.signs)
                {
                    punctuation.Print();
                    Console.WriteLine();
                }
            }
            public void Print_Token_Amount()
            {
                Console.WriteLine($"Token amount: {this.Count_Token_Amount()}");
            }
            public int Count_Words()
            {
                return this.words.Count;
            }
            public int Count_Punctuation()
            {
                return this.signs.Count;
            }
            public int Count_Token_Amount()
            {
                return this.Count_Words() + this.Count_Punctuation();
            }
            public int Sentence_Length()
            {
                int length = 0;
                foreach (Word word in this.words)
                {
                    length += word.Length();
                }
                foreach (Punctuation punctuation in this.signs)
                {
                    length += 1;
                }
                return length;
            }
            public int Sentence_Length_Words()
            {
                int length = 0;
                foreach (Word word in this.words)
                {
                    length += word.Length();
                }
                return length;
            }
            public void Sort_Words()
            {
                for (int i = 0; i < this.words.Count - 1; i++)
                {
                    for (int j = 0; j < this.words.Count - i - 1; j++)
                    {
                        int short_one = 0;
                        if (this.words[j].Length() >= this.words[j + 1].Length())
                        {
                            short_one = this.words[j+1].Length();
                        }
                        else
                        {
                            short_one = this.words[j].Length();
                        }
                        for (int k = 0; k < short_one; k++)
                        {
                            if (Char.ToLower(this.words[j][k]) > Char.ToLower(this.words[j + 1][k]))
                            {
                                Word temp = this.words[j];
                                this.words[j] = this.words[j + 1];
                                this.words[j + 1] = temp;
                                break;
                            }
                        }
                    }
                }
            }
        }
        [Serializable]
        public class Word : Token
        {
            public string word;
            public Word()
            {
                this.word = "";
                this.token_value = 0;
            }
            public Word(string s)
            {
                this.word = s;
                this.token_value += s.Length;
            }
            public char this[int index]
            {
                get
                {
                    return this.word[index];
                }
                set
                {
                    this[index] = value;
                }
            }
            public void Print()
            {
                Console.Write(this.word);
            }
            public int Length()
            {
                return this.word.Length;
            }
            public void Replace(string s)
            {
                this.token_value -= this.word.Length;
                this.word = s;
                this.token_value += s.Length;
            }
            public String ToLower()
            {
                return this.word.ToLower();
            }
            public String ToUpper()
            {
                return this.word.ToUpper();
            }
            public String ToString()
            {
                return this.word;
            }
        }
        [Serializable]
        public class Punctuation : Token
        {
            public char sign;
            public Punctuation()
            {
                this.token_value = 0;
                this.sign = '0';
            }
            public Punctuation(char sign)
            {
                this.sign = sign;
                this.token_value = 1;
            }
            public void Print()
            {
                if (this.sign == '0')
                {
                    Console.Write("Empty");
                }
                else
                {
                    Console.Write(this.sign);
                }
            }
        }
        public static Punctuation Create_Punctuation(char ch)
        {
            return new Punctuation(ch);
        } // Создать Знак
        public static Word Create_Word(string word)
        {
            return new Word(word);
        } // Создать слово
        public static List<Word> Create_Words(string s)
        {
            List<Word> result = new List<Word>();
            string[] words = s.Split(" ");
            foreach (string word in words)
            {
                result.Add(Create_Word(word));
            }
            return result;
        } // Создать список слов по пробелам
        public static Sentence Create_Sentence(List<Word> words, List<Punctuation> punctuations)
        {
            return new Sentence(words, punctuations);
        } // Создать предложение
        public static Text Create_Text(List<Sentence> sentences)
        {
            return new Text(sentences);
        } // Создать текст
        public static Text Read_txt(string path)
        {
            List<Sentence> sentences = new List<Sentence>();
            List<Word> words = new List<Word>();
            string word = "";
            string sentence = "";
            Regex reg_end_sentence = new Regex(@"[a-zA-Z0-9а-яА-Я][!.?]\s+[a-zA-Z0-9а-яА-Я]");
            Regex reg_end_sentence1 = new Regex(@"[a-zA-Z0-9а-яА-Я][!.?]\s*[\n\r]{2}\s*[\n\r]*\s*[a-zA-Z0-9а-яА-Я]");
            Regex reg_end_sentence2 = new Regex(@"\w[^.!?]\s*[\n\r]{2}\s*[\n\r]*\s*[a-zA-Z0-9а-яА-Я]");
            Regex reg_end_final = new Regex(@"[a-zA-Z0-9][!.?]\s*[\n\r]*");
            List<Punctuation> punctuations = new List<Punctuation>();
            char ch = ' ';
            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.EndOfStream != true)
                {
                    ch = Convert.ToChar(sr.Read());
                    sentence += ch;
                    if (Char.IsLetterOrDigit(ch))
                    {
                        word += ch;
                    }
                    else if ((ch == ' ' || ch == '\n' || ch == '\r') && word.Length != 0)
                    {
                        words.Add(Create_Word(word));
                        word = "";
                    }
                    else if (Char.IsPunctuation(ch))
                    {
                        punctuations.Add(Create_Punctuation(ch));
                        if (word.Length != 0)
                        {
                            words.Add(Create_Word(word));
                            word = "";
                        }
                    }
                    if (reg_end_sentence.IsMatch(sentence) || reg_end_sentence1.IsMatch(sentence))
                    {
                        sentence = "";
                        sentences.Add(Create_Sentence(words, punctuations));
                        words.Clear();
                        punctuations.Clear();
                    }
                    else if (reg_end_sentence2.IsMatch(sentence))
                    {
                        sentence = "";
                        punctuations.Add(Create_Punctuation('.'));
                        sentences.Add(Create_Sentence(words, punctuations));
                        words.Clear();
                        punctuations.Clear();
                    }
                }
                if (!reg_end_final.IsMatch(sentence))
                {
                    if (word.Length > 0 || words.Count > 0)
                    {
                        if (word.Length > 0)
                        {
                            words.Add(Create_Word(word));
                        }
                        punctuations.Add(Create_Punctuation('.'));
                        sentences.Add(Create_Sentence(words, punctuations));
                    }
                }
                else
                {
                    sentences.Add(Create_Sentence(words, punctuations));
                }
                return new Text(sentences);
            }
        }

        public static void PrintBy_Amount_Words(Text text) // По порядку по возрастанию кол-ва слов
        {
            for (int i = 0; i < text.Count_Sentences() - 1; i++)
            {
                for (int j = 0; j < text.Count_Sentences() - 1 - i; j++)
                {
                    if (text[j].Count_Words() > text[j + 1].Count_Words())
                    {
                        Sentence temp = text[j];
                        text[j] = text[j + 1];
                        text[j + 1] = temp;
                    }
                }
            }
            text.Print();
        }
        public static void PrintBy_Length_Words(Text text) // По порядку по возрастанию длины 
        {
            for (int i = 0; i < text.Count_Sentences() - 1; i++)
            {
                for (int j = 0; j < text.Count_Sentences() - 1 - i; j++)
                {
                    if (text[j].Sentence_Length_Words() > text[j + 1].Sentence_Length_Words())
                    {
                        Sentence temp = text[j];
                        text[j] = text[j + 1];
                        text[j + 1] = temp;
                    }
                }
            }
            text.Print();
        }
        public static List<Word> Find_Words(Text text, int length) // Найти слова нужной длины
        {
            List<Word> words = new List<Word>();
            foreach (Sentence sentence in text.sentences)
            {
                foreach (Word word in sentence.words)
                {
                    if (word.Length() == length)
                    {
                        words.Add(word);
                    }
                }
            }
            return words;
        }
        public static List<Word> Question_Find(Text text, int length) // Во всех вопросительных предложениях найти слова нужной длины без повторов
        {
            List<Word> words = new List<Word>();
            foreach (Sentence sentence in text.sentences)
            {
                if (Convert.ToChar(sentence.signs[sentence.signs.Count() - 1]) == '?')
                {
                    foreach (Word word in sentence.words)
                    {
                        if (word.Length() == length && words.Contains(word) == false)
                        {
                            words.Add(word);
                        }
                    }
                }
            }
            return words;
        }
        public static bool Check_Consonant(Char letter)
        {
            String consonants_low = "bcdfghjklmnpqrstvwxyzбвгджзйклмнпрстфхцчшщ";
            String consonants_up = consonants_low.ToUpper();
            foreach (char ch in consonants_low)
            {
                if (ch == letter)
                {
                    return true;
                }
            }
            foreach (char ch in consonants_up)
            {
                if (ch == letter)
                {
                    return true;
                }
            }
            return false;
        }
        public static Text Delete(Text text, int length) // Удалить все слова нужной длины, которые начинаются с согласной
        {
            int i = 0;
            foreach (Sentence sentence in text.sentences)
            {
                i = 0;
                while (i < sentence.words.Count())
                {
                    if (sentence.words[i].Length() == length && Check_Consonant(sentence.words[i][0]) == true)
                    {
                        sentence.Remove_Word(i);
                        i -= 1;
                        continue;
                    }
                    i += 1;
                }
            }
            return text;
        }
        public static void Replace(Text text, int number, int length, string s)
        {
            foreach (Word word in text[number].words)
            {
                if (word.Length() == length)
                {
                    word.Replace(s);
                }
            }
        } // Заменить
        public static bool Check_Stop_Word(Word word, List<Word> stop_words)
        {
            bool key = false;
            foreach (Word stop_word in stop_words)
            {
                if (stop_word == word)
                {
                    key = true;
                }
            }
            return key;
        } // Проверить наличие стоп-слова/слов
        public static Text Delete_Stop_Words(Text text, string path)
        {
            List<Word> stop_words_list = new List<Word>();
            string stop_word = "";
            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.EndOfStream != true)
                {
                    stop_word = sr.ReadLine();
                    string[] stop_words = stop_word.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    foreach (String word in stop_words)
                    {
                        stop_words_list.Add(Create_Word(word));
                    }
                }
            }
            foreach (Sentence sentence in text.sentences)
            {
                foreach (Word word in sentence.words)
                {
                    if (Check_Stop_Word(word, stop_words_list) == true)
                    {
                        sentence.Remove_Word(word, true);
                    }
                }
            }
            return text;
        } // Удалить стоп-слова
        public static void Serialize(Text text)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Text));
            using (FileStream fs = new FileStream(@"C:\\Users\\noob1\\source\\repos\\Laba 3\\Laba 3\\Xml.text.xml", FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, text);
            }
        }
        public static void Main(string[] args)
        {
            string path = @"C:\Users\noob1\source\repos\Laba 3\Laba 3\test.txt";
            Text text = Read_txt(path);
            Serialize(text);
            text.Print();
        }
    }
}
