﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatrixVisualization
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DisplayMatrix(new[,] {{1, 0, 1, 0, 0}, {0, 1, 1, 0, 0}});
        }

        public void DisplayMatrix(int[,] matrix, int threshold = int.MaxValue)
        {
            var m = matrix.GetLength(0);
            var n = matrix.GetLength(1);
            var cellSize = 50;
            var bmp = new Bitmap(m*cellSize, n*cellSize);
            var graphics = Graphics.FromImage(bmp);

            var c = new[] {255, 219, 182, 146, 109, 73, 36, 0};
            var brushes = Enumerable.Range(0, 8).Select(i => new SolidBrush(Color.FromArgb(255, c[i], c[i], c[i]))).ToList();
            var max = int.MinValue;
            for (var i = 0; i < m; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    if (matrix[i, j] > max) max = matrix[i, j];
                }
            }
            max = max > threshold ? threshold : max;
            var unit = max/8.0;

            graphics.Clear(Color.White);
            for (var i = 0; i < m; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    var value = matrix[i, j] > threshold ? threshold : matrix[i, j];
                    graphics.FillRectangle(brushes[value/(int) Math.Ceiling(unit)], i*cellSize, j*cellSize, cellSize, cellSize);
                }
            }

            pictureBox1.Image = bmp;
            Refresh();
        }
    }
}