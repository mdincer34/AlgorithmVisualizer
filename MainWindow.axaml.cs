using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AlgorithmVisualizer
{
    public partial class MainWindow : Window
    {
        private bool _isPaused;
        private int _speed = 1;
        private const int RectangleMargin = 3;
        private const int MaxValue = 259;
        private AllAlgorithmsWindow allAlgorithmsWindow;
        Implementations imp;


        public MainWindow()
        {
            InitializeComponent();
            StartButton.Click += StartButton_Click;
            //PauseButton.Click += PauseButton_Click;
            //ResetButton.Click += ResetButton_Click;
            AlgorithmSelector.SelectionChanged += AlgorithmSelector_SelectionChanged;
            imp = new Implementations();
            imp._stopwatch = new Stopwatch();
        }

        private void AlgorithmSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedAlgorithm = (AlgorithmSelector.SelectedItem as ComboBoxItem).Content.ToString();
            DescriptionTextBlock.Text = $"Selected Algorithm: {selectedAlgorithm}";
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            List<int> data = imp.GenerateRandomData(MaxValue);
            var selectedAlgorithm = (AlgorithmSelector.SelectedItem as ComboBoxItem).Content.ToString();

            ResetButton_Click(sender, e);
            imp._stopwatch = new Stopwatch();
            imp._stopwatch.Reset();
            imp._stopwatch.Start();

            switch (selectedAlgorithm)
            {
                case "Bubble Sort":
                    await imp.VisualizeBubbleSort(data, VisualizationCanvas, TimerTextBlock, imp._stopwatch);
                    break;
                case "Quick Sort":
                    await imp.VisualizeQuickSort(data, VisualizationCanvas, TimerTextBlock, imp._stopwatch);
                    break;
                case "Insertion Sort":
                    await imp.VisualizeInsertionSort(data, VisualizationCanvas, TimerTextBlock, imp._stopwatch);
                    break;
                case "Selection Sort":
                    await imp.VisualizeSelectionSort(data, VisualizationCanvas, TimerTextBlock, imp._stopwatch);
                    break;
                case "Merge Sort":
                    await imp.VisualizeMergeSort(data, VisualizationCanvas, TimerTextBlock, imp._stopwatch);
                    break;
            }

            imp._stopwatch.Stop();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            _isPaused = !_isPaused;
            // PauseButton.Content = _isPaused ? "Resume" : "Pause";

            if (_isPaused)
                imp._stopwatch.Stop();
            else
                imp._stopwatch.Start();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            imp._stopwatch.Reset();
            VisualizationCanvas.Children.Clear();
            DescriptionTextBlock.Text = "Select an algorithm to visualize.";
            TimerTextBlock.Text = "Elapsed Time: 00:00:00";
        }



        private void AllAlgorithmsWindow_Checked(object sender, RoutedEventArgs e)
        {
            if (allAlgorithmsWindow != null)
            {
                allAlgorithmsWindow.Activate();
                return;
            }

            allAlgorithmsWindow = new AllAlgorithmsWindow();
            allAlgorithmsWindow.Closed += (s, args) => allAlgorithmsWindow = null;
            allAlgorithmsWindow.Show();
            allAlgorithmsWindow.WindowState = WindowState.Maximized;
        }
    }
}
