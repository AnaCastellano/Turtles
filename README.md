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
                if (isValid())
                {
                    StreamReader jsonStream = File.OpenText(pathGameSettingsFile);
                    var json = jsonStream.ReadToEnd();
                    gameSettings gameSettingsAux = JsonConvert.DeserializeObject<gameSettings>(json);

                    jsonStream = File.OpenText(pathMovesFile);
                    json = jsonStream.ReadToEnd();
                    moves movesAux = JsonConvert.DeserializeObject<moves>(json);

                    int boardX = gameSettingsAux.board[0];
                    int boardY = gameSettingsAux.board[1];
                    int startX = gameSettingsAux.startPos[0];
                    int startY = gameSettingsAux.startPos[1];
                    char startDir = gameSettingsAux.startDir;
                    int exitX = gameSettingsAux.exitPos[0];
                    int exitY = gameSettingsAux.exitPos[1];
                    int[,] mines = gameSettingsAux.mines;
                    char[] mov = movesAux.m;

                    TableLayoutPanel tbBoard = new TableLayoutPanel();
                    tbBoard.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                    tbBoard.ColumnCount = boardX;
                    tbBoard.RowCount = boardY;
                    int sizeAux = (int)100 / boardX;
                    for (int i = 0; i < boardX; i++)
                    {
                        tbBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,sizeAux));
                        tbBoard.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                    }
                    sizeAux = (int)100 / boardY;
                    for (int i = 0; i < boardY; i++)
                    {
                        RowStyle auxRow = new RowStyle(SizeType.Percent,sizeAux);
                        tbBoard.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                        tbBoard.RowStyles.Add(auxRow);
                    }
                    tbBoard.Dock = DockStyle.Fill;
                    tbBoard.AutoSize = true;
                    pnlGame.Controls.Add(tbBoard);

                    //Load the initial turtle
                    PictureBox pbTurtle = new PictureBox();
                    int rotateAngle = 0;
                    switch (startDir)
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
                    pbTurtle.Image = RotateImage(Properties.Resources.Turtle,rotateAngle);
                    tbBoard.Controls.Add(pbTurtle, startY, startX);
                    pbTurtle.Dock = DockStyle.Fill;

                    //Load mines
                    int mineX = 0;
                    int mineY = 0;
                    PictureBox pbMine;
                    for (int m = 0; m < mines.GetLength(0); m++)
                    {
                        mineX = mines[m, 0];
                        mineY = mines[m, 1];
                        pbMine = new PictureBox();
                        pbMine.Image = Properties.Resources.mine;
                        tbBoard.Controls.Add(pbMine, mineY , mineX);
                        pbMine.Dock = DockStyle.Fill;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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


