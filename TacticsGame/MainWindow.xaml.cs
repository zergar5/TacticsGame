using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SharpGL.WPF;

namespace TacticsGame
{
    public partial class MainWindow : Window
    {
        private static string _path = "C:\\Programming\\UNI_Projects\\TacticsGame\\TacticsGame\\UIcons";
        //private List<Unit> _units = new List<Unit>{
        //        new(_path + "\\hero1.jpg", 80),
        //        new(_path + "\\hero2.jpg", 70),
        //        new(_path + "\\hero1.jpg", 70),
        //        new(_path + "\\hero1.jpg", 70),
        //        new(_path + "\\hero1.jpg", 70),
        //        new(_path + "\\hero2.jpg", 1000)};
        private List<UnitCard> _units = new List<UnitCard>{
                new ("Unit 1", "C:\\Programming\\UNI_Projects\\TacticsGame\\TacticsGame\\UIcons\\u001.jpg", 75, 100),
                new ("Unit 2", "C:\\Programming\\UNI_Projects\\TacticsGame\\TacticsGame\\UIcons\\u002.jpg", 80, 100),
                new ("Unit 3", "C:\\Programming\\UNI_Projects\\TacticsGame\\TacticsGame\\UIcons\\u003.jpg", 45, 100),
                new ("Unit 4", "C:\\Programming\\UNI_Projects\\TacticsGame\\TacticsGame\\UIcons\\u004.jpg", 14, 100),
                new ("Unit 5", "C:\\Programming\\UNI_Projects\\TacticsGame\\TacticsGame\\UIcons\\u005.jpg", 98, 100),
                new ("Unit 6", "C:\\Programming\\UNI_Projects\\TacticsGame\\TacticsGame\\UIcons\\u006.jpg", 100, 100)};
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
                    AddRoundCard(unitsList);
                    _info = 0;
                }

                StackPanel group;

                var unit = _units[0];

                group = new StackPanel()
                {
                    Orientation = Orientation.Vertical
                };
             
                var unitCard = unit.CreateBorder();
                unitCard.Unloaded += FillInTheQueue;
                unitsList.Children.Add(unitCard);

                //AddUnitImage(unit, group);
                //AddHealthBar(group);
                //group.MouseLeftButtonUp += Remove_Card;
                //unitsList.Children.Add(group);

                _info++;

                _units.RemoveAt(0);
                _units.Add(unit);
                if (_info == 6)
                {
                    _round++;
                }
            }



        }
        private void AddRoundCard(StackPanel unitsList)
        {
            var roundCard = new StackPanel();

            var roundText = new TextBlock()
            {
                Height = 95,
                Width = 50,
                FontSize = 16,
                Text = _round.ToString(),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            roundCard.Children.Add(roundText);
            unitsList.Children.Add(roundCard);
        }
        //private void AddUnitImage(Unit unit, StackPanel group)
        //{
        //    Image img = new Image();
        //    img.Height = 60;
        //    img.Source = unit.Image;
        //    img.Unloaded += FillInTheQueue;

        //    group.Children.Add(img);
        //}
        //private void AddHealthBar(StackPanel group)
        //{
        //    ProgressBar healthBar = new ProgressBar();
        //    healthBar.Minimum = 0;
        //    healthBar.Maximum = 100;
        //    healthBar.Value = 100;
        //    healthBar.Height = 10;
        //    healthBar.Width = 45;
        //    healthBar.Background = new SolidColorBrush(Color.FromArgb(0x69, 0x69, 0x69, 0x69));
        //    healthBar.Foreground = new SolidColorBrush(Color.FromRgb(0x32, 0xCD, 0x32));
        //    healthBar.Loaded += HealthBar_Loaded; // подписываемся на событие Loaded
        //                                          // добавляем ProgressBar на контейнер
        //    group.Children.Add(healthBar);
        //}

        //private void HealthBar_Loaded(object sender, RoutedEventArgs e)
        //{
        //    if (sender is ProgressBar healthBar)
        //    {
        //        // выполнение инициализации ProgressBar
        //        int hp = 100; // пример значения HP
        //        UpdateHealthBar(healthBar, hp); // вызываем метод обновления HP
        //    }
        //}

        //private void UpdateHealthBar(ProgressBar healthBar, int hp)
        //{
        //    healthBar.Value = hp;
        //}
        private void Remove_Card(object sender, RoutedEventArgs e)
        {
            unitsList.Children.Remove((StackPanel)sender);
            var firstCard = (StackPanel)unitsList.Children[0];
            var secondCard = (StackPanel)unitsList.Children[1];
            if (firstCard.Children.Count == 1 && secondCard.Children.Count == 1)
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
