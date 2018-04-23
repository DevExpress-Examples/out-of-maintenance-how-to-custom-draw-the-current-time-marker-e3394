using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;

namespace SchedulerCustomDrawCurrentTimeMarker {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            schedulerControl1.DayView.TimeRulers[0].ShowCurrentTime = false;
        }

        private void schedulerControl1_CustomDrawDayViewTimeRuler(object sender, DevExpress.XtraScheduler.CustomDrawObjectEventArgs e) {
            SchedulerControl scheduler = (SchedulerControl)sender;
            DateTime start = scheduler.Start;
            TimeSpan now = DateTime.Now.TimeOfDay;
            TimeRulerViewInfo viewInfo = (TimeRulerViewInfo)e.ObjectInfo;
            Rectangle rect = viewInfo.ContentBounds;
            Rectangle nowRect = rect;
            DayViewRowCollection visibleRows = scheduler.DayView.ViewInfo.VisibleRows;

            if (visibleRows.Count == 0)
                return;
            
            float offsetRatio = (now - scheduler.DayView.TopRowTime).Ticks / (float)TimeSpan.FromTicks(visibleRows[0].Interval.Duration.Ticks * visibleRows.Count).Ticks;

            if (offsetRatio < 0)
                return;

            nowRect.Height = 4;
            nowRect.Y += (int)Math.Round(rect.Height * offsetRatio - (float)nowRect.Height / 2);

            e.DrawDefault();
            e.Cache.FillRectangle(Brushes.Red, nowRect);
            e.Handled = true;
        }
    }
}