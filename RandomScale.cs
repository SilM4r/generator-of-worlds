using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace WorldGenerator
{
    public class RandomScale
    {

        private ScreenScale screen;
        private Random rnd = new Random();
        // private List<ObjectImage> objectImages;

        public RandomScale(Screen screen)
        {
            this.screen = new ScreenScale(screen);
        }

        public List<int> GenerateWorld()
        {

            screen.UpScaleScreen(-27);

            List<int> finalId =  RandomListGenerator(screen.size);

            screen.UpScaleScreen(3);
            finalId = IncreaseBy(screen.scale, finalId);
            finalId = Sleek(2, finalId);
            finalId = Sleek(2, finalId);

            screen.UpScaleScreen(3);
            finalId = IncreaseBy(screen.scale, finalId);
            finalId = Sleek(2, finalId);
            finalId = Sleek(2, finalId);

            screen.UpScaleScreen(3);
            finalId = IncreaseBy(screen.scale, finalId);
            finalId = Sleek(2, finalId);
            finalId = Sleek(2, finalId);

            finalId = CreateBorder(3,1,2, finalId);
            finalId = Sleek(3, finalId);

            return finalId;
        }


        private List<int> CreateBorder(int id, int around_what_id, int replace_block_id,List<int> originalList) 
        {
            List<int> resultList = new List<int>();

            int position = -1;
            foreach (int block in originalList) 
            {
                position += 1;
                if (block != replace_block_id)
                {
                    resultList.Add(block);
                    continue;
                }

                if (ProbabilityOfSameNeighbors(position, around_what_id, originalList) > 20)
                {
                    resultList.Add(id);
                }

                else
                {
                    resultList.Add(block);
                }
            }

            return resultList;
        }

        private List<int> Sleek(int id,List<int> originalList)
        {
            int position = -1;
            int probability = 0;
            List<int> resultList = new List<int>();

            foreach (int i in originalList) 
            {
                probability = 0;
                position++;
                if (i == id)
                {
                    resultList.Add(i);
                    continue;
                }

                probability = ProbabilityOfSameNeighbors(position, id, originalList);

                if (probability <= 0)
                {
                    resultList.Add(i);
                    continue;
                }

                else if (probability == 100) 
                {
                    resultList.Add(id);
                    continue;
                }

                if (rnd.Next(0, 101) <= probability)
                {
                    resultList.Add(id);
                }
                else
                {
                    resultList.Add(i);
                }
            }

            return resultList;
        }

        private List<int> IncreaseBy(int scale, List<int> originalList)
        {
            List<int> resultList = new List<int> ();

            List<int> oneRow = new List<int>();
            foreach (int id in originalList)
            {
                for (int i = 0; i < scale; i++)
                {
                    oneRow.Add(id);
                }
                    
                if (oneRow.Count % screen.x == 0)
                {
                    for (int i = 0; i < scale; i++)
                    {
                        foreach (int id2 in oneRow)
                        {
                            resultList.Add(id2);
                        }
                    }
                    oneRow.Clear();

                }

            }
            return resultList;
        }

        private int ProbabilityOfSameNeighbors(int position, int id, List<int> originalList)
        {
            int probability = 0;

            // top
            if (position - screen.x >= 0)
            {
                if (originalList[position - screen.x] == id)
                {
                    probability += 25;
                }
            }
            // bottom
            if (position + screen.x < screen.x * screen.y)
            {
                if (originalList[position + screen.x] == id)
                {
                    probability += 25; 
                }
            }
            // left
            if ((position - 1) % (screen.x + 1) != 0 && (position % screen.x) != 0)
            {
                if (originalList[position - 1] == id)
                {
                    probability += 25;
                }
            }
            // right
            if (((position + 1) % screen.x) != 0 && (position % screen.x + 1) != 0)
            {
                if (originalList[position + 1] == id)
                {
                    probability += 25;
                }
            }
            return probability;
        }

        private List<int> RandomListGenerator(int size)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < size; i++)
            {
                result.Add(rnd.Next(1, 3));
            }


            if (result.Contains(1) && result.Contains(2))
            {
                return result;
            }

            else
            {
                int position = rnd.Next(1, result.Count());
                int id = result[position];

                if (id == 1)
                {
                    result[position] = 2;
                }

                else
                {
                    result[position] = 1;
                }

                return result;
            }
        }
    }
}
