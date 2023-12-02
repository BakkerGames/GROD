using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace GrodLibrary;

/// <summary>
/// GROD - Game Resource Overlay Dictionary
/// </summary>
public class Grod : IDictionary<string, string>
{
    private readonly Dictionary<string, string> _base = [];
    private readonly Dictionary<string, string> _overlay = [];

    public bool UseOverlay { get; set; } = false;

    public string this[string key]
    {
        get
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            if (UseOverlay && _overlay.TryGetValue(key, out string? value1))
                return value1 ?? "";
            else if (_base.TryGetValue(key, out string? value2))
                return value2 ?? "";
            else
                return "";
        }
        set
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            //value ??= "";
            if (UseOverlay)
            {
                if (!_overlay.TryAdd(key, value))
                    _overlay[key] = value;
            }
            else
            {
                if (!_base.TryAdd(key, value))
                    _base[key] = value;
            }
        }
    }

    public ICollection<string> Keys =>
        UseOverlay ? _base.Keys.Union(_overlay.Keys).ToList() : _base.Keys;

    public ICollection<string> KeysOverlay =>
        _overlay.Keys;

    public ICollection<string> Values =>
        UseOverlay ? FlattenValues() : _base.Values;

    private List<string> FlattenValues()
    {
        List<string> result = [];
        foreach (string key in Keys)
            result.Add(this[key]);
        return result;
    }

    public int Count =>
        (UseOverlay ? _base.Keys.Union(_overlay.Keys) : _base.Keys).Count();

    public bool IsReadOnly => false;

    public void Add(string key, string value)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentNullException(nameof(key));
        this[key] = value;
    }

    public void Add(KeyValuePair<string, string> item)
    {
        if (string.IsNullOrWhiteSpace(item.Key))
            throw new ArgumentNullException(nameof(item));
        this[item.Key] = item.Value;
    }

    public void Clear()
    {
        _overlay.Clear();
        _base.Clear();
    }

    public void ClearBase()
    {
        _base.Clear();
    }

    public void ClearOverlay()
    {
        _overlay.Clear();
    }

    public bool Contains(KeyValuePair<string, string> item)
    {
        if (string.IsNullOrWhiteSpace(item.Key))
            throw new ArgumentNullException(nameof(item));
        return _base.Contains(item) || (UseOverlay && _overlay.Contains(item));
    }

    public bool ContainsKey(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentNullException(nameof(key));
        return _base.ContainsKey(key) || (UseOverlay && _overlay.ContainsKey(key));
    }

    public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
    {
        var i = 0;
        foreach (string key in Keys)
        {
            array[i + arrayIndex] = new KeyValuePair<string, string>(key, this[key]);
            i++;
        }
    }

    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
    {
        List<KeyValuePair<string, string>> result = [];
        foreach (string key in Keys)
            result.Add(new KeyValuePair<string, string>(key, this[key]));
        return (IEnumerator<KeyValuePair<string, string>>)result;
    }

    public bool Remove(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentNullException(nameof(key));
        bool result = _base.Remove(key);
        if (UseOverlay)
            result |= _overlay.Remove(key);
        return result;
    }

    public bool Remove(KeyValuePair<string, string> item)
    {
        if (string.IsNullOrWhiteSpace(item.Key))
            throw new ArgumentNullException(nameof(item));
        bool result = _base.Contains(item) && _base.Remove(item.Key);
        if (UseOverlay)
            result |= _overlay.Contains(item) && _overlay.Remove(item.Key);
        return result;
    }

    public bool TryGetValue(string key, [MaybeNullWhen(false)] out string value)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentNullException(nameof(key));
        if (UseOverlay && _overlay.ContainsKey(key))
            return _overlay.TryGetValue(key, out value);
        else
            return _base.TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return (IEnumerator)(UseOverlay ? _base.Union(_overlay) : _base);
    }
}