using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;

namespace SchedulerCustomDrawCurrentTimeMarker {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            schedulerControl1.DayView.TimeRulers[0].TimeMarkerVisibility = TimeMarkerVisibility.Never;

        }

        private void schedulerControl1_CustomDrawDayViewTimeRuler(object sender, DevExpress.XtraScheduler.CustomDrawObjectEventArgs e) {
            SchedulerControl scheduler = (SchedulerControl)sender;
            TimeSpan now = DateTime.Now.TimeOfDay;
            TimeRulerViewInfo viewInfo = (TimeRulerViewInfo)e.ObjectInfo;
            Rectangle rect = viewInfo.ContentBounds;
            Rectangle nowRect = rect;
            DayViewInfo dayViewInfo = scheduler.DayView.ViewInfo as DayViewInfo;

            if (dayViewInfo.VisibleRowsCount == 0)
                return;

            float offsetRatio = (now - scheduler.DayView.TopRowTime).Ticks / (float)TimeSpan.FromTicks(scheduler.DayView.TimeScale.Ticks * dayViewInfo.VisibleRowsCount).Ticks;

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