using UnityEngine;

public static class LanguageEncrypter
{
    public static int languageLevel = 2;

    public static string TranslateText(string text, int levelRequirement)
    {
        if (levelRequirement > languageLevel)
        {
            return EncryptText(text);
        }
        else
        {
            return text;
        }
    }

    private static string EncryptText(string text)
    {
        char[] charArray = text.ToCharArray();
        for (int i = 0; i < charArray.Length; i++)
        {
            charArray[i] = (char)(charArray[i] + 1);
        }
        return new string(charArray);
    }
}
