using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapCreator
{
    public partial class Info : Form
    {
        public Info()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Create_Click(object sender, EventArgs e)
        {
            int x;
            int y;
            bool xGood;
            bool yGood;

            // parse int values in textboxes
            xGood = int.TryParse(XLength.Text, out x);
            yGood = int.TryParse(YHeight.Text, out y);

            // check to see if int values are withing the range
            if (x < 1 && x > 50)
            {
                xGood = false;
            }
            if (y < 1 && y > 50)
            {
                yGood = false;
            }

            // check that all fields are valid
            if(xGood && yGood && fileName.Text != "")
            {
                this.Hide();
                Map map = new Map(x, y, fileName.Text, this);
                map.Show();
                fileName.Text = "";
                XLength.Text = "";
                YHeight.Text = "";
            }
        }

        private void Info_Load(object sender, EventArgs e)
        {
            this.ActiveControl = fileName;
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            fileName.Text = "";
            XLength.Text = "";
            YHeight.Text = "";
        }
    }
}
