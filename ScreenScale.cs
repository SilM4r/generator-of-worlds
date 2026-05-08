using System;

namespace WorldGenerator
{

    public class ScreenScale
    {
        private Screen screen;

        public int x;
        public int y;
        public int size;

        public int scale;

        public ScreenScale(Screen screen)
        {
            this.screen = screen;
            x = screen.x;
            y = screen.y;

            size = screen.y * screen.x;

            scale = 0;
        }


        public void UpScaleScreen(int scale)
        {
            this.scale = scale;
            if (scale >= 0) 
            {
                x *= scale;
                y *= scale;
                size = x * y;
            }

            else
            {
                scale = Math.Abs(scale);
                x /= scale;
                y /= scale;
                size = x * y;
            }
        }
    }
}
