using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Torpedo.Model;
using Torpedo.View;

namespace Torpedo.ViewModel
{
    public enum Player
    {
        FirstPlayer,
        SecondPlayer
    }
    public class BattleViewModel
    {
        Example example = new Example();
        private int _width;
        private int _height;
        public int[,] _firstPlayerRefShipMatrix;
        public int[,] _secondPlayerRefShipmatrix;

        public int pointFirstPlayer = 0;
        public int pointSecondPlayer = 0;

        public int rounds = 0;
        public int[] firstPlayerShipsCounts = { 1, 1, 1, 1, 1 };
        public int[] secondPlayerShipsCounts = { 1, 1, 1, 1, 1 };


        public BattleViewModel(int width, int height, int[,] firstArray, int[,] secondArray)
        {
            _width = width;
            _height = height;
            _firstPlayerRefShipMatrix = matrixTransformation(matrixTransformation((int[,]) Application.Current.Properties["1stPlayerArray"]));
            _secondPlayerRefShipmatrix = matrixTransformation(matrixTransformation((int[,])Application.Current.Properties["2ndPlayerArray"]));
        }
        public void guessTile(Canvas canvas, ref int[,] layout, ref int[] shipCounts,ref Player actualPlayer )
        {
            int x = Convert.ToInt32(GetPointOnCanvas(canvas).X);
            int y = Convert.ToInt32(GetPointOnCanvas(canvas).Y);
            Draw draw = new Draw(canvas, _width, _height);
            if (layout[x, y] != -1)
            {
                //ha nincs hajó a tileon
                if (layout[x, y] == 0)
                {
                    draw.DrawPoint(new Vector(x, y), Brushes.AliceBlue, "noHit");
                    layout[x, y] = -1;
                    swapPlayer(ref actualPlayer);
                }
                else //ha van hajó a tileon
                {
                    draw.DrawPoint(new Vector(x, y), Brushes.Red, "hit");
                    int shipNumber = layout[x, y];
                    layout[x, y] = -1;
                    increasePoint(canvas);
                    decreaseShipCount(ref shipCounts, layout, shipNumber);
                    swapPlayer(ref actualPlayer);
                }
                rounds++;
            }

        }
        public void endGame(int[] shipCounts,Player player, Window window)
        {
            swapPlayer(ref player);
            if(!shipCounts.Contains(1))
            {
                window.Close();
                //Remove -> navigate to scoreboard
                string winSentance = $"{player} is the winner";
                MessageBox.Show(winSentance);
            }
        }
        private void swapPlayer(ref Player player)
        {
            if (player == Player.FirstPlayer)
            {
                player = Player.SecondPlayer;
            }
            else player = Player.FirstPlayer;
        }
        private void increasePoint(Canvas c)
        {
            switch(c.Name)
            {
                case "canvasFirstPlayer":
                    {
                        pointFirstPlayer++;
                        return;
                    }
                case "canvasSecondPlayer":
                    {
                        pointSecondPlayer++;
                        return;
                    }
                default:
                    {
                        rounds = 0;
                        return;
                    }
            }
        }
        public string shipCountToString(int[] array)
        {
            return $"Boat: {array[0]}| Destroyer: {array[1]}| Submarine:{array[2]}| Carrier:{array[3]}| Battleship:{array[4]}";
        }
        private void decreaseShipCount(ref int[] shipCount, int[,] matrix, int shipNumber)
        {
            if(isSunk(matrix,shipNumber))
            {
                switch (shipNumber)
                {
                    case 1:
                        {
                            shipCount[0]--;
                            return;
                        }
                    case 2:
                        {
                            shipCount[1]--;
                            return;
                        }
                    case 3:
                        {
                            shipCount[2]--;
                            return;
                        }
                    case 4:
                        {
                            shipCount[3]--;
                            return;
                        }
                    case 5:
                        {
                            shipCount[4]--;
                            return;
                        }
                }
            }            
        }
        private bool isSunk(int[,] matrix, int shipNumber)
        {
            List<int> valuesFromMatrix = new List<int>();
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    valuesFromMatrix.Add(matrix[i, j]);
                }
            }
            if (valuesFromMatrix.Contains(shipNumber))
            {
                return false;
            }return true;
        }
        private int[,] matrixTransformation(int[,] array)
        {
            int[,] generatedArray = new int[array.GetLength(1), array.GetLength(0)];
            for (int i = 0; i < array.GetLength(1); i++)
            {
                for (int j = 0; j < array.GetLength(0); j++)
                {
                    generatedArray[i, j] = array[j, i];
                }
            }
            return generatedArray;
        }
        public void allShipDestroyed(int[] shipCountArray, Window windowToClose )
        {
           if(!shipCountArray.Contains(1))
            {
                windowToClose.Close();
            }
        }
        public void checkShipSink(int number,int[,] shipMatrix, Player actualPlayer)
        {
            List<int> matrixElementsAsList = new List<int>();
            for (int i = 0; i < shipMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < shipMatrix.GetLength(1); j++)
                {
                    matrixElementsAsList.Add( shipMatrix[i, j]);
                }
            }
            if (!matrixElementsAsList.Contains(number))
            {
                switch(actualPlayer)
                {
                    case Player.FirstPlayer:
                        {
                            firstPlayerShipsCounts[number - 1]--;
                            return;
                        }
                    case Player.SecondPlayer:
                        {
                            secondPlayerShipsCounts[number - 1]--;
                            return;
                        }
                }
            }
        }
        public Vector GetPointOnCanvas(Canvas canvas)
        {
            var mousePosition = Mouse.GetPosition(canvas);
            var mousePositionX = mousePosition.X;
            var mousePositionY = mousePosition.Y;
            var lowerLimitX = 0;
            var lowerLimitY = 0;
            for (int i = 1; i < _width; i++)
            {
                if (i * (canvas.Width / _width) < mousePositionX)
                {
                    lowerLimitX = i;
                }
            }
            for (int j = 0; j < _height; j++)
            {
                if (j * (canvas.Height / _height) < mousePositionY)
                {
                    lowerLimitY = j;
                }
            }
            var tileVector = new Vector(lowerLimitX, lowerLimitY);
            return tileVector;
        }
    }
}
