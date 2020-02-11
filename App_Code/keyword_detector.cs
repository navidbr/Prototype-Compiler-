using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compiler_Project_Phase1
{
    public class Keyword
    {
        StreamReader sr;
        string[] keywords;
        int count;
        public Keyword()
        {
            sr = new StreamReader("F:\\C# Projects\\Compiler Project\\Compiler Project Phase1\\Compiler Project Phase1\\Files\\Keywords.txt");
            keywords = new string[100];
            count = 0;
            while(!sr.EndOfStream)
            {
                keywords[count] = Convert.ToString(sr.ReadLine());
                count++;
            }
            sr.Close();
        }

        public Boolean is_keyword(string word)
        {
            for(int i = 0 ; i <= count ; i++)
            {
                if(word == keywords[i])
                {
                    return true;
                }
            }
            return false;
        }
    }
}