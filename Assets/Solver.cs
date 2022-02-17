using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct Score
{
    public string word;
    public float score;
    public Score(string word, float score)
    {
        this.word = word;
        this.score = score;
    }
}

public class WordSet
{
    bool [] set;
    int count;

    public WordSet(int count, bool state)
    {
        set = new bool [count];
        int i;
        for (i = 0; i < count; ++i)
        {
            set[i] = state;
        }
        this.count = state? count : 0;
    }

    public void Intersect(WordSet intersect)
    {
        int size = set.Length;
        for (int i = 0; i < size; ++i)
        {
            bool removed = set[i] && !intersect.set[i];
            if (removed)
            {
                count--;
                set[i] = intersect.set[i];
            }
        }
    }

    public void Add(int index)
    {
        if (!set[index])
        {
            set[index] = true;
            count++;
        }
    }

    public int Count()
    {
        return count;
    }
}

public class Solver : MonoBehaviour
{
    public Word target;
    public Word guess;
    public TextAsset wordList;
    public Slider progress;
    public Ranking topTen;
    List<string> allWords;
    WordList words;
    List<Score> rankings = new List<Score>();
    static readonly int size = 5;

    void Start()
    {
        allWords = new List<string>(wordList.text.Replace("\r\n", "\n").Split('\n'));
        words = new WordList(allWords);
        StartCoroutine(Solve());
    }

    IEnumerator Solve()
    {
        yield return null;
        var stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        float elapsed = 0.0f;
        float nextTimeCheck = 1000.0f;
        for (int i = 0; i < allWords.Count; ++i)
        {
            progress.value = (float)i / (float)allWords.Count;
            string start = allWords[i];
            guess.Set(start);
            float rating = 0;
            Match match = new Match(start);
            foreach (string word in allWords)
            {
                match.Score(word);
                rating += Rejections(match);
                if (stopwatch.ElapsedMilliseconds > 33)
                {
                    elapsed += stopwatch.ElapsedMilliseconds;
                    if (elapsed > nextTimeCheck)
                    {
                        nextTimeCheck = elapsed + 1000.0f;
                        float remaining = ((elapsed / (float)(i + 1)) * (allWords.Count - i)) / 3600000.0f;
                        Debug.Log($"{remaining} hours remaining");
                    }
                    target.Set(word);
                    guess.Set(match);
                    stopwatch.Restart();
                    yield return null;
                }
            }

            rating = rating / (float)allWords.Count;
            rankings.Add(new Score(start, rating));
            rankings.Sort((a, b) => b.score.CompareTo(a.score));

            for (int j = 0; j < 10; ++j)
            {
                if (j < rankings.Count)
                {
                    Score entry = rankings[j];
                    topTen.SetRank(j, $"{entry.word} {entry.score * 100.0f}");
                }
            }
        }
    }

    float Rejections(Match match)
    {
        WordSet possible = new WordSet(allWords.Count, true);
        int all = possible.Count();
        int [] letters = new int [Histogram.alphabet];
        for (int i = 0; i < size; ++i)
        {
            if (match.clues[i] == Match.Clue.Correct)
            {
                char c = match.word[i];
                possible.Intersect(words.WordsWithLetterInPlace(c, i));
                letters[Histogram.IndexFromLetter(c)]++;
            }
        }

        for (int i = 0; i < size; ++i)
        {
            char c = match.word[i];
            if (match.clues[i] == Match.Clue.Used)
            {
                letters[Histogram.IndexFromLetter(c)]++;
            }
        }

        for (char c = 'a'; c <= 'z'; ++c)
        {
            if (letters[Histogram.IndexFromLetter(c)] == 0)
            {
                letters[Histogram.IndexFromLetter(c)] = -1;
            }
        }

        for (int i = 0; i < size; ++i)
        {
            char c = match.word[i];
            if (match.clues[i] == Match.Clue.Unused && letters[Histogram.IndexFromLetter(c)] == -1)
            {
                letters[Histogram.IndexFromLetter(c)] = 0;
            }
        }

        for (char c = 'a'; c <= 'z'; ++c)
        {
            int count = letters[Histogram.IndexFromLetter(c)];
            if (count >= 0)
            {
                possible.Intersect(words.WordsWithLetterCount(c, count));
            }
        }

        return (float)(all - possible.Count()) / (float)all;
    }
}
