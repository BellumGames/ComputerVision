using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ComputerVision
{
    public partial class MainForm : Form
    {
        private string sSourceFileName = "";
        private FastImage workImage;
        private FastImage workImage2;
        private Bitmap image = null;
        private int[,] Q = new int[1000, 1000];

        void fill()
        {
            Color color;
            workImage = new FastImage(image);
            workImage.Lock();

            for (int i = 0; i < workImage.Width; i++)
            {
                for (int j = 0; j < workImage.Height; j++)
                {
                    color = workImage.GetPixel(i, j);
                    byte R = color.R;
                    byte G = color.G;
                    byte B = color.B;
                    Q[i, j] = (R + G + B) / 3;
                }
            }
            workImage.Unlock();
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
            sSourceFileName = openFileDialog.FileName;
            panelSource.BackgroundImage = new Bitmap(sSourceFileName);
            image = new Bitmap(sSourceFileName);
            workImage = new FastImage(image);
            workImage2 = new FastImage(image);
            fill();
        }

        private void buttonGrayscale_Click(object sender, EventArgs e)
        {
            Color color;
            workImage.SetImage(new Bitmap(sSourceFileName));
            workImage.Lock();
            int n = Int32.Parse(nBox.Text);
            int[][] H = new int[3][];
            for (int i = 0; i < workImage.Width; i++)
            {
                for (int j = 0; j < workImage.Height; j++)
                {
                    color = workImage.GetPixel(i, j);
                    byte R = color.R;
                    byte G = color.G;
                    byte B = color.B;

                    byte average = (byte)((R + G + B) / 3);

                    color = Color.FromArgb(average, average, average);

                    workImage.SetPixel(i, j, color);
                }
            }
            panelDestination.BackgroundImage = null;
            panelDestination.BackgroundImage = workImage.GetBitMap();
            workImage.Unlock();
        }

        private void button_Negative_Click(object sender, EventArgs e)
        {
            Color color;
            workImage.SetImage(new Bitmap(sSourceFileName));
            workImage.Lock();
            for (int i = 0; i < workImage.Width; i++)
            {
                for (int j = 0; j < workImage.Height; j++)
                {
                    color = workImage.GetPixel(i, j);
                    byte R = color.R;
                    byte G = color.G;
                    byte B = color.B;

                    byte average = (byte)((R + G + B) / 3);

                    color = Color.FromArgb(255-R, 255-G, 255-B);

                    workImage.SetPixel(i, j, color);
                }
            }
            panelDestination.BackgroundImage = null;
            panelDestination.BackgroundImage = workImage.GetBitMap();
            workImage.Unlock();
        }

        private void trackBrightness_ValueChanged(object sender, EventArgs e)
        {
            Color color;
            workImage.SetImage(new Bitmap(sSourceFileName));
            workImage.Lock();
            int delta = trackBrightness.Value;
            for (int i = 0; i < workImage.Width; i++)
            {
                for (int j = 0; j < workImage.Height; j++)
                {
                    color = workImage.GetPixel(i, j);
                    byte R = color.R;
                    byte G = color.G;
                    byte B = color.B;


                    //RED pixel
                    if (color.R + delta > 255)
                    {
                        R = 255;
                    }
                    else
                        if (color.R + delta < 0)
                    {
                        R = 0;
                    }
                    else
                    {
                        R = Convert.ToByte(color.R + delta);
                    }

                    //GREEN pixel
                    if (color.G + delta > 255)
                    {
                        G = 255;
                    }
                    else
                        if (color.G + delta < 0)
                    {
                        G = 0;
                    }
                    else
                    {
                        G = Convert.ToByte(color.G + delta);
                    }

                    //BLUE pixel
                    if (color.B + delta > 255)
                    {
                        B = 255;
                    }
                    else
                        if (color.B + delta < 0)
                    {
                        B = 0;
                    }
                    else
                    {
                        B = Convert.ToByte(color.B + delta);
                    }

                    color = Color.FromArgb(R, G, B);
                    workImage.SetPixel(i, j, color);
                }
            }
            panelDestination.BackgroundImage = null;
            panelDestination.BackgroundImage = workImage.GetBitMap();
            workImage.Unlock();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            trackBrightness.Value = 0;
        }

        private void trackContrast_ValueChanged(object sender, EventArgs e)
        {
            Color color;
            workImage.SetImage(new Bitmap(sSourceFileName));
            workImage.Lock();
            int delta = trackContrast.Value;
            byte minR = 255;
            byte minG = 255;
            byte minB = 255;

            byte maxR = 0;
            byte maxG = 0;
            byte maxB = 0;
            for (int i = 0; i < workImage.Width; i++)
            {
                for (int j = 0; j < workImage.Height; j++)
                {
                    color = workImage.GetPixel(i, j);
                    byte R = color.R;
                    byte G = color.G;
                    byte B = color.B;

                    minR = Math.Min(R, minR);
                    minG = Math.Min(G, minG);
                    minB = Math.Min(B, minB);

                    maxR = Math.Max(R, maxR);
                    maxG = Math.Max(G, maxG);
                    maxB = Math.Max(B, maxB);
                }
            }

            int aR = minR - delta;//aR (minR - delta)
            int bR = maxR + delta;//bR (maxR + delta)

            int aG = minG - delta;//aG
            int bG = maxG + delta;//bG

            int aB = minB - delta;//aB
            int bB = maxB + delta;//bB

            for (int i = 0; i < workImage.Width; i++)
            {
                for (int j = 0; j < workImage.Height; j++)
                {
                    int Rn = ((((bR - aR) * (workImage.GetPixel(i, j).R - minR)) / (maxR - minR)) + aR);
                    int Gn = ((((bG - aG) * (workImage.GetPixel(i, j).G - minG)) / (maxG - minG)) + aG);
                    int Bn = ((((bB - aB) * (workImage.GetPixel(i, j).B - minB)) / (maxB - minB)) + aB);

                    if (Rn < 0) Rn = 0;
                    if (Gn < 0) Gn = 0;
                    if (Bn < 0) Bn = 0;
                    if (Rn > 255) Rn = 255;
                    if (Gn > 255) Gn = 255;
                    if (Bn > 255) Bn = 255;

                    color = Color.FromArgb(Rn, Gn, Bn);
                    workImage.SetPixel(i, j, color);
                }
            }

            panelDestination.BackgroundImage = null;
            panelDestination.BackgroundImage = workImage.GetBitMap();
            workImage.Unlock();
        }

        private void buttonHistograma_Click(object sender, EventArgs e)
        {
            Color color;
            int[] hist = new int[256];
            int[] histc = new int[256];
            int[] transf = new int[256];
            workImage.SetImage(new Bitmap(sSourceFileName));
            workImage.Lock();
            for (int i = 0; i < workImage.Width; i++)
            {
                for (int j = 0; j < workImage.Height; j++)
                {
                    color = workImage.GetPixel(i, j);
                    byte R = color.R;
                    byte G = color.G;
                    byte B = color.B;
                    int average = ((R + G + B) / 3);
                    hist[average] = hist[average] + 1;
                }
            }
            histc[0] = hist[0];
            for (int i = 1; i <= 255; i++) 
            {
                histc[i] = histc[i - 1] + hist[i];
            }
            for (int i = 0; i < 255; i++) 
            {
                transf[i] = (histc[i] * 255) / (workImage.Width * workImage.Height);
            }
            for (int i = 0; i < workImage.Width; i++)
            {
                for (int j = 0; j < workImage.Height; j++)
                {
                    color = workImage.GetPixel(i, j);
                    byte R = color.R;
                    byte G = color.G;
                    byte B = color.B;
                    int average = ((R + G + B) / 3);
                    color = Color.FromArgb(transf[average], transf[average], transf[average]);
                    workImage.SetPixel(i, j, color);
                }
            }
            panelDestination.BackgroundImage = null;
            panelDestination.BackgroundImage = workImage.GetBitMap();
            workImage.Unlock();
        }

        private static double delta(int x0, int y0, int x1, int y1, double unghi) 
        {
            double delta = (x1 - x0) * Math.Sin(unghi) - (y1 - y0) * Math.Cos(unghi);
            return delta;
        }

        private void buttonReflexie_Click(object sender, EventArgs e)
        {
            string option = comboBoxReflexie.GetItemText(comboBoxReflexie.SelectedItem);
            Color color;
            workImage.SetImage(new Bitmap(sSourceFileName));
            workImage.Lock();
            int x0 = workImage.Width / 2;
            int y0 = workImage.Height / 2;
            int x1, y1, x2 = 0, y2 = 0;
            for (int i = 0; i < workImage.Width; i++)
            {
                for (int j = 0; j < workImage.Height; j++)
                {
                    x1 = i; y1 = j;
                    color = workImage.GetPixel(i, j);
                    switch (option) 
                    {
                        case "Y":
                            x2 = x1;
                            y2 = (-1 * y1) + (2 * y0);
                            break;
                        case "X":
                            x2 = (-1 * x1) + (2 * x0);
                            y2 = y1;
                            break;
                        case "A":
                            x2 = x1;
                            y2 = y1;
                            break;
                        default:
                            return;
                    }
                    color = Color.FromArgb(color.R, color.G, color.B);
                    workImage.SetPixel(x2, y2, color);
                }
            }
            panelDestination.BackgroundImage = null;
            panelDestination.BackgroundImage = workImage.GetBitMap();
            workImage.Unlock();
        }

        private void comboBoxReflexie_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void buttonFTJ_Click(object sender, EventArgs e)
        {
            FTJ();
        }

        private void FTJ()
        {
            Color color;
            image = new Bitmap(sSourceFileName);
            workImage = new FastImage(image);
            workImage.Lock();

            int n = Int32.Parse(nBox.Text);
            double coeficient = 1 / (Math.Pow(n + 2, 2));
            double[,] H = new double[3, 3] { { 1, n, 1 }, { n, n * n, n }, { 1, n, 1 } };
            /*
            for(int i = 0; i<3; i++) 
            {
                for (int j = 0; j < 3; j++) 
                {
                    H[i,j] = coeficient * H[i,j];
                }
            }*/

            for (int i = 1; i < workImage.Width - 2; i++)
            {
                for (int j = 1; j < workImage.Height - 2; j++)
                {
                    double sumR = 0;
                    double sumG = 0;
                    double sumB = 0;

                    for (int row = i - 1; row <= i + 1; row++)
                    {
                        for (int col = j - 1; col <= j + 1; col++)
                        {
                            color = workImage.GetPixel(row, col);
                            int intensitateR = color.R;
                            int intensitateG = color.G;
                            int intensitateB = color.B;

                            sumR = sumR + intensitateR * H[row - i + 1, col - j + 1];
                            sumG = sumG + intensitateG * H[row - i + 1, col - j + 1];
                            sumR = sumB + intensitateB * H[row - i + 1, col - j + 1];
                        }
                    }

                    sumR = sumR / ((n + 2) * (n + 2));
                    sumG = sumG / ((n + 2) * (n + 2));
                    sumB = sumB / ((n + 2) * (n + 2));

                    color = Color.FromArgb((int)sumR, (int)sumG, (int)sumB);
                    workImage.SetPixel(i, j, color);
                }
            }
            panelDestination.BackgroundImage = null;
            panelDestination.BackgroundImage = workImage.GetBitMap();
            workImage.Unlock();
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            Color color;
            image = new Bitmap(sSourceFileName);
            workImage = new FastImage(image);
            //workImage.Lock();

            List<int> listR = new List<int>();
            List<int> listG = new List<int>();
            List<int> listB = new List<int>();

            workImage.Lock();
            for (int i = 1; i < workImage.Width - 1; i++)
            {
                for (int j = 1; j < workImage.Height - 1; j++)
                {
                    for (int r = i - 1; r <= i + 1; r++)
                    {
                        for (int c = j - 1; c <= j + 1; c++)
                        {
                            color = workImage.GetPixel(r, c);
                            int R = color.R;
                            int G = color.G;
                            int B = color.B;

                            listR.Add(R);
                            listG.Add(G);
                            listB.Add(B);
                        }
                    }
                    listR.Sort();
                    listG.Sort();
                    listB.Sort();

                    color = Color.FromArgb(listR[4], listG[4], listB[4]);
                    workImage.SetPixel(i, j, color);

                    listR.Clear();
                    listG.Clear();
                    listB.Clear();
                }
            }
            panelDestination.BackgroundImage = null;
            panelDestination.BackgroundImage = workImage.GetBitMap();
            workImage.Unlock();
        }

        private int CBP(int x, int y, int CS, int SR, int T) 
        {
            image = new Bitmap(sSourceFileName);
            workImage = new FastImage(image);
            workImage.Lock();
            for (int i = x - SR; i < x + SR && i >= 0 && i < workImage.Width; i++) 
            {
                for (int j = y - SR; j < y + SR && j >= 0 && j < workImage.Height; j++) 
                {
                    if (i == x && j == y) 
                    {
                        continue;
                    }
                    if (SAD(x, y, i, j, CS) < T && !Salt_Pepper(i, j) == true) 
                    {
                        Q[i, j] = Q[i, j] + 1;
                    }
                }
            }
            workImage.Unlock();
            return Q.Cast<int>().Max();
        }

        private bool Salt_Pepper(int i, int j) 
        {
           int gray = (workImage.GetPixel(i, j).R + workImage.GetPixel(i, j).G + workImage.GetPixel(i, j).B) / 3;
           if (gray == 0 || gray == 255)
                return true;
            else
                return false;
        }

        private int SAD(int x1, int y1, int x2, int y2, int CS) 
        {
            image = new Bitmap(sSourceFileName);
            workImage = new FastImage(image);
            int S = 0;
            for (int i = (-1 * CS) / 2; i < CS / 2 && (i + x1) >= 0 && (i + x1) < workImage.Width && (i + x2) >= 0 && (i + x2) < workImage.Width; i ++) 
            {
                for (int j = (-1 * CS) / 2; j < CS / 2 && (j + y1) >= 0 && (j + y1) < workImage.Height && (j + y2) >= 0 && (j + y2) < workImage.Height; j++) 
                {
                    if (i == 0 && j == 0) 
                    {
                        continue;
                    }
                    Color c1 = workImage.GetPixel(i + x1, j + y1);
                    Color c2 = workImage.GetPixel(i + x2, j + y2);
                    int avg1 = (c1.R + c1.G + c1.B) / 3;
                    int avg2 = (c2.R + c2.G + c2.B) / 3;
                    S = S + Math.Abs(avg1 - avg2);
                }
            }
            return S;
        }

        private void CBPF(int CS, int SR, int T) 
        {
            image = new Bitmap(sSourceFileName);
            workImage = new FastImage(image);
            workImage.Lock();
            for (int i = 0; i < workImage.Width - 1; i++) 
            {
                for (int j = 0; j < workImage.Height - 1; j++) 
                {
                    if (Salt_Pepper(i, j))
                    {
                        int temp = CBP(i, j, CS, SR, T);
                        Color color = Color.FromArgb(temp, temp, temp);
                        workImage.SetPixel(i, j, color);
                    }
                }
            }
            panelDestination.BackgroundImage = null;
            panelDestination.BackgroundImage = workImage.GetBitMap();
            workImage.Unlock();
        }

        private void btnMarkov_Click(object sender, EventArgs e)
        {
            int CS = 3;
            int SR = 4;
            int T = 500;
            CBPF(CS, SR, T);
        }

        private void btnFTS_Click(object sender, EventArgs e)
        {
            Color color;
            image = new Bitmap(sSourceFileName);
            FastImage temp = new FastImage(image);

            temp.Lock();
            workImage.Lock();

            for (int i = 1; i < workImage.Width - 1; i++)
            {
                for (int j = 1; j < workImage.Height - 1; j++)
                {
                    color = workImage.GetPixel(i, j);
                    int R = color.R;
                    int G = color.G;
                    int B = color.B;

                    Color[,] matrix = new Color[,] { { workImage.GetPixel(i - 1, j - 1), workImage.GetPixel(i - 1, j) ,workImage.GetPixel(i - 1, j + 1)},
                                                    {workImage.GetPixel(i , j - 1), workImage.GetPixel(i , j) ,workImage.GetPixel(i , j + 1) },
                                                    { workImage.GetPixel(i + 1, j - 1), workImage.GetPixel(i + 1, j) ,workImage.GetPixel(i + 1, j + 1)} };

                    color = H(2, matrix);
                    temp.SetPixel(i, j, color);
                }
            }
            panelDestination.BackgroundImage = null;
            panelDestination.BackgroundImage = temp.GetBitMap();
            temp.Unlock();
            workImage.Unlock();
        }

        byte Norm(double sum)
        {
            if (sum < 0)
            {
                return 0;
            }
            else if (sum > 255)
            {
                return 255;
            }

            return (byte)sum;
        }
        Color H(int filtru, Color[,] matrix)
        {
            int[,] H;
            switch (filtru)
            {
                case 1:
                    {
                        H = new int[,] { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };
                        break;
                    }
                case 2:
                    {
                        H = new int[,] { { -1, -1, -1 }, { -1, 9, -1 }, { -1, -1, -1 } };
                        break;
                    }
                case 3:
                    {
                        H = new int[,] { { 1, -2, 1 }, { -2, 5, -2 }, { 1, -2, 1 } };
                        break;
                    }
                default:
                    {
                        H = new int[,] { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };
                        break;
                    }
            }

            double sumR = 0;
            double sumG = 0;
            double sumB = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    sumR += matrix[i, j].R * H[i, j];
                    sumG += matrix[i, j].G * H[i, j];
                    sumB += matrix[i, j].B * H[i, j];
                }
            }

            sumR = Norm(sumR);
            sumG = Norm(sumG);
            sumB = Norm(sumB);

            return Color.FromArgb((byte)sumR, (byte)sumG, (byte)sumB);
        }

        private int Normalizeaza(int value)
        {
            if (value > 255)
                return 255;
            if (value < 0)
                return 0;
            return value;
        }

        private int Normalizeaza(double value)
        {
            if (value > 255)
                return 255;
            if (value < 0)
                return 0;
            return (int)value;
        }

        private void btnUnsharp_Click(object sender, EventArgs e)
        {
            Color color;
            image = new Bitmap(sSourceFileName);
            FastImage temp = new FastImage(image);
            double c = 0.6;
            temp.Lock();
            workImage.Lock();

            for (int i = 1; i < workImage.Width - 1; i++)
            {
                for (int j = 1; j < workImage.Height - 1; j++)
                {
                    color = workImage.GetPixel(i, j);
                    int RF = color.R;
                    int GF = color.G;
                    int BF = color.B;

                    int sumR = workImage.GetPixel(i - 1, j - 1).R + workImage.GetPixel(i + 1, j + 1).R + workImage.GetPixel(i - 1, j + 1).R + workImage.GetPixel(i + 1, j - 1).R + workImage.GetPixel(i, j - 1).R + workImage.GetPixel(i, j + 1).R + workImage.GetPixel(i - 1, j).R + workImage.GetPixel(i + 1, j).R;
                    sumR = sumR / 8;

                    int sumG = workImage.GetPixel(i - 1, j - 1).G + workImage.GetPixel(i + 1, j + 1).G + workImage.GetPixel(i - 1, j + 1).G + workImage.GetPixel(i + 1, j - 1).G + workImage.GetPixel(i, j - 1).G + workImage.GetPixel(i, j + 1).G + workImage.GetPixel(i - 1, j).G + workImage.GetPixel(i + 1, j).G;
                    sumG = sumG / 8;

                    int sumB = workImage.GetPixel(i - 1, j - 1).B + workImage.GetPixel(i + 1, j + 1).B + workImage.GetPixel(i - 1, j + 1).B + workImage.GetPixel(i + 1, j - 1).B + workImage.GetPixel(i, j - 1).B + workImage.GetPixel(i, j + 1).B + workImage.GetPixel(i - 1, j).B + workImage.GetPixel(i + 1, j).B;
                    sumB = sumB / 8;

                    int medie = (sumR + sumG + sumB) / 3;
                    int medieRGB = (RF + GF + BF) / 3;

                    if (Math.Abs(medieRGB - medie) > 0.8)
                    {
                        color = Color.FromArgb((byte)sumR, (byte)sumG, (byte)sumB);
                    }

                    double R = (c * workImage.GetPixel(i, j).R) / (double)(2 * c - 1) - ((1 - c) * (byte)sumR) / (double)(2 * c - 1);
                    double G = (c * workImage.GetPixel(i, j).G) / (double)(2 * c - 1) - ((1 - c) * (byte)sumG) / (double)(2 * c - 1);
                    double B = (c * workImage.GetPixel(i, j).B) / (double)(2 * c - 1) - ((1 - c) * (byte)sumB) / (double)(2 * c - 1);

                    int r = Normalizeaza((int)R);
                    int b = Normalizeaza((int)B);
                    int g = Normalizeaza((int)G);
                    color = Color.FromArgb(r, g, b);
                    temp.SetPixel(i, j, color);
                }
            }
            panelDestination.BackgroundImage = null;
            panelDestination.BackgroundImage = temp.GetBitMap();
            temp.Unlock();
            workImage.Unlock();
        }

        private void btnKirsch_Click(object sender, EventArgs e)
        {
            Color color;
            image = new Bitmap(sSourceFileName);
            FastImage temp = new FastImage(image);
            temp.Lock();
            workImage.Lock();

            int[,] H1 = new int[3, 3] { { -1, 0, 1 }, { -1, 0, 1 }, { -1, 0, 1 } };
            int[,] H2 = new int[3, 3] { { 1, 1, 1 }, { 0, 0, 0 }, { -1, -1, -1 } };
            int[,] H3 = new int[3, 3] { { 0, 1, 1 }, { -1, 0, 1 }, { -1, -1, 0 } };
            int[,] H4 = new int[3, 3] { { 1, 1, 0 }, { 1, 0, -1 }, { 0, -1, -1 } };

            for (int i = 1; i < workImage.Width - 2; i++)
            {
                for (int j = 1; j < workImage.Height - 2; j++)
                {
                    int sumR = 0, sumG = 0, sumB = 0;
                    int sumR1 = 0, sumG1 = 0, sumB1 = 0;
                    int sumR2 = 0, sumG2 = 0, sumB2 = 0;
                    int sumR3 = 0, sumG3 = 0, sumB3 = 0;
                    int sumR4 = 0, sumG4 = 0, sumB4 = 0;
                    for (int row = i - 1; row <= i + 1; row++)
                    {
                        for (int col = j - 1; col <= j + 1; col++)
                        {
                            color = workImage.GetPixel(row, col);
                            int R = color.R;
                            int G = color.G;
                            int B = color.B;

                            sumR1 += R * H1[row - i + 1, col - j + 1];
                            sumG1 += G * H1[row - i + 1, col - j + 1];
                            sumB1 += B * H1[row - i + 1, col - j + 1];

                            sumR2 += R * H2[row - i + 1, col - j + 1];
                            sumG2 += G * H2[row - i + 1, col - j + 1];
                            sumB2 += B * H2[row - i + 1, col - j + 1];

                            sumR3 += R * H3[row - i + 1, col - j + 1];
                            sumG3 += G * H3[row - i + 1, col - j + 1];
                            sumB3 += B * H3[row - i + 1, col - j + 1];

                            sumR4 += R * H4[row - i + 1, col - j + 1];
                            sumG4 += G * H4[row - i + 1, col - j + 1];
                            sumB4 += B * H4[row - i + 1, col - j + 1];
                        }
                    }

                    sumR = Math.Max(Math.Max(sumR1, sumR2), Math.Max(sumR3, sumR4));
                    sumG = Math.Max(Math.Max(sumG1, sumG2), Math.Max(sumG3, sumG4));
                    sumB = Math.Max(Math.Max(sumB1, sumB2), Math.Max(sumB3, sumB4));

                    sumR = Normalizeaza(sumR);
                    sumG = Normalizeaza(sumG);
                    sumB = Normalizeaza(sumB);

                    color = Color.FromArgb(sumR, sumG, sumB);
                    temp.SetPixel(i, j, color);
                }
            }

            panelDestination.BackgroundImage = null;
            panelDestination.BackgroundImage = temp.GetBitMap();
            temp.Unlock();
            workImage.Unlock();
        }

        private void btnGabor_Click(object sender, EventArgs e)
        {
            //Gabor
            Color color;
            image = new Bitmap(sSourceFileName);
            FastImage temp = new FastImage(image);
            temp.Lock();
            workImage.Lock();

            int[,] P = new int[3, 3] { { 1, 1, 1 }, { 0, 0, 0  }, { -1, -1, -1 } };
            int[,] Q = new int[3, 3] { { -1, 0, 1 }, { -1, 0, 1 }, { -1, 0, 1 } };
            double pi = Math.PI;
            double teta = 0.66;
            double omega = 1.5;
            double euler = Math.E;

            for (int i = 1; i < workImage.Width - 2; i++)
            {
                for (int j = 1; j < workImage.Height - 2; j++)
                {
                    int sumRP = 0, sumGP = 0, sumBP = 0;
                    int sumRQ = 0, sumGQ = 0, sumBQ = 0;
                    for (int row = i - 1; row <= i + 1; row++)
                    {
                        for (int col = j - 1; col <= j + 1; col++)
                        {
                            color = workImage.GetPixel(row, col);
                            int R = color.R;
                            int G = color.G;
                            int B = color.B;

                            sumRP += R * P[row - i + 1, col - j + 1];
                            sumGP += G * P[row - i + 1, col - j + 1];
                            sumBP += B * P[row - i + 1, col - j + 1];

                            sumRQ += R * Q[row - i + 1, col - j + 1];
                            sumGQ += G * Q[row - i + 1, col - j + 1];
                            sumBQ += B * Q[row - i + 1, col - j + 1];
                        }
                    }
                    double uR = 0, uG = 0, uB = 0;

                    //Red
                    if (sumRQ == 0)
                    {
                        if (sumRP >= 0)
                            uR = pi / 2;
                        else if (sumRP < 0)
                            uR = -1 * pi / 2;
                    }
                    else 
                    {
                        uR = Math.Atan(sumRP / sumRQ);
                        if (sumRQ < 0)
                            uR = uR + pi;
                    }
                    uR = uR + (pi / 2);
                    
                    //Green
                    if (sumGQ == 0)
                    {
                        if (sumGP >= 0)
                            uG = pi / 2;
                        else if (sumGP < 0)
                            uG = -1 * pi / 2;
                    }
                    else
                    {
                        uG = Math.Atan(sumGP / sumGQ);
                        if (sumGQ < 0)
                            uG = uG + pi;
                    }
                    uG = uG + (pi / 2);

                    //Blue
                    if (sumBQ == 0)
                    {
                        if (sumBP >= 0)
                            uB = pi / 2;
                        else if (sumBP < 0)
                            uB = -1 * pi / 2;
                    }
                    else
                    {
                        uB = Math.Atan(sumBP / sumBQ);
                        if (sumBQ < 0)
                            uB = uB + pi;
                    }
                    uB = uB + (pi / 2);

                    double sumR = 0, sumG = 0, sumB = 0;
                    for (int row = i - 1; row <= i + 1; row++)
                    {
                        for (int col = j - 1; col <= j + 1; col++)
                        {
                            int pozR = row - i + 1;
                            int pozC = col - j + 1;
                            color = workImage.GetPixel(row, col);
                            int R = color.R;
                            int G = color.G;
                            int B = color.B;
                            double scaleR = 0, scaleG = 0, scaleB = 0;

                            //Red
                            scaleR = Math.Pow(euler, (-1 * ((Math.Pow(pozR, 2) + Math.Pow(pozC, 2)) / (2 * Math.Pow(teta, 2))))) * Math.Sin(omega * (pozR * Math.Cos(uR) + pozC * Math.Sin(uR)));
                            sumR += scaleR * R;
                            //Green
                            scaleG = Math.Pow(euler, (-1 * ((Math.Pow(pozR, 2) + Math.Pow(pozC, 2)) / (2 * Math.Pow(teta, 2))))) * Math.Sin(omega * (pozR * Math.Cos(uG) + pozC * Math.Sin(uG)));
                            sumG += scaleG * G;
                            //Blue
                            scaleB = Math.Pow(euler, (-1 * ((Math.Pow(pozR, 2) + Math.Pow(pozC, 2)) / (2 * Math.Pow(teta, 2))))) * Math.Sin(omega * (pozR * Math.Cos(uB) + pozC * Math.Sin(uB)));
                            sumB += scaleB * B;
                        }
                    }
                    //Setare Culoare si normalizare
                    int colorR = Normalizeaza(sumR);
                    int colorG = Normalizeaza(sumG);
                    int colorB = Normalizeaza(sumB);

                    color = Color.FromArgb(colorR, colorG, colorB);
                    temp.SetPixel(i, j, color);
                }
            }

            panelDestination.BackgroundImage = null;
            panelDestination.BackgroundImage = temp.GetBitMap();
            temp.Unlock();
            workImage.Unlock();
            //Gabor
        }

        private void panelSource_MouseClick(object sender, MouseEventArgs e)
        {
            //Region Growing
            int x, y;
            int prag = 100;
            workImage.Lock();
            workImage2.Lock();
            x = e.X * (workImage.Width / panelSource.Width);
            y = e.Y * (workImage.Height / panelSource.Height);

            Queue<Color> Q = new Queue<Color>();
            Queue<Color> R = new Queue<Color>();
            int[,] vizitat = new int[workImage.Width, workImage.Height];
            for (int i = 1; i < workImage.Width - 2; i++)
            {
                for (int j = 1; j < workImage.Height - 2; j++)
                {
                    vizitat[i, j] = 0;
                }
            }
            Color pixel = workImage.GetPixel(x, y);
            Q.Enqueue(pixel);
            R.Enqueue(pixel);
            double medie = 0;
            int avr = ((pixel.R + pixel.G + pixel.B) / 3);
            double suma = avr;
            while (Q.Count != 0)
            {
                if (x - 1 >= workImage.Width)
                {
                    Color pixelLeft = workImage.GetPixel(x - 1, y);
                    byte RL = pixelLeft.R;
                    byte GL = pixelLeft.G;
                    byte BL = pixelLeft.B;

                    byte averagePixelLeft = (byte)((RL + GL + BL) / 3);
                    medie = suma / R.Count;
                    if ((Math.Abs(averagePixelLeft - medie) < prag) && (vizitat[x - 1, y] == 0))
                    {
                        Q.Enqueue(pixelLeft);
                        R.Enqueue(pixelLeft);
                        suma += averagePixelLeft;
                        vizitat[x - 1, y] = 1;
                        workImage2.SetPixel(x - 1, y, pixelLeft);
                    }
                }
                if (x + 1 <= workImage.Width)
                {
                    Color pixelRight = workImage.GetPixel(x + 1, y);
                    byte RR = pixelRight.R;
                    byte GR = pixelRight.G;
                    byte BR = pixelRight.B;

                    byte averagePixelRight = (byte)((RR + GR + BR) / 3);
                    medie = suma / R.Count;
                    if ((Math.Abs(averagePixelRight - medie) < prag) && (vizitat[x + 1, y] == 0))
                    {
                        Q.Enqueue(pixelRight);
                        R.Enqueue(pixelRight);
                        suma += averagePixelRight;
                        vizitat[x + 1, y] = 1;
                        workImage2.SetPixel(x + 1, y, pixelRight);
                    }
                }
                if (y + 1 <= workImage.Height)
                {
                    Color pixelTop = workImage.GetPixel(x, y + 1);
                    byte RT = pixelTop.R;
                    byte GT = pixelTop.G;
                    byte BT = pixelTop.B;

                    byte averagePixelTop = (byte)((RT + GT + BT) / 3);
                    medie = suma / R.Count;
                    if ((Math.Abs(averagePixelTop - medie) < prag) && (vizitat[x, y + 1] == 0))
                    {
                        Q.Enqueue(pixelTop);
                        R.Enqueue(pixelTop);
                        suma += averagePixelTop;
                        vizitat[x, y + 1] = 1;
                        workImage2.SetPixel(x, y + 1, pixelTop);
                    }
                }
                if (y - 1 >= workImage.Height)
                {
                    Color pixelBottom = workImage.GetPixel(x, y - 1);
                    byte RB = pixelBottom.R;
                    byte GB = pixelBottom.G;
                    byte BB = pixelBottom.B;

                    byte averagePixelBottom = (byte)((RB + GB + BB) / 3);
                    medie = suma / R.Count;
                    if ((Math.Abs(averagePixelBottom - medie) < prag) && (vizitat[x, y - 1] == 0))
                    {
                        Q.Enqueue(pixelBottom);
                        R.Enqueue(pixelBottom);
                        suma += averagePixelBottom;
                        vizitat[x, y - 1] = 1;
                        workImage2.SetPixel(x, y - 1, pixelBottom);
                    }
                }
                Q.Dequeue();
            }
            panelDestination.BackgroundImage = null;
            panelDestination.BackgroundImage = workImage2.GetBitMap();
            workImage.Unlock();
            workImage2.Unlock();
        }

        private void btnSaM_Click(object sender, EventArgs e)
        {
            double T;
            T = Convert.ToDouble(boxSaM.Text);

            Color color;
            image = new Bitmap(sSourceFileName);
            FastImage temp = new FastImage(image);
            temp.Lock();
            workImage.Lock();

            //----------------------------------------------
            double N = workImage.Width * workImage.Height;
            for (int i = 1; i < workImage.Width - 2; i++)
            {
                for (int j = 1; j < workImage.Height - 2; j++)
                {
                    int colorR = 0, colorG = 0, colorB = 0;
                    double IR = 0, IG = 0, IB = 0;//I
                    double DR = 0, DG = 0, DB = 0;//Sigma
                    for (int row = i - 1; row <= i + 1; row++)
                    {
                        for (int col = j - 1; col <= j + 1; col++) 
                        {
                            color = workImage.GetPixel(row, col);
                            colorR = color.R;
                            colorG = color.G;
                            colorB = color.B;

                            IR += colorR;
                            IG += colorG;
                            IB += colorB;
                        }
                    }
                    IR /= N;
                    IG /= N;
                    IB /= N;
                    for (int row = i - 1; row <= i + 1; row++)
                    {
                        for (int col = j - 1; col <= j + 1; col++)
                        {
                            color = workImage.GetPixel(row, col);
                            colorR = color.R;
                            colorG = color.G;
                            colorB = color.B;

                            DR += Math.Pow(colorR - IR, 2);
                            DG += Math.Pow(colorG - IG, 2);
                            DB += Math.Pow(colorB - IB, 2);
                        }
                    }
                    DR /= (N - 1);
                    DG /= (N - 1);
                    DB /= (N - 1);
                    N /= 4;
                    _ = DR < T ? true : false;
                    _ = DG < T ? true : false;
                    _ = DB < T ? true : false;

                    colorR = Normalizeaza(colorR);
                    colorG = Normalizeaza(colorG);
                    colorB = Normalizeaza(colorB);
                    color = Color.FromArgb(colorR, colorG, colorB);
                    temp.SetPixel(i, j, color);
                }
            }
            //----------------------------------------------

            panelDestination.BackgroundImage = null;
            panelDestination.BackgroundImage = temp.GetBitMap();
            temp.Unlock();
            workImage.Unlock();
        }
    }
}