﻿using System;
using System.Windows;
using System.Windows.Input;

namespace GameBook.Wpf.Views
{
    public class ExitEventToCommandPipe
    {
        public static DependencyProperty CommandProperty = DependencyProperty.RegisterAttached(
            "Command",
            typeof(ICommand),
            typeof(ExitEventToCommandPipe),
            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ExitEventToCommandPipe.CommandChanged)));

        public static void SetCommand(DependencyObject target, ICommand value)
        {
            Console.Write("SetCommand");
            target.SetValue(ExitEventToCommandPipe.CommandProperty, value);
        }

        public static ICommand GetCommand(DependencyObject target)
        {
            Console.Write("GetCommand");
            return (ICommand)target.GetValue(ExitEventToCommandPipe.CommandProperty);
        }

        private static void CommandChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            var window = target as Window;
            if (window != null)
            {
                window.Closed += (o, evt) => GetCommand(target)?.Execute(null);
            }
        }
    }

}