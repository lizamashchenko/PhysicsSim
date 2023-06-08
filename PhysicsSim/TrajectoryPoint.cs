using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PhysicsSim;

public class TrajectoryPoint
{
    private Ellipse point;
    private Border info;
    private bool infoSeen = false;
    
    private double distance;
    private double height;
    private double time;

    private double horizontalSpeed;
    private double verticalSpeed;

    public TrajectoryPoint()
    {
        
    }

    public TrajectoryPoint(double distance, double height, double time)
    {
        this.distance = distance;
        this.height = height;
        this.time = time;
        createPoint();
        createInfo();
    }

    // private void createVectors(Point start, bool resistant)
    // {
    //     Line horizontal = new Line()
    //     {
    //         X1 = start.X,
    //         X2 = start.X - 10,
    //         Y1 = start.Y,
    //         Y2 = start.Y,
    //         StrokeThickness = 1,
    //         Stroke = new SolidColorBrush(Colors.Aquamarine),
    //     };
    //     Line vertical = new Line()
    //     {
    //         Y1 = start.Y,
    //         Y2 = start.Y - 10,
    //         X1 = start.X,
    //         X2 = start.X,
    //         StrokeThickness = 1,
    //         Stroke = new SolidColorBrush(Colors.Aquamarine),
    //     };
    // }
    private void createPoint()
    {
        point = new Ellipse()
        {
            Height = 3,
            Width = 3,
            Fill = new SolidColorBrush(Colors.Crimson)
        };
        point.PreviewMouseLeftButtonDown += PointOnMouseLeftButtonDown;
    }

    private void PointOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (!infoSeen)
        {
            info.Visibility = Visibility.Visible;
            infoSeen = true;
        }
        else
        {
            info.Visibility = Visibility.Hidden;
            infoSeen = false;
            point.Visibility = Visibility.Visible;

        }

        point.Visibility = Visibility.Visible;
    }

    

    public void showPoint()
    {
        point.Visibility = Visibility.Visible;
    }

    private void createInfo()
    {
        info = new Border()
        {
            BorderBrush = Brushes.Transparent,
            Background = Brushes.Beige,
        };
        Label heightLabel = new Label()
        {
            Content = "Height: " + height,
        };
        Label distanceLabel = new Label()
        {
            Content = "Distance: " + distance,
        };
        Label timeLabel = new Label()
        {
            Content = "Time: " + time,
        };
            
        Style borderStyle = new Style(typeof(Border));
        borderStyle.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(10)));
        Button cancel = new Button()
        {
            Content = "Cancel",
            Width = 50,
        };
        cancel.Resources.Add(typeof(Border), borderStyle);
        cancel.Click += CancelOnClick;
        
        StackPanel labels = new StackPanel();
        labels.Children.Add(heightLabel);
        labels.Children.Add(distanceLabel);
        labels.Children.Add(timeLabel);
        labels.Children.Add(cancel);
        info.Child = labels;
        info.Visibility = Visibility.Hidden;
    }

    private void CancelOnClick(object sender, RoutedEventArgs e)
    {
        this.info.Visibility = Visibility.Hidden;
    }

    public Ellipse GetPoint()
    {
        return point;
    }

    public Border GetInfo()
    {
        return info;
    }
}