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
        public void DrawPoint(Canvas canvas,int width,int height,Vector position, Brush brush, string Id)
        {
            var shape = new Rectangle();
            shape.Fill = brush;
            var unitX = canvas.Width / width;
            var unitY = canvas.Height / height;
            shape.Width = unitX;
            shape.Height = unitY;
            shape.Stroke = Brushes.Black;
            shape.StrokeThickness = 1;
            shape.Uid = Id;
            Canvas.SetLeft(shape, position.X * unitX);
            Canvas.SetTop(shape, position.Y * unitY);
            canvas.Children.Add(shape);
        }
    }
}
