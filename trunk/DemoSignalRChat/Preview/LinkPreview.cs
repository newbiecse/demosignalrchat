using NSoup.Nodes;
using NSoup.Select;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace DemoSignalRChat.Preview
{
    public class LinkPreview
    {
        string title = "";
        string description = "";
        string src = "";
        string url = "";

        public LinkPreview GetFirstLinkPreView(string message)
        {
            var firstUrl = Link.GetFirstLink(message);
            if (!string.IsNullOrEmpty(firstUrl))
            {
                url = firstUrl;

                using (var client = new WebClient())
                {
                    //client.DownloadData
                    client.Encoding = Encoding.UTF8;
                    string html = client.DownloadString(url);
                    Document doc = NSoup.NSoupClient.Parse(html);

                    // get title
                    this.title = doc.Select("title").First.Text();

                    // get description
                    if (doc.Select("meta[name=description]") != null)
                    {
                        if (doc.Select("meta[name=description]").First != null)
                        {
                            this.description = doc.Select("meta[name=description]").First.Attr("content");
                            if (string.IsNullOrEmpty(description))
                            {
                                this.description = title;
                            }
                        }
                    }

                    // get image
                    Elements imgs = doc.Select("img");
                    List<Img> images = new List<Img>();
                    foreach (var i in imgs)
                    {
                        images.Add(new Img(i.Attr("height"), i.Attr("width"), i.Attr("src")));
                    }
                    this.src = Img.GetSrcLargetestImage(images);

                    if (string.IsNullOrEmpty(src))
                    {
                        var EleIcons = doc.Select("link[rel=icon]");
                        if (EleIcons.Count > 0)
                        {
                            this.src = EleIcons.First.Attr("href");
                        }
                    }
                }
                return new LinkPreview { title = this.title, description = this.description, src = this.src, url = this.url };
            }
            return null;
        }
    }
}