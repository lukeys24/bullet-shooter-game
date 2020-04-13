using BHSTG.Factory;
using BHSTG.Product;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Xna.Framework.Media;

namespace BHSTG
{
    public class GameManager
    {
        //
        //Spawning has to do with this class
        //
        //this saves a reference to Content
        ContentManager tempContent;
 
        //variables to store music
        private Song Lvl1to3;
        private Song FinalBoss;

        //game timer
        System.Timers.Timer aTimer = new System.Timers.Timer(); // init timer
        static int timerSeconds = 0; // keep track of the seconds

        int curLevel = 1;
        int lastSpawn = 0;
        bool createdBoss = false;

        bool lvl1Started = false;
        bool lvl2Started = false;
        bool lvl3Started = false;
        bool lvl4Started = false;

        //this will keep track of if the game is over or not, this helps in enabeling timer in the update function
        bool gameOverStatus = false;

        int lvl4StartTime, lvl3StartTime, lvl2StartTime, lvl1StartTime = 0;
        


        EntityManager entityManager;

        EntityFactory factory;

        ScriptReader sr = new ScriptReader();
        Scoreboard gameScoreboard = new Scoreboard();
        

        //this function will reset the timer allowing us to restart the game
        public void resetGame()
        {
            //turining off the timer
            aTimer.Enabled = false;
            timerSeconds = 0;
            curLevel = 1;
            lastSpawn = 0;
            lvl4StartTime = lvl3StartTime = lvl2StartTime = lvl1StartTime = 0;
            lvl1Started = false;
            lvl2Started = false;
            lvl3Started = false;
            lvl4Started = false;
            createdBoss = false;
            entityManager.removeAllBullets();
            entityManager.removeAllEnemies();
            gameOverStatus = false;
            //reset everything on the player
            entityManager.player._health = 100;
            entityManager.player.isAlive = true;
            entityManager._drawPlayer = true;
        }

        //this will pause the timer which will basically pause the game
        public void pauseGame()
        {
            aTimer.Enabled = false;
        }

        //this will resume the timer and resume the game
        public void resumeGame()
        {
            aTimer.Enabled = true;
        }

        //this is event handler to increment time
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            timerSeconds++;
        }

        public GameManager(ContentManager content)
        {
            //making a copy of content manager so we can draw time string
            tempContent = content;
            //initializing timers
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 1000;

            //laod song
            
            FinalBoss = tempContent.Load<Song>("Music/FinalBoss");



            entityManager = new EntityManager();
            factory = new EntityFactory(content);

            entityManager.addPlayer(factory.getPlayer());

            //Level1();
            //Need creators here!

        }

        //Level1 spawn grunts
        public void Level1()
        {
            
            //checks to see if level 1 has already started then sets the lvl1StartTime to the current time on Timer
            if (!lvl1Started)
            {
                lvl1StartTime = sr.startTimer;
                lvl1StartTime = timerSeconds;
                lvl1Started = true; //wont go in here more than 1 time.
            }
            
            //Only has 4 lvl1 grunts spawned at a time. Will spawn more lvl1 grunts if at least 1 second has passed.
            if (entityManager.enemies.Count < 4 && (timerSeconds - lastSpawn >= 1))
            {
                entityManager.addEnemy(factory.getGrunt1());
                lastSpawn = timerSeconds;
            }

            //15 second duration for level 2
            if (timerSeconds - lvl1StartTime > 15)
            {
                entityManager.removeAllEnemies();
                entityManager.removeAllBullets();
                curLevel = 2;
            }
        }


        //Level2 Spawn midboss
        public void Level2()
        {


            //checks to see if this is first time level2 is being called and stores the startTime from the timerSeconds.
            if (!lvl2Started)
            {
                lvl2StartTime = timerSeconds;
                lvl2Started = true;
            }

            //makes sure that the midboss is only created one time.
            if (!createdBoss)
            {
                entityManager.addEnemy(factory.getMidBoss());
                createdBoss = true;
            }

            //15 second duration for level 2 or when all enemies get killed
            if(timerSeconds - lvl2StartTime > 15 || entityManager.enemies.Count == 0)
            {
                entityManager.removeAllEnemies();
                entityManager.removeAllBullets();
                createdBoss = false;
                curLevel = 3;//moves on to level3
            }
        }

        //level3 spawn final
        public void Level3()
        {
            //checks to see if this is first time level3 is being called and stores the startTime from the timerSeconds.
            if (!lvl3Started)
            {
                lvl3StartTime = timerSeconds;
                lvl3Started = true;
            }

            //Only has 4 lvl2 grunts spawned at a time. Will spawn more lvl2 grunts if at least 1 second has passed.
            if (entityManager.enemies.Count < 4 && (timerSeconds - lastSpawn >= 1))
            {
                entityManager.addEnemy(factory.getGrunt2());
                lastSpawn = timerSeconds;
            }

            //15 second duration for level 3 or when all enemies get killed (only final boss)
            if (timerSeconds - lvl3StartTime > 15)
            {
                entityManager.removeAllEnemies();
                entityManager.removeAllBullets();
                curLevel = 4;
            }
        }

