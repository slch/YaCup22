namespace MyApp;

public class Algo
{
    /**
Problem: A.Checkered board
Compiler: C# (MS .Net 6.0)+ASP
Verdict: OK
Status: Full solution
     */
    static void A(string[] args)
    {
        /*
        var n = int.Parse(args[0]);
        var k = int.Parse(args[1]);
        */
        var input = File
            .ReadAllLines("input.txt")[0]
            .Split(' ');
        var n = int.Parse(input[0]);
        var k = int.Parse(input[1]);

        if ((k == 1 && n > 1) || ((n * n) % k != 0))
        {
            Console.WriteLine("No");
            return;
        }

        var pool = new int[k];
        Array.Fill(pool, (n * n) / k);

        var result = new int[n][];
        var color = 0;
        for (var i = 0; i < n; i++)
        {
            result[i] = new int[n];

            for (var j = 0; j < n; j++)
            {
                var tries = -1;
                while (tries <= k)
                {
                    tries++;

                    color++;
                    if (color > k)
                    {
                        color = 1;
                    }

                    if ((i == 0 || result[i - 1][j] != color) && (j == 0 || result[i][j - 1] != color) &&
                        pool[color - 1] > 0)
                    {
                        break;
                    }
                }

                if (tries > k)
                {
                    Console.WriteLine("No");
                    return;
                }

                pool[color - 1]--;
                result[i][j] = color;
            }
        }

        Console.WriteLine("Yes");
        foreach (var row in result)
        {
            foreach (var elm in row)
            {
                Console.Write(elm);
                Console.Write(' ');
            }

            Console.WriteLine();
        }
    }


    // unfinished
    static void B(string[] args)
    {
        var input = File.ReadAllLines("input.txt");

        var nums = new int[]
        {
            0,
            5, // 2
            4, // 3
            3, // 4
            2, // 5
            2, // 6
            2, // 7
            2, // 8
            2, // 9
        };

        var t = int.Parse(input[0]);
        for (var i = 1; i < t; i++)
        {
            var line = input[i].Split(' ');
            var n = long.Parse(line[0]);
            var b = int.Parse(line[1]);

            if (b == 1)
            {
                Console.WriteLine(n);
            }

            var result = 0;
            var tens = 1;
            if (n % 10 > nums[b])
            {
                result += nums[b] - 1;
            }

            for (int j = 0; n > 0; j++)
            {
                var xd = n % 10;
                n /= 10;
                tens *= nums[b - 1];

                result += (nums[b - 1] - 1);
            }

            Console.WriteLine(n);
        }
    }
}