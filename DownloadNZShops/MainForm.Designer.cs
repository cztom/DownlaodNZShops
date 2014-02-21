namespace DownloadNZShops
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnFetch = new System.Windows.Forms.Button();
            this.btnFatherCategory = new System.Windows.Forms.Button();
            this.btnSonCategory = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGetShopLink = new System.Windows.Forms.Button();
            this.proBarDispose = new System.Windows.Forms.ProgressBar();
            this.labProcessBar = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lnkLabProcessUrl = new System.Windows.Forms.LinkLabel();
            this.btnStop = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnNewForm = new System.Windows.Forms.Button();
            this.labElapsedTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFetch
            // 
            this.btnFetch.Location = new System.Drawing.Point(424, 57);
            this.btnFetch.Name = "btnFetch";
            this.btnFetch.Size = new System.Drawing.Size(91, 23);
            this.btnFetch.TabIndex = 1;
            this.btnFetch.Text = "开始分析抓取";
            this.btnFetch.UseVisualStyleBackColor = true;
            this.btnFetch.Click += new System.EventHandler(this.btnFetch_Click);
            // 
            // btnFatherCategory
            // 
            this.btnFatherCategory.Location = new System.Drawing.Point(7, 21);
            this.btnFatherCategory.Name = "btnFatherCategory";
            this.btnFatherCategory.Size = new System.Drawing.Size(75, 23);
            this.btnFatherCategory.TabIndex = 5;
            this.btnFatherCategory.Text = "添加大分类";
            this.btnFatherCategory.UseVisualStyleBackColor = true;
            this.btnFatherCategory.Click += new System.EventHandler(this.btnFatherCategory_Click);
            // 
            // btnSonCategory
            // 
            this.btnSonCategory.Location = new System.Drawing.Point(102, 21);
            this.btnSonCategory.Name = "btnSonCategory";
            this.btnSonCategory.Size = new System.Drawing.Size(75, 23);
            this.btnSonCategory.TabIndex = 5;
            this.btnSonCategory.Text = "添加小分类";
            this.btnSonCategory.UseVisualStyleBackColor = true;
            this.btnSonCategory.Click += new System.EventHandler(this.btnSonCategory_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(5, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "处理进度：";
            // 
            // btnGetShopLink
            // 
            this.btnGetShopLink.Location = new System.Drawing.Point(198, 21);
            this.btnGetShopLink.Name = "btnGetShopLink";
            this.btnGetShopLink.Size = new System.Drawing.Size(88, 23);
            this.btnGetShopLink.TabIndex = 8;
            this.btnGetShopLink.Text = "提取店铺链接";
            this.btnGetShopLink.UseVisualStyleBackColor = true;
            this.btnGetShopLink.Click += new System.EventHandler(this.btnGetShopLink_Click);
            // 
            // proBarDispose
            // 
            this.proBarDispose.Location = new System.Drawing.Point(77, 57);
            this.proBarDispose.Name = "proBarDispose";
            this.proBarDispose.Size = new System.Drawing.Size(341, 23);
            this.proBarDispose.TabIndex = 9;
            // 
            // labProcessBar
            // 
            this.labProcessBar.AutoSize = true;
            this.labProcessBar.Location = new System.Drawing.Point(76, 98);
            this.labProcessBar.Name = "labProcessBar";
            this.labProcessBar.Size = new System.Drawing.Size(23, 12);
            this.labProcessBar.TabIndex = 10;
            this.labProcessBar.Text = "0/0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(5, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "正在处理的链接：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(5, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "处理刻度：";
            // 
            // lnkLabProcessUrl
            // 
            this.lnkLabProcessUrl.AutoSize = true;
            this.lnkLabProcessUrl.Location = new System.Drawing.Point(116, 150);
            this.lnkLabProcessUrl.Name = "lnkLabProcessUrl";
            this.lnkLabProcessUrl.Size = new System.Drawing.Size(0, 12);
            this.lnkLabProcessUrl.TabIndex = 11;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(521, 57);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(67, 23);
            this.btnStop.TabIndex = 8;
            this.btnStop.Text = "停止抓取";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnNewForm);
            this.groupBox1.Controls.Add(this.proBarDispose);
            this.groupBox1.Controls.Add(this.lnkLabProcessUrl);
            this.groupBox1.Controls.Add(this.btnFetch);
            this.groupBox1.Controls.Add(this.labElapsedTime);
            this.groupBox1.Controls.Add(this.labProcessBar);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnGetShopLink);
            this.groupBox1.Controls.Add(this.btnFatherCategory);
            this.groupBox1.Controls.Add(this.btnSonCategory);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(601, 174);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "天维网商家信息";
            // 
            // btnNewForm
            // 
            this.btnNewForm.Location = new System.Drawing.Point(479, 20);
            this.btnNewForm.Name = "btnNewForm";
            this.btnNewForm.Size = new System.Drawing.Size(109, 23);
            this.btnNewForm.TabIndex = 12;
            this.btnNewForm.Text = "打开小说抓取窗口";
            this.btnNewForm.UseVisualStyleBackColor = true;
            this.btnNewForm.Click += new System.EventHandler(this.btnNewForm_Click);
            // 
            // labElapsedTime
            // 
            this.labElapsedTime.AutoSize = true;
            this.labElapsedTime.Location = new System.Drawing.Point(76, 125);
            this.labElapsedTime.Name = "labElapsedTime";
            this.labElapsedTime.Size = new System.Drawing.Size(0, 12);
            this.labElapsedTime.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(5, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "耗用时间：";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 202);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "抓取新西兰华人店铺信息";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFetch;
        private System.Windows.Forms.Button btnFatherCategory;
        private System.Windows.Forms.Button btnSonCategory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGetShopLink;
        private System.Windows.Forms.ProgressBar proBarDispose;
        private System.Windows.Forms.Label labProcessBar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel lnkLabProcessUrl;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labElapsedTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNewForm;
    }
}

