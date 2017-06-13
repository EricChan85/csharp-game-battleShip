using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip
{
    public partial class Form1 : Form
    {
        private const int _max = 4;
        int[,] panels = new int[_max, _max];
        Ship horizontalShip = new Ship();
        Ship verticalShip = new Ship();
        private int _moveCount = 0;
        public Form1()
        {
            InitializeComponent();
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;

            InitPanels();
        }

        private void InitPanels()
        {
            lblResult.Text = "";
            _moveCount = 0;
            Array.Clear(panels, 0, 16);
            Random rnd = new Random();
            int value = rnd.Next(0, _max * _max);
            int col = value % _max;
            int row = value / _max;
            horizontalShip = new Ship();
            horizontalShip.X1 = row;
            horizontalShip.Y1 = col;
            horizontalShip.Uncovered1 = false;
            horizontalShip.X2 = row;
            if (col > 0)
            {
                horizontalShip.Y2 = col - 1;
            }
            else
            {
                horizontalShip.Y2 = col + 1;
            }
            horizontalShip.Uncovered2 = false;
            
            int value3 = horizontalShip.X2 * _max + horizontalShip.Y2;
            verticalShip = new Ship();            
            bool isVerticalNotOk = false;
            do
            {
                isVerticalNotOk = false;
                int value2 = rnd.Next(0, _max * _max);
                System.Diagnostics.Debug.WriteLine(value2);
                if (value2 == value || value2 == value3) 
                {
                    isVerticalNotOk = true;                    
                    continue;
                }
                col = value2 % _max;
                row = value2 / _max;
                verticalShip.X1 = row;
                verticalShip.Y1 = col;
                verticalShip.Uncovered1 = false;
                if (row > 0)
                {
                    verticalShip.X2 = row - 1;
                }
                else
                {
                    verticalShip.X2 = row + 1;
                }
                verticalShip.Y2 = col;
                verticalShip.Uncovered2 = false;
                if (verticalShip.IsSecond(horizontalShip.X1, horizontalShip.Y1) ||
                    verticalShip.IsSecond(horizontalShip.X2, horizontalShip.Y2))
                {
                    isVerticalNotOk = true;
                }
            }
            while (isVerticalNotOk);
            for (int i = 0; i < _max; i++)
            {
                for (int j = 0; j < _max; j++)
                {
                    Panel panel = (Panel)this.tableLayoutPanel1.GetControlFromPosition(j, i);
                    if (horizontalShip.IsFirst(i, j)) 
                    {
                        panel.Click += new EventHandler(horizontalShipFirstClick);
                    }
                    else if (horizontalShip.IsSecond(i, j))
                    {
                        panel.Click += new EventHandler(horizontalShipSecondClick);
                    }
                    else if (verticalShip.IsFirst(i, j)) 
                    {
                        panel.Click += new EventHandler(verticalShipFirstClick);
                    }
                    else if (verticalShip.IsSecond(i, j))
                    {
                        panel.Click += new EventHandler(verticalShipSecondClick);
                    }
                    else
                    {
                        panel.Click += new EventHandler(panelClick);
                    }
                    panel.BackColor = Color.Gray;
                }
            }
        }

        private void RemoveEvent()
        {
            for (int i = 0; i < _max; i++)
            {
                for (int j = 0; j < _max; j++)
                {
                    Panel panel = (Panel)this.tableLayoutPanel1.GetControlFromPosition(j, i);
                    if (horizontalShip.IsFirst(i, j))
                    {
                        panel.Click -= new EventHandler(horizontalShipFirstClick);
                    }
                    else if (horizontalShip.IsSecond(i, j))
                    {
                        panel.Click -= new EventHandler(horizontalShipSecondClick);
                    }
                    else if (verticalShip.IsFirst(i, j))
                    {
                        panel.Click -= new EventHandler(verticalShipFirstClick);
                    }
                    else if (verticalShip.IsSecond(i, j))
                    {
                        panel.Click -= new EventHandler(verticalShipSecondClick);
                    }
                    else
                    {
                        panel.Click -= new EventHandler(panelClick);
                    }
                }
            }
        }

        private void panelClick(object sender, EventArgs e)
        {
            Panel panel = (Panel)sender;
            if (panel.BackColor == Color.Blue)
            {
                return;
            }
            panel.BackColor = Color.Blue;
            CheckIfWon();
        }

        private void horizontalShipFirstClick(object sender, EventArgs e)
        {
            if (horizontalShip.Uncovered1)
            {
                return;
            }
            ((Panel)sender).BackColor = Color.Red;
            horizontalShip.Uncovered1 = true;
            CheckIfWon();
        }

        private void horizontalShipSecondClick(object sender, EventArgs e)
        {
            if (horizontalShip.Uncovered2)
            {
                return;
            }
            ((Panel)sender).BackColor = Color.Red;
            horizontalShip.Uncovered2 = true;
            CheckIfWon();
        }

        private void verticalShipFirstClick(object sender, EventArgs e)
        {
            if (verticalShip.Uncovered1)
            {
                return;
            }
            ((Panel)sender).BackColor = Color.Black;
            verticalShip.Uncovered1 = true;
            CheckIfWon();
        }

        private void verticalShipSecondClick(object sender, EventArgs e)
        {
            if (verticalShip.Uncovered2)
            {
                return;
            }
            ((Panel)sender).BackColor = Color.Black;
            verticalShip.Uncovered2 = true;
            CheckIfWon();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            InitPanels();
        }

        private void CheckIfWon()
        {
            _moveCount++;
            if (horizontalShip.Uncovered1 && horizontalShip.Uncovered2 &&
                verticalShip.Uncovered1 && verticalShip.Uncovered2)
            {
                lblResult.Text = "You won in " + _moveCount + " moves!";
                RemoveEvent();
            }
        }
    }
}
