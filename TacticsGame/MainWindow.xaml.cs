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
                new(_path + "\\hero1.jpg", 100),
                new(_path + "\\hero2.jpg", 70),
                new(_path + "\\hero1.jpg", 50),
                new(_path + "\\hero2.jpg", 1000)};
        private int round = 1;

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
                Image img;
                TextBlock hp;
                StackPanel group;
                var plug = new StackPanel();

                var roundText = new TextBlock()
                {
                    Height = 100,
                    Width = 60,
                    FontSize = 16,
                    Text = round.ToString(),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                plug.Children.Add(roundText);
                unitsList.Children.Add(plug);

                foreach (Unit unit in _units)
                {
                    img = new Image();
                    img.Height = 80;
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
                    group.MouseLeftButtonUp += Unit_Select;
                    unitsList.Children.Add(group);
                }

                round++;
            }      

                
            //unitsList.MouseLeftButtonUp += Select_Unit;
            
        }
        private void Select_Unit(object sender, RoutedEventArgs e)
        {
            ListBox list = (ListBox)sender;
            var selectedUnitIndex = list.SelectedIndex;
        }
        private void Unit_Select(object sender, RoutedEventArgs e)
        {
            unitsList.Children.Remove(unitsList.Children[0]);
            if (unitsList.Children.Count == 1)
            {
                var currentUnit = (StackPanel)unitsList.Children[0];
                var currentText = (TextBlock)currentUnit.Children[0];
                unitsList.Children.Remove(unitsList.Children[0]);
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
