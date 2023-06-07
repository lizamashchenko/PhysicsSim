using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PhysicsSim;

public class FlyingObject
{
    private Image image;
    private double weight;
    
    public FlyingObject()
    {
        
    }

    public FlyingObject(string imageSrc, double weight)
    {
        this.image = new Image();
        image.Source = new BitmapImage(new Uri(imageSrc));
        image.Width = 100;
        image.Visibility = Visibility.Hidden;
        Panel.SetZIndex(image, 0);
        this.weight = weight;
    }

    public void Hide()
    {
        image.Visibility = Visibility.Hidden;
    }
    public Image getImage()
    {
        return image;
    }

    public void Show()
    {
        image.Visibility = Visibility.Visible;
    }

    public double getWeight()
    {
        return weight;
    }
    public void SetObject(double x, double y, double angle)
    {
        double canvasWidth = ((Canvas)image.Parent).ActualWidth;
        double canvasHeight = ((Canvas)image.Parent).ActualHeight;
        double imageWidth = image.ActualWidth;
        double imageHeight = image.ActualHeight;

        Canvas.SetLeft(image, x - imageHeight / 2);
        Canvas.SetTop(image, canvasHeight - y - imageHeight - 10);
        image.RenderTransform = new RotateTransform(360 - angle, imageWidth / 2, imageHeight);
    }
}