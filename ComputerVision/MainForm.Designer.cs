namespace ComputerVision
{
    partial class MainForm
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
            this.panelSource = new System.Windows.Forms.Panel();
            this.panelDestination = new System.Windows.Forms.Panel();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBoxReflexie = new System.Windows.Forms.ComboBox();
            this.buttonReflexie = new System.Windows.Forms.Button();
            this.buttonHistograma = new System.Windows.Forms.Button();
            this.trackContrast = new System.Windows.Forms.TrackBar();
            this.trackBrightness = new System.Windows.Forms.TrackBar();
            this.button_Negative = new System.Windows.Forms.Button();
            this.buttonGrayscale = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.buttonFTJ = new System.Windows.Forms.Button();
            this.nBox = new System.Windows.Forms.TextBox();
            this.buttonSort = new System.Windows.Forms.Button();
            this.btnMarkov = new System.Windows.Forms.Button();
            this.btnFTS = new System.Windows.Forms.Button();
            this.btnUnsharp = new System.Windows.Forms.Button();
            this.btnKirsch = new System.Windows.Forms.Button();
            this.btnGabor = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackContrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBrightness)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSource
            // 
            this.panelSource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelSource.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelSource.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panelSource.Location = new System.Drawing.Point(12, 12);
            this.panelSource.Name = "panelSource";
            this.panelSource.Size = new System.Drawing.Size(320, 240);
            this.panelSource.TabIndex = 0;
            // 
            // panelDestination
            // 
            this.panelDestination.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelDestination.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelDestination.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panelDestination.Location = new System.Drawing.Point(348, 12);
            this.panelDestination.Name = "panelDestination";
            this.panelDestination.Size = new System.Drawing.Size(320, 240);
            this.panelDestination.TabIndex = 1;
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(12, 439);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(75, 23);
            this.buttonLoad.TabIndex = 2;
            this.buttonLoad.Text = "Load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.comboBoxReflexie);
            this.panel1.Controls.Add(this.buttonReflexie);
            this.panel1.Controls.Add(this.buttonHistograma);
            this.panel1.Controls.Add(this.trackContrast);
            this.panel1.Controls.Add(this.trackBrightness);
            this.panel1.Controls.Add(this.button_Negative);
            this.panel1.Controls.Add(this.buttonGrayscale);
            this.panel1.Location = new System.Drawing.Point(348, 271);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 190);
            this.panel1.TabIndex = 3;
            // 
            // comboBoxReflexie
            // 
            this.comboBoxReflexie.FormattingEnabled = true;
            this.comboBoxReflexie.Items.AddRange(new object[] {
            "Y",
            "X",
            "A"});
            this.comboBoxReflexie.Location = new System.Drawing.Point(88, 48);
            this.comboBoxReflexie.Name = "comboBoxReflexie";
            this.comboBoxReflexie.Size = new System.Drawing.Size(227, 21);
            this.comboBoxReflexie.TabIndex = 19;
            this.comboBoxReflexie.Text = "Reflexie";
            this.comboBoxReflexie.SelectedIndexChanged += new System.EventHandler(this.comboBoxReflexie_SelectedIndexChanged);
            // 
            // buttonReflexie
            // 
            this.buttonReflexie.Location = new System.Drawing.Point(7, 46);
            this.buttonReflexie.Name = "buttonReflexie";
            this.buttonReflexie.Size = new System.Drawing.Size(75, 23);
            this.buttonReflexie.TabIndex = 18;
            this.buttonReflexie.Text = "Reflexie";
            this.buttonReflexie.UseVisualStyleBackColor = true;
            this.buttonReflexie.Click += new System.EventHandler(this.buttonReflexie_Click);
            // 
            // buttonHistograma
            // 
            this.buttonHistograma.Location = new System.Drawing.Point(7, 97);
            this.buttonHistograma.Name = "buttonHistograma";
            this.buttonHistograma.Size = new System.Drawing.Size(75, 23);
            this.buttonHistograma.TabIndex = 17;
            this.buttonHistograma.Text = "Histograma";
            this.buttonHistograma.UseVisualStyleBackColor = true;
            this.buttonHistograma.Click += new System.EventHandler(this.buttonHistograma_Click);
            // 
            // trackContrast
            // 
            this.trackContrast.Location = new System.Drawing.Point(88, 75);
            this.trackContrast.Maximum = 180;
            this.trackContrast.Minimum = -100;
            this.trackContrast.Name = "trackContrast";
            this.trackContrast.Size = new System.Drawing.Size(227, 45);
            this.trackContrast.TabIndex = 16;
            this.trackContrast.ValueChanged += new System.EventHandler(this.trackContrast_ValueChanged);
            // 
            // trackBrightness
            // 
            this.trackBrightness.Location = new System.Drawing.Point(88, 126);
            this.trackBrightness.Maximum = 255;
            this.trackBrightness.Minimum = -255;
            this.trackBrightness.Name = "trackBrightness";
            this.trackBrightness.Size = new System.Drawing.Size(227, 45);
            this.trackBrightness.TabIndex = 15;
            this.trackBrightness.ValueChanged += new System.EventHandler(this.trackBrightness_ValueChanged);
            // 
            // button_Negative
            // 
            this.button_Negative.Location = new System.Drawing.Point(7, 126);
            this.button_Negative.Name = "button_Negative";
            this.button_Negative.Size = new System.Drawing.Size(75, 23);
            this.button_Negative.TabIndex = 14;
            this.button_Negative.Text = "Negative";
            this.button_Negative.UseVisualStyleBackColor = true;
            this.button_Negative.Click += new System.EventHandler(this.button_Negative_Click);
            // 
            // buttonGrayscale
            // 
            this.buttonGrayscale.Location = new System.Drawing.Point(7, 155);
            this.buttonGrayscale.Name = "buttonGrayscale";
            this.buttonGrayscale.Size = new System.Drawing.Size(75, 23);
            this.buttonGrayscale.TabIndex = 13;
            this.buttonGrayscale.Text = "Grayscale";
            this.buttonGrayscale.UseVisualStyleBackColor = true;
            this.buttonGrayscale.Click += new System.EventHandler(this.buttonGrayscale_Click);
            // 
            // buttonFTJ
            // 
            this.buttonFTJ.Location = new System.Drawing.Point(267, 413);
            this.buttonFTJ.Name = "buttonFTJ";
            this.buttonFTJ.Size = new System.Drawing.Size(75, 23);
            this.buttonFTJ.TabIndex = 20;
            this.buttonFTJ.Text = "FTJ";
            this.buttonFTJ.UseVisualStyleBackColor = true;
            this.buttonFTJ.Click += new System.EventHandler(this.buttonFTJ_Click);
            // 
            // nBox
            // 
            this.nBox.Location = new System.Drawing.Point(242, 442);
            this.nBox.Name = "nBox";
            this.nBox.Size = new System.Drawing.Size(100, 20);
            this.nBox.TabIndex = 21;
            this.nBox.Text = "n = ?";
            // 
            // buttonSort
            // 
            this.buttonSort.Location = new System.Drawing.Point(12, 410);
            this.buttonSort.Name = "buttonSort";
            this.buttonSort.Size = new System.Drawing.Size(75, 23);
            this.buttonSort.TabIndex = 22;
            this.buttonSort.Text = "Sortare";
            this.buttonSort.UseVisualStyleBackColor = true;
            this.buttonSort.Click += new System.EventHandler(this.buttonSort_Click);
            // 
            // btnMarkov
            // 
            this.btnMarkov.Location = new System.Drawing.Point(93, 410);
            this.btnMarkov.Name = "btnMarkov";
            this.btnMarkov.Size = new System.Drawing.Size(75, 23);
            this.btnMarkov.TabIndex = 23;
            this.btnMarkov.Text = "Markov";
            this.btnMarkov.UseVisualStyleBackColor = true;
            this.btnMarkov.Click += new System.EventHandler(this.btnMarkov_Click);
            // 
            // btnFTS
            // 
            this.btnFTS.Location = new System.Drawing.Point(93, 439);
            this.btnFTS.Name = "btnFTS";
            this.btnFTS.Size = new System.Drawing.Size(75, 23);
            this.btnFTS.TabIndex = 24;
            this.btnFTS.Text = "FTS";
            this.btnFTS.UseVisualStyleBackColor = true;
            this.btnFTS.Click += new System.EventHandler(this.btnFTS_Click);
            // 
            // btnUnsharp
            // 
            this.btnUnsharp.Location = new System.Drawing.Point(12, 381);
            this.btnUnsharp.Name = "btnUnsharp";
            this.btnUnsharp.Size = new System.Drawing.Size(75, 23);
            this.btnUnsharp.TabIndex = 26;
            this.btnUnsharp.Text = "Unsharp";
            this.btnUnsharp.UseVisualStyleBackColor = true;
            this.btnUnsharp.Click += new System.EventHandler(this.btnUnsharp_Click);
            // 
            // btnKirsch
            // 
            this.btnKirsch.Location = new System.Drawing.Point(93, 381);
            this.btnKirsch.Name = "btnKirsch";
            this.btnKirsch.Size = new System.Drawing.Size(75, 23);
            this.btnKirsch.TabIndex = 27;
            this.btnKirsch.Text = "Kirsch";
            this.btnKirsch.UseVisualStyleBackColor = true;
            this.btnKirsch.Click += new System.EventHandler(this.btnKirsch_Click);
            // 
            // btnGabor
            // 
            this.btnGabor.Location = new System.Drawing.Point(12, 352);
            this.btnGabor.Name = "btnGabor";
            this.btnGabor.Size = new System.Drawing.Size(75, 23);
            this.btnGabor.TabIndex = 28;
            this.btnGabor.Text = "Gabor";
            this.btnGabor.UseVisualStyleBackColor = true;
            this.btnGabor.Click += new System.EventHandler(this.btnGabor_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 473);
            this.Controls.Add(this.btnGabor);
            this.Controls.Add(this.btnKirsch);
            this.Controls.Add(this.btnUnsharp);
            this.Controls.Add(this.btnFTS);
            this.Controls.Add(this.btnMarkov);
            this.Controls.Add(this.buttonSort);
            this.Controls.Add(this.nBox);
            this.Controls.Add(this.buttonFTJ);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.panelDestination);
            this.Controls.Add(this.panelSource);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackContrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBrightness)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelSource;
        private System.Windows.Forms.Panel panelDestination;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonGrayscale;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button button_Negative;
        private System.Windows.Forms.TrackBar trackBrightness;
        private System.Windows.Forms.TrackBar trackContrast;
        private System.Windows.Forms.Button buttonHistograma;
        private System.Windows.Forms.ComboBox comboBoxReflexie;
        private System.Windows.Forms.Button buttonReflexie;
        private System.Windows.Forms.Button buttonFTJ;
        private System.Windows.Forms.TextBox nBox;
        private System.Windows.Forms.Button buttonSort;
        private System.Windows.Forms.Button btnMarkov;
        private System.Windows.Forms.Button btnFTS;
        private System.Windows.Forms.Button btnUnsharp;
        private System.Windows.Forms.Button btnKirsch;
        private System.Windows.Forms.Button btnGabor;
    }
}

