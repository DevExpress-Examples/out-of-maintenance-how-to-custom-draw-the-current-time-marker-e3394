Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Drawing

Namespace SchedulerCustomDrawCurrentTimeMarker
    Partial Public Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
            schedulerControl1.DayView.TimeRulers(0).TimeMarkerVisibility = TimeMarkerVisibility.Never

        End Sub

        Private Sub schedulerControl1_CustomDrawDayViewTimeRuler(ByVal sender As Object, ByVal e As DevExpress.XtraScheduler.CustomDrawObjectEventArgs) Handles schedulerControl1.CustomDrawDayViewTimeRuler
            Dim scheduler As SchedulerControl = DirectCast(sender, SchedulerControl)
            Dim now As TimeSpan = Date.Now.TimeOfDay
            Dim viewInfo As TimeRulerViewInfo = CType(e.ObjectInfo, TimeRulerViewInfo)
            Dim rect As Rectangle = viewInfo.ContentBounds
            Dim nowRect As Rectangle = rect
            Dim dayViewInfo As DayViewInfo = TryCast(scheduler.DayView.ViewInfo, DayViewInfo)

            If dayViewInfo.VisibleRowsCount = 0 Then
                Return
            End If

            Dim offsetRatio As Single = (now - scheduler.DayView.TopRowTime).Ticks / CSng(TimeSpan.FromTicks(scheduler.DayView.TimeScale.Ticks * dayViewInfo.VisibleRowsCount).Ticks)

            If offsetRatio < 0 Then
                Return
            End If

            nowRect.Height = 4
            nowRect.Y += CInt((Math.Round(rect.Height * offsetRatio - CSng(nowRect.Height) / 2)))

            e.DrawDefault()
            e.Cache.FillRectangle(Brushes.Red, nowRect)
            e.Handled = True
        End Sub
    End Class
End Namespace