using eZustellSendPOC.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eZustellSendPOC.About.Models
{
    public class AboutModel
    {
        public AboutModel()
        {

        }

        public List<OpenSourceListModel> OpenSourceListModel { get; private set; }

        public static AboutModel Load()
        {
            OpenSourceList os = new OpenSourceList();
            string osString = Resources.OpenSourceComponents;

            os = OpenSourceList.Deserialize(osString);

            AboutModel about = new AboutModel()
            {
                OpenSourceListModel = new List<OpenSourceListModel>()
            };
            foreach (OpenSourceListComponent item in os.Component)
            {
                
                about.OpenSourceListModel.Add(item);

            }
            return about;
        }

    }
    public class OpenSourceListModel
    {
        public string Name { get; set; }
        public string License { get; set; }
        public Uri Link { get; set; }

        public static implicit operator OpenSourceListModel(OpenSourceListComponent osLIst)
        {
            OpenSourceListModel model = new OpenSourceListModel()
            {
                Name = osLIst.Name,
                License = osLIst.License,
                Link = new Uri(osLIst.Link)
            };
            return model;
        }
    }
}
