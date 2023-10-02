using Newtonsoft.Json.Linq;
using System.Diagnostics;
using WebApplicationMVC.Programming;

namespace FramesworkTestProject
{
    [TestClass]
    public class Filter_UnitTests
    {
        [TestMethod]
        public void TestMethod_Filtering_15_Async()
        {
            string[] args = new string[3];
            args[0] = "Dawn";
            args[1] = DataLists.Get_People_10k_JSON();
            args[2] = "2";

            WebApplicationMVC.Programming.Filtering filtering
                = new WebApplicationMVC.Programming.Filtering(args);
            // Create new stopwatch
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing
            stopwatch.Start();

            JArray @string = filtering.GetFilteredObjectsAsync().Result;

            // Stop timing
            stopwatch.Stop();

            // Write result
            Console.WriteLine("Time elapsed (s): {0}", stopwatch.Elapsed.TotalSeconds);
            Console.WriteLine("Time elapsed (ms): {0}", stopwatch.Elapsed.TotalMilliseconds);
            Console.WriteLine("Time elapsed (ns): {0}", stopwatch.Elapsed.TotalMilliseconds * 1000000);
            Assert.IsTrue(@string.Count == 15);  
        }

        [TestMethod]
        public void TestMethod_Filtering_10000_Async()
        {
            string[] args = new string[3];
            args[0] = "Last";
            args[1] = DataLists.Get_People_10k_JSON();
            args[2] = "10";

            WebApplicationMVC.Programming.Filtering filtering
                = new WebApplicationMVC.Programming.Filtering(args);

            // Create new stopwatch
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing
            stopwatch.Start();

            JArray @string = filtering.GetFilteredObjectsAsync().Result;

            // Stop timing
            stopwatch.Stop();

            // Write result
            Console.WriteLine("Time elapsed (s): {0}", stopwatch.Elapsed.TotalSeconds);
            Console.WriteLine("Time elapsed (ms): {0}", stopwatch.Elapsed.TotalMilliseconds);
            Console.WriteLine("Time elapsed (ns): {0}", stopwatch.Elapsed.TotalMilliseconds * 1000000);

            Assert.IsTrue(@string.Count == 10000);
        }

        [TestMethod]
        public void TestMethod_Filtering_0_Async()
        {
            string[] args = new string[3];
            args[0] = "3976ae40-5e16-4d91-83db-25f2a6e8cfef";
            args[1] = DataLists.Get_People_10k_JSON();
            args[2] = "2";

            WebApplicationMVC.Programming.Filtering filtering
                = new WebApplicationMVC.Programming.Filtering(args);

            // Create new stopwatch
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing
            stopwatch.Start();

            JArray @string = filtering.GetFilteredObjectsAsync().Result;

            // Stop timing
            stopwatch.Stop();

            // Write result
            Console.WriteLine("Time elapsed (s): {0}", stopwatch.Elapsed.TotalSeconds);
            Console.WriteLine("Time elapsed (ms): {0}", stopwatch.Elapsed.TotalMilliseconds);
            Console.WriteLine("Time elapsed (ns): {0}", stopwatch.Elapsed.TotalMilliseconds * 1000000);

            Assert.IsTrue(@string.Count == 0);
        }

        [TestMethod]
        public void TestMethod_Filtering_15_ForEach()
        {
            string[] args = new string[3];
            args[0] = "Dawn";
            args[1] = DataLists.Get_People_10k_JSON();
            args[2] = "1";

            WebApplicationMVC.Programming.Filtering filtering
                = new WebApplicationMVC.Programming.Filtering(args);
            // Create new stopwatch
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing
            stopwatch.Start();

            JArray @string = filtering.GetFilteredObjectsForEach();

            // Stop timing
            stopwatch.Stop();

            // Write result
            Console.WriteLine("Time elapsed (s): {0}", stopwatch.Elapsed.TotalSeconds);
            Console.WriteLine("Time elapsed (ms): {0}", stopwatch.Elapsed.TotalMilliseconds);
            Console.WriteLine("Time elapsed (ns): {0}", stopwatch.Elapsed.TotalMilliseconds * 1000000);
            Assert.IsTrue(@string.Count == 15);
        }

