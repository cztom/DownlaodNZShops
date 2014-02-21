namespace DownloadNZShops {
    partial class TraceForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnTrace = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.cbBoxUrl = new System.Windows.Forms.ComboBox();
            this.cbBoxChapter = new System.Windows.Forms.ComboBox();
            this.cbBoxXPath = new System.Windows.Forms.ComboBox();
            this.cbBoxContentXPath = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "小说链接：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "当前章节：";
            // 
            // btnTrace
            // 
            this.btnTrace.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTrace.Location = new System.Drawing.Point(620, 12);
            this.btnTrace.Name = "btnTrace";
            this.btnTrace.Size = new System.Drawing.Size(170, 55);
            this.btnTrace.TabIndex = 2;
            this.btnTrace.Text = "开始追踪小说";
            this.btnTrace.UseVisualStyleBackColor = true;
            this.btnTrace.Click += new System.EventHandler(this.btnTrace_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("Arial", 14F);
            this.richTextBox1.Location = new System.Drawing.Point(14, 134);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(800, 516);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(14, 100);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 25);
            this.panel1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(16, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 14);
            this.label3.TabIndex = 5;
            // 
            // cbBoxUrl
            // 
            this.cbBoxUrl.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbBoxUrl.FormattingEnabled = true;
            this.cbBoxUrl.Location = new System.Drawing.Point(83, 14);
            this.cbBoxUrl.Name = "cbBoxUrl";
            this.cbBoxUrl.Size = new System.Drawing.Size(519, 20);
            this.cbBoxUrl.TabIndex = 6;
            this.cbBoxUrl.SelectedIndexChanged += new System.EventHandler(this.cbBoxUrl_SelectedIndexChanged);
            // 
            // cbBoxChapter
            // 
            this.cbBoxChapter.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbBoxChapter.FormattingEnabled = true;
            this.cbBoxChapter.Location = new System.Drawing.Point(83, 46);
            this.cbBoxChapter.Name = "cbBoxChapter";
            this.cbBoxChapter.Size = new System.Drawing.Size(121, 20);
            this.cbBoxChapter.TabIndex = 7;
            // 
            // cbBoxXPath
            // 
            this.cbBoxXPath.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbBoxXPath.FormattingEnabled = true;
            this.cbBoxXPath.Location = new System.Drawing.Point(215, 46);
            this.cbBoxXPath.Name = "cbBoxXPath";
            this.cbBoxXPath.Size = new System.Drawing.Size(190, 20);
            this.cbBoxXPath.TabIndex = 8;
            this.cbBoxXPath.SelectedIndexChanged += new System.EventHandler(this.cbBoxXPath_SelectedIndexChanged);
            // 
            // cbBoxContentXPath
            // 
            this.cbBoxContentXPath.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbBoxContentXPath.FormattingEnabled = true;
            this.cbBoxContentXPath.Location = new System.Drawing.Point(417, 46);
            this.cbBoxContentXPath.Name = "cbBoxContentXPath";
            this.cbBoxContentXPath.Size = new System.Drawing.Size(185, 20);
            this.cbBoxContentXPath.TabIndex = 8;
            this.cbBoxContentXPath.SelectedIndexChanged += new System.EventHandler(this.cbBoxContentXPath_SelectedIndexChanged);
            // 
            // TraceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 662);
            this.Controls.Add(this.cbBoxContentXPath);
            this.Controls.Add(this.cbBoxXPath);
            this.Controls.Add(this.cbBoxChapter);
            this.Controls.Add(this.cbBoxUrl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnTrace);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "TraceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "小说追踪";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TraceForm_FormClosed);
            this.Load += new System.EventHandler(this.TraceForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnTrace;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbBoxUrl;
        private System.Windows.Forms.ComboBox cbBoxChapter;
        private System.Windows.Forms.ComboBox cbBoxXPath;
        private System.Windows.Forms.ComboBox cbBoxContentXPath;
    }
}