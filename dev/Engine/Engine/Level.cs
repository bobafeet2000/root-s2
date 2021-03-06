﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Engine
{
    public class Level
    {
        public Player player { get; protected set; }
        public int nbtir { get; protected set; }
        public Enemy enemy1 { get; protected set; }
        public Enemy enemy2 { get; protected set; }
        public Enemy enemy3 { get; protected set; }
        public Enemy enemy4 { get; protected set; }
        public Enemy enemy1_1 { get; protected set; }
        public List<Enemy> enemies { get; protected set; }
        public List<Tir> tirs { get; protected set; }
        public SoundEffectInstance sound_explosion { get; protected set; }
        public SoundEffectInstance sound_tir { get; protected set; }    
        public int level_num;


        public enum LevelState
        {
            //Tous les états possibles de la partie
            Game,
        }
        LevelState CurrentLevelState = LevelState.Game;

        public enum EnemyType
        {
            enemy1,
            enemy2,
            enemy3,
            enemy4,
            enemy1_1,
        }
        public Level(int x)
        {
            level_num = x;
            player = new Player();
            enemies = new List<Enemy>();
            NewWave();
            /*
            // level de test
            enemy1_1 = new Enemy(Art.Texture_Enemy1_1, Constant.FRAME_ENEMY1_1, Constant.CYCLE_ENEMY1_1);
            enemy1_1.Setpos(200, 50);
            enemy1 = new Enemy(Art.Texture_Enemy1,Constant.FRAME_ENEMY1,Constant.CYCLE_ENEMY1);
            enemy1.Setpos(100,100);
            enemy2 = new Enemy(Art.Texture_Enemy2, Constant.FRAME_ENEMY2, Constant.CYCLE_ENEMY2);
            enemy2.Setpos(50, 150);
            enemy3 = new Enemy(Art.Texture_Enemy3, Constant.FRAME_ENEMY3, Constant.CYCLE_ENEMY3);
            enemy3.Setpos(150, 200);
            //enemy4 = new Enemy(Art.Texture_Enemy4, Constant.FRAME_ENEMY4, Constant.CYCLE_ENEMY4);
            //enemy4.Setpos(200, 250);
            enemies.Add(enemy1_1);
            enemies.Add(enemy1);
            enemies.Add(enemy2);
            enemies.Add(enemy3);
            //enemies.Add(enemy4);
            // fin test
            */
            tirs = new List<Tir>();
            nbtir = Constant.PLAYER_NBTIR;

            // A AJOUTER :
            // lecture du pattern level
            // liste des tirs enemy 


            //sound_explosion = Art.Song_explosion.CreateInstance(); // on charge le son sans le jouer         
           
        }

        public void collision_detection()
        {
            for (int i = tirs.Count-1; i>=0 ; i--)
            {
                for (int j = enemies.Count-1; j>=0 ; j--)
                {
                    if (tirs[i].rectangle.Intersects(enemies[j].rectangle))
                    {
                        enemies.Remove(enemies[j]);
                        tirs[i].Isdead();       // le tir est marqué comme dead pour être supprimé lors de l'update des tirs                  
                        sound_explosion = Art.Song_explosion.CreateInstance();// nouvelle instance sound_effect qui sera joué par dessus les précédentes
                        sound_explosion.Play();
                    }
                }
            }
        }

        static T RandomEnumValue<T>()
        {
            var v = Enum.GetValues(typeof(T));
            return (T)v.GetValue(new Random().Next(v.Length));
        }

        public void NewWave()
        {
            int NumberEnemy = new Random().Next(4, 4);
            EnemyType? PreviousEnemy = null;
            
            int PosX = 200;
            int PosY = 50;
            for (int j = 0; j < NumberEnemy; j++)
            {
                

                for (int i = 0; i < NumberEnemy; i++)
                {
                    EnemyType EnemyToAdd = RandomEnumValue<EnemyType>();
                    while (EnemyToAdd == PreviousEnemy)
                        EnemyToAdd = RandomEnumValue<EnemyType>();

                    //bool test1 = EnemyToAdd == EnemyType.enemy1_1;
                    if (EnemyToAdd == EnemyType.enemy1_1)
                    {
                        enemy1_1 = new Enemy(Art.Texture_Enemy1_1, Constant.FRAME_ENEMY1_1, Constant.CYCLE_ENEMY1_1);
                        enemy1_1.Setpos(PosX, PosY);
                        enemies.Add(enemy1_1);
                    }

                    //bool test2 = EnemyToAdd == EnemyType.enemy2;
                    if (EnemyToAdd == EnemyType.enemy2)
                    {
                        enemy2 = new Enemy(Art.Texture_Enemy2, Constant.FRAME_ENEMY2, Constant.CYCLE_ENEMY2);
                        enemy2.Setpos(PosX, PosY);
                        enemies.Add(enemy2);
                    }
                    //bool test3 = EnemyToAdd == EnemyType.enemy3;
                    if (EnemyToAdd == EnemyType.enemy3)
                    {
                        enemy3 = new Enemy(Art.Texture_Enemy3, Constant.FRAME_ENEMY3, Constant.CYCLE_ENEMY3);
                        enemy3.Setpos(PosX, PosY);
                        enemies.Add(enemy3);
                    }
                   // bool test4 = EnemyToAdd == EnemyType.enemy4;
                    if (EnemyToAdd == EnemyType.enemy4)
                    {
                        enemy4 = new Enemy(Art.Texture_Enemy4, Constant.FRAME_ENEMY4, Constant.CYCLE_ENEMY4);
                        enemy4.Setpos(PosX, PosY);
                        enemies.Add(enemy4);
                    }
                    //bool test0 = EnemyToAdd == EnemyType.enemy1;
                    if (EnemyToAdd == EnemyType.enemy1)
                    {

                        enemy1 = new Enemy(Art.Texture_Enemy1, Constant.FRAME_ENEMY1, Constant.CYCLE_ENEMY1);
                        enemy1.Setpos(PosX, PosY);
                        enemies.Add(enemy1);

                    }

                    PosX += 50;  // à normaliser
                    PreviousEnemy = EnemyToAdd;
                }
                PosY += 50;
            }

        }
        public void End() 
        {
            sound_explosion.Stop();
            sound_tir.Stop();
        }

        public void Update(float elapsetime)
        {
            switch (CurrentLevelState)
            {
                case LevelState.Game:

                    collision_detection();
                    // level de test
                    //enemy1.Update(elapsetime);      
                    //enemy2.Update(elapsetime);
                    //enemy3.Setrotation(enemy3.rotation + 0.1f);
                    //enemy3.Update(elapsetime);
                    //enemy4.Update(elapsetime);
                    //enemy1_1.Update(elapsetime);
                    // fin test

                    foreach (var e in enemies)
                    {
                        if (e == enemy3)
                        {
                            e.Setrotation(enemy3.rotation + 0.1f);
                        }
                        e.Update(elapsetime);
                    }

                    if (Input.KeyPressed(Keys.LeftControl))
                    {
                        if (tirs.Count() < nbtir)
                        {
                            tirs.Add(new Tir(player.pos_X + player.sprite.Texture.Width / 2 - 1, player.pos_Y));
                            sound_tir = Art.Song_tir.CreateInstance(); // nouvelle instance sound_effect qui sera joué par dessus les précédentes
                            sound_tir.Play();
                        }
                    }

                    if (tirs.Count() > 0) for (int i = 0; i < tirs.Count(); i++)
                        {
                            tirs[i].Update(elapsetime);
                            if ((tirs[i].pos_Y < 0) || (tirs[i].dead))
                            {
                                tirs.Remove(tirs[i]);
                            }
                        }

                    // update de la liste des tirs enemy


                    // update des detections collision enemy (parcours des listes tir enemy par rapport à la position player)

                    // update des detections collision player (parcours des listes tir et de la liste des enemy)
    
                    player.Update(elapsetime); // update du player 

                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (CurrentLevelState)
            {
                case LevelState.Game:

                    // level de test 
                    //enemy1.Draw(spriteBatch);
                    //enemy2.Draw(spriteBatch);
                    //enemy3.Draw(spriteBatch);
                    //enemy4.Draw(spriteBatch);
                    //enemy1_1.Draw(spriteBatch);
                    // fin test

                    foreach (var e in enemies)
                    {
                        e.Draw(spriteBatch);
                    }

                    if (tirs.Count() > 0) for (int i = 0; i < tirs.Count(); i++) tirs[i].Draw(spriteBatch); // draw de la liste des tirs player

                    // draw de la liste des tirs enemy



                    player.Draw(spriteBatch); // draw du player

                    break;
            }
        }
    }
}
