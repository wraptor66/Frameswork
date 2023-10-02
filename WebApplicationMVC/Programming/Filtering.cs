using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;

namespace WebApplicationMVC.Programming
{
    public class Filtering
    {

        string Filter;
        string JsonString;
        JArray FilterList;
        JArray FilteredList;
        int Partitions;
        int[] PartitionsCompleted;
        int PartitionsCompletedCount;
        public Filtering(string[] strings)
        {
            Filter = strings[0];
            JsonString = strings[1];
            FilterList = JArray.Parse(JsonString);
            FilteredList = new JArray();
            Partitions = Convert.ToInt32(strings[2]);
        }

        ///https://dotnettips.wordpress.com/2019/07/08/performance-tip-for-vs-foreach-in-microsoft-net/

        public async Task<JArray> GetFilteredObjectsAsync()
        {
            string string2Parse = string.Empty;
            Task[] tasks = new Task[Partitions];
            int i = 0;
            foreach (Task task in tasks)
            {
                await (tasks[i] = Task.Run(async () =>
                {
                    await FilterCollection(i);
                    i++;
                })).ConfigureAwait(false);
            }
            await Task.WhenAll(tasks);

            return FilteredList;
        }

        private async Task FilterCollection(int iPartition)
        {
            if (iPartition == Partitions)
            {
                return;
            }
            int filterListCount = FilterList.Count;
            int partitionObjectCount = (filterListCount / Partitions);
            int iStart = (partitionObjectCount * iPartition);
            int iEnd = (iStart + partitionObjectCount);
            await Task.Run(() =>
            {
                for (int i = iStart; i < iEnd; i++)
                {
                    var String2Parse = FilterList[i].ToString();
                    if (String2Parse.IndexOf(Filter) > -1)
                    {
                        FilteredList.Add(FilterList[i]);
                    }
                }
            });


        }

        public JArray GetFilteredObjectsForEach()
        {
            string string2Parse = string.Empty;
            foreach (var ea in FilterList)
            {
                var String2Parse = ea.ToString();
                if (String2Parse.IndexOf(Filter) > -1)
                {
                    FilteredList.Add(ea);
                }
            }
            return FilteredList;
        }

        public JArray GetFilteredObjectsFor()
        {
            for (int i = 0; i < FilterList.Count; i++)
            {
                var String2Parse = FilterList[i].ToString();
                if (String2Parse.IndexOf(Filter) > -1)
                {
                    FilteredList.Add(FilterList[i]);
                }
            }
            return FilteredList;
        }

        public JArray GetFilteredObjectsParallel()
        {
            string string2Parse = string.Empty;
            foreach (var ea in FilterList.AsParallel())
            {
                var String2Parse = ea.ToString();
                if (String2Parse.IndexOf(Filter) > -1)
                {
                    FilteredList.Add(ea);
                }
            }
            return FilteredList;

        }
    }
}
