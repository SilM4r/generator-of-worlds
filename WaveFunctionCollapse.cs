using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace WorldGenerator
{
    public class WaveFunctionCollapse
    {
        private Screen screen;

        private List<ObjectImage> objectImages;
        private List<List<WFCobj>> listOfPossibleValues = new List<List<WFCobj>>();
        private List<int> finalIdlist = new List<int>();


        private Random rnd = new Random();
        public WaveFunctionCollapse(Screen screen, List<ObjectImage> objectImages) 
        {
            this.screen = screen;
            this.objectImages = objectImages;
        }


        public List<int> GenerateWorld()
        {
            List<WFCobj> array = new List<WFCobj>();
            List<int> PossiblePositions = new List<int>();

            // vytvoří prázdný seznam ve kterém jsou seznamy a v těch jsou všechny možností v jednom daném bloku. {{1,2,3},{1,2,3},{1,2,3},...}

            for (int i = 0; i < (screen.x * screen.y); i++) 
            {
                foreach (ObjectImage image in objectImages)
                {
                    array.Add(new WFCobj(image,1));
                }

                listOfPossibleValues.Add(array);
                finalIdlist.Add(0);
                array = new List<WFCobj>();
            }

            int start_position = rnd.Next(0, screen.x * screen.y);
            int value_start = SelectValueByRarity(listOfPossibleValues[start_position]);

            refreshNeighborsValue(listOfPossibleValues[start_position][value_start].obj, start_position);


            while (finalIdlist.Contains(0))
            {
                int minNum = objectImages.Count()+1;
                PossiblePositions = new List<int>();
                for (int i = 0; i < (screen.x * screen.y); i++)
                {
                        
                    if (listOfPossibleValues[i].Count() < minNum && finalIdlist[i] == 0)
                    {
                        minNum = listOfPossibleValues[i].Count();
                        PossiblePositions = new List<int>();
                    }

                    if (listOfPossibleValues[i].Count() == minNum && finalIdlist[i] == 0)
                    {
                        PossiblePositions.Add(i);
                    }
                }


                int position = rnd.Next(0, PossiblePositions.Count());

                position = PossiblePositions[position];

                int value = SelectValueByRarity(listOfPossibleValues[position]);
                ObjectImage objValue = listOfPossibleValues[position][value].obj;

                refreshNeighborsValue(objValue,position);

            }

            return finalIdlist;


        }


        private void refreshNeighborsValue(ObjectImage obj, int position)
        {
            // nová hodnota na pozici
            listOfPossibleValues[position] = new List<WFCobj> { new WFCobj(obj,0) };
            finalIdlist[position] = obj.id;

            bool[] isTouching = { false, false, false, false};

            // kotroluje se jestli nepřesahuje okraj


            // top
            if (position - screen.x >= 0)
            {
                change(position - screen.x, obj);
                isTouching[0] = true;
            }

            // bot
            if (position + screen.x < screen.x * screen.y)
            {
                change(position + screen.x, obj);
                isTouching[1] = true;
            }

            // left
            if ((position - 1) % (screen.x +1) != 0 && (position % screen.x) != 0)
            {
                change(position - 1, obj);
                isTouching[2] = true;
            }

            // right
            if (((position + 1) % screen.x) != 0 && (position % screen.x+1) != 0)
            {
                change(position + 1, obj);
                isTouching[3] = true;
            }

            if (isTouching[0] && isTouching[2]) 
            {
                change(position - screen.x - 1, obj);
            }

            if (isTouching[0] && isTouching[3])
            {
                change(position - screen.x + 1, obj);
            }

            if (isTouching[1] && isTouching[2])
            {
                change(position + screen.x - 1, obj);
            }

            if (isTouching[1] && isTouching[3])
            {
                change(position + screen.x + 1, obj);
            }


            //debugLog();
        }


        private void change(int pozice, ObjectImage obj) 
        {

            if (finalIdlist[pozice] != 0) 
            {
                return;
            }

            listOfPossibleValues[pozice] = merge_ObjList_and_StringList(listOfPossibleValues[pozice], obj);
        }


        private List<WFCobj> merge_ObjList_and_StringList(List<WFCobj> list1, ObjectImage objectImage)
        {
            List<WFCobj> finalList = new List<WFCobj>();
            foreach (WFCobj obj in list1) 
            {
                int a = 0;
                foreach (string name in objectImage.neighboringObjects)
                {
                    if (obj.obj.name == name)
                    {
                        finalList.Add(new WFCobj(obj.obj, (objectImage.rarity[a] + obj.rarity) / 2));
                    }
                    a++;
                }
            }

            return finalList;

        }

        private List<string> mergeListStrings(List<string> list1, List<string> list2)
        {
            List<string> finalList = new List<string>();

            foreach (string name in list1)
            {
                foreach (string name2 in list2)
                {
                    if (name == name2)
                    {
                        finalList.Add(name);
                    }
                }
            }

            return finalList;

        }


        private int SelectValueByRarity(List<WFCobj> objectImages)
        {
            int final = 0;
            List<int> listInsts = new List<int>();


            int a = -1;
            foreach (WFCobj obj in objectImages) 
            {
                a++;
                for (int i = 0; i < obj.rarity + 0.5; i++)
                {
                    listInsts.Add(a);
                }
            }

            int random = rnd.Next(0, listInsts.Count);

            final = listInsts[random];

            return final;

        }


        private void debugLog() 
        {
            string s = "";

            int i = 0;

            foreach (List<WFCobj> items in listOfPossibleValues) 
            {
                foreach (WFCobj value in items) 
                {
                    s += value.obj.name + " ";
                }

                s += "/ ";
                i++;

                if (i == screen.x)
                {
                    s += "\n";
                    i = 0;
                }

                

            }

            MessageBox.Show(s);

        }

    }
}
