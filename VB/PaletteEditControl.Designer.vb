Imports Microsoft.VisualBasic
Imports System
Namespace ChartAppearanceSample
	Partial Public Class PaletteEditControl
		''' <summary> 
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary> 
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Component Designer generated code"

		''' <summary> 
		''' Required method for Designer support - do not modify 
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.components = New System.ComponentModel.Container()
			Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(PaletteEditControl))
			Me.paletteImages = New DevExpress.Utils.ImageCollection(Me.components)
			Me.btnEdit = New DevExpress.XtraEditors.SimpleButton()
			Me.lbPalettes = New DevExpress.XtraEditors.ImageListBoxControl()
			CType(Me.paletteImages, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.lbPalettes, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' paletteImages
			' 
			Me.paletteImages.ImageStream = (CType(resources.GetObject("paletteImages.ImageStream"), DevExpress.Utils.ImageCollectionStreamer))
			' 
			' btnEdit
			' 
			Me.btnEdit.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.btnEdit.Location = New System.Drawing.Point(2, 267)
			Me.btnEdit.Name = "btnEdit"
			Me.btnEdit.Size = New System.Drawing.Size(253, 22)
			Me.btnEdit.TabIndex = 3
			Me.btnEdit.Text = "Edit palettes..."
'			Me.btnEdit.Click += New System.EventHandler(Me.btnEdit_Click);
			' 
			' lbPalettes
			' 
			Me.lbPalettes.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.lbPalettes.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far
			Me.lbPalettes.ImageList = Me.paletteImages
			Me.lbPalettes.Location = New System.Drawing.Point(2, 4)
			Me.lbPalettes.Name = "lbPalettes"
			Me.lbPalettes.Size = New System.Drawing.Size(253, 257)
			Me.lbPalettes.TabIndex = 2
'			Me.lbPalettes.MouseDoubleClick += New System.Windows.Forms.MouseEventHandler(Me.lbPalettes_MouseDoubleClick);
'			Me.lbPalettes.SelectedIndexChanged += New System.EventHandler(Me.lbPalettes_SelectedIndexChanged);
'			Me.lbPalettes.KeyDown += New System.Windows.Forms.KeyEventHandler(Me.lbPalettes_KeyDown);
			' 
			' PaletteEditControl
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.Controls.Add(Me.btnEdit)
			Me.Controls.Add(Me.lbPalettes)
			Me.Name = "PaletteEditControl"
			Me.Size = New System.Drawing.Size(258, 293)
			CType(Me.paletteImages, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.lbPalettes, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private paletteImages As DevExpress.Utils.ImageCollection
		Private WithEvents btnEdit As DevExpress.XtraEditors.SimpleButton
		Private WithEvents lbPalettes As DevExpress.XtraEditors.ImageListBoxControl
	End Class
End Namespace
