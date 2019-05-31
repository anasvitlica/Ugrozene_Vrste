using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace Ugrozene_Vrste
{
    class DragAndDropHandler
    {
        public MainWindow MainWindowParent { get; set; }

        public DragAndDropHandler(MainWindow mw)
        {
            MainWindowParent = mw;
        }

        //Ako izlazi izvan granica canvasa
        //public double GetNewLeft(DragEventArgs e, double width, double firstXPos)
        //{
        //    //double newLeft = e.GetPosition(MainWindowParent.canvas).X - firstXPos - MainWindowParent.canvas.Margin.Left;
        //    double newLeft = firstXPos;

        //    // newLeft inside canvas right-border?
        //    if (newLeft > MainWindowParent.canvas.Margin.Left + MainWindowParent.canvas.ActualWidth - width)
        //    {
        //        return MainWindowParent.canvas.Margin.Left + MainWindowParent.canvas.ActualWidth - width;
        //    }
        //    // newLeft inside canvas left-border?
        //    else if (newLeft < MainWindowParent.canvas.Margin.Left)
        //    {
        //        return MainWindowParent.canvas.Margin.Left;
        //    }

        //    return newLeft;
        //}

        //public double GetNewTop(DragEventArgs e, double height, double firstYPos)
        //{
        //    //double newTop = e.GetPosition(MainWindowParent.canvas).Y - firstYPos - MainWindowParent.canvas.Margin.Top;
        //    double newTop = firstYPos;
        //    if (newTop > MainWindowParent.canvas.Margin.Top + MainWindowParent.canvas.ActualHeight - height)    //bottom
        //    {
        //        return MainWindowParent.canvas.Margin.Bottom - height;      //?????
        //    }
        
        //    else if (newTop < MainWindowParent.canvas.Margin.Top)
        //    {
        //        return MainWindowParent.canvas.Margin.Top;
        //    }

        //    return newTop;
        //}

        //public Rect MoveIfOverlapping(DragEventArgs e)
        //{
        //    Point mousePos = e.GetPosition(MainWindowParent.canvas);
        //    mousePos.X -= 25;   //centriranje
        //    mousePos.Y -= 25;
        //    Rect rectangle = new Rect(mousePos, new Size(50, 50));

        //    foreach (KeyValuePair<string, Rect> pair in MainWindowParent.SlikeNaMapi)
        //    {
        //        double diffLeft = pair.Value.Left - rectangle.Left;
        //        double diffTop = pair.Value.Y - rectangle.Y;
        //        if (pair.Value.IntersectsWith(rectangle))
        //        {
        //            if (diffLeft < 0)    //nova slika je desno od stare 
        //            {                    //povecavam joj X poziciju, da bi se pomerila jos udesno
        //                rectangle.X += Math.Abs(pair.Value.Right - rectangle.Left); //pomeram je za onoliko za koliko se sece
        //            }                                                               //sa starom slikom
        //            else if (diffLeft >= 0)     //nova slika je levo od stare
        //            {
        //                rectangle.X -= Math.Abs(pair.Value.Left - rectangle.Right);
        //            }
        //            rectangle.Location = new Point(rectangle.X, rectangle.Y);
        //        }
        //    }
        //    return rectangle;
        //}

        //public double GetNewLeft(MouseEventArgs e, double width, double firstXPos)
        //{
        //    double newLeft = e.GetPosition(MainWindowParent.canvas).X - firstXPos - MainWindowParent.canvas.Margin.Left;
        //    // newLeft inside canvas right-border?
        //    if (newLeft > MainWindowParent.canvas.Margin.Left + MainWindowParent.canvas.ActualWidth - width)
        //    {
        //        return MainWindowParent.canvas.Margin.Left + MainWindowParent.canvas.ActualWidth - width;
        //    }
        //    // newLeft inside canvas left-border?
        //    else if (newLeft < MainWindowParent.canvas.Margin.Left)
        //    {
        //        return MainWindowParent.canvas.Margin.Left;
        //    }

        //    return newLeft;
        //}


        //public double GetNewTop(MouseEventArgs e, double height, double firstYPos)
        //{
        //    double newTop = e.GetPosition(MainWindowParent.canvas).Y - firstYPos - MainWindowParent.canvas.Margin.Top;

        //    if (newTop > MainWindowParent.canvas.Margin.Top + MainWindowParent.canvas.ActualHeight - height)
        //    {
        //        return MainWindowParent.canvas.Margin.Top + MainWindowParent.canvas.ActualHeight - height;
        //    }

        //    else if (newTop < MainWindowParent.canvas.Margin.Top)
        //    {
        //        return MainWindowParent.canvas.Margin.Top;
        //    }

        //    return newTop;
        //}

        //public Rect MoveIfOverlapping(MouseButtonEventArgs e)
        //{
        //    Point mousePos = e.GetPosition(MainWindowParent.canvas);
        //    mousePos.X -= 25;   //centriranje
        //    mousePos.Y -= 25;
        //    Rect rectangle = new Rect(mousePos, new Size(50, 50));

        //    foreach (KeyValuePair<string, Rect> pair in MainWindowParent.SlikeNaMapi)
        //    {
        //        double diffLeft = pair.Value.Left - rectangle.Left;
        //        double diffTop = pair.Value.Y - rectangle.Y;
        //        if (pair.Value.IntersectsWith(rectangle))
        //        {
        //            if (diffLeft < 0)    //nova slika je desno od stare 
        //            {                    //povecavam joj X poziciju, da bi se pomerila jos udesno
        //                rectangle.X += Math.Abs(pair.Value.Right - rectangle.Left); //pomeram je za onoliko za koliko se sece
        //            }                                                               //sa starom slikom
        //            else if (diffLeft >= 0)     //nova slika je levo od stare
        //            {
        //                rectangle.X -= Math.Abs(pair.Value.Left - rectangle.Right);
        //            }
        //            rectangle.Location = new Point(rectangle.X, rectangle.Y);
        //        }
        //    }
        //    return rectangle;
        //}

    }
}
