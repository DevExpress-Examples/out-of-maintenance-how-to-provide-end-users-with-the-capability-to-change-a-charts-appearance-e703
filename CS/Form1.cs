using System;
using System.Windows.Forms;


namespace ChartAppearanceSample {

    public partial class Form1 : Form {

        public Form1() {
            InitializeComponent();
            stylesContainerControl1.Initialize(chartControl1);
            paletteEditControl1.Chart = chartControl1;
        }

        private void paletteEditControl1_OnPaletteChanged(object sender, EventArgs e) {
            stylesContainerControl1.Initialize(chartControl1);
        }


    }
}