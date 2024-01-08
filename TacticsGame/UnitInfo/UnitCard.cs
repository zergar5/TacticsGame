using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Data;
using System.Xml.Linq;
using System.Xml;
using TacticsGame.Converters;
using SharpGL.SceneGraph.Primitives;
using System.Windows.Input;
using Grid = System.Windows.Controls.Grid;

namespace TacticsGame
{
    public class RoundCard
    {
        public string ImagePath { get; set; }
        public RoundCard(string imagePath)
        {
            ImagePath = imagePath;
        }
        public Border CreateRoundBorder(int round)
        {
            var border = new Border();
            border.BorderThickness = new Thickness(1);
            border.BorderBrush = Brushes.Black;
            border.Background = new SolidColorBrush(Color.FromRgb(0xd1, 0xd1, 0xd1));
            //border.Margin = new Thickness(10);
            //border.Padding = new Thickness(5);
            //StackPanel stackPanel = (StackPanel)Application.Current.MainWindow.FindName("unitsList");
            //border.SetBinding(Border.WidthProperty, new Binding("ActualWidth") { Source = stackPanel, Converter = new PercentConverter(), ConverterParameter = 0.1 });
            border.SetBinding(Border.WidthProperty, new Binding("ActualWidth") { Source = Application.Current.MainWindow, Converter = new PercentConverter(), ConverterParameter = 0.05 });

            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition());

            var image = new Image();
            image.Source = new BitmapImage(new Uri(ImagePath, UriKind.Absolute));
            image.Stretch = Stretch.Uniform;
            image.SetBinding(FrameworkElement.WidthProperty, new Binding("ActualWidth") { Source = border });

            border.SetBinding(FrameworkElement.HeightProperty, new Binding("ActualHeight") { Source = image, Converter = new PercentConverter(), ConverterParameter = 1.2 });

            Grid.SetRow(image, 0);
            
            var roundText = new TextBlock();
            roundText.Text = "ROUND " + round.ToString();
            roundText.Foreground = Brushes.White;
            roundText.TextAlignment = TextAlignment.Center;
            roundText.SetBinding(TextBlock.FontSizeProperty, new MultiBinding
            {
                Converter = new TextConverter(),
                Bindings = {
                new Binding("ActualHeight") { Source = border },
                new Binding("ActualWidth") { Source = border }
            }
            });
            //roundText.MinHeight = 10;
            roundText.Height = border.Height - image.Height;
            roundText.Background = new SolidColorBrush(Color.FromRgb(0x24, 0x1e, 0x29));
            Grid.SetRow(roundText, 1);

            grid.Children.Add(image);
            grid.Children.Add(roundText);
            border.Child = grid;

            return border;
        }
    }


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
            //border.Margin = new Thickness(10);
            //border.Padding = new Thickness(5);
            //StackPanel stackPanel = (StackPanel)Application.Current.MainWindow.FindName("unitsList");
            //border.SetBinding(Border.WidthProperty, new Binding("ActualWidth") { Source = stackPanel, Converter = new PercentConverter(), ConverterParameter = 0.1 });
            border.SetBinding(Border.WidthProperty, new Binding("ActualWidth") { Source = Application.Current.MainWindow, Converter = new PercentConverter(), ConverterParameter = 0.05 });

            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition());

            var image = new Image();
            image.Source = new BitmapImage(new Uri(ImagePath, UriKind.Absolute));
            image.Stretch = Stretch.Uniform;

            image.SetBinding(Image.WidthProperty, new Binding("ActualWidth") { Source = border});

            border.SetBinding(Border.HeightProperty, new Binding("ActualHeight") { Source = image, Converter = new PercentConverter(), ConverterParameter = 1.2 });
            
            Grid.SetRow(image, 0);

            var healthPoints = new ProgressBar();
            healthPoints.Value = (double)Health / MaxHealth * 100;
            healthPoints.MinHeight = 10;
            healthPoints.Height = border.Height - image.Height;
            healthPoints.Background = new SolidColorBrush(Color.FromRgb(0x24, 0x1e, 0x29));
            healthPoints.Foreground = new SolidColorBrush(Color.FromRgb(0x32, 0xCD, 0x32));
            Grid.SetRow(healthPoints, 1);

            grid.Children.Add(image);
            grid.Children.Add(healthPoints);

            border.Child = grid;

            return border;
        }


    }
}
