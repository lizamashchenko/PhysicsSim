using System;
using System.Windows;
using System.Windows.Controls;
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
        image.Source = new BitmapImage(new Uri("pack://application:,,,/images/missile.png"));
        image.Width = 100;
        image.Visibility = Visibility.Hidden;
        Panel.SetZIndex(image, 0);
        this.weight = weight;
    }
}