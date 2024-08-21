using System;

namespace Windowsform33
{
    partial class Form1
    {

        private System.ComponentModel.IContainer components = null;


        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod


        private void InitializeComponent()
        {
            this.button_Browse = new System.Windows.Forms.Button();
            this.button_Discover = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.result = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // 
            this.button_Browse.Location = new System.Drawing.Point(14, 49);
            this.button_Browse.Margin = new System.Windows.Forms.Padding(7);
            this.button_Browse.Name = "button_Browse";
            this.button_Browse.Size = new System.Drawing.Size(157, 45);
            this.button_Browse.TabIndex = 0;
            this.button_Browse.Text = "Browse";
            this.button_Browse.UseVisualStyleBackColor = true;
            this.button_Browse.Click += new System.EventHandler(this.button1_Click);
            // 
            // 
            this.button_Discover.Location = new System.Drawing.Point(14, 50);
            this.button_Discover.Margin = new System.Windows.Forms.Padding(7);
            this.button_Discover.Name = "button_Discover";
            this.button_Discover.Size = new System.Drawing.Size(175, 51);
            this.button_Discover.TabIndex = 1;
            this.button_Discover.Text = "View Results";
            this.button_Discover.UseVisualStyleBackColor = true;
            this.button_Discover.Click += new System.EventHandler(this.button_Discover_Click);
            // 
            // 
            this.textBox2.Location = new System.Drawing.Point(278, 49);
            this.textBox2.Margin = new System.Windows.Forms.Padding(7);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(686, 35);
            this.textBox2.TabIndex = 3;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // 
            this.result.Location = new System.Drawing.Point(14, 42);
            this.result.Margin = new System.Windows.Forms.Padding(7);
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(541, 67);
            this.result.TabIndex = 13;
            this.result.Text = "Press to Find The Most Succesfull Algorithm";
            this.result.UseVisualStyleBackColor = true;
            this.result.Click += new System.EventHandler(this.result_Click);
            // 
            // 
            this.groupBox1.Controls.Add(this.button_Browse);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Location = new System.Drawing.Point(16, 16);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(7);
            this.groupBox1.Size = new System.Drawing.Size(1082, 127);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Step 1. Open The File";
            this.groupBox1.BackColor = System.Drawing.Color.LightBlue;
            // 
            // 
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.result);
            this.groupBox2.Location = new System.Drawing.Point(16, 185);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(7);
            this.groupBox2.Size = new System.Drawing.Size(1082, 181);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Step 2. Search For The Most Successful Algorithm";
            this.groupBox2.BackColor = System.Drawing.Color.LightCyan;
            // 
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 132);
            this.textBox1.Margin = new System.Windows.Forms.Padding(7);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(952, 35);
            this.textBox1.TabIndex = 16;
            // 
            // 
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Location = new System.Drawing.Point(28, 402);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(7);
            this.groupBox3.Size = new System.Drawing.Size(1070, 686);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Step 3. Add an Instance";
            this.groupBox3.BackColor = System.Drawing.Color.LightBlue;
            // 
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(2, 54);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(7);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 92;
            this.dataGridView1.Size = new System.Drawing.Size(950, 618);
            this.dataGridView1.TabIndex = 0;
            // 
            // 
            this.groupBox4.Controls.Add(this.textBox3);
            this.groupBox4.Controls.Add(this.button_Discover);
            this.groupBox4.Location = new System.Drawing.Point(28, 1154);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(7);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(7);
            this.groupBox4.Size = new System.Drawing.Size(1070, 125);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Step 4. Show Result of Instance Given";
            this.groupBox4.BackColor = System.Drawing.Color.LightBlue;
            // 
            // 
            this.textBox3.Location = new System.Drawing.Point(203, 50);
            this.textBox3.Margin = new System.Windows.Forms.Padding(7);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(749, 35);
            this.textBox3.TabIndex = 2;
            // 
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 1380);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(7);
            this.Name = "Form1";
            this.Text = "DSS HOMEWORK Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.Button button_Browse;
        private System.Windows.Forms.Button button_Discover;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button result;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox3;
    }
}

