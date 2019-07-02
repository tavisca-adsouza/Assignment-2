using System;
using System.Linq;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        public static List<int> filterToKeeepLowest(List<int> itemList, int criteria, int[,] items){
            int lowest = 999;
            foreach (int item in itemList)
            {
                if(items[item, criteria] < lowest)
                    lowest = items[item, criteria];
                
            }
            List<int> toBeRemoved = new List<int>();

             foreach (var item in itemList)
            {
                
                if (items[item, criteria] > lowest){
                    toBeRemoved.Add(item);
                    
                }
            }
            foreach (int item in toBeRemoved)
            {
                itemList.Remove(item);
            }

            

            return itemList;
        }
        public static List<int> filterToKeepHighest(List<int> itemList, int criteria, int[,] items){
            int highest = -1;
            foreach (int item in itemList)
            {
                if(items[item, criteria] > highest)
                    highest = items[item, criteria];
            }
            List<int> toBeRemoved = new List<int>();
            for (int i = 0; i < itemList.Count; i++)
            {
                int item = itemList[i];
                if (items[item, criteria] != highest)
                    toBeRemoved.Add(item);
            }
            foreach (int item in toBeRemoved)
            {
                itemList.Remove(item);
            }

            return itemList;
        }

        public static void print(List<int> ITEMS){
            System.Console.WriteLine("The items is answer now are:");
            foreach (var item in ITEMS)
            {
                System.Console.WriteLine(item);
            }
            System.Console.WriteLine("\n\n");
        }

        public static void printARR(int[] ITEMS){
            System.Console.WriteLine("The items is answer now are:");
            foreach (var item in ITEMS)
            {
                System.Console.WriteLine(item);
            }
            System.Console.WriteLine("\n\n");
        }

        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            // Add your code here.
            throw new NotImplementedException();
        }
        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            Dictionary<char, int> getCriteria = new Dictionary<char, int>();

            int proteinIndex = 0;
            int carbsIndex = 1;
            int fatIndex = 2;
            int totalCalorieCount = 3;
            int numOfItems = protein.Length;
            int[,] items = new int[numOfItems, 3 + 1]; //extra one to store the total calorei count

            for(int i=0; i<numOfItems; i++){
                items[i, proteinIndex] =        protein[i];
                items[i, carbsIndex] =          carbs[i];
                items[i, fatIndex] =            fat[i];
                items[i, totalCalorieCount] =   protein[i]*5 + carbs[i]*5 + fat[i]*9;
            }

            getCriteria.Add('c', carbsIndex);
            getCriteria.Add('C', carbsIndex);

            getCriteria.Add('p', proteinIndex);
            getCriteria.Add('P', proteinIndex);

            getCriteria.Add('f', fatIndex);
            getCriteria.Add('F', fatIndex);

            getCriteria.Add('t', totalCalorieCount);
            getCriteria.Add('T', totalCalorieCount);
        
            int[] resultArr = new int[numOfItems-1];
            List<int> resultList = new List<int>();
            List<int> ans = new List<int>();

            for (int i1 = 0; i1 < dietPlans.Length; i1++)
            {
                string item = dietPlans[i1];
                ans.Clear();
                for(int i=0; i<numOfItems; i++){
                    ans.Add(i);
                }
                foreach(char c in item){
                    

                    if(Char.IsUpper(c)){
                        
                        int criteriaIndex = getCriteria[c];
                        
                        ans = filterToKeepHighest(ans, criteriaIndex, items);
                        
                        if(ans.Count == 1){
                            resultList.Add(ans[0]);
                            //System.Console.WriteLine("This is for the case: {0} I have added the {1} to the result array", item, resultArr[i1]);
                            break;
                        }
                    }
                    else{
                        int criteriaIndex = getCriteria[c];
                        ans = filterToKeeepLowest(ans, criteriaIndex, items);
                        if(ans.Count == 1){
                            resultList.Add(ans[0]);
                            //System.Console.WriteLine("This is for the case: {0} I have added the {1} to the result array", item, resultArr[i1]);
                            break;
                        }
                    }

                        
                }
                 if(ans.Count > 1){
                    //System.Console.WriteLine("This is a speacial ans.Count>1 case for the case: {0} I have added the {1} to the result array", item, resultArr[i1]);
                    //System.Console.WriteLine(ans[0]);
                    resultList.Add(ans[0]);
                   // System.Console.WriteLine(resultArr[i1]);
                }
            }
            int[] a = resultList.ToArray();
            printARR(a);
            return a;
            //throw new NotImplementedException();
        }
         
    }
}
