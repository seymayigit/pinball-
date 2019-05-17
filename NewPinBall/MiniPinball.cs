using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewPinBall
{
    public partial class MiniPinball : Form
    {
        int directionX=2;
        int  directionY=2;
        private bool flag=true;
        private int i;


        String[] flipper_AImageLocation = { "Texture/Flipper_ADown.png", "Texture/Flipper_AMiddle.png", "Texture/Flipper_AUp.png" };
        int flipper_AImageLocationCount = 0;

        String[] flipper_DImageLocation = { "Texture/Flipper_DDown.png", "Texture/Flipper_DMiddle.png", "Texture/Flipper_DUp.png" };
        int flipper_DImageLocationCount = 0;

        private bool pressKeyA_flag=false;
        private bool pressKeyD_flag=false;


        public MiniPinball()
        {
            InitializeComponent();
            BallYellow.BringToFront();
            HoleEntry.BringToFront();

            PictureBoxFilpper_A.ImageLocation = flipper_AImageLocation[flipper_AImageLocationCount];
            PictureBoxFilpper_A.SizeMode = PictureBoxSizeMode.StretchImage;

            PictureBoxFilpper_D.ImageLocation = flipper_DImageLocation[flipper_DImageLocationCount];
            PictureBoxFilpper_D.SizeMode = PictureBoxSizeMode.StretchImage;
           
        }
       

        private void timer1_Tick(object sender, EventArgs e)
        {
              BallYellow.Top -= directionY;
             BallYellow.Left += directionX;
            

            //for wall collision
            if (BallYellow.Top <= WallTop.Bottom-25)
            {
                directionY = -directionY;
            }
            if (BallYellow.Bottom >= WallBottom.Bottom - 38)//for bottom ,delete later
            {
                directionY = -directionY;
            }
            if (BallYellow.Left >= WallRight.Left+3)
            {
                directionX = -directionX;
            }
            if (BallYellow.Right <= WallLeft.Right+3)
            {
                directionX = -directionX;
            }
           
            //for collision between ballYellow and ballOrange
            //if ((isCollideWithBall() && BallOrange.Right <= BallYellow.Right) || (isCollideWithBall() && BallOrange.Left <= BallYellow.Left))
            //if ((isCollideWithBall() && BallOrange.Right >= BallYellow.Left) || (isCollideWithBall() && BallOrange.Left >= BallYellow.Right))
            if (isCollideWithBall() && ( BallOrange.Right <= BallYellow.Right ||  BallOrange.Left <= BallYellow.Left))
            {
                 directionX *= -1;
                
            }
            if ((isCollideWithBall() && BallOrange.Top <= BallYellow.Top-30) || (isCollideWithBall() && BallOrange.Bottom <= BallYellow.Bottom-30))
            {
                directionY *=-1;

            }


            //if( detectCollision(BallOrange.Location.X,BallOrange.Location.Y,BallOrange.Width,BallOrange.Height,BallYellow.Location.X,BallYellow.Location.Y,BallYellow.Width,BallYellow.Height) )
            //{
            //    directionX *= -1;
            //    directionY *= -1;
            //}


          //  for collision between ballYellow and holeEntry
            if (isCollideWithHoleEntry())
            {
                directionX = 2;
                directionY = 2;
                directionX = +directionX;
                directionY = +directionY;
            }


            if (PictureBoxFilpper_A.Location.X < BallYellow.Location.X + BallYellow.Width &&
           PictureBoxFilpper_A.Location.X + PictureBoxFilpper_A.Width > BallYellow.Location.X &&
           PictureBoxFilpper_A.Location.Y < BallYellow.Location.Y + BallYellow.Height &&
           PictureBoxFilpper_A.Height + PictureBoxFilpper_A.Location.Y > BallYellow.Location.Y)
            {
                directionY = -directionY;
            }
            if (PictureBoxFilpper_D.Location.X < BallYellow.Location.X + BallYellow.Width &&
          PictureBoxFilpper_D.Location.X + PictureBoxFilpper_D.Width > BallYellow.Location.X &&
          PictureBoxFilpper_D.Location.Y < BallYellow.Location.Y + BallYellow.Height &&
          PictureBoxFilpper_D.Height + PictureBoxFilpper_D.Location.Y > BallYellow.Location.Y)
            {
                directionY = -directionY;
            }

        }

        private void MiniPinball_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.S)
            {

                if (flag)
                {
                    timer2_ShooterRod.Start();
                    Invalidate();

                }
                if (!flag)
                {
                    timer2_ShooterRod.Enabled = false;
                }

            }

            if (e.KeyCode == Keys.A)
            {
                pressKeyA_flag = true;
                pressKeyD_flag = false;
                timer2.Start();
                timer3.Stop();

            }
            if (e.KeyCode == Keys.D)
            {
                pressKeyD_flag = true;

                pressKeyA_flag = false;
                timer2.Start();
                timer3.Stop();

            }
           
        }

        private void MiniPinball_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S)
            {
                timer2_ShooterRod.Stop();
                flag = false;

                if (i >= 0 && i <= 3)
                {
                    timer1.Interval = 20;
                }
                if (i > 3 && i <= 6)
                {
                    timer1.Interval = 20;
                }
                if (i > 6 && i <= 100)
                {
                    timer1.Interval = 20;
                }
                timer1.Enabled = true;

            }
            if (e.KeyCode == Keys.A)
            {
                pressKeyA_flag = true;
                pressKeyD_flag = false;
                timer2.Stop();
                timer3.Start();

            }
            if (e.KeyCode == Keys.D)
            {
                pressKeyD_flag = true;
                pressKeyA_flag = false;
                timer2.Stop();
                timer3.Start();

            }
        }

        private void timer2_ShooterRod_Tick(object sender, EventArgs e)
        {
            i++;
            if (ShooterRod.Top != this.ClientSize.Height - 47)
           {
                BallYellow.Top += 10;
                ShooterRod.Top += 10;
                ShooterRod.Height -= 10;
            }
        }


        
        public static bool detectCollision(int object1X, int object1Y, int object1Width, int object1Height,
         int object2X, int object2Y, int object2Width, int object2Height)
        {
            if (object1X < object2X + object2Width &&
               object1X + object1Width > object2X &&
               object1Y < object2Y + object2Height &&
               object1Height + object1Y > object2Y)
            {
                return true;
            }
            return false;
        }
        public bool detectCollision()
        {
            if (BallYellow.Location.X < BallOrange.Location.X + BallOrange.Width &&
               BallYellow.Location.X + BallYellow.Width > BallOrange.Location.X &&
               BallYellow.Location.Y < BallOrange.Location.Y + BallOrange.Height &&
               BallOrange.Height + BallYellow.Location.Y > BallOrange.Location.Y)
            {
                return true;
            }
            return false;
        }


        public bool isCollideWithBall()
        {

            //int BallYellow_CenterX = BallYellow.Location.X + BallYellow.Width / 2;
            //int BallYellow_CenterY = BallYellow.Location.Y + BallYellow.Height / 2;

            int BallYellow_CenterX = BallYellow.Bounds.Location.X + BallYellow.Bounds.Width / 2;
            int BallYellow_CenterY = BallYellow.Bounds.Location.Y + BallYellow.Bounds.Height / 2;

            //int BallOrange_CenterX = BallOrange.Location.X + BallOrange.Width / 2;
            //int BallOrange_CenterY = BallOrange.Location.Y + BallOrange.Height / 2;

            int BallOrange_CenterX = BallOrange.Bounds.Location.X + BallOrange.Bounds.Width / 2;
            int BallOrange_CenterY = BallOrange.Bounds.Location.Y + BallOrange.Bounds.Height / 2;

            float distance_X = Math.Abs(BallYellow_CenterX - BallOrange_CenterX);
            float distance_Y = Math.Abs(BallYellow_CenterY - BallOrange_CenterY);

            double hypotenuse = Math.Sqrt((distance_X * distance_X) + (distance_Y * distance_Y));


            if ( hypotenuse <= (BallYellow.Width/2) + (BallOrange.Width/2) )
            {
                return true;

            }


            return false;

        }
        public bool isCollideWithHoleEntry()
        {
            
            int BallYellow_CenterX = BallYellow.Location.X + BallYellow.Width / 2;
            int BallYellow_CenterY = BallYellow.Location.Y + BallYellow.Height / 2;

            int HoleEntry_CenterX = HoleEntry.Location.X + HoleEntry.Width / 2;
            int HoleEntry_CenterY = HoleEntry.Location.Y + HoleEntry.Height / 2;

            float distance_X = Math.Abs(BallYellow_CenterX - HoleEntry_CenterX);
            float distance_Y = Math.Abs(BallYellow_CenterY - HoleEntry_CenterY);

            double hypotenuse = Math.Sqrt((distance_X * distance_X) + (distance_Y * distance_Y));


            if (hypotenuse <= (BallYellow.Width / 2) + (HoleEntry.Width / 2))
            {
                return true;

            }


            return false;

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if ( pressKeyA_flag == true && pressKeyD_flag != true)
            {
                if (flipper_AImageLocationCount++ < 2)
                {
                    PictureBoxFilpper_A.ImageLocation = flipper_AImageLocation[flipper_AImageLocationCount];
                }
            }
            if ( pressKeyD_flag == true && pressKeyA_flag != true)
            {
                if (flipper_DImageLocationCount++ < 2)
                {
                    PictureBoxFilpper_D.ImageLocation = flipper_DImageLocation[flipper_DImageLocationCount];
                }
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if ( pressKeyA_flag == true && pressKeyD_flag != true)
            {
               
                PictureBoxFilpper_A.ImageLocation = flipper_AImageLocation[flipper_AImageLocationCount = 0];

            }
            if (pressKeyD_flag == true && pressKeyA_flag != true)
            {

                PictureBoxFilpper_D.ImageLocation = flipper_DImageLocation[flipper_DImageLocationCount = 0];

            }
        }
       
      




    }
}