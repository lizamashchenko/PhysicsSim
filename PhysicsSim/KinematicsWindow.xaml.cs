using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PhysicsSim;

public partial class KinematicsWindow : Window
{
    private bool isDragging = false;
    private Point startPosition;
    private double initialAngle;


    private double himarsAngle = 0;
    private Point himarsLauncherLowLeft;
    private Point himarsLauncherLowRigth;

    private double kremlinDistance = 500;
    private double kremlinWidth = 200;
    private double kremlinHeight = 100;

    private Image missile = new Image();
    private Image kremlin = new Image();
    private Image kremlinOnFire = new Image();
    
    
    private const int RefreshRate = 10;
    private const double ScaleFactor = 10;
    private double speed = 0;
    private double speedX, speedY;
    private int pointInTime = 0;
    private const double Gravity = 9.8;
    private const double airResistance = 0.05;
    private const double mass = 300;
    private bool airResistanceOn = false;

    private List<FlyingObject> objects;
    private List<string> objectNames = new List<string>();

    DispatcherTimer timer = new DispatcherTimer();

    public KinematicsWindow()
    {
        InitializeComponent();
        CreateObjects();
        himarsLauncherLowLeft = new Point(5, 40 - himarsLauncher.ActualHeight);
        missile.Source = new BitmapImage(new Uri("pack://application:,,,/images/missile.png"));
        missile.Width = 100;
        missile.Visibility = Visibility.Hidden;

        kremlin = new Image
        {
            Source = new BitmapImage(new Uri("pack://application:,,,/images/kremlin.jpeg")),
            Width = kremlinWidth
        };
        Panel.SetZIndex(kremlin, 0);
        kremlinOnFire = new Image
        {
            Source = new BitmapImage(new Uri("pack://application:,,,/images/kremlin_on_fire.jpeg")),
            Width = kremlinWidth,
            Visibility = Visibility.Hidden
        };
        Canvas.SetBottom(kremlin, 0);
        Canvas.SetBottom(kremlinOnFire, 0);
        Canvas.SetLeft(kremlin, kremlinDistance);
        Canvas.SetLeft(kremlinOnFire, kremlinDistance);

        Panel.SetZIndex(kremlinOnFire, 0);
        Panel.SetZIndex(missile, 1);
        Panel.SetZIndex(himarsLauncher, 2);
        
        field.Children.Add(missile);
        field.Children.Add(kremlin);
        field.Children.Add(kremlinOnFire);
    }

    private void CreateObjects()
    {
        objectNames.Add("Missile");
        objectNames.Add("Cannonball");
        objectNames.Add("Superman");

        foreach (string obj in objectNames)
        {
            ComboBoxItem item = new ComboBoxItem()
            {
                Content = obj,
            };
            ObjectSelector.Items.Add(item);
        }
        // objects.Add(new FlyingObject("pack://application:,,,/images/superman.png", 100));
        // objects.Add(new FlyingObject("pack://application:,,,/images/missile.png", 200));
        // objects.Add(new FlyingObject("pack://application:,,,/images/cannonball.png", 400));
    }
    
    private void SpeedSlider_OnValueChanged(object sender, TextChangedEventArgs textChangedEventArgs)
    {
        try
        {
            speed = SpeedSlider.Value;
        }
        catch (Exception exception)
        {
            MessageBox.Show("Input a correct numeric value");
        }
    }

    private void StopAnimation()
    {
        if (timer != null && timer.IsEnabled)
        {
            timer.Stop();
            timer.Tick -= MoveMissile;
            timer.Tick -= MoveMissileAir;
        }
    }
    private void FireButton_OnClick(object sender, RoutedEventArgs e)
    {
        StopAnimation();

        if (speed != 0 && airResistanceCheck.IsChecked == false)
        {
            pointInTime = RefreshRate;
            SetMissile();
            StartAnimation();
        }
        else if (speed != 0 && airResistanceCheck.IsChecked == true)
        {
            pointInTime = RefreshRate;
            SetMissile();
            StartAnimationAir();
        }
        else
        {
            MessageBox.Show("Invalid speed value!");
        }
    }

    private void SetMissile()
    {
        double offsetX = Math.Sin(himarsAngle) * (himarsLauncher.ActualHeight - missile.ActualHeight);
        double offsetY = Math.Sin(90 - himarsAngle) * (himarsLauncher.ActualHeight - missile.ActualHeight);

        Point MissileBottomLeft = new Point(himarsLauncherLowLeft.X, himarsLauncherLowLeft.Y);
        // Point MissileBottomLeft = new Point(himarsLauncherLowLeft.X - offsetX, himarsLauncherLowLeft.Y + offsetY);
        Canvas.SetLeft(missile, himarsLauncherLowLeft.X);
        Canvas.SetBottom(missile, himarsLauncherLowLeft.Y);
        missile.RenderTransform = new RotateTransform(360 - himarsAngle, himarsLauncherLowLeft.X, himarsLauncherLowLeft.Y);
        missile.Visibility = Visibility.Visible;

        startPosition = MissileBottomLeft;
    }
    private void StartAnimation()
    {
        timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromMilliseconds(RefreshRate);
        timer.Tick += MoveMissile;
        timer.Start();
    }
    private void StartAnimationAir()
    {
        timer = new DispatcherTimer();
        speedY = speed * Math.Sin(himarsAngle * Math.PI / 180);
        speedX = speed * Math.Cos(himarsAngle * Math.PI / 180);
        timer.Interval = TimeSpan.FromMilliseconds(RefreshRate);
        timer.Tick += MoveMissileAir;
        timer.Start();
    }