        [TestMethod]
        public void TestMethod_Filtering_10000_ForEach()
        {
            string[] args = new string[3];
            args[0] = "Last";
            args[1] = DataLists.Get_People_10k_JSON();
            args[2] = "10";

            WebApplicationMVC.Programming.Filtering filtering
                = new WebApplicationMVC.Programming.Filtering(args);

            // Create new stopwatch
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing
            stopwatch.Start();

            JArray @string = filtering.GetFilteredObjectsForEach();

            // Stop timing
            stopwatch.Stop();

            // Write result
            Console.WriteLine("Time elapsed (s): {0}", stopwatch.Elapsed.TotalSeconds);
            Console.WriteLine("Time elapsed (ms): {0}", stopwatch.Elapsed.TotalMilliseconds);
            Console.WriteLine("Time elapsed (ns): {0}", stopwatch.Elapsed.TotalMilliseconds * 1000000);

            Assert.IsTrue(@string.Count == 10000);
        }

        [TestMethod]
        public void TestMethod_Filtering_0_ForEach()
        {
            string[] args = new string[3];
            args[0] = "3976ae40-5e16-4d91-83db-25f2a6e8cfef";
            args[1] = DataLists.Get_People_10k_JSON();
            args[2] = "10";

            WebApplicationMVC.Programming.Filtering filtering
                = new WebApplicationMVC.Programming.Filtering(args);

            // Create new stopwatch
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing
            stopwatch.Start();

            JArray @string = filtering.GetFilteredObjectsForEach();

            // Stop timing
            stopwatch.Stop();

            // Write result
            Console.WriteLine("Time elapsed (s): {0}", stopwatch.Elapsed.TotalSeconds);
            Console.WriteLine("Time elapsed (ms): {0}", stopwatch.Elapsed.TotalMilliseconds);
            Console.WriteLine("Time elapsed (ns): {0}", stopwatch.Elapsed.TotalMilliseconds * 1000000);

            Assert.IsTrue(@string.Count == 0);
        }


        [TestMethod]
        public void TestMethod_Filtering_15_For()
        {
            string[] args = new string[3];
            args[0] = "Dawn";
            args[1] = DataLists.Get_People_10k_JSON();
            args[2] = "1";

            WebApplicationMVC.Programming.Filtering filtering
                = new WebApplicationMVC.Programming.Filtering(args);
            // Create new stopwatch
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing
            stopwatch.Start();

            JArray @string = filtering.GetFilteredObjectsFor();

            // Stop timing
            stopwatch.Stop();

            // Write result
            Console.WriteLine("Time elapsed (s): {0}", stopwatch.Elapsed.TotalSeconds);
            Console.WriteLine("Time elapsed (ms): {0}", stopwatch.Elapsed.TotalMilliseconds);
            Console.WriteLine("Time elapsed (ns): {0}", stopwatch.Elapsed.TotalMilliseconds * 1000000);
            Assert.IsTrue(@string.Count == 15);
        }

        [TestMethod]
        public void TestMethod_Filtering_10000_For()
        {
            string[] args = new string[3];
            args[0] = "Last";
            args[1] = DataLists.Get_People_10k_JSON();
            args[2] = "10";

            WebApplicationMVC.Programming.Filtering filtering
                = new WebApplicationMVC.Programming.Filtering(args);

            // Create new stopwatch
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing
            stopwatch.Start();

            JArray @string = filtering.GetFilteredObjectsFor();

            // Stop timing
            stopwatch.Stop();

            // Write result
            Console.WriteLine("Time elapsed (s): {0}", stopwatch.Elapsed.TotalSeconds);
            Console.WriteLine("Time elapsed (ms): {0}", stopwatch.Elapsed.TotalMilliseconds);
            Console.WriteLine("Time elapsed (ns): {0}", stopwatch.Elapsed.TotalMilliseconds * 1000000);

            Assert.IsTrue(@string.Count == 10000);
        }

        [TestMethod]
        public void TestMethod_Filtering_0_For()
        {
            string[] args = new string[3];
            args[0] = "3976ae40-5e16-4d91-83db-25f2a6e8cfef";
            args[1] = DataLists.Get_People_10k_JSON();
            args[2] = "10";

            WebApplicationMVC.Programming.Filtering filtering
                = new WebApplicationMVC.Programming.Filtering(args);

            // Create new stopwatch
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing
            stopwatch.Start();

            JArray @string = filtering.GetFilteredObjectsFor();

            // Stop timing
            stopwatch.Stop();

            // Write result
            Console.WriteLine("Time elapsed (s): {0}", stopwatch.Elapsed.TotalSeconds);
            Console.WriteLine("Time elapsed (ms): {0}", stopwatch.Elapsed.TotalMilliseconds);
            Console.WriteLine("Time elapsed (ns): {0}", stopwatch.Elapsed.TotalMilliseconds * 1000000);

            Assert.IsTrue(@string.Count == 0);
        }

