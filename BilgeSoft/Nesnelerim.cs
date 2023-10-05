using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
namespace BilgeSoft
{

    class Nesnelerim
    {
    }
    class lStandart :Label
    {
        public lStandart()
        {
            this.ForeColor = System.Drawing.Color.DarkCyan;
            this.Text = "lStandart";
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "lStandart";
        }
    }
    class bStandart : Button
    {
        public bStandart()
        {
            this.BackColor = System.Drawing.Color.OrangeRed;
            this.FlatAppearance.BorderColor = System.Drawing.Color.Tomato;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ForeColor = System.Drawing.Color.White;
            this.Location = new System.Drawing.Point(1, 1);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "bStandart";
            this.Size = new System.Drawing.Size(127, 137);
            this.TabIndex = 0;
            this.Text = "bStandart";
            this.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.UseVisualStyleBackColor = false;
        }
    }
    class tStandart :TextBox
    {
        public tStandart()
        {
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Location = new System.Drawing.Point(106, 61);
            this.Name = "tStandart";
            this.Size = new System.Drawing.Size(232, 26);
            this.TabIndex = 1;
        }
    }
    class tNumeric : TextBox
    {
        public tNumeric()
        {
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Name = "tNumeric";
            this.Size = new System.Drawing.Size(115, 26);
            this.BackColor = System.Drawing.Color.White;
            this.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Click += TNumeric_Click;
            this.KeyPress += TNumeric_KeyPress;
        }

        private void TNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar)==false&&e.KeyChar!=(char)08&&e.KeyChar!=(char)44)
            {
                e.Handled = true;
            }
        }

        private void TNumeric_Click(object sender, EventArgs e)
        {
            this.SelectAll();
        }
    }
    class gridOzel : DataGridView
    {
        public gridOzel()
        {
            this.ReadOnly = true;
            this.AllowUserToAddRows = false;
            this.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.BackgroundColor = System.Drawing.Color.SeaGreen;
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(3);
            this.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnHeadersDefaultCellStyle = this.DefaultCellStyle;
            this.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EnableHeadersVisualStyles = false;
            this.Location = new System.Drawing.Point(3, 103);
            this.Name = "gridSatisListesi";
            this.RowHeadersVisible = false;
            this.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(3);
            this.RowsDefaultCellStyle = this.DefaultCellStyle;
            this.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(3);
            this.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Silver;
            this.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.RowTemplate.Height = 32;
            this.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Size = new System.Drawing.Size(639, 548);
        }
    }

    class tGuna : Guna2TextBox
    {
        public tGuna()
        {
            
            this.AutoRoundedCorners = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.BorderRadius = 14;
            this.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.DefaultText = "kullanıcı adı";
            this.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Font = new System.Drawing.Font("Segoe UI Historic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Location = new System.Drawing.Point(138, 139);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "guna2TextBox1";
            this.PasswordChar = '\0';
            this.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.PlaceholderText = "Kullanıcı Adı";
            this.SelectedText = "";
            this.ShadowDecoration.BorderRadius = 20;
            this.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.ShadowDecoration.Enabled = true;
            this.Size = new System.Drawing.Size(200, 30);
            
        }
    }
    class tGunaNumeric : Guna2TextBox
    {
        public tGunaNumeric()
        {
            this.Name = "tNumeric";
            this.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            this.AutoRoundedCorners = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.BorderRadius = 14;
            this.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Font = new System.Drawing.Font("Segoe UI Historic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)), true);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Location = new System.Drawing.Point(138, 139);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "guna2TextBox1";
            this.PasswordChar = '\0';
            this.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.SelectedText = "";
            this.ShadowDecoration.BorderRadius = 20;
            this.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.ShadowDecoration.Enabled = true;
            this.Size = new System.Drawing.Size(120, 30);
            this.Click += TGunaNumeric_Click;
            this.KeyPress += TGunaNumeric_KeyPress;
        }

        private void TGunaNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)08 && e.KeyChar != (char)44)
            {
                e.Handled = true;
            }
        }

        private void TGunaNumeric_Click(object sender, EventArgs e)
        {
            this.SelectAll();
        }
    }

    class tGunaButton : Guna2Button
    {
        public tGunaButton()
        {
            this.Animated = true;
            //this.AutoRoundedCorners = true;
            this.BackColor = System.Drawing.Color.Transparent;
            //this.BorderRadius = 34;
            this.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.FillColor = System.Drawing.Color.DarkGoldenrod;
            this.FocusedColor = System.Drawing.Color.Goldenrod;
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ForeColor = System.Drawing.Color.White;
            this.HoverState.BorderColor = System.Drawing.Color.Goldenrod;
            this.HoverState.CustomBorderColor = System.Drawing.Color.Goldenrod;
            this.HoverState.FillColor = System.Drawing.Color.Goldenrod;
            this.Location = new System.Drawing.Point(357, 378);
            this.Name = "guna2Button1";
            this.ShadowDecoration.BorderRadius = 40;
            this.ShadowDecoration.Color = System.Drawing.Color.DarkKhaki;
            this.ShadowDecoration.Enabled = true;
            this.Size = new System.Drawing.Size(123, 70);
            this.TabIndex = 10;
            this.Text = "ButtonS";
        }
    }
    class tGunaButtonIcon : Guna2Button
    {
        public tGunaButtonIcon()
        {
            this.Animated = true;
            //this.AutoRoundedCorners = true;
            this.BackColor = System.Drawing.Color.Transparent;
            //this.BorderRadius = 34;
            this.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.FillColor = System.Drawing.Color.DarkGoldenrod;
            this.FocusedColor = System.Drawing.Color.Goldenrod;
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ForeColor = System.Drawing.Color.White;
            this.HoverState.BorderColor = System.Drawing.Color.Goldenrod;
            this.HoverState.CustomBorderColor = System.Drawing.Color.Goldenrod;
            this.HoverState.FillColor = System.Drawing.Color.Goldenrod;
            this.ImageAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ImageSize = new System.Drawing.Size(40, 40);
            this.Location = new System.Drawing.Point(357, 288);
            this.Name = "guna2Button2";
            this.ShadowDecoration.BorderRadius = 40;
            this.ShadowDecoration.Color = System.Drawing.Color.DarkKhaki;
            this.ShadowDecoration.Enabled = true;
            this.Size = new System.Drawing.Size(123, 70);
            this.TabIndex = 12;
            this.Text = "İconS";
            this.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
        }
    }

    class tGunaLabel : Guna2HtmlLabel
    {
        public tGunaLabel()
        {
            this.BackColor = System.Drawing.Color.Transparent;
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.Location = new System.Drawing.Point(138, 288);
            this.Name = "guna2HtmlLabel1";
            this.Size = new System.Drawing.Size(127, 23);
            this.TabIndex = 13;
            this.Text = "guna2HtmlLabel1";
        }
    }
}
