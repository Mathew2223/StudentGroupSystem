using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace StudentGroupSystem
{
    public partial class ReportWindow : Window
    {
        public ReportWindow(string reportText)
        {
            InitializeComponent();
            TxtReport.Text = reportText;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog
            {
                Filter = "Текстовый файл (*.txt)|*.txt|Все файлы (*.*)|*.*",
                FileName = $"Отчет_{DateTime.Now:yyyyMMdd_HHmm}.txt"
            };
            if (dlg.ShowDialog() == true)
            {
                File.WriteAllText(dlg.FileName, TxtReport.Text);
                MessageBox.Show("Отчёт сохранён.", "Готово",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}