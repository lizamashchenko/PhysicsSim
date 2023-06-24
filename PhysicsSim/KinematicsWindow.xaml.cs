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
    // параметри пускової установки
    private Point startPosition;
    private double initialAngle;

    private double himarsAngle = 0;
    private Point himarsLauncherLowLeft;
    private Point himarsLauncherLowRigth;

    // параметри цілі
    private double kremlinDistance = 750;
    private double kremlinWidth = 200;
    private double kremlinHeight = 200;

    private Image kremlin = new Image();
    private Image kremlinOnFire = new Image();
    
    // програмні змінні
    private const int RefreshRate = 10;
    private double speed = 0;
    private double speedX, speedY;
    private int pointInTime = 0;
    private const double Gravity = 9.8;
    private const double airResistance = 0.05;
    private double mass = 0;
    private bool airResistanceOn = false;

    // списки
    private Dictionary<string, FlyingObject> objects = new Dictionary<string, FlyingObject>();
    private List<string> objectNames = new List<string>();
    private FlyingObject currentObject;

    private List<Ellipse> trajectories = new List<Ellipse>();
    private List<TrajectoryPoint> trajectoryPoints = new List<TrajectoryPoint>();

    // таймер анімації
    DispatcherTimer timer = new DispatcherTimer();

    public KinematicsWindow()
    {
        InitializeComponent();
        // ініціалізація об'єктів
        CreateObjects();
        
        // ініціалізація цілі
        kremlin = new Image
        {
            Source = new BitmapImage(new Uri("pack://application:,,,/images/kremlin.png")),
            Width = kremlinWidth,
            Height = kremlinHeight
        };
        Panel.SetZIndex(kremlin, 0);
        kremlinOnFire = new Image
        {
            Source = new BitmapImage(new Uri("pack://application:,,,/images/kremlin_on_fire.png")),
            Width = kremlinWidth,
            Height = kremlinHeight,
            Visibility = Visibility.Hidden
        };
        // встановлення цілі на полотні
        Canvas.SetBottom(kremlin, 0);
        Canvas.SetBottom(kremlinOnFire, 0);
        Canvas.SetLeft(kremlin, kremlinDistance);
        Canvas.SetLeft(kremlinOnFire, kremlinDistance);

        Panel.SetZIndex(kremlinOnFire, 0);
        Panel.SetZIndex(himarsLauncher, 2);
        
        field.Children.Add(kremlin);
        field.Children.Add(kremlinOnFire);
        himarsLauncherLowLeft = new Point(8, 65);
        Ellipse el = new Ellipse()
        {
            Height = 3,
            Width = 3,
            Fill = new SolidColorBrush(Colors.Red),
        };
        Canvas.SetLeft(el, himarsLauncherLowLeft.X);
        Canvas.SetBottom(el, himarsLauncherLowLeft.Y);
        field.Children.Add(el);
    }

    // створюємо об'єкти запуску
    private void CreateObjects()
    {
        objectNames.Add("Missile");
        objectNames.Add("Cannonball");
        objectNames.Add("Superman");

        // створюємо випадаючий список об'єктів
        foreach (string obj in objectNames)
        {
            ComboBoxItem item = new ComboBoxItem()
            {
                Content = obj,
                FontFamily = new FontFamily("Constantia"),
                FontSize = 15
            };
            ObjectSelector.Items.Add(item);
        }
        objects.Add("Superman", new FlyingObject("pack://application:,,,/images/superman.png", 100));
        objects.Add("Missile", new FlyingObject("pack://application:,,,/images/missile.png", 200));
        objects.Add("Cannonball", new FlyingObject("pack://application:,,,/images/cannonball.png", 400));

        // додаємо елементи на полотно
        foreach (FlyingObject f in objects.Values)
        {
            field.Children.Add(f.GetImage());
        }
    }
    
    // обробка події - зміна швидкості запуску
    private void SpeedSlider_OnValueChanged(object sender, TextChangedEventArgs textChangedEventArgs)
    {
        // зупиняємо попередні анімації
        StopAnimation();
        try
        {
            // змініємо параметри запуску
            speed = SpeedSlider.Value;
        }
        catch (Exception exception)
        {
            MessageBox.Show("Input a correct numeric value");
        }
    }

    // зупинка анімації
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
    
    // обробка події - натиснута кнопка "Запуск"
    private void FireButton_OnClick(object sender, RoutedEventArgs e)
    {
        // зупиняємо всі попередні анімації
        StopAnimation();

        // ініціалізуємо початкові значення 
        if (speed != 0 && currentObject != null && currentObject.GetWeight() != 0)
        {
            pointInTime = RefreshRate;
            SetObject();
            StartAnimation();
        } 
        else
        {
            MessageBox.Show(speed == 0 ? "Invalid speed value!" : "Please select an object");
        }
    }

    // встановлюємо об'єкт на новій позиції
    private void SetObject()
    {
        currentObject.SetObject(himarsLauncherLowLeft.X, himarsLauncherLowLeft.Y, himarsAngle);
        startPosition = himarsLauncherLowLeft;
    }
    
    // програвач анімації
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

    // переміщення об'єкту 
    private void MoveObject(object sender, EventArgs e)
    {
        double newX;
        double newY;

        // різна логіка вираховування нової координати в залкжностф від опору повітря
        if (!airResistanceOn)
        {
            newX = startPosition.X + speed * Math.Cos(himarsAngle * Math.PI / 180) * pointInTime / 1000;
            newY =  startPosition.Y + speed * Math.Sin(himarsAngle * Math.PI / 180) * pointInTime / 1000 - 0.5 * Gravity * pointInTime / 1000 * pointInTime / 1000;
        }
        else
        {
            if (speedY > 0)
            {
                speedY -= (airResistance * speedY * speedY) / (40 * mass) + Gravity / 20;
            }
            else
                speedY += (airResistance * speedY * speedY) / (40 * mass) - Gravity / 20;
            speedX -= (airResistance * speedX * speedX) / (40 * mass);

            newX = startPosition.X + speedX * pointInTime / 1000;
            newY = startPosition.Y + speedY * pointInTime / 1000 - 0.5 * Gravity * pointInTime / 1000 * pointInTime / 1000;
        }
        
        // новий кут нахилу об'єкта
        double newAngle = Math.Atan2(newY - startPosition.Y, newX - startPosition.X) * (180 / Math.PI);

        // перевірка влучання
        if (newX > kremlinDistance && newX < kremlinDistance + kremlinWidth && newY > 0 && newY < kremlinHeight)
        {
            kremlin.Visibility = Visibility.Hidden;
            kremlinOnFire.Visibility = Visibility.Visible;
            currentObject.Hide();
        }
        // перевірка вильоту за межі
        else if (newX > this.ActualWidth || newX < 0 || newY > this.ActualHeight || newY < 0)
        {
            currentObject.Hide();
        }
        else
        {
            currentObject.SetObject(newX, newY, newAngle);
            pointInTime += RefreshRate;
        }

        // створення нової точки з інформацією кожну секунду
        if (pointInTime % 1000 == 0)
        {
            TrajectoryPoint p = new TrajectoryPoint(newX, newY, pointInTime / 1000.0);
            Canvas.SetBottom(p.GetPoint(), newY - p.getR());
            Canvas.SetLeft(p.GetPoint(), newX - p.getR());
            Canvas.SetZIndex(p.GetPoint(), 1);
            p.showPoint();
            field.Children.Add(p.GetPoint());
            
            Canvas.SetBottom(p.GetInfo(), newY);
            Canvas.SetLeft(p.GetInfo(), newX);
            Canvas.SetZIndex(p.GetInfo(), 0);
            field.Children.Add(p.GetInfo());
            
            trajectoryPoints.Add(p);
        }
        // створення звичайної точки, що потім формує траекторію
        else
        {
            Ellipse el = new Ellipse()
            {
                Height = 2,
                Width = 2,
                Fill = (Brush)Application.Current.MainWindow.FindResource("logo"),
            };
            trajectories.Add(el);
            Canvas.SetBottom(el, newY);
            Canvas.SetLeft(el, newX);
            Canvas.SetZIndex(el, 0);
            field.Children.Add(el);
        }
    }

    // змінюємо прапорець опору
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

    // оновлюємо кут нахилу пускової установки на змінні значення слайдеру
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

    // обробка події - зміна об'єкту запуску
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
    // обробка події - натиснуна кнопка "Очистити"
    // всі точки траекторії видаляються з полотна
    private void ClearButton_OnClick(object sender, RoutedEventArgs e)
    {
        foreach (Ellipse el in trajectories)
        {
            field.Children.Remove(el);
        }

        foreach (TrajectoryPoint tp in trajectoryPoints)
        {
            field.Children.Remove(tp.GetInfo());
            field.Children.Remove(tp.GetPoint());
        }

        trajectories = new List<Ellipse>();
        trajectoryPoints = new List<TrajectoryPoint>();
    }
}