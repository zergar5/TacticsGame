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
using TacticsGame.Core.Dto;

namespace TacticsGame.Cards
{
    public class UnitCard
    {
        public UnitDto _unit;

        public UnitCard(UnitDto unit)
        {
            _unit = unit;
        }

        //public string GetId()
        //{
        //    return Id.ToString();
        //}

        public Border CreateUnitBorder()
        {
            var border = new Border();
            border.BorderThickness = new Thickness(1);
            border.BorderBrush = Brushes.Black;
            border.Name = "Unit" + _unit.UnitId.ToString();
            border.SetBinding(Border.WidthProperty, new Binding("ActualWidth") { Source = Application.Current.MainWindow, Converter = new PercentConverter(), ConverterParameter = 0.05 });

            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition());

            var image = new Image();
            image.Source = new BitmapImage(new Uri(_unit.Sprite, UriKind.Absolute));
            image.Stretch = Stretch.Uniform;

            image.SetBinding(Image.WidthProperty, new Binding("ActualWidth") { Source = border });

            border.SetBinding(Border.HeightProperty, new Binding("ActualHeight") { Source = image, Converter = new PercentConverter(), ConverterParameter = 1.2 });

            Grid.SetRow(image, 0);

            var healthPoints = new ProgressBar();
            healthPoints.Value = (double)_unit.RemainingWounds / _unit.Wounds * 100;
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
