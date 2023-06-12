using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PhysicsSim;

public partial class OpticsWindow : Window
{
    private Image concaveLens = new Image();
    private Image convexLens = new Image();
    private Line xAxis = new Line();
    private Line yAxis = new Line();
    private bool isFirstStart = false;

    private double lensHeight = 100;
    private double concaveWidth = 26;
    private double convexWidth = 44;
    private double focalDistance = 40;
    private double objectDistance = 200;
    private bool isConvex = false;

    private Point lensCenter;

    private Ellipse f1, f2;
    private int focusRadius = 5;
    
    private double f;
    private double h;

    private const int amountOfOjects = 4;
    private string[] objectNames = {"Daffy", "Cat", "Dog", "Person"};
    private int objectIndex = 0;
    
    private bool isDragged = false;
    private List<Tuple<Image, Image>> projections = new List<Tuple<Image, Image>>();
    private List<Line> rays = new List<Line>();
    public OpticsWindow()
    {
        InitializeComponent();
        isFirstStart = true;

        concaveLens.Source = new BitmapImage(new Uri("pack://application:,,,/images/concave_lens.png"));
        convexLens.Source = new BitmapImage(new Uri("pack://application:,,,/images/convex_lens.png"));

        for (int i = 0; i < amountOfOjects; i++)
        {

            Image image = new Image()
            {
                Source = new BitmapImage(new Uri($"pack://application:,,,/images/{objectNames[i].ToLower()}.png")),
                Height = 100,
                Visibility = Visibility.Hidden
            };
            Image projection = new Image()
            {
                Source = new BitmapImage(new Uri($"pack://application:,,,/images/{objectNames[i].ToLower()}.png")),
                Height = 100,
                Visibility = Visibility.Hidden
            };
            projections.Add(new Tuple<Image, Image>(image, projection));
            
            ComboBoxItem item = new ComboBoxItem
            {
                Content = objectNames[i],
            };
            if (i == 0)
                item.IsSelected = true;
            ObjectSelector.Items.Add(item);
            FocusSlider.Value = focalDistance;
            DistanceSlider.Value = objectDistance;
        }
    }

    private void SetElements()
    {
        xAxis = new Line
        {
            X1 = 0,
            X2 = field.ActualWidth,
            Y1 = field.ActualHeight / 2,
            Y2 = field.ActualHeight / 2,
            StrokeThickness = 2,
            Stroke = (Brush)Application.Current.MainWindow.FindResource("darkGreen"),
        };
        field.Children.Add(xAxis);

        yAxis = new Line()
        {
            X1 = field.ActualWidth / 2,
            X2 = field.ActualWidth / 2,
            Y1 = field.ActualHeight,
            Y2 = 0,
            StrokeThickness = 2,
            Stroke = (Brush)Application.Current.MainWindow.FindResource("darkGreen"),
        };
        field.Children.Add(yAxis);

        f1 = new Ellipse()
        {
            Height = 2 * focusRadius,
            Width = 2 * focusRadius,
            Stroke = (Brush)Application.Current.MainWindow.FindResource("brightGreen"),
            Fill = (Brush)Application.Current.MainWindow.FindResource("brightGreen"),
        };
        f2 = new Ellipse()
        {
            Height = 2 * focusRadius,
            Width = 2 * focusRadius,
            Stroke = (Brush)Application.Current.MainWindow.FindResource("brightGreen"),
            Fill = (Brush)Application.Current.MainWindow.FindResource("brightGreen"),
        };
        setFocuses();
        field.Children.Add(f1);
        field.Children.Add(f2);
        
        concaveLens.Height = lensHeight;
        convexLens.Height = lensHeight;
        
        Canvas.SetLeft(concaveLens, field.ActualWidth / 2 - concaveWidth / 2 + 2);
        Canvas.SetBottom(concaveLens, field.ActualHeight / 2 - lensHeight / 2);
        concaveLens.Visibility = Visibility.Hidden;
        field.Children.Add(concaveLens);

        Canvas.SetLeft(convexLens, field.ActualWidth / 2 - convexWidth / 2 + 2);
        Canvas.SetBottom(convexLens, field.ActualHeight / 2 - lensHeight / 2);
        convexLens.Visibility = Visibility.Hidden;
        field.Children.Add(convexLens);
        
        foreach (Tuple<Image, Image> t in projections)
        {
            field.Children.Add(t.Item1);
            field.Children.Add(t.Item2);
        }
    }

