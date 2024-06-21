using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AlgorithmVisualizer
{
    public partial class AllAlgorithmsWindow : Window
    {
        Implementations imp;
        private const int MaxValue = 100;

        public AllAlgorithmsWindow()
        {
            InitializeComponent();
            imp = new Implementations();
            StartButton.Click += StartButton_Click;
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            #region GenericLists
            List<int> bubbleNums = imp.GenerateRandomData(MaxValue);
            List<int> quickNums = imp.GenerateRandomData(MaxValue);
            List<int> insertionNums = imp.GenerateRandomData(MaxValue);
            List<int> selectionNums = imp.GenerateRandomData(MaxValue);
            List<int> mergeNums = imp.GenerateRandomData(MaxValue);
            #endregion

            Stopwatch bubbleStopwatch = new();
            Stopwatch quickStopwatch = new();
            Stopwatch insertionStopwatch = new();
            Stopwatch selectionStopwatch = new();
            Stopwatch mergeStopwatch = new();

            var bubbleTask = imp.VisualizeBubbleSort(bubbleNums, BubbleCanvas, TimerTextBlockBubble, bubbleStopwatch);
            var quickTask = imp.VisualizeQuickSort(quickNums, QuickCanvas, TimerTextBlockQuick, quickStopwatch);
            var insertionTask = imp.VisualizeInsertionSort(insertionNums, InsertionCanvas, TimerTextBlockInsertion, insertionStopwatch);
            var selectionTask = imp.VisualizeSelectionSort(selectionNums, SelectionCanvas, TimerTextBlockSelection, selectionStopwatch);
            var mergeTask = imp.VisualizeMergeSort(mergeNums, MergeCanvas, TimerTextBlockMerge, mergeStopwatch);

            await Task.WhenAll(bubbleTask, quickTask, insertionTask, selectionTask, mergeTask);

            StartButton.Content = "All Done!";
        }
    }
}
