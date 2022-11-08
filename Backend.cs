namespace MyApp;

public class Backend
{
    /**
     * Failed on 56'th test with wrong answer xd
Postprocessor output
stdout:
50


stderr:
samples: 0 points 

group 1: 15 points 

group 2: 35 points 

group 3: 0 points 

total: 50 points

     */
    static void A(string[] args)
    {
        var input = File.ReadAllLines("input.txt");

        var config = input[0].Split(' ');

        var n = int.Parse(config[0]);
        var m = int.Parse(config[1]);
        var q = int.Parse(config[2]);


        var datacenterInfo = new Dictionary<int, DatacenterInfo>();
        for (var i = 1; i < n + 1; i++)
        {
            datacenterInfo[i] = new DatacenterInfo();
        }

        int maxD = 1, minD = 1;

        /**
         * n - # of datacenters
         * m - # of servers in each datacenter (same)
         * q - # of logs
         * 
         * i - datacenter id
         * j - server id
         */
        for (var line = 1; line < q + 1; line++)
        {
            var logEntry = input[line].Split(' ');
            var i = 0;
            var j = 0;

            switch (logEntry[0])
            {
                case "DISABLE":
                    i = int.Parse(logEntry[1]);
                    j = int.Parse(logEntry[2]);

                    datacenterInfo[i].DisabledServers.Add(j);
                    datacenterInfo[i].UpdateRa(m);

                    if (i == maxD)
                    {
                        maxD = -1;
                    }

                    if (minD != -1)
                    {
                        if (datacenterInfo[i].ra > datacenterInfo[minD].ra) continue;
                        if (datacenterInfo[i].ra == datacenterInfo[minD].ra && i > minD) continue;

                        minD = i;
                    }

                    break;
                case "RESET":
                    i = int.Parse(logEntry[1]);

                    datacenterInfo[i].DisabledServers.Clear();
                    datacenterInfo[i].RestartCount++;
                    datacenterInfo[i].UpdateRa(m);

                    if (i == minD)
                    {
                        minD = -1;
                    }

                    if (maxD != -1)
                    {
                        if (datacenterInfo[i].ra < datacenterInfo[maxD].ra) continue;
                        if (datacenterInfo[i].ra == datacenterInfo[maxD].ra && i > maxD) continue;

                        maxD = i;
                    }

                    break;
                case "GETMAX":
                    if (maxD != -1)
                    {
                        Console.WriteLine(maxD);
                        continue;
                    }

                    var max = datacenterInfo
                        .OrderByDescending(v => v.Value.ra)
                        .ThenBy(v => v.Key)
                        .First();
                    maxD = max.Key;

                    Console.WriteLine(maxD);
                    break;
                case "GETMIN":
                    if (minD != -1)
                    {
                        Console.WriteLine(minD);
                        continue;
                    }

                    var min = datacenterInfo
                        .OrderBy(v => v.Value.ra)
                        .ThenBy(v => v.Key)
                        .First();
                    minD = min.Key;

                    Console.WriteLine(minD);
                    break;
                default:
                    // ignore?
                    break;
            }
        }
    }
    
    class DatacenterInfo
    {
        public HashSet<int> DisabledServers { get; } = new HashSet<int>();
        public int RestartCount { get; set; }
        public int ra { get; private set; }

        public void UpdateRa(int m)
        {
            ra = RestartCount * (m - DisabledServers.Count);
        }
    }
}