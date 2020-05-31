using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics.CodeAnalysis;

namespace tictactoe
{
    class Board
    {
        public static Square[] squares = new Square[9];
        public static bool xWin = false, oWin = false;
        public static string turn = "X";
        protected Texture2D boardSprite, xSprite, oSprite;
        protected SpriteFont gameFont;
        protected bool isX = false, isO = false;
        protected Vector2 position;
        public Vector2 Position { get { return position; } set { this.position = value; } }

        public void DrawBoard(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(boardSprite, new Vector2(0, 0), Color.White);
        }

        public void UpdateBoard()
        {
            MouseState m = Mouse.GetState();
            foreach(Square s in Square.squares)
            {
                if (!CheckWin())
                {
                    // Check for collision against the mouse, and draws X if left mouse is pressed or O if right mouse is pressed.

                    // Gets the distance between the mouse and the position of each square. Also, adding 35 pixels to adjust for the actual size of the square in the board.
                    float distance = Vector2.Distance(new Vector2(s.position.X + 35, s.position.Y + 35), new Vector2(m.X, m.Y));
                    if (!s.isX && !s.isO) //Making sure that the square isn't marked yet.
                    {
                        // If the distance between the mouse and the square is less than the radius of the square, mark it.
                        if (distance < 42 && m.LeftButton == ButtonState.Pressed && turn == "X")
                        {
                            s.isX = true;
                            s.isO = false;
                            turn = "O";
                        }else if(distance < 42 && m.LeftButton == ButtonState.Pressed && turn == "O")
                        {
                            s.isO = true;
                            s.isX = false;
                            turn = "X";
                        }
                        Console.WriteLine(turn);
                    }
                }
            }
        }

        public bool CheckWin()
        {
            if ((Board.squares[0].isX && Board.squares[1].isX && Board.squares[2].isX) ||
                (Board.squares[3].isX && Board.squares[4].isX && Board.squares[5].isX) ||
                (Board.squares[6].isX && Board.squares[7].isX && Board.squares[8].isX) ||
                (Board.squares[0].isX && Board.squares[4].isX && Board.squares[8].isX) ||
                (Board.squares[2].isX && Board.squares[4].isX && Board.squares[6].isX) ||
                (Board.squares[0].isX && Board.squares[3].isX && Board.squares[6].isX) ||
                (Board.squares[1].isX && Board.squares[4].isX && Board.squares[7].isX) ||
                (Board.squares[2].isX && Board.squares[5].isX && Board.squares[8].isX))
            {
                xWin = true;
                return true;
            }
            if ((Board.squares[0].isO && Board.squares[1].isO && Board.squares[2].isO) ||
                 (Board.squares[3].isO && Board.squares[4].isO && Board.squares[5].isO) ||
                 (Board.squares[6].isO && Board.squares[7].isO && Board.squares[8].isO) ||
                 (Board.squares[0].isO && Board.squares[4].isO && Board.squares[8].isO) ||
                 (Board.squares[2].isO && Board.squares[4].isO && Board.squares[6].isO) ||
                 (Board.squares[0].isO && Board.squares[3].isO && Board.squares[6].isO) ||
                 (Board.squares[1].isO && Board.squares[4].isO && Board.squares[7].isO) ||
                 (Board.squares[2].isO && Board.squares[5].isO && Board.squares[8].isO))
            {
                oWin = true;
                return true;
            }
            return false;
        }
        public void LoadBoard(ContentManager content)
        {
            boardSprite = content.Load<Texture2D>("Board");
            foreach (Square s in Board.squares)
            {
                s.xSprite = content.Load<Texture2D>("X");
                s.oSprite = content.Load<Texture2D>("O");
                s.gameFont = content.Load<SpriteFont>("GameFont");
            }
        }
    }

    class Square : Board
    {
        public Square(Vector2 position)
        {
            this.Position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isX)
            {
                spriteBatch.Draw(xSprite, this.position, Color.White);
            }
            if (isO)
            {
                spriteBatch.Draw(oSprite, this.position, Color.White);
            }
            if (xWin)
            {
                spriteBatch.DrawString(gameFont, "X Wins!", new Vector2(35, 100), Color.Red);
            }
            if (oWin)
            {
                spriteBatch.DrawString(gameFont, "O Wins!", new Vector2(35, 100), Color.Red);
            }
        }
    }
}