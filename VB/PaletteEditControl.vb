Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Windows.Forms.Design
Imports DevExpress.Utils
Imports DevExpress.LookAndFeel
Imports DevExpress.XtraEditors
Imports DevExpress.XtraCharts.Native
Imports DevExpress.XtraCharts
Imports DevExpress.XtraCharts.Design

Namespace ChartAppearanceSample
	Partial Public Class PaletteEditControl
		Inherits XtraUserControl
		Public Shared Function CreateEditorImage(ByVal palette As Palette) As Image
			Const imageSize As Integer = 10
			Dim image As Bitmap = Nothing
			Try
				image = New Bitmap(palette.Count * (imageSize + 1) - 1, imageSize)
				Using g As Graphics = Graphics.FromImage(image)
					Dim rect As New Rectangle(Point.Empty, New Size(imageSize, imageSize))
					Dim i As Integer = 0
					Do While i < palette.Count
						Using brush As Brush = New SolidBrush(palette(i).Color)
							g.FillRectangle(brush, rect)
						End Using
						Dim penRect As Rectangle = rect
						penRect.Width -= 1
						penRect.Height -= 1
						Using pen As New Pen(Color.Gray)
							g.DrawRectangle(pen, penRect)
						End Using
						i += 1
						rect.X += 11
					Loop
				End Using
			Catch
				If image IsNot Nothing Then
					image.Dispose()
					image = Nothing
				End If
			End Try
			Return image
		End Function

		Private chart_Renamed As Chart
		Private paletteRepository_Renamed As PaletteRepository
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Property Chart() As Chart
			Get
				Return chart_Renamed
			End Get
			Set(ByVal value As Chart)
				chart_Renamed = value
				PaletteRepository = chart_Renamed.PaletteRepository
				SelectedPalette = chart_Renamed.Palette
			End Set
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Property PaletteRepository() As PaletteRepository
			Get
				Return paletteRepository_Renamed
			End Get
			Set(ByVal value As PaletteRepository)
				paletteRepository_Renamed = value
				FillPalettes()
			End Set
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Property SelectedPalette() As Palette
			Get
				Return TryCast(lbPalettes.SelectedValue, Palette)
			End Get
			Set(ByVal value As Palette)
				If value Is Nothing Then
					If lbPalettes.ItemCount > 0 Then
						lbPalettes.SelectedIndex = 0
					End If
				Else
					lbPalettes.SelectedValue = value
				End If
			End Set
		End Property
		Public Event OnPaletteChanged As EventHandler
		Public Event OnNeedClose As EventHandler
		Public Sub New()
			InitializeComponent()
		End Sub
		Public Sub SetLookAndFeel(ByVal lookAndFeel As UserLookAndFeel)
			Me.LookAndFeel.ParentLookAndFeel = lookAndFeel
			lbPalettes.LookAndFeel.ParentLookAndFeel = lookAndFeel
			btnEdit.LookAndFeel.ParentLookAndFeel = lookAndFeel
		End Sub
		Private Sub FillPalettes()
			Dim names() As String = paletteRepository_Renamed.PaletteNames
			Dim images(names.Length - 1) As Image
			Dim imageSize As Size = Size.Empty
			lbPalettes.Items.BeginUpdate()
			lbPalettes.Items.Clear()
			For i As Integer = 0 To names.Length - 1
				Dim name As String = names(i)
				Dim palette As Palette = paletteRepository_Renamed(name)
				Dim image As Image = CreateEditorImage(palette)
				images(i) = image
				If image.Width > imageSize.Width Then
					imageSize.Width = image.Width
				End If
				If image.Height > imageSize.Height Then
					imageSize.Height = image.Height
				End If
				lbPalettes.Items.Add(palette, i)
			Next i
			lbPalettes.Items.EndUpdate()
			paletteImages.BeginInit()
			paletteImages.Clear()
			paletteImages.ImageSize = imageSize
			For i As Integer = 0 To images.Length - 1
				Dim image As Image = images(i)
				Dim newImage As Bitmap = Nothing
				If image.Size <> imageSize Then
					Try
						newImage = New Bitmap(imageSize.Width, imageSize.Height)
						Using gr As Graphics = Graphics.FromImage(newImage)
							gr.DrawImage(image, Point.Empty)
						End Using
						image.Dispose()
					Catch
						If newImage IsNot Nothing Then
							newImage.Dispose()
							newImage = Nothing
						End If
					End Try
				End If
				If newImage Is Nothing Then
					paletteImages.AddImage(image)
				Else
					paletteImages.AddImage(newImage)
				End If
			Next i
			paletteImages.EndInit()
		End Sub
		Private Sub RaisePaletteChanged()
			RaiseEvent OnPaletteChanged(Me, EventArgs.Empty)
		End Sub
		Private Sub RaiseNeedClose()
			RaiseEvent OnNeedClose(Me, EventArgs.Empty)
		End Sub
		Private Sub lbPalettes_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lbPalettes.SelectedIndexChanged
			If chart_Renamed IsNot Nothing Then
				chart_Renamed.Palette = SelectedPalette
			End If
			RaisePaletteChanged()
		End Sub
		Private Sub lbPalettes_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles lbPalettes.MouseDoubleClick
			If e.Button = MouseButtons.Left Then
				RaiseNeedClose()
			End If
		End Sub
		Private Sub lbPalettes_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lbPalettes.KeyDown
			If e.KeyCode = Keys.Enter Then
				RaiseNeedClose()
			End If
		End Sub
		Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEdit.Click
			Dim palette As Palette = SelectedPalette
			Using form As New PaletteEditorForm(paletteRepository_Renamed)
				form.LookAndFeel.ParentLookAndFeel = LookAndFeel.ParentLookAndFeel
				form.Location = ControlUtils.CalcLocation(Cursor.Position, Cursor.Position, form.Size)
				form.TopMost = True
				form.CurrentPalette = palette
				Dim result As DialogResult = form.ShowDialog()
				FillPalettes()
				If result = DialogResult.OK Then
					SelectedPalette = form.CurrentPalette
				Else
					SelectedPalette = palette
				End If
			End Using
		End Sub
	End Class
End Namespace