    private void setFocuses()
    {
        if (f1 == null || f2 == null)
            return;

        Canvas.SetLeft(f1, field.ActualWidth / 2 - focalDistance - focusRadius);
        Canvas.SetLeft(f2, field.ActualWidth / 2 + focalDistance - focusRadius);
        Canvas.SetBottom(f1, field.ActualHeight / 2 - focusRadius);
        Canvas.SetBottom(f2, field.ActualHeight / 2 - focusRadius);
    }

    private void placeObjects()
    {
        lensCenter = new Point(field.ActualWidth / 2, field.ActualHeight / 2);

        Canvas.SetBottom(projections[objectIndex].Item1, field.ActualHeight / 2);
        Canvas.SetLeft(projections[objectIndex].Item1, field.ActualWidth / 2 - objectDistance - projections[objectIndex].Item1.ActualWidth / 2);

        f = focalDistance * objectDistance / (objectDistance - focalDistance);

        foreach (Tuple<Image, Image> t in projections)
        { 
            h = Math.Abs(f) * projections[objectIndex].Item1.ActualHeight / objectDistance;
            t.Item2.Height = h;
            if (isConvex)
            {
                if (objectDistance >= focalDistance)
                {
                    Canvas.SetBottom(t.Item2, field.ActualHeight / 2 - 2 * h);
                    t.Item2.RenderTransform = new ScaleTransform(1, -1);
                    Canvas.SetLeft(t.Item2, lensCenter.X + f - t.Item2.ActualWidth / 2);
                }
                else
                {
                    Canvas.SetBottom(t.Item2, field.ActualHeight / 2);
                    t.Item2.RenderTransform = new ScaleTransform(1, 1);
                    Canvas.SetLeft(t.Item2, lensCenter.X + f - t.Item2.ActualWidth / 2);
                }

            }
            else
            {
                f = -focalDistance * objectDistance / (objectDistance + focalDistance);

                t.Item2.RenderTransform = new ScaleTransform(1, 1);
                Canvas.SetLeft(t.Item2, lensCenter.X + f - t.Item2.ActualWidth / 2);
                Canvas.SetBottom(t.Item2, field.ActualHeight / 2);
            }
        }
    }
    private void DiameterSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if(field == null)
            return;
        try
        {
            objectDistance = DistanceSlider.Value;
            Canvas.SetLeft(projections[objectIndex].Item1, field.ActualWidth / 2 - objectDistance - projections[objectIndex].Item1.ActualWidth / 2);
            Update();
        }
        catch (Exception exception)
        {
            MessageBox.Show("Input a correct numeric value");
        }
    }

    private void FocusSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (field == null)
            return;
        try
        {
            focalDistance = FocusSlider.Value;
            setFocuses();
            Update();
        }
        catch (Exception exception)
        {
            MessageBox.Show("Input a correct numeric value");
        }
    }

    private void LensSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (isFirstStart)
        {
            SetElements();
            isFirstStart = false;
        }

        ComboBoxItem selectedItem = (ComboBoxItem)LensSelector.SelectedItem;
        if (selectedItem.Content == null)
            return;
        string selected = selectedItem.Content.ToString();
        if (selected == "Concave Lens")
        {
            isConvex = false;
            concaveLens.Visibility = Visibility.Visible;
            convexLens.Visibility = Visibility.Hidden;
        }
        else if (selected == "Convex Lens")
        {
            isConvex = true;
            concaveLens.Visibility = Visibility.Hidden;
            convexLens.Visibility = Visibility.Visible;
        }
        else
        {
            concaveLens.Visibility = Visibility.Hidden;
            convexLens.Visibility = Visibility.Hidden;
        }

        Update();
    }

    private void BuildRays()
    {
        foreach (Line ray in rays)
        {
            field.Children.Remove(ray);
        }

        rays.Clear();

        if (isConvex && (objectDistance >= focalDistance))
        {
            Line principalRay1 = new Line()
            {
                X1 = field.ActualWidth / 2 - objectDistance,
                Y1 = field.ActualHeight / 2 - projections[objectIndex].Item1.ActualHeight,
                X2 = field.ActualWidth / 2,
                Y2 = field.ActualHeight / 2 + h,
                StrokeThickness = 1,
                Stroke = new SolidColorBrush(Colors.Red),
                Visibility = RaysCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden
            };
            Line principalRay12 = new Line()
            {
                X1 = field.ActualWidth / 2,
                Y1 = field.ActualHeight / 2 + h,
                X2 = field.ActualWidth / 2 + f,
                Y2 = field.ActualHeight / 2 + h,
                StrokeThickness = 1,
                Stroke = new SolidColorBrush(Colors.Red),
                Visibility = RaysCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden
            };
            Line principalRay2 = new Line()
            {
                X1 = field.ActualWidth / 2 - objectDistance,
                Y1 = field.ActualHeight / 2 - projections[objectIndex].Item1.ActualHeight,
                X2 = field.ActualWidth / 2 + f,
                Y2 = field.ActualHeight / 2 + h,
                StrokeThickness = 1,
                Stroke = new SolidColorBrush(Colors.Green),
                Visibility = RaysCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden
            };
            Line principalRay3 = new Line()
            {
                X1 = field.ActualWidth / 2 - objectDistance,
                Y1 = field.ActualHeight / 2 - projections[objectIndex].Item1.ActualHeight,
                X2 = field.ActualWidth / 2,
                Y2 = field.ActualHeight / 2 - projections[objectIndex].Item1.ActualHeight,
                StrokeThickness = 1,
                Stroke = new SolidColorBrush(Colors.Blue),
                Visibility = RaysCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden
            };
            Line principalRay32 = new Line()
            {
                X1 = field.ActualWidth / 2,
                Y1 = field.ActualHeight / 2 - projections[objectIndex].Item1.ActualHeight,
                X2 = field.ActualWidth / 2 + f,
                Y2 = field.ActualHeight / 2 + h,
                StrokeThickness = 1,
                Stroke = new SolidColorBrush(Colors.Blue),
                Visibility = RaysCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden
            };

            field.Children.Add(principalRay1);
            field.Children.Add(principalRay12);
            field.Children.Add(principalRay2);
            field.Children.Add(principalRay3);
            field.Children.Add(principalRay32);

            rays.Add(principalRay1);
            rays.Add(principalRay12);
            rays.Add(principalRay2);
            rays.Add(principalRay3);
            rays.Add(principalRay32);
        }
        else if (isConvex)
        {
            Line principalRay1 = new Line()
            {
                X1 = field.ActualWidth / 2 - objectDistance,
                Y1 = field.ActualHeight / 2 - projections[objectIndex].Item1.ActualHeight,
                X2 = field.ActualWidth / 2,
                Y2 = field.ActualHeight / 2 - projections[objectIndex].Item1.ActualHeight,
                StrokeThickness = 1,
                Stroke = new SolidColorBrush(Colors.Blue),
                Visibility = RaysCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden
            };
            Line principalRay12 = new Line()
            {
                X1 = field.ActualWidth / 2,
                Y1 = field.ActualHeight / 2 - projections[objectIndex].Item1.ActualHeight,
                X2 = field.ActualWidth / 2 + focalDistance,
                Y2 = field.ActualHeight / 2,
                StrokeThickness = 1,
                Stroke = new SolidColorBrush(Colors.Blue),
                Visibility = RaysCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden
            };
            Line principalRay13 = new Line()
            {
                X1 = field.ActualWidth / 2,
                Y1 = field.ActualHeight / 2 - projections[objectIndex].Item1.ActualHeight,
                X2 = field.ActualWidth / 2 + f,
                Y2 = field.ActualHeight / 2 - h,
                StrokeThickness = 1,
                StrokeDashArray = {2, 2},
                Stroke = new SolidColorBrush(Colors.Blue),
                Visibility = RaysCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden
            };
            Line principalRay2 = new Line()
            {
                X1 = field.ActualWidth / 2 - objectDistance,
                Y1 = field.ActualHeight / 2 - projections[objectIndex].Item1.ActualHeight,
                X2 = field.ActualWidth / 2,
                Y2 = field.ActualHeight / 2,
                StrokeThickness = 1,
                Stroke = new SolidColorBrush(Colors.Green),
                Visibility = RaysCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden
            };
            Line principalRay21 = new Line()
            {
                X1 = field.ActualWidth / 2 - objectDistance,
                Y1 = field.ActualHeight / 2 - projections[objectIndex].Item1.ActualHeight,
                X2 = field.ActualWidth / 2 + f,
                Y2 = field.ActualHeight / 2 - h,
                StrokeThickness = 1,
                StrokeDashArray = {2, 2},
                Stroke = new SolidColorBrush(Colors.Green),
                Visibility = RaysCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden
            };
            

            field.Children.Add(principalRay1);
            field.Children.Add(principalRay12);
            field.Children.Add(principalRay13);
            field.Children.Add(principalRay2);
            field.Children.Add(principalRay21);

            rays.Add(principalRay1);
            rays.Add(principalRay12);
            rays.Add(principalRay13);
            rays.Add(principalRay2);
            rays.Add(principalRay21);
        }
        else
        {
            Line principalRay1 = new Line()
            {
                X1 = field.ActualWidth / 2 - objectDistance,
                Y1 = field.ActualHeight / 2 - projections[objectIndex].Item1.ActualHeight,
                X2 = field.ActualWidth / 2,
                Y2 = field.ActualHeight / 2 - projections[objectIndex].Item1.ActualHeight,
                StrokeThickness = 1,
                Stroke = new SolidColorBrush(Colors.Red),
                Visibility = RaysCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden
            };
            Line principalRay12 = new Line()
            {
                X1 = field.ActualWidth / 2,
                Y1 = field.ActualHeight / 2 - projections[objectIndex].Item1.ActualHeight,
                X2 = field.ActualWidth / 2 - focalDistance,
                Y2 = field.ActualHeight / 2,
                StrokeThickness = 1,
                StrokeDashArray= {2, 2},
                Stroke = new SolidColorBrush(Colors.Red),
                Visibility = RaysCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden
            };
            Line principalRay2 = new Line()
            {
                X1 = field.ActualWidth / 2 - objectDistance,
                Y1 = field.ActualHeight / 2 - projections[objectIndex].Item1.ActualHeight,
                X2 = field.ActualWidth / 2,
                Y2 = field.ActualHeight / 2,
                StrokeThickness = 1,
                Stroke = new SolidColorBrush(Colors.Green),
                Visibility = RaysCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden
            };
            Line principalRay3 = new Line()
            {
                X1 = field.ActualWidth / 2 - objectDistance,
                Y1 = field.ActualHeight / 2 - projections[objectIndex].Item1.ActualHeight,
                X2 = field.ActualWidth / 2,
                Y2 = field.ActualHeight / 2 - h,
                StrokeThickness = 1,
                Stroke = new SolidColorBrush(Colors.Blue),
                Visibility = RaysCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden
            };
            Line principalRay32 = new Line()
            {
                X1 = field.ActualWidth / 2,
                Y1 = field.ActualHeight / 2 - h,
                X2 = field.ActualWidth / 2 + f,
                Y2 = field.ActualHeight / 2 - h,
                StrokeThickness = 1,
                Stroke = new SolidColorBrush(Colors.Blue),
                StrokeDashArray= {2, 2},
                Visibility = RaysCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden
            };
            Line principalRay33 = new Line()
            {
                X1 = field.ActualWidth / 2,
                Y1 = field.ActualHeight / 2 - h,
                X2 = field.ActualWidth / 2 + focalDistance,
                Y2 = field.ActualHeight / 2,
                StrokeThickness = 1,
                Stroke = new SolidColorBrush(Colors.Blue),
                StrokeDashArray= {2, 2},
                Visibility = RaysCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden
            };
            field.Children.Add(principalRay1);
            field.Children.Add(principalRay12);
            field.Children.Add(principalRay2);
            field.Children.Add(principalRay3);
            field.Children.Add(principalRay32);
            field.Children.Add(principalRay33);

            rays.Add(principalRay1);
            rays.Add(principalRay12);
            rays.Add(principalRay2);
            rays.Add(principalRay3);
            rays.Add(principalRay32);
            rays.Add(principalRay33);
        }
    }

    private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
    {
        foreach (Line ray in rays)
        {
            ray.Visibility = Visibility.Visible;
        }
    }

    private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
    {
        foreach (Line ray in rays)
        {
            ray.Visibility = Visibility.Hidden;
        }
    }

    private void ObjectSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ComboBoxItem selectedItem = (ComboBoxItem)ObjectSelector.SelectedItem;
        if (selectedItem.Content == null)
            return;
        string selected = selectedItem.Content.ToString();

        int i;
        for (i = 0; i < amountOfOjects; i++)
        {
            if (objectNames[i].Equals(selected))
                break;
        }

        if (i is < 4 and >= 0)
        {
            projections[objectIndex].Item1.Visibility = Visibility.Hidden;
            projections[objectIndex].Item2.Visibility = Visibility.Hidden;
            projections[i].Item1.Visibility = Visibility.Visible;
            projections[i].Item2.Visibility = Visibility.Visible;

            objectIndex = i;
            
            Update();
        }
    }

    private void Update()
    {
        placeObjects();
        BuildRays();
    }
}