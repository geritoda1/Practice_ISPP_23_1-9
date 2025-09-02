using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace practik
{
    public partial class Info4 : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public Info4()
        {
            this.ClientSize = new Size(530, 250);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "purple", "infobackground.png"));
            this.BackgroundImageLayout = ImageLayout.Stretch;

            PictureBox contentImage = new PictureBox()
            {
                Image = Image.FromFile(Path.Combine(Application.StartupPath, "purple", "infotext2.png")),
                Location = new Point(5, 6),
                Size = new Size(519, 240),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };

            Button exitBtn = new Button()
            {
                Location = new Point(505, 5),
                Size = new Size(20, 20),
                BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "purple", "exit.png")),
                BackgroundImageLayout = ImageLayout.Stretch,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat
            };

            exitBtn.FlatAppearance.BorderSize = 0;
            exitBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            exitBtn.FlatAppearance.MouseDownBackColor = Color.Transparent;

            exitBtn.Click += (s, e) =>
            {
                Form10 f4 = new Form10();
                f4.Show();
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
