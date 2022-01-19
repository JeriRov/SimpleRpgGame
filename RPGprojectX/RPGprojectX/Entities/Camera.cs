using System.Drawing;
using RPGprojectX.Controllers;

namespace RPGprojectX.Entities
{
    public static class Camera
    {
        private static Entity map;
        public static void View(Graphics g)
        {
            MapController.DrawMap(g);
        }
    }
}