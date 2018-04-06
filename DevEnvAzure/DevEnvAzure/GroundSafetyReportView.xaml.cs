using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroundSafetyReportView : ContentView
    {
        public static string lightValue;
        public static string weatherValue;
        public static string precipitationValue;
        public static string groundconditionValue;

        public static string typeofFuelValue;
        public static string WhereSpillValue;

        public static string baggageValue;
        public static string cargoValue;
        public static string loadsheetValue;
        public static string impactairValue;

        public static string paxValue;
        public static string identWhenValue;
        public static string idenWhereValue;



        public GroundSafetyReportView()
        {
            InitializeComponent();
        }

        private void lightValue_Changed(object sender, EventArgs e)
        {
            if (Lightpicker.SelectedIndex > 0)
                lightValue = Lightpicker.Items.ElementAt(Lightpicker.SelectedIndex);
        }
        private void weatherValue_Changed(object sender, EventArgs e)
        {
            if (Weatherpicker.SelectedIndex > 0)
                weatherValue = Weatherpicker.Items.ElementAt(Weatherpicker.SelectedIndex);
        }
        private void precipitationValue_Changed(object sender, EventArgs e)
        {
            if (Precipitationpicker.SelectedIndex > 0)
                precipitationValue = Precipitationpicker.Items.ElementAt(Precipitationpicker.SelectedIndex);
        }
        private void groundconditionValue_Changed(object sender, EventArgs e)
        {
            if (GroundConditionspicker.SelectedIndex > 0)
                groundconditionValue = GroundConditionspicker.Items.ElementAt(GroundConditionspicker.SelectedIndex);
        }
        private void typeofFuelValue_Changed(object sender, EventArgs e)
        {
            if (TypeofLiquidSpilledpicker.SelectedIndex > 0)
                typeofFuelValue = TypeofLiquidSpilledpicker.Items.ElementAt(TypeofLiquidSpilledpicker.SelectedIndex);
        }
        private void WhereSpillValue_Changed(object sender, EventArgs e)
        {
            if (WhereSpillOccurredpicker.SelectedIndex > 0)
                WhereSpillValue = WhereSpillOccurredpicker.Items.ElementAt(WhereSpillOccurredpicker.SelectedIndex);
        }
        private void baggageValue_Changed(object sender, EventArgs e)
        {
            if (Baggagepicker.SelectedIndex > 0)
                baggageValue = Baggagepicker.Items.ElementAt(Baggagepicker.SelectedIndex);
        }
        private void cargoValue_Changed(object sender, EventArgs e)
        {
            if (Cargopicker.SelectedIndex > 0)
                cargoValue = Cargopicker.Items.ElementAt(Cargopicker.SelectedIndex);
        }
        private void loadsheetValue_Changed(object sender, EventArgs e)
        {
            if (LoadSheetpicker.SelectedIndex > 0)
                loadsheetValue = LoadSheetpicker.Items.ElementAt(LoadSheetpicker.SelectedIndex);
        }
        private void impactairValue_Changed(object sender, EventArgs e)
        {
            if (ImpacttoAircraftPerformancepicker.SelectedIndex > 0)
                impactairValue = ImpacttoAircraftPerformancepicker.Items.ElementAt(ImpacttoAircraftPerformancepicker.SelectedIndex);
        }
        private void paxValue_Changed(object sender, EventArgs e)
        {
            if (PaxorCargopicker.SelectedIndex > 0)
                paxValue = PaxorCargopicker.Items.ElementAt(PaxorCargopicker.SelectedIndex);
        }
        private void identWhenValue_Changed(object sender, EventArgs e)
        {
            if (IdentifiedWhenpicker.SelectedIndex > 0)
                identWhenValue = IdentifiedWhenpicker.Items.ElementAt(IdentifiedWhenpicker.SelectedIndex);
        }
        private void idenWhereValue_Changed(object sender, EventArgs e)
        {
            if (IdentifiedWherepicker.SelectedIndex > 0)
                idenWhereValue = IdentifiedWherepicker.Items.ElementAt(IdentifiedWherepicker.SelectedIndex);
        }

    }
}