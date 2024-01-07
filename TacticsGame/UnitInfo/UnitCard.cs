using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Data;

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
            border.Width = double.NaN; // автоматический размер по содержимому
            border.Height = double.NaN;
            StackPanel stackPanel = (StackPanel)Application.Current.MainWindow.FindName("unitsList");
            border.SetBinding(Border.WidthProperty, new Binding("ActualWidth") { Source = stackPanel, Converter = new PercentConverter(), ConverterParameter = 0.1 });
            border.SetBinding(Border.HeightProperty, new Binding("ActualHeight") { Source = stackPanel });

            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition());

            var image = new Image();
            image.Source = new BitmapImage(new Uri(ImagePath, UriKind.Absolute));
            //image.Stretch = Stretch.Uniform;
            //image.MaxWidth = double.PositiveInfinity;
            //image.MaxHeight = double.PositiveInfinity;
            image.SetBinding(Image.MaxWidthProperty, new Binding("ActualWidth") { Source = border, Converter = new PercentConverter(), ConverterParameter = 0.5 });
            image.SetBinding(Image.MaxHeightProperty, new Binding("ActualHeight") { Source = border, Converter = new PercentConverter(), ConverterParameter = 0.5 });
            Grid.SetRow(image, 0);

            var healthPoints = new ProgressBar();
            //healthPoints.Value = (double)Health / MaxHealth;
            healthPoints.Value = Health;
            healthPoints.MaxHeight = 10;
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
