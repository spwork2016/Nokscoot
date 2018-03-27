﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MultiSelectView : ContentView
    {
        public MultiSelectView()
        {
           
            InitializeComponent();

            PopuViewList.ItemsSource = App.multiview;
            PopuViewList.BindingContext = this;
            BindingContext = this;
        }
    }
}