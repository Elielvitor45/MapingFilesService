using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MapingFolders
{
    public class Folders
    {

        public string Path { get; set; }
        public List<FolderOB> folderOBs { get; set; } = new List<FolderOB>();
        public Folders(string path)
        {
            Path = @path;
            Path += @"\Folders.xml";
            bool controler = AddListFolderOB();
            if (!controler)
            {
                return;
            }
            else
            {
                StartMaping();
            }
        }
        public void StartMaping()
        {
            Maping maping = new Maping(folderOBs);
            maping.map();
        }
        public bool AddListFolderOB()
        {
            XmlNodeList xmlNodeList = getXmlNodeList();
            if (xmlNodeList == null)
            {
                return false;
            }
            else
            {
                List<XmlNode> listFolderXml = getListXmlFolder(xmlNodeList);
                folderOBs = getListObjFolderOB(listFolderXml);
                return true;
            }
        }
        public XmlNodeList getXmlNodeList()
        {
            XmlDocument xmlDocument = new XmlDocument();
            if (!File.Exists(Path))
            {
                return null;
            }
            xmlDocument.Load(Path);
            XmlNodeList NodeList = xmlDocument.SelectNodes("//Folders/*");
            return NodeList;
        }
        public List<XmlNode> getListXmlFolder(XmlNodeList folder)
        {
            List<XmlNode> nodeList = new List<XmlNode>();
            int cont = 0;
            foreach (XmlNode node in folder)
            {
                if (node.Name.StartsWith("Folder") && cont != 0)
                {
                    nodeList.Add(node);
                }
                cont++;
            }
            return nodeList;
        }
        public List<FolderOB> getListObjFolderOB(List<XmlNode> listFolders)
        {
            List<FolderOB> listObjFolderOB = new List<FolderOB>();
            foreach (var item in listFolders)
            {
                FolderOB objectFolder = new FolderOB(item);
                listObjFolderOB.Add(objectFolder);
            }
            return listObjFolderOB;
        }

    }
}
