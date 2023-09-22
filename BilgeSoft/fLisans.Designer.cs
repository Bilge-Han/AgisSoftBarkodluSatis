
namespace BilgeSoft
{
    partial class fLisans
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tLisans = new BilgeSoft.tNumeric();
            this.lStandart1 = new BilgeSoft.lStandart();
            this.lKontrolNo = new BilgeSoft.lStandart();
            this.bTamam = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tLisans
            // 
            this.tLisans.BackColor = System.Drawing.Color.White;
            this.tLisans.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tLisans.Location = new System.Drawing.Point(114, 67);
            this.tLisans.Name = "tLisans";
            this.tLisans.Size = new System.Drawing.Size(115, 26);
            this.tLisans.TabIndex = 0;
            this.tLisans.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lStandart1
            // 
            this.lStandart1.AutoSize = true;
            this.lStandart1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lStandart1.ForeColor = System.Drawing.Color.DarkCyan;
            this.lStandart1.Location = new System.Drawing.Point(110, 9);
            this.lStandart1.Name = "lStandart1";
            this.lStandart1.Size = new System.Drawing.Size(111, 20);
            this.lStandart1.TabIndex = 2;
            this.lStandart1.Text = "KONTROL NO";
            // 
            // lKontrolNo
            // 
            this.lKontrolNo.AutoSize = true;
            this.lKontrolNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lKontrolNo.ForeColor = System.Drawing.Color.DarkCyan;
            this.lKontrolNo.Location = new System.Drawing.Point(122, 29);
            this.lKontrolNo.Name = "lKontrolNo";
            this.lKontrolNo.Size = new System.Drawing.Size(83, 20);
            this.lKontrolNo.TabIndex = 3;
            this.lKontrolNo.Text = "lStandart2";
            // 
            // bTamam
            // 
            this.bTamam.Location = new System.Drawing.Point(130, 110);
            this.bTamam.Name = "bTamam";
            this.bTamam.Size = new System.Drawing.Size(75, 23);
            this.bTamam.TabIndex = 4;
            this.bTamam.Text = "Tamam";
            this.bTamam.UseVisualStyleBackColor = true;
            this.bTamam.Click += new System.EventHandler(this.bTamam_Click);
            // 
            // fLisans
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 366);
            this.Controls.Add(this.bTamam);
            this.Controls.Add(this.lKontrolNo);
            this.Controls.Add(this.lStandart1);
            this.Controls.Add(this.tLisans);
            this.Name = "fLisans";
            this.Text = "LİSANS İŞLEMİ";
            this.Load += new System.EventHandler(this.fLisans_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private tNumeric tLisans;
        private lStandart lStandart1;
        private System.Windows.Forms.Button bTamam;
        internal lStandart lKontrolNo;
    }
}