using System.Reflection;

namespace GROD;

public partial class Grod
{
    public static string ReadMe()
    {
        return GetResourceText("GROD.README.md");
    }

    public static string License()
    {
        return GetResourceText("GROD.LICENSE.md");
    }

    public static string VersionHistory()
    {
        return GetResourceText("GROD.VERSIONS.md");
    }

    #region Private

    private static string GetResourceText(string resourceName)
    {
        var result = "";
        try
        {
            var _assembly = Assembly.GetExecutingAssembly();
            if (_assembly != null)
            {
                var _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream(resourceName));
                result = _textStreamReader.ReadToEnd();
            }
            return result;
        }
        catch
        {
            return "";
        }
    }

    #endregion
}
