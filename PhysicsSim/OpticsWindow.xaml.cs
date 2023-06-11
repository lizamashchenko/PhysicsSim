using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.ActiveDirectory;
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
    private bool isFirstStart = false;

    private double lensHeight = 100;
    private double concaveWidth = 26;
    private double convexWidth = 44;
    private double focalDistance = 40;
    private double objectDistance = 200;
    private bool isConvex = false;

    private Point objectBottom;
    private Point lensCenter;

    private Ellipse f1, f2;
    private int focusRadius = 5;

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
            Stroke = new SolidColorBrush(Colors.Black)
        };
        field.Children.Add(xAxis);

        Line yAxis = new Line()
        {
            X1 = field.ActualWidth / 2,
            X2 = field.ActualWidth / 2,
            Y1 = field.ActualHeight,
            Y2 = 0,
            StrokeThickness = 2,
            Stroke = new SolidColorBrush(Colors.Gray),
        };
        field.Children.Add(yAxis);

        f1 = new Ellipse()
        {
            Height = 2 * focusRadius,
            Width = 2 * focusRadius,
            Fill = new SolidColorBrush(Colors.Aqua)
        };
        f2 = new Ellipse()
        {
            Height = 2 * focusRadius,
            Width = 2 * focusRadius,
            Fill = new SolidColorBrush(Colors.Aqua)
        };
        setFocuses();
        field.Children.Add(f1);
        field.Children.Add(f2);
        
        concaveLens.Height = lensHeight;
        convexLens.Height = lensHeight;
        
        Canvas.SetLeft(concaveLens, field.ActualWidth / 2 - concaveWidth / 2);
        Canvas.SetBottom(concaveLens, field.ActualHeight / 2 - lensHeight / 2);
        concaveLens.Visibility = Visibility.Hidden;
        field.Children.Add(concaveLens);

        Canvas.SetLeft(convexLens, field.ActualWidth / 2 - convexWidth / 2);
        Canvas.SetBottom(convexLens, field.ActualHeight / 2 - lensHeight / 2);
        convexLens.Visibility = Visibility.Hidden;
        field.Children.Add(convexLens);
        
        foreach (Tuple<Image, Image> t in projections)
        {
            field.Children.Add(t.Item1);
            field.Children.Add(t.Item2);
        }

        FocusSlider.Value = focalDistance;
        DistanceSlider.Value = objectDistance;

        Ellipse dist = new Ellipse()
        {
            Height = 2,
            Width = 2,
            Fill = new SolidColorBrush(Colors.Red)
        };
        Canvas.SetBottom(dist, field.ActualHeight / 2);
        Canvas.SetLeft(dist, field.ActualWidth / 2 - objectDistance);
        field.Children.Add(dist);
    }

    private void setFocuses()
    {
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
        objectBottom = new Point(field.ActualWidth / 2 - objectDistance  - projections[objectIndex].Item1.ActualWidth / 2, field.ActualHeight / 2);

        double f = focalDistance * objectDistance / (objectDistance - focalDistance);

        foreach (Tuple<Image, Image> t in projections)
        {
            t.Item2.Height = focalDistance * lensHeight / objectDistance;
            if (isConvex)
            {
                t.Item2.RenderTransform = new ScaleTransform(1, -1);
                Canvas.SetLeft(t.Item2, lensCenter.X + f - t.Item2.ActualWidth / 2);
                Canvas.SetTop(t.Item2, field.ActualHeight / 2);
            }
            else
            {
                t.Item2.RenderTransform = new ScaleTransform(1, 1);
                Canvas.SetLeft(t.Item2, lensCenter.X - f - t.Item2.ActualWidth / 2);
                Canvas.SetBottom(t.Item2, field.ActualHeight / 2);
            }
        }
    }
    private void DiameterSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        try
        {
            objectDistance = DistanceSlider.Value;
            Canvas.SetLeft(projections[objectIndex].Item1, field.ActualWidth / 2 - objectDistance - projections[objectIndex].Item1.ActualWidth / 2);
            Update();
        }
        catch (Exception exception)
        {
            DistanceSlider.Value = 1;
            MessageBox.Show("Input a correct numeric value");
        }
    }

    private void FocusSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        try
        {
            focalDistance = FocusSlider.Value;
            setFocuses();
            Update();
        }
        catch (Exception exception)
        {
            FocusSlider.Value = 1;
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

        if (isConvex)
        {
            // Line principalRay1 = new Line()
            // {
            //     X1 = field.ActualWidth / 2 - objectDistance + projections[objectIndex].Item1.ActualWidth / 2,
            //     Y1 = field.ActualHeight / 2 - projections[objectIndex].Item1.ActualHeight,
            //     X2 = field.ActualWidth / 2  - focalDistance + focusRadius,
            //     Y2 = field.ActualHeight / 2,
            //     StrokeThickness = 1,
            //     Stroke = new SolidColorBrush(Colors.Red),
            //     Visibility = RaysCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden
            // };
            // Line principalRay2 = new Line()
            // {
            //     X1 = field.ActualWidth / 2 - objectDistance + projections[objectIndex].Item1.ActualWidth / 2,
            //     Y1 = field.ActualHeight / 2 - projections[objectIndex].Item1.ActualHeight,
            //     X2 = field.ActualWidth / 2 + focalDistance * objectDistance / (objectDistance - focalDistance) + projections[objectIndex].Item2.ActualWidth / 2,
            //     Y2 = field.ActualHeight / 2 + projections[objectIndex].Item2.ActualHeight,
            //     StrokeThickness = 1,
            //     Stroke = new SolidColorBrush(Colors.Green),
            //     Visibility = RaysCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden
            // };
            // Line principalRay3 = new Line()
            // {
            //     X1 = field.ActualWidth / 2 - focalDistance + focusRadius,
            //     Y1 = field.ActualHeight / 2,
            //     X2 = field.ActualWidth / 2 + focalDistance * objectDistance / (objectDistance - focalDistance) + projections[objectIndex].Item2.ActualWidth / 2,
            //     Y2 = field.ActualHeight / 2 + projections[objectIndex].Item2.ActualHeight,
            //     StrokeThickness = 1,
            //     Stroke = new SolidColorBrush(Colors.Blue),
            //     Visibility = RaysCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden
            // };
            Line principalRay1 = new Line()
            {
                X1 = field.ActualWidth / 2 - objectDistance,
                Y1 = field.ActualHeight / 2 - projections[objectIndex].Item1.ActualHeight,
                X2 = field.ActualWidth / 2,
                Y2 = field.ActualHeight / 2 + projections[objectIndex].Item2.ActualHeight,
                StrokeThickness = 1,
                Stroke = new SolidColorBrush(Colors.Red),
                Visibility = RaysCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden
            };
            Line principalRay12 = new Line()
            {
                X2 = field.ActualWidth / 2 + focalDistance * objectDistance / (objectDistance - focalDistance) + projections[objectIndex].Item2.ActualWidth / 2,
                Y2 = field.ActualHeight / 2 + projections[objectIndex].Item2.ActualHeight,
                X1 = field.ActualWidth / 2,
                Y1 = field.ActualHeight / 2 + projections[objectIndex].Item2.ActualHeight,
                StrokeThickness = 1,
                Stroke = new SolidColorBrush(Colors.Red),
                Visibility = RaysCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden
            };
            Line principalRay2 = new Line()
            {
                X1 = field.ActualWidth / 2 - objectDistance + projections[objectIndex].Item1.ActualWidth / 2,
                Y1 = field.ActualHeight / 2 - projections[objectIndex].Item1.ActualHeight,
                X2 = field.ActualWidth / 2 + focalDistance * objectDistance / (objectDistance - focalDistance) + projections[objectIndex].Item2.ActualWidth / 2,
                Y2 = field.ActualHeight / 2 + projections[objectIndex].Item2.ActualHeight,
                StrokeThickness = 1,
                Stroke = new SolidColorBrush(Colors.Green),
                Visibility = RaysCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden
            };
            Line principalRay3 = new Line()
            {
                X1 = field.ActualWidth / 2 - objectDistance + projections[objectIndex].Item1.ActualWidth / 2,
                Y1 = field.ActualHeight / 2 - projections[objectIndex].Item1.ActualHeight,
                X2 = field.ActualWidth / 2,
                Y2 = field.ActualHeight / 2 - projections[objectIndex].Item1.ActualHeight,
                StrokeThickness = 1,
                Stroke = new SolidColorBrush(Colors.Blue),
                Visibility = RaysCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden
            };
            Line principalRay32 = new Line()
            {
                X2 = field.ActualWidth / 2 + focalDistance * objectDistance / (objectDistance - focalDistance) + projections[objectIndex].Item2.ActualWidth / 2,
                Y2 = field.ActualHeight / 2 + projections[objectIndex].Item2.ActualHeight,
                X1 = field.ActualWidth / 2,
                Y1 = field.ActualHeight / 2 - projections[objectIndex].Item1.ActualHeight,
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
        else
        {
            double objectHeight = projections[objectIndex].Item1.ActualHeight;
            double imageHeight = projections[objectIndex].Item2.ActualHeight;
            double objectBottomY = field.ActualHeight / 2;
            double imageTopY = field.ActualHeight / 2 - imageHeight;

            // Incident Ray
            Line incidentRay = new Line()
            {
                X1 = field.ActualWidth / 2 - objectDistance + projections[objectIndex].Item1.ActualWidth / 2,
                Y1 = objectBottomY,
                X2 = field.ActualWidth / 2,
                Y2 = field.ActualHeight / 2,
                StrokeThickness = 1,
                Stroke = new SolidColorBrush(Colors.Red),
                Visibility = RaysCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden
            };

            // Refracted Ray
            double refractedRayX = field.ActualWidth / 2 - (focalDistance - objectDistance) * (imageTopY - field.ActualHeight / 2) / focalDistance;
            Line refractedRay = new Line()
            {
                X1 = field.ActualWidth / 2,
                Y1 = field.ActualHeight / 2,
                X2 = refractedRayX,
                Y2 = imageTopY,
                StrokeThickness = 1,
                Stroke = new SolidColorBrush(Colors.Blue),
                Visibility = RaysCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden
            };

            field.Children.Add(incidentRay);
            field.Children.Add(refractedRay);

            rays.Add(incidentRay);
            rays.Add(refractedRay);
            
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