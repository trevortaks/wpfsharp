﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace instasharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    
    public partial class MainWindow : Window
    {
       //User currentUser = null;
        public ViewModel _model;
        public MainWindow()
        {

            InitializeComponent();
            _model = new ViewModel();
            DataContext = _model;
            //ppLogin.Visibility 
            var a = 1;
        }
           
    }
}
