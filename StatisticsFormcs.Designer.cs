namespace proekt_formichki_klasove
{
    partial class StatisticsForm
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
            this.lblMostCommonShape = new System.Windows.Forms.Label();
            this.lblLargestShape = new System.Windows.Forms.Label();
            this.lblAverageArea = new System.Windows.Forms.Label();
            this.lblShapesByTypeRes = new System.Windows.Forms.Label();
            this.lblAverageAreaRes = new System.Windows.Forms.Label();
            this.lblLargestShapeRes = new System.Windows.Forms.Label();
            this.lblShapesByType = new System.Windows.Forms.Label();
            this.lblMCSRes = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMostCommonShape
            // 
            this.lblMostCommonShape.AutoSize = true;
            this.lblMostCommonShape.Location = new System.Drawing.Point(49, 65);
            this.lblMostCommonShape.Name = "lblMostCommonShape";
            this.lblMostCommonShape.Size = new System.Drawing.Size(139, 16);
            this.lblMostCommonShape.TabIndex = 0;
            this.lblMostCommonShape.Text = "Most Common Shape:";
            // 
            // lblLargestShape
            // 
            this.lblLargestShape.AutoSize = true;
            this.lblLargestShape.Location = new System.Drawing.Point(49, 118);
            this.lblLargestShape.Name = "lblLargestShape";
            this.lblLargestShape.Size = new System.Drawing.Size(98, 16);
            this.lblLargestShape.TabIndex = 1;
            this.lblLargestShape.Text = "Largest Shape:";
            // 
            // lblAverageArea
            // 
            this.lblAverageArea.AutoSize = true;
            this.lblAverageArea.Location = new System.Drawing.Point(49, 176);
            this.lblAverageArea.Name = "lblAverageArea";
            this.lblAverageArea.Size = new System.Drawing.Size(94, 16);
            this.lblAverageArea.TabIndex = 2;
            this.lblAverageArea.Text = "Average Area:";
            // 
            // lblShapesByTypeRes
            // 
            this.lblShapesByTypeRes.AutoSize = true;
            this.lblShapesByTypeRes.Location = new System.Drawing.Point(197, 232);
            this.lblShapesByTypeRes.Name = "lblShapesByTypeRes";
            this.lblShapesByTypeRes.Size = new System.Drawing.Size(44, 16);
            this.lblShapesByTypeRes.TabIndex = 3;
            this.lblShapesByTypeRes.Text = "label4";
            // 
            // lblAverageAreaRes
            // 
            this.lblAverageAreaRes.AutoSize = true;
            this.lblAverageAreaRes.Location = new System.Drawing.Point(197, 176);
            this.lblAverageAreaRes.Name = "lblAverageAreaRes";
            this.lblAverageAreaRes.Size = new System.Drawing.Size(44, 16);
            this.lblAverageAreaRes.TabIndex = 4;
            this.lblAverageAreaRes.Text = "label5";
            // 
            // lblLargestShapeRes
            // 
            this.lblLargestShapeRes.AutoSize = true;
            this.lblLargestShapeRes.Location = new System.Drawing.Point(197, 118);
            this.lblLargestShapeRes.Name = "lblLargestShapeRes";
            this.lblLargestShapeRes.Size = new System.Drawing.Size(44, 16);
            this.lblLargestShapeRes.TabIndex = 5;
            this.lblLargestShapeRes.Text = "label6";
            // 
            // lblShapesByType
            // 
            this.lblShapesByType.AutoSize = true;
            this.lblShapesByType.Location = new System.Drawing.Point(49, 232);
            this.lblShapesByType.Name = "lblShapesByType";
            this.lblShapesByType.Size = new System.Drawing.Size(111, 16);
            this.lblShapesByType.TabIndex = 6;
            this.lblShapesByType.Text = "Shapes By Type:";
            // 
            // lblMCSRes
            // 
            this.lblMCSRes.AutoSize = true;
            this.lblMCSRes.Location = new System.Drawing.Point(197, 65);
            this.lblMCSRes.Name = "lblMCSRes";
            this.lblMCSRes.Size = new System.Drawing.Size(44, 16);
            this.lblMCSRes.TabIndex = 7;
            this.lblMCSRes.Text = "label8";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(200, 288);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // StatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 336);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblMCSRes);
            this.Controls.Add(this.lblShapesByType);
            this.Controls.Add(this.lblLargestShapeRes);
            this.Controls.Add(this.lblAverageAreaRes);
            this.Controls.Add(this.lblShapesByTypeRes);
            this.Controls.Add(this.lblAverageArea);
            this.Controls.Add(this.lblLargestShape);
            this.Controls.Add(this.lblMostCommonShape);
            this.Name = "StatisticsForm";
            this.Text = "StatisticsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMostCommonShape;
        private System.Windows.Forms.Label lblLargestShape;
        private System.Windows.Forms.Label lblAverageArea;
        private System.Windows.Forms.Label lblShapesByTypeRes;
        private System.Windows.Forms.Label lblAverageAreaRes;
        private System.Windows.Forms.Label lblLargestShapeRes;
        private System.Windows.Forms.Label lblShapesByType;
        private System.Windows.Forms.Label lblMCSRes;
        private System.Windows.Forms.Button btnClose;
    }
}