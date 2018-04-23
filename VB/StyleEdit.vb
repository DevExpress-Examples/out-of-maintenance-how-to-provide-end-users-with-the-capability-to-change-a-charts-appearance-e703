Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports DevExpress.Skins
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraEditors.Drawing


Namespace DevExpress.XtraCharts.Design
	Friend Class StyleEditPainter
		Inherits PictureEditPainter
		Implements IDisposable
		Protected highlightPen As Pen

		Protected Overridable ReadOnly Property InnerIndent() As Integer
			Get
				Return 0
			End Get
		End Property

		Public Sub New(ByVal focusColor As Color)
			highlightPen = New Pen(focusColor, 2.0f)
		End Sub
		Public Sub Dispose() Implements IDisposable.Dispose
			If highlightPen IsNot Nothing Then
				highlightPen.Dispose()
				highlightPen = Nothing
			End If
		End Sub
		Protected Overrides Sub DrawFocusRect(ByVal info As ControlGraphicsInfoArgs)
			Dim vi As BaseEditViewInfo = TryCast(info.ViewInfo, BaseEditViewInfo)
			If vi IsNot Nothing AndAlso vi.DrawFocusRect Then
				DrawCustomBorder(info)
				Return
			End If
			MyBase.DrawFocusRect(info)
		End Sub
		Protected Sub DrawCustomBorder(ByVal info As ControlGraphicsInfoArgs)
			Dim smoothing As SmoothingMode = info.Graphics.SmoothingMode
			Try
				info.Graphics.SmoothingMode = SmoothingMode.HighQuality
				Using path As New GraphicsPath()
					Dim halfWidth As Integer = -CInt(Fix(Math.Ceiling(highlightPen.Width / 2.0))) - InnerIndent
					Dim bounds As Rectangle = Rectangle.Inflate(info.Bounds, halfWidth, halfWidth)
					Dim fraction As Integer = 6
					Dim right As Integer = bounds.Right - fraction - 1
					Dim bottom As Integer = bounds.Bottom - fraction - 1
					path.AddArc(bounds.X, bounds.Y, fraction, fraction, 180, 90)
					path.AddArc(right, bounds.Y, fraction, fraction, 270, 90)
					path.AddArc(right, bottom, fraction, fraction, 0, 90)
					path.AddArc(bounds.Left, bottom, fraction, fraction, 90, 90)
					path.CloseFigure()
					info.Graphics.DrawPath(highlightPen, path)
				End Using
			Finally
				info.Graphics.SmoothingMode = smoothing
			End Try
		End Sub
	End Class
	Friend Class StyleEdit
		Inherits PictureEdit
		Private painter_Renamed As StyleEditPainter
		Protected Overrides ReadOnly Property Painter() As BaseControlPainter
			Get
				If painter_Renamed Is Nothing Then
					painter_Renamed = New StyleEditPainter(CommonSkins.GetSkin(LookAndFeel).Colors("Highlight"))
				End If
				Return painter_Renamed
			End Get
		End Property
		Protected Overrides Overloads Sub Dispose(ByVal disposing As Boolean)
			MyBase.Dispose(disposing)
			If disposing AndAlso painter_Renamed IsNot Nothing Then
				painter_Renamed.Dispose()
				painter_Renamed = Nothing
			End If
		End Sub
		Protected Overrides Sub OnGotFocus(ByVal e As EventArgs)
			MyBase.OnGotFocus(e)
			Dim container As StylesContainerControl = TryCast(Parent, StylesContainerControl)
			If container IsNot Nothing Then
				container.OnFocusChanged()
			End If
		End Sub
		Protected Overrides Sub OnMouseDoubleClick(ByVal e As MouseEventArgs)
			If e.Button = MouseButtons.Left Then
				CloseContainer()
			Else
				MyBase.OnMouseDoubleClick(e)
			End If
		End Sub
		Protected Overrides Sub OnKeyDown(ByVal e As KeyEventArgs)
			If e.KeyCode = Keys.Return Then
				CloseContainer()
			Else
				MyBase.OnKeyDown(e)
			End If
		End Sub
		Private Sub CloseContainer()
			Dim container As StylesContainerControl = TryCast(Parent, StylesContainerControl)
			If container IsNot Nothing Then
				container.RaiseNeedClose()
			End If
		End Sub
	End Class
End Namespace