        //level4 spawn final
        public void Level4()
        {

            //checks to see if this is first time level3 is being called and stores the startTime from the timerSeconds.
            if (!lvl4Started)
            {
                lvl4StartTime = timerSeconds;
                lvl4Started = true;
            }

            //makes sure to only create 1 final boss
            if (!createdBoss)
            {
                entityManager.addEnemy(factory.getFinalBoss());
                createdBoss = true;
            }

            //15 second duration for level 4 or when all enemies get killed (only final boss)
            if (timerSeconds - lvl4StartTime > 15 || entityManager.enemies.Count == 0)
            {
                entityManager.removeAllEnemies();
                entityManager.removeAllBullets();

                createdBoss = false;
                curLevel = -1;
            }
        }

        public void GameOver(SpriteBatch spriteBatch)
        {
            //drawing game over sprite
            //spriteBatch.DrawString(tempContent.Load<SpriteFont>("Font/Font"), "Game Over ", new Vector2(500, 230), Color.White);
            spriteBatch.Draw(tempContent.Load<Texture2D>("Background/GameOver"), new Vector2(550, 250), Color.White);
            //Console.WriteLine("GameOver\n");
            entityManager.removeAllBullets();
            entityManager.removeAllEnemies();
            entityManager._drawPlayer = false;

            gameOverStatus = true;
            aTimer.Enabled = false;//stoping the timer


        }

        public void HealthMeter(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(tempContent.Load<SpriteFont>("Font/Font"), "Player Health : ", new Vector2(5, 10), Color.White);

            int iteratorTime = 0;
            int gap = 0;

            if (entityManager.getPlayer()._health >= 75)
                iteratorTime = 4;
            else if (entityManager.getPlayer()._health >= 50)
                iteratorTime = 3;
            else if (entityManager.getPlayer()._health >= 25)
                iteratorTime = 2;
            else if (entityManager.getPlayer()._health >= 0)
                iteratorTime = 1;
            else
                iteratorTime = 0;

            if(!gameOverStatus)
                for (int loop=0;loop<iteratorTime;loop++)
                {
                    spriteBatch.Draw(tempContent.Load<Texture2D>("Background/health"), new Rectangle(200 + gap, 7, 40, 40), Color.White);
                    gap += 40;
                }
                
        }

        public void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
           // Console.WriteLine("updating game manager !");
            spriteBatch.Begin();
            //this condition makes sure enabeling timer is done only once
            if (!aTimer.Enabled && gameOverStatus== false)
                aTimer.Enabled = true;

    
            if (curLevel == 1)
            {
                Level1();
            }
            else if (curLevel == 2)
            {
                Level2();
            }
            else if(curLevel == 3)
            {
                Level3();
            }
            else if(curLevel == 4)
            {
                Level4();
            }
            else
            {
                GameOver(spriteBatch);
            }

            entityManager.Update(gameTime);

            //this will update the timer 



            //this will draw the players health
            HealthMeter(spriteBatch);

            //this draws the game time
            spriteBatch.DrawString(tempContent.Load<SpriteFont>("Font/Font"), "Elapsed Time : " + timerSeconds.ToString(), new Vector2(1670, 10), Color.White);

            spriteBatch.DrawString(tempContent.Load<SpriteFont>("Font/Font"), "Player Score : " + gameScoreboard.playerScore.ToString(), new Vector2(1370, 10), Color.White);


            //check if game is over
            if (entityManager.player.isDead())
            {
                spriteBatch.DrawString(tempContent.Load<SpriteFont>("Font/Font"), "High Score: " + gameScoreboard.playerScore.ToString(), new Vector2(1370, 10), Color.White);
                GameOver(spriteBatch);
            }

            

            spriteBatch.End();
           // Console.WriteLine(timerSeconds);

        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            entityManager.Draw(gameTime, spriteBatch);
            //this will draw the timer 
            HealthMeter(spriteBatch);
            spriteBatch.DrawString(tempContent.Load<SpriteFont>("Font/Font"), "Elapsed Time : " + timerSeconds.ToString(), new Vector2(1670, 10), Color.White);
            spriteBatch.DrawString(tempContent.Load<SpriteFont>("Font/Font"), "Player Score : " + gameScoreboard.playerScore.ToString(), new Vector2(1370, 10), Color.White);


            //check if game is over
            if (entityManager.player.isDead())
            {
                //NEED TO FIX THE LOCATION OF WHERE THIS APPEARS ON THE SCREEN.
                spriteBatch.DrawString(tempContent.Load<SpriteFont>("Font/Font"), "High Score: " + gameScoreboard.playerScore.ToString(), new Vector2(1370, 10), Color.White);
                GameOver(spriteBatch);
            }
        }
    }



}
