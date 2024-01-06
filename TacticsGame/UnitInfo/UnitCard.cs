using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace TacticsGame
{
    public class UnitCard
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }

        public UnitCard(string name, string imagePath, int health, int maxHealth)
        {
            Name = name;
            ImagePath = imagePath;
            Health = health;
            MaxHealth = maxHealth;
        }

        public Border CreateBorder()
        {
            var border = new Border();
            border.BorderThickness = new Thickness(1);
            border.BorderBrush = Brushes.Black;
            border.Margin = new Thickness(10);
            border.Padding = new Thickness(5);

            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition());

            var image = new Image();
            image.Source = new BitmapImage(new Uri(ImagePath, UriKind.Absolute));
            image.Stretch = Stretch.Uniform;
            Grid.SetRow(image, 0);

            var healthPoints = new ProgressBar();
            //healthPoints.Value = (double)Health / MaxHealth;
            healthPoints.Value = Health;
            healthPoints.MinHeight = 10;
            healthPoints.Width = image.Width;
            healthPoints.Background = new SolidColorBrush(Color.FromArgb(0x69, 0x69, 0x69, 0x69));
            healthPoints.Foreground = new SolidColorBrush(Color.FromRgb(0x32, 0xCD, 0x32));
            Grid.SetRow(healthPoints, 1);

            grid.Children.Add(image);
            grid.Children.Add(healthPoints);

            border.Child = grid;

            return border;
        }


    }
}
