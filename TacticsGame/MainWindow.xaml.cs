using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SharpGL.WPF;
using TacticsGame;

namespace TacticsGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string _path = "C:\\Programming\\UNI_Projects\\TacticsGame\\TacticsGame\\UIcons";
        private List<Unit> _units = new List<Unit>{
                new(_path + "\\hero1.jpg", 80),
                new(_path + "\\hero2.jpg", 70),
                new(_path + "\\hero1.jpg", 70),
                new(_path + "\\hero1.jpg", 70),
                new(_path + "\\hero1.jpg", 70),
                new(_path + "\\hero2.jpg", 1000)};
        private int _round = 1;
        private int _info = 0;

        public MainWindow()
        {
            InitializeComponent();
            //var units = new List<Image>();
            //unitsList.ItemsSource = units;
            
            string[] imagesPath = Directory.GetFiles(_path);

            //var linkedListUnits = new LinkedList<Unit>(_units);

            this.Loaded += FillInTheQueue;
            
        }

        private void FillInTheQueue(object sender, RoutedEventArgs e)
        {
          
            while (unitsList.Children.Count < 10)
            {
                if (_round != 1 && _info == 6)
                {
                    var plug = new StackPanel();

                    var roundText = new TextBlock()
                    {
                        Height = 95,
                        Width = 50,
                        FontSize = 16,
                        Text = _round.ToString(),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    plug.Children.Add(roundText);
                    unitsList.Children.Add(plug);
                    _info = 0;
                }

                Image img;
                TextBlock hp;
                StackPanel group;
                
                
                var unit = _units[0];
                img = new Image();
                img.Height = 60;
                img.Width = 50;
                img.Source = unit.Image;
                hp = new TextBlock()
                {
                    Text = unit.HP.ToString(),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                group = new StackPanel()
                {
                    Orientation = Orientation.Vertical
                };
                img.Unloaded += FillInTheQueue;
                group.Children.Add(img);
                group.Children.Add(hp);
                group.MouseLeftButtonUp += Remove_Card;
                unitsList.Children.Add(group);

                _info++;

                _units.RemoveAt(0);
                _units.Add(unit);
                if (_info == 6)
                {
                    _round++;
                }
            }



        }
        private void Select_Unit(object sender, RoutedEventArgs e)
        {
            ListBox list = (ListBox)sender;
            var selectedUnitIndex = list.SelectedIndex;
        }
        private void Remove_Card(object sender, RoutedEventArgs e)
        {
            unitsList.Children.Remove((StackPanel)sender);
            var firstCard = (StackPanel)unitsList.Children[0];
            var secondCard = (StackPanel)unitsList.Children[1];
            if (firstCard.Children.Count == 1 && secondCard.Children.Count == 1 )
            {
                unitsList.Children.Remove(firstCard);
            }
            
        }
        private void OpenGLDraw(object sender, OpenGLRoutedEventArgs args)
        {
           
        }

        private void OpenGLInitialized(object sender, OpenGLRoutedEventArgs args)
        {
           
        }

        private void OpenGLControlResized(object sender, OpenGLRoutedEventArgs args)
        {
            
        }
    }
}
