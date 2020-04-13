using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BHSTG
{
    public class ScriptReader
    {
        public int level { get; set; }
        public int startTimer { get; set; }
        public int endTimer { get; set; }
        public int xStart { get; set; }
        public int yStart { get; set; }
        public int xEnd { get; set; }
        public int yEnd { get; set; }
        public string enemySprite { get; set; }
        public string enemyBullets { get; set; }
        public string enemyMovementType { get; set; }
        public string enemyShootType { get; set; }

        public ScriptReader()
        {
            //LoadScriptInfo();
        }
        
        public void LoadScriptInfo()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("BHSGScript.xml");

            foreach(XmlNode node in doc.DocumentElement)
            {
                level = int.Parse(node["level"].Value);
                startTimer = int.Parse(node["startTime"].Value);
                endTimer = int.Parse(node["endTime"].Value);
                enemySprite = node.Attributes[0].Value;
                
            }
        }
    }
}
