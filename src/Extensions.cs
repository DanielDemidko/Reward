using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;


static class Extensions
{
    public static String GetDomainName(this String url)
    {
        // Порядок префиксов важен
        foreach (String i in new[] { "https://", "http://", "www." })
        {
            if (url.IndexOf(i) == 0)
            {
                url = url.Remove(0, i.Length);
            }
        }
        Int32 subdomain = url.IndexOf('/');
        return subdomain == -1 ? url : url.Remove(subdomain);
    }

    public static List<String> GetHtmlLinks(this String page, String domainName = null)
    {
        List<String> result = new List<String>();
        Regex reHref = new Regex(@"(?inx)
        <a \s [^>]*
            href \s* = \s*
                (?<q> ['""] )
                    (?<url> [^""]+ )
                \k<q>
        [^>]* >");
        foreach (Match i in reHref.Matches(page))
        {
            result.Add(i.Groups["url"].ToString());
        }
        return domainName == null ? result : new List<String>(result.Where(i => i.GetDomainName() == domainName));
    }
}

