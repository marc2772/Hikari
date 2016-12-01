using System;
using System.Collections;
using System.IO;
using System.Xml;
 
using UnityEngine;
 
public class Language
{
    private Hashtable Strings;
 
    /*
    Initialize Lang class
    path = path to XML resource example:  Path.Combine(Application.dataPath, "lang.xml")
    language = language to use example:  "English"
    web = boolean indicating if resource is local or on-line example:  true if on-line, false if local
    */
    public Language(string path, string language)
	{
		setLanguage(path, language);
    }
 
    /*
    Use the setLanguage function to swap languages after the Lang class has been initialized.
    This function is called automatically when the Lang class is initialized.
    path = path to XML resource example:  Path.Combine(Application.dataPath, "lang.xml")
    language = language to use example:  "English"
    */
    public void setLanguage(string path, string language)
	{
        var xml = new XmlDocument();
        xml.Load(path);
     	
        Strings = new Hashtable();
        var element = xml.DocumentElement[language];
        if (element != null)
		{
            var elemEnum = element.GetEnumerator();
            while (elemEnum.MoveNext()) {
                var xmlItem = (XmlElement)elemEnum.Current;
                Strings.Add(xmlItem.GetAttribute("name"), xmlItem.InnerText);
            }
        }
		else
		{
            Debug.LogError("The specified language does not exist: " + language);
        }
    }
 
    /*
    Access strings in the currently selected language by supplying this getString function with
    the name identifier for the string used in the XML resource.
    */
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
