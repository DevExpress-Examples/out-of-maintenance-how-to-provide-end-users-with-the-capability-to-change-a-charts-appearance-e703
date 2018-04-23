Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraCharts.Native

Namespace ChartAppearanceSample
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
			stylesContainerControl1.Initialize((CType(chartControl1, IChartContainer)).Chart)
			paletteEditControl1.Chart = (CType(chartControl1, IChartContainer)).Chart
		End Sub

		Private Sub paletteEditControl1_OnPaletteChanged(ByVal sender As Object, ByVal e As EventArgs) Handles paletteEditControl1.OnPaletteChanged
			stylesContainerControl1.Initialize((CType(chartControl1, IChartContainer)).Chart)
		End Sub



	End Class
End Namespace