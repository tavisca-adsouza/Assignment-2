using System;
using System.Collections.Generic;

namespace final
{

    class Program
    {
        static string convertToString(double number){
            string result = Convert.ToString(number);
            if(result.Length == 1){
                result = "0" + result;
            }
            return result;
        }
        static void Main(string[] args)
        {
            Test(new[] {"12:12:12"}, new [] { "few seconds ago" }, "12:12:12");
            Test(new[] { "23:23:23", "23:23:23" }, new[] { "59 minutes ago", "59 minutes ago" }, "00:22:23");
            Test(new[] { "00:10:10", "00:10:10" }, new[] { "59 minutes ago", "1 hours ago" }, "impossible");
            Test(new[] { "11:59:13", "11:13:23", "12:25:15" }, new[] { "few seconds ago", "46 minutes ago", "23 hours ago" }, "11:59:23");
            //Console.ReadKey(true);
        }

        private static void Test(string[] postTimes, string[] showTimes, string expected)
        {
            var result = GetCurrentTime(postTimes, showTimes).Equals(expected) ? "PASS" : "FAIL";
            var postTimesCsv = string.Join(", ", postTimes);
            var showTimesCsv = string.Join(", ", showTimes);
            Console.WriteLine($"[{postTimesCsv}], [{showTimesCsv}] => {result}");
        }

        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {

            Dictionary<string, string> getShowPostTime = new Dictionary<string, string>();

            
            int numberOfPosts = exactPostTime.Length;
            string result = "";

            for (int i = 0; i < numberOfPosts; i++)
            {
                string key = exactPostTime[i];
                string value = showPostTime[i];
                if (getShowPostTime.ContainsKey(key))
                {
                    if(getShowPostTime[key] != value){
                        System.Console.WriteLine("The answer is impossible, line 49 ");
                        return "impossible";
                        
                    }
                }
                else
                {
                    getShowPostTime.Add(key, value);
                }
            }

            string[] resultArray = new string [numberOfPosts];
            double[] resultInNumbers = new double[numberOfPosts];

            for(int i=0; i<numberOfPosts; i++){
                if(showPostTime[i].Contains("seconds")){
                    string[] tokens = exactPostTime[i].Split(":");
                    string value = tokens[0] + tokens[1] + tokens[2];
                    double finalValue = Convert.ToDouble(value);
                    resultInNumbers[i] = finalValue; 

                    result = exactPostTime[i];
                    resultArray[i] = result;
                    
                }
                else if(showPostTime[i].Contains("minutes")){
                    string[] tokens = exactPostTime[i].Split(":");
                    string value = tokens[0] + tokens[1] + tokens[2];
                    double finalValue = Convert.ToDouble(value);
                    resultInNumbers[i] = finalValue; 

                    double hour = Convert.ToDouble(tokens[0]);
                    double minute = Convert.ToDouble(tokens[1]);
                    double seconds = Convert.ToDouble(tokens[2]);

                    //fix this:
                    

                    string[] gapTime = showPostTime[i].Split(" ");
                    double minuteGap = Convert.ToDouble(gapTime[0]);

                    
                    //var minuteGap = Convert.ToDouble(gapTime[0]);
                    
                

                    // add the minutes
                    double currentMinute = minute + minuteGap;
                    double currentHour = hour;
                    double currentSecond = seconds;

                    if(currentMinute >= 60){
                        currentHour = currentHour + 1;
                        if(currentHour >= 24)
                            currentHour = 0;
                        currentMinute = currentMinute%60;
                    } 
                    string currentMinuteStr = Program.convertToString(currentMinute);
                    string currentHourStr = Program.convertToString(currentHour);
                    string currentSecondStr = Program.convertToString(currentSecond);

                    result = currentHourStr + ":" + currentMinuteStr + ":" + currentSecondStr;
                    resultArray[i] = result;

                    
                    
                }
                else if(showPostTime[i].Contains("hours")){
                    
                    string[] tokens = exactPostTime[i].Split(":");
                    string value = tokens[0] + tokens[1] + tokens[2];
                    double finalValue = Convert.ToDouble(value);
                    resultInNumbers[i] = finalValue; 

                    double hour = Convert.ToDouble(tokens[0]);
                    double minute = Convert.ToDouble(tokens[1]);
                    double seconds = Convert.ToDouble(tokens[2]);

                    //fix this:
                    

                    string[] gapHour = showPostTime[i].Split(" ");
                    double hourGap = Convert.ToDouble(gapHour[0]);

                    // add the minutes
                    double currentMinute = minute;
                    double currentHour = hour + hourGap; 
                    double currentSecond = seconds;

                    if(currentHour >= 24){
                        currentHour = 0;
                    } 
                    string currentMinuteStr = Program.convertToString(currentMinute);
                    string currentHourStr = Program.convertToString(currentHour);
                    string currentSecondStr = Program.convertToString(currentSecond);

                    result = currentHourStr + ":" + currentMinuteStr + ":" + currentSecondStr;
                    resultArray[i] = result;

                }
                
            }
            if(numberOfPosts == 1){
                System.Console.WriteLine("The answer is {0}", result);
                return result;
            }
                
            string currentAns = result;
            double least = 999999;
            int leastIndex = 9999;
             for(int i=0; i<numberOfPosts; i++)
            {
                System.Console.WriteLine(resultArray[i]);
                if(resultInNumbers[i] < least){
                    least = resultInNumbers[i];
                    leastIndex = i;
                }
             /*    if(currentAns != resultArray[i]){
                    System.Console.WriteLine("The answer is impossible, line 147 ");      
                    return "impossible";        
                }
                */        
            }
            System.Console.WriteLine("The answer is {0}, line 167 ", result); 
            return resultArray[leastIndex];
            throw new NotImplementedException();
        }
    }
}
