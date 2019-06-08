using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace GrafRed
{
    public partial class MDIParent1 : Form
    {
        private int childFormNumber = 0;
        String childFormActive = "";
        int childActive;
        Bitmap bmp1, bmp2;
        int width, height, width2, height2;
        List<String> filenameActiveChildForm = new List<String>();
        Color clr = Color.Black;
        List<Brush> fillBrush = new List<Brush>();
        Color clrBr = Color.White;
        Color clrWh = Color.White;
        Color next;
        int r = 0, g = 0, b = 0;
        String line="";
        

        public MDIParent1()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            MDIChildForm childForm = new MDIChildForm(this);
            childForm.MdiParent = this;
            childForm.Text = "Окно " + childFormNumber++;
            childForm.Show();
            width = childForm.pictureBox1.Width;
            height = childForm.pictureBox1.Height;
            filenameActiveChildForm.Add("");
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Все файлы (*.*)|*.*| Изображение jpg (*.jpg)|*.jpg| Изображение bmp (*.bmp)|*.bmp| Изображение gif (*.gif)|*.gif| Изображение png (*.png)|*.png";
            if (((MDIChildForm)this.ActiveMdiChild) == null)
                ShowNewForm(this, null);
            childFormActive = ((MDIChildForm)this.ActiveMdiChild).Text;
            childActive = Convert.ToInt32(childFormActive.Substring(5));
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                filenameActiveChildForm[childActive] = openFileDialog1.FileName;
                bmp1 = new Bitmap(filenameActiveChildForm[childActive]);
                bmp2 = new Bitmap(width, height);
                width2 = bmp1.Width;
                height2 = bmp1.Height;
                if (((MDIChildForm)this.ActiveMdiChild).pictureBox1.Image != null)
                {
                    ((MDIChildForm)this.ActiveMdiChild).pictureBox1.Image.Dispose();
                }
                if (width2 < width)
                {
                    for (int i = 0; i < width; i++)
                        for (int j = 0; j < height; j++)
                        {
                            if (i < width2 && j < height2)
                            {
                                Color cur = bmp1.GetPixel(i, j);
                                r = cur.R;
                                g = cur.G;
                                b = cur.B;
                                next = Color.FromArgb((byte)r, (byte)g, (byte)b);
                                bmp2.SetPixel(i, j, next);
                            }
                            else
                                bmp2.SetPixel(i, j, clrWh);
                        }
                }
                else if (height2 < height)
                { 
                    for (int i = 0; i < width; i++)
                        for (int j = 0; j < height; j++)
                        {
                            if (j < height2)
                            {
                                Color cur = bmp1.GetPixel(i, j);
                                r = cur.R;
                                g = cur.G;
                                b = cur.B;
                                next = Color.FromArgb((byte)r, (byte)g, (byte)b);
                                bmp2.SetPixel(i, j, next);
                            }
                            else
                                bmp2.SetPixel(i, j, clrWh);
                        }
                }
                else 
                {
                    for (int i = 0; i < width; i++)
                        for (int j = 0; j < height; j++)
                        {
                            Color cur = bmp1.GetPixel(i, j);
                            r = cur.R;
                            g = cur.G;
                            b = cur.B;
                            next = Color.FromArgb((byte)r, (byte)g, (byte)b);
                            bmp2.SetPixel(i, j, next);
                        }
                }
                ((MDIChildForm)this.ActiveMdiChild).bmpOpen = bmp2;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((MDIChildForm)this.ActiveMdiChild).pictureBox1.Image != null)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Изображение bmp (*.bmp)|*.bmp| Изображение jpg (*.jpg)|*.jpg| Изображение gif (*.gif)|*.gif| Изображение png (*.png)|*.png| ";
                childFormActive = ((MDIChildForm)this.ActiveMdiChild).Text;
                childActive = Convert.ToInt32(childFormActive.Substring(5));
                if (filenameActiveChildForm[childActive] == "")
                {
                    if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
                    {
                        filenameActiveChildForm[childActive] = saveFileDialog1.FileName;
                        Image bmpSave = ((MDIChildForm)this.ActiveMdiChild).pictureBox1.Image;
                        bmpSave = new Bitmap(bmpSave);
                        bmpSave.Save(filenameActiveChildForm[childActive]);
                    }
                }
                else
                {
                    Image bmpSave = ((MDIChildForm)this.ActiveMdiChild).pictureBox1.Image;
                    bmpSave = new Bitmap(bmpSave);
                    bmpSave.Save(filenameActiveChildForm[childActive]);
                }
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Изображение bmp (*.bmp)|*.bmp| Изображение jpg (*.jpg)|*.jpg| Изображение gif (*.gif)|*.gif| Изображение png (*.png)|*.png|Все файлы (*.*)|*.*";
            childFormActive = ((MDIChildForm)this.ActiveMdiChild).Text;
            childActive = Convert.ToInt32(childFormActive.Substring(5));
            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                filenameActiveChildForm[childActive] = saveFileDialog1.FileName;
                Image bmpSave = ((MDIChildForm)this.ActiveMdiChild).pictureBox1.Image; 
                bmpSave = new Bitmap(bmpSave);
                bmpSave.Save(filenameActiveChildForm[childActive]);
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((MDIChildForm)this.ActiveMdiChild).tool = 5; 
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((MDIChildForm)this.ActiveMdiChild).tool = 6;
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((MDIChildForm)this.ActiveMdiChild).tool = 7;
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((MDIChildForm)this.ActiveMdiChild).tool = 8;
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ((MDIChildForm)this.ActiveMdiChild).tool = 2;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ((MDIChildForm)this.ActiveMdiChild).tool = 3;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ((MDIChildForm)this.ActiveMdiChild).tool = 4;
        }

        private void brushSolidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((MDIChildForm)this.ActiveMdiChild).tool = 1;
        }

        private void solidColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog1 = new ColorDialog();
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                clrBr = colorDialog1.Color;
                ((MDIChildForm)this.ActiveMdiChild).clrFil = clrBr;
                button2.BackColor = clrBr;
            }
        }

        private void nonFillingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((MDIChildForm)this.ActiveMdiChild).penNonFil = 4;
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog1 = new ColorDialog();
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                clr = colorDialog1.Color;
                ((MDIChildForm)this.ActiveMdiChild).clr1 = clr;
                button1.BackColor = clr;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Курсовая работа\nMDI графический редактор\nВыполнила Иванина Ксения", "О программе");
        }

        private void linePrint()
        {
            Form Form2 = new Form();
            TextBox textBox1 = new TextBox();
            Label label1 = new Label();
            Button button1 = new Button();
            
            Form2.Bounds = new Rectangle(0, 0, 360, 140);
            Form2.Text = "Подпись для печати";
            Form2.StartPosition = FormStartPosition.CenterParent;
            Form2.MaximizeBox = false;
            label1.Location = new System.Drawing.Point(100,12);
            label1.Text = "Введите подпись для печати";
            label1.Size = new System.Drawing.Size(250, 20);
            textBox1.Location = new System.Drawing.Point(25, 40);
            textBox1.Size = new System.Drawing.Size(300, 20);
            button1.DialogResult = DialogResult.OK;
            button1.Text = "OK";
            button1.Location = new System.Drawing.Point(130, 72);
            Form2.Controls.Add(label1);
            Form2.Controls.Add(textBox1);
            Form2.Controls.Add(button1);
            if (Form2.ShowDialog(this) == DialogResult.OK)
            {
                line = textBox1.Text;
            }
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            linePrint();
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics grap = e.Graphics;
            float leftMargin = e.MarginBounds.Left-22;
            float topMargin = e.MarginBounds.Top-20;
            Bitmap bmpPrint = new Bitmap(((MDIChildForm)this.ActiveMdiChild).pictureBox1.Image);
            grap.DrawImage(bmpPrint, leftMargin, topMargin);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            float x = leftMargin;
            float y = topMargin + ((MDIChildForm)this.ActiveMdiChild).pictureBox1.Image.Height + 30;
            float width = ((MDIChildForm)this.ActiveMdiChild).pictureBox1.Image.Width + 25;
            float height = 30;
            Rectangle rect = new Rectangle((int)x, (int)y, (int)width, (int)height);
            grap.DrawString(line, new Font("Times New Roman", 16, FontStyle.Regular), new SolidBrush(Color.Black), rect, stringFormat);
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;
            linePrint();
            printDialog1.ShowDialog();
        }
    }
}
