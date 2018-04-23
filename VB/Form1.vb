Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms


Namespace ChartAppearanceSample

	Partial Public Class Form1
		Inherits Form

		Public Sub New()
			InitializeComponent()
			stylesContainerControl1.Initialize(chartControl1)
			paletteEditControl1.Chart = chartControl1
		End Sub

		Private Sub paletteEditControl1_OnPaletteChanged(ByVal sender As Object, ByVal e As EventArgs) Handles paletteEditControl1.OnPaletteChanged
			stylesContainerControl1.Initialize(chartControl1)
		End Sub


	End Class
End Namespace