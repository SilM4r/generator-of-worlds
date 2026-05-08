using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace WorldGenerator
{
    public class Screen
    {
        private List<GridImag> imges = new List<GridImag>();


        public int x, y, imgsize, size_beetwen_imgs;

        private Grid MyGrid;


        private int marge_up = 20;
        private int marge_left = 20;

        public Screen(Grid MyGrid, int x, int y, int imgsize, int size_beetwen_imgs = 0)
        {
            this.x = x;
            this.y = y;
            this.MyGrid = MyGrid;
            this.imgsize = imgsize;
            this.size_beetwen_imgs = size_beetwen_imgs;

        }


        public void GenerateGrid()
        {
            GridImag img;
            int a = marge_up;
            int b = marge_left;

            MyGrid.Children.Clear();
            imges.Clear();

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    img = new GridImag($"img_{j}_{i}", "voda.png", "null", imgsize, b, a);
                    MyGrid.Children.Add(img.image);
                    b += imgsize + size_beetwen_imgs;

                    imges.Add(img);
                }
                a += imgsize + size_beetwen_imgs;
                b = marge_left;
            }
        }


        public void GenerateGridByObjectName(List<string> NameList, List<ObjectImage> ListObjectImage)
        {
            List<int> Array = new List<int>();
            bool isExist;

            foreach (string name in NameList)
            {
                isExist = false;
                foreach (ObjectImage image in ListObjectImage)
                {
                    if (image.name == name) 
                    {
                        Array.Add(image.id);
                        isExist = true;
                    }
                }

                if (!isExist) 
                {
                    MessageBox.Show("Funkce GenerateGridByObjectName dostala v seznamu jméno které nezná prosím zkotrolujte zadaná data jestli nedošlo k překlepu");
                    return;
                }
            }

            if (Array.Count != this.x * this.y)
            {
                MessageBox.Show("Snažíš se vygenrovat Grid pomocí listu který posíláš ale daný list není stejně velký jako zadná velikost Gridu v Screen");
                return;
            }

            GenerateGridByListOfObjectImage(Array, ListObjectImage);
        }

        public void GenerateGridByListOfObjectImage(List<int> Array, List<ObjectImage> ListObjectImage )
        {
            int y = marge_up;
            int x = marge_left;

            GridImag? img;

            MyGrid.Children.Clear();

            if ( Array.Count != this.x*this.y)
            {
                MessageBox.Show("Snažíš se vygenrovat Grid pomocí listu který posíláš ale daný list není stejně velký jako zadná velikost Gridu v Screen");
                return;
            }

            for (int k = 0; k < Array.Count; k++)
            {
                img = null;

                if (k % this.x == 0)
                {
                    x = marge_left;
                    y += imgsize + size_beetwen_imgs;
                }

                foreach (ObjectImage Oimage in ListObjectImage)
                {
                    if(Oimage.id == Array[k])
                    {
                        img = new GridImag($"img_{x}_{y}",Oimage.source, "null", imgsize, x, y);
                    }
                }

                if (img == null)
                {
                    img = new GridImag($"img_{x}_{y}", ListObjectImage[0].source, "null", imgsize, x, y);
                    MessageBox.Show("chyba");
                }

                MyGrid.Children.Add(img.image);

                x += imgsize + size_beetwen_imgs;

            }
        }

    }
}
