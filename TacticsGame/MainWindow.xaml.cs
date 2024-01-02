﻿using System;
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
        public MainWindow()
        {
            InitializeComponent();
            //var units = new List<Image>();
            //unitsList.ItemsSource = units;
            var path = "C:\\Programming\\UNI_Projects\\TacticsGame\\TacticsGame\\UIcons";
            string[] imagesPath = Directory.GetFiles(path);
            Image img;
            TextBlock hp;
            StackPanel group;

            var units = new List<Unit>() {
                new Unit(path + "\\hero1.jpg", 80),
                new Unit(path + "\\hero1.jpg", 100),
                new Unit(path + "\\hero2.jpg", 70),
                new Unit(path + "\\hero1.jpg", 50),
                new Unit(path + "\\hero2.jpg", 1000)};


            foreach (Unit unit in units)
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
                group.Children.Add(img);
                group.Children.Add(hp);
                unitsList.Children.Add(group);
            }

            //unitsList.Items.Refresh();

            //GameData gameData = new GameData();
            //gameData.Hero1Image = "C:\\Programming\\UNI_Projects\\TacticsGame\\TacticsGame\\UIcons\\hero1.jpg";            
            //gameData.Hero1HP = 50;
            //gameData.Hero2Image = "C:\\Programming\\UNI_Projects\\TacticsGame\\TacticsGame\\UIcons\\hero2.jpg";
            //gameData.Hero2HP = 90;
            //gameData.Hero3Image = "hero3.png";
            //gameData.Hero3HP = 100;
            //gameData.HPWidth = 100;

            //GamePanel.DataContext = gameData;

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
