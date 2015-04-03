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
        // holds the x and y height of the map
        private int x;
        private int y;

        // holds the position of the cursor
        private int posX;
        private int posY;

        // holds the characters to be written to the file
        private char[,] values;

        // holds the images to be drawn to the screen
        private PictureBox[,] images;

        // image of the cursor
        private PictureBox cursor;

        // holds the name of the file to be written
        private string fileName;

        // holds the info form
        private Info infoForm;

        public Map(int xIn, int yIn, string fileNameIn, Info infoFormIn)
        {
            InitializeComponent();

            // assign the x and y components
            // add two to compensate for the default walls around the edges
            x = xIn + 2;
            y = yIn + 2;

            // initialize the arrays
            values = new char[x,y];
            images = new PictureBox[x, y];

            // assign the info form
            infoForm = infoFormIn;

            // set the file name
            fileName = fileNameIn;

            // create the cursor and add it to the screen
            cursor = new PictureBox();
            cursor.SizeMode = PictureBoxSizeMode.AutoSize;
            cursor.BorderStyle = BorderStyle.FixedSingle;
            cursor.Image = new Bitmap(MapImages.CursorImage);
            cursor.Location = new Point(4 * MapImages.ImageWidth + MapImages.ImageWidth / 2 - 5, 4 * MapImages.ImageHeight + MapImages.ImageHeight / 2 - 5);
            Controls.Add(cursor);

            // loop through all of the values in the arrays
            for(int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    // assign default value to character array
                    values[i, j] = '0';

                    // assign default picture to images array and add to the screen
                    images[i,j] = new PictureBox();
                    images[i, j].SizeMode = PictureBoxSizeMode.AutoSize;
                    images[i, j].BorderStyle = BorderStyle.FixedSingle;
                    images[i, j].Image = new Bitmap(MapImages.EmptyImage);
                    images[i, j].Location = new Point(-300, -300);
                    Controls.Add(images[i, j]);
                }
            }

            //loop through all values in array
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {

                    // if value is on either edge, assign it to a wall
                    if(j == 0 || j == y - 1)
                    {
                        values[i, j] = 'w';
                        images[i, j].Image = new Bitmap(MapImages.WallImage);
                    }

                    if (i == 0 || i == x - 1)
                    {
                        values[i, j] = 'w';
                        images[i, j].Image = new Bitmap(MapImages.WallImage);
                    }
                }
            }

            // assign default positions at 0,0
            posX = 0;
            posY = 0;

        }

        // draw all of the boxes
        public void Draw()
        {
            // keep track of which position on screen to draw
            int xTrack = 0;
            int yTrack = 0;

            // loop through all of the values in the array
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    // move all of the images off screen
                    images[i, j].Location = new Point(-300, -300);
                }
            }

            // loop through the values in the array that will apear on screen
            for (int i = posX - 4; i <= posX + 4; i++)
            {
                for (int j = posY - 4; j <= posY + 4; j++)
                {
                    // only draw images if position is within array bounds
                    if(i >= 0 && i < x && j >= 0 && j < y)
                    {
                        // move to approppriate location
                        images[i, j].Location = new Point(xTrack * MapImages.ImageWidth, yTrack * MapImages.ImageHeight);
                    }
                    // incriment tracking values
                    yTrack += 1;
                }
                xTrack += 1;

                // go to the top of the next row
                yTrack = 0;
            }
        }

        private void Map_Load(object sender, EventArgs e)
        {
            // draw default positions
            Draw();
        }

        private void Map_KeyPress(object sender, KeyPressEventArgs e)
        {
            // check to see if cursor is within range
            if (posX >= 0 && posX < x && posY >= 0 && posY < y)
            {
                // choose key that is pressed
                // change image to the appropriate image
                // change character to the pressed character
                switch (e.KeyChar)
                {
                    case 'g':
                        images[posX, posY].Image = new Bitmap(MapImages.GoldImage);
                        values[posX, posY] = 'g';
                        break;
                    case 'f':
                        // check to see if there is already a goal
                        if(!CheckGoal())
                        {
                            images[posX, posY].Image = new Bitmap(MapImages.GoalImage);
                            values[posX, posY] = 'f';
                        }
                        // give error message if there is already a goal
                        else
                        {
                            MessageBox.Show("There is already a goal");
                        }
                        break;
                    case 'c':
                        // check to see if there is already a player
                        if(!CheckPlayer())
                        {
                            images[posX, posY].Image = new Bitmap(MapImages.PlayerImage);
                            values[posX, posY] = 'c';
                        }
                        // give error message if there is already a player
                        else
                        {
                            MessageBox.Show("There is already a player");
                        }
                        break;
                    case 'u':
                        images[posX, posY].Image = new Bitmap(MapImages.EnemyUpImage);
                        values[posX, posY] = 'u';
                        break;
                    case 'r':
                        images[posX, posY].Image = new Bitmap(MapImages.EnemyRightImage);
                        values[posX, posY] = 'd';
                        break;
                    case 's':
                        images[posX, posY].Image = new Bitmap(MapImages.EnemyImage);
                        values[posX, posY] = 's';
                        break;
                    case 'n':
                        images[posX, posY].Image = new Bitmap(MapImages.EmptyImage);
                        values[posX, posY] = '0';
                        break;
                    case 'w':
                        images[posX, posY].Image = new Bitmap(MapImages.WallImage);
                        values[posX, posY] = 'w';
                        break;
                    case 'o':
                        images[posX, posY].Image = new Bitmap(MapImages.ObstacleImage);
                        values[posX, posY] = 'o';
                        break;
                }
            }

            // draw after changes made
            Draw();
        }

        // check if a player object is on the map
        public bool CheckPlayer()
        {
            bool created = false;
            // go through all objects
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if(values[i,j] == 'c')
                    {
                        // if there is a player
                        // record that a player exists
                        created = true;
                    }
                }
            }
            return created;
        }

        // check if a goal object has been made
        public bool CheckGoal()
        {
            bool created = false;
            // go through all objects
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (values[i, j] == 'f')
                    {
                        // if there is a goal
                        // record that a goal exists
                        created = true;
                    }
                }
            }
            return created;
        }

        private void Map_KeyDown(object sender, KeyEventArgs e)
        {
            // get arrow key inputs
            // move acording to the arrow key
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

            // draw new positions
            Draw();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // check to see if a player and goal has bee created
            if(CheckPlayer() && CheckGoal())
            {
                // open the file to write to
                StreamWriter output = new StreamWriter(fileName + ".txt");

                // write level info
                output.WriteLine(fileName);
                output.WriteLine('0');
                output.WriteLine('0');

                output.WriteLine(x);
                output.WriteLine(y);

                // go through all characters
                for (int i = 0; i < y; i++)
                {
                    for (int j = 0; j < x; j++)
                    {
                        // write the character to the file
                        output.Write(values[j, i]);
                        output.Write(' ');
                    }
                    output.WriteLine();
                }
                output.Close();
            }
            // show error message if there is no player or goal
            else if(!CheckPlayer())
            {
                MessageBox.Show("There is no player");
            }
            else if (!CheckGoal())
            {
                MessageBox.Show("There is no goal");
            }
        }


        private void Map_FormClosed(object sender, FormClosedEventArgs e)
        {
            // also close the info form with map form
            infoForm.Close();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // display controls form
            Controls controls = new Controls();
            controls.Show();
        }
    }
}
