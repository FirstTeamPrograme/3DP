﻿namespace Interface_WindowsFormsApp_
{
    partial class Zn
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Zn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Zn";
            this.Text = "Form1_Zn";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Zn_Paint);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Zn_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Zn_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Zn_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Zn_MouseUp);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(Form1_MouseWheel);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
