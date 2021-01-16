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
        SecondPlayer,
        AIPlayer
    }
    public class BattleViewModel
    {
        private int _width;
        private int _height;
        private GameType _gameType;
        public int[,] originAiArray;
        public int[,] _firstPlayerRefShipMatrix;
        public int[,] _secondPlayerRefShipmatrix;
        private int[,] _testArray = new int[,] {
            {1,0,0,0,0,0,0,0,0,0 },
            {0,2,0,0,0,0,0,0,0,0 },
            {0,2,0,0,0,3,3,3,0,0 },
            {0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,4,0,0,0 },
            {0,0,0,0,0,0,4,0,0,0 },
            {0,0,0,0,0,0,4,0,0,0 },
            {0,0,0,0,0,0,4,0,0,0 },
            {5,5,5,5,5,0,0,0,0,0 }
        };

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
            //_secondPlayerRefShipmatrix = matrixTransformation(_testArray);
            originAiArray = matrixTransformation(matrixTransformation((int[,])Application.Current.Properties["2ndPlayerArray"]));
            _gameType =(GameType) Application.Current.Properties["gameType"];
        }

        public void guessTile(Canvas canvas, ref int[,] layout, ref int[] shipCounts,ref Player actualPlayer,Vector hitCordinates )
        {
            int x = Convert.ToInt32(hitCordinates.X);
            int y = Convert.ToInt32(hitCordinates.Y);
            Draw draw = new Draw(canvas, _width, _height);
            if (layout[x, y] != -1)
            {
                //ha nincs hajó a tileon
                if (layout[x, y] == 0)
                {
                    draw.DrawPoint(new Vector(x, y), Brushes.AliceBlue, "noHit");
                    layout[x, y] = -1;
                    swapPlayer(ref actualPlayer, _gameType);
                }
                else //ha van hajó a tileon
                {
                    draw.DrawPoint(new Vector(x, y), Brushes.Red, "hit");
                    int shipNumber = layout[x, y];
                    layout[x, y] = -2;
                    increasePoint(canvas);
                    decreaseShipCount(ref shipCounts, layout, shipNumber);
                    swapPlayer(ref actualPlayer, _gameType);
                }
            }
        }
        public void increaseRound()
        {
            rounds++;
        }
        public void endGame(int[] shipCounts,Player player, Window window)
        {

            swapPlayer(ref player, _gameType);
            if(!shipCounts.Contains(1))
            {
                window.Close();
                //Remove -> navigate to scoreboard
                string winSentance = $"{player} is the winner";
                MessageBox.Show(winSentance);
            }
        }
        private void swapPlayer(ref Player player, GameType gameType)
        {
            if(gameType == GameType.Pvp)
            {
                if (player == Player.FirstPlayer)
                {
                    player = Player.SecondPlayer;
                }
                else player = Player.FirstPlayer;
            }
            else
            {
                if (player == Player.FirstPlayer)
                {
                    player = Player.AIPlayer;
                }
                else player = Player.FirstPlayer;
            }
            
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
        public void showAiShips(int[,] array, Canvas canvas)
        {
            canvas.Children.Clear();
            Draw draw = new Draw(canvas, array.GetLength(0), array.GetLength(0));
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(0); j++)
                {
                    Vector coordinate = new Vector(i, j);
                    Brush brush;
                    if (array[i, j] == 0) brush = Brushes.AliceBlue;
                    else brush = Brushes.Red;
                    draw.DrawPoint(coordinate, brush, "hint");
                }
            }
        }
        public void restoreCanvas(int[,] array, Canvas canvas)
        {
            canvas.Children.Clear();
            Draw draw = new Draw(canvas, array.GetLength(0), array.GetLength(0));
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(0); j++)
                {
                    Vector coordinate = new Vector(i, j);
                    Brush brush;
                    if (array[i, j] == -2)
                    {
                        brush = Brushes.Red;
                        draw.DrawPoint(coordinate, brush, "hint");
                    }
                    else if (array[i, j] == -1)
                    {
                        brush = Brushes.AliceBlue;
                        draw.DrawPoint(coordinate, brush, "hint");
                    }

                }
            }
        }
    }
}
