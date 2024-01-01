using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TacticsGame
{
    public class Unit
    {
        public String Name { get; }
        public int HP { get; set; }
        public ImageSource Image { get; }

        public Unit(string path)
        {
            Name = Path.GetFileName(path);
            Image = BitmapFrame.Create(new Uri(path));
        }

    }
}
