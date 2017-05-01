using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

class RandomBrowser
{
    private readonly WebClient Downoader = new WebClient();
    private readonly Random Randomizer = new Random();

    public readonly String DomainName;
    public readonly String DomainPage;
    public String NextUrl { private set; get; }

    public void Request()
    {
        try
        {
            List<String> links = Downoader.DownloadString(NextUrl).GetHtmlLinks(DomainName);
            NextUrl = links.Count > 0 ? links[Randomizer.Next(links.Count)] : DomainPage;
        }
        catch (Exception)
        {
            NextUrl = DomainPage;
        }
    }

    public RandomBrowser(String startPage)
    {
        DomainName = startPage?.GetDomainName();
        DomainPage = @"http://www." + DomainName;
        NextUrl = startPage;
    }
}