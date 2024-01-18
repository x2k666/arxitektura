using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

public class GraphicalCalculator : Form
{
    private Button squareButton;
    private Button cubeButton;
    private Button linearButton;
    private Button sineButton;
    private Button cosineButton;
    private PictureBox plotPictureBox;

    public GraphicalCalculator()
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        squareButton = new Button();
        cubeButton = new Button();
        linearButton = new Button();
        sineButton = new Button();
        cosineButton = new Button();
        plotPictureBox = new PictureBox();

        squareButton.Location = new System.Drawing.Point(20, 20);
        squareButton.Size = new System.Drawing.Size(100, 30);
        squareButton.Text = "x^2";
        squareButton.Click += new EventHandler(OnPlotSquare);

        cubeButton.Location = new System.Drawing.Point(130, 20);
        cubeButton.Size = new System.Drawing.Size(100, 30);
        cubeButton.Text = "x^3";
        cubeButton.Click += new EventHandler(OnPlotCube);

        linearButton.Location = new System.Drawing.Point(240, 20);
        linearButton.Size = new System.Drawing.Size(100, 30);
        linearButton.Text = "x";
        linearButton.Click += new EventHandler(OnPlotLinear);

        sineButton.Location = new System.Drawing.Point(20, 60);
        sineButton.Size = new System.Drawing.Size(100, 30);
        sineButton.Text = "sin(x)";
        sineButton.Click += new EventHandler(OnPlotSine);

        cosineButton.Location = new System.Drawing.Point(130, 60);
        cosineButton.Size = new System.Drawing.Size(100, 30);
        cosineButton.Text = "cos(x)";
        cosineButton.Click += new EventHandler(OnPlotCosine);

        plotPictureBox.Location = new System.Drawing.Point(20, 100);
        plotPictureBox.Size = new System.Drawing.Size(400, 300);

        this.Controls.Add(squareButton);
        this.Controls.Add(cubeButton);
        this.Controls.Add(linearButton);
        this.Controls.Add(sineButton);
        this.Controls.Add(cosineButton);
        this.Controls.Add(plotPictureBox);

        this.Size = new System.Drawing.Size(450, 450);
        this.Text = "Graphical Calculator";
    }

    private void OnPlotSquare(object sender, EventArgs e)
    {
        PlotFunction(x => x * x);
    }

    private void OnPlotCube(object sender, EventArgs e)
    {
        PlotFunction(x => x * x * x);
    }

    private void OnPlotLinear(object sender, EventArgs e)
    {
        PlotFunction(x => x);
    }

    private void OnPlotSine(object sender, EventArgs e)
    {
        PlotFunction(x => (float)Math.Sin(x));
    }

    private void OnPlotCosine(object sender, EventArgs e)
    {
        PlotFunction(x => (float)Math.Cos(x));
    }

    private void PlotFunction(Func<float, float> function)
    {
        Bitmap plotBitmap = new Bitmap(plotPictureBox.Width, plotPictureBox.Height);
        using (Graphics g = Graphics.FromImage(plotBitmap))
        {
            Pen pen = new Pen(Color.Blue);
            float scaleX = plotPictureBox.Width / 10.0f;
            float scaleY = plotPictureBox.Height / 2.0f;

            float prevX = -5.0f;
            float prevY = function(prevX);

            for (float x = -5.0f; x <= 5.0f; x += 0.1f)
            {
                float y = function(x);
                float pixelX = x * scaleX + plotPictureBox.Width / 2;
                float pixelY = -y * scaleY + plotPictureBox.Height / 2;

                g.DrawLine(pen, prevX * scaleX + plotPictureBox.Width / 2, -prevY * scaleY + plotPictureBox.Height / 2, pixelX, pixelY);

                prevX = x;
                prevY = y;
            }
        }

        plotPictureBox.Image = plotBitmap;
    }

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new GraphicalCalculator());
    }
}