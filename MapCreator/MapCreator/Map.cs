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

        private char tile = 'w';

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

        // get all images in
        private Bitmap Wall = new Bitmap(MapImages.WallImage);
        private Bitmap Player = new Bitmap(MapImages.PlayerImage);
        private Bitmap Goal = new Bitmap(MapImages.GoalImage);
        private Bitmap Gold = new Bitmap(MapImages.GoldImage);
        private Bitmap Enemy = new Bitmap(MapImages.EnemyImage);
        private Bitmap EnemyUp = new Bitmap(MapImages.EnemyUpImage);
        private Bitmap EnemyRight = new Bitmap(MapImages.EnemyRightImage);
        private Bitmap Empty = new Bitmap(MapImages.EmptyImage);
        private Bitmap Obstacle = new Bitmap(MapImages.ObstacleImage);

        private Bitmap WallHalf = new Bitmap(MapImages.WallHalfImage);
        private Bitmap PlayerHalf = new Bitmap(MapImages.PlayerHalfImage);
        private Bitmap GoalHalf = new Bitmap(MapImages.GoalHalfImage);
        private Bitmap GoldHalf = new Bitmap(MapImages.GoldHalfImage);
        private Bitmap EnemyHalf = new Bitmap(MapImages.EnemyHalfImage);
        private Bitmap EnemyUpHalf = new Bitmap(MapImages.EnemyUpHalfImage);
        private Bitmap EnemyRightHalf = new Bitmap(MapImages.EnemyRightHalfImage);
        private Bitmap EmptyHalf = new Bitmap(MapImages.EmptyHalfImage);
        private Bitmap ObstacleHalf = new Bitmap(MapImages.ObstacleHalfImage);

        // create control form
        Controls controls = new Controls();

        public Map(int xIn, int yIn, string fileNameIn, Info infoFormIn)
        {
            InitializeComponent();

            // assign the x and y components
            x = xIn;
            y = yIn;

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
            cursor.Image = WallHalf;
            cursor.Location = new Point(4 * MapImages.ImageWidth, 4 * MapImages.ImageHeight);
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
                    images[i, j].Image = Empty;
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
                        images[i, j].Image = Wall;
                    }

                    if (i == 0 || i == x - 1)
                    {
                        values[i, j] = 'w';
                        images[i, j].Image = Wall;
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
            // set all location 
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    images[i, j].Location = new Point(4 * MapImages.ImageWidth + i * MapImages.ImageWidth - posX * MapImages.ImageWidth, 4 * MapImages.ImageWidth + j * MapImages.ImageWidth - posY * MapImages.ImageWidth);
                }
            }

        }

        private void Map_Load(object sender, EventArgs e)
        {
            // draw default positions
            Draw();
            controls.Show();
            controls.Focus();
        }

        private void Map_KeyPress(object sender, KeyPressEventArgs e)
        {
            // check to see if cursor is within range
            if (posX >= 0 && posX < x && posY >= 0 && posY < y)
            {
                // choose key that is pressed
                // change cursor image to the appropriate image
                // change tile character to the pressed character
                switch (e.KeyChar)
                {
                    case '4':
                        tile = 'g';
                        cursor.Image = GoldHalf;
                        break;
                    case '3':
                        tile = 'f';
                        cursor.Image = GoalHalf;
                        break;
                    case '2':
                        tile = 'c';
                        cursor.Image = PlayerHalf;
                        break;
                    case '5':
                        tile = 'u';
                        cursor.Image = EnemyUpHalf;
                        break;
                    case '6':
                        tile = 'r';
                        cursor.Image = EnemyRightHalf;
                        break;
                    case '7':
                        tile = 's';
                        cursor.Image = EnemyHalf;
                        break;
                    case '0':
                        tile = 'n';
                        cursor.Image = EmptyHalf;
                        break;
                    case '1':
                        tile = 'w';
                        cursor.Image = WallHalf;
                        break;
                    case '8':
                        tile = 'o';
                        cursor.Image = ObstacleHalf;
                        break;
                }
            }
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
                    for (int i = 0; i < x; i++)
                    {
                        for (int j = 0; j < y; j++)
                        {
                            images[i, j].Location = new Point(images[i, j].Location.X, images[i, j].Location.Y + MapImages.ImageHeight);
                        }
                    }
                    break;
                case Keys.Down:
                    posY += 1;
                    for (int i = 0; i < x; i++)
                    {
                        for (int j = 0; j < y; j++)
                        {
                            images[i, j].Location = new Point(images[i, j].Location.X, images[i, j].Location.Y - MapImages.ImageHeight);
                        }
                    }
                    break;
                case Keys.Left:
                    posX -= 1;
                    for (int i = 0; i < x; i++)
                    {
                        for (int j = 0; j < y; j++)
                        {
                            images[i, j].Location = new Point(images[i, j].Location.X + MapImages.ImageWidth, images[i, j].Location.Y);
                        }
                    }
                    break;
                case Keys.Right:
                    posX += 1;
                    for (int i = 0; i < x; i++)
                    {
                        for (int j = 0; j < y; j++)
                        {
                            images[i, j].Location = new Point(images[i, j].Location.X - MapImages.ImageWidth, images[i, j].Location.Y);
                        }
                    }
                    break;
                case Keys.Space:

                    if (posX >= 0 && posX < x && posY >= 0 && posY < y)
                    {
                        switch (tile)
                        {
                            // set image to appropriate image
                            case 'g':
                                images[posX, posY].Image = Gold;
                                values[posX, posY] = 'g';
                                break;
                            case 'f':
                                // check to see if there is already a goal
                                if (!CheckGoal())
                                {
                                    images[posX, posY].Image = Goal;
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
                                if (!CheckPlayer())
                                {
                                    images[posX, posY].Image = Player;
                                    values[posX, posY] = 'c';
                                }
                                // give error message if there is already a player
                                else
                                {
                                    MessageBox.Show("There is already a player");
                                }
                                break;
                            case 'u':
                                images[posX, posY].Image = EnemyUp;
                                values[posX, posY] = 'u';
                                break;
                            case 'r':
                                images[posX, posY].Image = EnemyRight;
                                values[posX, posY] = 'd';
                                break;
                            case 's':
                                images[posX, posY].Image = Enemy;
                                values[posX, posY] = 's';
                                break;
                            case 'n':
                                images[posX, posY].Image = Empty;
                                values[posX, posY] = '0';
                                break;
                            case 'w':
                                images[posX, posY].Image = Wall;
                                values[posX, posY] = 'w';
                                break;
                            case 'o':
                                images[posX, posY].Image = Obstacle;
                                values[posX, posY] = 'o';
                                break;
                        }
                    }
                    break;

            }

            // draw new positions
            // Draw();
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
            
            controls.Show();
        }

        private void newMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            infoForm.Show();
            controls.Close();
            this.Hide();
        }
    }
}
