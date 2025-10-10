using System.Text;

namespace main
{
    class Prog
    {
        public class Token
        {
            public int value;
        }
        public class Text : Token
        {
            public List<Sentence> sentences { get; set; }
            public Text()
            {
                this.sentences = new List<Sentence>();
            }
            public Text(List<Sentence> sentences)
            {
                this.sentences = sentences;
            }
            public void Print()
            {
                foreach (Sentence sentence in this.sentences)
                {
                    sentence.Print();
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
            public class Sentence : Token
            {
                public List<Word> words;
                public List<Punctuation> signs;
                public Sentence()
                {
                    this.words = new List<Word>();
                    this.signs = new List<Punctuation>();
                }
                public Sentence(List<Word> words, List<Punctuation> punctuations)
                {
                    this.words = words;
                    this.signs = punctuations;
                }
                public void Print()
                {
                    foreach (Word word in this.words)
                    {
                        word.Print();
                        Console.WriteLine();
                    }
                    foreach (Punctuation punctuation in this.signs)
                    {
                        punctuation.Print();
                        Console.Write(" ");
                    }
                }
                public void Print_Words()
                {
                    foreach (Word word in this.words)
                    {
                        word.Print();
                        Console.WriteLine();
                    }
                }
                public void Print_Punctuation()
                {
                    foreach (Word word in this.words)
                    {
                        word.Print();
                        Console.WriteLine();
                    }
                }
                public int Count_Words()
                {
                    return this.words.Count;
                }
                public int Count_Punctuation()
                {
                    return this.signs.Count;
                }
                public int Sentence_Length()
                {
                    int length = 0;
                    foreach (Word word in this.words)
                    {
                        length += word.Length();
                    }
                    foreach(Punctuation punctuation in this.signs)
                    {
                        length += 1;
                    }
                    return length;
                }
            }
            public class Word : Token
            {
                public string word { get; set; }
                public Word()
                {
                    this.word = "";
                }
                public Word(string s)
                {
                    this.word = s;
                }
                public char this[int index]
                {
                    get
                    {
                        return this[index];
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
            }
            public class Punctuation : Token
            {
                public char sign { get; set; }
                public Punctuation()
                {
                    this.sign = '0';
                }
                public Punctuation(char sign)
                {
                    this.sign = sign;
                }
                public void Print()
                {
                    Console.Write(this.sign);
                }
            }
            public static Punctuation Create_Punctuation(char ch)
            {
                return new Punctuation(ch);
            }
            public static Word Create_Word(string s)
            {
                return new Word(s);
            }
            public static Sentence Create_Sentence(List<Word> words, List<Punctuation> punctuations)
            {
                Sentence ret = new Sentence();
                foreach (Word word in words)
                {
                    ret.words.Add(word);
                }
                foreach (Punctuation signs in punctuations)
                {
                    ret.signs.Add(signs);
                }
                return ret;
            }
            public static Text Create_Text(List<Sentence> sentences)
            {
                return new Text(sentences);
            }
            public static Text Read_text(string path)
            {
                List<Word> words = new List<Word>();
                List<Punctuation> punctuations = new List<Punctuation>();
                List<Sentence> sentences = new List<Sentence>();
                string word = "";
                using (StreamReader sr = new StreamReader(path))
                {
                    char ch;
                    while (sr.EndOfStream != true)
                    {
                        ch = Convert.ToChar(sr.Read());
                        if (Char.IsLetter(ch) || Char.IsDigit(ch)) // if [0-9] and [a-z] and [A-Z]
                        {
                            word += ch;
                        }
                        else
                        {
                            if (ch == ' ' && word.Length != 0)
                            {
                                words.Add(Create_Word(word));
                                word = "";
                            }
                            else if (ch == '.' || ch == '!' || ch == '?') // ... Не будет работать 
                            {
                                if (word.Length != 0)
                                {
                                    words.Add(Create_Word(word));
                                    word = "";
                                    punctuations.Add(Create_Punctuation(ch));
                                    sentences.Add(Create_Sentence(words, punctuations));
                                    words.Clear();
                                    punctuations.Clear();
                                }
                                else
                                {
                                    punctuations.Add(Create_Punctuation(ch));
                                    sentences.Add(Create_Sentence(words, punctuations));
                                    words.Clear();
                                    punctuations.Clear();
                                }
                            }
                            else if (Char.IsPunctuation(ch) == true)
                            {
                                if (word.Length != 0)
                                {
                                    words.Add(Create_Word(word));
                                    word = "";
                                }
                                punctuations.Add(Create_Punctuation(ch));
                            }
                        }
                    }
                }
                return new Text(sentences);
            }
            public static void Print_Amount_Words(Text text) // По порядку по возрастанию кол-ва слов
            {
                for (int i = 0; i < text.Count_Sentences() - 1; i++)
                {
                    for (int j = 0; j < text.Count_Sentences() - 1 - i; j++)
                    {
                        if (text[i].Count_Words() > text[i + 1].Count_Words())
                        {
                            Sentence temp = text[j];
                            text[j] = text[j + 1];
                            text[j + 1] = temp;
                        }
                    }
                }
                text.Print(); // дописать !!!!!!!
            }
            public static void Print_Length_Words(Text text) // По порядку по возрастанию длины 
            {
                for (int i = 0; i < text.Count_Sentences() - 1; i++)
                {
                    for (int j = 0; j < text.Count_Sentences() - 1 - i; j++)
                    {
                        if (text[i].Sentence_Length() > text[i + 1].Sentence_Length())
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
            public static List<Word> Delete(Text text, int length) // Удалить все слова нужной длины, которые начинаются с согласной
            {
                string vowel_letters = "AEIOUaeiou";
                List<Word> words = new List<Word>();
                foreach (Sentence sentence in text.sentences)
                {
                    foreach (Word word in sentence.words)
                    {
                        if (word.Length() == length && vowel_letters.Contains(word[0]) == false)
                        {
                            words.Add(word);
                        }
                    }
                }
                return words;
            }
            public static void Replace(Text text, int number)
            {

            }
            public static void Main(string[] args)
            {
                string path = @"C:\Users\noob1\source\repos\Laba 3\Laba 3\TextFile1.txt";
                Text text = Read_text(path);
                text.Print();
            }

        }
    }
}