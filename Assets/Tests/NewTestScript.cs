using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTestScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void NewTestScriptSimplePasses()
    {
        // Use the Assert class to test conditions
        Assert.That("Test", Is.EqualTo("Test"));
    }

    [Test]
    public void TestHistogram()
    {
        Histogram histogram = new Histogram("lilly");
        Assert.That(histogram.LetterCount('l'), Is.EqualTo(3));
        Assert.That(histogram.LetterCount('o'), Is.EqualTo(0));
    }

    [Test]
    public void TestMatch()
    {
        Match match = new Match("lilly");
        string result = match.Compare("loser");
        Assert.That(result[0], Is.EqualTo('m'));
        Assert.That(result[1], Is.EqualTo('n'));
    }

    [Test]
    public void TestContain()
    {
        Match match = new Match("lilly");
        string result = match.Compare("point");
        Assert.That(result[2], Is.EqualTo('c'));
    }

    [Test]
    public void TestGuessContainsTwo()
    {
        Match match = new Match("onkus");
        string result = match.Compare("fanny");
        Assert.That(result, Is.EqualTo("nncnn"));
    }

    [Test]
    public void TestGuessContainsTwoAndOneMatch()
    {
        Match match = new Match("trogs");
        string result = match.Compare("siphs");
        Assert.That(result, Is.EqualTo("nnnnm"));
    }

    [Test]
    public void TestTripleLetter()
    {
        Match match = new Match("salad");
        string result = match.Compare("saala");
        Assert.That(result, Is.EqualTo("mmccn"));
    }

}
