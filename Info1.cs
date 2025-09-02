using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace practik
{
    public partial class Info1 : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public Info1()
        {
            this.ClientSize = new Size(530, 250);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;

            string imgPath(string fileName) => Path.Combine(Application.StartupPath, "blue", fileName);

            this.BackgroundImage = Image.FromFile(imgPath("infobackground.png"));
            this.BackgroundImageLayout = ImageLayout.Stretch;

            PictureBox contentImage = new PictureBox()
            {
                Image = Image.FromFile(imgPath("infotext2.png")),
                Location = new Point(5, 6),
                Size = new Size(519, 240),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };

            Button exitBtn = new Button()
            {
                Location = new Point(505, 5),
                Size = new Size(20, 20),
                BackgroundImage = Image.FromFile(imgPath("exit.png")),
                BackgroundImageLayout = ImageLayout.Stretch,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat
            };

            exitBtn.FlatAppearance.BorderSize = 0;
            exitBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            exitBtn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            exitBtn.Click += (s, e) =>
            {
                Form1 form = new Form1();
                form.Show();
                this.Close();
            };

            this.Controls.Add(contentImage);
            this.Controls.Add(exitBtn);
            exitBtn.BringToFront();

            this.MouseDown += (s, e) =>
            {
                dragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = this.Location;
            };

            this.MouseMove += (s, e) =>
            {
                if (dragging)
                {
                    Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                    this.Location = Point.Add(dragFormPoint, new Size(diff));
                }
            };

            this.MouseUp += (s, e) => dragging = false;
        }
    }
}
