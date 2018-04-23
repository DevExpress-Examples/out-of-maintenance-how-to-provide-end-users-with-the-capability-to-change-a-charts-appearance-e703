namespace ChartAppearanceSample {
    partial class PaletteEditControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaletteEditControl));
            this.paletteImages = new DevExpress.Utils.ImageCollection(this.components);
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.lbPalettes = new DevExpress.XtraEditors.ImageListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.paletteImages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbPalettes)).BeginInit();
            this.SuspendLayout();
            // 
            // paletteImages
            // 
            this.paletteImages.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("paletteImages.ImageStream")));
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(2, 267);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(253, 22);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Edit palettes...";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // lbPalettes
            // 
            this.lbPalettes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPalettes.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lbPalettes.ImageList = this.paletteImages;
            this.lbPalettes.Location = new System.Drawing.Point(2, 4);
            this.lbPalettes.Name = "lbPalettes";
            this.lbPalettes.Size = new System.Drawing.Size(253, 257);
            this.lbPalettes.TabIndex = 2;
            this.lbPalettes.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbPalettes_MouseDoubleClick);
            this.lbPalettes.SelectedIndexChanged += new System.EventHandler(this.lbPalettes_SelectedIndexChanged);
            this.lbPalettes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbPalettes_KeyDown);
            // 
            // PaletteEditControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.lbPalettes);
            this.Name = "PaletteEditControl";
            this.Size = new System.Drawing.Size(258, 293);
            ((System.ComponentModel.ISupportInitialize)(this.paletteImages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbPalettes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.ImageCollection paletteImages;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.ImageListBoxControl lbPalettes;
    }
}
