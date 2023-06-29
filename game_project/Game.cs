using System;
using System.Windows.Forms;
using System.Drawing;

class Game : Form
{
    private Ball bl;
    private Ball ubl;
    private Ball kbl;
    private Cart ct;
    private Image im;
    private int dx, dy;
    private int ux, uy;
    private int kx, ky;
    private bool isOver;
    private bool isIn;

    public static void Main()
    {
        Application.Run(new Game());

    }
    public Game()
    {
        this.Text = "ゲーム";
        this.ClientSize = new Size(600, 300);
        this.DoubleBuffered = true;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.MinimizeBox = false;

        // 背景
        im = Image.FromFile("c:\\sky.bmp");

        isOver = false;
        isIn = false;

        // モモンガ
        bl = new Ball();

        Point blp = new Point(0, 0);
        Image bim = Image.FromFile("c:\\momo.png");

        bl.Point = blp;
        bl.Image = bim;

        dx = 10;
        dy = 10;

        // ウサギ
        ubl = new Ball();

        Point ublp = new Point(0, 0);
        Image ubim = Image.FromFile("c:\\hachi.png");

        ubl.Point = ublp;
        ubl.Image = ubim;

        ux = 5;
        uy = 5;

        // くりまんじゅう
        kbl = new Ball();

        Point kblp = new Point(0, 0);
        Image kbim = Image.FromFile("c:\\chiikawa.png");

        kbl.Point = kblp;
        kbl.Image = kbim;

        kx = 7;
        ky = 7;


        // ラッコ
        ct = new Cart();

        Point ctp = new Point(this.ClientSize.Width / 2, this.ClientSize.Height - 80);
        Image cim = Image.FromFile("c:\\rakko.png");

        ct.Point = ctp;
        ct.Image = cim;
        Timer tm = new Timer();
        tm.Interval = 100;
        tm.Start();

        this.Paint += new PaintEventHandler(fm_Paint);
        tm.Tick += new EventHandler(tm_Tick);

        this.KeyDown += new KeyEventHandler(fm_KeyDown);
    }


    public void fm_Paint(Object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;

        // 背景の描画
        g.DrawImage(im, 0, 0, im.Width, im.Height);

        // モモンガの描画
        Point blp = bl.Point;
        Image bim = bl.Image;

        g.DrawImage(bl.Image, blp.X, blp.Y, bim.Width, bim.Height);

        // ウサギの描画
        Point ublp = ubl.Point;
        Image ubim = ubl.Image;
        g.DrawImage(ubl.Image, ublp.X, ublp.Y, ubim.Width, ubim.Height);

        // くりまんじゅうの描画
        Point kblp = kbl.Point;
        Image kbim = kbl.Image;
        g.DrawImage(kbl.Image, kblp.X, kblp.Y, kbim.Width, kbim.Height);

        // ラッコの描画
        Point ctp = ct.Point;
        Image cim = ct.Image;
        g.DrawImage(ct.Image, ctp.X, ctp.Y, cim.Width, cim.Height);

        // ゲームオーバーのとき
        if (isOver == true)
        {
            Font f = new Font("SansSerif", 30);
            SizeF s = g.MeasureString("Game Over", f);

            float cx = (this.ClientSize.Width - s.Width) / 2;
            float cy = (this.ClientSize.Height - s.Height) / 2;

            g.DrawString("Game Over", f, new SolidBrush(Color.Blue), cx, cy);

        }

    }

    public void tm_Tick(Object sender, EventArgs e)
    {
        Point blp = bl.Point;
        Point ctp = ct.Point;
        Point ublp = ubl.Point;
        Point kblp = kbl.Point;

        Image bim = bl.Image;
        Image cim = ct.Image;
        Image ubim = ubl.Image;
        Image kbim = kbl.Image;

        // モモンガ
        if (blp.X < 0 || blp.X > this.ClientSize.Width - bim.Width)
            dx = -dx;
        if (blp.Y < 0) dy = -dy;
        if ((isIn == false) && (blp.X > ctp.X - bim.Width
            && blp.X < ctp.X + cim.Width)
            && (blp.Y > ctp.Y - bim.Height
            && blp.Y < ctp.Y - bim.Height / 2))
        {
            isIn = true;
            dy = -dy;
        }
        if (blp.Y < ctp.Y - bim.Height)
        {
            isIn = false;
        }
        if (blp.Y > this.ClientSize.Height)
        {
            isOver = true;
            Timer t = (Timer)sender;
            t.Stop();
        }

        blp.X = blp.X + dx;
        blp.Y = blp.Y + dy;

        bl.Point = blp;

        // ウサギ
        if (ublp.X < 0 || ublp.X > this.ClientSize.Width - ubim.Width)
            ux = -ux;
        if (ublp.Y < 0) uy = -uy;
        if ((isIn == false) && (ublp.X > ctp.X - ubim.Width
            && ublp.X < ctp.X + cim.Width)
            && (ublp.Y > ctp.Y - ubim.Height
            && ublp.Y < ctp.Y - ubim.Height / 2))
        {
            isIn = true;
            uy = -uy;
        }
        if (ublp.Y < ctp.Y - ubim.Height)
        {
            isIn = false;
        }
        if (ublp.Y > this.ClientSize.Height)
        {
            isOver = true;
            Timer t = (Timer)sender;
            t.Stop();
        }

        ublp.X = ublp.X + ux;
        ublp.Y = ublp.Y + uy;

        ubl.Point = ublp;

        // くりまんじゅう
        if (kblp.X < 0 || kblp.X > this.ClientSize.Width - kbim.Width)
            kx = -kx;
        if (kblp.Y < 0) ky = -ky;
        if ((isIn == false) && (kblp.X > ctp.X - kbim.Width
            && kblp.X < ctp.X + cim.Width)
            && (kblp.Y > ctp.Y - kbim.Height
            && kblp.Y < ctp.Y - kbim.Height / 2))
        {
            isIn = true;
            ky = -ky;
        }
        if (kblp.Y < ctp.Y - kbim.Height)
        {
            isIn = false;
        }
        if (kblp.Y > this.ClientSize.Height)
        {
            isOver = true;
            Timer t = (Timer)sender;
            t.Stop();
        }

        kblp.X = kblp.X + kx;
        kblp.Y = kblp.Y + ky;

        kbl.Point = kblp;

        this.Invalidate();

    }

    public void fm_KeyDown(Object sender, KeyEventArgs e)
    //ラッコ
    {
        Point ctp = ct.Point;
        Image cim = ct.Image;

        if (e.KeyCode == Keys.Right)
        {
            ctp.X = ctp.X + 20;
            if (ctp.X > this.ClientSize.Width - cim.Width)
                ctp.X = this.ClientSize.Width - cim.Width;
        }
        else if (e.KeyCode == Keys.Left)
        {
            ctp.X = ctp.X - 20;
            if (ctp.X < 0)
                ctp.X = 0;
        }

        ct.Point = ctp;
        this.Invalidate();
    }

}

class Ball
{
    public Image Image;
    public Point Point;
}

class Cart
{
    public Image Image;
    public Point Point;
}