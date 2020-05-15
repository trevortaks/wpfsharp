﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace instasharp
{
    public class JoinConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return values.Clone();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
