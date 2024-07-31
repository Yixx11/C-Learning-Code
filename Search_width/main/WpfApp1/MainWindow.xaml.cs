using System;
using System.Reflection.Metadata;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int col = 30, row = 30;
        private Rectangle[,] _rectangle = new Rectangle[30, 30];
        private Random _random = new Random();
        private int[,] visited = new int[row, col];
        private int[,] map = new int[row, col];
        Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();
        Queue<Tuple<int, int>> pathqueue = new Queue<Tuple<int, int>>();
        private int[] dx = new int[4] {-1,0,1,0};
        private int[] dy = new int[4] { 0, 1, 0, -1}; //逆时针
        private int startX = 11, startY = 25;
        private int endX = 28, endY = 9;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Draw_Line();
            RandomStartEnd();
            InitArry();
            Createobstacle();          
            BFS();
            SetPathColor();
        }

        private void Draw_Line() 
        {
            for (int i = 0; i < 30; i++)
            {
                mapGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < 30; i++)
            {
                mapGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    Rectangle rectangle = new Rectangle();
                    //在每次内层循环中，创建一个新的Rectangle对象。
                    rectangle.Fill = Brushes.White;
                    rectangle.SetValue(Grid.RowProperty, j);
                    //将矩形放置在Grid的第j行。
                    rectangle.SetValue(Grid.ColumnProperty, i);
                    //将矩形放置在Grid的第i列。
                    rectangle.Margin = new Thickness(1);
                    //设置矩形的边距为1个单位，使矩形之间有一些间隔。
                    _rectangle[i, j] = rectangle;
                    //将矩形存储在名为_rectangles的二维数组中，以便后续访问和操作。
                    mapGrid.Children.Add(rectangle);
                    //将矩形添加到Grid控件的子控件集合中，使其在界面上显示。
                }
            }
        }
         
        private void RandomStartEnd() 
        {
             startX = _random.Next(1, 30) - 1;
             startY = _random.Next(1, 30) - 1;
            //map[StartIndex1, StartIndex2].Fill = Brushes.Red;
            // map[0, 0] = -1;
            _rectangle[startX, startY].Fill = Brushes.Red;

             endX = _random.Next(1, 30) - 1;
             endY = _random.Next(1, 30) - 1;
            //map[EndIndex1, EndIndex2].Fill = Brushes.Green;
            _rectangle[endX, endY].Fill = Brushes.Green;
           // map[29, 29] = -1;
           
        }

        private void InitArry() 
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    //map[i][j] = 0;
                    visited[i,j] = -1;
                    map[i, j] = 0;
                }
            }

        }
        private void BFS() 
        {
            queue.Enqueue(new Tuple<int,int>(startX, startY));
            visited[startX, startY] = 0;
            while (queue.Count > 0) 
            {
                Tuple<int, int> tuple = queue.Peek(); //将队列中的首元素赋值给元组tuple
                for (int i = 0; i < 4; i++) 
                {
                    int nex = tuple.Item1 + dx[i];
                    int ney = tuple.Item2 + dy[i];
                    // 尝试将 Fill 属性转换为 SolidColorBrush
                    //SolidColorBrush brush = map[nex, ney].Fill as SolidColorBrush;
                    // if (nex > 0 && nex < 30 && ney > 0 && ney < 30 && visited[nex, ney] == -1 && brush.Color == Colors.Gray)
                    if (nex >= 0 && nex < col && ney >= 0 && ney < row && visited[nex, ney] == -1 && map[nex,ney]==0)
                    { 
                        visited[nex, ney] = visited[tuple.Item1,tuple.Item2] + 1;
                         queue.Enqueue(new Tuple<int, int>(nex,ney));
                        if (nex == endX && ney == endY)
                        {
                            //variable.Text = visited[row - 1, col - 1].ToString();
                            variable.Text = visited[endX, endY].ToString();
                            return;
                        }
                    }
                }
                //_rectangle[tuple.Item1, tuple.Item2].Fill = Brushes.Black;
                queue.Dequeue();
            }
            variable.Text = visited[row-1,col-1].ToString();
        }

        private void DFS() 
        {
            
        }

        private void SetPathColor() 
        {
            int endx = endX, endy = endY;
            for (int val = visited[endX, endY]; val > 0; val--)
            {
               
                for (int i = 0; i < 4; i++)
                {
                    int nextx = endx + dx[i], nexty = endy + dy[i];
                    if (nextx >= 0 & nextx < 30 && nexty >= 0 & nexty < 30) 
                    {
                        if (visited[nextx, nexty] == val)
                        {
                            _rectangle[nextx, nexty].Fill = Brushes.Black;
                            endx = endx + dx[i];
                            endy = endy + dy[i];
                            break;
                        }
                    }
                }
            }
            //pathqueue.Enqueue(new Tuple<int, int>(0, 0));
        }

        private void Createobstacle() 
        {
            for (int i = 3; i < 18; i++)
            {
                _rectangle[i, 5].Fill = Brushes.Gray;
                map[i, 5] = 1;
            }
            for (int i = 14; i < 24; i++)
            {
                _rectangle[i, 14].Fill = Brushes.Gray;
                map[i, 14] = 1;

            }
            for (int i = 12; i < 22; i++)
            {
                _rectangle[5, i].Fill = Brushes.Gray;
                    map[5, i] = 1;
            }
            for (int i = 6; i < 24; i++)
            {
                _rectangle[26, i].Fill = Brushes.Gray;
                    map[26, i] = 1;
            }
            for (int i = 5; i < 16; i++)
            {
                _rectangle[i, 22].Fill = Brushes.Gray;
                map[i, 22]  = 1;
            }

            for (int i = 19; i < 30; i++)
            {
                _rectangle[22, i].Fill = Brushes.Gray;
                map[22, i] = 1;
            }
            for (int i = 27; i < 30; i++)
            {
                _rectangle[i, 6].Fill = Brushes.Gray;
                map[i, 6] = 1;
            }
        }
        

        private void width_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deepth_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}