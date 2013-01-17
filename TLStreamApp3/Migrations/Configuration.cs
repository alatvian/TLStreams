namespace TLStreamApp3.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TLStreamApp3.Models;
    using System.Web;
    using System.Net;
    using System.IO;
    using System.Xml;
    using System.Xml.Linq;
    using System.Collections.Generic;


    internal sealed class Configuration : DbMigrationsConfiguration<TLStreamApp3.Models.TLStreamApp3Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TLStreamApp3.Models.TLStreamApp3Context context)
        {
            Stream dataStream;
            StreamReader strReader;
            //   request.Credentials = CredentialCache.DefaultCredentials;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.teamliquid.net/video/streams/?xml=1&filter=live");
            request.UserAgent = "hi";
            request.ContentType = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.AutomaticDecompression = DecompressionMethods.GZip;
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

            dataStream = resp.GetResponseStream();
            strReader = new StreamReader(dataStream);
            string responseFromServer = strReader.ReadToEnd();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(responseFromServer);


            var streams = new List<TLStream>();

            XmlNode StreamListNode = doc.SelectSingleNode("/streamlist");
            XmlNodeList StreamNodeList = StreamListNode.SelectNodes("stream");
            foreach (XmlNode node in StreamNodeList)
            {
                TLStream aTLStream = new TLStream();

                try
                {
                    aTLStream.type = node.Attributes.GetNamedItem("type").Value;
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("No stream type");
                    System.Console.WriteLine(e.Message);
                }


                try
                {
                    aTLStream.status = node.Attributes.GetNamedItem("status").Value;
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("No stream status");
                    System.Console.WriteLine(e.Message);
                }
                try
                {
                    aTLStream.owner = node.Attributes.GetNamedItem("owner").Value;
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("No stream owner");
                    System.Console.WriteLine(e.Message);
                }
                try
                {
                    aTLStream.featured = node.Attributes.GetNamedItem("featured").Value;
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("No stream featured");
                    System.Console.WriteLine(e.Message);
                }
                try
                {
                    aTLStream.viewers = Convert.ToInt32(node.Attributes.GetNamedItem("viewers").Value);
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("No stream viewers");
                    System.Console.WriteLine(e.Message);
                }
                try
                {
                    aTLStream.race = node.Attributes.GetNamedItem("race").Value;
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("No stream race");
                    System.Console.WriteLine(e.Message);
                }
                try
                {
                    aTLStream.rating = node.Attributes.GetNamedItem("rating").Value;
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("No stream rating");
                    System.Console.WriteLine(e.Message);
                }
                foreach (XmlNode child in node.ChildNodes)
                {
                    if (child.Name == "channel")
                    {
                        aTLStream.channelType = node.ChildNodes[0].Attributes.GetNamedItem("type").Value;
                        aTLStream.channelTitle = node.ChildNodes[0].Attributes.GetNamedItem("title").Value;
                        aTLStream.channelId = node.ChildNodes[0].InnerText;

                    }
                    else if (child.Name == "link")
                    {
                        if (child.Attributes.GetNamedItem("type").Value == "embed")
                        {
                            aTLStream.channelLink = child.InnerText;
                        }
                        else if (child.Attributes.GetNamedItem("type").Value == "thread")
                        {
                            aTLStream.threadLink = child.InnerText;
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                    }
                }

                streams.Add(aTLStream);
            }//end of big foreach

            streams.ForEach(i => context.TLStreams.AddOrUpdate(p => p.name, new TLStream { name = i.name, type = i.type, status = i.status, owner = i.owner, featured = i.featured, race = i.race, viewers = i.viewers, rating = i.rating, channelId = i.channelId, channelType = i.channelType, channelTitle = i.channelTitle, channelLink = i.channelLink, threadLink = i.threadLink }));

        }
    }
}

