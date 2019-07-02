using System;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    class Program
    {
        static void Main(string[] args)
        {
            Test("42*47=1?74", 9);
            Test("4?*47=1974", 2);
            Test("42*?7=1974", 4);
            Test("42*?47=1974", -1);
            Test("2*12?=247", -1);
            Console.ReadKey(true);
        }

        private static void Test(string args, int expected)
        {
            var result = FindDigit(args).Equals(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"{args} : {result}");
        }


        

	static public int GetMissingNumber(string complete, string MissingNumber)
        {
            if(complete.Length != MissingNumber.Length)
                return -1;
            for (int i = 0; i < complete.Length; i++)
            {
                if(MissingNumber[i] == '?'){
                    int ans = complete[i]  - '0';
                    return ans;
                }
            }
            return -1;
        }
        public static int FindDigit(string str){
            string[] tokens = str.Split('=');
            string LHS = tokens[0];
            string RHS = tokens[1];
            

            if(LHS.Contains("?")){
                string[] tokens1 = LHS.Split('*');
                string left = tokens1[0];
                string right = tokens1[1];
                

               
                if(left.Contains("?")){
                    double resultDouble = Convert.ToDouble(RHS);
                    double rightDouble = Convert.ToDouble(right);
                    double whatLeftShouldBe = resultDouble/rightDouble;
                    if((whatLeftShouldBe % 1) != 0 )
                        return -1;
                    string whatLeftShouldBeSTR = Convert.ToString(whatLeftShouldBe);

                    int ans = Program.GetMissingNumber(whatLeftShouldBeSTR, left);
                    return ans;

                }
                else{
                    double resultDouble = Convert.ToDouble(RHS);
                    double leftDouble = Convert.ToDouble(left);
                    double whatRightShouldBe = resultDouble/leftDouble;

                    //checking to see if a integral solution is possible in the first place 
                    if((whatRightShouldBe % 1) != 0 )
                        return -1;
                    string whatRightShouldBeSTR = Convert.ToString(whatRightShouldBe);

                    int ans = Program.GetMissingNumber(whatRightShouldBeSTR, right);
                    return ans;
                }
            }
            else{
                if(RHS[0] == '?')
                    return -1;
                string[] tokens2 = LHS.Split('*');
                int left = Convert.ToInt32(tokens2[0]);
                int right = Convert.ToInt32(tokens2[1]);

                int whatResultShouldBe = left*right;
                string whatResultShouldBeSTR = Convert.ToString(whatResultShouldBe);
                int ans = Program.GetMissingNumber(whatResultShouldBeSTR, RHS);
                return ans;

            }
        }
    }
}
