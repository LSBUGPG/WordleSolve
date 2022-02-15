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

public class Solver : MonoBehaviour
{
    public Word target;
    public Word guess;
    public Word reject;
    public Word keep;
    public TextAsset wordList;
    public Slider progress;
    public Ranking topTen;
    List<string> words = new List<string>();
    List<Score> rankings = new List<Score>();

    void Start()
    {
        words.AddRange(wordList.text.Replace("\r\n", "\n").Split('\n'));
        //words.RemoveRange(1000, words.Count - 1000);
        StartCoroutine(Solve());
    }

    IEnumerator Solve()
    {
        yield return null;
        var stopwath = new System.Diagnostics.Stopwatch();
        stopwath.Start();
        Match match = new Match(words[0]);
        //int i = 12016;
        for (int i = 0; i < words.Count; ++i)
        {
            progress.value = (float)i / (float)words.Count;
            string start = words[i];
            guess.Set(start);
            float rating = 0;
            //string word = words[Random.Range(0, words.Count)];
            //target.Set(word);
            foreach (string word in words)
            {
                match.Set(start);
                match.Score(word);
                rating += Rejections(match);
/*
                guess.Set(match);
                while (true)
                {
                    string candidate = words[Random.Range(0, words.Count)];
                    if (match.CanReject(candidate))
                    {
                        reject.Set(candidate);
                    }
                    else
                    {
                        keep.Set(candidate);
                    }
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                    yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Space));
                }
*/

                if (stopwath.ElapsedMilliseconds > 33)
                {
                    target.Set(word);
                    guess.Set(match);
                    stopwath.Restart();
                    yield return null;
                }
            }

            rating = rating / (float)words.Count;
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
        int rejections = 0;
        foreach (string word in words)
        {
            if (match.CanReject(word))
            {
                rejections++;
            }
        }
        return (float)rejections / (float)words.Count;
    }
}
