Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraCharts.Native

Namespace DevExpress.XtraCharts.Design
	Public Class StylesContainerControl
		Inherits XtraScrollableControl
		Private Class AppearanceColors
			Private appearance_Renamed As RootAppearance
			Private palette As Palette
			Private editors() As StyleEdit
			Public ReadOnly Property Appearance() As RootAppearance
				Get
					Return appearance_Renamed
				End Get
			End Property
			Public ReadOnly Property ColorsCount() As Integer
				Get
					Return palette.Count + 1
				End Get
			End Property
			Default Public Property Item(ByVal index As Integer) As StyleEdit
				Get
					Return editors(index)
				End Get
				Set(ByVal value As StyleEdit)
					editors(index) = value
				End Set
			End Property
			Public Sub New(ByVal appearance As RootAppearance, ByVal palette As Palette)
				Me.appearance_Renamed = appearance
				Me.palette = palette
				editors = New StyleEdit(ColorsCount - 1){}
			End Sub
		End Class
		Private paletteRepository As PaletteRepository
		Private matrix As List(Of AppearanceColors)
		Private lastRegisteredAppearance As Integer
		Private elementSize As Size
		Private current As Point
		Private chart As Chart
		Private lockChangeCurrent As Boolean = False
		Public ReadOnly Property CurrentAppearance() As RootAppearance
			Get
				Return matrix(current.Y).Appearance
			End Get
		End Property
		Public ReadOnly Property CurrentPaletteIndex() As Integer
			Get
				Return current.X
			End Get
		End Property
		Public Event OnEditValueChanged As EventHandler
		Public Event OnNeedClose As EventHandler
		Public Sub Initialize(ByVal chart As Chart)
			Me.chart = chart
			UpdateIt()

		End Sub

		Public Sub UpdateIt()
			DisposeAppearanceImages()
			Dim viewType As ViewType
			If chart.Series.Count > 0 Then
				viewType = SeriesViewFactory.GetViewType(chart.Series(0).View)
			Else
				viewType = ViewType.Bar
			End If
			SuspendLayout()
			Try
				SetPaletteRepository(chart.PaletteRepository)
				SetAppearancesCount(chart.AppearanceRepository.Names.Length)
				For Each appearance As RootAppearance In chart.AppearanceRepository
					If (Not chart.Palette.Predefined) OrElse appearance Is chart.Appearance OrElse String.IsNullOrEmpty(appearance.PaletteName) OrElse appearance.PaletteName = chart.Palette.Name Then
						RegisterAppearance(appearance, viewType, chart.Palette)
					End If
				Next appearance
			Finally
				ResumeLayout()
			End Try
		End Sub
		Public Sub SetPaletteRepository(ByVal paletteRepository As PaletteRepository)
			Me.paletteRepository = paletteRepository
		End Sub
		Public Sub SetAppearancesCount(ByVal count As Integer)
			matrix = New List(Of AppearanceColors)(count)
			lastRegisteredAppearance = 0
		End Sub
		Public Sub SelectStyle(ByVal appearance As RootAppearance, ByVal paletteIndex As Integer)
			For i As Integer = 0 To matrix.Count - 1
				Dim row As AppearanceColors = matrix(i)
				If row.Appearance Is appearance AndAlso paletteIndex < row.ColorsCount Then
					current = New Point(paletteIndex, i)
					lockChangeCurrent = True
					Return
				End If
			Next i
			current = Point.Empty
		End Sub
		Public Sub RegisterAppearance(ByVal appearance As RootAppearance, ByVal viewType As ViewType, ByVal palette As Palette)
			Dim row As New AppearanceColors(appearance, palette)
			matrix.Add(row)
			For i As Integer = 0 To palette.Count
				row(i) = AddStyleEditor(appearance.CreateImage(viewType, palette, i), i)
			Next i
			lastRegisteredAppearance += 1
		End Sub
		Private Sub DisposeAppearanceImages()
			For Each control As Control In Controls
				Dim pictureEdit As PictureEdit = TryCast(control, PictureEdit)
				If pictureEdit IsNot Nothing AndAlso pictureEdit.Image IsNot Nothing Then
					pictureEdit.Image.Dispose()
				End If
				control.Dispose()
			Next control
			Controls.Clear()
		End Sub
		Private Function AddStyleEditor(ByVal image As Image, ByVal column As Integer) As StyleEdit
			elementSize = image.Size
			Dim styleEdit As New StyleEdit()
			styleEdit.Location = New Point(elementSize.Width * column, elementSize.Height * lastRegisteredAppearance)
			styleEdit.Size = elementSize
			styleEdit.Image = image
			styleEdit.BorderStyle = BorderStyles.NoBorder
			styleEdit.Properties.ReadOnly = True
			Controls.Add(styleEdit)
			Return styleEdit
		End Function
		Private Sub SelectCurrentChild()
			lockChangeCurrent = True
			Try
				matrix(current.Y)(current.X).Focus()
			Finally
				lockChangeCurrent = False
			End Try
		End Sub
		Private Sub CheckCurrentX()
			Dim colorsCount As Integer = matrix(current.Y).ColorsCount
			If current.X >= colorsCount Then
				current.X = colorsCount - 1
			End If
		End Sub
		Private Sub EditValueChanged()
			Dim palette As Palette = chart.Palette
			chart.Appearance = CurrentAppearance
			chart.Palette = palette
			chart.PaletteBaseColorNumber = CurrentPaletteIndex
			RaiseEvent OnEditValueChanged(Me, EventArgs.Empty)

		End Sub
		Friend Sub RaiseNeedClose()
			RaiseEvent OnNeedClose(Me, EventArgs.Empty)
		End Sub
		Friend Sub OnFocusChanged()
			If lockChangeCurrent Then
				Return
			End If
			For y As Integer = 0 To matrix.Count - 1
				Dim row As AppearanceColors = matrix(y)
				For x As Integer = 0 To row.ColorsCount - 1
					If row(x).Focused Then
						current.X = x
						current.Y = y
						EditValueChanged()
						Return
					End If
				Next x
			Next y
		End Sub
		Protected Overrides Sub OnGotFocus(ByVal e As EventArgs)
			SelectCurrentChild()
		End Sub
		Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
			Select Case keyData
				Case Keys.Left
