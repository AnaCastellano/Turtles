namespace TurtlesGame
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnGameSettingsFile = new System.Windows.Forms.Button();
            this.btnMovesFile = new System.Windows.Forms.Button();
            this.lblGameSettingsFile = new System.Windows.Forms.Label();
            this.lblMoveFile = new System.Windows.Forms.Label();
            this.btnPlay = new System.Windows.Forms.Button();
            this.pnlGame = new System.Windows.Forms.Panel();
            this.lbResult = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnGameSettingsFile
            // 
            this.btnGameSettingsFile.Location = new System.Drawing.Point(12, 12);
            this.btnGameSettingsFile.Name = "btnGameSettingsFile";
            this.btnGameSettingsFile.Size = new System.Drawing.Size(220, 23);
            this.btnGameSettingsFile.TabIndex = 0;
            this.btnGameSettingsFile.Text = "Add Game Settings file";
            this.btnGameSettingsFile.UseVisualStyleBackColor = true;
            this.btnGameSettingsFile.Click += new System.EventHandler(this.BtnGameSettingsFile_Click);
            // 
            // btnMovesFile
            // 
            this.btnMovesFile.Location = new System.Drawing.Point(12, 41);
            this.btnMovesFile.Name = "btnMovesFile";
            this.btnMovesFile.Size = new System.Drawing.Size(220, 23);
            this.btnMovesFile.TabIndex = 1;
            this.btnMovesFile.Text = "Add Moves file";
            this.btnMovesFile.UseVisualStyleBackColor = true;
            this.btnMovesFile.Click += new System.EventHandler(this.BtnMovesFile_Click);
            // 
            // lblGameSettingsFile
            // 
            this.lblGameSettingsFile.AutoSize = true;
            this.lblGameSettingsFile.Location = new System.Drawing.Point(253, 17);
            this.lblGameSettingsFile.Name = "lblGameSettingsFile";
            this.lblGameSettingsFile.Size = new System.Drawing.Size(0, 13);
            this.lblGameSettingsFile.TabIndex = 3;
            // 
            // lblMoveFile
            // 
            this.lblMoveFile.AutoSize = true;
            this.lblMoveFile.Location = new System.Drawing.Point(253, 58);
            this.lblMoveFile.Name = "lblMoveFile";
            this.lblMoveFile.Size = new System.Drawing.Size(0, 13);
            this.lblMoveFile.TabIndex = 4;
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(12, 70);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(220, 23);
            this.btnPlay.TabIndex = 5;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.BtnPlay_Click);
            // 
            // pnlGame
            // 
            this.pnlGame.Location = new System.Drawing.Point(12, 99);
            this.pnlGame.Name = "pnlGame";
            this.pnlGame.Size = new System.Drawing.Size(531, 457);
            this.pnlGame.TabIndex = 6;
            // 
            // lbResult
            // 
            this.lbResult.FormattingEnabled = true;
            this.lbResult.Location = new System.Drawing.Point(562, 102);
            this.lbResult.Name = "lbResult";
            this.lbResult.Size = new System.Drawing.Size(400, 446);
            this.lbResult.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 568);
            this.Controls.Add(this.lbResult);
            this.Controls.Add(this.pnlGame);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.lblMoveFile);
            this.Controls.Add(this.lblGameSettingsFile);
            this.Controls.Add(this.btnMovesFile);
            this.Controls.Add(this.btnGameSettingsFile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Turtles Game";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnGameSettingsFile;
        private System.Windows.Forms.Button btnMovesFile;
        private System.Windows.Forms.Label lblGameSettingsFile;
        private System.Windows.Forms.Label lblMoveFile;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Panel pnlGame;
        private System.Windows.Forms.ListBox lbResult;
    }
}

