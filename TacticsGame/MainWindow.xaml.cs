using System;
using System.Collections.Generic;
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
using SharpGL;
using SharpGL.WPF;
using TacticsGame.Core;
using TacticsGame.Core.BattlefieldGenerator;

namespace TacticsGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Game _game;
        private readonly BattlefieldGenerator _battlefieldGenerator = new();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenGLDraw(object sender, OpenGLRoutedEventArgs args)
        {
            _game.Render();
        }

        private void OpenGLInitialized(object sender, OpenGLRoutedEventArgs args)
        { 
            _game.InitRenderSystems(args.OpenGL);
        }

        private void OpenGLControlResized(object sender, OpenGLRoutedEventArgs args)
        {
            args.OpenGL.Ortho(0, ActualWidth, 0, ActualHeight, -1, 1);

            var battlefield = _battlefieldGenerator.Generate();
        }
    }
}
