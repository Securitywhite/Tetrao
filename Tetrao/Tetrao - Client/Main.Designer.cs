namespace Tetrao___Client
{
    partial class Main
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
            this.mainBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // mainBrowser
            // 
            this.mainBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainBrowser.Location = new System.Drawing.Point(0, 0);
            this.mainBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.mainBrowser.Name = "mainBrowser";
            this.mainBrowser.ScriptErrorsSuppressed = true;
            this.mainBrowser.Size = new System.Drawing.Size(569, 409);
            this.mainBrowser.TabIndex = 0;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 409);
            this.Controls.Add(this.mainBrowser);
            this.Name = "Main";
            this.Text = "Tetrao ~ Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser mainBrowser;
    }
}

