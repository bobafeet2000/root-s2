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
        public List<Tirenemy> tirsenemy { get; protected set; }
        public SoundEffectInstance sound_explosion { get; protected set; }
        public SoundEffectInstance sound_death { get; protected set; }
        public SoundEffectInstance sound_tir { get; protected set; }
        public SoundEffectInstance sound_tir_enemy { get; protected set; }
        public ScreenOver screen_over { get; protected set; }
        public int level_num;
        public int PERCENTAGE_SHOT = 200;
        public int PLAYER_LIVES = 3; //TODO: INTEGRER LE SYSTEME DE VIE
        public int blink_text = 0;


        public enum LevelState
        {
            //Tous les états possibles de la partie
            Game, Over,
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
            tirsenemy = new List<Tirenemy>();

            tirs = new List<Tir>();
            nbtir = Constant.PLAYER_NBTIR;

            // A AJOUTER : lecture du pattern level          
        }

        public void collision_detection()
        {
            if (tirs.Count > 0)
            {
                for (int i = tirs.Count - 1; i >= 0; i--)
                {
                    for (int j = enemies.Count - 1; j >= 0; j--)
                    {
                        if (tirs[i].rectangle.Intersects(enemies[j].rectangle))
                        {
                            enemies.Remove(enemies[j]);
                            tirs[i].Isdead();       // le tir est marqué comme dead pour être supprimé lors de l'update des tirs                  
                            sound_death = Art.Song_death.CreateInstance();// nouvelle instance sound_effect qui sera joué par dessus les précédentes
                            sound_death.Play();
                        }
                    }
                }
            }
            if(tirsenemy.Count > 0)
            {
                for (int i = tirsenemy.Count() - 1; i >= 0; i--)
                {
                    if (tirsenemy[i].rectangle.Intersects(player.rectangle))
                    {
                        tirsenemy.Remove(tirsenemy[i]);
                        PLAYER_LIVES -= 1;
                        sound_explosion = Art.Song_explosion.CreateInstance();
                        sound_explosion.Play();
                    }
                }
            }

        // TODO : Détection colision entre un enemy et le player (colision vaisseau)
            
        }

        public void ReturnFire()
        {
            Random random = new Random();
            double distance = Int32.MaxValue;
            Enemy EnemyClosestToPlayer = null;
            foreach (Enemy enemy in enemies)
            {
                if (Math.Pow(enemy.pos_X - player.pos_X, 2) + Math.Pow(enemy.pos_Y - player.pos_Y, 2) < distance)
                {
                    EnemyClosestToPlayer = enemy;
                    distance = Math.Pow(enemy.pos_X - player.pos_X, 2) + Math.Pow(enemy.pos_Y - player.pos_Y, 2);
                }
            }
            if (random.Next(0, PERCENTAGE_SHOT) == 0 && enemies.Count() > 0)
            {
                Tirenemy tirenemy = new Tirenemy(EnemyClosestToPlayer.pos_X, EnemyClosestToPlayer.pos_Y, "down");
                tirsenemy.Add(tirenemy);
                sound_tir_enemy = Art.Song_tir_enemy.CreateInstance();
                sound_tir_enemy.Play();
            }
        }
        static T RandomEnumValue<T>()
        {
            var v = Enum.GetValues(typeof(T));
            return (T)v.GetValue(new Random().Next(v.Length));
        }

        public void NewWave()
        {
            int NumberEnemy = new Random().Next(2) + level_num;
            EnemyType? PreviousEnemy = null;
            int PosX = 50;
            int PosY = 50;

            for (int j = 0; j < NumberEnemy; j++)
            {

                for (int i = 0; i < NumberEnemy; i++)
                {
                    EnemyType EnemyToAdd = RandomEnumValue<EnemyType>();
                    while (EnemyToAdd == PreviousEnemy)
                        EnemyToAdd = RandomEnumValue<EnemyType>();

                    if (EnemyToAdd == EnemyType.enemy1_1)
                    {
                        enemy1_1 = new Enemy(Art.Texture_Enemy1_1, Constant.FRAME_ENEMY1_1, Constant.CYCLE_ENEMY1_1);
                        enemy1_1.Setpos(PosX, PosY);
                        enemies.Add(enemy1_1);
                    }

                    if (EnemyToAdd == EnemyType.enemy2)
                    {
                        enemy2 = new Enemy(Art.Texture_Enemy2, Constant.FRAME_ENEMY2, Constant.CYCLE_ENEMY2);
                        enemy2.Setpos(PosX, PosY);
                        enemies.Add(enemy2);
                    }

                    if (EnemyToAdd == EnemyType.enemy3)
                    {
                        enemy3 = new Enemy(Art.Texture_Enemy3, Constant.FRAME_ENEMY3, Constant.CYCLE_ENEMY3);
                        enemy3.Setpos(PosX, PosY);
                        enemies.Add(enemy3);
                    }

                    if (EnemyToAdd == EnemyType.enemy4)
                    {
                        enemy4 = new Enemy(Art.Texture_Enemy4, Constant.FRAME_ENEMY4, Constant.CYCLE_ENEMY4);
                        enemy4.Setpos(PosX, PosY);
                        enemies.Add(enemy4);
                    }

                    if (EnemyToAdd == EnemyType.enemy1)
                    {

                        enemy1 = new Enemy(Art.Texture_Enemy1, Constant.FRAME_ENEMY1, Constant.CYCLE_ENEMY1);
                        enemy1.Setpos(PosX, PosY);
                        enemies.Add(enemy1);

                    }
                    PreviousEnemy = EnemyToAdd;
                    PosX += 50;
                }

                PosX = 50;
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

                    if (!enemies.Any())
                    {
                        level_num += 1;
                        NewWave();
                        PERCENTAGE_SHOT /= 2; ;
                    }


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

                    ReturnFire();

                    // update de la liste des tirs enemy
                    if (tirsenemy.Any()) for (int i = 0; i < tirsenemy.Count(); i++)
                        {
                            tirsenemy[i].Update(elapsetime);
                            if (tirsenemy[i].pos_Y > Constant.MAIN_WINDOW_HEIGHT)
                            {
                                tirsenemy.Remove(tirsenemy[i]);
                            }
                        }

                    player.Update(elapsetime); // update du player 

                    if (PLAYER_LIVES == 0)
                    {
                        CurrentLevelState = LevelState.Over;
                        screen_over = new ScreenOver(Constant.GAME_OVER);
                    }

                    break;

                case LevelState.Over :
                    blink_text += (int)elapsetime;
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (CurrentLevelState)
            {
                case LevelState.Game:

                    foreach (var e in enemies)
                    {
                        e.Draw(spriteBatch);
                    }

                    if (tirs.Count() > 0) for (int i = 0; i < tirs.Count(); i++) tirs[i].Draw(spriteBatch); // draw de la liste des tirs player
                    if (tirsenemy.Any()) for (int i = 0; i < tirsenemy.Count(); i++) tirsenemy[i].Draw(spriteBatch); // draw de la liste des tirs ennemy                                                                                                        

                    // Affichage nombre de vie
                    string name = $"Lives : {PLAYER_LIVES}";
                    int pos_X = (10);
                    int pos_Y = Constant.MAIN_WINDOW_HEIGHT - 20;
                    Color font_color_game_small = new Color(Constant.FONT_GAME_SMALL_COLOR_R, Constant.FONT_GAME_SMALL_COLOR_G, Constant.FONT_GAME_SMALL_COLOR_B);
                    spriteBatch.DrawString(Art.Font_Game_small, name, new Vector2(pos_X, pos_Y), font_color_game_small * Constant.FONT_GAME_SMALL_COLOR_A);
                    player.Draw(spriteBatch); // draw du player

                    break;

                case LevelState.Over:
                    if ((blink_text < 600)) screen_over.Draw(spriteBatch);
                    else if (blink_text > 1200) blink_text = 0;

                    break;
            }
        }
    }
}
