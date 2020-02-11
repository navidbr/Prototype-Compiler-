using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Compiler_Project_Phase1
{ 
    public class Filee
    {

        public void write_string_to_file(string code) // write code textbox to code.txt
        {
            StreamWriter sw = new StreamWriter("F:\\C# Projects\\Compiler Project\\Compiler Project Phase1\\Compiler Project Phase1\\Files\\Code.txt");
            sw.Write(code);
            sw.Write(' ');
            sw.Close();
        }


        public void write_file_to_file_and_textbox(Form1 form1) // write external code file to code.txt and show it on code textbox 
        {
            string address;
            address = form1.txt_file_address.Text.ToString();
            StreamReader sr = new StreamReader(address);
            string temp = "";
            while (!sr.EndOfStream)
            {
                temp += Convert.ToChar(sr.Read());
            }
            sr.Close();
            StreamWriter sw = new StreamWriter("F:\\C# Projects\\Compiler Project\\Compiler Project Phase1\\Compiler Project Phase1\\Files\\Code.txt");
            sw.Write(temp);
            sw.Close();
            form1.txt_code.Text = temp;
        }
    }


    public class Code_Reader
    {
        StreamReader sr;
        public Code_Reader()
        {
            sr = new StreamReader("F:\\C# Projects\\Compiler Project\\Compiler Project Phase1\\Compiler Project Phase1\\Files\\Code.txt");
        }
        public char read_char_code()
        {
            char temp = ' ';

            if(!sr.EndOfStream)
            {
                temp = Convert.ToChar(sr.Read());

            }
            else
            {
                sr.Close();
                throw new System.Exception("EndOfFile");
            }
            return temp;
        }
    }
}
