namespace TicTacToe
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NouveauJeuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.QuitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aProposToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.A1 = new System.Windows.Forms.Button();
            this.A2 = new System.Windows.Forms.Button();
            this.A3 = new System.Windows.Forms.Button();
            this.B1 = new System.Windows.Forms.Button();
            this.B2 = new System.Windows.Forms.Button();
            this.B3 = new System.Windows.Forms.Button();
            this.C1 = new System.Windows.Forms.Button();
            this.C2 = new System.Windows.Forms.Button();
            this.C3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fichierToolStripMenuItem,
            this.aideToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(651, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fichierToolStripMenuItem
            // 
            this.fichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NouveauJeuToolStripMenuItem,
            this.QuitterToolStripMenuItem});
            this.fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
            this.fichierToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.fichierToolStripMenuItem.Text = "Fichier";
            // 
            // NouveauJeuToolStripMenuItem
            // 
            this.NouveauJeuToolStripMenuItem.Name = "NouveauJeuToolStripMenuItem";
            this.NouveauJeuToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.NouveauJeuToolStripMenuItem.Text = "Nouveau jeu";
            this.NouveauJeuToolStripMenuItem.Click += new System.EventHandler(this.NouveauJeuToolStripMenuItem_Click);
            // 
            // QuitterToolStripMenuItem
            // 
            this.QuitterToolStripMenuItem.Name = "QuitterToolStripMenuItem";
            this.QuitterToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.QuitterToolStripMenuItem.Text = "Quitter";
            this.QuitterToolStripMenuItem.Click += new System.EventHandler(this.QuitterToolStripMenuItem_Click);
            // 
            // aideToolStripMenuItem
            // 
            this.aideToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aProposToolStripMenuItem});
            this.aideToolStripMenuItem.Name = "aideToolStripMenuItem";
            this.aideToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.aideToolStripMenuItem.Text = "Aide";
            // 
            // aProposToolStripMenuItem
            // 
            this.aProposToolStripMenuItem.Name = "aProposToolStripMenuItem";
            this.aProposToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.aProposToolStripMenuItem.Text = "A propos";
            this.aProposToolStripMenuItem.Click += new System.EventHandler(this.aProposToolStripMenuItem_Click);
            // 
            // A1
            // 
            this.A1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.A1.Location = new System.Drawing.Point(12, 36);
            this.A1.Name = "A1";
            this.A1.Size = new System.Drawing.Size(64, 60);
            this.A1.TabIndex = 2;
            this.A1.Tag = "1";
            this.A1.UseVisualStyleBackColor = true;
            this.A1.Click += new System.EventHandler(this.btn_Click);
            //this.A1.MouseEnter += new System.EventHandler(this.button_enter);
            //this.A1.MouseLeave += new System.EventHandler(this.button_leave);
            // 
            // A2
            // 
            this.A2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.A2.Location = new System.Drawing.Point(80, 36);
            this.A2.Name = "A2";
            this.A2.Size = new System.Drawing.Size(64, 60);
            this.A2.TabIndex = 3;
            this.A2.Tag = "2";
            this.A2.UseVisualStyleBackColor = true;
            this.A2.Click += new System.EventHandler(this.btn_Click);
            //this.A2.MouseEnter += new System.EventHandler(this.button_enter);
            //this.A2.MouseLeave += new System.EventHandler(this.button_leave);
            // 
            // A3
            // 
            this.A3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.A3.Location = new System.Drawing.Point(152, 36);
            this.A3.Name = "A3";
            this.A3.Size = new System.Drawing.Size(64, 60);
            this.A3.TabIndex = 4;
            this.A3.Tag = "3";
            this.A3.UseVisualStyleBackColor = true;
            this.A3.Click += new System.EventHandler(this.btn_Click);
            //this.A3.MouseEnter += new System.EventHandler(this.button_enter);
            //this.A3.MouseLeave += new System.EventHandler(this.button_leave);
            // 
            // B1
            // 
            this.B1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.B1.Location = new System.Drawing.Point(12, 104);
            this.B1.Name = "B1";
            this.B1.Size = new System.Drawing.Size(64, 60);
            this.B1.TabIndex = 5;
            this.B1.Tag = "4";
            this.B1.UseVisualStyleBackColor = true;
            this.B1.Click += new System.EventHandler(this.btn_Click);
            //this.B1.MouseEnter += new System.EventHandler(this.button_enter);
            //this.B1.MouseLeave += new System.EventHandler(this.button_leave);
            // 
            // B2
            // 
            this.B2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.B2.Location = new System.Drawing.Point(80, 104);
            this.B2.Name = "B2";
            this.B2.Size = new System.Drawing.Size(64, 60);
            this.B2.TabIndex = 6;
            this.B2.Tag = "5";
            this.B2.UseVisualStyleBackColor = true;
            this.B2.Click += new System.EventHandler(this.btn_Click);
            //this.B2.MouseEnter += new System.EventHandler(this.button_enter);
            //this.B2.MouseLeave += new System.EventHandler(this.button_leave);
            // 
            // B3
            // 
            this.B3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.B3.Location = new System.Drawing.Point(152, 104);
            this.B3.Name = "B3";
            this.B3.Size = new System.Drawing.Size(64, 60);
            this.B3.TabIndex = 7;
            this.B3.Tag = "6";
            this.B3.UseVisualStyleBackColor = true;
            this.B3.Click += new System.EventHandler(this.btn_Click);
            //this.B3.MouseEnter += new System.EventHandler(this.button_enter);
            //this.B3.MouseLeave += new System.EventHandler(this.button_leave);
            // 
            // C1
            // 
            this.C1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C1.Location = new System.Drawing.Point(12, 172);
            this.C1.Name = "C1";
            this.C1.Size = new System.Drawing.Size(64, 60);
            this.C1.TabIndex = 8;
            this.C1.Tag = "7";
            this.C1.UseVisualStyleBackColor = true;
            this.C1.Click += new System.EventHandler(this.btn_Click);
            //this.C1.MouseEnter += new System.EventHandler(this.button_enter);
            //this.C1.MouseLeave += new System.EventHandler(this.button_leave);
            // 
            // C2
            // 
            this.C2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C2.Location = new System.Drawing.Point(80, 172);
            this.C2.Name = "C2";
            this.C2.Size = new System.Drawing.Size(64, 60);
            this.C2.TabIndex = 9;
            this.C2.Tag = "8";
            this.C2.UseVisualStyleBackColor = true;
            this.C2.Click += new System.EventHandler(this.btn_Click);
            //this.C2.MouseEnter += new System.EventHandler(this.button_enter);
            //this.C2.MouseLeave += new System.EventHandler(this.button_leave);
            // 
            // C3
            // 
            this.C3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C3.Location = new System.Drawing.Point(152, 172);
            this.C3.Name = "C3";
            this.C3.Size = new System.Drawing.Size(64, 60);
            this.C3.TabIndex = 10;
            this.C3.Tag = "9";
            this.C3.UseVisualStyleBackColor = true;
            this.C3.Click += new System.EventHandler(this.btn_Click);
            //this.C3.MouseEnter += new System.EventHandler(this.button_enter);
            //this.C3.MouseLeave += new System.EventHandler(this.button_leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(330, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(330, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "label1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(330, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 352);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.C3);
            this.Controls.Add(this.C2);
            this.Controls.Add(this.C1);
            this.Controls.Add(this.B3);
            this.Controls.Add(this.B2);
            this.Controls.Add(this.B1);
            this.Controls.Add(this.A3);
            this.Controls.Add(this.A2);
            this.Controls.Add(this.A1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TicTacToe";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fichierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NouveauJeuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem QuitterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aProposToolStripMenuItem;
        private System.Windows.Forms.Button A1;
        private System.Windows.Forms.Button A2;
        private System.Windows.Forms.Button A3;
        private System.Windows.Forms.Button B1;
        private System.Windows.Forms.Button B2;
        private System.Windows.Forms.Button B3;
        private System.Windows.Forms.Button C1;
        private System.Windows.Forms.Button C2;
        private System.Windows.Forms.Button C3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

