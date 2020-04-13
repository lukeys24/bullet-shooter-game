using BHSTG.Product;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHSTG.Factory
{
    public class EntityFactory
    {
        EntityCreator factory;
        ContentManager content;
        public EntityFactory(ContentManager content)
        {
            this.content = content;
        }
        public Player getPlayer()
        {
            factory = new PlayerCreator(content);
            return (Player)factory.GetEntity();
        }
        public Grunt1 getGrunt1()
        {
            factory = new Grunt1Creator(content);
            return (Grunt1)factory.GetEntity();
        }
        public Grunt2 getGrunt2()
        {
            factory = new Grunt2Creator(content);
            return (Grunt2)factory.GetEntity();
        }
        public MidBoss getMidBoss()
        {
            factory = new MidBossCreator(content);
            return (MidBoss) factory.GetEntity();
        }

        public FinalBoss getFinalBoss()
        {
            factory = new FinalBossCreator(content);
            return (FinalBoss)factory.GetEntity();
        }
    }
}
