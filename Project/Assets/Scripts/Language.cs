using System;
using System.Collections;
using System.IO;
using System.Xml;
 
using UnityEngine;
 
public class Language
{
    private Hashtable Strings;
	private TextAsset text;

    public Language(string path, string language)
	{
		text = Resources.Load(path) as TextAsset;
		setLanguage(path, language);
    }
 
    public void setLanguage(string path, string language)
	{
        var xml = new XmlDocument();
		xml.LoadXml(text.text);
     	
        Strings = new Hashtable();
        var element = xml.DocumentElement[language];
        if (element != null)
		{
            var elemEnum = element.GetEnumerator();
            while (elemEnum.MoveNext())
			{
                var xmlItem = (XmlElement)elemEnum.Current;
                Strings.Add(xmlItem.GetAttribute("name"), xmlItem.InnerText);
            }
        }
		else
		{
            Debug.LogError("The specified language does not exist: " + language);
        }
    }
 
    public string getString(string name)
	{
        if (!Strings.ContainsKey(name))
		{
            Debug.LogError("The specified string does not exist: " + name);
         
            return "";
        }
 
        return (string)Strings[name];
    }
 
}
