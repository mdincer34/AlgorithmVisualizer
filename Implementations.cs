using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AlgorithmVisualizer
{
    public class Implementations
    {
        private const int RectangleMargin = 3;
        private int _speed = 10;
        private bool _isPaused;
        private const int MaxValue = 100;

        public Stopwatch _stopwatch;

        #region BubbleSort
        public async Task VisualizeBubbleSort(List<int> data, Canvas canvas, TextBlock textBlock, Stopwatch timer)
        {
            timer.Start();
            for (int i = 0; i < data.Count - 1; i++)
            {
                for (int j = 0; j < data.Count - i - 1; j++)
                {
                    if (data[j] > data[j + 1])
                    {
                        int temp = data[j];
                        data[j] = data[j + 1];
                        data[j + 1] = temp;

                        UpdateVisualization(data, canvas);
                        UpdateTimer(textBlock, timer);
                        await Task.Delay(_speed);

                        while (_isPaused)
                        {
                            await Task.Delay(100);
                        }
                    }
                }
            }
            timer.Stop();
        }
        #endregion

        #region Helpers

        private void UpdateVisualization(List<int> data, Canvas canvas)
        {
            canvas.Children.Clear();

            double canvasWidth = canvas.Bounds.Width;
            double canvasHeight = canvas.Bounds.Height;

            double rectWidth = (canvasWidth / data.Count) - RectangleMargin;

            double scaleFactor = canvasHeight / MaxValue;

            for (int i = 0; i < data.Count; i++)
            {
                var rect = new Rectangle
                {
                    Width = rectWidth,
                    Height = data[i] * scaleFactor,
                    Fill = Brushes.GreenYellow
                };
                Canvas.SetLeft(rect, i * (rectWidth + RectangleMargin));
                Canvas.SetTop(rect, canvasHeight - rect.Height);
                canvas.Children.Add(rect);
            }
        }

        public List<int> GenerateRandomData(int howMany)
        {
            List<int> data = new List<int>();

            Random rng = new Random();
            for (int i = 0; i < howMany; i++)
            {
                data.Add(rng.Next(1, MaxValue));
            }
            return data;
        }

        private void UpdateTimer(TextBlock textBlock, Stopwatch stopwatch)
        {
            TimeSpan elapsed = stopwatch.Elapsed;
            textBlock.Text = $"Elapsed Time: {elapsed:hh\\:mm\\:ss}";
        }
        #endregion

        #region QuickSort
        public async Task VisualizeQuickSort(List<int> data, Canvas canvas, TextBlock textBlock, Stopwatch stopwatch)
        {
            stopwatch.Start();
            await QuickSort(data, 0, data.Count - 1, canvas, textBlock, stopwatch);
            stopwatch.Stop();
        }

        private async Task QuickSort(List<int> data, int low, int high, Canvas canvas, TextBlock textBlock, Stopwatch stopwatch)
        {
            if (low < high)
            {
                int pi = await Partition(data, low, high, canvas, textBlock, stopwatch);

                await QuickSort(data, low, pi - 1, canvas, textBlock, stopwatch);
                await QuickSort(data, pi + 1, high, canvas, textBlock, stopwatch);
            }
        }

        private async Task<int> Partition(List<int> data, int low, int high, Canvas canvas, TextBlock textBlock, Stopwatch stopwatch)
        {
            int pivot = data[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (data[j] < pivot)
                {
                    i++;
                    int temp = data[i];
                    data[i] = data[j];
                    data[j] = temp;

                    UpdateVisualization(data, canvas);
                    UpdateTimer(textBlock, stopwatch);
                    await Task.Delay(_speed);

                    while (_isPaused)
                    {
                        await Task.Delay(100);
                    }
                }
            }

            int temp1 = data[i + 1];
            data[i + 1] = data[high];
            data[high] = temp1;

            UpdateVisualization(data, canvas);
            UpdateTimer(textBlock, stopwatch);
            await Task.Delay(_speed);

            while (_isPaused)
            {
                await Task.Delay(100);
            }

            return i + 1;
        }
        #endregion

        #region InsertionSort
        public async Task VisualizeInsertionSort(List<int> data, Canvas canvas, TextBlock textBlock, Stopwatch stopwatch)
        {
            stopwatch.Start();
            for (int i = 1; i < data.Count; i++)
            {
                int key = data[i];
                int j = i - 1;

                while (j >= 0 && data[j] > key)
                {
                    data[j + 1] = data[j];
                    j--;

                    UpdateVisualization(data, canvas);
                    UpdateTimer(textBlock, stopwatch);
                    await Task.Delay(_speed);

                    while (_isPaused)
                    {
                        await Task.Delay(100);
                    }
                }
                data[j + 1] = key;

                UpdateVisualization(data, canvas);
                UpdateTimer(textBlock, stopwatch);
                await Task.Delay(_speed);

                while (_isPaused)
                {
                    await Task.Delay(100);
                }
            }
            stopwatch.Stop();
        }
        #endregion

        #region SelectionSort
        public async Task VisualizeSelectionSort(List<int> data, Canvas canvas, TextBlock textBlock, Stopwatch stopwatch)
        {
            stopwatch.Start();
            for (int i = 0; i < data.Count - 1; i++)
            {
                int minIdx = i;
                for (int j = i + 1; j < data.Count; j++)
                {
                    if (data[j] < data[minIdx])
                    {
                        minIdx = j;
                    }
                }

                int temp = data[minIdx];
                data[minIdx] = data[i];
                data[i] = temp;

                UpdateVisualization(data, canvas);
                UpdateTimer(textBlock, stopwatch);
                await Task.Delay(_speed);

                while (_isPaused)
                {
                    await Task.Delay(100);
                }
            }
            stopwatch.Stop();
        }
        #endregion

        #region MergeSort
        public async Task VisualizeMergeSort(List<int> data, Canvas canvas, TextBlock textBlock, Stopwatch stopwatch)
        {
            stopwatch.Start();
            await MergeSort(data, 0, data.Count - 1, canvas, textBlock, stopwatch);
            stopwatch.Stop();
        }

        private async Task MergeSort(List<int> data, int left, int right, Canvas canvas, TextBlock textBlock, Stopwatch stopwatch)
        {
            if (left < right)
            {
                int mid = left + (right - left) / 2;

                await MergeSort(data, left, mid, canvas, textBlock, stopwatch);
                await MergeSort(data, mid + 1, right, canvas, textBlock, stopwatch);

                await Merge(data, left, mid, right, canvas, textBlock, stopwatch);
            }
        }

        private async Task Merge(List<int> data, int left, int mid, int right, Canvas canvas, TextBlock textBlock, Stopwatch stopwatch)
        {
            int n1 = mid - left + 1;
            int n2 = right - mid;

            int[] leftArray = new int[n1];
            int[] rightArray = new int[n2];

            Array.Copy(data.ToArray(), left, leftArray, 0, n1);
            Array.Copy(data.ToArray(), mid + 1, rightArray, 0, n2);

            int i = 0, j = 0;
            int k = left;

            while (i < n1 && j < n2)
            {
                if (leftArray[i] <= rightArray[j])
                {
                    data[k] = leftArray[i];
                    i++;
                }
                else
                {
                    data[k] = rightArray[j];
                    j++;
                }
                k++;

                UpdateVisualization(data, canvas);
                UpdateTimer(textBlock, stopwatch);
                await Task.Delay(_speed);

                while (_isPaused)
                {
                    await Task.Delay(100);
                }
            }

            while (i < n1)
            {
                data[k] = leftArray[i];
                i++;
                k++;

                UpdateVisualization(data, canvas);
                UpdateTimer(textBlock, stopwatch);
                await Task.Delay(_speed);

                while (_isPaused)
                {
                    await Task.Delay(100);
                }
            }

            while (j < n2)
            {
                data[k] = rightArray[j];
                j++;
                k++;

                UpdateVisualization(data, canvas);
                UpdateTimer(textBlock, stopwatch);
                await Task.Delay(_speed);

                while (_isPaused)
                {
                    await Task.Delay(100);
                }
            }
        }
        #endregion
    }
}
