using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Controls;
using AForge.Video.DirectShow;

namespace CamPad
{
    public partial class MainForm : Form
    {
        FilterInfoCollection videodevices;
        VideoCaptureDevice videoSource;
        VideoSourcePlayer videoSourcePlayer;
        UnsafeBitmap fastBitmap;
        double[] x = new double[4];
        double[] y = new double[4];
        Solver solver;
        Col pen = new Col();
        Col[,] picture = new Col[640, 480];

        struct Col
        {
            public int r, g, b;

            long Sqr(long x)
            {
                return x * x;
            }

            public int Distance(Col col)
            {
                return (int)(Sqr(Sqr(r - col.r) + Sqr(g - col.g) + Sqr(b - col.b)) / 255 / 255 / 3);
            }
        }

        public MainForm()
        {
            InitializeComponent();

            videodevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            videoSource = new VideoCaptureDevice(videodevices[1].MonikerString);
            videoSourcePlayer = new VideoSourcePlayer();
            videoSourcePlayer.Bounds = new Rectangle(0, 0, 640, 480);
            Controls.Add(videoSourcePlayer);
            videoSourcePlayer.VideoSource = videoSource;
            videoSource.Start();

            videoSourcePlayer.NewFrame += new VideoSourcePlayer.NewFrameHandler(videoSourcePlayer_NewFrame);
            videoSourcePlayer.MouseClick += new MouseEventHandler(videoSourcePlayer_MouseClick);
            videoSourcePlayer.Paint += new PaintEventHandler(videoSourcePlayer_Paint);
            videoSourcePlayer.KeyPress += new KeyPressEventHandler(MainForm_KeyPress);
        }

        void videoSourcePlayer_Paint(object sender, PaintEventArgs e)
        {
            Point p = Coordinates();
            e.Graphics.FillEllipse(new SolidBrush(Color.Lime), new Rectangle(p.X - 5, p.Y - 5, 10, 10));
            /*if (solver == null)
            {
                return;
            }
            p = solver.FromCam(p);
            Cursor.Position = p;*/

            int maxD = 0;
            for (int i = 0; i < 640; i++)
            {
                for (int j = 0; j < 480; j++)
                {
                    int d = picture[i, j].Distance(pen);
                    if (d > maxD)
                    {
                        maxD = d;
                    }
                }
            }
            maxD++;
            for (int i = 0; i < 640; i++)
            {
                for (int j = 0; j < 480; j++)
                {
                    int col = picture[i, j].Distance(pen) * 255 / maxD;
                    if (col > 255) col = 255;
                    if (col <= trackBar1.Value)
                    {
                        e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Red)), i, j, i + 1, j + 1);
                    }
                }
            }
        }

        void videoSourcePlayer_MouseClick(object sender, MouseEventArgs e)
        {
            pen = picture[e.X, e.Y];
        }

        void videoSourcePlayer_NewFrame(object sender, ref Bitmap image)
        {
            fastBitmap = new UnsafeBitmap(image);
            fastBitmap.LockBitmap();

            for (int i = 0; i < 640; i++)
            {
                for (int j = 0; j < 480; j++)
                {
                    PixelData pixel = fastBitmap.GetPixel(i, j);
                    picture[i, j].r = pixel.red;
                    picture[i, j].g = pixel.green;
                    picture[i, j].b = pixel.blue;
                }
            }

            fastBitmap.UnlockBitmap();
        }

        Point Coordinates()
        {
            int sumX = 0, sumY = 0;
            int num = 0;
            for (int i = 0; i < 640; i++)
            {
                for (int j = 0; j < 480; j++)
                {
                    if (picture[i, j].Distance(pen) <= 1)
                    {
                        num++;
                        sumX += i;
                        sumY += j;
                    }
                }
            }
            if (num == 0)
            {
                num = 1;
            }
            return new Point(sumX / num, sumY / num);
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                for (int i = 0; i < 4; i++)
                {
                    if (x[i] == 0)
                    {
                        Point p = Coordinates();
                        x[i] = p.X;
                        y[i] = p.Y;
                        if (i == 3)
                        {
                            solver = new Solver(x, y);
                        }
                        return;
                    }
                }
            }
        }
    }
}
