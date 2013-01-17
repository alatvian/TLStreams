using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace TLStreamApp3.Models
{
    public class TLStream
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }  //game
        public string status { get; set; }  //online?
        public string owner { get; set; }  //publisher
        public string featured { get; set; }
        public string race { get; set; }
        public int viewers { get; set; }
        public string rating { get; set; }
        public string channelId { get; set; }
        public string channelType { get; set; }
        public string channelTitle { get; set; }
        public string channelLink { get; set; }
        public string threadLink { get; set; }
        public string Self
        {
            get
            {
                return string.Format(CultureInfo.CurrentCulture,
                    "api/TLStreams/{0}", this.id);
            }
            set { }
        }
    }
}