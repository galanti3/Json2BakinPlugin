namespace Json2BakinPlugin
{
    partial class TestForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestForm));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.jsonSelector = new System.Windows.Forms.Button();
			this.jsonFolderComboBox = new System.Windows.Forms.ComboBox();
			this.bakinFolderComboBox = new System.Windows.Forms.ComboBox();
			this.bakinSelector = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.transformButton = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			resources.ApplyResources(this.pictureBox1, "pictureBox1");
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// jsonSelector
			// 
			resources.ApplyResources(this.jsonSelector, "jsonSelector");
			this.jsonSelector.Name = "jsonSelector";
			this.jsonSelector.UseVisualStyleBackColor = true;
			this.jsonSelector.Click += new System.EventHandler(this.jsonSelector_Click);
			// 
			// jsonFolderComboBox
			// 
			resources.ApplyResources(this.jsonFolderComboBox, "jsonFolderComboBox");
			this.jsonFolderComboBox.FormattingEnabled = true;
			this.jsonFolderComboBox.Name = "jsonFolderComboBox";
			this.jsonFolderComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// bakinFolderComboBox
			// 
			resources.ApplyResources(this.bakinFolderComboBox, "bakinFolderComboBox");
			this.bakinFolderComboBox.FormattingEnabled = true;
			this.bakinFolderComboBox.Name = "bakinFolderComboBox";
			// 
			// bakinSelector
			// 
			resources.ApplyResources(this.bakinSelector, "bakinSelector");
			this.bakinSelector.Name = "bakinSelector";
			this.bakinSelector.UseVisualStyleBackColor = true;
			this.bakinSelector.Click += new System.EventHandler(this.bakinSelector_Click);
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			this.label2.Click += new System.EventHandler(this.jsonSelector_Click);
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			// 
			// transformButton
			// 
			resources.ApplyResources(this.transformButton, "transformButton");
			this.transformButton.Name = "transformButton";
			this.transformButton.UseVisualStyleBackColor = true;
			this.transformButton.Click += new System.EventHandler(this.transformButton_Click);
			// 
			// label5
			// 
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			// 
			// TestForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label5);
			this.Controls.Add(this.transformButton);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.bakinSelector);
			this.Controls.Add(this.bakinFolderComboBox);
			this.Controls.Add(this.jsonFolderComboBox);
			this.Controls.Add(this.jsonSelector);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.Name = "TestForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestForm_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button jsonSelector;
        private System.Windows.Forms.ComboBox jsonFolderComboBox;
        private System.Windows.Forms.ComboBox bakinFolderComboBox;
        private System.Windows.Forms.Button bakinSelector;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button transformButton;
        private System.Windows.Forms.Label label5;
    }
}