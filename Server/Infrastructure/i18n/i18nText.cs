using System.Collections;

namespace Touhou_Songs.Infrastructure.i18n;

public enum Lang { EN, VN };

public class I18nText
{
	private Dictionary<Lang, string> _dictionary = new();

	public I18nText(string defaultEnglishText, Dictionary<Lang, string>? dictionary = null)
	{
		if (dictionary is null)
		{
			_dictionary = new()
			{
				{ Lang.EN, defaultEnglishText }
			};
		}
		else
		{
			_dictionary = dictionary;
			_dictionary[Lang.EN] = defaultEnglishText;
		}
	}

	public I18nText(string englishText, string vietText = "NONE_PROVIDED")
	{
		_dictionary.Add(Lang.EN, englishText);
		_dictionary.Add(Lang.VN, vietText);
	}

	public string ToLanguage(Lang lang) => _dictionary[lang];
}

public class I18nTextWithParams
{
	private Dictionary<Lang, string> _dictionary = new();

	public I18nTextWithParams(string defaultEnglishText, Dictionary<Lang, string>? dictionary = null)
	{
		if (dictionary is null)
		{
			_dictionary = new()
			{
				{ Lang.EN, defaultEnglishText }
			};
		}
		else
		{
			_dictionary = dictionary;
			_dictionary[Lang.EN] = defaultEnglishText;
		}
	}

	public I18nTextWithParams(string englishText, string vietText = "NONE_PROVIDED")
	{
		_dictionary.Add(Lang.EN, englishText);
		_dictionary.Add(Lang.VN, vietText);
	}

	public string ToLanguage(Lang lang, object arg1, params object[] args)
	{
		var argsJoined = args.Select(arg =>
			arg is IEnumerable argAsIEnumerable and not string ?
				string.Join(", ", argAsIEnumerable.Cast<object>())
				: arg);

		return string.Format(_dictionary[lang], [arg1, .. argsJoined]);
	}
}
