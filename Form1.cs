using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Drawing.Drawing2D;

namespace TurtlesGame
{
    public partial class Form1 : Form
    {
        static string pathGameSettingsFile { get; set; }
        static string pathMovesFile { get; set; }
        static int boardX { get; set; }
        static int boardY { get; set; }
        static char startDir { get; set; }
        static int startX { get; set; }
        static int startY { get; set; }
        static int exitX { get; set; }
        static int exitY { get; set; }
        static int[,] mines { get; set; }
        static char[] moves { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnGameSettingsFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog gameSettingsFile = new OpenFileDialog();
                gameSettingsFile.InitialDirectory = Directory.GetCurrentDirectory();
                gameSettingsFile.Title = "Browse Json Game settings file";
                gameSettingsFile.DefaultExt = "json";
                gameSettingsFile.CheckFileExists = true;
                gameSettingsFile.CheckPathExists = true;
                gameSettingsFile.ShowDialog();
                pathGameSettingsFile = gameSettingsFile.FileName;
                lblGameSettingsFile.Text = pathGameSettingsFile;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        private void BtnMovesFile_Click(object sender, EventArgs e)
        {

            try
            {
                OpenFileDialog gameSettingsFile = new OpenFileDialog();
                gameSettingsFile.InitialDirectory = Directory.GetCurrentDirectory();
                gameSettingsFile.Title = "Browse Json Moves file";
                gameSettingsFile.DefaultExt = "json";
                gameSettingsFile.CheckFileExists = true;
                gameSettingsFile.CheckPathExists = true;
                gameSettingsFile.ShowDialog();
                pathMovesFile = gameSettingsFile.FileName;
                lblMoveFile.Text = pathGameSettingsFile;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                lbResult.Items.Clear();
                if (isValid())
                {
                    SetInitValuesGame();

                    TableLayoutPanel tbBoard = new TableLayoutPanel();
                    CreateBoard(ref tbBoard);

                    //Load the initial turtle
                    LoadTurtleIcon(ref tbBoard,startDir,startX,startY );

                    //Load Exit
                    LoadExitIcon(ref tbBoard);

                    //Load mines
                    LoadMines(ref tbBoard);

                    //Execute moves
                    ExecuteMoves(ref tbBoard);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetInitValuesGame()
        {
            try
            {
                StreamReader jsonStream = File.OpenText(pathGameSettingsFile);
                var json = jsonStream.ReadToEnd();
                gameSettings gameSettingsAux = JsonConvert.DeserializeObject<gameSettings>(json);

                jsonStream = File.OpenText(pathMovesFile);
                json = jsonStream.ReadToEnd();
                moves movesAux = JsonConvert.DeserializeObject<moves>(json);

                boardX = gameSettingsAux.board[0];
                boardY = gameSettingsAux.board[1];
                startX = gameSettingsAux.startPos[0];
                startY = gameSettingsAux.startPos[1];
                startDir = gameSettingsAux.startDir;
                exitX = gameSettingsAux.exitPos[0];
                exitY = gameSettingsAux.exitPos[1];
                mines = gameSettingsAux.mines;
                moves = movesAux.m;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CreateBoard(ref TableLayoutPanel tbBoard)
        {
            try
            {
                tbBoard.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                tbBoard.ColumnCount = boardX;
                tbBoard.RowCount = boardY;
                int sizeAux = (int)100 / boardX;
                for (int i = 0; i < boardX; i++)
                {
                    tbBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, sizeAux));
                    tbBoard.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                }
                sizeAux = (int)100 / boardY;
                for (int i = 0; i < boardY; i++)
                {
                    RowStyle auxRow = new RowStyle(SizeType.Percent, sizeAux);
                    tbBoard.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                    tbBoard.RowStyles.Add(auxRow);
                }
                tbBoard.Dock = DockStyle.Fill;
                tbBoard.AutoSize = false;
                pnlGame.Controls.Add(tbBoard);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadTurtleIcon(ref TableLayoutPanel tbBoard, char dir, int posX, int posY)
        {
            PictureBox pbTurtle = new PictureBox();
            int rotateAngle = 0;
            switch (dir)
            {
                case 'N':
                    rotateAngle = 0;
                    break;
                case 'E':
                    rotateAngle = 90;
                    break;
                case 'S':
                    rotateAngle = 180;
                    break;
                case 'W':
                    rotateAngle = 270;
                    break;
            }
            pbTurtle.Image = RotateImage(Properties.Resources.turtle, rotateAngle);
            tbBoard.Controls.Add(pbTurtle, startX, startY);
        }

        public void LoadExitIcon (ref TableLayoutPanel tbBoard)
        {
            PictureBox pbExit = new PictureBox();
            pbExit.Image = Properties.Resources.exit;
            tbBoard.Controls.Add(pbExit, exitX, exitY);
        }

        public void LoadMines (ref TableLayoutPanel tbBoard)
        {
            int mineX = 0;
            int mineY = 0;
            PictureBox pbMine;
            for (int m = 0; m < mines.GetLength(0); m++)
            {
                mineX = mines[m, 0];
                mineY = mines[m, 1];
                pbMine = new PictureBox();
                pbMine.Image = Properties.Resources.mine;
                tbBoard.Controls.Add(pbMine, mineX, mineY);
            }
        }

        public void ExecuteMoves(ref TableLayoutPanel tbBoard)
        {
            char dir = startDir;
            int x = startX;
            int y = startY;
            string aux = "";
            for (int i = 0; i < moves.Length; i++)
            {
                if (moves[i] == 'r')
                {
                    switch (dir)
                    {
                        case 'N':
                            dir = 'E';
                            break;
                        case 'E':
                            dir = 'S';
                            break;
                        case 'S':
                            dir = 'W';
                            break;
                        case 'W':
                            dir = 'N';
                            break;
                    }
                }
                else
                {
                    switch (dir)
                    {
                        case 'N':
                            y -= 1;
                            break;
                        case 'E':
                            x += 1;
                            break;
                        case 'S':
                            y += 1;
                            break;
                        case 'W':
                            x -= 1;
                            break;
                    }
                }
                aux = moves[i].ToString() + "'- x: " + x.ToString() + " y: " + y.ToString() + " - ";
                if (x < 0 || y < 0 || x > boardX || y > boardY)
                { 
                    lbResult.Items.Add("Squence '" + aux + (i + 1).ToString() + ": Failure!");
                }
                else if ((x == exitX) && (y == exitY))
                {
                    lbResult.Items.Add("Squence '" + aux + (i + 1).ToString() + ": Exit");
                }
                else
                {
                    bool mineHit = false;
                    int[,] minesAux = mines;
                    int rows = minesAux.GetLength(0);
                    int cols = minesAux.GetLength(1);
                    for (int j = 0; j < rows; j++)
                    {
                        if ((minesAux[j, 0] == x) && (minesAux[j, 1] == y))
                        {
                            mineHit = true;
                        }
                    }
                    if (mineHit == true)
                    {
                        lbResult.Items.Add("Sequence '" + aux + (i + 1).ToString() + " : Mine hit!");
                    }
                    else
                    {
                        lbResult.Items.Add("Sequence '" + aux + (i + 1).ToString() + " : Success!");
                    }
                }
            }
        }
        public static Image RotateImage(Image img, float rotationAngle)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            Graphics gfx = Graphics.FromImage(bmp);

            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);
            gfx.RotateTransform(rotationAngle);
            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gfx.DrawImage(img, new Point(0, 0));
            gfx.Dispose();

            return bmp;
        }

        private bool isValid()
        {
            bool result = false;
            if ((lblGameSettingsFile.Text != "") && (lblMoveFile.Text != ""))
            {
                result = true;
            }
            return result;
        }
    }
    public partial class gameSettings
    {
        public int[] board { get; set; }
        public int[] startPos { get; set; }
        public char startDir { get; set; }
        public int[] exitPos { get; set; }
        public int[,] mines { get; set; }
    }
    public partial class moves
    {
        public Char[] m { get; set; }
    }

}


