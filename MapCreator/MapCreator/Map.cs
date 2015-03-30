using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MapCreator
{
    public partial class Map : Form
    {
        private int x;
        private int y;
        private int posX;
        private int posY;
        private char[,] values;
        private PictureBox[,] images;
        private PictureBox cursor;
        private string fileName;
        private Info infoForm;

        public Map(int xIn, int yIn, string fileNameIn, Info infoFormIn)
        {
            InitializeComponent();
            x = xIn;
            y = yIn;
            values = new char[x,y];
            images = new PictureBox[x, y];
            infoForm = infoFormIn;

            fileName = fileNameIn;

            cursor = new PictureBox();
            cursor.SizeMode = PictureBoxSizeMode.AutoSize;
            cursor.BorderStyle = BorderStyle.FixedSingle;
            cursor.Image = new Bitmap(MapImages.CursorImage);
            cursor.Location = new Point(445, 445);
            Controls.Add(cursor);

            for(int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    values[i, j] = '0';
                    images[i,j] = new PictureBox();
                    images[i, j].SizeMode = PictureBoxSizeMode.AutoSize;
                    images[i, j].BorderStyle = BorderStyle.FixedSingle;
                    images[i, j].Image = new Bitmap(MapImages.EmptyImage);
                    images[i, j].Location = new Point(-101, -101);
                    Controls.Add(images[i, j]);
                }
            }

            
            posX = 0;
            posY = 0;

        }

        public void Draw()
        {
            int xTrack = 0;
            int yTrack = 0;

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    images[i, j].Location = new Point(-101, -101);
                }
            }

            for (int i = posX - 4; i <= posX + 4; i++)
            {
                for (int j = posY - 4; j <= posY + 4; j++)
                {
                    if(i >= 0 && i < x && j >= 0 && j < y)
                    {
                        images[i, j].Location = new Point(xTrack * 100, yTrack * 100);
                    }
                    yTrack += 1;
                }
                xTrack += 1;
                yTrack = 0;
            }
        }

        private void Map_Load(object sender, EventArgs e)
        {
            Draw();
        }

        private void Map_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (posX >= 0 && posX < x && posY >= 0 && posY < y)
            {
                switch (e.KeyChar)
                {
                    case 'g':
                        images[posX, posY].Image = new Bitmap(MapImages.GoldImage);
                        values[posX, posY] = 'g';
                        break;
                    case 'f':
                        images[posX, posY].Image = new Bitmap(MapImages.GoalImage);
                        values[posX, posY] = 'f';
                        break;
                    case 'c':
                        images[posX, posY].Image = new Bitmap(MapImages.PlayerImage);
                        values[posX, posY] = 'c';
                        break;
                    case 'e':
                        images[posX, posY].Image = new Bitmap(MapImages.EnemyImage);
                        values[posX, posY] = 'e';
                        break;
                    case 'n':
                        images[posX, posY].Image = new Bitmap(MapImages.EmptyImage);
                        values[posX, posY] = '0';
                        break;
                    case 'w':
                        images[posX, posY].Image = new Bitmap(MapImages.WallImage);
                        values[posX, posY] = 'w';
                        break;
                }
            }
            

            Draw();
            
        }

        private void Map_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Up:
                    posY -= 1;
                    break;
                case Keys.Down:
                    posY += 1;
                    break;
                case Keys.Left:
                    posX -= 1;
                    break;
                case Keys.Right:
                    posX += 1;
                    break;
            }

            Draw();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter output = new StreamWriter(fileName + ".txt");
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    output.Write(values[j,i]);
                }
                output.WriteLine();
            }
            output.Close();
        }

        private void Map_FormClosed(object sender, FormClosedEventArgs e)
        {
            infoForm.Close();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Controls controls = new Controls();
            controls.Show();
        }
    }
}
