using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
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

    private Image kremlin = new Image();
    private Image kremlinOnFire = new Image();
    
    
    private const int RefreshRate = 10;
    private const double ScaleFactor = 10;
    private double speed = 0;
    private double speedX, speedY;
    private int pointInTime = 0;
    private const double Gravity = 9.8;
    private const double airResistance = 0.05;
    private double mass = 0;
    private bool airResistanceOn = false;

    private Dictionary<string, FlyingObject> objects = new Dictionary<string, FlyingObject>();
    private List<string> objectNames = new List<string>();
    private FlyingObject currentObject;

    DispatcherTimer timer = new DispatcherTimer();

    public KinematicsWindow()
    {
        InitializeComponent();
        CreateObjects();
        himarsLauncherLowLeft = new Point(5, 40 - himarsLauncher.ActualHeight);
        
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
        Panel.SetZIndex(himarsLauncher, 2);
        
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
        objects.Add("Superman", new FlyingObject("pack://application:,,,/images/superman.png", 100));
        objects.Add("Missile", new FlyingObject("pack://application:,,,/images/missile.png", 200));
        objects.Add("Cannonball", new FlyingObject("pack://application:,,,/images/cannonball.png", 400));

        foreach (FlyingObject f in objects.Values)
        {
            field.Children.Add(f.GetImage());
        }
    }
    
    private void SpeedSlider_OnValueChanged(object sender, TextChangedEventArgs textChangedEventArgs)
    {
        StopAnimation();
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
        if (timer.IsEnabled)
        {
            timer.Stop();
            timer.Tick -= MoveObject;
        }

        if (currentObject != null)
        {
            currentObject.Hide();
        }
    }
    private void FireButton_OnClick(object sender, RoutedEventArgs e)
    {
        StopAnimation();

        if (speed != 0 && currentObject.GetWeight() != 0)
        {
            pointInTime = RefreshRate;
            SetObject();
            StartAnimation();
        } 
        else
        {
            MessageBox.Show("Invalid speed value!");
        }
    }

    private void SetObject()
    {
        currentObject.SetObject(himarsLauncherLowLeft.X, himarsLauncherLowLeft.Y, himarsAngle);
        startPosition = himarsLauncherLowLeft;
    }
    private void StartAnimation()
    {
        currentObject.Show();
        speedY = speed * Math.Sin(himarsAngle * Math.PI / 180);
        speedX = speed * Math.Cos(himarsAngle * Math.PI / 180);
        timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromMilliseconds(RefreshRate);
        timer.Tick += MoveObject;
        timer.Start();
    }

    private void MoveObject(object sender, EventArgs e)
    {
        double newX;
        double newY;

        if (!airResistanceOn)
        {
            newX = startPosition.X + speed * Math.Cos(himarsAngle * Math.PI / 180) * pointInTime / 1000;
            newY =  startPosition.Y + speed * Math.Sin(himarsAngle * Math.PI / 180) * pointInTime / 1000 - 0.5 * Gravity * pointInTime / 1000 * pointInTime / 1000;
        }
        else
        {
            if (speedY > 0)
            {
                double d = (airResistance * speedY * speedY) / (40 * mass) - Gravity / 20;
                speedY -= (airResistance * speedY * speedY) / (40 * mass) + Gravity / 20;
            }
            else
                speedY += (airResistance * speedY * speedY) / (40 * mass) - Gravity / 20;
            speedX -= (airResistance * speedX * speedX) / (40 * mass);

            newX = startPosition.X + speedX * pointInTime / 1000;
            newY = startPosition.Y + speedY * pointInTime / 1000 - 0.5 * Gravity * pointInTime / 1000 * pointInTime / 1000;
        }
        
        double newAngle = Math.Atan2(newY - startPosition.Y, newX - startPosition.X) * (180 / Math.PI);

        if (newX > kremlinDistance && newX < kremlinDistance + kremlinWidth && newY > 0 && newY < kremlinHeight)
        {
            kremlin.Visibility = Visibility.Hidden;
            kremlinOnFire.Visibility = Visibility.Visible;
            currentObject.Hide();
        }
        else if (newX > this.ActualWidth || newX < 0 || newY > this.ActualHeight || newY < 0)
        {
            currentObject.Hide();
        }
        else
        {
            currentObject.SetObject(newX, newY, newAngle);
            pointInTime += RefreshRate;
        }
        Ellipse el = new Ellipse()
        {
            Height = 2,
            Width = 2,
            Fill = new SolidColorBrush(Colors.Aqua)
        };
        Canvas.SetBottom(el, newY);
        Canvas.SetLeft(el, newX);
        field.Children.Add(el);

    }

    private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
    {
        StopAnimation();
        airResistanceOn = true;
    }

    private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
    {
        StopAnimation();
        airResistanceOn = false;
    }

    private void AngleTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        StopAnimation();
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
        StopAnimation();

        if (field == null)
            return;
        ComboBoxItem selectedItem = (ComboBoxItem)ObjectSelector.SelectedItem;
        string selected = selectedItem.Content.ToString();
        currentObject = objects[selected] == null ? new FlyingObject() : objects[selected];
        mass = currentObject.GetWeight();
    }
}