        [TestMethod]
        public void TestMethod_Filtering_15_Parallel()
        {
            string[] args = new string[3];
            args[0] = "Dawn";
            args[1] = DataLists.Get_People_10k_JSON();
            args[2] = "1";

            WebApplicationMVC.Programming.Filtering filtering
                = new WebApplicationMVC.Programming.Filtering(args);
            // Create new stopwatch
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing
            stopwatch.Start();

            JArray @string = filtering.GetFilteredObjectsParallel();

            // Stop timing
            stopwatch.Stop();

            // Write result
            Console.WriteLine("Time elapsed (s): {0}", stopwatch.Elapsed.TotalSeconds);
            Console.WriteLine("Time elapsed (ms): {0}", stopwatch.Elapsed.TotalMilliseconds);
            Console.WriteLine("Time elapsed (ns): {0}", stopwatch.Elapsed.TotalMilliseconds * 1000000);
            Assert.IsTrue(@string.Count == 15);
        }

        [TestMethod]
        public void TestMethod_Filtering_10000_Parallel()
        {
            string[] args = new string[3];
            args[0] = "Last";
            args[1] = DataLists.Get_People_10k_JSON();
            args[2] = "10";

            WebApplicationMVC.Programming.Filtering filtering
                = new WebApplicationMVC.Programming.Filtering(args);

            // Create new stopwatch
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing
            stopwatch.Start();

            JArray @string = filtering.GetFilteredObjectsParallel();

            // Stop timing
            stopwatch.Stop();

            // Write result
            Console.WriteLine("Time elapsed (s): {0}", stopwatch.Elapsed.TotalSeconds);
            Console.WriteLine("Time elapsed (ms): {0}", stopwatch.Elapsed.TotalMilliseconds);
            Console.WriteLine("Time elapsed (ns): {0}", stopwatch.Elapsed.TotalMilliseconds * 1000000);

            Assert.IsTrue(@string.Count == 10000);
        }

        [TestMethod]
        public void TestMethod_Filtering_0_Parallel()
        {
            string[] args = new string[3];
            args[0] = "3976ae40-5e16-4d91-83db-25f2a6e8cfef";
            args[1] = DataLists.Get_People_10k_JSON();
            args[2] = "10";

            WebApplicationMVC.Programming.Filtering filtering
                = new WebApplicationMVC.Programming.Filtering(args);

            // Create new stopwatch
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing
            stopwatch.Start();

            JArray @string = filtering.GetFilteredObjectsParallel();

            // Stop timing
            stopwatch.Stop();

            // Write result
            Console.WriteLine("Time elapsed (s): {0}", stopwatch.Elapsed.TotalSeconds);
            Console.WriteLine("Time elapsed (ms): {0}", stopwatch.Elapsed.TotalMilliseconds);
            Console.WriteLine("Time elapsed (ns): {0}", stopwatch.Elapsed.TotalMilliseconds * 1000000);

            Assert.IsTrue(@string.Count == 0);
        }

        [TestMethod]
        public void TestMethod_Filtering_0_Comparison()
        {
            string[] args = new string[3];
            args[0] = "3976ae40-5e16-4d91-83db-25f2a6e8cfef";
            args[1] = DataLists.Get_People_10k_JSON();
            args[2] = "2";

            WebApplicationMVC.Programming.Filtering filtering
                = new WebApplicationMVC.Programming.Filtering(args);

            List<KeyValuePair<string,double>> elapsedAsync = new List<KeyValuePair<string,double>>();
            List<KeyValuePair<string, double>> elapsedForEach = new List<KeyValuePair<string, double>>();
            List<KeyValuePair<string, double>> elapsedFor = new List<KeyValuePair<string, double>>();
            List<KeyValuePair<string, double>> elapsedAsParallel = new List<KeyValuePair<string, double>>();

            JArray @string;
            // Create new stopwatch
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing
            stopwatch.Start();

            @string = filtering.GetFilteredObjectsForEach();

            // Stop timing
            stopwatch.Stop();

            // Write result
            elapsedAsync.Add(new KeyValuePair<string, double>
                ("ForEach", stopwatch.Elapsed.TotalMilliseconds));

            //clear stopwatch
            stopwatch.Restart();

            @string = filtering.GetFilteredObjectsFor();

            // Stop timing
            stopwatch.Stop();

            // Write result
            elapsedAsync.Add(new KeyValuePair<string, double>
                ("For", stopwatch.Elapsed.TotalMilliseconds));

            //clear stopwatch
            stopwatch.Restart();

            @string = filtering.GetFilteredObjectsParallel();

            // Stop timing
            stopwatch.Stop();

            // Write result
            elapsedAsync.Add(new KeyValuePair<string, double>
                ("AsParallel", stopwatch.Elapsed.TotalMilliseconds));

            //clear stopwatch
            stopwatch.Restart();

            @string = filtering.GetFilteredObjectsAsync().Result;

            // Stop timing
            stopwatch.Stop();

            // Write result
            elapsedAsync.Add(new KeyValuePair<string, double>
                ("Async", stopwatch.Elapsed.TotalMilliseconds));

            Assert.IsTrue(@string.Count == 0);
        }

