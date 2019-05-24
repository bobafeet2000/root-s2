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
    public abstract class Screen
    {
        public String name { get; protected set; }
        public Color font_color_game;
        public Color font_color_boot;
        public int pos_X { get; protected set; }
        public int pos_Y { get; protected set; }
        public Background background0 { get; protected set; }
        public Background background1 { get; protected set; }
        public Background background2 { get; protected set; }
        public int timer { get; protected set; }

        public Screen()
        {
            // constructeur
        }

        public virtual void Update(float elapsetime)
        {
            // update
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // draw
        }
    }

    public class ScreenBoot : Screen
    {
        public SoundEffectInstance sound_boot_msg { get; protected set; }

        public ScreenBoot(String name)
        {
            this.name = name;
            pos_X = (int)(Constant.MAIN_WINDOW_WIDTH / 2 - Art.Font_Game.MeasureString(name).Length() / 2);
            pos_Y = Constant.MAIN_WINDOW_HEIGHT / 2;
            font_color_game = new Color(Constant.FONT_GAME_COLOR_R, Constant.FONT_GAME_COLOR_G, Constant.FONT_GAME_COLOR_B); // couleur de la font game
            font_color_boot = new Color(Constant.FONT_BOOT_COLOR_R, Constant.FONT_BOOT_COLOR_G, Constant.FONT_BOOT_COLOR_B); // couleur de la font boot msg
            sound_boot_msg = Art.Song_boot_msg.CreateInstance();
        }

        public override void Update(float elapsetime)
        {
            timer += (int)elapsetime;

            if ((timer >1000 && timer <1060) || (timer > 1200 && timer < 1260) || (timer > 1400 && timer < 1460) || (timer > 1600 && timer < 1660) || (timer > 1800 && timer < 1860) || (timer > 2000 && timer < 2060) || (timer > 2200 && timer < 2260)) sound_boot_msg.Play();

        }
        public override void Draw(SpriteBatch spriteBatch)
        {          
            if (timer<1000) spriteBatch.DrawString(Art.Font_Game, this.name, new Vector2(this.pos_X, this.pos_Y), font_color_game * Constant.FONT_GAME_COLOR_A); // Affichage du titre de l'écran
            if (timer >1000) spriteBatch.DrawString(Art.Font_Boot, Constant.GAME_BOOT_MSG[0], new Vector2(10, 100), font_color_boot * Constant.FONT_GAME_COLOR_A);
            if (timer > 1200) spriteBatch.DrawString(Art.Font_Boot, Constant.GAME_BOOT_MSG[1], new Vector2(10, 120), font_color_boot * Constant.FONT_GAME_COLOR_A);
            if (timer > 1400) spriteBatch.DrawString(Art.Font_Boot, Constant.GAME_BOOT_MSG[2], new Vector2(10, 140), font_color_boot * Constant.FONT_GAME_COLOR_A);
            if (timer > 1600) spriteBatch.DrawString(Art.Font_Boot, Constant.GAME_BOOT_MSG[3], new Vector2(10, 160), font_color_boot * Constant.FONT_GAME_COLOR_A);
            if (timer > 1800) spriteBatch.DrawString(Art.Font_Boot, Constant.GAME_BOOT_MSG[4], new Vector2(10, 180), font_color_boot * Constant.FONT_GAME_COLOR_A);
            if (timer > 2000) spriteBatch.DrawString(Art.Font_Boot, Constant.GAME_BOOT_MSG[5], new Vector2(10, 200), font_color_boot * Constant.FONT_GAME_COLOR_A);
            if (timer > 2200) spriteBatch.DrawString(Art.Font_Boot, Constant.GAME_BOOT_MSG[6], new Vector2(10, 220), font_color_boot * Constant.FONT_GAME_COLOR_A);

        }
    }

    public class ScreenHome : Screen
    {
        public String name_2 { get; protected set; }
        public int pos_X_2 { get; protected set; }
        public String name_3 { get; protected set; }
        public int pos_X_3 { get; protected set; }

        public ScreenHome(String name)
        {
            this.name = name;
            name_2 = "Press ENTER to start the game";
            name_3 = "Insert a Coin";
            pos_X = (int)(Constant.MAIN_WINDOW_WIDTH / 2 - Art.Font_Game.MeasureString(name).Length() / 2);
            pos_X_2 = (int)(Constant.MAIN_WINDOW_WIDTH / 2 - Art.Font_Boot.MeasureString(name_2).Length() / 2);
            pos_X_3 = (int)(Constant.MAIN_WINDOW_WIDTH / 2 - Art.Font_Boot.MeasureString(name_3).Length() / 2);
            pos_Y = Constant.MAIN_WINDOW_HEIGHT / 2;
            font_color_game = new Color(Constant.FONT_GAME_COLOR_R, Constant.FONT_GAME_COLOR_G, Constant.FONT_GAME_COLOR_B); // couleur de la font 

            background0 = new Background(0, 16);
            background1 = new Background(1, 8);
            background2 = new Background(2, 4);

        }

        public override void Update(float elapsetime)
        {
            background0.Update(elapsetime);
            background1.Update(elapsetime);
            background2.Update(elapsetime);

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            background0.Draw(spriteBatch);
            background1.Draw(spriteBatch);
            background2.Draw(spriteBatch);
            spriteBatch.DrawString(Art.Font_Game, this.name, new Vector2(this.pos_X, this.pos_Y), font_color_game * Constant.FONT_GAME_COLOR_A); // Affichage du titre de l'écran
            spriteBatch.DrawString(Art.Font_Boot, name_2 , new Vector2(this.pos_X_2, Constant.MAIN_WINDOW_HEIGHT -50), font_color_game * Constant.FONT_GAME_COLOR_A);
            spriteBatch.DrawString(Art.Font_Boot, name_3, new Vector2(this.pos_X_3, pos_Y+30), font_color_game * Constant.FONT_GAME_COLOR_A);
            spriteBatch.DrawString(Art.Font_Boot, "Press H key to display instructions", new Vector2(13, Constant.MAIN_WINDOW_HEIGHT-30), font_color_game * Constant.FONT_GAME_COLOR_A);
        }
    }

    public class ScreenInstruction : Screen
    {
        public String name_2 { get; protected set; }
        public String name_3 { get; protected set; }
        public String name_4 { get; protected set; }

        public int pos_X_2 { get; protected set; }
        public int pos_X_3 { get; protected set; }
        public int pos_X_4 { get; protected set; }
        public int pos_Y_2 { get; protected set; }
        public int pos_Y_3 { get; protected set; }
        public int pos_Y_4 { get; protected set; }

        public ScreenInstruction(String name)
        {
            this.name = name;
            name_2 = "Press arrow keys to move your vessel";
            name_3 = "Press CTRL key to shoot the enemies";
            name_4 = "Press ECHAP key to quit the game";
            pos_X = (int)(Constant.MAIN_WINDOW_WIDTH / 2 - Art.Font_Game.MeasureString(name).Length() / 2);
            pos_X_2 = (int)(Constant.MAIN_WINDOW_WIDTH / 2 - Art.Font_Boot.MeasureString(name_2).Length() / 2);
            pos_X_3 = (int)(Constant.MAIN_WINDOW_WIDTH / 2 - Art.Font_Boot.MeasureString(name_3).Length() / 2);
            pos_X_4 = (int)(Constant.MAIN_WINDOW_WIDTH / 2 - Art.Font_Boot.MeasureString(name_4).Length() / 2);
            pos_Y = Constant.MAIN_WINDOW_HEIGHT / 4;
            pos_Y_2 = Constant.MAIN_WINDOW_HEIGHT / 3;
            pos_Y_3 = Constant.MAIN_WINDOW_HEIGHT / 2;
            pos_Y_4 = pos_Y_2 * 2;
            font_color_game = new Color(Constant.FONT_GAME_COLOR_R, Constant.FONT_GAME_COLOR_G, Constant.FONT_GAME_COLOR_B); // couleur de la font 

            background0 = new Background(0, 16);
            background1 = new Background(1, 8);
            background2 = new Background(2, 4);

        }

        public override void Update(float elapsetime)
        {
            background0.Update(elapsetime);
            background1.Update(elapsetime);
            background2.Update(elapsetime);

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            background0.Draw(spriteBatch);
            background1.Draw(spriteBatch);
            background2.Draw(spriteBatch);
            spriteBatch.DrawString(Art.Font_Game, this.name, new Vector2(this.pos_X, this.pos_Y), font_color_game * Constant.FONT_GAME_COLOR_A); // Affichage du titre de l'écran
            spriteBatch.DrawString(Art.Font_Boot, name_2, new Vector2(this.pos_X_2, this.pos_Y_2), font_color_game * Constant.FONT_GAME_COLOR_A);
            spriteBatch.DrawString(Art.Font_Boot, name_3, new Vector2(this.pos_X_3, this.pos_Y_3), font_color_game * Constant.FONT_GAME_COLOR_A);
            spriteBatch.DrawString(Art.Font_Boot, name_4, new Vector2(this.pos_X_4, this.pos_Y_4), font_color_game * Constant.FONT_GAME_COLOR_A);
        }
    }

    public class ScreenText : Screen
    {
        public ScreenText(String name)
        {
            this.name = name;
            pos_X = (int)(Constant.MAIN_WINDOW_WIDTH / 2 - Art.Font_Game.MeasureString(name).Length() / 2);
            pos_Y = Constant.MAIN_WINDOW_HEIGHT / 2;
            font_color_game = new Color(Constant.FONT_GAME_COLOR_R, Constant.FONT_GAME_COLOR_G, Constant.FONT_GAME_COLOR_B); // couleur de la font 
        }

        public override void Update(float elapsetime)
        {

        }
        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.DrawString(Art.Font_Game, this.name, new Vector2(this.pos_X, this.pos_Y), font_color_game * Constant.FONT_GAME_COLOR_A); // Affichage du titre de l'écran

        }
    }

    public class ScreenOver : Screen
    {
        public String name_2 { get; protected set; }
        public String name_3 { get; protected set; }

        public int pos_X_2 { get; protected set; }
        public int pos_X_3 { get; protected set; }

        public ScreenOver(String name)
        {
            this.name = name;
            name_2 = "Press the ESC to return to the menu";
            name_3 = "Press the R to try again";
            pos_X = (int)(Constant.MAIN_WINDOW_WIDTH / 2 - Art.Font_Game.MeasureString(name).Length() / 2);
            pos_Y = Constant.MAIN_WINDOW_HEIGHT / 2;
            pos_X_2 = (int)(Constant.MAIN_WINDOW_WIDTH / 2 - Art.Font_Boot.MeasureString(name_2).Length() / 2);
            pos_X_3 = (int)(Constant.MAIN_WINDOW_WIDTH / 2 - Art.Font_Boot.MeasureString(name_3).Length() / 2);

            font_color_game = new Color(Constant.FONT_GAME_COLOR_R, Constant.FONT_GAME_COLOR_G, Constant.FONT_GAME_COLOR_B); // couleur de la font 
        }

        public override void Update(float elapsetime)
        {

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Art.Font_Game, this.name, new Vector2(this.pos_X, this.pos_Y), font_color_game * Constant.FONT_GAME_COLOR_A);// Affichage du titre de l'écran
            spriteBatch.DrawString(Art.Font_Boot, name_2, new Vector2(this.pos_X_2, Constant.MAIN_WINDOW_HEIGHT - 50), font_color_game * Constant.FONT_GAME_COLOR_A);
            spriteBatch.DrawString(Art.Font_Boot, name_3, new Vector2(this.pos_X_3, Constant.MAIN_WINDOW_HEIGHT - 30), font_color_game * Constant.FONT_GAME_COLOR_A);
        }
    }

    public class ScreenNext : Screen
    { 
        public ScreenNext(String name)
        {
            this.name = name;
            pos_X = (int)(Constant.MAIN_WINDOW_WIDTH / 2 - Art.Font_Game.MeasureString(name).Length() / 2);
            pos_Y = Constant.MAIN_WINDOW_HEIGHT / 2;

            font_color_game = new Color(Constant.FONT_BOOT_COLOR_R, Constant.FONT_BOOT_COLOR_G, Constant.FONT_BOOT_COLOR_B);
        }

        public override void Update(float elapsetime)
        {
           
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Art.Font_Game, this.name, new Vector2(this.pos_X, this.pos_Y), font_color_game * Constant.FONT_GAME_COLOR_A);// Affichage du titre de l'écran
        }
    }

    public class ScreenCredit : Screen
    {
        public String name_2 { get; protected set; }
        public String name_3 { get; protected set; }
        public String name_4 { get; protected set; }
        public String name_5 { get; protected set; }

        public int pos_X_2 { get; protected set; }
        public int pos_X_3 { get; protected set; }
        public int pos_X_4 { get; protected set; }
        public int pos_X_5 { get; protected set; }
        public int pos_Y_2 { get; protected set; }
        public int pos_Y_3 { get; protected set; }
        public int pos_Y_4 { get; protected set; }
        public int pos_Y_5 { get; protected set; }

        public ScreenCredit(String name)
        {
            this.name = name;
            name_2 = "Paul Murelli-Soullier Nasaboy";
            name_3 = "Marius Andre Boba";
            name_4 = "Theo Le Bever Belvedere";
            name_5 = "Ethan Orsolle-Tyberg Jewny Halliday";
            pos_X = (int)(Constant.MAIN_WINDOW_WIDTH / 2 - Art.Font_Game.MeasureString(name).Length() / 2);
            pos_X_2 = (int)(Constant.MAIN_WINDOW_WIDTH / 2 - Art.Font_Boot.MeasureString(name_2).Length() / 2);
            pos_X_3 = (int)(Constant.MAIN_WINDOW_WIDTH / 2 - Art.Font_Boot.MeasureString(name_3).Length() / 2);
            pos_X_4 = (int)(Constant.MAIN_WINDOW_WIDTH / 2 - Art.Font_Boot.MeasureString(name_4).Length() / 2);
            pos_X_5 = (int)(Constant.MAIN_WINDOW_WIDTH / 2 - Art.Font_Boot.MeasureString(name_5).Length() / 2);
            pos_Y = Constant.MAIN_WINDOW_HEIGHT / 4;
            pos_Y_2 = pos_Y + 50;
            pos_Y_3 = pos_Y_2 + 50;
            pos_Y_4 = pos_Y_3 + 50;
            pos_Y_5 = pos_Y_4 + 50;
            font_color_game = new Color(Constant.FONT_GAME_COLOR_R, Constant.FONT_GAME_COLOR_G, Constant.FONT_GAME_COLOR_B); // couleur de la font 

            background0 = new Background(0, 16);
            background1 = new Background(1, 8);
            background2 = new Background(2, 4);

        }

        public override void Update(float elapsetime)
        {
            background0.Update(elapsetime);
            background1.Update(elapsetime);
            background2.Update(elapsetime);

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            background0.Draw(spriteBatch);
            background1.Draw(spriteBatch);
            background2.Draw(spriteBatch);
            spriteBatch.DrawString(Art.Font_Game, this.name, new Vector2(this.pos_X, this.pos_Y), font_color_game * Constant.FONT_GAME_COLOR_A); // Affichage du titre de l'écran
            spriteBatch.DrawString(Art.Font_Boot, name_2, new Vector2(this.pos_X_2, this.pos_Y_2), font_color_game * Constant.FONT_GAME_COLOR_A);
            spriteBatch.DrawString(Art.Font_Boot, name_3, new Vector2(this.pos_X_3, this.pos_Y_3), font_color_game * Constant.FONT_GAME_COLOR_A);
            spriteBatch.DrawString(Art.Font_Boot, name_4, new Vector2(this.pos_X_4, this.pos_Y_4), font_color_game * Constant.FONT_GAME_COLOR_A);
            spriteBatch.DrawString(Art.Font_Boot, name_5, new Vector2(this.pos_X_5, this.pos_Y_5), font_color_game * Constant.FONT_GAME_COLOR_A);
        }
    }

    public class ScreenMenuMulti : Screen
    {
        public String name_2 { get; protected set; }
        public String name_3 { get; protected set; }
        public String name_4 { get; protected set; }
        public String name_5 { get; protected set; }

        public int pos_X_2 { get; protected set; }
        public int pos_X_4 { get; protected set; }
        public int pos_X_5 { get; protected set; }
        public int pos_Y_2 { get; protected set; }
        public int pos_Y_3 { get; protected set; }
        public int pos_Y_4 { get; protected set; }

        public int choice;

        public ScreenMenuMulti(String name)
        {
            this.name = name;
            name_2 = "Player 1 (HOST)";
            name_3 = "Player 2";
            name_4 = "Press 1 or 2 to select your player";
            name_5 = "Press ENTER to confirm";

            pos_X = (int)(Constant.MAIN_WINDOW_WIDTH / 2 - Art.Font_Game.MeasureString(name).Length() / 2);
            pos_X_2 = 10;
            pos_X_4 = (int)(Constant.MAIN_WINDOW_WIDTH / 2 - Art.Font_Boot.MeasureString(name_4).Length() / 2);
            pos_X_5 = (int)(Constant.MAIN_WINDOW_WIDTH / 2 - Art.Font_Boot.MeasureString(name_5).Length() / 2);
            pos_Y = 50;
            pos_Y_2 = Constant.MAIN_WINDOW_HEIGHT / 3;
            pos_Y_3 = Constant.MAIN_WINDOW_HEIGHT / 2;
            pos_Y_4 = Constant.MAIN_WINDOW_HEIGHT - 50;
            font_color_game = new Color(Constant.FONT_GAME_COLOR_R, Constant.FONT_GAME_COLOR_G, Constant.FONT_GAME_COLOR_B); // couleur de la font
            font_color_boot = new Color(Constant.FONT_BOOT_COLOR_R, Constant.FONT_BOOT_COLOR_G, Constant.FONT_BOOT_COLOR_B);

            background0 = new Background(0, 16);
            background1 = new Background(1, 8);
            background2 = new Background(2, 4);
        }

        public override void Update(float elapsetime)
        {
            background0.Update(elapsetime);
            background1.Update(elapsetime);
            background2.Update(elapsetime);
        }

        public void SetChoice(int choice)
        {
            this.choice = choice;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            background0.Draw(spriteBatch);
            background1.Draw(spriteBatch);
            background2.Draw(spriteBatch);
            spriteBatch.DrawString(Art.Font_Game, this.name, new Vector2(this.pos_X, this.pos_Y), font_color_game * Constant.FONT_GAME_COLOR_A); // Affichage du titre de l'écran
            switch (choice)
            {
                case 0:
                    spriteBatch.DrawString(Art.Font_Game, name_2, new Vector2(this.pos_X_2, this.pos_Y_2), font_color_game * Constant.FONT_GAME_COLOR_A);
                    spriteBatch.DrawString(Art.Font_Game, name_3, new Vector2(this.pos_X_2, this.pos_Y_3), font_color_game * Constant.FONT_GAME_COLOR_A);
                    break;
                case 1:
                    spriteBatch.DrawString(Art.Font_Game, name_2, new Vector2(this.pos_X_2, this.pos_Y_2), font_color_boot * Constant.FONT_GAME_COLOR_A);
                    spriteBatch.DrawString(Art.Font_Game, name_3, new Vector2(this.pos_X_2, this.pos_Y_3), font_color_game * Constant.FONT_GAME_COLOR_A);
                    break;
                case 2:
                    spriteBatch.DrawString(Art.Font_Game, name_2, new Vector2(this.pos_X_2, this.pos_Y_2), font_color_game * Constant.FONT_GAME_COLOR_A);
                    spriteBatch.DrawString(Art.Font_Game, name_3, new Vector2(this.pos_X_2, this.pos_Y_3), font_color_boot * Constant.FONT_GAME_COLOR_A);
                    break;
                default:
                    spriteBatch.DrawString(Art.Font_Game, name_2, new Vector2(this.pos_X_2, this.pos_Y_2), font_color_game * Constant.FONT_GAME_COLOR_A);
                    spriteBatch.DrawString(Art.Font_Game, name_3, new Vector2(this.pos_X_2, this.pos_Y_3), font_color_game * Constant.FONT_GAME_COLOR_A);
                    break;
            }
            spriteBatch.DrawString(Art.Font_Boot, name_5, new Vector2(this.pos_X_5, this.pos_Y_4-20), font_color_game * Constant.FONT_GAME_COLOR_A);
            spriteBatch.DrawString(Art.Font_Boot, name_4, new Vector2(this.pos_X_4, this.pos_Y_4), font_color_game * Constant.FONT_GAME_COLOR_A);
        }
    }

    public class ScreenMenuMulti2 : Screen
    {
        public String name_2 { get; protected set; }
        public String name_3 { get; protected set; }
        public String name_4 { get; protected set; }
        public String name_5 { get; protected set; }

        public int pos_X_2 { get; protected set; }
        public int pos_X_4 { get; protected set; }
        public int pos_X_5 { get; protected set; }
        public int pos_Y_2 { get; protected set; }
        public int pos_Y_3 { get; protected set; }
        public int pos_Y_4 { get; protected set; }

        public int choice;
        public bool connected;
        public int blink_text = 0;

        public ScreenMenuMulti2(String name,int choice, bool connected)
        {
            this.name = name;
            name_2 = "Player 1 (HOST)";
            name_3 = "Player 2";
            name_4 = "Press ENTER to start the game";
            name_5 = ". . .";

            this.connected = connected;
            this.choice = choice;

            pos_X = (int)(Constant.MAIN_WINDOW_WIDTH / 2 - Art.Font_Game.MeasureString(name).Length() / 2);
            pos_X_2 = 10;
            pos_X_4 = (int)(Constant.MAIN_WINDOW_WIDTH / 2 - Art.Font_Boot.MeasureString(name_4).Length() / 2);
            pos_X_5 = (int)(Constant.MAIN_WINDOW_WIDTH / 2 - Art.Font_Boot.MeasureString(name_5).Length() / 2);
            pos_Y = 50;
            pos_Y_2 = Constant.MAIN_WINDOW_HEIGHT / 3;
            pos_Y_3 = Constant.MAIN_WINDOW_HEIGHT / 2;
            pos_Y_4 = Constant.MAIN_WINDOW_HEIGHT - 50;
            font_color_game = new Color(Constant.FONT_GAME_COLOR_R, Constant.FONT_GAME_COLOR_G, Constant.FONT_GAME_COLOR_B); // couleur de la font
            font_color_boot = new Color(Constant.FONT_BOOT_COLOR_R, Constant.FONT_BOOT_COLOR_G, Constant.FONT_BOOT_COLOR_B);

            background0 = new Background(0, 16);
            background1 = new Background(1, 8);
            background2 = new Background(2, 4);
        }

        public override void Update(float elapsetime)
        {
            background0.Update(elapsetime);
            background1.Update(elapsetime);
            background2.Update(elapsetime);
            blink_text += (int)elapsetime;
        }

        public void SetConnected(bool connected)
        {
            this.connected = connected;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            background0.Draw(spriteBatch);
            background1.Draw(spriteBatch);
            background2.Draw(spriteBatch);
            spriteBatch.DrawString(Art.Font_Game, this.name, new Vector2(this.pos_X, this.pos_Y), font_color_game * Constant.FONT_GAME_COLOR_A); // Affichage du titre de l'écran
            switch (choice)
            {
                case 1:
                    if (connected==true)
                    {
                        spriteBatch.DrawString(Art.Font_Game, name_2, new Vector2(this.pos_X_2, this.pos_Y_2), font_color_boot * Constant.FONT_GAME_COLOR_A);
                        spriteBatch.DrawString(Art.Font_Game, name_3, new Vector2(this.pos_X_2, this.pos_Y_3), font_color_game * Constant.FONT_GAME_COLOR_A);
                    }
                    else
                    {
                        spriteBatch.DrawString(Art.Font_Game, name_2, new Vector2(this.pos_X_2, this.pos_Y_2), font_color_boot * Constant.FONT_GAME_COLOR_A);
                    }   
                    break;
                case 2:
                    if (connected==true)
                    {
                        spriteBatch.DrawString(Art.Font_Game, name_3, new Vector2(this.pos_X_2, this.pos_Y_2), font_color_game * Constant.FONT_GAME_COLOR_A);
                        spriteBatch.DrawString(Art.Font_Game, name_2, new Vector2(this.pos_X_2, this.pos_Y_3), font_color_boot * Constant.FONT_GAME_COLOR_A);
                    }
                    else
                    {
                        spriteBatch.DrawString(Art.Font_Game, name_3, new Vector2(this.pos_X_2, this.pos_Y_2), font_color_boot * Constant.FONT_GAME_COLOR_A);
                    }
                    break;
                default:
                    spriteBatch.DrawString(Art.Font_Game, name_2, new Vector2(this.pos_X_2, this.pos_Y_2), font_color_game * Constant.FONT_GAME_COLOR_A);
                    spriteBatch.DrawString(Art.Font_Game, name_3, new Vector2(this.pos_X_2, this.pos_Y_3), font_color_game * Constant.FONT_GAME_COLOR_A);
                    break;
            }
            if (!connected)
            {
                if ((blink_text < 600)) spriteBatch.DrawString(Art.Font_Game, this.name_5, new Vector2(10, this.pos_Y_3), font_color_game * Constant.FONT_GAME_COLOR_A);// Affichage du titre de l'écran;
                else if (blink_text > 1200) blink_text = 0;
            }
            spriteBatch.DrawString(Art.Font_Boot, name_4, new Vector2(this.pos_X_4, this.pos_Y_4), font_color_game * Constant.FONT_GAME_COLOR_A);
        }
    }

    public class ScreenScore : Screen
    {
        public String name_2 { get; protected set; }
        public String name_3 { get; protected set; }
        public String name_4 { get; protected set; }
        public String name_5 { get; protected set; }
        public String name_6 { get; protected set; }

        public int pos_X_2 { get; protected set; }

        public int pos_Y_2 { get; protected set; }
        public int pos_Y_3 { get; protected set; }
        public int pos_Y_4 { get; protected set; }
        public int pos_Y_5 { get; protected set; }
        public int pos_Y_6 { get; protected set; }

        public ScreenScore(String name, String name_2, String name_3, String name_4, String name_5, String name_6)
        {
            this.name = name;
            this.name_2 = name_2;
            this.name_3 = name_3;
            this.name_4 = name_4;
            this.name_5 = name_5;
            pos_X = (int)(Constant.MAIN_WINDOW_WIDTH / 2 - Art.Font_Game.MeasureString(name).Length() / 2);
            pos_X_2 = 10;
            pos_Y = Constant.MAIN_WINDOW_HEIGHT / 4;
            pos_Y_2 = pos_Y + 50;
            pos_Y_3 = pos_Y_2 + 50;
            pos_Y_4 = pos_Y_3 + 50;
            pos_Y_5 = pos_Y_4 + 50;
            pos_Y_5 = pos_Y_5 + 50;
            font_color_game = new Color(Constant.FONT_GAME_COLOR_R, Constant.FONT_GAME_COLOR_G, Constant.FONT_GAME_COLOR_B); // couleur de la font 

            background0 = new Background(0, 16);
            background1 = new Background(1, 8);
            background2 = new Background(2, 4);

        }

        public override void Update(float elapsetime)
        {
            background0.Update(elapsetime);
            background1.Update(elapsetime);
            background2.Update(elapsetime);

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            background0.Draw(spriteBatch);
            background1.Draw(spriteBatch);
            background2.Draw(spriteBatch);
            spriteBatch.DrawString(Art.Font_Game, this.name, new Vector2(this.pos_X, this.pos_Y), font_color_game * Constant.FONT_GAME_COLOR_A); // Affichage du titre de l'écran
            spriteBatch.DrawString(Art.Font_Boot, name_2, new Vector2(this.pos_X, this.pos_Y_2), font_color_game * Constant.FONT_GAME_COLOR_A);
            spriteBatch.DrawString(Art.Font_Boot, name_3, new Vector2(this.pos_X, this.pos_Y_3), font_color_game * Constant.FONT_GAME_COLOR_A);
            spriteBatch.DrawString(Art.Font_Boot, name_4, new Vector2(this.pos_X, this.pos_Y_4), font_color_game * Constant.FONT_GAME_COLOR_A);
            spriteBatch.DrawString(Art.Font_Boot, name_5, new Vector2(this.pos_X, this.pos_Y_5), font_color_game * Constant.FONT_GAME_COLOR_A);
            spriteBatch.DrawString(Art.Font_Boot, name_6, new Vector2(this.pos_X, this.pos_Y_6), font_color_game * Constant.FONT_GAME_COLOR_A);
        }
    }
}

