using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using TacticsGame.Converters;

namespace TacticsGame.Cards
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
}