    private void MoveMissile(object sender, EventArgs e)
    {
        double newX = startPosition.X + speed * Math.Cos(himarsAngle * Math.PI / 180) * pointInTime / 1000;
        double newY = startPosition.Y + speed * Math.Sin(himarsAngle * Math.PI / 180) * pointInTime / 1000 - 0.5 * Gravity * pointInTime / 1000 * pointInTime / 1000;

        double newAngle = Math.Atan2(newY - startPosition.Y, newX - startPosition.X) * (180 / Math.PI);
        missile.RenderTransform = new RotateTransform(360 - newAngle, startPosition.X, startPosition.Y);

        if (newX > kremlinDistance && newX < kremlinDistance + kremlinWidth && newY > 0 && newY < kremlinHeight)
        {
            kremlin.Visibility = Visibility.Hidden;
            kremlinOnFire.Visibility = Visibility.Visible;
            missile.Visibility = Visibility.Hidden;
        }
        else
        {
            Canvas.SetLeft(missile, newX);
            Canvas.SetBottom(missile, newY);
            pointInTime += RefreshRate;
        }
    }
    private void MoveMissileAir(object sender, EventArgs e)
    {
        if (speedY > 0)
        {
            double d = (airResistance * speedY * speedY) / (40 * mass) - Gravity / 20;
            speedY -= (airResistance * speedY * speedY) / (40 * mass) + Gravity / 20;
        }
        else
            speedY += (airResistance * speedY * speedY) / (40 * mass) - Gravity / 20;
        speedX -= (airResistance * speedX * speedX) / (40 * mass);

        double newX = startPosition.X + speedX * pointInTime / 1000;
        double newY = startPosition.Y + speedY * pointInTime / 1000 - 0.5 * Gravity * pointInTime / 1000 * pointInTime / 1000;

        double newAngle = Math.Atan2(newY - startPosition.Y, newX - startPosition.X) * (180 / Math.PI);
        missile.RenderTransform = new RotateTransform(360 - newAngle, startPosition.X, startPosition.Y);

        if (newX > kremlinDistance && newX < kremlinDistance + kremlinWidth && newY > 0 && newY < kremlinHeight)
        {
            kremlin.Visibility = Visibility.Hidden;
            kremlinOnFire.Visibility = Visibility.Visible;
            missile.Visibility = Visibility.Hidden;
        }
        else
        {
            Canvas.SetLeft(missile, newX);
            Canvas.SetBottom(missile, newY);
            pointInTime += RefreshRate;
        }
    }

    // private void Field_OnMouseDown(object sender, MouseButtonEventArgs e)
    // {
    //     isDragging = true;
    //     startPosition = e.GetPosition(field);
    //     initialAngle = himarsLauncher.RenderTransform is RotateTransform rotateTransform
    //         ? rotateTransform.Angle
    //         : 0;
    //     himarsLauncher.CaptureMouse();
    // }
    //
    // private void Field_OnMouseUp(object sender, MouseButtonEventArgs e)
    // {
    //     isDragging = false;
    //     himarsLauncher.ReleaseMouseCapture();
    // }
    //
    // private void Field_OnMouseMove(object sender, MouseEventArgs e)
    // {
    //     if (isDragging)
    //     {
    //         Point currentPosition = e.GetPosition(field);
    //         double dx = currentPosition.X - startPosition.X;
    //         double dy = startPosition.Y - currentPosition.Y;
    //         double angle = Math.Atan2(dy, dx) * (180 / Math.PI);
    //         double rotationAngle = initialAngle + (360 - angle);
    //
    //         RotateTransform rotateTransform = new RotateTransform(rotationAngle, himarsLauncherLowLeft.X, himarsLauncherLowLeft.Y);
    //         himarsLauncher.RenderTransform = rotateTransform;
    //
    //         himarsAngle = angle;
    //     }
    // }

    private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
    {
        airResistanceOn = true;
    }

    private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
    {
        airResistanceOn = false;
    }

    private void AngleTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            himarsAngle = AngleSlider.Value;
            RotateTransform rotateTransform = new RotateTransform(360 - himarsAngle, himarsLauncherLowLeft.X, himarsLauncherLowLeft.Y);
            himarsLauncher.RenderTransform = rotateTransform;
        }
        catch (Exception exception)
        {
            MessageBox.Show("Input a correct numeric value");
        }
    }

    private void ObjectSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
    }
}