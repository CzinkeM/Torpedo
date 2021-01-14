using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Torpedo
{
    class Draw
    {
        private Canvas _canvas;
        private int _width;
        private int _height;

        public Draw(Canvas canvas,int width, int height)
        {
            _canvas = canvas;
            _width = width;
            _height = height;
        }
        public void DrawPoint(Vector position, Brush brush, string id)
        {
            var shape = new Rectangle();
            shape.Fill = brush;
            var unitX = _canvas.Width / _width;
            var unitY = _canvas.Height / _height;
            shape.Width = unitX;
            shape.Height = unitY;
            shape.Stroke = Brushes.Black;
            shape.StrokeThickness = 1;
            shape.Uid = id;
            Canvas.SetLeft(shape, position.X * unitX);
            Canvas.SetTop(shape, position.Y * unitY);
            _canvas.Children.Add(shape);
        }
    }
}
