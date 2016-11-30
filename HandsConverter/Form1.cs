using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CodedUI_Temp.ATProMethods;
using HandsConverter.Converters;
using System.Diagnostics;
using System.Threading;

namespace HandsConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public long filesCount = 0;
        public long skippedFilesCount = 0;
        public long convertedFilesCount = 0;

        private void BrowseBtn_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                SourceFilePathTxb.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private static List<Hand> SplitHands(List<string> allLines)
        {
            var hands = new List<Hand>();
            var lines = new List<string>();
            int newLineCount = 0;

            foreach (var line in allLines)
            {
                if (newLineCount == 3)
                {
                    hands.Add(new Hand(lines));
                    lines = new List<string>();
                }
                if (line == "")
                    newLineCount++;
                else
                {
                    newLineCount = 0;
                    lines.Add(line);
                }
            }
            if (newLineCount == 3)
                hands.Add(new Hand(lines));
            return hands;
        }


        private void ConvertBtn_Click(object sender, EventArgs e)
        {
            //backgroundWorker1.RunWorkerAsync();
            
            var timer = Stopwatch.StartNew();
            MULTI();
            timer.Stop();
            ConvertProgressBar.Value = 100*((int)skippedFilesCount + (int)convertedFilesCount)/(int)filesCount;
            ProgressbarLbl.Text = ConvertProgressBar.Value.ToString();
            label1.Text = timer.Elapsed.ToString("g")+". " + String.Format(" Converted: {0}; Skipped {1}  of " + filesCount, convertedFilesCount, skippedFilesCount); 
        }


        private string ConvertFileName(string fileName)
        {
            var pattern = @"HH(?<fileName>.*)";
            var match = new Regex(pattern).Match(fileName);
            var tournamentNumber = match.Groups["fileName"].Value;
            return String.Format("CNV({0})", tournamentNumber);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void BrowseFileBtn_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = @"HH (*.txt) | *.txt";
            openFileDialog.InitialDirectory = @"C:\";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SourceFilePathTxb.Text = openFileDialog.FileName;
            }
        }

        private void BrowseFolderOutBtn_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = true;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                OutputFolderPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                backgroundWorker1.ReportProgress(0);
                List<FileInfo> files = new List<FileInfo>();
                if (SourceFilePathTxb.Text.EndsWith("txt"))
                {
                    files.Add(new FileInfo(SourceFilePathTxb.Text));
                }
                else
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(SourceFilePathTxb.Text);
                    files =
                        dirInfo.GetFiles("*.txt", SearchOption.AllDirectories)
                            .Where(f => f.Name.StartsWith("HH"))
                            .ToList();
                }
                filesCount = files.Count;
                double percent = 0;
                
                foreach (var file in files)
                {
                    if (backgroundWorker1.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                    var outputFileName = new DirectoryInfo(OutputFolderPath.Text).FullName + @"\" +
                                         ConvertFileName(file.Name) + ".txt";

                    var hands = SplitHands(File.ReadAllLines(file.FullName).Select(l => l.Trim()).ToList());

                    using (
                        StreamWriter sw =
                            new StreamWriter(new FileStream(outputFileName, FileMode.Create, FileAccess.Write)))
                    {
                        foreach (var hand in hands)
                        {
                            var handParty = hand.ToParty();
                            if (handParty != null)
                                foreach (var line in handParty)
                                {
                                    sw.WriteLine(line);
                                    sw.Flush();
                                }
                        }
                    }
                    var convertedFile = new FileInfo(outputFileName);
                    if (convertedFile.Length == 0)
                    {
                        convertedFile.Delete();
                        skippedFilesCount++;
                    }
                    else
                        convertedFilesCount++;
                    percent = 100 * ((double)skippedFilesCount + (double)convertedFilesCount) / (double)filesCount;
                    backgroundWorker1.ReportProgress((int)percent);
                }


                e.Result = String.Format("Successfully converted {0} of {1} files", convertedFilesCount, filesCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void backgroundWorker1_RunWorkerCompleted(object sender,
            System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("Error: " + e.Error.Message.ToString());
            }
            if (e.Cancelled)
                MessageBox.Show("User cancel operation");
            else
                MessageBox.Show(e.Result.ToString());
            ConvertProgressBar.Value = 0;
            ProgressbarLbl.Text = "0%";
            ConvertBtn.Enabled = true;
        }
        
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                ConvertBtn.Enabled = false;
            }
            ConvertProgressBar.Value = e.ProgressPercentage;
            ProgressbarLbl.Text = e.ProgressPercentage.ToString() + "%";
            label1.Text = String.Format("Converted: {0}; Skipped {1}  of " + filesCount, convertedFilesCount,
                skippedFilesCount);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var timer = Stopwatch.StartNew();
            MainProcess();
            timer.Stop();
            label1.Text = timer.ElapsedMilliseconds.ToString()+ String.Format(" Converted: {0}; Skipped {1}  of " + filesCount, convertedFilesCount, skippedFilesCount);
        }


        public void MainProcess()
        {
            List<FileInfo> files = new List<FileInfo>();
            if (SourceFilePathTxb.Text.EndsWith("txt"))
            {
                files.Add(new FileInfo(SourceFilePathTxb.Text));
            }
            else
            {
                DirectoryInfo dirInfo = new DirectoryInfo(SourceFilePathTxb.Text);
                files =
                    dirInfo.GetFiles("*.txt", SearchOption.AllDirectories)
                        .Where(f => f.Name.StartsWith("HH"))
                        .ToList();
            }
            filesCount = files.Count;
            label1.Text = "0 files of " + filesCount.ToString();
            skippedFilesCount = 0;
            convertedFilesCount = 0;
            double percent = 0;

            foreach (var file in files)
            {
                var outputFileName = new DirectoryInfo(OutputFolderPath.Text).FullName + @"\" +
                                     ConvertFileName(file.Name) + ".txt";

                var hands = SplitHands(File.ReadAllLines(file.FullName).Select(l => l.Trim()).ToList());

                using (
                    StreamWriter sw =
                        new StreamWriter(new FileStream(outputFileName, FileMode.Create, FileAccess.Write)))
                {
                    foreach (var hand in hands)
                    {
                        var handParty = hand.ToParty();
                        if (handParty != null)
                            foreach (var line in handParty)
                            {
                                sw.WriteLine(line);
                                sw.Flush();
                            }
                    }
                }
                var convertedFile = new FileInfo(outputFileName);
                if (convertedFile.Length == 0)
                {
                    convertedFile.Delete();
                    skippedFilesCount++;
                }
                else
                    convertedFilesCount++;
                //label1.Text = String.Format("Converted: {0}; Skipped {1}  of " + filesCount.ToString(), convertedFilesCount, skippedFilesCount);
            }
        }





        //http://professorweb.ru/my/csharp/optimization/level4/4_2.php
        public void MULTI()
        {
            List<FileInfo> files = new List<FileInfo>();
            if (SourceFilePathTxb.Text.EndsWith("txt"))
            {
                files.Add(new FileInfo(SourceFilePathTxb.Text));
            }
            else
            {
                DirectoryInfo dirInfo = new DirectoryInfo(SourceFilePathTxb.Text);
                files =
                    dirInfo.GetFiles("*.txt", SearchOption.AllDirectories)
                        .Where(f => f.Name.StartsWith("HH"))
                        .ToList();
            }
            var outputDir = new DirectoryInfo(OutputFolderPath.Text);
            MultiThreadConvert(files, outputDir);
        }


        /// <summary>
        /// using POOL
        /// </summary>
        /// <param name="files"></param>
        /// <param name="outputDir"></param>
        public void MultiPoolConvert(List<FileInfo> files, DirectoryInfo outputDir)
        {
            uint iteration = 6;//threads count
            uint filesCountForOneThread = (uint)filesCount / iteration;
            ManualResetEvent allDone = new ManualResetEvent(initialState: false);
            filesCount = files.Count;
            long completed = 0;

            for (uint i = 0; i < iteration; i++) 
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object s)
                {
                    var startI = i * filesCountForOneThread;
                    var endI = startI + filesCountForOneThread;
                    if (i >= iteration - 1)
                        endI = (uint)filesCount;

                    for (uint j = (uint)startI; j < endI; j++)
                    {
                        var file = files[(int)j];
                        var outputFileName = outputDir.FullName + @"\" + ConvertFileName(file.Name) + ".txt";

                        var hands = SplitHands(File.ReadAllLines(file.FullName).Select(l => l.Trim()).ToList());

                        using (
                            StreamWriter sw =
                                new StreamWriter(new FileStream(outputFileName, FileMode.Create, FileAccess.Write)))
                        {
                            foreach (var hand in hands)
                            {
                                var handParty = hand.ToParty();
                                if (handParty != null)
                                    foreach (var line in handParty)
                                    {
                                        sw.WriteLine(line);
                                        sw.Flush();
                                    }
                            }
                        }

                        var convertedFile = new FileInfo(outputFileName);
                        if (convertedFile.Length == 0)
                        {
                            convertedFile.Delete();
                            skippedFilesCount++;
                        }
                        else
                            convertedFilesCount++;
                    }
                    if (Interlocked.Increment(ref completed) == iteration)
                    {
                        allDone.Set();
                    }
                }));
                Thread.Sleep(200);
            }
            allDone.WaitOne();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            //var timer = Stopwatch.StartNew();
            MULTI();
            //timer.Stop();
            label1.Text = String.Format(" Converted: {0}; Skipped {1}  of " + filesCount, convertedFilesCount, skippedFilesCount);;
        }


        public void MultiThreadConvert(List<FileInfo> files, DirectoryInfo outputDir)
        {
            filesCount = files.Count;
            uint numThreads = 8;// (uint)Environment.ProcessorCount;
            uint chunk = (uint)filesCount/numThreads;

            Thread[] threads = new Thread[numThreads];
            for (uint i = 0; i < numThreads; ++i)
            {
                uint chunkStart = i * chunk;
                uint chunkEnd = chunkStart + chunk;
                if (i == numThreads - 1) // the latest thread should work with all rest files
                {
                    chunkEnd = (uint)filesCount;
                }
                
                threads[i] = new Thread(() =>
                {
                    for (uint number = chunkStart; number < chunkEnd; number++)
                    {
                        var file = files[(int)number];
                        var outputFileName = outputDir.FullName + @"\" + ConvertFileName(file.Name) + ".txt";

                        var hands = SplitHands(File.ReadAllLines(file.FullName).Select(l => l.Trim()).ToList());
                        
                        using (
                            StreamWriter sw =
                                new StreamWriter(new FileStream(outputFileName, FileMode.Create, FileAccess.Write,FileShare.Write)))
                        {
                            foreach (var hand in hands)
                            {
                                var handParty = hand.ToParty();
                                if (handParty != null)
                                    foreach (var line in handParty)
                                    {
                                        sw.WriteLine(line);
                                        sw.Flush();
                                    }
                            }
                        }

                        var convertedFile = new FileInfo(outputFileName);
                        if (convertedFile.Length == 0)
                        {
                            convertedFile.Delete();
                            skippedFilesCount++;
                        }
                        else
                            convertedFilesCount++;
                    }
                });
                threads[i].IsBackground = true;
                //threads[i].Start();
            }

            threads.ToList().ForEach(t=>t.Start());

            while (threads.Any(t=>t.IsAlive))
            {
                var percent = 100 * (skippedFilesCount + convertedFilesCount) / filesCount;
                ConvertProgressBar.Value = (int)percent;
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }
        }
        public delegate void UpdateTextCallback(string text);

        private void UpdateText(string text)
        {
            label1.Text = text;
        }
    }
}