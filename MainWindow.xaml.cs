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

namespace WorldGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<ObjectImage> listImg = new List<ObjectImage>();

        public MainWindow()
        {
            InitializeComponent();

            Screen s = new Screen(MyGrid, 243, 243, 5);

            //s.generateGrid();


            new ObjectImage("voda",   "voda.png",   new List<string> { "písek", "hvoda", "voda" },  new List<int> { 1, 3, 3 }, listImg);
            new ObjectImage("zem",    "zem.png",    new List<string> { "kytka", "zem", "písek" },   new List<int> { 1, 6, 1 }, listImg);
            new ObjectImage("písek",  "písek.png",  new List<string> { "zem", "písek", "voda" },    new List<int> {8,1,6},    listImg);
            new ObjectImage("kytka",  "kytka.png",  new List<string> { "zem"},                      new List<int> {1}, listImg);
            new ObjectImage("hvoda",  "hvoda.png",  new List<string> { "hvoda", "voda" },           new List<int> { 1, 2 }, listImg);


            //s.GenerateGridByListOfObjectImage(new List<int> {1,2,1,2,2,2,1,2,1}, listImg);
            //s.GenerateGridByObjectName(new List<string> { "zem", "voda", "zem", "voda", "zem", "voda", "zem", "voda", "zem" }, listImg);

            WaveFunctionCollapse w = new WaveFunctionCollapse(s,listImg);
            RandomScale rs = new RandomScale(s);

            List<int> arrayValue = rs.GenerateWorld();

            s.GenerateGridByListOfObjectImage(arrayValue, listImg);


        }
    }
}
