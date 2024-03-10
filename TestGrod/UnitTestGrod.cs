using GROD;

namespace TestGrod;

public class Tests
{
    [Test]
    public void TestNullKey()
    {
        Grod g = [];
        g.UseOverlay = false;
        try
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            g[null] = "null";
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Fail();
        }
        catch (Exception)
        {
            Assert.Pass();
        }
    }

    [Test]
    public void TestEmptyKey()
    {
        Grod g = [];
        g.UseOverlay = false;
        try
        {
            g[""] = "empty";
            Assert.Fail();
        }
        catch (Exception)
        {
            Assert.Pass();
        }
    }

    [Test]
    public void TestWhitespaceKey()
    {
        Grod g = [];
        g.UseOverlay = false;
        try
        {
            g["   \t\r\n  "] = "whitespace";
            Assert.Fail();
        }
        catch (Exception)
        {
            Assert.Pass();
        }
    }

    [Test]
    public void TestNotFound()
    {
        Grod g = [];
        g.UseOverlay = false;
        Assert.That(g["k"], Is.EqualTo(""));
    }

    [Test]
    public void TestSingleValue()
    {
        Grod g = [];
        g.UseOverlay = false;
        g["k"] = "v";
        Assert.That(g["k"], Is.EqualTo("v"));
    }

    [Test]
    public void TestAdd()
    {
        Grod g = [];
        g.UseOverlay = false;
        g.Add("k", "v");
        Assert.That(g["k"], Is.EqualTo("v"));
    }

    [Test]
    public void TestAddTwice()
    {
        Grod g = [];
        g.UseOverlay = false;
        g.Add("k", "v");
        g.Add("k", "vvv");
        Assert.That(g["k"], Is.EqualTo("vvv"));
    }

    [Test]
    public void TestAddKeyValuePair()
    {
        Grod g = [];
        g.UseOverlay = false;
        g.Add(new KeyValuePair<string, string>("k", "v"));
        Assert.That(g["k"], Is.EqualTo("v"));
    }

    [Test]
    public void TestNullValue()
    {
        Grod g = [];
        g.UseOverlay = false;
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        g["k"] = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        Assert.That(g["k"], Is.EqualTo(""));
    }

    [Test]
    public void TestCaseSensitiveKeys()
    {
        Grod g = [];
        g.UseOverlay = false;
        g["k"] = "v";
        Assert.That(g["K"], Is.EqualTo(""));
    }

    [Test]
    public void TestOverlayGetOverlayBaseThru()
    {
        Grod g = [];
        g.UseOverlay = false;
        g["k"] = "v";
        g.UseOverlay = true;
        Assert.That(g["k"], Is.EqualTo("v"));
    }

    [Test]
    public void TestOverlayGetOverlayValue()
    {
        Grod g = [];
        g.UseOverlay = false;
        g["k"] = "v";
        g.UseOverlay = true;
        g["k"] = "value";
        Assert.That(g["k"], Is.EqualTo("value"));
    }

    [Test]
    public void TestOverlayGetOverlayBackToBase()
    {
        Grod g = [];
        g.UseOverlay = false;
        g["k"] = "v";
        g.UseOverlay = true;
        g["k"] = "value";
        g.UseOverlay = false;
        Assert.That(g["k"], Is.EqualTo("v"));
    }

    [Test]
    public void TestGetKeys()
    {
        Grod g = [];
        g.UseOverlay = false;
        g["a"] = "1";
        g["b"] = "2";
        g["c"] = "3";
        var answer = "";
        foreach (string s in g.Keys)
        {
            answer += s;
            answer += g[s];
        }
        // test separately, order is not guaranteed
        Assert.That(answer.Contains("a1") && answer.Contains("b2") && answer.Contains("c3"), Is.True);
    }

    [Test]
    public void TestGetKeysBaseAndOverlay()
    {
        Grod g = [];
        g.UseOverlay = false;
        g["a"] = "1";
        g["b"] = "2";
        g.UseOverlay = true;
        g["c"] = "3";
        var answer = "";
        foreach (string s in g.Keys)
        {
            answer += s;
            answer += g[s];
        }
        // test separately, order is not guaranteed
        Assert.That(answer.Contains("a1") && answer.Contains("b2") && answer.Contains("c3"), Is.True);
    }

    [Test]
    public void TestRemoveKey()
    {
        Grod g = [];
        g.UseOverlay = false;
        g["a"] = "1";
        g["b"] = "2";
        g["c"] = "3";
        g.Remove("b");
        var answer = "";
        foreach (string s in g.Keys)
        {
            answer += s;
            answer += g[s];
        }
        // test separately, order is not guaranteed
        Assert.That(answer.Contains("a1") && !answer.Contains("b2") && answer.Contains("c3"), Is.True);
    }

    [Test]
    public void TestContainsKey()
    {
        Grod g = [];
        g.UseOverlay = false;
        g["a"] = "1";
        g["b"] = "2";
        g["c"] = "3";
        Assert.That(g.ContainsKey("a") && g.ContainsKey("b") && g.ContainsKey("c"), Is.True);
    }

    [Test]
    public void TestContainsKeyValuePair()
    {
        Grod g = [];
        g.UseOverlay = false;
        var kv = new KeyValuePair<string, string>("a", "1");
        g.Add(kv);
#pragma warning disable NUnit2014 // Use SomeItemsConstraint for better assertion messages in case of failure
        Assert.That(g.Contains(kv), Is.True);
#pragma warning restore NUnit2014 // Use SomeItemsConstraint for better assertion messages in case of failure
    }

    [Test]
    public void TestContainsKeyAfterRemove()
    {
        Grod g = [];
        g.UseOverlay = false;
        g["a"] = "1";
        g["b"] = "2";
        g["c"] = "3";
        g.Remove("b");
        Assert.That(g.ContainsKey("a") && !g.ContainsKey("b") && g.ContainsKey("c"), Is.True);
    }

    [Test]
    public void TestContainsKeyAny()
    {
        Grod g = [];
        g.UseOverlay = false;
        g["a"] = "1";
        g.UseOverlay = true;
        g["b"] = "2";
        Assert.That(g.ContainsKey("a") && g.ContainsKey("b"), Is.True);
    }

    [Test]
    public void TestMissingKeyNotFound()
    {
        Grod g = [];
        g.UseOverlay = false;
        Assert.That(g.ContainsKey("a"), Is.False);
    }

    [Test]
    public void TestOverlayContainsKeyInBase()
    {
        Grod g = [];
        g.UseOverlay = false;
        g["a"] = "1";
        g.UseOverlay = true;
        Assert.That(g.ContainsKey("a"), Is.True);
    }

    [Test]
    public void TestModifyValue()
    {
        Grod g = [];
        g.UseOverlay = false;
        g["a"] = "1";
        g["a"] = "123456";
        var answer = g["a"];
        Assert.That(answer, Is.EqualTo("123456"));
    }

    [Test]
    public void TestClear()
    {
        Grod g = [];
        g.UseOverlay = false;
        g["a"] = "1";
        g["b"] = "2";
        g["c"] = "3";
        g.Clear();
        var answer = g.Count;
        Assert.That(answer, Is.Zero);
    }

    [Test]
    public void TestClearOnlyOverlay()
    {
        Grod g = [];
        g.UseOverlay = false;
        g["a"] = "1";
        g["b"] = "2";
        g["c"] = "3";
        g.UseOverlay = true;
        g["aaa"] = "111";
        g["bbb"] = "222";
        g["ccc"] = "333";
        var answerBeforeClear = g.Count;
        g.ClearOverlay();
        var answerAfterClearOverlay = g.Count;
        g.UseOverlay = false;
        var answerAfterClearBase = g.Count;
        Assert.That(answerBeforeClear == 6 && answerAfterClearOverlay == 3 & answerAfterClearBase == 3, Is.True);
    }

    [Test]
    public void TestClearOnlyBase()
    {
        Grod g = [];
        g.UseOverlay = false;
        g["a"] = "1";
        g["b"] = "2";
        g["c"] = "3";
        g.UseOverlay = true;
        g["aaa"] = "111";
        g["bbb"] = "222";
        g["ccc"] = "333";
        var answerBeforeClear = g.Count;
        g.UseOverlay = false;
        g.ClearBase();
        var answerAfterClearBase = g.Count;
        g.UseOverlay = true;
        var answerAfterClearOverlay = g.Count;
        Assert.That(answerBeforeClear == 6 && answerAfterClearBase == 0 && answerAfterClearOverlay == 3, Is.True);
    }

    [Test]
    public void TestKeys()
    {
        Grod g = [];
        g.UseOverlay = false;
        g["a"] = "1";
        g["b"] = "2";
        g["c"] = "3";
        var keys = g.Keys;
        Assert.That(keys.Contains("a") && keys.Contains("b") && keys.Contains("c"), Is.True);
    }

    [Test]
    public void TestKeysOverlay()
    {
        Grod g = [];
        g.UseOverlay = false;
        g["a"] = "1";
        g["b"] = "2";
        g["c"] = "3";
        g.UseOverlay = true;
        g["bbb"] = "222";
        var keys = g.KeysOverlay;
        Assert.That(!keys.Contains("a") && !keys.Contains("b") && !keys.Contains("c") && keys.Contains("bbb"), Is.True);
    }

    [Test]
    public void TestKeysCountMatchesValuesCountBase()
    {
        Grod g = [];
        g.UseOverlay = false;
        g["a"] = "1";
        g["b"] = "2";
        g["c"] = "3";
        Assert.That(g.Keys, Has.Count.EqualTo(g.Values.Count));
    }

    [Test]
    public void TestKeysCountMatchesValuesCountOverlay()
    {
        Grod g = [];
        g.UseOverlay = false;
        g["a"] = "1";
        g["b"] = "2";
        // no "c" in base
        g.UseOverlay = true;
        g["a"] = "111";
        g["b"] = "222";
        g["c"] = "333";
        Assert.That(g.Keys, Has.Count.EqualTo(g.Values.Count));
    }

    [Test]
    public void Test_Readme()
    {
        var helpText = Grod.ReadMe();
        Assert.That(helpText, !Is.EqualTo(null));
    }

    [Test]
    public void Test_License()
    {
        var helpText = Grod.License();
        Assert.That(helpText, !Is.EqualTo(null));
    }

    [Test]
    public void Test_VersionHistory()
    {
        var helpText = Grod.VersionHistory();
        Assert.That(helpText, !Is.EqualTo(null));
    }
}