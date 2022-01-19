using RPGprojectX.Entities;
using RPGprojectX.Models;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using RPGprojectX.Controllers;

namespace RPGprojectX
{
    public partial class Form1 : Form
    {
        public Entity player;
        public Image hero;
        private Point delta;
        private int playerSpeed = 7;
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 20;
            timer1.Tick += new EventHandler(Update);
            KeyDown += new KeyEventHandler(OnPress);
            KeyUp += new KeyEventHandler(OnKeyUp);
            Init();
        }
        public void Init() {
            MapController.Init();
            this.Width = MapController.GetWidth();
            this.Height = MapController.GetHeight();
            hero = new Bitmap(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Sprites\\Hero.png"));
           
            player = new Entity(this.Width/2 - 20, this.Height/2-20, Hero.idleFrames, Hero.runFrames, Hero.attackFrames, hero);
            timer1.Start();
        }

        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    player.dirY = 0;
                    delta.Y = 0;
                    break;
                case Keys.S:
                    player.dirY = 0;
                    delta.Y = 0;
                    break;
                case Keys.A:
                    player.dirX = 0;
                    delta.X = 0;
                    break;
                case Keys.D:
                    player.dirX = 0;
                    delta.X = 0;
                    break;
            }

            if (player.dirX == 0 && player.dirY == 0)
            {
                player.isMoving = false;
                if(player.flip == 1) player.SetAnimationConfiguration(0);
                else player.SetAnimationConfiguration(5);
            }
        }

        public void OnPress(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    player.dirY = -playerSpeed;
                    player.isMoving = true;
                    delta.Y = playerSpeed;
                    if (player.flip == 1) player.SetAnimationConfiguration(1);
                    else player.SetAnimationConfiguration(6);
                    break;
                case Keys.S:
                    player.dirY = playerSpeed;
                    player.isMoving = true;
                    delta.Y = -playerSpeed;
                    if (player.flip == 1) player.SetAnimationConfiguration(1);
                    else player.SetAnimationConfiguration(6);
                    break;
                case Keys.A:
                    player.dirX = -playerSpeed;
                    
                    delta.X = playerSpeed;
                    player.isMoving = true;
                    player.SetAnimationConfiguration(6);
                    player.flip = -1;
                    break;
                case Keys.D:
                    player.dirX = playerSpeed;
                    delta.X = -playerSpeed;
                    player.isMoving = true;
                    player.SetAnimationConfiguration(1);
                    player.flip = 1;
                    break;
                case Keys.Space:
                    player.dirX = 0;
                    player.dirY = 0;
                    player.isMoving = false;
                    if (player.flip == 1) player.SetAnimationConfiguration(2);
                    else player.SetAnimationConfiguration(7);
                    break;
            }
        }
        private void CreateMap(Graphics g)
        {
            Camera.View(g);
            player.PlayAnimation(g);
        }
        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            CreateMap(g);
        }

        public void Update(object sender, EventArgs e)
        {
            delta.X = -player.dirX;
            delta.Y = -player.dirY;
            if (player.posY - MapController.mapDelta.Y < this.Height / 2)
                delta.Y = 0;
            if (player.posX  + MapController.cellSize*4 - this.Width/2 >= this.Width/2) 
                delta.X = 0;
            if (player.posX - MapController.mapDelta.X < this.Width / 2)
                delta.X = 0;
            if ( player.posY - MapController.cellSize*8 + MapController.cellSize/2 - this.Height/2>= this.Height/2) 
                delta.Y = 0;
            MapController.MapMoving(delta);
            if (!PhysicsController.IsCollide(player, new Point(player.dirX, player.dirY)))
            {
                if (player.isMoving)
                {
                    player.Move();
                }
            }
            Invalidate();
        }
    }
}