        [TestMethod]
        public void TestMethod_Filtering_15_Comparison()
        {
            string[] args = new string[3];
            args[0] = "Dawn";
            args[1] = DataLists.Get_People_10k_JSON();
            args[2] = "1";

            WebApplicationMVC.Programming.Filtering filtering
                = new WebApplicationMVC.Programming.Filtering(args);

            List<KeyValuePair<string, double>> elapsedAsync = new List<KeyValuePair<string, double>>();
            List<KeyValuePair<string, double>> elapsedForEach = new List<KeyValuePair<string, double>>();
            List<KeyValuePair<string, double>> elapsedFor = new List<KeyValuePair<string, double>>();
            List<KeyValuePair<string, double>> elapsedAsParallel = new List<KeyValuePair<string, double>>();

            JArray @string;
            // Create new stopwatch
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing
            stopwatch.Start();

            @string = filtering.GetFilteredObjectsForEach();

            // Stop timing
            stopwatch.Stop();

            //clear stopwatch
            stopwatch.Restart();

            @string = filtering.GetFilteredObjectsAsync().Result;

            // Stop timing
            stopwatch.Stop();

            // Write result
            elapsedAsync.Add(new KeyValuePair<string, double>
                ("Async", stopwatch.Elapsed.TotalMilliseconds));


            // Write result
            elapsedAsync.Add(new KeyValuePair<string, double>
                ("ForEach", stopwatch.Elapsed.TotalMilliseconds));

            //clear stopwatch
            stopwatch.Restart();

            @string = filtering.GetFilteredObjectsFor();

            // Stop timing
            stopwatch.Stop();

            // Write result
            elapsedAsync.Add(new KeyValuePair<string, double>
                ("For", stopwatch.Elapsed.TotalMilliseconds));

            //clear stopwatch
            stopwatch.Restart();

            @string = filtering.GetFilteredObjectsParallel();

            // Stop timing
            stopwatch.Stop();

            // Write result
            elapsedAsync.Add(new KeyValuePair<string, double>
                ("AsParallel", stopwatch.Elapsed.TotalMilliseconds));

            Assert.IsTrue(@string.Count == 60);
        }

        [TestMethod]
        public void TestMethod_Filtering_10000_Comparison()
        {
            string[] args = new string[3];
            args[0] = "Last";
            args[1] = DataLists.Get_People_10k_JSON();
            args[2] = "2";

            WebApplicationMVC.Programming.Filtering filtering
                = new WebApplicationMVC.Programming.Filtering(args);

            List<KeyValuePair<string, double>> elapsedAsync = new List<KeyValuePair<string, double>>();
            List<KeyValuePair<string, double>> elapsedForEach = new List<KeyValuePair<string, double>>();
            List<KeyValuePair<string, double>> elapsedFor = new List<KeyValuePair<string, double>>();
            List<KeyValuePair<string, double>> elapsedAsParallel = new List<KeyValuePair<string, double>>();

            JArray @string;
            // Create new stopwatch
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing
            stopwatch.Start();

            @string = filtering.GetFilteredObjectsForEach();

            // Stop timing
            stopwatch.Stop();

            // Write result
            elapsedAsync.Add(new KeyValuePair<string, double>
                ("ForEach", stopwatch.Elapsed.TotalMilliseconds));

            //clear stopwatch
            stopwatch.Restart();

            @string = filtering.GetFilteredObjectsFor();

            // Stop timing
            stopwatch.Stop();

            // Write result
            elapsedAsync.Add(new KeyValuePair<string, double>
                ("For", stopwatch.Elapsed.TotalMilliseconds));

            //clear stopwatch
            stopwatch.Restart();

            @string = filtering.GetFilteredObjectsParallel();

            // Stop timing
            stopwatch.Stop();

            // Write result
            elapsedAsync.Add(new KeyValuePair<string, double>
                ("AsParallel", stopwatch.Elapsed.TotalMilliseconds));

            //clear stopwatch
            stopwatch.Restart();

            @string = filtering.GetFilteredObjectsAsync().Result;

            // Stop timing
            stopwatch.Stop();

            // Write result
            elapsedAsync.Add(new KeyValuePair<string, double>
                ("Async", stopwatch.Elapsed.TotalMilliseconds));

            Assert.IsTrue(@string.Count == 40000);
        }
    }
}