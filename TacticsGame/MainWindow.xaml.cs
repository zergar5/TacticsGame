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
                ProgressBar hp;
                StackPanel group;
                
                
                var unit = _units[0];
                img = new Image();
                img.Height = 60;
                img.Source = unit.Image;
                
                
                group = new StackPanel()
                {
                    Orientation = Orientation.Vertical
                };
                img.Unloaded += FillInTheQueue;
                group.Children.Add(img);
                AddHealthBar(group);
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
        private void AddHealthBar(StackPanel group)
        {
            ProgressBar healthBar = new ProgressBar();
            healthBar.Minimum = 0;
            healthBar.Maximum = 100;
            healthBar.Value = 100;
            healthBar.Height = 10;
            healthBar.Width = 45;
            healthBar.Loaded += HealthBar_Loaded; // подписываемся на событие Loaded
                                                  // добавляем ProgressBar на контейнер
           group.Children.Add(healthBar);
        }

        private void HealthBar_Loaded(object sender, RoutedEventArgs e)
        {
            ProgressBar healthBar = sender as ProgressBar;
            if (healthBar != null)
            {
                // выполнение инициализации ProgressBar
                int hp = 100; // пример значения HP
                UpdateHealthBar(healthBar, hp); // вызываем метод обновления HP
            }
        }

        private void UpdateHealthBar(ProgressBar healthBar, int hp)
        {
            healthBar.Value = hp;
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
