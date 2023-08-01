using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MapingFolders
{
    public class FolderOB
    {
        public FolderOB(XmlNode folder)
        {
            FolderPath = folder.SelectSingleNode("Target")?.InnerText;
        }
        public string FolderPath { get; set; }



    }
}