'INSTANT VB TODO TASK: Assignments within expressions are not supported in VB.NET
'ORIGINAL LINE: if (--current.X < 0)
					If --current.X < 0 Then
						current.X = matrix(current.Y).ColorsCount - 1
					End If
				Case Keys.Right
'INSTANT VB TODO TASK: Assignments within expressions are not supported in VB.NET
'ORIGINAL LINE: if (++current.X >= matrix[current.Y].ColorsCount)
					If ++current.X >= matrix(current.Y).ColorsCount Then
						current.X = 0
					End If
				Case Keys.Up
'INSTANT VB TODO TASK: Assignments within expressions are not supported in VB.NET
'ORIGINAL LINE: if (--current.Y < 0)
					If --current.Y < 0 Then
						current.Y = matrix.Count - 1
					End If
					CheckCurrentX()
				Case Keys.Down
'INSTANT VB TODO TASK: Assignments within expressions are not supported in VB.NET
'ORIGINAL LINE: if (++current.Y >= matrix.Count)
					If ++current.Y >= matrix.Count Then
						current.Y = 0
					End If
					CheckCurrentX()
				Case Keys.Home
					current.X = 0
					current.Y = 0
				Case Keys.End
					current.Y = matrix.Count - 1
					current.X = matrix(current.Y).ColorsCount - 1
				Case Keys.PageUp
					Dim [step] As Integer = ClientSize.Height \ elementSize.Height
					If [step] = 0 Then
						[step] += 1
					End If
					current.Y -= [step]
					If current.Y < 0 Then
						current.Y = 0
					End If
					current.X = 0
					Exit Select
				Case Keys.PageDown
					Dim [step] As Integer = ClientSize.Height \ elementSize.Height
					If [step] = 0 Then
						[step] += 1
					End If
					current.Y += [step]
					If current.Y >= matrix.Count Then
						current.Y = matrix.Count - 1
					End If
					current.X = Integer.MaxValue
					CheckCurrentX()
					Exit Select
				Case Else
					Return MyBase.ProcessDialogKey(keyData)
			End Select
			SelectCurrentChild()
			EditValueChanged()
			Return True
		End Function
		Protected Overrides Overloads Sub Dispose(ByVal disposing As Boolean)
			MyBase.Dispose(disposing)
			DisposeAppearanceImages()
		End Sub
	End Class
End Namespace
