﻿namespace eZustellSendPOC.About.Views
{
    partial class FrmAboutView
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.mCBxOpenSourceList = new WinFormsMvvm.MultiColumnComboBox();
            this.aboutViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.aboutViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(473, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "eZustellung Proof-Of-Concept - Windows Desktop";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(222, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Entwickelt von Bogad & Partner Consulting OG";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(263, 41);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(112, 13);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://www.bogad.at";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "im Auftrag von AustriaPro";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(263, 58);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(123, 13);
            this.linkLabel2.TabIndex = 4;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "http://www.austriapro.at";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(238, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "auf Basis der Spezifikationen des AK eZustellung";
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(263, 75);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(130, 13);
            this.linkLabel3.TabIndex = 6;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "http://www.ezustellung.at";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(290, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Die Software wird von AustriaPro as-is bereitgestellt. Lizenz: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(280, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Folgende Open Source Komponenten werden verwendet:";
            // 
            // mCBxOpenSourceList
            // 
            this.mCBxOpenSourceList.DataSource = this.aboutViewModelBindingSource;
            this.mCBxOpenSourceList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.mCBxOpenSourceList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mCBxOpenSourceList.FormattingEnabled = true;
            this.mCBxOpenSourceList.Location = new System.Drawing.Point(12, 160);
            this.mCBxOpenSourceList.Name = "mCBxOpenSourceList";
            this.mCBxOpenSourceList.Size = new System.Drawing.Size(504, 21);
            this.mCBxOpenSourceList.TabIndex = 9;
            // 
            // aboutViewModelBindingSource
            // 
            this.aboutViewModelBindingSource.DataSource = typeof(eZustellSendPOC.About.ViewModels.AboutViewModel);
            // 
            // FrmAboutView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 204);
            this.Controls.Add(this.mCBxOpenSourceList);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.linkLabel3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FrmAboutView";
            this.Text = "Über eZustellPOC";
            ((System.ComponentModel.ISupportInitialize)(this.aboutViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private WinFormsMvvm.MultiColumnComboBox mCBxOpenSourceList;
        private System.Windows.Forms.BindingSource aboutViewModelBindingSource;
    }
}