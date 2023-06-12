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
    // збурігаємо фігуру точки то інформаційного вікна
    private Ellipse point;
    private Border info;
    private bool infoSeen = false;
    
    // параметри 
    private double distance;
    private double height;
    private double time;

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
    
    // ініціалізація точки
    private void createPoint()
    {
        point = new Ellipse()
        {
            Height = 3,
            Width = 3,
            Fill = (Brush)Application.Current.MainWindow.FindResource("logoBright"),
        };
        point.PreviewMouseLeftButtonDown += PointOnMouseLeftButtonDown;
    }

    // обробка події - натиснуто на точку
    // відкривається інформація
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

    // робить точку видимою
    public void showPoint()
    {
        point.Visibility = Visibility.Visible;
    }

    // створення вікна з інформацією
    private void createInfo()
    {
        info = new Border()
        {
            BorderBrush = Brushes.Transparent,
            Background = (Brush)Application.Current.MainWindow.FindResource("logoLight"),
        };
        Label heightLabel = new Label()
        {
            Content = "Height: " + height,
            FontFamily = new FontFamily("Constantia"),
            Foreground = (Brush)Application.Current.MainWindow.FindResource("logoBright")
        };
        Label distanceLabel = new Label()
        {
            Content = "Distance: " + distance,
            FontFamily = new FontFamily("Constantia"),
            Foreground = (Brush)Application.Current.MainWindow.FindResource("logoBright")
        };
        Label timeLabel = new Label()
        {
            Content = "Time: " + time + " s",
            FontFamily = new FontFamily("Constantia"),
            Foreground = (Brush)Application.Current.MainWindow.FindResource("logoBright")
        };
            
        Style borderStyle = new Style(typeof(Border));
        borderStyle.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(10)));
        Button cancel = new Button()
        {
            Content = "Close",
            Width = 50,
            FontFamily = new FontFamily("Constantia"),
            Background = (Brush)Application.Current.MainWindow.FindResource("logo"),
            Foreground = (Brush)Application.Current.MainWindow.FindResource("logoBright")
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

    // обробка події - натиснуто на "Закрити"
    // вікно зникає
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