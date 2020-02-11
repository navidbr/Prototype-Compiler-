using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Compiler_Project_Phase1
{
    public enum symboltype { Keyword, Operator, Identifier, Literal, Delimiter, Comment };

    struct Token
    {
        public int col;
        public int row;
        public int block;
        public symboltype type;
        public string name;
    }
    public partial class Form1 : Form
    {
        #region variables definition
        int This_char_col;
        int This_char_row;
        int This_block;
        char This_character;
        int[] stack;// baraye tashkhise block
        int sp = 0;
        int used_blocks = 1;//baraye moshakhas kardan shomare block hayi ke gablan estefade shode and
        Token[] token;
        int tokensp = 0;
        int state = 0;
        string This_string;
        int This_string_col;
        int This_string_row;
        Boolean end_of_compile;
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            token = new Token[1000];
            stack = new int[100];
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)  // compile text box
        {
            string code = txt_code.Text.ToString();
            Filee f = new Filee();
            f.write_string_to_file(code);
            start_compile();
        }

        private void button2_Click(object sender, EventArgs e)  // compile file
        {
            Filee f = new Filee();
            f.write_file_to_file_and_textbox(this);
            start_compile();
        }

        private void button3_Click(object sender, EventArgs e)  // open file dialog
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.ShowDialog();
            string address = fd.FileName;
            txt_file_address.Text = address;
        }

        private void txt_code_cursor_changed(object sender, EventArgs e)
        {

            int i = 0;
            int col = 1;
            int row = 1;
            string temp = txt_code.Text.ToString();
            while (i < txt_code.SelectionStart && txt_code.Text!="")
            {
                if (temp[i] == '\n')
                {
                    i += 1;
                    col = 1;
                    row++;
                }
                else
                {
                    if (temp[i] == '\t')
                    {
                        i++;
                        col += 8;
                    }
                    else
                    {
                        i++;
                        col++;
                    }
                }
                
            }

            lbl_col.Text = col.ToString();
            lbl_row.Text = row.ToString();
        }

        public void start_compile()
        {

            #region initialize
            Keyword kw = new Keyword();
            Code_Reader cr = new Code_Reader();

            txt_error.Text = "";
            end_of_compile = false;
            This_char_col = 0;
            This_char_row = 1;
            This_string_col = 0;
            This_string_row = 1;
            This_block = 1;
            sp = 1;
            stack[0] = 1;
            tokensp = 0;
            used_blocks = 1;
            state = 0;
            This_string = "";
            This_character = ' ';
            #endregion

            next_char(cr);


            #region start switch cases
            while (!end_of_compile)
            { 
                switch(state)
                {
                    #region case 0
                    case 0:
                        This_string = "";
                        This_string_col = 0;
                        This_string_row = 1;
                        switch(This_character)
                        {
                            #region case \n
                            case '\n':
                                state = 0;
                                next_char(cr);
                                break;
                            #endregion

                            #region case ' '
                            case ' ':
                                state = 0;
                                next_char(cr);
                                break;
                            #endregion

                            #region case \t
                            case '\t':
                                state = 0;
                                next_char(cr);
                                break;
                            #endregion

                            #region case '
                            case (char)39: // 39 = '
                                state = 21;
                                create_token(This_char_col, This_char_row, This_block, This_character, symboltype.Delimiter);  // return '
                                next_char(cr);
                                break;
                            #endregion

                            #region case "
                            case '"':
                                state = 24;
                                create_token(This_char_col, This_char_row, This_block, This_character, symboltype.Delimiter);  // return "
                                next_char(cr);
                                break;
                            #endregion

                            #region case 0
                            case '0':
                                This_string = "0";
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;
                                state = 15;
                                next_char(cr);
                                break;
                            #endregion

                            #region case /
                            case '/':
                                state = 50;
                                This_string = This_character.ToString();
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;
                                next_char(cr);
                                break;
                            #endregion

                            #region case %
                            case '%':
                                state = 0;
                                This_string = This_character.ToString();
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;
                                next_char(cr);
                                if(This_character=='=')
                                {
                                    This_string += This_character.ToString();
                                    create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Operator); // return %=
                                    next_char(cr);
                                }
                                else
                                {
                                    create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Operator); // return %
                                }
                                break;
                            #endregion

                            #region case *
                            case '*':
                                state = 0;
                                This_string = This_character.ToString();
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;
                                next_char(cr);
                                if (This_character == '=')
                                {
                                    This_string += This_character.ToString();
                                    create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Operator); // return *=
                                    next_char(cr);
                                }
                                else
                                {
                                    create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Operator); // return *
                                }
                                break;
                            #endregion

                            #region case +
                            case '+':
                                state = 0;
                                This_string = This_character.ToString();
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;
                                next_char(cr);
                                if (This_character == '=')
                                {
                                    This_string += This_character.ToString();
                                    create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Operator); // return +=
                                    next_char(cr);
                                }
                                else
                                {
                                    if(This_character=='+')
                                    {
                                        This_string += This_character.ToString();
                                        create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Operator); // return ++
                                        next_char(cr);
                                    }
                                    else
                                    {
                                        create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Operator); // return +
                                    }
                                }
                                break;
                            #endregion

                            #region case -
                            case '-':
                                state = 0;
                                This_string = This_character.ToString();
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;
                                next_char(cr);
                                if (This_character == '=')
                                {
                                    This_string += This_character.ToString();
                                    create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Operator); // return -=
                                    next_char(cr);
                                }
                                else
                                {
                                    if (This_character == '-')
                                    {
                                        This_string += This_character.ToString();
                                        create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Operator); // return --
                                        next_char(cr);
                                    }
                                    else
                                    {
                                        create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Operator); // return -
                                    }
                                }
                                break;
                            #endregion

                            #region case <
                            case '<':
                                state = 0;
                                This_string = This_character.ToString();
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;
                                next_char(cr);
                                if (This_character == '=')
                                {
                                    This_string += This_character.ToString();
                                    create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Operator); // return <=
                                    next_char(cr);
                                }
                                else
                                {
                                    if (This_character == '<')
                                    {
                                        This_string += This_character.ToString();
                                        create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Operator); // return <<
                                        next_char(cr);
                                    }
                                    else
                                    {
                                        create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Operator); // return <
                                    }
                                }
                                break;
                            #endregion

                            #region case > 
                            case '>':
                                state = 0;
                                This_string = This_character.ToString();
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;
                                next_char(cr);
                                if (This_character == '=')
                                {
                                    This_string += This_character.ToString();
                                    create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Operator); // return >=
                                    next_char(cr);
                                }
                                else
                                {
                                    if (This_character == '>')
                                    {
                                        This_string += This_character.ToString();
                                        create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Operator); // return >>
                                        next_char(cr);
                                    }
                                    else
                                    {
                                        create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Operator); // return >
                                    }
                                }
                                break;
                            #endregion
                                
                            #region case = 
                            case '=':
                                state = 0;
                                This_string = This_character.ToString();
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;
                                next_char(cr);
                                if (This_character == '=')
                                {
                                    This_string += This_character.ToString();
                                    create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Operator); // return ==
                                    next_char(cr);
                                }
                                else
                                {
                                    create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Operator); // return =
                                }
                                break;
                            #endregion

                            #region case ! 
                            case '!':
                                state = 0;
                                This_string = This_character.ToString();
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;
                                next_char(cr);
                                if (This_character == '=')
                                {
                                    This_string += This_character.ToString();
                                    create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Operator); // return !=
                                    next_char(cr);
                                }
                                else
                                {
                                    create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Operator); // return !
                                }
                                break;
                            #endregion

                            #region case ~ 
                            case '~':
                                state = 0;
                                This_string = This_character.ToString();
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;
                                create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Operator); // return ~
                                next_char(cr);
                                break; 
                            #endregion

                            #region case |
                            case '|':
                                state = 0;
                                This_string = This_character.ToString();
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;
                                next_char(cr);
                                if (This_character == '|')
                                {
                                    This_string += This_character.ToString();
                                    create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Operator); // return ||
                                    next_char(cr);
                                }
                                else
                                {
                                    create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Operator); // return |
                                }
                                break;
                            #endregion

                            #region case &
                            case '&':
                                state = 0;
                                This_string = This_character.ToString();
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;
                                next_char(cr);
                                if (This_character == '&')
                                {
                                    This_string += This_character.ToString();
                                    create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Operator); // return &&
                                    next_char(cr);
                                }
                                else
                                {
                                    create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Operator); // return &
                                }
                                break;
                            #endregion

                            #region case ^
                            case '^':
                                state = 0;
                                This_string = This_character.ToString();
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;
                                create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Operator); // return ^
                                next_char(cr);
                                break;
                            #endregion

                            #region case ?
                            case '?':
                                state = 0;
                                This_string = This_character.ToString();
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;
                                create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Operator); // return ?
                                next_char(cr);
                                break;
                            #endregion

                            #region case ,
                            case ',':
                                state = 0;
                                This_string = This_character.ToString();
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;
                                create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Delimiter); // return ,
                                next_char(cr);
                                break;
                            #endregion

                            #region case ;
                            case ';':
                                state = 0;
                                This_string = This_character.ToString();
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;
                                create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Delimiter); // return ;
                                next_char(cr);
                                break;
                            #endregion

                            #region case #
                            case '#':
                                state = 0;
                                This_string = This_character.ToString();
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;
                                create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Delimiter); // return #
                                next_char(cr);
                                break;
                            #endregion

                            #region case :
                            case ':':
                                state = 0;
                                This_string = This_character.ToString();
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;
                                create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Delimiter); // return :
                                next_char(cr);
                                break;
                            #endregion

                            #region case }
                            case '}':
                                state = 0;
                                This_string = This_character.ToString();
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;
                                create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Delimiter); // return }
                                sp--;
                                if (sp < 1)
                                {
                                    throw new System.Exception("broket counts not correct");
                                }
                                This_block = stack[sp - 1];
                                next_char(cr);
                                break;
                            #endregion

                            #region case {
                            case '{':
                                used_blocks++;
                                This_block = used_blocks;
                                stack[sp] = This_block;
                                sp++;
                                state = 0;
                                This_string = This_character.ToString();
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;
                                create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Delimiter); // return {
                                next_char(cr);
                                break;
                            #endregion

                            #region case )
                            case ')':
                                state = 0;
                                This_string = This_character.ToString();
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;
                                create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Delimiter); // return )
                                next_char(cr);
                                break;
                            #endregion

                            #region case (
                            case '(':
                                state = 0;
                                This_string = This_character.ToString();
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;
                                create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Delimiter); // return (
                                next_char(cr);
                                break;
                            #endregion

                            #region case [
                            case '[':
                                state = 0;
                                This_string = This_character.ToString();
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;
                                create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Delimiter); // return (
                                next_char(cr);
                                break;
                            #endregion

                            #region case ]
                            case ']':
                                state = 0;
                                This_string = This_character.ToString();
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;
                                create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Delimiter); // return (
                                next_char(cr);
                                break;
                            #endregion

                            #region case \r
                            case '\r':
                                state = 0;
                                next_char(cr);
                                break;
                            #endregion

                            default:
                                if(is_digit_but_no_zero(This_character))
                                {
                                    state = 10;
                                    This_string = This_character.ToString();
                                    This_string_col = This_char_col;
                                    This_string_row = This_char_row;
                                    next_char(cr);
                                }
                                else
                                {
                                    if(is_letter(This_character) || This_character=='_')
                                    {
                                        state = 1;
                                        This_string = This_character.ToString();
                                        This_string_col = This_char_col;
                                        This_string_row = This_char_row;
                                        next_char(cr);
                                    }
                                }
                                break;

                        }
                        break;
                    #endregion

                    #region case 1
                    case 1:
                        if(is_letter(This_character) || is_digit(This_character) || This_character=='_')
                        {
                            state = 1;
                            This_string += This_character.ToString();
                            next_char(cr);
                        }
                        else
                        {
                            if(kw.is_keyword(This_string))
                            {
                                create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Keyword);
                            }
                            else
                            {
                                create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Identifier);
                            }
                            state = 0;
                        }
                        break;
                    #endregion

                    #region case 10
                    case 10:
                        if(is_digit(This_character))
                        {
                            state = 10;
                            This_string += This_character.ToString();
                            next_char(cr);
                        }
                        else
                        {
                            if(This_character=='.')
                            {
                                state = 12;
                                This_string += This_character.ToString();
                                next_char(cr);
                            }
                            else
                            {
                                create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Literal);  //decimal integer
                                state = 0;
                            }
                        }
                        
                        break;
                    #endregion

                    #region case 12
                    case 12:
                        if(is_digit(This_character))
                        {
                            state = 13;
                            This_string += This_character.ToString();
                            next_char(cr);
                        }
                        else
                        {
                            print_error("col:" + This_string_col + " row:" + This_string_row + " Not a Real Number!");
                            state = 0;
                            scape_word_has_error(cr);
                        }
                        break;
                    #endregion

                    #region case 13
                    case 13:
                        if (is_digit(This_character))
                        {
                            state = 13;
                            This_string += This_character.ToString();
                            next_char(cr);
                        }
                        else
                        {
                            create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Literal);  // decimal real number
                            state = 0;
                        }
                        break;
                    #endregion

                    #region case 15
                    case 15:
                        if(is_octal_but_no_zero(This_character))
                        {
                            state = 19;
                            This_string += This_character.ToString();
                            next_char(cr);
                        }
                        else
                        {
                            if(This_character=='x' || This_character=='X')
                            {
                                state = 16;
                                This_string += This_character.ToString();
                                next_char(cr);
                            }
                            else
                            {
                                create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Literal);  // return 0 as a literal
                                state = 0;
                            }
                        }
                        break;
                    #endregion

                    #region case 16
                    case 16:
                        if(is_hex(This_character))
                        {
                            This_string += This_character.ToString();
                            next_char(cr);
                            state = 17;
                        }else
                        {
                            print_error("col:" + This_string_col + " row:" + This_string_row + " not a Hex Number!");
                            state = 0;
                            scape_word_has_error(cr);
                        }
                        break;
                    #endregion

                    #region case 17
                    case 17:
                        if(is_hex(This_character))
                        {
                            This_string += This_character.ToString();
                            next_char(cr);
                            state = 17;
                        }
                        else
                        {
                            create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Literal);  // hexa decimal number
                            state = 0;
                        }
                        break;
                    #endregion

                    #region case 19
                    case 19:
                        if(is_octal(This_character))
                        {
                            This_string += This_character.ToString();
                            state = 19;
                            next_char(cr);
                        }
                        else
                        {
                            create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Literal);  // octal number
                            state = 0;
                        }
                        break;
                    #endregion

                    #region case 21
                    case 21:
                        This_string = This_character.ToString();
                        This_string_col = This_char_col;
                        This_string_row = This_char_row;
                        state = 22;
                        next_char(cr);
                        break;
                    #endregion

                    #region case 22
                    case 22:
                        if(This_character==39)
                        {
                            create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Literal);  // character literal
                            create_token(This_char_col, This_char_row, This_block, This_character, symboltype.Delimiter);  // return '
                            next_char(cr);
                            state = 0;
                        }
                        else
                        {
                            //throw new System.Exception("col:" + This_string_col + " row:" + This_string_row + " not a character");
                            print_error("col:" + This_string_col + " row:" + This_string_row + " not a character!");
                            state = 0;
                            scape_word_has_error(cr);
                            if (This_character == 39) // scape last ' in 'vkgvhb'
                                next_char(cr);
                        }
                        break;
                    #endregion

                    #region case 24
                    case 24:
                        if(This_character!='"')
                        {
                            if(This_string=="")
                            {
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;
                                This_string += This_character.ToString();
                            }
                            else
                            {
                                This_string += This_character.ToString();
                            }
                            next_char(cr);
                            state = 24;
                        }
                        else
                        {
                            create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Literal);  // string literal
                            create_token(This_char_col, This_char_row, This_block, This_character, symboltype.Delimiter);  // return "
                            state = 0;
                            next_char(cr);
                        }
                        break;
                    #endregion

                    #region case 50
                    case 50:
                        switch(This_character)
                        {
                            case '=':
                                create_token(This_string_col, This_string_row, This_block, "/=", symboltype.Operator);  // return /=
                                state = 0;
                                next_char(cr);
                                break;

                            case '*':
                                create_token(This_string_col, This_string_row, This_block, "/*", symboltype.Delimiter); // return /*
                                state = 100;
                                next_char(cr);
                                This_string = "";
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;

                                break;

                            case '/':
                                create_token(This_string_col, This_string_row, This_block, "//", symboltype.Delimiter); // return //
                                state = 108;
                                next_char(cr);
                                This_string = "";
                                This_string_col = This_char_col;
                                This_string_row = This_char_row;
                                break;

                            default:
                                create_token(This_string_col, This_string_row, This_block, '/', symboltype.Operator); // return /
                                state = 0;
                                next_char(cr);
                                break;
                        }
                        break;
                    #endregion

                    #region case 100 
                    case 100:
                        This_string += This_character.ToString();
                        if(This_character=='*')
                        {
                            state = 101;
                            next_char(cr);
                        }
                        else
                        {
                            state = 100;
                            next_char(cr);
                        }
                        break;
                    #endregion

                    #region case 101
                    case 101:
                        switch(This_character)
                        {
                            case '*':
                                state = 101;
                                This_string += This_character.ToString();
                                next_char(cr);
                                break;

                            case '/':
                                state = 0;
                                create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Comment);  // return comment
                                create_token(This_char_col-1, This_char_row, This_block, "*/", symboltype.Delimiter); // return */
                                next_char(cr);
                                break;

                            default:
                                state = 100;
                                This_string += This_character.ToString();
                                next_char(cr);
                                break;
                        }
                        break;
                    #endregion

                    #region case 108
                    case 108:
                        if(This_character=='\n')
                        {
                            create_token(This_string_col, This_string_row, This_block, This_string, symboltype.Comment);  // return comment
                            state = 0;
                            next_char(cr);
                        }
                        else
                        {
                            This_string += This_character.ToString();
                            state = 108;
                            next_char(cr);
                        }
                        break;
                        #endregion

                }
            }
            #endregion

            write_tokens_to_file_and_txt_token();

        }

        public void next_char(Code_Reader cr)
        {
            if (!end_of_compile)
            {
                if (This_character == '\n')
                {
                    This_char_col = 1;
                    This_char_row++;
                }
                else
                {
                    if (This_character == '\t')
                    {
                        This_char_col += 8;
                    }
                    else
                    {
                        This_char_col++;         
                    }
                }
            }
            try
            {
                This_character = cr.read_char_code();
            }
            catch (Exception e)
            {
                if (e.Message == "EndOfFile")
                    end_of_compile = true;
            }
            //if (!end_of_compile)
            //{
            //    if (This_character == '{')
            //    {
                    
            //        used_blocks++;
            //        This_block = used_blocks;
            //        stack[sp] = This_block;
            //        sp++;
            //    }
            //    else
            //    {
            //        if (This_character == '}')
            //        {
                        
            //            sp--;
            //            if (sp < 0)
            //            {
            //                throw new System.Exception("broket counts not correct");
            //            }
            //            This_block = stack[sp-1];
            //        }
            //    }
            //}
        }



        public void create_token(int col, int row, int block, string name, symboltype type)
        {
            token[tokensp].col = col;
            token[tokensp].row = row;
            token[tokensp].block = block;
            token[tokensp].name = name;
            token[tokensp].type = type;
            tokensp++;
        }
        public void create_token(int col, int row, int block, char name, symboltype type)
        {
            token[tokensp].col = col;
            token[tokensp].row = row;
            token[tokensp].block = block;
            token[tokensp].name = name.ToString();
            token[tokensp].type = type;
            tokensp++;
        }





        public Boolean is_letter(char character)
        {
            if (((character <= 122) && (character >= 97)) || ((character <= 90) && (character >= 65)))
                return true;
            else
                return false;
        }


        public Boolean is_digit(char character)
        {
            if ((character <= 57) && (character >= 48)) 
                return true;
            else
                return false;
        }
        public Boolean is_digit_but_no_zero(char character)
        {
            if ((character <= 57) && (character >= 49))
                return true;
            else
                return false;
        }


        public Boolean is_hex(char character)
        {
            if (is_digit(character) || ((character <= 102) && (character >= 97)) || ((character <= 70) && (character >= 65)))
                return true;
            else
                return false;
        }
        public Boolean is_hex_but_no_zero(char character)
        {
            if (is_digit_but_no_zero(character) || ((character <= 102) && (character >= 97)) || ((character <= 70) && (character >= 65)))
                return true;
            else
                return false;
        }


        public Boolean is_octal(char character)
        {
            if ((character <= 55) && (character >= 48))
                return true;
            else
                return false;
        }
        public Boolean is_octal_but_no_zero(char character)
        {
            if ((character <= 55) && (character >= 49))
                return true;
            else
                return false;
        }

        public void print_error(string message)
        {
            txt_error.Text += message;
            txt_error.Text += '\n';
        }

        public void scape_word_has_error(Code_Reader cr)
        {
            while (is_letter(This_character) || is_digit(This_character))
            {
                next_char(cr);
                if (end_of_compile)
                    break;
            }
        }

        public void write_tokens_to_file_and_txt_token()
        {
            txt_token.Text = "";
            StreamWriter sw = new StreamWriter("F:/C# Projects/Compiler Project/Compiler Project Phase1/Compiler Project Phase1/Files/Token.txt");
            for (int i = 0; i < tokensp; i++)
            {
                string temp = " "+token[i].name + '\t' + token[i].type.ToString() + '\t'+"Block : " + token[i].block + '\t'+ "Col : " + token[i].col + '\t' + "Row : " + token[i].row;
                txt_token.Text += temp;
                txt_token.Text += "\r\n".ToString();
                sw.WriteLine(temp);
            }
            sw.Close();
        }



    }
}