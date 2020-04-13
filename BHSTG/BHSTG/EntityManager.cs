﻿using BHSTG.Product;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHSTG
{
    public class EntityManager
    {
        internal Player player;
        internal List<Entity> enemies = new List<Entity>();
        internal List<Bullet> enemyBullets = new List<Bullet>();
        internal List<Bullet> playerBullets = new List<Bullet>();

        //this will determine whether we should draw the player or not. helps in gameover
        internal bool drawPlayer = true;
        internal Scoreboard gameScoreboard = new Scoreboard();

        //property for drawPlayer
        public bool _drawPlayer
        {
            get { return drawPlayer; }
            set { drawPlayer = value; }
        }

        public void addEnemyBullets(List<Bullet> bullets)
        {
            enemyBullets.AddRange(bullets);
        }
        public void addPlayerBullets(List<Bullet> bullets)
        {
            playerBullets.AddRange(bullets);
        }
        public void addEnemy(Entity enemy)
        {
            enemies.Add(enemy);
        }

        public void addPlayer(Player nPlayer)
        {
            player = nPlayer;
        }

        public void removePlayer()
        {
            player = null;
        }

        //adding this getter to get the instance of player object
        public Player getPlayer()
        {
            return player;
        }

        public void checkDamage()
        {
            foreach (Entity enemy in enemies)
            {
                foreach (Bullet bullet in playerBullets)
                {
                    if (enemy.rectangle.Intersects(bullet.bulletRectangle)) 
                    {
                        //enemy hit
                        bullet.isActive = false;
                        enemy._health -= bullet.damage;
                        if (enemy._health <= 0)
                        {
                            enemy.isAlive = false;
                            gameScoreboard.playerScore += 25;
                            Console.WriteLine("Player Score: " + gameScoreboard.playerScore.ToString());
                        }
                    }
                }
            }
            if (player != null)
            {
                foreach (Bullet bullet in enemyBullets)
                {
                    if (player.rectangle.Intersects(bullet.bulletRectangle))
                    {
                        //Player hit
                        player._health -= bullet.damage;
                        bullet.isActive = false;
                        
                        if (player._health <= 0)
                        {
                            player.isAlive = false;

                        }
                    }
                        
                }
            }

        }
        public void MakeEntitiesShoot(GameTime gameTime){
            foreach (Entity entity in enemies) {
                addEnemyBullets(entity.Shoot(gameTime));
             
            }
            if (player != null)
            {
                addPlayerBullets(player.Shoot(gameTime));
            }
        }
        public void checkDeath()
        {
            enemies.RemoveAll(enemy => enemy.isDead());
            enemyBullets.RemoveAll(bullet => bullet.isDead());
            playerBullets.RemoveAll(bullet => bullet.isDead());
            //Console.WriteLine("Bullets: " + enemyBullets.Count() + "\n");
            

        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Entity entity in enemies)
            {
                entity.Draw(gameTime, spriteBatch);
            }
            if (player != null && drawPlayer)
            {
                player.Draw(gameTime, spriteBatch);
            }
            foreach (Bullet bullet in enemyBullets)
            {
                bullet.Draw(spriteBatch);
            }
            foreach (Bullet bullet in playerBullets)
            {
                bullet.Draw(spriteBatch);
            }

        }

        public void removeAllEnemies()
        {
            enemies.Clear();
        }

        public void removeAllBullets()
        {
            enemyBullets.Clear();
            playerBullets.Clear();
        }

        public void Update(GameTime gameTime)
        {
            if (player != null && drawPlayer)
            {
                player.Update(gameTime, enemies);
            }
            
            foreach (Entity entity in enemies)
            {
                entity.Update(gameTime);
            }
            MakeEntitiesShoot(gameTime);
            checkDamage();
            checkDeath();
            foreach (Bullet bullet in enemyBullets)
            {
                bullet.Update(gameTime);
            }
            foreach (Bullet bullet in playerBullets)
            {
                bullet.Update(gameTime);
            }
        }

    }
}
