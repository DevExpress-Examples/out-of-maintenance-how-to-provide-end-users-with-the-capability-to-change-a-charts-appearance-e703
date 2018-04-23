using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraCharts.Native;

namespace ChartAppearanceSample {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            stylesContainerControl1.Initialize(((IChartContainer)chartControl1).Chart);
            paletteEditControl1.Chart = ((IChartContainer)chartControl1).Chart;
        }

        private void paletteEditControl1_OnPaletteChanged(object sender, EventArgs e) {
            stylesContainerControl1.Initialize(((IChartContainer)chartControl1).Chart);
        }


    }
}