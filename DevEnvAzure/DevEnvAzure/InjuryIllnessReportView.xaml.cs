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
    public partial class InjuryIllnessReportView : ContentView
    {
        public static string PersonInvolvedValue;
        public static string NatureofInjuryValue;
        public static string HowInjuryOccurredValue;
        public static string ObjectValue;
        public static string TreatmentByValue;
        public static string PartofbodyinjuredValue;
        public static string TypeofIllnessInjuryValue;
        public static string TypeofoccurrenceValue;


        public InjuryIllnessReportView()
        {
            InitializeComponent();
        }
        private void PersonInvolvedValue_Changed(object sender, EventArgs e)
        {
            if (PersonInvolvedpicker.SelectedIndex > 0)
                PersonInvolvedValue = PersonInvolvedpicker.Items.ElementAt(PersonInvolvedpicker.SelectedIndex);
        }
        private void NatureofInjuryValue_Changed(object sender, EventArgs e)
        {
            if (NatureofInjurypicker.SelectedIndex > 0)
                NatureofInjuryValue = NatureofInjurypicker.Items.ElementAt(NatureofInjurypicker.SelectedIndex);
        }
        private void HowInjuryOccurredValue_Changed(object sender, EventArgs e)
        {
            if (HowInjuryOccurredpicker.SelectedIndex > 0)
                HowInjuryOccurredValue = HowInjuryOccurredpicker.Items.ElementAt(HowInjuryOccurredpicker.SelectedIndex);
        }
        private void ObjectValue_Changed(object sender, EventArgs e)
        {
            if (Objectpicker.SelectedIndex > 0)
                ObjectValue = Objectpicker.Items.ElementAt(Objectpicker.SelectedIndex);
        }
        private void TreatmentByValue_Changed(object sender, EventArgs e)
        {
            if (TreatmentByspicker.SelectedIndex > 0)
                TreatmentByValue = TreatmentByspicker.Items.ElementAt(TreatmentByspicker.SelectedIndex);
        }
        private void PartofbodyinjuredValue_Changed(object sender, EventArgs e)
        {
            if (Partofbodyinjuredpicker.SelectedIndex > 0)
                PartofbodyinjuredValue = Partofbodyinjuredpicker.Items.ElementAt(Partofbodyinjuredpicker.SelectedIndex);
        }
        private void TypeofIllnessInjuryValue_Changed(object sender, EventArgs e)
        {
            if (TypeofIllnessInjurypicker.SelectedIndex > 0)
                TypeofIllnessInjuryValue = TypeofIllnessInjurypicker.Items.ElementAt(TypeofIllnessInjurypicker.SelectedIndex);
        }
        private void TypeofoccurrenceValue_Changed(object sender, EventArgs e)
        {
            if (Typeofoccurrencepicker.SelectedIndex > 0)
                TypeofoccurrenceValue = Typeofoccurrencepicker.Items.ElementAt(Typeofoccurrencepicker.SelectedIndex);
        }
       
    }
}