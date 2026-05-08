using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Input;

namespace WorldGenerator
{
    public class GridImag
    {
        private string path = AppDomain.CurrentDomain.BaseDirectory;

        public Image image;

        public int poziceX, poziceY;

        public string Type { get; set; }

        public GridImag(string IMGname, string sourceName, string type, int size, int x, int y)
        {
            Type = type;

            string[] img = IMGname.Split("_");

            poziceX = int.Parse(img[1]);
            poziceY = int.Parse(img[2]);


            image = new Image()
            {
                Name = IMGname,
                Width = size,
                Height = size,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(x, y, 0, 0)
            };

            BitmapImage bitmap = new BitmapImage();

            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path2() + sourceName, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();

            image.Source = bitmap;
        }
    

    private string path2()
    {
        string path2 = path.Substring(0, path.Length - 25);

        path2 += "img/";

        return path2;
    }

    }
}
